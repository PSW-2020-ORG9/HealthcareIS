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

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for Building.xaml
    /// </summary>
    public partial class Building : Window
    {
        AllMapObjects allMapObjects = new AllMapObjects();
        Dictionary<int, Floor> buildingFloors = new Dictionary<int, Floor>();
        HospitalMap hospitalMap = new HospitalMap();

        public Building(int id)
        {
            InitializeComponent();
            clearAll();
            if(id == 1)
            {
                buildingFloors.Add(0, new Floor(AllMapObjects.allFirstBuildingFirstFloorObjects));
                buildingFloors.Add(1, new Floor(AllMapObjects.allFirstBuildingSecondFloorObjects));
            } else
            {
                buildingFloors.Add(0, new Floor(AllMapObjects.allSecondBuildingFirstFloorObjects));
                buildingFloors.Add(1, new Floor(AllMapObjects.allSecondBuildingSecondFloorObjects));
            }

            hospitalMap.canvas = this.canvas;
            setFloorComboBox();
            hospitalMap.addObjectToCanvas(buildingFloors[0].getAllFloorMapObjects(), canvas);
            displayLegend(buildingFloors[0].getAllFloorMapObjects());
            floor.SelectedIndex = 0;
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
            hospitalMap.ShowDialog();
        }

        private void floor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearAll();
            int index = floor.SelectedIndex;
            if (buildingFloors.Count == 0) return;
            hospitalMap.addObjectToCanvas(buildingFloors[index].getAllFloorMapObjects(), canvas);
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
            MapObject chosenMapObject = hospitalMap.checkWhichObjectIsClicked(e, buildingFloors[index].getAllFloorMapObjects());
            if (chosenMapObject != null)
            {
                openAdditionalInformationDialog(chosenMapObject);
            }
        }
        
        private void openAdditionalInformationDialog(MapObject mapObject)
        {
            AdditionalInformation additionalInformation = new AdditionalInformation(mapObject);
            additionalInformation.ShowDialog();
        }
    }
}
