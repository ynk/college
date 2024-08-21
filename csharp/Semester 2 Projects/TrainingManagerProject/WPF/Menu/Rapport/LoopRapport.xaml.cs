using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DomainLibrary.Domain;
using WPF.Utils;

namespace WPF.Menu.Rapport
{
    /// <summary>
    /// Interaction logic for LoopRapport.xaml
    /// </summary>
    public partial class LoopRapport : Window
    {
        public LoopRapport()
        {
            InitializeComponent();
            datePicker.ItemsSource = GetPossibleDates();
        }
        private void datePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime picked = (DateTime)datePicker.SelectedItem;
            var report = dbFunctions.GenerateMonthReport(picked.Month, picked.Year);
            UpdateValues(report);
        }
        private void UpdateValues(Report report)
        {

            aantalSessies.Text = report.RunningSessions.ToString();
            LoopTrainingTijd.Text = report.TotalRunningTrainingTime.ToString();
            LooptrainingAfstand.Text = report.TotalRunningDistance.ToString();

            List<RapportData> tijdlijn = new List<RapportData>();
            foreach (var x in report.TimeLine)
            {
                if (x.Item1 == SessionType.Running)
                    tijdlijn.Add(new RapportData(x.Item2 as RunningSession));
            }
            overView.ItemsSource = tijdlijn;
            var maxDis = new List<RapportData>() { new RapportData(report.MaxDistanceSessionRunning) };
            BestDistance.ItemsSource = maxDis;
            var maxSpeed = new List<RapportData>() { new RapportData(report.MaxSpeedSessionRunning) };
            BestSpeed.ItemsSource = maxSpeed;


        }
        private List<DateTime> GetPossibleDates()
        {
            
            Dictionary<string, DateTime> dateTimes = new Dictionary<string, DateTime>();
            dbFunctions.GetAllRunningData().ForEach(x => dateTimes.TryAdd(x.colum1.ToString("MM-yyyy"), x.colum1));
            List<DateTime> sortedList = dateTimes.Values.ToList();
            return sortedList;
        }
        private void DeleteRun(object sender, RoutedEventArgs routedEventArgs)
        {
            
            var index = overView.SelectedIndex;
            var ctxData = overView.Items[index] as RapportData;
            dbFunctions.DeleteRun(ctxData.Id);
     //       MessageBox.Show("delete", ctxData.Id.ToString());


        }


    }
}
