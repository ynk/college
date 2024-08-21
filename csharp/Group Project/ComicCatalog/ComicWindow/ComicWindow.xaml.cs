using BusinessLayer;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComicCatalog.ComicWindow
{
    /// <summary>
    /// Interaction logic for ComicWindow.xaml
    /// </summary>
    public partial class ComicWindow : Page
    {
        string connectionString = Properties.Resources.ConnectionString;
        private ComicResult cr;
        private Author _author;

        public ComicWindow()
        {
            InitializeComponent();
            ReloadSearchComicsGrid();
        }

        // Toevoegen van author in grid, checken of author al bestaat in db & in grid.
        private void BtnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
            string authorName = txtAddAuthor.Text.Trim();

            if (dgAuthorsFound.SelectedItem != null)
            {
                _author = (Author)dgAuthorsFound.SelectedItem;
                authorName = _author.Name;
            }

            if (am.DoesAuthorExist(authorName))
            {
                _author = am.GetAllAuthors().Where(x => x.Name.ToLower() == authorName.ToLower().Trim()).FirstOrDefault();
                if (!dgAuthorsToAdd.Items.Contains(_author))
                {
                    dgAuthorsToAdd.Items.Add(_author);
                }
                else
                {
                    MessageBox.Show("Author already added for this comic.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Author does not yet exist.\nDo you want to add it now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    AuthorWindow.AuthorWindow authorWindow = new AuthorWindow.AuthorWindow(authorName);
                    MainWindow main = new MainWindow();
                    main.MainFrame.Content = authorWindow;
                    main.btnComics.IsEnabled = true;
                    main.btnAuthors.IsEnabled = false;
                    main.Show();
                }
            }
        }

        // Het verwijderen van meerdere auteurs uit de datagrid tegelijk is mogelijk
        private void BtnRemoveAuthor_Click(object sender, RoutedEventArgs e)
        {
            if ((Button)sender == btnRemoveAuthor)
            {
                if (dgAuthorsToAdd.SelectedItems.Count > 0)
                {
                    for (int i = dgAuthorsToAdd.SelectedItems.Count; i > 0; i--)
                    {
                        dgAuthorsToAdd.Items.Remove(dgAuthorsToAdd.SelectedItems[i - 1]);
                    }
                }
            }
            if ((Button)sender == btnSearchRemoveAuthor)
            {
                if (dgAuthorsToUpdate.SelectedItems.Count > 0)
                {
                    for (int i = dgAuthorsToUpdate.SelectedItems.Count; i > 0; i--)
                    {
                        dgAuthorsToUpdate.Items.Remove(dgAuthorsToUpdate.SelectedItems[i - 1]);
                        txtSearchAuthor.Clear();
                    }
                }
            }
        }

        // Meenemen van Publisher name naar de Publisher page
        private void BtnAddPublisher_Click(object sender, RoutedEventArgs e)
        {
            PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
            string publisherName = "";
            if((Button)sender == btnsearchAddNewPublisher)
            {
                publisherName = txtSearchPublisher.Text.Trim();
            }
            else if((Button)sender == btnAddPublisher)
            {
                publisherName = txtAddPublisher.Text.Trim();
            }
            bool doesPublisherExist = pm.DoesPublisherExist(publisherName);
            if (!doesPublisherExist)
            {
                MessageBoxResult result = MessageBox.Show("Publisher does not exist yet.\nDo you want to add it now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PublisherWindow.PublisherWindow publisherWindow = new PublisherWindow.PublisherWindow(publisherName);
                    MainWindow main = new MainWindow();
                    main.MainFrame.Content = publisherWindow;
                    main.btnComics.IsEnabled = true;
                    main.btnPublishers.IsEnabled = false;
                    main.Show();
                }
            }
            else
            {
                MessageBox.Show("The Publisher already exist.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Meenemen van Serie name naar de Serie page
        private void BtnAddSerie_Click(object sender, RoutedEventArgs e)
        {
            SerieManager sm = new SerieManager(new UnitOfWork(connectionString));
            string serieName = "";
            if ((Button)sender == btnSearchAddNewSerie)
            {
                serieName = txtSearchSerie.Text.Trim();
            }else if((Button)sender == btnAddSerie)
            {
                serieName = txtAddSerie.Text.Trim();
            }
            
            bool doesSerieExist = sm.GetAllSeries().Any(x => x.Name.ToLower().Trim() == serieName.ToLower().Trim());
            if (!doesSerieExist)
            {
                MessageBoxResult result = MessageBox.Show("Serie does not exist yet.\nDo you want to add it now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SerieWindow.SerieWindow serieWindow = new SerieWindow.SerieWindow(serieName);
                    MainWindow main = new MainWindow();
                    main.MainFrame.Content = serieWindow;
                    main.btnSeries.IsEnabled = false;
                    main.btnComics.IsEnabled = true;
                    main.Show();
                }
            }
            else
            {
                MessageBox.Show("The serie already exist.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Het legen van alle inputvelden + datagrid 
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            if ((Button)sender == btnClear)
            {
                ClearFieldsAndDatagridInAdd();
            }

            if ((Button)sender == btnSearchClear)
            {
                ClearFieldsAndDatagrid();
            }
        }

        // Legen van tekstvelden en datagrid in add
        private void ClearFieldsAndDatagridInAdd()
        {
            txtAddTitle.Clear();
            txtAddAuthor.Clear();
            txtAddPublisher.Clear();
            txtAddSerie.Clear();
            txtAddSerieNumber.Clear();
            dgAuthorsToAdd.Items.Clear();
            dgAuthorsToAdd.SelectedItem = null;
        }

        // Legen van tekstvelden en datagrid in search
        private void ClearFieldsAndDatagrid()
        {
            txtSearchTitle.Clear();
            txtSearchPublisher.Clear();
            txtSearchSerie.Clear();
            txtSearchSerieNumber.Clear();
            txtSearchAuthor.Clear();
            dgAuthorsToUpdate.SelectedItem = null;
            dgSearchComics.SelectedItem = null;
            dgAuthorsToUpdate.Items.Clear();
            dgSearchAuthorsFound.Visibility = Visibility.Hidden;
            dgSearchPublishersFound.Visibility = Visibility.Hidden;
            dgSearchSerieFound.Visibility = Visibility.Hidden;
        }

        // Het toevoegen van een comic + checks
        private void BtnAddComic_Click(object sender, RoutedEventArgs e)
        {
            PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
            SerieManager sm = new SerieManager(new UnitOfWork(connectionString));
            ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
            try
            {
                string title = txtAddTitle.Text.Trim();
                List<Author> authors = new List<Author>();
                foreach (Author a in dgAuthorsToAdd.Items)
                {
                    authors.Add(a);
                }
                Publisher publisher = pm.GetAllPublishers().Where(x => x.Name.ToLower().Trim() == txtAddPublisher.Text.ToLower().Trim()).FirstOrDefault();
                Serie serie = sm.GetAllSeries().Where(x => x.Name.ToLower().Trim() == txtAddSerie.Text.ToLower().Trim()).FirstOrDefault();
                int? serieSeqNumber = null;
                if (!string.IsNullOrWhiteSpace(txtAddSerieNumber.Text.Trim()))
                {
                    serieSeqNumber = int.Parse(txtAddSerieNumber.Text.Trim());
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to add this comic?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    cm.AddComic(new Comic(title, publisher, serie, authors, serieSeqNumber));
                    ClearFieldsAndDatagridInAdd();
                    ReloadSearchComicsGrid();
                    dgPublishersFound.Visibility = Visibility.Hidden;
                    dgSeriesFound.Visibility = Visibility.Hidden;
                    dgAuthorsFound.Visibility = Visibility.Hidden;
                    MessageBox.Show("Comic is added!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (ComicException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        // Het laden van de datagrid bij het aanklikken de search tab
        private void ReloadSearchComicsGrid()
        {
            ComicManager cm = new ComicManager(new UnitOfWork(connectionString));

            List<ComicResult> allComics = cm.SearchComics(null, null, null, null, null).OrderBy(x => x.ComicId).Take(25).ToList();

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

        // Invullen van inputvelden en auteur datagrid in het search scherm op basis van de selectie in de datagrid
        private void Datagrid_Comics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSearchComics.SelectedItem != null)
            {
                dgAuthorsToUpdate.Items.Clear();
                cr = (ComicResult)dgSearchComics.SelectedItem;
                txtSearchTitle.Text = cr.Title;
                foreach (string authorName in cr.AuthorName)
                {
                    Author author = new Author(authorName);
                    dgAuthorsToUpdate.Items.Add(author);
                }
                txtSearchPublisher.Text = cr.PublisherName;
                txtSearchSerie.Text = cr.SerieName;
                txtSearchSerieNumber.Text = cr.SerieSeqNumber.ToString();
            }
        }

        // Invullen van Auteur naam in het auteurveld van de keuze uit het auteur datagrid
        private void DgAuthorsToUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAuthorsToUpdate.SelectedItem != null)
            {
                Author author = (Author)dgAuthorsToUpdate.SelectedItem;
                txtSearchAuthor.Text = author.Name;
            }
        }

        // Update van auteurs + checks
        private void BtnUpdateAuthor_Click(object sender, RoutedEventArgs e)
        {
            AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
            string authorName = txtSearchAuthor.Text.Trim();
            Author author = am.GetAllAuthors().Where(x => x.Name.ToLower() == authorName.ToLower()).FirstOrDefault();
            if (author != null)
            {
                List<Author> authors = dgAuthorsToUpdate.Items.Cast<Author>().ToList();
                if (!dgAuthorsToUpdate.Items.Contains(author))
                {
                    dgAuthorsToUpdate.Items.Add(author);
                    txtSearchAuthor.Clear();
                }
                else
                {
                    MessageBox.Show("Author already added for this comic.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Author does not exist.\nDo you want it now?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    AuthorWindow.AuthorWindow authorWindow = new AuthorWindow.AuthorWindow(authorName);
                    MainWindow main = new MainWindow();
                    main.MainFrame.Content = authorWindow;
                    main.btnComics.IsEnabled = true;
                    main.btnAuthors.IsEnabled = false;
                    main.Show();
                }
            }
        }

        // Het verwijderen van de geselecteerde comics
        private void BtnSearchComic_Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
                List<Comic> comics = new List<Comic>();
                foreach (ComicResult cr in dgSearchComics.SelectedItems)
                {
                    Comic c = cm.AllComics.Where(x => x.Id == cr.ComicId).FirstOrDefault();
                    comics.Add(c);
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this comic?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    cm.RemoveComic(comics);
                    ReloadSearchComicsGrid();
                    dgSearchAuthorsFound.Visibility = Visibility.Hidden;
                    dgSearchPublishersFound.Visibility = Visibility.Hidden;
                    dgSearchSerieFound.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Autocomplete opvullen
        private void AutocompleteResults_Changed(object sender, TextChangedEventArgs e)
        {
            if (txtAddAuthor.IsFocused)
            {
                dgPublishersFound.Visibility = Visibility.Hidden;
                dgSeriesFound.Visibility = Visibility.Hidden;
                dgAuthorsFound.Visibility = Visibility.Visible;
                if (txtAddAuthor.Text.Length > 2)
                {
                    AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
                    List<Author> allAuthors = am.GetAllAuthors().Where(x => x.Name.ToLower().Contains(txtAddAuthor.Text.ToLower().Trim())).ToList();
                    dgAuthorsFound.Items.Clear();
                    foreach (Author author in allAuthors)
                    {
                        dgAuthorsFound.Items.Add(author);
                    }
                }
                else if (txtAddAuthor.Text.Length <= 2)
                {
                    dgAuthorsFound.Items.Clear();
                }
            }
            if (txtAddPublisher.IsFocused)
            {
                dgAuthorsFound.Visibility = Visibility.Hidden;
                dgSeriesFound.Visibility = Visibility.Hidden;
                dgPublishersFound.Visibility = Visibility.Visible;
                if (txtAddPublisher.Text.Length > 2)
                {
                    PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                    List<Publisher> allPublishers = pm.GetAllPublishers().Where(x => x.Name.ToLower().Contains(txtAddPublisher.Text.ToLower().Trim())).ToList();
                    dgPublishersFound.Items.Clear();
                    foreach (Publisher publisher in allPublishers)
                    {
                        dgPublishersFound.Items.Add(publisher);
                    }
                }
                else if (txtAddPublisher.Text.Length <= 2)
                {
                    dgPublishersFound.Items.Clear();
                }
            }

            if (txtAddSerie.IsFocused)
            {
                dgAuthorsFound.Visibility = Visibility.Hidden;
                dgPublishersFound.Visibility = Visibility.Hidden;
                dgSeriesFound.Visibility = Visibility.Visible;
                if (txtAddSerie.Text.Length > 2)
                {
                    SerieManager sm = new SerieManager(new UnitOfWork(connectionString));
                    List<Serie> allSeries = sm.GetAllSeries().Where(x => x.Name.ToLower().Contains(txtAddSerie.Text.ToLower().Trim())).ToList();
                    dgSeriesFound.Items.Clear();
                    foreach (Serie serie in allSeries)
                    {
                        dgSeriesFound.Items.Add(serie);
                    }
                }
                else if (txtAddSerie.Text.Length <= 2)
                {
                    dgSeriesFound.Items.Clear();
                }
            }
        }

        //Opvullen van txtAddPublishers en txtAddSerie
        private void DgSelectionAutocomplete_changed(object sender, SelectionChangedEventArgs e)
        {
            if (dgPublishersFound.SelectedItem != null)
            {
                Publisher publisher = (Publisher)dgPublishersFound.SelectedItem;
                txtAddPublisher.Text = publisher.Name;
            }
            if (dgSeriesFound.SelectedItem != null)
            {
                Serie serie = (Serie)dgSeriesFound.SelectedItem;
                txtAddSerie.Text = serie.Name;
            }
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
                    List<ComicResult> comics = cm.SearchComics(title, author, serie, publisher, null);
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

                if (txtSearchAuthor.IsFocused)
                {
                    dgSearchAuthorsFound.Visibility = Visibility.Visible;
                    dgSearchPublishersFound.Visibility = Visibility.Hidden;
                    dgSearchSerieFound.Visibility = Visibility.Hidden;
                    if (txtSearchAuthor.Text.Length > 2)
                    {
                        AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
                        List<Author> allAuthors = am.GetAllAuthors().Where(x => x.Name.ToLower().Contains(txtSearchAuthor.Text.ToLower().Trim())).ToList();
                        dgSearchAuthorsFound.Items.Clear();
                        foreach (Author author in allAuthors)
                        {
                            dgSearchAuthorsFound.Items.Add(author);
                        }
                    }
                    else if (txtSearchAuthor.Text.Length <= 2)
                    {
                        dgSearchAuthorsFound.Items.Clear();
                        dgSearchAuthorsFound.Visibility = Visibility.Hidden;
                    }
                }
                if (txtSearchPublisher.IsFocused)
                {
                    dgSearchAuthorsFound.Visibility = Visibility.Hidden;
                    dgSearchPublishersFound.Visibility = Visibility.Visible;
                    dgSearchSerieFound.Visibility = Visibility.Hidden;
                    if (txtSearchPublisher.Text.Length > 2)
                    {
                        PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
                        List<Publisher> allPublishers = pm.GetAllPublishers().Where(x => x.Name.ToLower().Contains(txtSearchPublisher.Text.ToLower().Trim())).ToList();
                        dgSearchPublishersFound.Items.Clear();
                        foreach (Publisher publisher in allPublishers)
                        {
                            dgSearchPublishersFound.Items.Add(publisher);
                        }
                    }
                    else if (txtSearchPublisher.Text.Length <= 2)
                    {
                        dgSearchPublishersFound.Items.Clear();
                        dgSearchPublishersFound.Visibility = Visibility.Hidden;
                    }
                }
                if (txtSearchSerie.IsFocused)
                {
                    dgSearchAuthorsFound.Visibility = Visibility.Hidden;
                    dgSearchPublishersFound.Visibility = Visibility.Hidden;
                    dgSearchSerieFound.Visibility = Visibility.Visible;
                    if (txtSearchSerie.Text.Length > 2)
                    {
                        SerieManager sm = new SerieManager(new UnitOfWork(connectionString));
                        List<Serie> allSeries = sm.GetAllSeries().Where(x => x.Name.ToLower().Contains(txtSearchSerie.Text.ToLower().Trim())).ToList();
                        dgSearchSerieFound.Items.Clear();
                        foreach (Serie serie in allSeries)
                        {
                            dgSearchSerieFound.Items.Add(serie);
                        }
                    }
                    else if (txtSearchSerie.Text.Length <= 2)
                    {
                        dgSearchSerieFound.Items.Clear();
                        dgSearchSerieFound.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch (ComicException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        //Updaten van de comic
        private void BtnUpdateComic_Click(object sender, RoutedEventArgs e)
        {
            ComicManager cm = new ComicManager(new UnitOfWork(connectionString));
            PublisherManager pm = new PublisherManager(new UnitOfWork(connectionString));
            SerieManager sm = new SerieManager(new UnitOfWork(connectionString));
            AuthorManager am = new AuthorManager(new UnitOfWork(connectionString));
            ComicResult comicResult = cr;
            Publisher publisher = pm.GetAllPublishers().Where(x => x.Name.ToLower().Trim() == txtSearchPublisher.Text.ToLower().Trim()).FirstOrDefault();
            Serie serie = sm.GetAllSeries().Where(x => x.Name.ToLower().Trim() == txtSearchSerie.Text.ToLower().Trim()).FirstOrDefault();
            List<Author> authors = new List<Author>();
            foreach (Author authorInDg in dgAuthorsToUpdate.Items)
            {
                Author author = am.GetAllAuthors().Where(x => x.Name.ToLower().Trim() == authorInDg.Name.ToLower().Trim()).FirstOrDefault();
                authors.Add(author);
            }
            int? serieSeqNumber = null;
            if (int.TryParse(txtSearchSerieNumber.Text, out int result))
            {
                serieSeqNumber = result;
            }
            
            Comic comic = new Comic(comicResult.ComicId, txtSearchTitle.Text.Trim(), publisher, serie, authors, serieSeqNumber, cr.Stock);
            cm.UpdateComic(comic);
            ReloadSearchComicsGrid();

            cr = cm.SearchComics(null, null, null, null, null).FirstOrDefault(x => x.ComicId == comic.Id);
            txtSearchTitle.Text = cr.Title;
            txtSearchSerie.Text = cr.SerieName;
            txtSearchPublisher.Text = cr.PublisherName;
            foreach (string authorName in cr.AuthorName)
            {
                Author author = new Author(authorName);
                dgAuthorsToUpdate.Items.Add(author);
            }
            txtSearchSerieNumber.Text = cr.SerieSeqNumber.ToString();
        }

        //Text content opvullen met gridselectie
        private void GetSelectedGridItem_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (dgSearchAuthorsFound.SelectedItem != null)
            {
                Author author = (Author)dgSearchAuthorsFound.SelectedItem;
                txtSearchAuthor.Text = author.Name;
            }
            if (dgSearchPublishersFound.SelectedItem != null)
            {
                Publisher publisher = (Publisher)dgSearchPublishersFound.SelectedItem;
                txtSearchPublisher.Text = publisher.Name;
            }
            if (dgSearchSerieFound.SelectedItem != null)
            {
                Serie serie = (Serie)dgSearchSerieFound.SelectedItem;
                txtSearchSerie.Text = serie.Name;
            }
        }
    }
}