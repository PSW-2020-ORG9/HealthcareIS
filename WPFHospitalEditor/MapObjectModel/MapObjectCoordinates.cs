using WPFHospitalEditor.Exceptions;

namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObjectCoordinates
    {
        public double X { get; }
        public double Y { get; }

        public MapObjectCoordinates(double x, double y)
        {
            Validate(x, y);
            X = x;
            Y = y;
        }

        public void Validate(double x, double y)
        {
            if (x < 0 || y < 0)
                throw new ValidationException("X and Y must be greater then zero");
        }
    }
}