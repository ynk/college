using BusinessLayer;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using DataLayer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComicCatalog.SerieWindow
{
    /// <summary>
    /// Interaction logic for SerieWindow.xaml
    /// </summary>
    public partial class SerieWindow : Page
    {

        private readonly string _connectionString = Properties.Resources.ConnectionString;
        private Serie _serie;

        public SerieWindow()
        {
            InitializeComponent();
            FillSeriesGrid();
        }

        public SerieWindow(string serieName) : this()
        {
            txtSerie.Text = serieName;
        }

        private void FillSeriesGrid()
        {
            SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));

            dgSerie.Items.Clear();

            List<Serie> series = sm.GetAllSeries();

            foreach (Serie s in series)
            {
                dgSerie.Items.Add(s);
            }
        }

        private void TxtSerie_TextChanged(object sender, TextChangedEventArgs e)
        {
            SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));

            List<Serie> series = sm.GetAllSeries().Where(x => x.Name.ToLower().Trim().Contains(txtSerie.Text.ToLower().Trim())).ToList();

            dgSerie.Items.Clear();

            foreach (Serie s in series)
            {
                dgSerie.Items.Add(s);
            }
        }

        private void BtnAddSerie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));

                Serie serie = new Serie(txtSerie.Text.Trim());

                MessageBoxResult result = MessageBox.Show("Do you want to add this series?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    sm.AddSerie(serie);
                    FillSeriesGrid();
                    txtSerie.Text = "";
                    txtSerie.Text = serie.Name;
                    MessageBox.Show("Series added!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                } 
            }
            catch (SerieException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdateSerie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));
                _serie.SetName(txtSerie.Text.Trim());

                MessageBoxResult result = MessageBox.Show("Do you want to update this series?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    sm.UpdateSerie(_serie);
                    FillSeriesGrid();
                    txtSerie.Text = "";
                    txtSerie.Text = _serie.Name;
                    MessageBox.Show("Series updated!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }           
            }
            catch (SerieException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRemoveSerie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SerieManager sm = new SerieManager(new UnitOfWork(_connectionString));

                MessageBoxResult result = MessageBox.Show("Do you want to remove this series?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    sm.RemoveSerie(_serie);
                    FillSeriesGrid();
                    txtSerie.Clear();
                    MessageBox.Show("Serie removed!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }          
            }
            catch (SerieException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgSerie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSerie.SelectedItem != null)
            {
                _serie = (Serie)dgSerie.SelectedItem;
                txtSerie.Text = _serie.Name;
            }
        }
    }
}
