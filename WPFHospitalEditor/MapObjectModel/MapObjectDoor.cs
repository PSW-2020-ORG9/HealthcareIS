using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObjectDoor
    {
        public Rectangle rectangle = new Rectangle();
        public MapObjectDoorOrientation MapObjectDoorOrientation { get; set; }
        public MapObjectDoor(MapObjectDoorOrientation doorOrientation)
        {
            this.MapObjectDoorOrientation = doorOrientation;
            setMapObjectDoorProperties();
        }
        public void setMapObjectDoorProperties()
        {
            this.rectangle.Width = AllConstants.DoorWidth;
            this.rectangle.Height = AllConstants.DoorHeight;
            if (MapObjectDoorOrientation == MapObjectDoorOrientation.Left || MapObjectDoorOrientation == MapObjectDoorOrientation.Right)
            {
                this.rectangle.Width = AllConstants.DoorHeight;
                this.rectangle.Height = AllConstants.DoorWidth;
            }
            setMapObjectDoorColor();
        }
        public void setMapObjectDoorColor()
        {
            this.rectangle.Fill = Brushes.SaddleBrown;
        }
    }
}