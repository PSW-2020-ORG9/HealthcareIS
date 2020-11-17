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
            setMapObjectProperties(MapObjectMetrics, MapObjectType);
        }

        public void setMapObjectProperties(MapObjectMetrics MapObjectMetrics, MapObjectType MapObjectType)
        {
            this.rectangle = new Rectangle();
            this.rectangle.Width = MapObjectMetrics.MapObjectDimensions.Width;
            this.rectangle.Height = MapObjectMetrics.MapObjectDimensions.Height;
            setRectanglePositionOnMap(MapObjectMetrics.MapObjectCoordinates);
            setTextBlockProperties();
            setTextBlockPositionOnMap(MapObjectMetrics.MapObjectCoordinates);
            setMapObjectColor();
        }

        public void setRectanglePositionOnMap(MapObjectCoordinates mapObjectCoordinates)
        {
            this.rectangle.SetValue(Canvas.LeftProperty, mapObjectCoordinates.X);
            this.rectangle.SetValue(Canvas.TopProperty, mapObjectCoordinates.Y);
        }

        public void setTextBlockProperties()
        {
            this.name = new TextBlock();
            this.name.FontSize = 20;
            this.name.HorizontalAlignment = HorizontalAlignment.Center;
            this.name.SetValue(Canvas.WidthProperty, this.rectangle.Width);
            this.name.SetValue(Canvas.HeightProperty, this.rectangle.Height);
            this.name.TextWrapping = TextWrapping.Wrap;
            this.name.TextAlignment = TextAlignment.Center;
        }

        public void setTextBlockPositionOnMap(MapObjectCoordinates mapObjectCoordinates)
        {
            this.name.SetValue(Canvas.LeftProperty, mapObjectCoordinates.X);
            this.name.SetValue(Canvas.TopProperty, mapObjectCoordinates.Y);
        }

        public void setMapObjectColor()
        {
            MapObjectColors mapObjectColors = MapObjectColors.getInstance();
            this.rectangle.Fill = MapObjectColors.mapObjectTypesColors[MapObjectType];
            if (MapObjectType != MapObjectType.Parking && MapObjectType != MapObjectType.Road && MapObjectType != MapObjectType.WaitingRoom && MapObjectType != MapObjectType.ParkingSlot)
            {
                this.rectangle.Stroke = Brushes.Black;
            }
        }
    }
}
    

