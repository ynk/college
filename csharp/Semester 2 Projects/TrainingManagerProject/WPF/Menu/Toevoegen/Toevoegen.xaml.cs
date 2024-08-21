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
using DomainLibrary.Domain;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Toevoegen.xaml
    /// </summary>
    public partial class Toevoegen : Window
    {
        public Toevoegen()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Voeg toe button
            // Check inputs
            if (inputCheck())
            {
                MessageBox.Show("All checks passed");
                 SendToDb();
            }
            else
            {
                MessageBox.Show(
                    "Niet alles is correct ingevuld!",
                    "Toevoegen", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error
                    );
            }

        }

        private bool inputCheck()
        {
            bool distance = false;
            bool speed = false;
            bool watt = false;
            bool when = dateWhen.SelectedDate != null && dateWhen.SelectedDate < DateTime.Now;


            if (textboxSnelheid.Text != "")
            {
                if (float.TryParse(textboxSnelheid.Text, out float x)) speed = true;

            }
            else
                speed = true;

            if (FietsTrainingRadio.IsChecked == true)
            {
                if (textboxAfstand.Text != "")
                {
                    if (float.TryParse(textboxAfstand.Text, out float x) == true)
                        distance = true;
 
                }
                else
                    distance = true;

                if (textboxWattage.Text != "")
                {
                    if (int.TryParse(textboxWattage.Text, out int x) == true) watt = true;

                }
                else  watt = true;

                if (when && distance && speed && watt)  return true;
            }
            else
            {

                if (textboxAfstand.Text != "")
                {
                    if (int.TryParse(textboxAfstand.Text, out int x) == true)  distance = true;
                }
                else  distance = true;

                if (distance && speed && when) return true; // Alle checks

            }

            return false;
        }

        private void SendToDb()
        {
            TimeSpan time = timeConverter(sliderTimeSpan.Value);
            string comments = textboxComments.Text.ToString();
            DateTime date = new DateTime(dateWhen.SelectedDate.Value.Year, dateWhen.SelectedDate.Value.Month, dateWhen.SelectedDate.Value.Day, int.Parse(timeWhenHr.Text),int.Parse(timeWhenMin.Text),0);

            float avgSpeed;
            if (textboxSnelheid.Text != "" && float.TryParse(textboxSnelheid.Text, out float _)== true)
                avgSpeed = float.Parse(textboxSnelheid.Text);
            else
                avgSpeed = 0;

            TrainingType trainingType = TrainingType.Endurance;
            switch (comboboxTrainingType.SelectedIndex)
            {
                case 0:
                    trainingType = TrainingType.Endurance;
                    break;
                case 1:
                    trainingType = TrainingType.Interval;
                    break;
                case 2:
                    trainingType = TrainingType.Recuperation;
                    break;
            } // we hebben sws een trainingtype

            if (FietsTrainingRadio.IsChecked == true)
            {
                float distance;
                if (textboxAfstand.Text != "" && float.TryParse(textboxAfstand.Text, out float Result) == true)
                    distance = float.Parse(textboxAfstand.Text);
                else
                    distance = 0;

                int avgWatt;
                if (textboxWattage.Text != "" && int.TryParse(textboxWattage.Text, out int xResult) == true)
                    avgWatt = int.Parse(textboxWattage.Text);
                else
                    avgWatt = 0;

                BikeType bikeType = BikeType.CityBike;
                switch (comboboxFietsType.SelectedIndex)
                {
                    case 0:
                        bikeType = BikeType.IndoorBike;
                        break;
                    case 1:
                        bikeType = BikeType.RacingBike;
                        break;
                    case 2:
                        bikeType = BikeType.CityBike;
                        break;
                    case 3:
                        bikeType = BikeType.MountainBike;
                        break;
                }
                //add fietsTrainingpublic static void AddCyclingTraining(DateTime when,  TimeSpan time, TrainingType tp, BikeType bt, string comments, float avgSpeed, int avgWatt, float distance)
                dbFunctions.AddCycling(date,time,trainingType,bikeType, comments, avgSpeed,avgWatt, distance);

            }
            else // Fiets = False
            {
                int distance = int.Parse(textboxAfstand.Text);
                dbFunctions.AddRunning(date, time, trainingType, comments, avgSpeed, distance);
                // lopen
            }

          


        }
        private TimeSpan timeConverter(double toConvert)
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.timespan.fromseconds?view=netcore-3.1
            int fullHours = (int)toConvert;
            int minutes = (int)toConvert - fullHours;
            return new TimeSpan(fullHours, minutes, 0);
        }

    }


}
