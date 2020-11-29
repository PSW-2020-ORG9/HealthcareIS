using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for Building.xaml
    /// </summary>
    public partial class Building : Window
    {
        private Dictionary<int, Floor> buildingFloors = new Dictionary<int, Floor>();
        private List<MapObject> allBuildingObjects = new List<MapObject>();
        public List<MapObject> floorBuildingObjects = new List<MapObject>();
        MapObjectController mapObjectController = new MapObjectController();
        private Role role;
        public static Canvas canvasBuilding;

        public Building(List<MapObject> buildingObjects, int selectedFloor, Role role)
        {
            allBuildingObjects = buildingObjects;
            InitializeComponent();
            clearAll();          
            populateBuildingFloors(buildingObjects);            
            setFloorComboBox();
            floor.SelectedIndex = selectedFloor;
            floorBuildingObjects = buildingFloors[floor.SelectedIndex].getAllFloorMapObjects();
            this.role = role;
            canvasBuilding = canvas;
        }
        
        private void populateBuildingFloors(List<MapObject> allBuildingObjects)
        {
            foreach(MapObject mapObjectIterate in allBuildingObjects)
            {
                int index = int.Parse(findFloor(mapObjectIterate));
                if(!buildingFloors.ContainsKey(index))
                {
                    buildingFloors.Add(index, new Floor());
                }
                buildingFloors[index].addMapObject(mapObjectIterate);
            }
        }
              
        public static String findFloor(MapObject mapObject)
        {
            String[] descriptionParts = mapObject.Description.Split("&");
            String[] floors = descriptionParts[0].Split("-");
            return floors[1];
        }

        public static String findBuilding(MapObject mapObject)
        {
            String[] descriptionParts = mapObject.Description.Split("&");
            String[] buildings = descriptionParts[0].Split("-");
            return buildings[0];
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
            this.Close();
            CanvasService.addObjectToCanvas(mapObjectController.getOutterMapObjects(), HospitalMap.canvasHospitalMap);
            Owner.Show();
        }

        private void floor_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            AdditionalInformation additionalInformation = new AdditionalInformation(mapObject, this, index, role);
            additionalInformation.Owner = this;
            additionalInformation.ShowDialog(); 
        }                     
    }
}
