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
    /// Interaction logic for Building1.xaml
    /// </summary>
    public partial class Building1 : Window
    {
        AllMapObjects allMapObjects = new AllMapObjects();

        public Building1()
        {
            InitializeComponent();
            clearAll();

            addObjectToCanvas(AllMapObjects.allFirstBuildingFirstFloorObjects);
            displayLegend(AllMapObjects.allFirstBuildingFirstFloorObjects);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            clearAll();
            HospitalMap window1 = new HospitalMap();
            this.Close();
            window1.ShowDialog();
        }

        private void addObjectToCanvas(List<MapObject> objectsToShow)
        {
            for (int i = 0; i < objectsToShow.Count; i++)
            {
                canvas.Children.Add(objectsToShow[i].rectangle);
                canvas.Children.Add(objectsToShow[i].name);
                canvas.Children.Add(objectsToShow[i].MapObjectDoor.rectangle);
            }
        }

        private void floor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearAll();
            if (floor.SelectedIndex == 0)
            {
                addObjectToCanvas(AllMapObjects.allFirstBuildingFirstFloorObjects);
                displayLegend(AllMapObjects.allFirstBuildingFirstFloorObjects);
            }
            else if (floor.SelectedIndex == 1)
            {
                addObjectToCanvas(AllMapObjects.allFirstBuildingSecondFloorObjects);
                displayLegend(AllMapObjects.allFirstBuildingSecondFloorObjects);
            }
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
            HashSet<MapObjectType> mapObjectTypes = new HashSet<MapObjectType>();
            for (int i = 0; i < displayedMapObjects.Count; i++)
            {
                mapObjectTypes.Add(displayedMapObjects[i].MapObjectType);
            }

            int numberOfRows = (int)(mapObjectTypes.Count / legend.ColumnDefinitions.Count) + 1;

            for (int i = 0; i < numberOfRows; i++) {
                legend.RowDefinitions.Add(new RowDefinition() {  });
            }

            int index = 0;

            foreach (MapObjectType mapObjectType in mapObjectTypes) {
                organiseLegend(mapObjectType, index);
                index++;
            }

        }

        private void organiseLegend(MapObjectType mapObjectType, int index)
        {
            if (legend == null) return;
            Rectangle rectangle = createColorObjectInLegend(mapObjectType);
            TextBlock textblock = createTextObjectInLegend(mapObjectType);
            int row = (int)(index / legend.ColumnDefinitions.Count);
            int column = index - row * legend.ColumnDefinitions.Count;
            rectangle.SetValue(Grid.ColumnProperty, column);
            rectangle.SetValue(Grid.RowProperty, row);
            textblock.SetValue(Grid.ColumnProperty, column);
            textblock.SetValue(Grid.RowProperty, row);
            addToLegend(rectangle, textblock);
        }

        private Rectangle createColorObjectInLegend(MapObjectType mapObjectType)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = MapObjectColors.getInstance().getColor(mapObjectType);
            rectangle.Width = 25;
            rectangle.Height = 25;
            return rectangle;
        }

        private TextBlock createTextObjectInLegend(MapObjectType mapObjectType)
        {
            TextBlock textblock = new TextBlock();
            textblock.Text = "-" + mapObjectType.ToString() + "   ";
            return textblock;
        }

        private void addToLegend(Rectangle rectangle, TextBlock textblock)
        {
            if (legend == null) return;
            legend.Children.Add(rectangle);
            legend.Children.Add(textblock);
        }
    }
}
