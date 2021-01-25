using System.Windows;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.StrategyPattern;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for AlternativeRenovationAppointments.xaml
    /// </summary>
    public partial class AlternativeRenovationAppointments : Window
    {

        private readonly int roomId;
        private readonly RoomRenovation roomRenovation;
        private readonly SchedulingDto schedulingDto;

        public AlternativeRenovationAppointments(int roomId, RoomRenovation roomRenovation, SchedulingDto schedulingDto)
        {
            InitializeComponent();
            this.roomId = roomId;
            this.roomRenovation = roomRenovation;
            this.roomId = roomId;
            this.schedulingDto = schedulingDto;
            givenRoom.Text = "Room with id: " + roomId.ToString() + " is unavailable in given time interval. " +
                "Click on 'Show appointments' " +
                "to see available appointments for renovation";
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowAppointments(object sender, RoutedEventArgs e)
        {
            if (schedulingDto.DestinationRoomId == -1)
            {
                ISearchResultStrategy strategy = new SearchResultStrategy(new BasicRenovationaAppointmentsSearchResult(schedulingDto));
                SearchResultDialog equipmentRelocationDialog = new SearchResultDialog(strategy.GetSearchResult(), SearchType.BasicRoomRenovationSearch);
                equipmentRelocationDialog.ShowDialog();
            }
            else
            {
                ISearchResultStrategy strategy = new SearchResultStrategy(new RenovationaAppointmentsSearchResult(schedulingDto));
                SearchResultDialog equipmentRelocationDialog = new SearchResultDialog(strategy.GetSearchResult(), SearchType.RoomRenovationSearch);
                equipmentRelocationDialog.ShowDialog();
            }
        }
    }
}
