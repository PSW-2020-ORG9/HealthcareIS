using System.Collections.Generic;
using System.Linq;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.StrategyPattern
{
    interface ISearchResultStrategy
    {
        List<SearchResultDTO> GetSearchResult();
    }

    class SearchResultStrategy : ISearchResultStrategy
    {
        private readonly ISearchResultStrategy strategy;

        public SearchResultStrategy(ISearchResultStrategy strategy)
        {
            this.strategy = strategy;
        }

        public List<SearchResultDTO> GetSearchResult()
        {
            return strategy.GetSearchResult();
        }
    }

    class MapObjectSearchResult : ISearchResultStrategy
    {
        private readonly List<MapObject> searchResults;

        public MapObjectSearchResult(List<MapObject> searchResults)
        {
            this.searchResults = searchResults;
        }
        public List<SearchResultDTO> GetSearchResult()
        {
            List<SearchResultDTO> retVal = new List<SearchResultDTO>();
            
            for (int i = 0; i < searchResults.Count(); i++)
            {
                MapObject mo = searchResults.ElementAt(i);
                SearchResultDTO searchResultDTO = new SearchResultDTO()
                {
                    MapObjectId = mo.Id,
                    Content = MapObjectToRow(mo)
                };
                retVal.Add(searchResultDTO);
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

    class MedicationSearchResult : ISearchResultStrategy
    {
        private readonly string name;

        public MedicationSearchResult(string name)
        {
            this.name = name;
        }
        public List<SearchResultDTO> GetSearchResult()
        {
            IMedicationServerController medicationServerController = new MedicationServerController();
            IMapObjectController mapObjectController = new MapObjectController();
            List<MedicationDto> searchResults = medicationServerController.GetAllMedicationByName(name).ToList();
            List<SearchResultDTO> retVal = new List<SearchResultDTO>();

            MapObject mo = mapObjectController.GetMapObjectById(AllConstants.StorageRoomId);
            for (int i = 0; i < searchResults.Count(); i++)
            {
                MedicationDto medicationDto = searchResults.ElementAt(i);
                SearchResultDTO searchResultDTO = new SearchResultDTO()
                {
                    MapObjectId = mo.Id,
                    Content = medicationDto.Quantity + AllConstants.ContentSeparator + MapObjectSearchResult.MapObjectToRow(mo)
                };
                retVal.Add(searchResultDTO);
            }
            return retVal;
        }
    }

    class EquipmentSearchResult : ISearchResultStrategy
    {
        private readonly string type;

        public EquipmentSearchResult(string type)
        {
            this.type = type;
        }
        public List<SearchResultDTO> GetSearchResult()
        {
            IEquipmentServerController equipmentServerController = new EquipmentServerController();
            IMapObjectController mapObjectController = new MapObjectController();
            List<EquipmentDto> searchResult = equipmentServerController.GetEquipmentByType(type).ToList();
            List<SearchResultDTO> retVal = new List<SearchResultDTO>();

            for (int i = 0; i < searchResult.Count(); i++)
            {
                EquipmentDto equipmentDto = searchResult.ElementAt(i);
                MapObject mo = mapObjectController.GetMapObjectById(equipmentDto.RoomId);
                SearchResultDTO searchResultDTO = new SearchResultDTO()
                {
                    MapObjectId = mo.Id,
                    Content = equipmentDto.Quantity + AllConstants.ContentSeparator + MapObjectSearchResult.MapObjectToRow(mo)
                };
                retVal.Add(searchResultDTO);
            }
            return retVal;
        }
    }

    class AppointmentSearchResult : ISearchResultStrategy
    {
        private readonly List<RecommendationDto> searchResult;

        public AppointmentSearchResult(List<RecommendationDto> searchResult)
        {
            this.searchResult = searchResult;
        }
        public List<SearchResultDTO> GetSearchResult()
        {
            IMapObjectController mapObjectController = new MapObjectController();
            List<SearchResultDTO> retVal = new List<SearchResultDTO>();

            for (int i = 0; i < searchResult.Count(); i++)
            {
                RecommendationDto recommendationDto = searchResult.ElementAt(i);
                MapObject mo = mapObjectController.GetMapObjectById(recommendationDto.RoomId);
                string doctor = recommendationDto.Doctor.Person.Name + " " + recommendationDto.Doctor.Person.Surname;
                string timeInterval = recommendationDto.TimeInterval.Start.ToString() + "-" + recommendationDto.TimeInterval.End.ToString();
                AppointmentSearchResultDTO searchResultDTO = new AppointmentSearchResultDTO()
                {
                    MapObjectId = mo.Id,
                    Content = mo.Name + AllConstants.ContentSeparator + doctor + AllConstants.ContentSeparator + timeInterval,
                    RecommendationDto = recommendationDto
                };
                retVal.Add(searchResultDTO);
            }
            return retVal;
        }
    }

    class EquipmentRelocationSearchResult : ISearchResultStrategy
    {
        private readonly List<EquipmentRelocationDto> searchResult;
        private string equipment;

        public EquipmentRelocationSearchResult(List<EquipmentRelocationDto> searchResult, string equipment)
        {
            this.searchResult = searchResult;
            this.equipment = equipment;
        }

        public List<SearchResultDTO> GetSearchResult()
        {
            IMapObjectController mapObjectController = new MapObjectController();
            List<SearchResultDTO> retVal = new List<SearchResultDTO>();
            IEquipmentServerController equipmentServerController = new EquipmentServerController();

            for (int i = 0; i < searchResult.Count(); i++)
            {
                EquipmentRelocationDto equipmentRelocationDto = searchResult.ElementAt(i);
                MapObject mo = mapObjectController.GetMapObjectById(equipmentRelocationDto.SourceRoomId);
                var equipments = equipmentServerController.GetEquipmentByRoomId(mo.Id);
                string amount = "";
                foreach (EquipmentDto eq in equipments)
                {
                    if (eq.Name.Equals(equipment))
                    {
                        amount = eq.Quantity.ToString();
                    }
                }
                string timeInterval = equipmentRelocationDto.TimeInterval.Start.ToString() + "-" + equipmentRelocationDto.TimeInterval.End.ToString();
                EquipmentRelocationSearchResultDTO searchResultDTO = new EquipmentRelocationSearchResultDTO()
                {
                    Content = equipmentRelocationDto.SourceRoomId.ToString() + AllConstants.ContentSeparator
                    + equipmentRelocationDto.DestinationRoomId.ToString() + AllConstants.ContentSeparator
                    + amount + AllConstants.ContentSeparator
                    + timeInterval,
                    EquipmentRelocationDto = equipmentRelocationDto
                };
                retVal.Add(searchResultDTO);
            }
            return retVal;
        }
    }
}
