using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newProj.classes
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string NationalID { get; set; }
        public string Name { get; set; }
        public DateTime? DOB { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string BloodGroup { get; set; }

        public Patient() { }
        public Patient(string nationalID, string name, DateTime? dob, string address, string phonenumber, string gender, string maritalstatus, int height, int weight, string bloodgroup)
        {
            NationalID = nationalID;
            Name = name;
            DOB = dob;
            Address = address;
            PhoneNumber = phonenumber;
            Gender = gender;
            Height = height;
            Weight = weight;
            BloodGroup = bloodgroup;
            MaritalStatus = maritalstatus;
            PatientID = int.Parse(nationalID.Substring(nationalID.Length - 4));

            if (DOB.HasValue)
            {
                DateTime currentDate = DateTime.Now;
                Age = currentDate.Year - DOB.Value.Year;

                // Check if the birthday for this year has occurred or not
                if (DOB.Value.Date > currentDate.AddYears(-Age))
                {
                    Age--;
                }
            }
            //PatientID = ++incrementalID;

        }
        public Patient(string nationalid, string name, string phonenumber)
        {
            NationalID = nationalid;
            Name = name;
            PhoneNumber = phonenumber;
            PatientID = int.Parse(nationalid.Substring(nationalid.Length - 4));

        }
        public Patient(int id, string name)
        {
            PatientID = id;
            Name = name;
        }
    }
}
