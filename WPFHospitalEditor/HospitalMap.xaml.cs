using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using HealthcareBase.Dto;
using System.Linq;
using HealthcareBase.Model.Users.Employee.Doctors.DTOs;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Utilities;
using HospitalWebApp.Dtos;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for HospitalMap.xaml
    /// </summary>
    public partial class HospitalMap : Window
    {
        IEquipmentServerController equipmentServerController = new EquipmentServerController();
        IMapObjectController mapObjectController = new MapObjectController();
        IEquipmentTypeServerController equipmentTypeServerController = new EquipmentTypeServerController();
        IMedicationServerController medicationServerController = new MedicationServerController();
        IDoctorServerController doctorServerController = new DoctorServerController();
        ISchedulingServerController schedulingController = new SchedulingServerController();

        public static Canvas canvasHospitalMap;  
        public static Role role;
        public static List<MapObject> searchResult = new List<MapObject>();
        public static List<EquipmentDto> equipmentSearchResult = new List<EquipmentDto>();
        public static List<MedicationDto> medicationSearchResult = new List<MedicationDto>();
        public static List<RecommendationDto> appointmentSearchResult = new List<RecommendationDto>();
        private const int regularExaminationDepartment = 1;

        public HospitalMap(List<MapObject> allMapObjects, Role role)
        {                     
            InitializeComponent();
            setMapObjectTypeComboBox();
            setEquipmentTypeComboBox();
            setMedicationNameComboBox();
            setDoctorNameComboBox();
            setNonSelectedComboBoxItem();
            CanvasService.addObjectToCanvas(mapObjectController.getOutterMapObjects(), canvas);
            canvasHospitalMap = canvas;
            HospitalMap.role = role;
            if (IsRoleLogged(Role.Patient)) equipmentAndMedicineSearchStackPanel.Visibility = Visibility.Hidden;
            if (!IsRoleLogged(Role.Secretary)) appointmentSearchStackPanel.Visibility = Visibility.Hidden;
        }
        
        private void selectBuilding(object sender, MouseButtonEventArgs e)
        {
            MapObject chosenBuilding = CanvasService.checkWhichObjectIsClicked(e, mapObjectController.getAllMapObjects(), canvas);
            if (chosenBuilding != null && chosenBuilding.MapObjectType == MapObjectType.Building)
            {
                goToClickedBuilding(chosenBuilding);
            }
        }
       
        private void goToClickedBuilding(MapObject mapObject)
        {
            List<MapObject> buildingObjects = new List<MapObject>();
            MapObjectController mapObjectController = new MapObjectController();
            foreach (MapObject mapObjectIteration in mapObjectController.getAllMapObjects())
            {
                if (mapObject.Id.ToString().Equals(findBuilding(mapObjectIteration)))
                {                   
                    buildingObjects.Add(mapObjectIteration);
                }                
            }
            canvas.Children.Clear();
            Building building = new Building(buildingObjects, 0);
            building.Owner = this;
            this.Hide();
            building.ShowDialog();
        }

        private String findBuilding(MapObject mapObjectIteration)
        {
            String[] firstSplit = mapObjectIteration.Description.Split("&");
            String[] buildingIndex = firstSplit[0].Split("-");
            return buildingIndex[0];
        }

        private void Basic_Search(object sender, RoutedEventArgs e)
        {
            clearAllResults();
            searchResult = mapObjectController.searchForMapObjects(searchInputTB.Text, searchInputComboBox.Text);
            if (searchResult.Count > 0)
            {
                SearchResultDialog searchResultDialog = new SearchResultDialog(this, SearchType.MapObjectSearch);
                searchResultDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("There is nothing we could find.");
            }
        }

        private bool isNoNameObject(MapObjectType mop)
        {
            return mop.Equals(MapObjectType.Parking) ||
                   mop.Equals(MapObjectType.ParkingSlot) ||
                   mop.Equals(MapObjectType.Road) ||
                   mop.Equals(MapObjectType.WaitingRoom);
        }

        public void Equipment_Search(object sender, RoutedEventArgs e)
        {
            clearAllResults();
            if (NoEquipmentTypeIsPicked())
            {
                MessageBox.Show("No equipment is picked.");
            }
            else
            {
                equipmentSearchResult = equipmentServerController.getEquipmentByType(equipmentSearchComboBox.Text).ToList();
                SearchResultDialog equipmentDialog = new SearchResultDialog(this, SearchType.EquipmentSearch);
                equipmentDialog.ShowDialog();
            }
        }

        public void Medication_Search(object sender, RoutedEventArgs e)
        {
            clearAllResults();
            if (NoMedicationNameIsPicked())
            {
                MessageBox.Show("No medication is picked.");
            }
            else
            {
                medicationSearchResult = medicationServerController.GetAllMedicationByName(medicationSearchComboBox.Text).ToList();
                SearchResultDialog medicationDialog = new SearchResultDialog(this, SearchType.MedicationSearch);
                medicationDialog.ShowDialog();
            }
        }

        public void appointmentSearch_Click(object sender, RoutedEventArgs e)
        {
            clearAllResults();
            if (InvalidInputForAppointment())
            {
                MessageBox.Show("Invalid input.");
            }
            else
            {
                int DoctorId = int.Parse(doctorsComboBox.SelectedItem.ToString().Split(" ")[0]);
                DateTime startDate = DateTime.ParseExact(startDatePicker.Text + AllConstants.DayStart, "MM/dd/yyyy HH:mm", null);
                DateTime endDate = DateTime.ParseExact(endDatePicker.Text + AllConstants.DayEnd, "MM/dd/yyyy HH:mm", null);
                RecommendationRequestDto recommendationRequestDto = new RecommendationRequestDto()
                {
                    DoctorId = DoctorId,
                    SpecialtyId = regularExaminationDepartment,
                    TimeInterval = new TimeInterval(startDate, endDate),
                    Preference = GetRecommendationPreference()
                };

                appointmentSearchResult = schedulingController.GetAppointments(recommendationRequestDto);
                if (appointmentSearchResult != null)
                {
                    SearchResultDialog appointmentDialog = new SearchResultDialog(this, SearchType.AppointmentSearch);
                    appointmentDialog.ShowDialog();
                }
                else
                {
                    MessageBox.Show("There are no available appointments for chosen period!");
                }
               
            }
        }

        private void setMapObjectTypeComboBox()
        {
            foreach (MapObjectType mop in Enum.GetValues(typeof(MapObjectType)))
            {
                if(!isNoNameObject(mop))
                {
                    searchInputComboBox.Items.Add(mop);
                } 
            }
        }

        private void setEquipmentTypeComboBox()
        {
            foreach (EquipmentTypeDto eqTD in equipmentTypeServerController.GetAllEquipmentTypes())
            {
                equipmentSearchComboBox.Items.Add(eqTD.Name);
            }
        }

        private void setMedicationNameComboBox()
        {
            foreach (MedicationDto medDto in medicationServerController.GetAllMedication())
            {
                medicationSearchComboBox.Items.Add(medDto.Name);
            }
        }

        private void setDoctorNameComboBox()
        {
            foreach (DoctorDto docDto in doctorServerController.GetDoctorsByDepartment(regularExaminationDepartment))
            {
                doctorsComboBox.Items.Add(docDto.DoctorId + " " + docDto.Name + " " + docDto.Surname);
            }
        }

        private Boolean IsRoleLogged(Role r)
        {
            if (role == r) return true;
            return false;
        }

        private void clearAllResults()
        {
            searchResult.Clear();
            equipmentSearchResult.Clear();
            medicationSearchResult.Clear();
            SearchResultDialog.selectedObjectId = -1;
        }

        private Boolean NoEquipmentTypeIsPicked()
        {
            if (equipmentSearchComboBox.Text.Equals(AllConstants.emptyComboBox)) return true;
            return false;
        }

        private Boolean NoMedicationNameIsPicked()
        {
            if (medicationSearchComboBox.Text.Equals(AllConstants.emptyComboBox)) return true;
            return false;
        }
        private void setNonSelectedComboBoxItem()
        {
            emptyMapObjectComboBox.Content = AllConstants.emptyComboBox;
            emptyMedicationComboBox.Content = AllConstants.emptyComboBox;
            emptyEquipmentComboBox.Content = AllConstants.emptyComboBox;
        }

        private bool InvalidInputForAppointment()
        {
            if (doctorsComboBox.Text.Equals("") || startDatePicker.Text.Equals("") || endDatePicker.Text.Equals("") || InvalidDateInput())
            {
                return true;
            }          
            return false;
        }

        private bool InvalidDateInput()
        {
            if (endDatePicker.SelectedDate < startDatePicker.SelectedDate)
            {
                return true;
            }
            return false;
        }
        private RecommendationPreference GetRecommendationPreference()
        {
            if (PriorityComboBox.SelectedIndex == 0) return RecommendationPreference.Doctor;
            return RecommendationPreference.Time;
        }
    }
}
