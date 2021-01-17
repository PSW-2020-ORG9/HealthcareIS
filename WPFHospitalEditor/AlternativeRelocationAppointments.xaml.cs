using System.Windows;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Pages;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for AlternativeRelocationAppointments.xaml
    /// </summary>
    public partial class AlternativeRelocationAppointments : Window
    {
        private readonly IMapObjectController mapObjectController = new MapObjectController();
        private readonly IRoomServerController roomServerController = new RoomServerController();

        int roomId;
        EquipmentRelocation er;

        public AlternativeRelocationAppointments(int roomId, EquipmentRelocation er)
        {
            InitializeComponent();
            this.er = er;
            this.roomId = roomId;
            givenRoom.Text = "Room with id: " + roomId.ToString() + " is unavailable in given time interval. Click 'Show on map' to see that room.";
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

    }
}
