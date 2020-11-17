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
        public Rectangle rectangle = new Rectangle();

        public TextBlock name = new TextBlock();
        public String Description { get; set; }
        public MapObjectMetrics MapObjectMetrics { get; set; }
        public MapObjectType MapObjectType { get; set; }
        public MapObjectDoor MapObjectDoor { get; set; }

        public MapObject(MapObjectMetrics MapObjectMetrics, MapObjectType MapObjectType, String Name, MapObjectDoor MapObjectDoor, String Description)
        {
            this.MapObjectMetrics = MapObjectMetrics;
            this.MapObjectType = MapObjectType;
            this.name.Text = Name;
            this.MapObjectDoor = MapObjectDoor;
            this.Description = Description;
            setMapObjectProperties(MapObjectMetrics.MapObjectDimensions);
        }

        public void setMapObjectProperties(MapObjectDimensions MapObjectDimensions)
        {
            this.rectangle.Width = MapObjectDimensions.Width;
            this.rectangle.Height = MapObjectDimensions.Height;
            setTextBlockProperties();
            setRectanglePositionOnMap(MapObjectMetrics.MapObjectCoordinates);
            setTextBlockPositionOnMap(MapObjectMetrics.MapObjectCoordinates);
            setMapObjectColor();
            setMapObjectDoor(MapObjectDoor.MapObjectDoorOrientation);
        }
        public void setTextBlockProperties()
        {
            this.name.FontSize = 20;
            this.name.HorizontalAlignment = HorizontalAlignment.Center;
            this.name.SetValue(Canvas.WidthProperty, this.rectangle.Width);
            this.name.SetValue(Canvas.HeightProperty, this.rectangle.Height);
            this.name.TextWrapping = TextWrapping.Wrap;
            this.name.TextAlignment = TextAlignment.Center;
        }
        public void setRectanglePositionOnMap(MapObjectCoordinates mapObjectCoordinates)
        {
            this.rectangle.SetValue(Canvas.LeftProperty, mapObjectCoordinates.X);
            this.rectangle.SetValue(Canvas.TopProperty, mapObjectCoordinates.Y);
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
        public void setMapObjectDoor(MapObjectDoorOrientation mapObjectDoorOrientation)
        {
            MapObjectCoordinates mocDoor = new MapObjectCoordinates(0, 0);
            switch (mapObjectDoorOrientation)
            {
                case MapObjectDoorOrientation.Up:
                    this.MapObjectDoor.rectangle.SetValue(Canvas.LeftProperty, MapObjectMetrics.MapObjectCoordinates.X + MapObjectMetrics.MapObjectDimensions.Width / 2 - AllConstants.doorWidth / 2);
                    this.MapObjectDoor.rectangle.SetValue(Canvas.TopProperty, MapObjectMetrics.MapObjectCoordinates.Y - AllConstants.doorHeight / 2);
                    break;
                case MapObjectDoorOrientation.Down:
                    this.MapObjectDoor.rectangle.SetValue(Canvas.LeftProperty, MapObjectMetrics.MapObjectCoordinates.X + MapObjectMetrics.MapObjectDimensions.Width / 2 - AllConstants.doorWidth / 2);
                    this.MapObjectDoor.rectangle.SetValue(Canvas.TopProperty, MapObjectMetrics.MapObjectCoordinates.Y + MapObjectMetrics.MapObjectDimensions.Height - AllConstants.doorHeight / 2);
                    break;
                case MapObjectDoorOrientation.Left:
                    this.MapObjectDoor.rectangle.SetValue(Canvas.LeftProperty, MapObjectMetrics.MapObjectCoordinates.X - AllConstants.doorHeight / 2);
                    this.MapObjectDoor.rectangle.SetValue(Canvas.TopProperty, MapObjectMetrics.MapObjectCoordinates.Y + MapObjectMetrics.MapObjectDimensions.Height / 2 - AllConstants.doorWidth / 2);
                    break;
                case MapObjectDoorOrientation.Right:
                    this.MapObjectDoor.rectangle.SetValue(Canvas.LeftProperty, MapObjectMetrics.MapObjectCoordinates.X + MapObjectMetrics.MapObjectDimensions.Width - AllConstants.doorHeight / 2);
                    this.MapObjectDoor.rectangle.SetValue(Canvas.TopProperty, MapObjectMetrics.MapObjectCoordinates.Y + MapObjectMetrics.MapObjectDimensions.Height / 2 - AllConstants.doorWidth / 2);
                    break;
                case MapObjectDoorOrientation.NoDoors:
                    this.MapObjectDoor.rectangle.Visibility = Visibility.Hidden;
                    break;
            }
        }
    }
}
    }
}
