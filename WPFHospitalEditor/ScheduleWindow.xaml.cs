using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Users.Patient;
using System;
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
        IPatientServerController patientServerController = new PatientServerController();
        IExaminationServerController examinationServerController = new ExaminationServerController();

        public ScheduleWindow(RecommendationDto recommendationDto)
        {
            InitializeComponent();
            this.recommendationDto = recommendationDto;
            SetAppointmentInfoContent();
            SetPatientNameComboBox();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ScheduleAppointment(object sender, RoutedEventArgs e)
        {
            if (patientsComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("You must select a patient!");
            }
            else { 
                String patient = patientsComboBox.SelectedItem.ToString();
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

        private void PatientTextInputChanged(object sender, EventArgs e)
        {
            patientsComboBox.Items.Clear();
            SetPatientNameComboBox();
        }

        private void SetPatientNameComboBox()
        {
            patientsComboBox.Items.Add(AllConstants.EmptyComboBox);
            patientsComboBox.SelectedIndex = 0;
            foreach (Patient p in patientServerController.SearchPatients(PatientSearchInput.Text))
            {
                patientsComboBox.Items.Add(p.Id.ToString() + " " + p.Person.Name + " " + p.Person.Surname + " - " + p.Person.Jmbg);
            }
        }

        private void SetAppointmentInfoContent()
        {
            this.appointmentInfo.Content = "Doctor: " + recommendationDto.Doctor.Person.Name + " "
                                                     + recommendationDto.Doctor.Person.Surname + " " +
                                           ",Time: " + recommendationDto.TimeInterval.Start.ToString();
        }
    }
}
