using Shouldly;
using WPFHospitalEditor.Exceptions;
using WPFHospitalEditor.MapObjectModel;
using Xunit;

namespace WPFHospitalEditorUnitTests
{
    public class MapObjectDimensionsTest
    {
        [StaFact]
        public void Create_valid_object()
        {
            double height = 50;
            double width = 40;
            MapObjectDimensions mapObjectDimensions = null;
            try
            {
                mapObjectDimensions = new MapObjectDimensions(height, width);
            }
            catch { }
            mapObjectDimensions.ShouldNotBeNull();
        }

        [StaFact]
        public void Create_invalid_object()
        {
            double height = 50;
            double width = -40;
            Assert.Throws<ValidationException>(() => { new MapObjectDimensions(height, width); });
        }
    }
}
