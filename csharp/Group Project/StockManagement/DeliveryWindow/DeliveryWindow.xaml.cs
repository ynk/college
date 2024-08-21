using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using DataLayer;
using StockManagement.StockWindow;

namespace StockManagement.DeliveryWindow
{
    /// <summary>
    /// Interaction logic for DeliveryWindow.xaml
    /// </summary>
    public partial class DeliveryWindow : Page
    {
        private List<ComicResult> _DeliveryLines = new List<ComicResult>();
        private readonly string _connectionString = Properties.Resources.ConnectionString;
        public DeliveryWindow()
        {
            InitializeComponent();
            foreach(ComicResult cr in _DeliveryLines)
            {
                dgAddDelivery.Items.Add(cr);
            }
            //dgAddDelivery.ItemsSource = _DeliveryLines;
            dpAddDeliveryDate.SelectedDate = DateTime.Now;
        }

        //we voegen de delivery toe aan de databank
        private void btnAddAddDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgAddDelivery.Items.Count <= 0) throw new DeliveryException("There are no comics selected.");
                var delivery = new Delivery();
                var uow = new UnitOfWork(_connectionString);
                var stockManager = new BusinessLayer.StockManager(uow);
                var comicManager = new BusinessLayer.ComicManager(uow);
                if (dpAddDateOfReceipt.SelectedDate == null ||dpAddDateOfReceipt.SelectedDate.Value < DateTime.Now) throw new DeliveryException("The date cannot be in the past.");
                delivery.DatumLevering = dpAddDeliveryDate.SelectedDate.Value;
                delivery.DatumOntvangst = dpAddDateOfReceipt.SelectedDate.Value;
                delivery.DeliveryLines = new List<DeliveryComic>();
                foreach (var deliveryLine in _DeliveryLines)
                {
                    var dlComic = new DeliveryComic();
                    dlComic.Comic = comicManager.GetComic(deliveryLine.ComicId);
                    dlComic.Aantal = deliveryLine.Stock;
                    dlComic.Delivery = delivery;
                    delivery.DeliveryLines.Add(dlComic);
                }
                
                MessageBoxResult result =  MessageBox.Show("Do you want to add this delivery?", "Question?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if(result == MessageBoxResult.Yes)
                {
                    stockManager.AddDelivery(delivery);
                    dpAddDeliveryDate.SelectedDate = DateTime.Now;
                    dpAddDateOfReceipt.Text = String.Empty;
                    _DeliveryLines = new List<ComicResult>();
                    dgAddDelivery.Items.Clear();
                    dgAddDelivery.Items.Refresh();
                    MessageBox.Show("The delivery is added", "Confirmation!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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

        //we voegen een lijn toe aan het grid met een comic en quantity
        private void btnAddComicForDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_SelectedComic == null || string.IsNullOrEmpty(txtQuantity.Text)) return;

                var com = _DeliveryLines.FirstOrDefault(x => x.ComicId == _SelectedComic.ComicId);
                if (com == null)
                {
                    if (int.Parse(txtQuantity.Text) <= 0) throw new DeliveryException("The quantity cannot be zero.");
                    _SelectedComic.Stock = Convert.ToInt32(txtQuantity.Text);
                    _DeliveryLines.Add(_SelectedComic);
                    dgAddDelivery.Items.Add(_SelectedComic);
                }
                else
                {
                    com.Stock += Convert.ToInt32(txtQuantity.Text);
                    if (com.Stock <= 0)
                    {
                        _DeliveryLines.Remove(com);
                        dgAddDelivery.Items.Remove(com);
                    }
                }

                _SelectedComic = null;
                dgAddDelivery.Items.Refresh();
                txtQuantity.Text = String.Empty;
                txtSearchComic.Text = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
           
        }

        private void btnRemoveComicFromDelivery_Click(object sender, RoutedEventArgs e)
        {
            ComicResult comicResult = (ComicResult)dgAddDelivery.SelectedItem;
            dgAddDelivery.Items.Remove(comicResult);
        }
    }
}
