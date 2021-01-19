
using WPFHospitalEditor.Exceptions;

namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObjectCoordinates
    {
        public double X { get; }
        public double Y { get; }

        public MapObjectCoordinates(double X, double Y)
        {
            if(Validate(X, Y))
            {
                this.X = X;
                this.Y = Y;
            }
        }

        public bool Validate(double x, double y)
        {
            if (x < 0 || y < 0) throw new ValidationException("X and Y must be greater then zero");
            return true;
        }
    }
}