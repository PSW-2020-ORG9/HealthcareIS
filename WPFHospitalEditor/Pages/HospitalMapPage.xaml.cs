using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.StrategyPattern;

namespace WPFHospitalEditor.Pages
{
    /// <summary>
    /// Interaction logic for HospitalMapPage.xaml
    /// </summary>
    public partial class HospitalMapPage : Page
    {
        private readonly IEquipmentServerController equipmentServerController = new EquipmentServerController();
        private readonly IMapObjectController mapObjectController = new MapObjectController();
        private readonly IEquipmentTypeServerController equipmentTypeServerController = new EquipmentTypeServerController();
        private readonly IMedicationServerController medicationServerController = new MedicationServerController();
        private readonly IDoctorServerController doctorServerController = new DoctorServerController();
        private readonly ISchedulingServerController schedulingController = new SchedulingServerController();

        public static Canvas canvasHospitalMap;

        public HospitalMapPage()
        {
            InitializeComponent();
            CanvasService.AddObjectToCanvas(mapObjectController.GetOutterMapObjects(), canvas);
            canvasHospitalMap = canvas;
            SetComponentsVisibility();
        }

        private void SetComponentsVisibility()
        {
            if (HospitalMainWindow.role == Role.Patient)
            {
                EquipmentSearchTab.Visibility = Visibility.Hidden;
                MedicationSearchTab.Visibility = Visibility.Hidden;
            }
            if (HospitalMainWindow.role != Role.Secretary)
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
                HospitalMainWindow window = HospitalMainWindow.GetInstance(HospitalMainWindow.role);
                window.ChangePage(new BuildingPage(chosenBuilding.Id));
            }
        }

        private void Basic_Search(object sender, RoutedEventArgs e)
        {
            ClearAllResults();
            List<MapObject> searchResults = mapObjectController.SearchMapObjects(searchInputTB.Text, searchInputComboBox.Text);
            if (searchResults.Count == 0)
            {
                MessageBox.Show("There is nothing we could find.");
                return;
            }
            ISearchResultStrategy strategy = new SearchResultStrategy(new MapObjectSearchResult(searchResults));
            SearchResultDialog searchResultDialog = new SearchResultDialog(strategy.GetSearchResult(), SearchType.MapObjectSearch);
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
            ISearchResultStrategy strategy = new SearchResultStrategy(new EquipmentSearchResult(equipmentSearchComboBox.Text));
            SearchResultDialog equipmentDialog = new SearchResultDialog(strategy.GetSearchResult(), SearchType.EquipmentSearch);
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
            ISearchResultStrategy strategy = new SearchResultStrategy(new MedicationSearchResult(medicationSearchComboBox.Text));
            SearchResultDialog medicationDialog = new SearchResultDialog(strategy.GetSearchResult(), SearchType.MedicationSearch);
            medicationDialog.ShowDialog();
        }

        public void AppointmentSearch_Click(object sender, RoutedEventArgs e)
        {
            DoSearch(false);
        }

        private void SpecialistAppointmentSearch_Click(object sender, RoutedEventArgs e)
        {
            DoSearch(true);
        }

        private void DoSearch(bool isSpecialistSearch)
        {
            ClearAllResults();
            if (isSpecialistSearch ? InvalidInputForSpecialistAppointment() : InvalidInputForAppointment())
            {
                MessageBox.Show("Invalid input.");
                return;
            }

            DateTime startDate = DateTime.ParseExact(GetStartDatePicker(isSpecialistSearch).SelectedDate.Value.ToString("MM/dd/yyyy") + AllConstants.DayStart, "MM/dd/yyyy HH:mm", null);
            DateTime endDate = DateTime.ParseExact(GetEndDatePicker(isSpecialistSearch).SelectedDate.Value.ToString("MM/dd/yyyy") + AllConstants.DayEnd, "MM/dd/yyyy HH:mm", null);

            Doctor chosenDoctor = GetChosenDoctor(isSpecialistSearch);
            RecommendationRequestDto recommendationRequestDto = new RecommendationRequestDto()
            {
                DoctorId = chosenDoctor.Id,
                SpecialtyId = chosenDoctor.DepartmentId,
                TimeInterval = new TimeInterval(startDate, endDate),
                Preference = GetRecommendationPreference(isSpecialistSearch ? specialistPriorityComboBox : PriorityComboBox)
            };
            List<RecommendationDto> searchResults = schedulingController.GetAppointments(recommendationRequestDto);

            if (!IsValidRequest(isSpecialistSearch, searchResults))
            {
                MessageBox.Show(isSpecialistSearch ? "There is no room with required equipment!" : "There are no available appointments for chosen period!");
                return;
            }

            ISearchResultStrategy strategy = new SearchResultStrategy(new AppointmentSearchResult(searchResults));
            SearchResultDialog appointmentDialog = new SearchResultDialog(strategy.GetSearchResult(), SearchType.AppointmentSearch);
            appointmentDialog.ShowDialog();
        }

        private bool IsValidRequest(bool isSpecialistSearch, List<RecommendationDto> searchResults)
        {
            return !(searchResults == null || (isSpecialistSearch && CheckEquipmentExistance(searchResults)));
        }

        private DatePicker GetStartDatePicker(bool isSpecialistSearch)
        {
            return isSpecialistSearch ? startDatePickerSpecApp : startDatePicker;
        }

        private DatePicker GetEndDatePicker(bool isSpecialistSearch)
        {
            return isSpecialistSearch ? endDatePickerSpecApp : endDatePicker;
        }

        private Doctor GetChosenDoctor(bool isSpecialistSearch)
        {
            if (isSpecialistSearch)
            {
                String specialist = specialistComboBox.SelectedItem.ToString();
                return doctorServerController.GetDoctorById(int.Parse(specialist.Split(" ")[0]));
            }
            return doctorServerController.GetDoctorById(int.Parse(doctorsComboBox.SelectedItem.ToString().Split(" ")[0]));
        }

        private void EquipmentTextInputChanged(object sender, EventArgs e)
        {
            SetEquipmentTypeComboBox();
        }

        private void SetEquipmentTypeComboBox()
        {
            SetComboBoxDefaultValues(equipmentSearchComboBox);
            foreach (EquipmentTypeDto eqTypeDto in equipmentTypeServerController.SearchEquipmentTypes(EquipmentSearchInput.Text))
                equipmentSearchComboBox.Items.Add(eqTypeDto.Name);
        }

        private void MedicationTextInputChanged(object sender, EventArgs e)
        {
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
            SetComboBoxDefaultValues(medicationSearchComboBox);
            foreach (MedicationDto medDto in medicationServerController.SearchMedications(MedicationSearchInput.Text))
                medicationSearchComboBox.Items.Add(medDto.Name);
        }

        private void DoctorTextInputChanged(object sender, EventArgs e)
        {
            SetDoctorNameComboBox();
        }

        private void SetDoctorNameComboBox()
        {
            SetComboBoxDefaultValues(doctorsComboBox);
            foreach (DoctorDto docDto in doctorServerController.SearchDoctors(DoctorSearchInput.Text))
                doctorsComboBox.Items.Add(docDto.DoctorId + " " + docDto.Name + " " + docDto.Surname);
        }

        private void SpecialistTextInputChanged(object sender, EventArgs e)
        {
            SetSpecialistNameComboBox();
        }

        private void SetSpecialistNameComboBox()
        {
            SetComboBoxDefaultValues(specialistComboBox);
            foreach (DoctorDto docDto in doctorServerController.SearchSpecialists(SpecialistSearchInput.Text))
                specialistComboBox.Items.Add(docDto.DoctorId.ToString() + " " + docDto.Name + " " + docDto.Surname + " [" + docDto.DepartmentName + "]");
        }

        private void SpecialistEquipmentTextInputChanged(object sender, EventArgs e)
        {
            SetSpecialistEquipmentComboBox();
        }

        private void SetSpecialistEquipmentComboBox()
        {
            SetComboBoxDefaultValues(specialistEquipmentAppSearchComboBox);
            foreach (EquipmentTypeDto eqTypeDto in equipmentTypeServerController.SearchEquipmentTypes(EquipmentForSpecialistAppSearchInput.Text))
                specialistEquipmentAppSearchComboBox.Items.Add(eqTypeDto.Name);
        }

        private void SetMapObjectTypeComboBox()
        {
            SetComboBoxDefaultValues(searchInputComboBox);
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
            SearchResultDialog.selectedObjectId = -1;
        }

        private Boolean NoEquipmentTypeIsPicked()
        {
            return equipmentSearchComboBox.Text.Equals(AllConstants.EmptyComboBox);
        }

        private Boolean NoMedicationNameIsPicked()
        {
            return medicationSearchComboBox.Text.Equals(AllConstants.EmptyComboBox);
        }

        private bool InvalidInputForAppointment()
        {
            return doctorsComboBox.Text.Equals(AllConstants.EmptyComboBox) || startDatePicker.Text.Equals("") || endDatePicker.Text.Equals("") || InvalidDateInput();
        }

        private bool InvalidInputForSpecialistAppointment()
        {
            return specialistComboBox.Text.Equals(AllConstants.EmptyComboBox) || startDatePickerSpecApp.Text.Equals("")
                || endDatePickerSpecApp.Text.Equals("") || specialistEquipmentAppSearchComboBox.Text.Equals(AllConstants.EmptyComboBox)
                || specialistPriorityComboBox.Text.Equals("");
        }

        private bool InvalidDateInput()
        {
            return endDatePicker.SelectedDate < startDatePicker.SelectedDate;
        }

        private RecommendationPreference GetRecommendationPreference(ComboBox comboBox)
        {
            if (comboBox.SelectedIndex == 0) return RecommendationPreference.Doctor;
            return RecommendationPreference.Time;
        }

        private bool CheckEquipmentExistance(List<RecommendationDto> searchResults)
        {
            for (int i = 0; i < searchResults.Count; i++)
             {
                 int roomId = searchResults[i].RoomId;
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
            SetMapObjectTypeComboBox();
        }

        private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                switch (tabControl.SelectedIndex)
                {
                    case 0:
                        SetMapObjectTypeComboBox();
                        break;
                    case 1:
                        SetMedicationNameComboBox();
                        break;
                    case 2:
                        SetEquipmentTypeComboBox();
                        break;
                    case 3:
                        SetDoctorNameComboBox();
                        break;
                    case 4:
                        SetSpecialistNameComboBox();
                        SetSpecialistEquipmentComboBox();
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
