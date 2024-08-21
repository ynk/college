using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockManagement.StockWindow
{
    /// <summary>
    /// Interaction logic for ComicSelect.xaml
    /// </summary>
    public partial class ComicSelect : Window
    {
        public ComicSelect()
        {
            InitializeComponent();
            StockWindow stockWindow = new StockWindow();
            //gebruiken we zowel als page voor main window maar ook als window om comics te zoeken in delivery en order
            stockWindow.ComicSelected += StockWindowOnComicSelected;
            StockWindow.Content = stockWindow;
        }

        public event EventHandler<StockWindow.ComicSelectedEventArgs> ComicSelected;
        private void StockWindowOnComicSelected(object? sender, StockWindow.ComicSelectedEventArgs e) //StockWindow.StockWindow.ComicSelectedEventArgs
        {
            //we vangen de event op en geven deze door, erna wordt het zoek venster gesloten
            ComicSelected?.Invoke(this, e);
            this.Close();
        }
    }
}
