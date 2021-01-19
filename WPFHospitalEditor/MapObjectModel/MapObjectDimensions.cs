using WPFHospitalEditor.Exceptions;

namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObjectDimensions
    {
        public double Width { get; }
        public double Height { get; }

        public MapObjectDimensions(double width, double height)
        {
            Validate(width, height);
            Width = width;
            Height = height;
        }

        private void Validate(double Width, double Height)
        {
            if (Height < 0 || Width < 0) 
                throw new ValidationException("Width and must be greater then zero");
        }
    }
}