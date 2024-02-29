using newProj.classes;
using newProj;
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
using Syncfusion.UI.Xaml.Charts;
using System.Windows.Media.Effects;

namespace newProj
{
    public partial class DoctorDashboard : Page
    {
        public List<DataPoint> Data { get; set; }
        public DoctorDashboard()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzExODg3N0AzMjM0MmUzMDJlMzBnMVAvbWxSR1V4V09oV0FETlJtTDhFUGFkc2NWT3BNVGhLd3hQZTJzSnJRPQ==");

            LoadAppointments();
            DataContext = this;
            DB db = new DB();
            // Call the GetAllAppointments function to fetch appointments from the database
            List<Appointment> appointments = db.GetAllAppointments();
            // Aggregate data to count the number of appointments per day
            var appointmentCountPerDay = appointments
                .GroupBy(appointment => appointment.AppointmentDate.DayOfWeek)
                .Select(group => new { DayOfWeek = group.Key, Count = group.Count() });
            Data = appointmentCountPerDay.Select(item => new DataPoint(item.DayOfWeek.ToString(), item.Count)).ToList();
        }
        public class DataPoint
        {
            public string DayOfWeek { get; set; }
            public int Value { get; set; }

            public DataPoint(string dayOfWeek, int value)
            {
                DayOfWeek = dayOfWeek;
                Value = value;
            }
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