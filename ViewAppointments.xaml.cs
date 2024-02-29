using newProj.classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace newProj
{
    /// <summary>
    /// Interaction logic for ViewAppointments.xaml
    /// </summary>
    public partial class ViewAppointments : Page
    {
        DispatcherTimer _timer;
        TimeSpan _time;

        public ViewAppointments()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzExODg3N0AzMjM0MmUzMDJlMzBnMVAvbWxSR1V4V09oV0FETlJtTDhFUGFkc2NWT3BNVGhLd3hQZTJzSnJRPQ==");
            _time = new TimeSpan(1, 30, 30); // Set the initial time to 1 hour, 30 minutes, and 30 seconds

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString(@"hh\:mm\:ss"); // Format the time to show hours, minutes, and seconds
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop(); // Stop the timer when the time reaches zero
                                   // Add any action you want to perform after the countdown completes
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1)); // Decrement the time by 1 second
            }, Application.Current.Dispatcher);

            _timer.Start();
            LoadAppointments();
        }
        private void LoadAppointments()
        {
            DB db = new DB();
            List<Appointment> appointments = db.GetAppointmentsFromNow();
            foreach (var app in appointments.OrderBy(a => a.AppointmentTime).ToList())
            {
                Patient p = db.GetPatientById(app.PatientID);
                Grid grid = new Grid();
                grid.Height = 30;
                Border border = new Border();
                border.BorderBrush = Brushes.Gray;
                for (int i = 0; i < 6; i++)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    if (i == 0 || i == 5)
                        column.Width = new GridLength(1, GridUnitType.Star); // 1* for first and last column
                    else
                        column.Width = new GridLength(2, GridUnitType.Star); // 2* for other columns
                    grid.ColumnDefinitions.Add(column);
                }
                // Add TextBlocks to the grid
                for (int i = 1; i <= 4; i++)
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = i == 1 ? p.Name : i == 2 ? p.Age.ToString() : i == 3 ? app.AppointmentTime.ToString() : app.AppointmentType,
                        FontFamily = new FontFamily("Inter"),
                        FontSize = 15,
                        FontWeight = FontWeights.Medium,
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Foreground = Brushes.Black
                    };
                    Grid.SetColumn(textBlock, i);
                    grid.Children.Add(textBlock);
                    Button b = new Button();
                    b.Background = Brushes.Transparent;
                    b.BorderBrush = Brushes.Transparent;
                    Grid.SetColumn(b, i);
                    grid.Children.Add(b);
                }
                border.Child = grid;
                appsPanel.Children.Add(border);
            }
        }

    }
}
