using newProj.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for DataAnalysis.xaml
    /// </summary>
    public partial class DataAnalysis : Page
{
        public List<AgeGroupData> AgeGroupDataList { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<PatientGenderData> _segments;

        public ObservableCollection<PatientGenderData> Segments
        {
            get { return _segments; }
            set
            {
                _segments = value;
                OnPropertyChanged("Segments");
            }
        }

        public DataAnalysis()
    {
        InitializeComponent();
            DataContext = this;
            DB db = new DB();
            // Get all patients from the database
            List<Patient> patients = db.GetAllPatients();

            // Calculate age group distribution
            AgeGroupDataList = CalculateAgeGroupDistribution(patients);
            int femaleCount = patients.Count(p => p.Gender == "Female");
            int maleCount = patients.Count(p => p.Gender == "Male");

            // Create data objects for the segments
            Segments = new ObservableCollection<PatientGenderData>
            {
                new PatientGenderData { Gender = "Female", Count = femaleCount },
                new PatientGenderData { Gender = "Male", Count = maleCount }
            };
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private List<AgeGroupData> CalculateAgeGroupDistribution(List<Patient> patients)
        {
            List<AgeGroupData> ageGroupDataList = new List<AgeGroupData>();

            // Define age group ranges
            int[] ageRanges = { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

            // Initialize age group counts
            Dictionary<string, int> ageGroupCounts = new Dictionary<string, int>();
            foreach (var range in ageRanges)
            {
                ageGroupCounts.Add($"{range}-{range + 9}", 0); // e.g., "0-9", "10-19", etc.
            }

            // Count patients in each age group
            foreach (var patient in patients)
            {
                foreach (var range in ageRanges)
                {
                    if (patient.Age >= range && patient.Age < range + 10)
                    {
                        string ageGroup = $"{range}-{range + 9}";
                        ageGroupCounts[ageGroup]++;
                        break;
                    }
                }
            }

            // Convert dictionary to list of AgeGroupData objects
            foreach (var kvp in ageGroupCounts)
            {
                ageGroupDataList.Add(new AgeGroupData { AgeGroup = kvp.Key, Count = kvp.Value });
            }

            return ageGroupDataList;
        }
    }
    public class PatientGenderData
    {
        public string Gender { get; set; }
        public int Count { get; set; }
    }
    // Define a class to represent age group data
    public class AgeGroupData
    {
        public string AgeGroup { get; set; }
        public int Count { get; set; }
    }
}
