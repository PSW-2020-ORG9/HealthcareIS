using System;
using Shouldly;
using WPFHospitalEditor.Model;
using Xunit;

namespace WPFHospitalEditorUnitTests.ValueObjectTests
{
    public class TimeIntervalTest
    {
        [StaFact]
        public void Create_valid_object()
        {
            DateTime start = new DateTime(2022, 5, 18, 9, 0, 0);
            DateTime end = new DateTime(2022, 5, 18, 10, 0, 0);
            TimeInterval timeInterval = null;
            try
            {
                timeInterval = new TimeInterval(start, end);
            } catch
            {

            }
            timeInterval.ShouldNotBeNull();
        }

        [StaFact]
        public void Create_invalid_object()
        {
            DateTime start = new DateTime(2022, 5, 18, 9, 0, 0);
            DateTime end = new DateTime(2022, 5, 18, 9, 0, 0);
            TimeInterval timeInterval = null;
            try
            {
                timeInterval = new TimeInterval(start, end);
            }
            catch
            {

            }
            timeInterval.ShouldBeNull();
        }
    }
}
