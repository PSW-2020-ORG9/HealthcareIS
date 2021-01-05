using System;
using Newtonsoft.Json;
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
        [JsonIgnore]
        public TextBlock nameOnMap;
        public String Name { get; set; }
        public MapObjectDescription MapObjectDescription { get; set; }
        public MapObjectMetrics MapObjectMetrics { get; set; }
        public MapObjectType MapObjectType { get; set; }
        public MapObjectDoor MapObjectDoor { get; set; }

        public MapObject(String name, int Id, MapObjectMetrics MapObjectMetrics, MapObjectType MapObjectType, MapObjectDoor MapObjectDoor, MapObjectDescription MapObjectDescription)
        {
            this.Name = name;
            this.Id = Id;
            this.MapObjectMetrics = MapObjectMetrics;
            this.MapObjectType = MapObjectType;
            this.MapObjectDoor = MapObjectDoor;
            this.MapObjectDescription = MapObjectDescription;
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
            this.nameOnMap = new TextBlock();
            setMapObjectNameOnMap();
            this.nameOnMap.FontSize = 15;
            this.nameOnMap.HorizontalAlignment = HorizontalAlignment.Center;
            this.nameOnMap.SetValue(Canvas.WidthProperty, this.rectangle.Width);
            this.nameOnMap.SetValue(Canvas.HeightProperty, this.rectangle.Height);
            this.nameOnMap.TextWrapping = TextWrapping.Wrap;
            this.nameOnMap.TextAlignment = TextAlignment.Center;
        }
        public void setMapObjectNameOnMap()
        {
            if(isNameNeeded())
            {
                this.nameOnMap.Text = Name;
            }
        }

        public void setTextBlockPositionOnMap(MapObjectCoordinates mapObjectCoordinates)
        {
            this.nameOnMap.SetValue(Canvas.LeftProperty, mapObjectCoordinates.X);
            this.nameOnMap.SetValue(Canvas.TopProperty, mapObjectCoordinates.Y);
        }

        public void setMapObjectColor()
        {
            this.rectangle.Fill = MapObjectColors.getInstance().getColor(MapObjectType);
            if (isStrokeNeeded())
            {
                this.rectangle.Stroke = Brushes.Black;
            }
        }

        public bool ContainsEquipment()
        {
            return MapObjectType.Equals(MapObjectType.Canteen) || MapObjectType.Equals(MapObjectType.SurgeryRoom) || MapObjectType.Equals(MapObjectType.DentistryRoom)
                || MapObjectType.Equals(MapObjectType.RecoveryRoom) || MapObjectType.Equals(MapObjectType.NeurologyRoom) || MapObjectType.Equals(MapObjectType.ExaminationRoom)
                || MapObjectType.Equals(MapObjectType.DermatologyRoom) || MapObjectType.Equals(MapObjectType.OphthalmologyRoom) || MapObjectType.Equals(MapObjectType.OnDuty) || MapObjectType.Equals(MapObjectType.StorageRoom);
        }

        public bool ContainsMedication()
        {
            return MapObjectType == MapObjectType.StorageRoom;
        }

        public void setMapObjectDoorPosition(MapObjectDoorOrientation mapObjectDoorOrientation)
        {
            switch (mapObjectDoorOrientation)
            {
                case MapObjectDoorOrientation.Up:
                    this.MapObjectDoor.rectangle.SetValue(Canvas.LeftProperty, MapObjectMetrics.MapObjectCoordinates.X + MapObjectMetrics.MapObjectDimensions.Width / 2 - AllConstants.DoorWidth / 2);
                    this.MapObjectDoor.rectangle.SetValue(Canvas.TopProperty, MapObjectMetrics.MapObjectCoordinates.Y - AllConstants.DoorHeight / 2);
                    break;
                case MapObjectDoorOrientation.Down:
                    this.MapObjectDoor.rectangle.SetValue(Canvas.LeftProperty, MapObjectMetrics.MapObjectCoordinates.X + MapObjectMetrics.MapObjectDimensions.Width / 2 - AllConstants.DoorWidth / 2);
                    this.MapObjectDoor.rectangle.SetValue(Canvas.TopProperty, MapObjectMetrics.MapObjectCoordinates.Y + MapObjectMetrics.MapObjectDimensions.Height - AllConstants.DoorHeight / 2);
                    break;
                case MapObjectDoorOrientation.Left:
                    this.MapObjectDoor.rectangle.SetValue(Canvas.LeftProperty, MapObjectMetrics.MapObjectCoordinates.X - AllConstants.DoorHeight / 2);
                    this.MapObjectDoor.rectangle.SetValue(Canvas.TopProperty, MapObjectMetrics.MapObjectCoordinates.Y + MapObjectMetrics.MapObjectDimensions.Height / 2 - AllConstants.DoorWidth / 2);
                    break;
                case MapObjectDoorOrientation.Right:
                    this.MapObjectDoor.rectangle.SetValue(Canvas.LeftProperty, MapObjectMetrics.MapObjectCoordinates.X + MapObjectMetrics.MapObjectDimensions.Width - AllConstants.DoorHeight / 2);
                    this.MapObjectDoor.rectangle.SetValue(Canvas.TopProperty, MapObjectMetrics.MapObjectCoordinates.Y + MapObjectMetrics.MapObjectDimensions.Height / 2 - AllConstants.DoorWidth / 2);
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

        private Boolean isNameNeeded()
        {
            if (MapObjectType != MapObjectType.Road && MapObjectType != MapObjectType.Parking && MapObjectType != MapObjectType.ParkingSlot)
            {
                return true;
            }
            return false;
        }
    }
}   

