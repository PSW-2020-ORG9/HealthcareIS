using HealthcareBase.Dto;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using HealthcareBase.Model.Utilities;
using HospitalWebApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for SpecialistAppointmentSearchDialog.xaml
    /// </summary>
    public partial class SpecialistAppointmentSearchDialog : Window
    {
        IDoctorServerController doctorServerController = new DoctorServerController();
        IEquipmentTypeServerController equipmentTypeServerController = new EquipmentTypeServerController();
        IEquipmentServerController equipmentServerController = new EquipmentServerController();
        ISchedulingServerController schedulingServerController = new SchedulingServerController();
        HospitalMap hospitalMap;

        public SpecialistAppointmentSearchDialog(HospitalMap hospitalMap)
        {
            InitializeComponent();
            this.hospitalMap = hospitalMap;
            loadSpecialists();
            loadEquipment();
        }

        private void loadSpecialists()
        {
            List<DoctorDto> specialists = doctorServerController.GetAllSpecialists().ToList();
            foreach(DoctorDto d in specialists)
            {
                specialistComboBox.Items.Add(d.DoctorId.ToString() + " " + d.Name + " " + d.Surname + " [" + d.DepartmentName + "]");
            }
            
        }

        private void loadEquipment()
        {
            List<EquipmentTypeDto> equipmentTypes = equipmentTypeServerController.GetAllEquipmentTypes().ToList();
            
            foreach(EquipmentTypeDto e in equipmentTypes)
            {
                equipmentComboBox.Items.Add(e.Name);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            HospitalMap.ClearAllResults();

            if (InvalidInputForAppointment())
            {
                MessageBox.Show("Invalid input.");
                return;
            }

            String specialist = specialistComboBox.SelectedItem.ToString();
            int specialistID = int.Parse(specialist.Split(" ")[0]);
            Doctor chosenDoctor = doctorServerController.GetDoctorById(specialistID);
            DateTime startDate = DateTime.ParseExact(dateFrom.Text + AllConstants.DayStart, "MM/dd/yyyy HH:mm", null);
            DateTime endDate = DateTime.ParseExact(dateTo.Text + AllConstants.DayEnd, "MM/dd/yyyy HH:mm", null);

            TimeInterval timeInterval = new TimeInterval(startDate, endDate);

            RecommendationRequestDto recommendationRequestDto = new RecommendationRequestDto()
            {
                DoctorId = chosenDoctor.Id,
                SpecialtyId = chosenDoctor.DepartmentId,
                TimeInterval = timeInterval,
                Preference = getPriorityFromComboBox()
            };

            HospitalMap.appointmentSearchResult = schedulingServerController.GetAppointments(recommendationRequestDto);

            if (!CheckEquipmentExistance())
            {
                MessageBox.Show("There is no room with required equipment!");
                return;
            }

            SearchResultDialog appointmentDialog = new SearchResultDialog(hospitalMap, SearchType.AppointmentSearch);
            appointmentDialog.ShowDialog();
        }

        private bool InvalidInputForAppointment()
        {
            if (specialistComboBox.Text.Equals("") || dateFrom.Text.Equals("") || dateTo.Text.Equals("") || equipmentComboBox.Text.Equals("") || priorityComboBox.Text.Equals("")) return true;
            return false;
        }

        private RecommendationPreference getPriorityFromComboBox()
        {
            if (priorityComboBox.SelectedIndex == 0) return RecommendationPreference.Doctor;
            return RecommendationPreference.Time;
        }

        private bool CheckEquipmentExistance()
        {
            for (int i = 0; i < HospitalMap.appointmentSearchResult.Count; i++)
            {
                int roomId = HospitalMap.appointmentSearchResult[i].RoomId;
                List<EquipmentDto> equipmentDtos = equipmentServerController.GetEquipmentByRoomId(roomId).ToList();
                if(CheckEquipmentInRoomExistance(equipmentDtos))
                    return true;
            }
            return false;
        }

        private bool CheckEquipmentInRoomExistance(List<EquipmentDto> equipmentDtos)
        {
            foreach (EquipmentDto eq in equipmentDtos)
            {
                if (eq.Name.Equals(equipmentComboBox.SelectedItem))
                    return true;
            }
            return false;
        }
    }
}
