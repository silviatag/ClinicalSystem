using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql;
using MySql.Data.MySqlClient;
using newProj.classes;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Configuration;
using System.Data;


namespace newProj
{
    internal class DB
    {
        static public string connectionString = "server=localhost;database=caretech;user=root;password=caretech;";
        static public MySqlConnection connection;

        public DB()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        //////////// APPOINTMENTS ////////////
        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM appointment";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create an Appointment object from the retrieved data
                            Appointment appointment = new Appointment
                            {
                                AppointmentDate = reader.GetDateTime("appointmentDate"),
                                AppointmentTime = reader.GetTimeSpan("appointmentTime"),
                                AppointmentType = reader.GetString("appointmentType"),
                                PatientID = reader.GetInt32("patientID"),
                                DoctorID = reader.GetInt32("doctorID")
                            };

                            appointments.Add(appointment);
                        }
                    }
                }
            }

            return appointments;
        }

        public List<Appointment> GetAppointmentsFromNow()
        {
            List<Appointment> appointments = new List<Appointment>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Your SQL query to select appointments for today
                string query = "SELECT * FROM appointment WHERE DATE(appointmentDate) = CURDATE() AND TIME(appointmentTime) >= CURTIME()";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Map database fields to _Appointment properties
                            Appointment appointment = new Appointment
                            {
                                AppointmentID = Convert.ToInt32(reader["appointmentID"]),
                                AppointmentDate = Convert.ToDateTime(reader["appointmentDate"]),
                                AppointmentTime = (TimeSpan)reader["appointmentTime"],
                                AppointmentType = reader["appointmentType"].ToString(),
                                AppointmentStatus = reader["appointmentStatus"].ToString(),
                                AppointmentFees = Convert.ToInt32(reader["appointmentFees"]),
                                PatientID = Convert.ToInt32(reader["patientID"]),
                                DoctorID = Convert.ToInt32(reader["doctorID"])
                            };

                            appointments.Add(appointment);
                        }
                    }
                }
            }

            return appointments;
        }


        ////////// PATIENT /////////////////
        public Patient GetPatientById(int patientId)
        {
            Patient patient = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Your SQL query to select a patient by ID
                string query = "SELECT * FROM patient WHERE patientID = @PatientId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientId", patientId);


                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Map database fields to Patient properties
                            patient = new Patient
                            {
                                PatientID = Convert.ToInt32(reader["patientID"]),
                                NationalID = reader["nationalID"].ToString(),
                                Name = reader["name"].ToString(),
                                DOB = reader["DOB"] is DBNull ? null : (DateTime?)reader["DOB"],
                                Age = Convert.ToInt32(reader["age"]),
                                Address = reader["address"].ToString(),
                                PhoneNumber = reader["phoneNumber"].ToString(),
                                Gender = reader["gender"].ToString(),
                                MaritalStatus = reader["maritalStatus"].ToString(),
                                Height = Convert.ToInt32(reader["height"]),
                                Weight = Convert.ToInt32(reader["weight"]),
                                BloodGroup = reader["bloodGroup"].ToString()
                            };
                        }
                    }
                }
            }

            return patient;
        }
        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM patient WHERE age IS NOT NULL";

                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, connection))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient
                            {
                                PatientID = reader.GetInt32("patientID"),
                                NationalID = reader.GetString("nationalID"),
                                Name = reader.GetString("name"),
                                DOB = reader.IsDBNull(reader.GetOrdinal("DOB")) ? (DateTime?)null : reader.GetDateTime("DOB"),
                                Age = reader.GetInt32("age"),
                                Address = reader.GetString("address"),
                                PhoneNumber = reader.GetString("phoneNumber"),
                                Gender = reader.GetString("gender"),
                                MaritalStatus = reader.GetString("maritalStatus"),
                                Height = reader.GetInt32("height"),
                                Weight = reader.GetInt32("weight"),
                                BloodGroup = reader.GetString("bloodGroup")
                            };

                            patients.Add(patient);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return patients;
        }
    }
}
