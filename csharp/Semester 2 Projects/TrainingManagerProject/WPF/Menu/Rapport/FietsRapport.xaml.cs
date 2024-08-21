using DomainLibrary.Domain;
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
using WPF.Utils;

namespace WPF.Menu.Rapport
{
    /// <summary>
    /// Interaction logic for FietsRapport.xaml
    /// </summary>
    public partial class FietsRapport : Window
    {
        public FietsRapport()
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

      
            List<RapportData> tijdlijn = new List<RapportData>();
            foreach (var x in report.TimeLine)
            {
                if (x.Item1 == SessionType.Cycling)
                    tijdlijn.Add(new RapportData(x.Item2 as CyclingSession));
            }
            aantalSessies.Text = report.RunningSessions.ToString();
            LoopTrainingTijd.Text = report.TotalRunningTrainingTime.ToString();
            LoopTrainingAfstand.Text = report.TotalRunningDistance.ToString();

            overView.ItemsSource = tijdlijn;
            var maxDis = new List<RapportData>() { new RapportData(report.MaxDistanceSessionCycling) };
            BestDistance.ItemsSource = maxDis;
            var maxSpeed = new List<RapportData>() { new RapportData(report.MaxSpeedSessionCycling) };
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
            dbFunctions.DeleteCycle(ctxData.Id);
       


        }


    }
}
