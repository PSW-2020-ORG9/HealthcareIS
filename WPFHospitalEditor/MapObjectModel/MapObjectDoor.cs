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
            this.rectangle.Width = AllConstants.doorWidth;
            this.rectangle.Height = AllConstants.doorHeight;
            if (MapObjectDoorOrientation == MapObjectDoorOrientation.Left || MapObjectDoorOrientation == MapObjectDoorOrientation.Right)
            {
                this.rectangle.Width = AllConstants.doorHeight;
                this.rectangle.Height = AllConstants.doorWidth;
            }
            setMapObjectDoorColor();
        }
        public void setMapObjectDoorColor()
        {
            this.rectangle.Fill = Brushes.SaddleBrown;
        }
    }
}