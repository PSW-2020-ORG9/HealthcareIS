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
            canvas.Children.Clear();
            if (legend1 != null && legend2 != null && legend3 != null)
            {
                legend1.Children.Clear();
                legend2.Children.Clear();
                legend3.Children.Clear();
            }
        }

        private void displayLegend(List<MapObject> displayedMapObjects)
        {
            int itemsInOneRow = 0;
            List<MapObjectType> mapObjectTypes = new List<MapObjectType>();
            for (int i = 0; i < displayedMapObjects.Count; i++)
            {
                if (!mapObjectTypes.Contains(displayedMapObjects[i].MapObjectType))
                {
                    mapObjectTypes.Add(displayedMapObjects[i].MapObjectType);
                    itemsInOneRow = organiseLegend(displayedMapObjects[i].MapObjectType, itemsInOneRow);
                }
            }
        }

        private int organiseLegend(MapObjectType mapObjectType, int itemsInOneRow)
        {
            Rectangle rectangle = createColorObjectInLegend(mapObjectType);
            TextBlock textblock = createTextObjectInLegend(mapObjectType);
            itemsInOneRow = organiseRows(rectangle, textblock, itemsInOneRow);
            return itemsInOneRow;
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

        private int organiseRows(Rectangle rectangle, TextBlock textblock, int itemsInOneRow)
        {
            if (itemsInOneRow < 5)
            {
                addToLegend(legend1, rectangle, textblock);
            }
            else if (itemsInOneRow >= 5 && itemsInOneRow < 10)
            {
                addToLegend(legend2, rectangle, textblock);
            }
            else
            {
                addToLegend(legend3, rectangle, textblock);
            }
            return itemsInOneRow + 1;
        }

        private void addToLegend(StackPanel legend, Rectangle rectangle, TextBlock textblock)
        {
            if (legend != null)
            {
                legend.Children.Add(rectangle);
                legend.Children.Add(textblock);
            }
        }
    }
}
