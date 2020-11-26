using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Controller;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for HospitalMap.xaml
    /// </summary>
    public partial class HospitalMap : Window
    {       
        public static Canvas canvasHospitalMap;
        MapObjectController mapObjectController = new MapObjectController();
        public static List<MapObject> searchResult = new List<MapObject>();

        public HospitalMap(List<MapObject> allMapObjects)
        {                     
            InitializeComponent();
            setTypeComboBox();
            CanvasService.addObjectToCanvas(mapObjectController.getOutterMapObjects(allMapObjects), canvas);
            canvasHospitalMap = canvas;
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
            searchResult.Clear();
            List<MapObject> allMapObjects = mapObjectController.getAllMapObjects();

            foreach (MapObject mapObject in allMapObjects)
            {
                if (isNoNameObject(mapObject))
                {
                    MessageBox.Show("Wanted objects are shown on this map.");
                    return;
                }
                else if (textBoxEmpty(mapObject))
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
                SearchResultDialog searchResultDialog = new SearchResultDialog();
                searchResultDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("There is nothing we could find.");
            }


        }

        private bool isNoNameObject(MapObject mapObject)
        {
            return searchInputComboBox.Text.Equals(MapObjectType.ParkingSlot.ToString()) ||
                   searchInputComboBox.Text.Equals(MapObjectType.Parking.ToString()) ||
                   searchInputComboBox.Text.Equals(MapObjectType.Road.ToString()) ||
                   searchInputComboBox.Text.Equals(MapObjectType.WaitingRoom.ToString());
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

        private void setTypeComboBox()
        {
            foreach (MapObjectType mop in Enum.GetValues(typeof(MapObjectType)))
            {
                searchInputComboBox.Items.Add(mop);
            }
        }
    }
}
