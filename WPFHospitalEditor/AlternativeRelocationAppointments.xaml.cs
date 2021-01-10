using System.Windows;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for AlternativeRelocationAppointments.xaml
    /// </summary>
    public partial class AlternativeRelocationAppointments : Window
    {
        public IMapObjectController mapObjectController = new MapObjectController();
        public IRoomServerController roomServerController = new RoomServerController();

        MapObject mapObject = null;
        int roomId;
        HospitalMap hospitalMap;
        EquipmentRelocation er;

        public AlternativeRelocationAppointments(int roomId, HospitalMap hospitalMap, EquipmentRelocation er)
        {
            InitializeComponent();
            this.er = er;
            this.hospitalMap = hospitalMap;
            this.roomId = roomId;
            givenRoom.Text += roomId.ToString();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowRoomOnMap(object sender, RoutedEventArgs e)
        {
            mapObject = mapObjectController.GetMapObjectById(roomId);
            SearchResultDialog.selectedObjectId = roomId;
            mapObjectController.Update(mapObject);
            DisplayBuildingAndFloorBasedOnSelectedObject(mapObject.MapObjectDescription.FloorNumber, mapObject.MapObjectDescription.BuildingId);
            this.Close();
            er.Close();
            hospitalMap.OwnedWindows[0].Close();
        }

        public void DisplayBuildingAndFloorBasedOnSelectedObject(int floorNumber, int buildingId)
        {
            Building searchedBuilding = new Building(buildingId, hospitalMap, floorNumber);
            searchedBuilding.Owner = hospitalMap;
            searchedBuilding.Show();
        }

    }
}
