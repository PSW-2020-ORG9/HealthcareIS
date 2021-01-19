using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;
using Xunit;

namespace WPFHospitalEditorUnitTests.ValueObjectTests
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
            catch
            {

            }
            mapObjectDimensions.ShouldNotBeNull();
        }

        public void Create_invalid_object()
        {
            double height = 50;
            double width = -40;
            MapObjectDimensions mapObjectDimensions = null;
            try
            {
                mapObjectDimensions = new MapObjectDimensions(height, width);
            }
            catch
            {

            }
            mapObjectDimensions.ShouldBeNull();
        }
    }
}
