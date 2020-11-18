using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Rectangle = System.Windows.Shapes.Rectangle;
namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObject
    {
        public int Id { get; set; }
        public Rectangle rectangle;
        public TextBlock name;
        public String Description { get; set; }
        public MapObjectMetrics MapObjectMetrics { get; set; }
        public MapObjectType MapObjectType { get; set; }
        public MapObjectDoor MapObjectDoor { get; set; }

        public MapObject(int Id, MapObjectMetrics MapObjectMetrics, MapObjectType MapObjectType, String name, MapObjectDoor MapObjectDoor, String Description)
        {
            this.Id = Id;
            this.MapObjectMetrics = MapObjectMetrics;
            this.MapObjectType = MapObjectType;
            this.MapObjectDoor = MapObjectDoor;
            this.Description = Description;
            setMapObjectProperties(MapObjectMetrics);
        }

        public void setMapObjectProperties(MapObjectMetrics mapObjectMetrics)
        {
            this.rectangle = new Rectangle();
            this.rectangle.Width = mapObjectMetrics.MapObjectDimensions.Width;
            this.rectangle.Height = mapObjectMetrics.MapObjectDimensions.Height;
            setRectanglePositionOnMap(mapObjectMetrics.MapObjectCoordinates);
            setTextBlockProperties();
            setTextBlockPositionOnMap(mapObjectMetrics.MapObjectCoordinates);
            setMapObjectColor();
            setMapObjectDoorPosition(MapObjectDoor.MapObjectDoorOrientation);
        }

        public void setRectanglePositionOnMap(MapObjectCoordinates mapObjectCoordinates)
        {
            this.rectangle.SetValue(Canvas.LeftProperty, mapObjectCoordinates.X);
            this.rectangle.SetValue(Canvas.TopProperty, mapObjectCoordinates.Y);
        }

        public void setTextBlockProperties()
        {
            this.name = new TextBlock();
            setMapObjectNameOnMap();
            this.name.FontSize = 20;
            this.name.HorizontalAlignment = HorizontalAlignment.Center;
            this.name.SetValue(Canvas.WidthProperty, this.rectangle.Width);
            this.name.SetValue(Canvas.HeightProperty, this.rectangle.Height);
            this.name.TextWrapping = TextWrapping.Wrap;
            this.name.TextAlignment = TextAlignment.Center;
        }
        public void setMapObjectNameOnMap()
        {
            if(MapObjectType != MapObjectType.Road && MapObjectType != MapObjectType.Parking && MapObjectType != MapObjectType.ParkingSlot)
            this.name.Text = MapObjectType.ToString() + Id.ToString();
        }

        public void setTextBlockPositionOnMap(MapObjectCoordinates mapObjectCoordinates)
        {
            this.name.SetValue(Canvas.LeftProperty, mapObjectCoordinates.X);
            this.name.SetValue(Canvas.TopProperty, mapObjectCoordinates.Y);
        }

        public void setMapObjectColor()
        {
            this.rectangle.Fill = MapObjectColors.getInstance().getColor(MapObjectType);
            if (isStrokeNeeded())
            {
                this.rectangle.Stroke = Brushes.Black;
            }
        }

        public void setMapObjectDoorPosition(MapObjectDoorOrientation mapObjectDoorOrientation)
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
         
        private Boolean isStrokeNeeded()
        {
            if (MapObjectType != MapObjectType.Parking && MapObjectType != MapObjectType.Road && MapObjectType != MapObjectType.WaitingRoom && MapObjectType != MapObjectType.ParkingSlot)
            {
                return true;
            }
            return false;
        }
    }
}   

