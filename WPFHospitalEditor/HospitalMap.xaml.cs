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

        public static Canvas canvasHospitalMap;  
        public static Role role;
        public static List<MapObject> searchResult = new List<MapObject>();
        public static List<EquipmentDto> equipmentSearchResult = new List<EquipmentDto>();
        public static List<MedicationDto> medicationSearchResult = new List<MedicationDto>();


        public HospitalMap(List<MapObject> allMapObjects, Role role)
        {                     
            InitializeComponent();
            setMapObjectTypeComboBox();
            setEquipmentTypeComboBox();
            setMedicationNameComboBox();
            CanvasService.addObjectToCanvas(mapObjectController.getOutterMapObjects(), canvas);
            canvasHospitalMap = canvas;
            HospitalMap.role = role;
            if (IsPatientLogged()) equipmentAndMedicineSearchStackPanel.Visibility = Visibility.Hidden;
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
            mapObjectController.checkMapObjectSearchInput(searchInputTB.Text, searchInputComboBox.Text);
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

        public void Equipment_Search(object sender, RoutedEventArgs e)
        {
            checkEquipmentSearchInput();
        }

        public void checkEquipmentSearchInput()
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
            checkMedicineSearchInput();
        }

        public void checkMedicineSearchInput()
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

        private Boolean IsPatientLogged()
        {
            if (role == Role.Patient) return true;
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
            if (equipmentSearchComboBox.Text.Equals("Pick equipment type")) return true;
            return false;
        }

        private Boolean NoMedicationNameIsPicked()
        {
            if (medicationSearchComboBox.Text.Equals("Pick medication name")) return true;
            return false;
        }
    }
}
