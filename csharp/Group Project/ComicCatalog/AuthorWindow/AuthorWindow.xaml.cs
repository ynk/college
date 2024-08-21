using BusinessLayer;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using DataLayer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComicCatalog.AuthorWindow
{
    /// <summary>
    /// Interaction logic for AuthorWindow.xaml
    /// </summary>
    public partial class AuthorWindow : Page
    {

        private readonly string _connectionString = Properties.Resources.ConnectionString;
        private Author _author;

        public AuthorWindow()
        {
            InitializeComponent();
            FillAuthorGrid();
        }

        public AuthorWindow(string authorName) : this()
        {
            txtAuthors.Text = authorName;
        }

        //toevoegen authors in grid
        private void TxtAuthors_TextChanged(object sender, TextChangedEventArgs e)
        {
            AuthorManager am = new AuthorManager(new UnitOfWork(_connectionString));
            List<Author> authors = am.GetAllAuthors().Where(x => x.Name.ToLower().Trim().Contains(txtAuthors.Text.ToLower().Trim())).ToList();

            dgAuthors.Items.Clear();

            foreach (Author a in authors)
            {
                dgAuthors.Items.Add(a);
            }
        }
        
        //Alles zien bij het opstarten
        private void FillAuthorGrid()
        {
            AuthorManager am = new AuthorManager(new UnitOfWork(_connectionString));
            dgAuthors.Items.Clear();
            List<Author> authors = am.GetAllAuthors();

            foreach (Author a in authors)
            {
                dgAuthors.Items.Add(a);
            }
        }

        private void BtnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AuthorManager am = new AuthorManager(new UnitOfWork(_connectionString));

                Author author = new Author(txtAuthors.Text);
                MessageBoxResult result = MessageBox.Show("Do you want to add this author?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    am.AddAuthor(author);
                    FillAuthorGrid();
                    txtAuthors.Text = "";
                    txtAuthors.Text = author.Name;
                    MessageBox.Show("Author added!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (AuthorException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnUpdateAuthor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AuthorManager am = new AuthorManager(new UnitOfWork(_connectionString));

                _author.SetName(txtAuthors.Text.Trim());

                MessageBoxResult result = MessageBox.Show("Do you want to update this author?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    am.UpdateAuthor(_author);
                    FillAuthorGrid();
                    txtAuthors.Text = "";
                    txtAuthors.Text = _author.Name;
                    MessageBox.Show("Author updated!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (AuthorException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnRemoveAuthor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AuthorManager am = new AuthorManager(new UnitOfWork(_connectionString));

                MessageBoxResult result = MessageBox.Show("Do you want to remove this author?", "Question", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    am.RemoveAuthor(_author);
                    FillAuthorGrid();
                    txtAuthors.Clear();
                    MessageBox.Show("Author removed!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (AuthorException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // bij het aanklikkken op het grid, word de text velden ingevuld
        private void DgAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAuthors.SelectedItem != null)
            {
                _author = (Author)dgAuthors.SelectedItem;
                txtAuthors.Text = _author.Name;
            }
        }
    }
}
