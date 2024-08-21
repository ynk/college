using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessLayer;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using DataLayer;
using StockManagement.StockWindow;

namespace StockManagement.OrderWindow
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Page
    {
        private List<ComicResult> _Orderlines = new List<ComicResult>();
        private readonly string _connectionString = Properties.Resources.ConnectionString;
        public OrderWindow()
        {
            InitializeComponent();
            foreach (ComicResult cr in _Orderlines)
            {
                OrderDataGrid.Items.Add(cr);
            }
            dpAddOrderDate.SelectedDate = DateTime.Now;
            //OrderDataGrid.ItemsSource = _Orderlines;
        }
        //openen we het zoek venster en koppelen we de event
        private void btnSearchComic_Click(object sender, RoutedEventArgs e)
        {
            var comicSelect = new ComicSelect();
            comicSelect.ComicSelected += StockWindowOnComicSelected;
            comicSelect.ShowDialog();
        }
        private ComicResult _SelectedComic;
        //we vangen het event van het zoek venster op in een field en vullen text boek in
        private void StockWindowOnComicSelected(object? sender, StockWindow.StockWindow.ComicSelectedEventArgs e) //StockWindow.StockWindow.ComicSelectedEventArgs
        {
            _SelectedComic = e.ComicResult;

            txtSearchComic.Text = $"{_SelectedComic.Title} {_SelectedComic.ToString()}";

        }
        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OrderDataGrid.Items.Count <= 0) throw new OrderException("There are no comics selected.");
                var order = new Order();
                var uow = new UnitOfWork(_connectionString);
                var stockManager = new StockManager(uow);
                var comicManager = new ComicManager(uow);
                if (dpAddOrderDate.SelectedDate == null || dpAddOrderDate.SelectedDate < DateTime.Now.Date) throw new OrderException("The date cannot be in the past.");
                order.OrderDate = dpAddOrderDate.SelectedDate.Value;

                order.OrderLines = new List<OrderComic>();
                foreach (var orderLine in _Orderlines)
                {
                    var oComic = new OrderComic();
                    oComic.Comic = comicManager.GetComic(orderLine.ComicId);
                    oComic.Aantal = orderLine.Stock;
                    oComic.Order = order;
                    order.OrderLines.Add(oComic);
                }
                MessageBoxResult result = MessageBox.Show("Do you want to add this order?", "Question?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    stockManager.AddOrder(order);
                    dpAddOrderDate.SelectedDate = DateTime.Now;
                    OrderDataGrid.Items.Clear();
                    OrderDataGrid.Items.Clear();
                    _Orderlines = new List<ComicResult>();
                    OrderDataGrid.Items.Refresh();
                    MessageBox.Show("The order is added", "Confirmation!", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnSearchOrderComics_Click(object sender, RoutedEventArgs e)
        {
            var comicSelect = new ComicSelect();
            comicSelect.ComicSelected += StockWindowOnComicSelected;
            comicSelect.ShowDialog();
        }
        private void btnAddComicForOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_SelectedComic == null || string.IsNullOrEmpty(txtQuantity.Text)) return;

                var com = _Orderlines.FirstOrDefault(x => x.ComicId == _SelectedComic.ComicId);
                if (com == null)
                {
                    if (int.Parse(txtQuantity.Text) <= 0) throw new OrderException("The quantity cannot be zero.");
                    _SelectedComic.Stock = Convert.ToInt32(txtQuantity.Text);
                    OrderDataGrid.Items.Add(_SelectedComic);
                    _Orderlines.Add(_SelectedComic);
                }
                else
                {
                    com.Stock += Convert.ToInt32(txtQuantity.Text);
                    if (com.Stock <= 0)
                    {
                        _Orderlines.Remove(com);
                        OrderDataGrid.Items.Remove(com);
                    }
                }

                _SelectedComic = null;
                OrderDataGrid.Items.Refresh();
                txtQuantity.Text = String.Empty;
                txtSearchComic.Text = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void btnRemoveComicFromOrder_Click(object sender, RoutedEventArgs e)
        {
            ComicResult selectedComic = (ComicResult)OrderDataGrid.SelectedItem;
            OrderDataGrid.Items.Remove(selectedComic);
        }
    }
}
