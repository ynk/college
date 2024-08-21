using System.Windows;

namespace StockManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenStockWindow(object sender, RoutedEventArgs e)
        {
            btnStock.IsEnabled = false;
            btnOrder.IsEnabled = true;
            btnDelivery.IsEnabled = true;
            StockWindow.StockWindow stockWindow = new StockWindow.StockWindow();
            MainFrame.Content = stockWindow;
        }

        private void OpenDeliveryWindow(object sender, RoutedEventArgs e)
        {
            btnStock.IsEnabled = true;
            btnOrder.IsEnabled = true;
            btnDelivery.IsEnabled = false;
            DeliveryWindow.DeliveryWindow deliveryWindow = new DeliveryWindow.DeliveryWindow();
            MainFrame.Content = deliveryWindow;
        }

        private void OpenOrderWindow(object sender, RoutedEventArgs e)
        {
            btnStock.IsEnabled = true;
            btnOrder.IsEnabled = false;
            btnDelivery.IsEnabled = true;
            OrderWindow.OrderWindow orderWindow = new OrderWindow.OrderWindow();
            MainFrame.Content = orderWindow;
        }
    }
}
