using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using System.Linq;
using WPFHospitalEditor.Repository;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

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
            SetNonSelectedComboBoxItem(emptyMapObjectComboBox);
            CanvasService.AddObjectToCanvas(mapObjectController.GetOutterMapObjects(), canvas);
            canvasHospitalMap = canvas;
            HospitalMap.role = role;
            SetComponentsVisibility();         
        }

        private void SetComponentsVisibility()
        {
            if (role == Role.Patient)
            {
                EquipmentSearchTab.Visibility = Visibility.Hidden;
                MedicationSearchTab.Visibility = Visibility.Hidden;
            }
            if (role != Role.Secretary)
            {
                AppointmentSearchTab.Visibility = Visibility.Hidden;
                SpecialistAppointmentSearchTab.Visibility = Visibility.Hidden;
            }
        }

        private void ShowBuilding(object sender, MouseButtonEventArgs e)
        {
            MapObject chosenBuilding = CanvasService.CheckWhichObjectIsClicked(e, mapObjectController.GetAllMapObjects(), canvas);
            if (chosenBuilding != null && chosenBuilding.MapObjectType == MapObjectType.Building)
            {
                canvas.Children.Clear();
                Building building = new Building(chosenBuilding.Id);
                building.Owner = this;
                this.Hide();
                building.ShowDialog();
            }
        }

        private void Basic_Search(object sender, RoutedEventArgs e)
        {
            ClearAllResults();
            searchResult = mapObjectController.SearchMapObjects(searchInputTB.Text, searchInputComboBox.Text);
            if (searchResult.Count == 0)
            {
                MessageBox.Show("There is nothing we could find.");
                return;
            }
            SearchResultDialog searchResultDialog = new SearchResultDialog(this, SearchType.MapObjectSearch);
            searchResultDialog.ShowDialog();
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
                return;
            }

            equipmentSearchResult = equipmentServerController.GetEquipmentByType(equipmentSearchComboBox.Text).ToList();
            SearchResultDialog equipmentDialog = new SearchResultDialog(this, SearchType.EquipmentSearch);
            equipmentDialog.ShowDialog();

        }

        public void Medication_Search(object sender, RoutedEventArgs e)
        {
            ClearAllResults();
            if (NoMedicationNameIsPicked())
            {
                MessageBox.Show("No medication is picked.");
                return;
            }
            medicationSearchResult = medicationServerController.GetAllMedicationByName(medicationSearchComboBox.Text).ToList();
            SearchResultDialog medicationDialog = new SearchResultDialog(this, SearchType.MedicationSearch);
            medicationDialog.ShowDialog();
        }

        public void AppointmentSearch_Click(object sender, RoutedEventArgs e)
        {
            ClearAllResults();
            if (InvalidInputForAppointment())
            {
                MessageBox.Show("Invalid input.");
                return;
            }

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
            if (appointmentSearchResult == null)
            {
                MessageBox.Show("There are no available appointments for chosen period!");
                return;
            }

            SearchResultDialog appointmentDialog = new SearchResultDialog(this, SearchType.AppointmentSearch);
            appointmentDialog.ShowDialog();

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
            foreach (EquipmentTypeDto eqTypeDto in equipmentTypeServerController.SearchEquipmentTypes(EquipmentSearchInput.Text))
                equipmentSearchComboBox.Items.Add(eqTypeDto.Name);
        }

        private void MedicationTextInputChanged(object sender, EventArgs e)
        {
            SetComboBoxDefaultValues(medicationSearchComboBox);
            SetMedicationNameComboBox();
        }

        private void SetComboBoxDefaultValues(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(AllConstants.EmptyComboBox);
            comboBox.SelectedIndex = 0;
        }
        private void SetMedicationNameComboBox()
        {
            foreach (MedicationDto medDto in medicationServerController.SearchMedications(MedicationSearchInput.Text))
                medicationSearchComboBox.Items.Add(medDto.Name);
        }

        private void DoctorTextInputChanged(object sender, EventArgs e)
        {
            SetComboBoxDefaultValues(doctorsComboBox);
            SetDoctorNameComboBox();
        }

        private void SetDoctorNameComboBox()
        {
            foreach (DoctorDto docDto in doctorServerController.SearchDoctors(DoctorSearchInput.Text))
                doctorsComboBox.Items.Add(docDto.DoctorId + " " + docDto.Name + " " + docDto.Surname);
        }

        private void SpecialistTextInputChanged(object sender, EventArgs e)
        {
            SetComboBoxDefaultValues(specialistComboBox);
            SetSpecialistNameComboBox();
        }

        private void SetSpecialistNameComboBox()
        {
            foreach (DoctorDto docDto in doctorServerController.SearchSpecialists(SpecialistSearchInput.Text))
                specialistComboBox.Items.Add(docDto.DoctorId.ToString() + " " + docDto.Name + " " + docDto.Surname + " [" + docDto.DepartmentName + "]");
        }

        private void SpecialistEquipmentTextInputChanged(object sender, EventArgs e)
        {
            SetComboBoxDefaultValues(specialistEquipmentAppSearchComboBox);
            SetSpecialistEquipmentComboBox();
        }

        private void SetSpecialistEquipmentComboBox()
        {
            foreach (EquipmentTypeDto eqTypeDto in equipmentTypeServerController.SearchEquipmentTypes(EquipmentForSpecialistAppSearchInput.Text))
                specialistEquipmentAppSearchComboBox.Items.Add(eqTypeDto.Name);
        }

        private void SetMapObjectTypeComboBox()
        {
            foreach (MapObjectType mapObjectType in Enum.GetValues(typeof(MapObjectType)))
            {
                if (!IsNoNameObject(mapObjectType) && CompareInput(mapObjectType, searchInputTB.Text))
                    searchInputComboBox.Items.Add(mapObjectType);
            }
        }

        private bool CompareInput(MapObjectType mapObjectType, string name)
        {
            return mapObjectType.ToString().ToLower().Contains(name.ToLower());
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
            return equipmentSearchComboBox.Text.Equals(AllConstants.EmptyComboBox);
        }

        private Boolean NoMedicationNameIsPicked()
        {
            return medicationSearchComboBox.Text.Equals(AllConstants.EmptyComboBox) ;
        }
        private void SetNonSelectedComboBoxItem(ComboBoxItem comboBoxItem)
        {
            comboBoxItem.Content = AllConstants.EmptyComboBox;
        }

        private bool InvalidInputForAppointment()
        {
            return doctorsComboBox.Text.Equals(AllConstants.EmptyComboBox) || startDatePicker.Text.Equals("") || endDatePicker.Text.Equals("") || InvalidDateInput() ;
        }

        private bool InvalidInputForSpecialistAppointment()
        {
            return specialistComboBox.Text.Equals(AllConstants.EmptyComboBox) || startDatePickerSpecApp.Text.Equals("")
                || endDatePickerSpecApp.Text.Equals("") || specialistEquipmentAppSearchComboBox.Text.Equals(AllConstants.EmptyComboBox)
                || specialistPriorityComboBox.Text.Equals("") ;
        }

        private bool InvalidDateInput()
        {
            return endDatePicker.SelectedDate < startDatePicker.SelectedDate ;
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

        private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                switch (tabControl.SelectedIndex)
                {
                    case 0:
                        SetComboBoxDefaultValues(searchInputComboBox);
                        SetMapObjectTypeComboBox();
                        SetNonSelectedComboBoxItem(emptySpecialistEquipmentAppComboBox);
                        break;
                    case 1:
                        SetComboBoxDefaultValues(medicationSearchComboBox);
                        SetMedicationNameComboBox();
                        SetNonSelectedComboBoxItem(emptyMedicationComboBox);
                        break;
                    case 2:
                        SetComboBoxDefaultValues(equipmentSearchComboBox);
                        SetEquipmentTypeComboBox();
                        SetNonSelectedComboBoxItem(emptyEquipmentComboBox);
                        break;
                    case 3:
                        SetComboBoxDefaultValues(doctorsComboBox);
                        SetDoctorNameComboBox();
                        SetNonSelectedComboBoxItem(emptyDoctorComboBox);
                        break;
                    case 4:
                        SetComboBoxDefaultValues(specialistComboBox);
                        SetComboBoxDefaultValues(specialistEquipmentAppSearchComboBox);
                        SetSpecialistNameComboBox();
                        SetNonSelectedComboBoxItem(emptySpecialistComboBox);
                        SetSpecialistEquipmentComboBox();
                        SetNonSelectedComboBoxItem(emptySpecialistEquipmentAppComboBox);
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
