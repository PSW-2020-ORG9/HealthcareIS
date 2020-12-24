using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Users.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        RecommendationDto recommendationDto;
        List<Patient> allPatients;
        IPatientServerController patientServerController = new PatientServerController();
        IExaminationServerController examinationServerController = new ExaminationServerController();

        public ScheduleWindow(RecommendationDto recommendationDto)
        {
            InitializeComponent();
            this.recommendationDto = recommendationDto;
            setAppointmentInfoContent();
            allPatients = patientServerController.GetAllPatients().ToList();
            foreach (Patient p in allPatients)
                patients.Items.Add(p.Id.ToString() + " " + p.Person.Name + " " + p.Person.Surname + " - " + p.Person.Id);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void scheduleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (patients.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a patient!");
            }
            else { 
                String patient = patients.SelectedItem.ToString();
                int patientID = int.Parse(patient.Split(" ")[0]);
                Examination examination = examinationServerController.ScheduleExamination(recommendationDto.TimeInterval.Start, recommendationDto.Doctor.Id, patientID);
                if (examination!= null)
                {
                    MessageBox.Show("Examination has been scheduled successfuly!");
                }
                else
                {
                    MessageBox.Show("An error has occured, examination is NOT scheduled!");
                }
            this.Close();
            }
        }

        private void setAppointmentInfoContent()
        {
            this.appointmentInfo.Content = "Doctor: " + recommendationDto.Doctor.Person.Name + " "
                                                     + recommendationDto.Doctor.Person.Surname + " " +
                                           ",Time: " + recommendationDto.TimeInterval.Start.Date.ToString();
        }
    }
}
