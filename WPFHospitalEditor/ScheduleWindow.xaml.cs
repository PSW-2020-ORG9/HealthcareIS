﻿using System;
using System.Windows;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

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

        public ScheduleWindow(RecommendationDto recommendationDto, Window Owner)
        {
            InitializeComponent();
            this.recommendationDto = recommendationDto;
            this.Owner = Owner;
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
                if (this.Owner == null)
                {
                    ScheduleEmergencyExamination();
                }
                else { 
                    String patient = patientsComboBox.SelectedItem.ToString();
                    int patientID = int.Parse(patient.Split(" ")[0]);
                    Examination examination = examinationServerController.ScheduleExamination(recommendationDto.TimeInterval.Start, recommendationDto.Doctor.Id, patientID);
                    if (examination!= null)
                    {
                        MessageBox.Show("Examination has been scheduled successfuly!");
                        Owner.Close();
                    }
                    else
                    {
                        MessageBox.Show("An error has occured, examination is NOT scheduled!");
                    }
                }
                this.Close();
            }
        }

        private void ScheduleEmergencyExamination()
        {
            String patient = patientsComboBox.SelectedItem.ToString();
            int patientID = int.Parse(patient.Split(" ")[0]);
            Examination examination = examinationServerController.ScheduleEmergencyExamination(recommendationDto.TimeInterval.Start, recommendationDto.Doctor.Id, patientID);
            if (examination != null)
            {
                MessageBox.Show("Examination has been scheduled successfuly!");
            }
            else
            {
                MessageBox.Show("An error has occured, examination is NOT scheduled!");
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
                patientsComboBox.Items.Add(p.Id.ToString() + " " + p.Person.Name + " " + p.Person.Surname + " - " + p.Person.Id);
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
