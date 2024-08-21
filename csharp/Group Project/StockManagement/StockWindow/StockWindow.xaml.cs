using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessLayer;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using DataLayer;

namespace StockManagement.StockWindow
{
    /// <summary>
    /// Interaction logic for StockWindow.xaml
    /// </summary>
    public partial class StockWindow : Page
    {
    private string connectionString = Properties.Resources.ConnectionString;

        private ComicResult cr;

        public StockWindow()
        {
            InitializeComponent();
            ReloadSearchComicsGrid();
        }

        //we maken een custom eventArgs aan om de comicresult mee te kunnen sturen
        public class ComicSelectedEventArgs : EventArgs
        {
            public ComicSelectedEventArgs(ComicResult comicResult)
            {
                ComicResult = comicResult;
            }

            public ComicResult ComicResult { get;  set; }
        }

        public event EventHandler<ComicSelectedEventArgs> ComicSelected;

        // Het legen van alle inputvelden + datagrid 
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            
            if ((Button)sender == btnSearchClear)
            {
                ClearFieldsAndDatagrid();
            }
        }

        // Legen van tekstvelden en datagrid in search
        private void ClearFieldsAndDatagrid()
        {
            txtSearchTitle.Clear();
            txtSearchPublisher.Clear();
            txtSearchSerie.Clear();
            txtSearchSerieNumber.Clear();
            txtSearchAuthor.Clear();
            dgSearchComics.SelectedItem = null;
        }

        // Het laden van de datagrid bij het aanklikken de search tab
        private void ReloadSearchComicsGrid()
        {
            ComicManager cm = new ComicManager(new UnitOfWork(connectionString));

            List<ComicResult> allComics = cm.SearchComics(null, null, null, null, null).OrderBy(x => x.ComicId).Take(50).ToList();

            ClearFieldsAndDatagrid();

            dgSearchComics.Items.Clear();

            if (allComics.Count > 0)
            {
                foreach (ComicResult result in allComics)
                {
                    result.AuthorNames = result.ToString();
                    dgSearchComics.Items.Add(result);
                }
            }
        }

        // Als er een comic geselecteerd dit laten weten aan gebruikers 
        private void Datagrid_Comics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSearchComics.SelectedItem != null)
            {
                
                cr = (ComicResult)dgSearchComics.SelectedItem;
                OnComicSelected(new ComicSelectedEventArgs(cr));
            }
        }
        
        //Event raisen, als er een comic geselecteerd word, doen we er iets mee als er een gebruiker is op het event 
        protected virtual void OnComicSelected(ComicSelectedEventArgs e)
        {
            //scherm wordt herbruikt maar heeft niet altijd een gebruik, dus in geval van null wordt event niet gebruikt
            ComicSelected?.Invoke(this, e);
            
        }

        //Filteren van de searchComics
        private void FilterComics(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSearchTitle.Text.Length > 2 || txtSearchAuthor.Text.Length > 2 || txtSearchPublisher.Text.Length > 2 || txtSearchSerie.Text.Length > 2)
                {
                    dgSearchComics.Items.Clear();
                    ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
                    string title = txtSearchTitle.Text;
                    string author = txtSearchAuthor.Text;
                    string publisher = txtSearchPublisher.Text;
                    string serie = txtSearchSerie.Text;
                    List<ComicResult> comics = cm.SearchComics(title, author, serie, publisher, null).Take(50).ToList();
                    foreach (ComicResult comicResult in comics)
                    {
                        comicResult.AuthorNames = comicResult.ToString();
                        dgSearchComics.Items.Add(comicResult);
                    }
                }
                else if (txtSearchTitle.Text.Length == 0 && txtSearchAuthor.Text.Length == 0 && txtSearchPublisher.Text.Length == 0 && txtSearchSerie.Text.Length == 0)
                {
                    ReloadSearchComicsGrid();
                }
            }
            catch (ComicException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}