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
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Repository;
using Newtonsoft.Json;
using System.Diagnostics;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for Building.xaml
    /// </summary>
    public partial class Building : Window
    {
        private Dictionary<int, Floor> buildingFloors = new Dictionary<int, Floor>();
        private List<MapObject> allBuildingObjects = new List<MapObject>();

        MapObjectController mapObjectController = new MapObjectController(new MapObjectService(new MapObjectRepository(new FileRepository(AllConstants.MAPOBJECT_PATH))));

        public Building(List<MapObject> buildingObjects, int selectedFloor)
        {
            allBuildingObjects = buildingObjects;
            InitializeComponent();
            clearAll();          
            populateBuildingFloors(buildingObjects);            
            setFloorComboBox();
            floor.SelectedIndex = selectedFloor;            
        }

        private void populateBuildingFloors(List<MapObject> allBuildingObjects)
        {          
            int numberOfFloors = findMaxFloor(findAllFloors(allBuildingObjects));
            for(int i = 0; i <= numberOfFloors; i++)
            {
                buildingFloors.Add(i, new Floor(findChosenFloor(allBuildingObjects,i)));
            }
        }

        private List<int> findAllFloors(List<MapObject> allBuildingObjects)
        {
            List<int> floors = new List<int>();
            foreach (MapObject mapObjectIteration in allBuildingObjects)
            {
                floors.Add(int.Parse(findFloor(mapObjectIteration)));
            }
            return floors;
        }

        private int findMaxFloor(List<int> floors)
        {
            int maxFloor = 0;
            for(int i = 0; i < floors.Count; i++)
            {
                if (floors[i] > maxFloor)
                {
                    maxFloor = floors[i];
                }
            }
            return maxFloor;
        }

        private List<MapObject> findChosenFloor(List<MapObject> buildingObjects, int floor)
        {
            List<MapObject> floorObjects = new List<MapObject>();
            foreach (MapObject mapObjectIteration in buildingObjects)
            {
                if (floor.ToString().Equals(findFloor(mapObjectIteration)))
                {
                    floorObjects.Add(mapObjectIteration);
                }
            }
            return floorObjects;
        }

        private String findFloor(MapObject mapObjectIteration)
        {
            String[] firstSplit = mapObjectIteration.Description.Split("&");
            String[] floor = firstSplit[0].Split("-");
            return floor[1];
        }
        
        private void setFloorComboBox()
        {
            for (int i = 0; i < buildingFloors.Count; i++)
            {
                floor.Items.Add((i + 1) + ". floor");
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            clearAll();           
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects());
            this.Close();
            hospitalMap.Show();
        }

        public void floor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearAll();
            int index = floor.SelectedIndex;
            if (buildingFloors.Count == 0) return;
            CanvasService.addObjectToCanvas(buildingFloors[index].getAllFloorMapObjects(), canvas);
            displayLegend(buildingFloors[index].getAllFloorMapObjects());
        }

        private void clearAll()
        {
            if (legend == null) return;
            canvas.Children.Clear();
            legend.Children.Clear();
        }

        private void displayLegend(List<MapObject> displayedMapObjects)
        {
            if (legend == null) return;
            HashSet<MapObjectType> mapObjectTypes = findAllMapObjectTypesOnFloor(displayedMapObjects);
            addingRowsToGrid(mapObjectTypes);
            
            int index = 0;
            foreach (MapObjectType mapObjectType in mapObjectTypes) {
                organiseLegend(mapObjectType, index);
                index++;
            }
        }

        private HashSet<MapObjectType> findAllMapObjectTypesOnFloor(List<MapObject> displayedMapObjects)
        {
            HashSet<MapObjectType> mapObjectTypes = new HashSet<MapObjectType>();
            for (int i = 0; i < displayedMapObjects.Count; i++)
            {
                mapObjectTypes.Add(displayedMapObjects[i].MapObjectType);
            }
            return mapObjectTypes;
        }

        private void addingRowsToGrid(HashSet<MapObjectType> mapObjectTypes)
        {
            int numberOfRows = (int)(mapObjectTypes.Count / legend.ColumnDefinitions.Count) + 1;
            for (int i = 0; i < numberOfRows; i++)
            {
                legend.RowDefinitions.Add(new RowDefinition() { });
            }
        }
  
        private void organiseLegend(MapObjectType mapObjectType, int index)
        {
            Rectangle rectangle = createRectangleInLegend(mapObjectType);
            TextBlock textblock = createTextBlockInLegend(mapObjectType);
            settingPosition(index, rectangle, textblock);
            addToLegend(rectangle, textblock);
        }

        private Rectangle createRectangleInLegend(MapObjectType mapObjectType)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = MapObjectColors.getInstance().getColor(mapObjectType);
            rectangle.Width = 25;
            rectangle.Height = 25;
            return rectangle;
        }

        private TextBlock createTextBlockInLegend(MapObjectType mapObjectType)
        {
            TextBlock textblock = new TextBlock();
            textblock.Text = mapObjectType.ToString() + "-  ";
            return textblock;
        }

        private void settingPosition(int index, Rectangle rectangle, TextBlock textblock)
        {
            int row = (int)(index / legend.ColumnDefinitions.Count);
            int column = index - row * legend.ColumnDefinitions.Count;
            rectangle.SetValue(Grid.ColumnProperty, column);
            rectangle.SetValue(Grid.RowProperty, row);
            textblock.SetValue(Grid.ColumnProperty, column);
            textblock.SetValue(Grid.RowProperty, row);
        }

        private void addToLegend(Rectangle rectangle, TextBlock textblock)
        {
            legend.Children.Add(rectangle);
            legend.Children.Add(textblock);
        }

        private void selectMapObject(object sender, MouseButtonEventArgs e)
        {
            int index = floor.SelectedIndex;
            MapObject chosenMapObject = CanvasService.checkWhichObjectIsClicked(e, buildingFloors[index].getAllFloorMapObjects(), this.canvas);
            if (chosenMapObject != null)
            {
                openAdditionalInformationDialog(chosenMapObject);
            }
        }
        
        private void openAdditionalInformationDialog(MapObject mapObject)
        {
            int index = floor.SelectedIndex;
            AdditionalInformation additionalInformation = new AdditionalInformation(mapObject, this, index);
            additionalInformation.Owner = this;
            additionalInformation.ShowDialog(); 
        }

        public Floor getFloor(int index)
        {
            return buildingFloors[index];
        }

        public List<MapObject> getAllBuildingObjects()
        {
            return allBuildingObjects;
        }

        public static String getBuildingId(MapObject mapObject)
        {
            String[] firstSplit = mapObject.Description.Split("&");
            String[] secondSplit = firstSplit[0].Split("-");
            return secondSplit[0];
        }
    }
}
