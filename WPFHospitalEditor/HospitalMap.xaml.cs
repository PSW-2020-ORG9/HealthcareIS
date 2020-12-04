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

        public static Canvas canvasHospitalMap;  
        private Role role;
        public static List<MapObject> searchResult = new List<MapObject>();
        public static List<EquipmentDto> equipmentSearchResult = new List<EquipmentDto>();


        public HospitalMap(List<MapObject> allMapObjects, Role role)
        {                     
            InitializeComponent();
            setMapObjectTypeComboBox();
            setEquipmentTypeComboBox();
            CanvasService.addObjectToCanvas(mapObjectController.getOutterMapObjects(), canvas);
            canvasHospitalMap = canvas;
            this.role = role;
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
            Building building = new Building(buildingObjects, 0, role);
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
            List<MapObject> allMapObjects = mapObjectController.getAllMapObjects();
            foreach (MapObject mapObject in allMapObjects)
            {
                if (textBoxEmpty(mapObject))
                {
                    searchResult.Add(mapObject);
                }
                else if (typeNotChosen(mapObject))
                {
                    searchResult.Add(mapObject);
                }
                else if (bothParametersActive(mapObject))
                {
                    searchResult.Add(mapObject);
                }
            }

            if (searchResult.Count > 0)
            {
                SearchResultDialog searchResultDialog = new SearchResultDialog(this, role);
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

        private bool bothParametersActive(MapObject mapObject)
        {
            return mapObject.MapObjectType.ToString().Equals(searchInputComboBox.Text) && mapObject.Name.ToLower().Contains(searchInputTB.Text.ToLower()) && !searchInputTB.Text.Equals("");
        }

        private bool typeNotChosen(MapObject mapObject)
        {
            return mapObject.Name.ToLower().Contains(searchInputTB.Text.ToLower()) && searchInputComboBox.Text.Equals("Pick type of object") && !searchInputTB.Text.Equals("");
        }

        private bool textBoxEmpty(MapObject mapObject)
        {
            return mapObject.MapObjectType.ToString().Equals(searchInputComboBox.Text) && searchInputTB.Text.Equals("");
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
            clearAllResults();
            if (NoEquipmentTypeIsPicked())
            {
                MessageBox.Show("No equipment is picked.");
            }
            else
            {
                equipmentSearchResult = equipmentServerController.getEquipmentByType(equipmentSearchComboBox.Text).ToList();
                EquipmentSearchResultDialog equipmentDialog = new EquipmentSearchResultDialog(this, role);
                equipmentDialog.ShowDialog();
            }
        }

        private void setEquipmentTypeComboBox()
        {
            foreach (EquipmentTypeDto eqTD in equipmentTypeServerController.GetAllEquipmentTypes())
            {
                equipmentSearchComboBox.Items.Add(eqTD.Name);
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
            SearchResultDialog.selectedObjectId = -1;
        }

        private Boolean NoEquipmentTypeIsPicked()
        {
            if (equipmentSearchComboBox.Text.Equals("Pick equipment type")) return true;
            return false;
        }
    }
}
