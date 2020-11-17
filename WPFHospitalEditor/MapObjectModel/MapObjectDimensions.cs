namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObjectDimensions
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public MapObjectDimensions(double Width, double Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
    }
}