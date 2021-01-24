using Shouldly;
using WPFHospitalEditor.Exceptions;
using WPFHospitalEditor.MapObjectModel;
using Xunit;

namespace WPFHospitalEditorUnitTests
{
    public class MapObjectCoordinatesTest
    {
        [StaFact]
        public void Create_valid_object()
        {
            double x = 5;
            double y = 4;
            MapObjectCoordinates mapObjectCoordinates = null;
            try
            {
                mapObjectCoordinates = new MapObjectCoordinates(x, y);
            }
            catch { }
            mapObjectCoordinates.ShouldNotBeNull();
        }

        [StaFact]
        public void Create_invalid_object()
        {
            double x = -5;
            double y = 4;
            Assert.Throws<ValidationException>(() => { new MapObjectCoordinates(x, y); });
        }
    }
}
