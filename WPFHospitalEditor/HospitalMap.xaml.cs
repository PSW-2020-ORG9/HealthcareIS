using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Repository;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Controller;
using System.Diagnostics;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for HospitalMap.xaml
    /// </summary>
    public partial class HospitalMap : Window
    {       
        public static List<MapObject> allOuterMapObjects = new List<MapObject>();

        public HospitalMap(List<MapObject> allMapObjects)
        {                     
            InitializeComponent();
            CanvasService.addObjectToCanvas(findOutterMapObjects(allMapObjects), canvas);
        }

        private List<MapObject> findOutterMapObjects(List<MapObject> allMapObjects)
        {
            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.Description.Equals(""))
                {
                    allOuterMapObjects.Add(mapObject);
                }
            }
            return allOuterMapObjects;
        }
        private void selectBuilding(object sender, MouseButtonEventArgs e)
        {           
            MapObject chosenBuilding = CanvasService.checkWhichObjectIsClicked(e, allOuterMapObjects, canvas);
            if (chosenBuilding != null && chosenBuilding.MapObjectType == MapObjectType.Building)
            {
                goToClickedBuilding(chosenBuilding);
            }
        }
       
        private void goToClickedBuilding(MapObject mapObject)
        {
            MapObjectController mapObjectController = new MapObjectController(new MapObjectService(new MapObjectRepository(new FileRepository(AllConstants.MAPOBJECT_PATH))));
            List<MapObject> buildingObjects = new List<MapObject>();

            foreach (MapObject mapObjectIteration in mapObjectController.getAllMapObjects())
            {
                if (mapObject.Id.ToString().Equals(findBuilding(mapObjectIteration)))
                {
                    buildingObjects.Add(mapObjectIteration);
                }                
            }
            canvas.Children.Clear();
            Building window1 = new Building(buildingObjects, 0);
            this.Close();
            window1.ShowDialog();
        }

        private String findBuilding(MapObject mapObjectIteration)
        {
            String[] firstSplit = mapObjectIteration.Description.Split("&");
            String[] buildingIndex = firstSplit[0].Split("-");
            return buildingIndex[0];
        }
    }
}
