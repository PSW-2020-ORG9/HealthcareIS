using WPFHospitalEditor.Exceptions;

namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObjectDimensions
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public MapObjectDimensions(double Width, double Height)
        {
            if(Validate(Width, Height))
            this.Width = Width;
            this.Height = Height;
        }

        private bool Validate(double Width, double Height)
        {
            if (Height < 0 || Width < 0) throw new ValidationException("Width and must be greater then zero");
            return true;
        }
    }
}