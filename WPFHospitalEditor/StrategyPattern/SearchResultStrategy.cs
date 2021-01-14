using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.StrategyPattern
{
    interface ISearchResultStrategy
    {
        Dictionary<int, string> GetContentRows();
    }

    class SearchResultStrategy : ISearchResultStrategy
    {
        private ISearchResultStrategy strategy;

        public SearchResultStrategy() { }
        public SearchResultStrategy(ISearchResultStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Dictionary<int, string> GetContentRows()
        {
            return strategy.GetContentRows();
        }
    }

    class MapObjectSearchContentRows : ISearchResultStrategy
    {
        private List<MapObject> searchResults;
        public MapObjectSearchContentRows(List<MapObject> searchResults)
        {
            this.searchResults = searchResults;
        }
        public Dictionary<int, string> GetContentRows()
        {
            IMapObjectController mapObjectController = new MapObjectController();
            Dictionary<int, string> retVal = new Dictionary<int, string>();
            
            for (int i = 0; i < searchResults.Count(); i++)
            {
                MapObject mo = searchResults.ElementAt(i);
                retVal[mo.Id] = MapObjectToRow(mo);
            }
            return retVal;
        }

        public static string MapObjectToRow(MapObject mo)
        {
            string result = mo.Name + AllConstants.ContentSeparator
                            + mo.MapObjectDescription.BuildingId
                            + AllConstants.ContentSeparator + mo.MapObjectDescription.FloorNumber;
            return result;
        }
    }

    class MedicationSearchContentRows : ISearchResultStrategy
    {
        private string name;

        public MedicationSearchContentRows(string name)
        {
            this.name = name;
        }
        public Dictionary<int, string> GetContentRows()
        {
            IMedicationServerController medicationServerController = new MedicationServerController();
            IMapObjectController mapObjectController = new MapObjectController();
            List<MedicationDto> searchResults = medicationServerController.GetAllMedicationByName(name).ToList();
            Dictionary<int, string> retVal = new Dictionary<int, string>();

            MapObject mo = mapObjectController.GetMapObjectById(AllConstants.StorageRoomId);
            for (int i = 0; i < searchResults.Count(); i++)
            {
                MedicationDto medicationDto = searchResults.ElementAt(i);
                retVal[mo.Id] = medicationDto.Quantity + AllConstants.ContentSeparator + MapObjectSearchContentRows.MapObjectToRow(mo);
            }
            return retVal;
        }
    }

    class EquipmentSearchContentRows : ISearchResultStrategy
    {
        private string type;

        public EquipmentSearchContentRows(string type)
        {
            this.type = type;
        }
        public Dictionary<int, string> GetContentRows()
        {
            IEquipmentServerController equipmentServerController = new EquipmentServerController();
            IMapObjectController mapObjectController = new MapObjectController();
            List<EquipmentDto> searchResult = equipmentServerController.GetEquipmentByType(type).ToList();
            Dictionary<int, string> retVal = new Dictionary<int, string>();

            for (int i = 0; i < searchResult.Count(); i++)
            {
                EquipmentDto equipmentDto = searchResult.ElementAt(i);
                MapObject mo = mapObjectController.GetMapObjectById(equipmentDto.RoomId);
                retVal[mo.Id] = equipmentDto.Quantity + AllConstants.ContentSeparator + MapObjectSearchContentRows.MapObjectToRow(mo);
            }
            return retVal;
        }
    }
}
