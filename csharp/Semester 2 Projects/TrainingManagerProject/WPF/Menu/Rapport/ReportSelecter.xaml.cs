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
using WPF.Menu.Rapport;

namespace WPF
{
    /// <summary>
    /// Interaction logic for ReportSelecter.xaml
    /// </summary>
    public partial class ReportSelecter : Window
    {
        public ReportSelecter()
        {
            InitializeComponent();
        }
        private void loopRapport_Click(object sender, RoutedEventArgs e)
        {
            LoopRapport lp = new LoopRapport();
            
            lp.Show();
        }
        private void fietsRapport_Click(object sender, RoutedEventArgs e)
        {
            FietsRapport fp = new FietsRapport();
            fp.Show();
            
        }
    }
}
