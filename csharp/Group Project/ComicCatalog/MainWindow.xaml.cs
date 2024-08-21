using System.Windows;
using BusinessLayer;
using DataLayer;

namespace ComicCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnComics.IsEnabled = false;
            MainFrame.Content = new ComicWindow.ComicWindow();
        }

        private void OpenComicWindow(object sender, RoutedEventArgs e)
        {
            ComicWindow.ComicWindow comicWindow = new ComicWindow.ComicWindow();
            btnComics.IsEnabled = false;
            btnAuthors.IsEnabled = true;
            btnPublishers.IsEnabled = true;
            btnSeries.IsEnabled = true;
            MainFrame.Content = comicWindow;
        }

        private void OpenAuthorWindow(object sender, RoutedEventArgs e)
        {
            AuthorWindow.AuthorWindow authorWindow = new AuthorWindow.AuthorWindow();
            btnComics.IsEnabled = true;
            btnAuthors.IsEnabled = false;
            btnPublishers.IsEnabled = true;
            btnSeries.IsEnabled = true;
            MainFrame.Content = authorWindow;

        }

        private void OpenPublisherWindow(object sender, RoutedEventArgs e)
        {
            PublisherWindow.PublisherWindow publisherWindow = new PublisherWindow.PublisherWindow();
            btnComics.IsEnabled = true;
            btnAuthors.IsEnabled = true;
            btnPublishers.IsEnabled = false;
            btnSeries.IsEnabled = true;
            MainFrame.Content = publisherWindow;
        }

        private void OpenSerieWindow(object sender, RoutedEventArgs e)
        {
            SerieWindow.SerieWindow serieWindow = new SerieWindow.SerieWindow();
            btnComics.IsEnabled = true;
            btnAuthors.IsEnabled = true;
            btnPublishers.IsEnabled = true;
            btnSeries.IsEnabled = false;
            MainFrame.Content = serieWindow;
        }
    }
}
