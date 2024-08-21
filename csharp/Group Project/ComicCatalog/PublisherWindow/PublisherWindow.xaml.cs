using BusinessLayer;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using DataLayer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComicCatalog.PublisherWindow
{
    /// <summary>
    /// Interaction logic for PublisherWindow.xaml
    /// </summary>
    public partial class PublisherWindow : Page
    {

        private readonly string _connectionString = Properties.Resources.ConnectionString;
        private Publisher _publisher;

        public PublisherWindow()
        {
            InitializeComponent();
            FillPublisherGrid();
        }

        public PublisherWindow(string publisherName) : this()
        {
            txtPublisher.Text = publisherName;
        }

        private void BtnAddPublisher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PublisherManager pm = new PublisherManager(new UnitOfWork(_connectionString));

                Publisher publisher = new Publisher(txtPublisher.Text.Trim());
                MessageBoxResult result = MessageBox.Show("Do you want to add this publisher?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    pm.AddPublisher(publisher);
                    FillPublisherGrid();
                    txtPublisher.Text = "";
                    txtPublisher.Text = publisher.Name;
                    MessageBox.Show("Publisher added!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }    
            }
            catch (PublisherException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnUpdatePublisher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PublisherManager pm = new PublisherManager(new UnitOfWork(_connectionString));
                _publisher.SetName(txtPublisher.Text.Trim());
                MessageBoxResult result = MessageBox.Show("Do you want to update this publisher?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    pm.UpdatePublisher(_publisher);
                    FillPublisherGrid();
                    txtPublisher.Text = "";
                    txtPublisher.Text = _publisher.Name;
                    MessageBox.Show("Publisher updated!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }           
            }
            catch (PublisherException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRemovePublisher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PublisherManager pm = new PublisherManager(new UnitOfWork(_connectionString));
                _publisher = (Publisher)dgPublishers.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Do you want to remove this publisher?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    pm.RemovePublisher(_publisher);
                    FillPublisherGrid();
                    MessageBox.Show("Publisher removed!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }        
            }
            catch (PublisherException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillPublisherGrid()
        {
            PublisherManager pm = new PublisherManager(new UnitOfWork(_connectionString));

            List<Publisher> publishers = pm.GetAllPublishers();

            dgPublishers.Items.Clear();

            foreach (Publisher p in publishers)
            {
                dgPublishers.Items.Add(p);
            }
        }

        private void TxtPublisher_TextChanged(object sender, TextChangedEventArgs e)
        {
            PublisherManager pm = new PublisherManager(new UnitOfWork(_connectionString));

            List<Publisher> publishers = pm.GetAllPublishers().Where(x => x.Name.ToLower().Trim().Contains(txtPublisher.Text.ToLower().Trim())).ToList();

            dgPublishers.Items.Clear();

            foreach (Publisher p in publishers)
            {
                dgPublishers.Items.Add(p);
            }
        }

        private void DgPublishers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if( dgPublishers.SelectedItem != null)
            {
                _publisher = (Publisher)dgPublishers.SelectedItem;
                txtPublisher.Text = _publisher.Name;
            }
        }
    }
}
