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

namespace newProj
{
    /// <summary>
    /// Interaction logic for patientsList.xaml
    /// </summary>
    public partial class patientsList : Page
{
    public patientsList()
    {
        InitializeComponent();
        LoadPatients();
    }
        private void LoadPatients()
        {
            DB db = new DB();
            List<Patient> patients = db.GetAllPatients();
            foreach (var p in patients)
            {
                StackPanel horizontalStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Height = 60
                };
                Grid imageGrid = new Grid
                {
                    Width = 116
                };
                Image imageFill = new Image();
                imageFill.Source = new BitmapImage(new Uri("/images/profile.png", UriKind.Relative));
                imageGrid.Children.Add(imageFill);
                horizontalStackPanel.Children.Add(imageGrid);
                Grid nameGrid = new Grid
                {
                    Width = 233
                };
                TextBlock nametxt = new TextBlock
                {
                    Text = p.Name,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    FontSize = 20,
                    FontWeight = FontWeights.SemiBold
                };
                nameGrid.Children.Add(nametxt);
                horizontalStackPanel.Children.Add(nameGrid);
                Grid idGrid = new Grid
                {
                    Width = 350
                };
                TextBlock idtxt = new TextBlock
                {
                    Text = "#" + p.PatientID.ToString(),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    FontWeight = FontWeights.Medium
                };
                idGrid.Children.Add(idtxt);
                horizontalStackPanel.Children.Add(idGrid);

                Grid phoneGrid = new Grid
                {
                    Width = 350
                };

                TextBlock phonetxt = new TextBlock
                {
                    Text = p.PhoneNumber,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                    FontSize = 20,
                    FontWeight = FontWeights.Medium
                };

                // Add the TextBlock to the Grid
                phoneGrid.Children.Add(phonetxt);

                // Add the Grid to the StackPanel
                horizontalStackPanel.Children.Add(phoneGrid);


                Grid DetailsGrid = new Grid
                {
                    Width = 390
                };
                Rectangle rec = new Rectangle
                {
                    Fill = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#387C6D")),
                    RadiusX = 15,
                    RadiusY = 15,
                    StrokeThickness = 0,
                    Width = 100,
                    Height = 30
                };
                DetailsGrid.Children.Add(rec);
                Button detailsBtn = new Button
                {
                    Name = "id" + p.PatientID.ToString(),
                    Content = "Details",
                    FontFamily = new System.Windows.Media.FontFamily("Inter"),
                    FontWeight = FontWeights.Medium,
                    Foreground = System.Windows.Media.Brushes.White,
                    Width = 100,
                    Height = 30,
                    FontSize = 15,
                    Background = System.Windows.Media.Brushes.Transparent,
                    BorderThickness = new Thickness(0),

                };
                //detailsBtn.Click += Details_CLick;
                DetailsGrid.Children.Add(detailsBtn);

                // Add the Grid to the StackPanel
                horizontalStackPanel.Children.Add(DetailsGrid);
                patientsStack.Children.Add(horizontalStackPanel);
            }
        }
    }
}
