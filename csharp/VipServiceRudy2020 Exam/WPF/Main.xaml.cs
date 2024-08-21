namespace WPF
{
    using BusinessLayer;
    using Entiteiten;
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for Main.xaml.
    /// </summary>
    public partial class Main : Window
    {
        /// <summary>
        /// Defines the rs.
        /// </summary>
        internal static ReservatieManager rs = new ReservatieManager();

        /// <summary>
        /// Defines the title.
        /// </summary>
        private static string title = Configuration.TitelProgramma;

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            InitializeComponent();

            var klanten = rs.GetKlanten();
            AutoCompleteComboBox.ItemsSource = klanten;
            AutoCompleteComboBoxVoertuigen.ItemsSource = rs.GetVoertuigen();
            AutoCompleteComboBoxArragement.ItemsSource = rs.GetAlleArragementen();
            AutoCompleteComboBoxLocatiesStart.ItemsSource = rs.GetLocaties();
            AutoCompleteComboBoxLocatiesStop.ItemsSource = rs.GetLocaties();
            KlantenBestand.ItemsSource = klanten;

            for (int i = 1; i <= Configuration.MaxArragementTime; i++)
            {
                uuren.Items.Add(i);
            }
        }

        /// <summary>
        /// The MaakReservatieaan_click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void MaakReservatieaan_click(object sender, RoutedEventArgs e)
        {
            if (ValidateNewRes())
            {
                try
                {
                    var x = Int32.Parse(uuren.SelectionBoxItem.ToString());
                    var arragement = (Arragement)AutoCompleteComboBoxArragement.SelectedItem;
                    var auto = (Voertuig)AutoCompleteComboBoxVoertuigen.SelectedItem;
                    var klant = (Klant)AutoCompleteComboBox.SelectedItem;
                    var start_loc = (Locaties)AutoCompleteComboBoxLocatiesStart.SelectedItem;
                    var stop_loc = (Locaties)AutoCompleteComboBoxLocatiesStop.SelectedItem;
                    var r = rs.HandleTheReservatie(arragement, StartDate.Value.Value, x, klant, auto, start_loc, stop_loc);

                    MessageBoxResult result = MessageBox.Show($"Reservatie Details: \n\nTotaal Exel btw: {r.TotaalExclusiefBtw}\nTotaal Incl btw: {r.TotaalInclusiefBtw}\nTotaal Bedrag na korting: {r.TotaalBedrag}\n\n Wilt U deze Reservatie opslaan?", title, MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            rs.SaveReservatie(r);
                            MessageBox.Show("Reservatie is opgeslagen!", title);
                            break;
                        case MessageBoxResult.No:
                            MessageBox.Show("Reservatie is geannuleerd ", title);
                            break;

                    }

                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Error: {exception.Message}");
                    //   throw;
                }
            }
        }

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool ValidateNewRes()
        {
            try
            {

                if (AutoCompleteComboBox.SelectedIndex == null)
                {
                    throw new Exception("Je hebt geen klant geselecteerd");
                }

                if (AutoCompleteComboBoxVoertuigen.SelectedIndex == null)
                {
                    throw new Exception("Je hebt geen auto geselecteerd");
                }

                if (AutoCompleteComboBoxArragement.SelectedItem == null)
                {
                    throw new Exception("Je hebt geen arragement geselecteerd");
                }

                if (AutoCompleteComboBoxLocatiesStart.SelectedItem == null)
                {
                    throw new Exception("Je hebt geen Start locatie geselecteerd ");
                }

                if (AutoCompleteComboBoxLocatiesStop.SelectedItem == null)
                {
                    throw new Exception("Je hebt geen Start locatie geselecteerd ");
                }

                if (StartDate.Value.Value == null)
                {
                    throw new Exception("Je hebt geen geldig datum geselecteerd ");
                }

                if (uuren.SelectedIndex <= -1)
                {
                    throw new Exception("Je hebt geen geldig uur geselecteerd ");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Validatie Error: {e.Message}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// The KlantenBestand_Closed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void KlantenBestand_Closed(object sender, EventArgs e)
        {
            var klant = (Klant)KlantenBestand.SelectedItem;
            var reservatieses = rs.GetReservatiesDoorKlantId(klant);
            Overview.ItemsSource = reservatieses;
        }

        /// <summary>
        /// The SearchDateButton_click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void SearchDateButton_click(object sender, RoutedEventArgs e)
        {

            if (StartDate_search.Value.Value == null)
            {
                throw new Exception("Je hebt geen geldig datum geselecteerd ");
            }


            var date = StartDate_search.Value.Value;
            //Check for radio button
            if (radio_NaamPlusDatum.IsChecked == true)
            {

                if (KlantenBestand.SelectedItem == null)
                    throw new Exception("Je hebt geen klant geselecteerd");

                var klant = (Klant)KlantenBestand.SelectedItem;
                var result = rs.GetReservatiesDoorKlantIdEnDatum(klant, date);
                Overview.ItemsSource = result;
            }
            else
            {
                // datum met die reseravatie
                var result = rs.GetReservatiesDoorDate(date);
                Overview.ItemsSource = result;
            }
        }
    }
}
