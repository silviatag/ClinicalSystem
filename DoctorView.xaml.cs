using @new;
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

namespace newProj
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
{
    public DoctorView()
    {
        InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzExODg3N0AzMjM0MmUzMDJlMzBnMVAvbWxSR1V4V09oV0FETlJtTDhFUGFkc2NWT3BNVGhLd3hQZTJzSnJRPQ==");
            Loaded += DoctorView_Loaded;
        }
        private void DoctorView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create an instance of the page you want to display
            DoctorDashboard page = new DoctorDashboard();

            // Navigate to the page within the frame
            MainFrame.Navigate(page);
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            DoctorDashboard page = new DoctorDashboard();
            MainFrame.Navigate(page);
        }

        private void appointments_Click(object sender, RoutedEventArgs e)
        {
            ViewAppointments page = new ViewAppointments();
            MainFrame.Navigate(page);
        }

        private void patients_Click(object sender, RoutedEventArgs e)
        {
            patientsList page = new patientsList();
            MainFrame.Navigate(page);
        }

        private void analytics_Click(object sender, RoutedEventArgs e)
        {
            DataAnalysis page = new DataAnalysis();
            MainFrame.Navigate(page);
        }

        private void finance_Click(object sender, RoutedEventArgs e)
        {

        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OCR_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Equp_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_finance.Visibility = Visibility.Collapsed;
                tt_analytics.Visibility = Visibility.Collapsed;
                tt_patients.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_appointements.Visibility = Visibility.Collapsed;
                tt_ocr.Visibility = Visibility.Collapsed;
                tt_equip.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_finance.Visibility = Visibility.Visible;
                tt_analytics.Visibility = Visibility.Visible;
                tt_patients.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_appointements.Visibility = Visibility.Visible;
                tt_ocr.Visibility = Visibility.Visible;
                tt_equip.Visibility = Visibility.Visible;
            }
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
