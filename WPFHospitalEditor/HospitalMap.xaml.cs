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
using HealthcareBase.Model.Users.Employee.Doctors;

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

        public HospitalMap(List<MapObject> allMapObjects, Role role)
        {
            InitializeComponent();
            SetMapObjectTypeComboBox();
            SetEquipmentTypeComboBox();
            SetMedicationNameComboBox();
            SetDoctorNameComboBox();
            SetSpecialistNameComboBox();
            SetSpecialistEquipmentComboBox();
            SetNonSelectedComboBoxItem();
            CanvasService.AddObjectToCanvas(mapObjectController.GetOutterMapObjects(), canvas);
            canvasHospitalMap = canvas;
            HospitalMap.role = role;
            if (IsRoleLogged(Role.Patient))
            {
                EquipmentSearchTab.Visibility = Visibility.Hidden;
                MedicationSearchTab.Visibility = Visibility.Hidden;
            }
            if (!IsRoleLogged(Role.Secretary))
            {
                AppointmentSearchTab.Visibility = Visibility.Hidden;
                SpecialistAppointmentSearchTab.Visibility = Visibility.Hidden;
            }
        }

        private void SelectBuilding(object sender, MouseButtonEventArgs e)
        {
            MapObject chosenBuilding = CanvasService.CheckWhichObjectIsClicked(e, mapObjectController.GetAllMapObjects(), canvas);
            if (chosenBuilding != null && chosenBuilding.MapObjectType == MapObjectType.Building)
            {
                GoToClickedBuilding(chosenBuilding);
            }
        }

        private void GoToClickedBuilding(MapObject mapObject)
        {
            List<MapObject> buildingObjects = new List<MapObject>();
            foreach (MapObject mapObjectIteration in mapObjectController.GetAllMapObjects())
            {
                if (mapObject.Id.ToString().Equals(FindBuilding(mapObjectIteration)))
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

        private String FindBuilding(MapObject mapObjectIteration)
        {
            String[] firstSplit = mapObjectIteration.Description.Split("&");
            String[] buildingIndex = firstSplit[0].Split("-");
            return buildingIndex[0];
        }

        private void Basic_Search(object sender, RoutedEventArgs e)
        {
            ClearAllResults();
            searchResult = mapObjectController.SearchForMapObjects(searchInputTB.Text, searchInputComboBox.Text);
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

        private bool IsNoNameObject(MapObjectType mop)
        {
            return mop.Equals(MapObjectType.Parking) ||
                   mop.Equals(MapObjectType.ParkingSlot) ||
                   mop.Equals(MapObjectType.Road) ||
                   mop.Equals(MapObjectType.WaitingRoom) ||
                   mop.Equals(MapObjectType.Building);
        }

        public void Equipment_Search(object sender, RoutedEventArgs e)
        {
            ClearAllResults();
            if (NoEquipmentTypeIsPicked())
            {
                MessageBox.Show("No equipment is picked.");
            }
            else
            {
                equipmentSearchResult = equipmentServerController.GetEquipmentByType(equipmentSearchComboBox.Text).ToList();
                SearchResultDialog equipmentDialog = new SearchResultDialog(this, SearchType.EquipmentSearch);
                equipmentDialog.ShowDialog();
            }
        }

        public void Medication_Search(object sender, RoutedEventArgs e)
        {
            ClearAllResults();
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

        public void AppointmentSearch_Click(object sender, RoutedEventArgs e)
        {
            ClearAllResults();
            if (InvalidInputForAppointment())
            {
                MessageBox.Show("Invalid input.");
            }
            else
            {
                int DoctorId = int.Parse(doctorsComboBox.SelectedItem.ToString().Split(" ")[0]);
                DateTime startDate = DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy") + AllConstants.DayStart, "MM/dd/yyyy HH:mm", null);
                DateTime endDate = DateTime.ParseExact(endDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy") + AllConstants.DayEnd, "MM/dd/yyyy HH:mm", null);

                RecommendationRequestDto recommendationRequestDto = new RecommendationRequestDto()
                {
                    DoctorId = DoctorId,
                    SpecialtyId = AllConstants.RegularExaminationDepartment,
                    TimeInterval = new TimeInterval(startDate, endDate),
                    Preference = GetRecommendationPreference(PriorityComboBox)
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

        private void SpecialistAppointmentSearch_Click(object sender, RoutedEventArgs e)
        {
            ClearAllResults();

            if (InvalidInputForSpecialistAppointment())
            {
                MessageBox.Show("Invalid input.");
                return;
            }

            String specialist = specialistComboBox.SelectedItem.ToString();
            Doctor chosenDoctor = doctorServerController.GetDoctorById(int.Parse(specialist.Split(" ")[0]));
            DateTime startDate = DateTime.ParseExact(startDatePickerSpecApp.SelectedDate.Value.ToString("MM/dd/yyyy") + AllConstants.DayStart, "MM/dd/yyyy HH:mm", null);
            DateTime endDate = DateTime.ParseExact(endDatePickerSpecApp.SelectedDate.Value.ToString("MM/dd/yyyy") + AllConstants.DayEnd, "MM/dd/yyyy HH:mm", null);

            TimeInterval timeInterval = new TimeInterval(startDate, endDate);

            RecommendationRequestDto recommendationRequestDto = new RecommendationRequestDto()
            {
                DoctorId = chosenDoctor.Id,
                SpecialtyId = chosenDoctor.DepartmentId,
                TimeInterval = timeInterval,
                Preference = GetRecommendationPreference(specialistPriorityComboBox)
            };

            appointmentSearchResult = schedulingController.GetAppointments(recommendationRequestDto);

            if (!CheckEquipmentExistance())
            {
                MessageBox.Show("There is no room with required equipment!");
                return;
            }

            SearchResultDialog appointmentDialog = new SearchResultDialog(this, SearchType.AppointmentSearch);
            appointmentDialog.ShowDialog();
        }

        private void EquipmentTextInputChanged(object sender, EventArgs e)
        {
            SetComboBoxDefaultValues(equipmentSearchComboBox);
            SetEquipmentTypeComboBox();
        }

        private void SetEquipmentTypeComboBox()
        {
            foreach (EquipmentTypeDto eqTypeDto in equipmentTypeServerController.GetFilteredEquipmentTypes(EquipmentSearchInput.Text))
            {
                equipmentSearchComboBox.Items.Add(eqTypeDto.Name);
            }
        }

        private void MedicationTextInputChanged(object sender, EventArgs e)
        {
            SetComboBoxDefaultValues(medicationSearchComboBox);
            SetMedicationNameComboBox();
        }

        private void SetComboBoxDefaultValues(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add("None");
            comboBox.SelectedIndex = 0;
        }
        private void SetMedicationNameComboBox()
        {
            foreach (MedicationDto medDto in medicationServerController.GetFilteredMedications(MedicationSearchInput.Text))
            {
                medicationSearchComboBox.Items.Add(medDto.Name);
            }
        }

        private void DoctorTextInputChanged(object sender, EventArgs e)
        {
            SetComboBoxDefaultValues(doctorsComboBox);
            SetDoctorNameComboBox();
        }

        private void SetDoctorNameComboBox()
        {
            foreach (DoctorDto docDto in doctorServerController.GetFilteredDoctors(DoctorSearchInput.Text))
            {
                doctorsComboBox.Items.Add(docDto.DoctorId + " " + docDto.Name + " " + docDto.Surname);
            }
        }

        private void SpecialistTextInputChanged(object sender, EventArgs e)
        {
            SetComboBoxDefaultValues(specialistComboBox);
            SetSpecialistNameComboBox();
        }

        private void SetSpecialistNameComboBox()
        {
            foreach (DoctorDto docDto in doctorServerController.GetFilteredSpecialists(SpecialistSearchInput.Text))
            {
                specialistComboBox.Items.Add(docDto.DoctorId.ToString() + " " + docDto.Name + " " + docDto.Surname + " [" + docDto.DepartmentName + "]");
            }
        }

        private void SpecialistEquipmentTextInputChanged(object sender, EventArgs e)
        {
            SetComboBoxDefaultValues(specialistEquipmentAppSearchComboBox);
            SetSpecialistEquipmentComboBox();
        }

        private void SetSpecialistEquipmentComboBox()
        {
            List<EquipmentTypeDto> equipmentTypes = equipmentTypeServerController.GetAllEquipmentTypes().ToList();

            foreach (EquipmentTypeDto eqTypeDto in equipmentTypeServerController.GetFilteredEquipmentTypes(EquipmentForSpecialistAppSearchInput.Text))
            {
                specialistEquipmentAppSearchComboBox.Items.Add(eqTypeDto.Name);
            }
        }
        private void SetMapObjectTypeComboBox()
        {
            foreach (MapObjectType mapObjectType in Enum.GetValues(typeof(MapObjectType)))
            {
                if (!IsNoNameObject(mapObjectType) && CompareInput(mapObjectType, searchInputTB.Text))
                {
                    searchInputComboBox.Items.Add(mapObjectType);
                }
            }
        }

        private bool CompareInput(MapObjectType mapObjectType, string name)
        {
            return mapObjectType.ToString().ToLower().Contains(name.ToLower());
        }

        private Boolean IsRoleLogged(Role r)
        {
            if (role == r) return true;
            return false;
        }

        private void ClearAllResults()
        {
            searchResult.Clear();
            equipmentSearchResult.Clear();
            medicationSearchResult.Clear();
            SearchResultDialog.selectedObjectId = -1;
        }

        private Boolean NoEquipmentTypeIsPicked()
        {
            if (equipmentSearchComboBox.Text.Equals(AllConstants.EmptyComboBox)) return true;
            return false;
        }

        private Boolean NoMedicationNameIsPicked()
        {
            if (medicationSearchComboBox.Text.Equals(AllConstants.EmptyComboBox)) return true;
            return false;
        }
        private void SetNonSelectedComboBoxItem()
        {
            emptyMapObjectComboBox.Content = AllConstants.EmptyComboBox;
            emptyMedicationComboBox.Content = AllConstants.EmptyComboBox;
            emptyEquipmentComboBox.Content = AllConstants.EmptyComboBox;
            emptyDoctorComboBox.Content = AllConstants.EmptyComboBox;
            emptySpecialistComboBox.Content = AllConstants.EmptyComboBox;
            emptySpecialistEquipmentAppSearchComboBox.Content = AllConstants.EmptyComboBox;
        }

        private bool InvalidInputForAppointment()
        {
            if (doctorsComboBox.Text.Equals("None") || startDatePicker.Text.Equals("") || endDatePicker.Text.Equals("") || InvalidDateInput())
            {
                return true;
            }
            return false;
        }

        private bool InvalidInputForSpecialistAppointment()
        {
            if (specialistComboBox.Text.Equals("None") || startDatePickerSpecApp.Text.Equals("")
                || endDatePickerSpecApp.Text.Equals("") || specialistEquipmentAppSearchComboBox.Text.Equals("None")
                || specialistPriorityComboBox.Text.Equals("")) return true;
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

        private RecommendationPreference GetRecommendationPreference(ComboBox comboBox)
        {
            if (comboBox.SelectedIndex == 0) return RecommendationPreference.Doctor;
            return RecommendationPreference.Time;
        }

        private bool CheckEquipmentExistance()
        {
            for (int i = 0; i < HospitalMap.appointmentSearchResult.Count; i++)
            {
                int roomId = HospitalMap.appointmentSearchResult[i].RoomId;
                List<EquipmentDto> equipmentDtos = equipmentServerController.GetEquipmentByRoomId(roomId).ToList();
                if (CheckEquipmentInRoomExistance(equipmentDtos))
                    return true;
            }
            return false;
        }

        private bool CheckEquipmentInRoomExistance(List<EquipmentDto> equipmentDtos)
        {
            foreach (EquipmentDto eq in equipmentDtos)
            {
                if (eq.Name.Equals(specialistEquipmentAppSearchComboBox.SelectedItem))
                    return true;
            }
            return false;
        }

        private void MapObjectTextInputChanged(object sender, TextChangedEventArgs e)
        {
            SetComboBoxDefaultValues(searchInputComboBox);
            SetMapObjectTypeComboBox();
        }
    }
}
