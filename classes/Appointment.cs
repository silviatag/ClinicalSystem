using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newProj.classes
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string AppointmentType { get; set; }
        public string AppointmentStatus { get; set; }
        public int AppointmentFees { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }

        public Appointment() { }

        public Appointment(DateTime appointmentDate, TimeSpan appointmentTime,
                           string appointmentType,
                           int patientID, int doctorID)
        {
            AppointmentDate = appointmentDate;
            AppointmentTime = appointmentTime;
            AppointmentType = appointmentType;
            PatientID = patientID;
            DoctorID = doctorID;
            if (appointmentType == "Follow Up")
                AppointmentFees = 300;
            else AppointmentFees = 200;
        }
    }
}



