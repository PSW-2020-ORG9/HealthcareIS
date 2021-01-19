using Shouldly;
using WPFHospitalEditor.MapObjectModel;
using Xunit;

namespace WPFHospitalEditorUnitTests.ValueObjectTests
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
            catch
            {

            }
            mapObjectCoordinates.ShouldNotBeNull();
        }

        [StaFact]
        public void Create_invalid_object()
        {
            double x = -5;
            double y = 4;
            MapObjectCoordinates mapObjectCoordinates = null;
            try
            {
                mapObjectCoordinates = new MapObjectCoordinates(x, y);
            }
            catch
            {

            }
            mapObjectCoordinates.ShouldBeNull();
        }
    }
}
