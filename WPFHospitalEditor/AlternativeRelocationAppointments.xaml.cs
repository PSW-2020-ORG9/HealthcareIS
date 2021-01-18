using System.Collections.Generic;
using System.Windows;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Pages;
using WPFHospitalEditor.StrategyPattern;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for AlternativeRelocationAppointments.xaml
    /// </summary>
    public partial class AlternativeRelocationAppointments : Window
    {
        public IMapObjectController mapObjectController = new MapObjectController();
        public IRoomServerController roomServerController = new RoomServerController();
        private ISchedulingServerController schedulingServerController = new SchedulingServerController();
        private EquipmentRecommendationRequestDto eqRequest;
        private string equipmentName;

        int roomId;
        EquipmentRelocation er;

        public AlternativeRelocationAppointments(int roomId, EquipmentRelocation er, EquipmentRecommendationRequestDto eqRequest, string equipmentName)
        {
            InitializeComponent();
            this.equipmentName = equipmentName;
            this.eqRequest = eqRequest;
            this.er = er;
            this.roomId = roomId;
            givenRoom.Text = "Room with id: " + roomId.ToString() + " is unavailable in given time interval. " +
                "Click 'Show on map' to see that room and click on 'Show appointments' " +
                "to see available appointments for relocation";
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowRoomOnMap(object sender, RoutedEventArgs e)
        {
            MapObject mapObject;
            mapObject = mapObjectController.GetMapObjectById(roomId);
            SearchResultDialog.selectedObjectId = roomId;
            mapObjectController.Update(mapObject);
            DisplayBuildingAndFloorBasedOnSelectedObject(mapObject.MapObjectDescription.FloorNumber, mapObject.MapObjectDescription.BuildingId);
            this.Close();
            er.Close();
        }

        private void DisplayBuildingAndFloorBasedOnSelectedObject(int floorNumber, int buildingId)
        {
            BuildingPage searchedBuilding = new BuildingPage(buildingId, floorNumber);
            HospitalMainWindow.GetInstance().ChangePage(searchedBuilding);
        }

        private void ShowAppointments(object sender, RoutedEventArgs e)
        {
            List<EquipmentRelocationDto> searchResult = schedulingServerController.GetEquipmentRelocationAppointments(eqRequest);
            ISearchResultStrategy strategy = new SearchResultStrategy(new EquipmentRelocationSearchResult(searchResult, equipmentName));
            SearchResultDialog equipmentRelocationDialog = new SearchResultDialog(strategy.GetSearchResult(), SearchType.EquipmentRelocationSearch);
            equipmentRelocationDialog.ShowDialog();
        }
    }
}
