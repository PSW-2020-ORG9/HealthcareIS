using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input.Manipulations;
using System.Windows.Media;
using System.Windows.Shapes;
using Rectangle = System.Windows.Shapes.Rectangle;
namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObject
    {
        public Rectangle rectangle;

        public TextBlock name;
        public String Description { get; set; }
        public MapObjectMetrics MapObjectMetrics { get; set; }
        public MapObjectType MapObjectType { get; set; }
        public MapObjectDoor MapObjectDoor { get; set; }

        public MapObject(MapObjectMetrics MapObjectMetrics, MapObjectType MapObjectType, MapObjectDoor MapObjectDoor, String Description)
        {
            this.MapObjectMetrics = MapObjectMetrics;
            this.MapObjectType = MapObjectType;
            this.MapObjectDoor = MapObjectDoor;
            this.Description = Description;

        }

        public void setMapObjectProperties(MapObjectDimensions MapObjectDimensions)
        {
            this.rectangle.Width = MapObjectDimensions.Width;
            this.rectangle.Height = MapObjectDimensions.Height;
            setRectanglePositionOnMap(MapObjectMetrics.MapObjectCoordinates);
        }

        public void setRectanglePositionOnMap(MapObjectCoordinates mapObjectCoordinates)
        {
            this.rectangle.SetValue(Canvas.LeftProperty, mapObjectCoordinates.X);
            this.rectangle.SetValue(Canvas.TopProperty, mapObjectCoordinates.Y);
        }
    }
}
    

