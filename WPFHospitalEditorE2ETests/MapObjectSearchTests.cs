using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.MapObjectModel;
using Xunit;

namespace WPFHospitalEditorE2ETests
{
    public class MapObjectSearchTests
    {
        [StaFact]
        public void Search_by_empty_text_box_and_empty_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            var searchedMapObjects = new List<MapObject>();

            searchedMapObjects = mapObjectController.SearchForMapObjects("", AllConstants.EmptyComboBox);

            Assert.Empty(searchedMapObjects);
        }

        [StaFact]
        public void Search_by_empty_text_box_and_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            var searchedMapObjects = new List<MapObject>();

            searchedMapObjects = mapObjectController.SearchForMapObjects("", "Informations");

            Assert.NotEmpty(searchedMapObjects);
        }

        [StaFact]
        public void Search_by_filled_text_box_and_empty_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            var searchedMapObjects = new List<MapObject>();

            searchedMapObjects = mapObjectController.SearchForMapObjects("Info", AllConstants.EmptyComboBox);

            Assert.NotEmpty(searchedMapObjects);
        }

        [StaFact]
        public void Search_by_filled_text_box_and_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            var searchedMapObjects = new List<MapObject>();

            searchedMapObjects = mapObjectController.SearchForMapObjects("Informations 1", "Informations");

            Assert.NotEmpty(searchedMapObjects);
        }
    }
}
