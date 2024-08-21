using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeerpadWPF
{
    /// <summary>
    /// Interaction logic for VoorbeeldOpdracht2.xaml
    /// </summary>
    public partial class VoorbeeldOpdracht2 : Window
    {
        public VoorbeeldOpdracht2()
        {
            InitializeComponent();
        }

        private void Calendar1_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (DateTime item in e.AddedItems)
            {
                GeselecteerdeDataListBox.Items.Add(item);
            }
        }
    }
}
