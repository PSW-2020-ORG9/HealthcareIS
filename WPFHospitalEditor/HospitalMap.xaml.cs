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

        public HospitalMap(List<MapObject> allMapObjects)
        {                     
            InitializeComponent();
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
    }
}
