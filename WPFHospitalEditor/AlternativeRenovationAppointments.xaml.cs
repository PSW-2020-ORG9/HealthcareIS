using System.Windows;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for AlternativeRenovationAppointments.xaml
    /// </summary>
    public partial class AlternativeRenovationAppointments : Window
    {

        private readonly int roomId;
        private readonly RoomRenovation rr;

        public AlternativeRenovationAppointments(int roomId, RoomRenovation rr)
        {
            InitializeComponent();
            this.roomId = roomId;
            this.rr = rr;
            this.roomId = roomId;
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

        }
    }
}
