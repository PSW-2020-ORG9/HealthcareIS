namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObjectCoordinates
    {
        public double X { get; set; }
        public double Y { get; set; }

        public MapObjectCoordinates(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}