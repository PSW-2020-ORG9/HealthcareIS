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
    /// Interaction logic for HospitalMap.xaml
    /// </summary>
    public partial class HospitalMap : Window
    {
        AllMapObjects allMapObjects = new AllMapObjects();
        public HospitalMap()
        {
            InitializeComponent();
            addObjectToCanvas(AllMapObjects.allOuterMapObjects, canvas);
        }
        private void selectBuilding(object sender, MouseButtonEventArgs e)
        {
            MapObject chosenBuilding = checkWhichObjectIsClicked(e, AllMapObjects.allOuterMapObjects);
            if (chosenBuilding != null)
            {
                goToClickedBuilding(chosenBuilding);
            }
        }
        private MapObject checkWhichObjectIsClicked(MouseButtonEventArgs e, List<MapObject> allMapObjectsShowed)
        {
            for (int i = 0; i < allMapObjectsShowed.Count; i++)
            {
                if (checkIfPointIsInRectangle(e, allMapObjectsShowed[i]))
                {
                    return allMapObjectsShowed[i];
                }
            }
            return null;
        }
        private Boolean checkIfPointIsInRectangle(MouseButtonEventArgs e, MapObject mapObject)
        {
            return (e.GetPosition(canvas).X > mapObject.MapObjectMetrics.MapObjectCoordinates.X
                    && e.GetPosition(canvas).X < mapObject.MapObjectMetrics.MapObjectCoordinates.X + mapObject.MapObjectMetrics.MapObjectDimensions.Width
                    && e.GetPosition(canvas).Y > mapObject.MapObjectMetrics.MapObjectCoordinates.Y
                    && e.GetPosition(canvas).Y < mapObject.MapObjectMetrics.MapObjectCoordinates.Y + mapObject.MapObjectMetrics.MapObjectDimensions.Height);
        }
        private void goToClickedBuilding(MapObject mapObject)
        {
                canvas.Children.Clear();
                Building window1 = new Building(mapObject.Id);
                this.Close();
                window1.ShowDialog();
        }
        public void addObjectToCanvas(List<MapObject> objectsToShow, Canvas canvas)
        {
            for (int i = 0; i < objectsToShow.Count; i++)
            {
                canvas.Children.Add(objectsToShow[i].rectangle);
                canvas.Children.Add(objectsToShow[i].name);
                canvas.Children.Add(objectsToShow[i].MapObjectDoor.rectangle);
            }
        }
    }
}
