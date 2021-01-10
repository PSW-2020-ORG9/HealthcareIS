using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for EquipmentRelocation.xaml
    /// </summary>
    public partial class EquipmentRelocation : Window
    {
        private IRoomServerController roomServerController = new RoomServerController();
        private IEquipmentServerController equipmentServerController = new EquipmentServerController();
        private IExaminationServerController examinationServerController = new ExaminationServerController();
        public ObservableCollection<RoomsWithChosenEquipmentAmount> roomsWithEquipmenAmount { get; set; }
        private HospitalMap hospitalMap;
        private int destinationRoomId;
        private string relocationEquipmentName;

        public EquipmentRelocation(string relocationEquipmentName, int roomId, HospitalMap hospitalMap)
        {
            SearchResultDialog.selectedObjectId = -1;
            InitializeComponent();
            equipmentName.Text += relocationEquipmentName;
            this.destinationRoomId = roomId;
            this.relocationEquipmentName = relocationEquipmentName;
            this.hospitalMap = hospitalMap;
            roomsWithEquipmenAmount = new ObservableCollection<RoomsWithChosenEquipmentAmount>();
            GetRoomsWithGivenEquipmentAmount();
            SetRoomsComboBox();
            this.DataContext = this;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetRoomsComboBox()
        {
            List<Room> rooms = roomServerController.getRoomsByEquipmentType(relocationEquipmentName).ToList();
            foreach (Room room in rooms)
            {
                roomSearchComboBox.Items.Add(room.Id);
            }
        }

        private void relocateEquipment(object sender, RoutedEventArgs e)
        {
            if(CheckAmount())
            {
                List<int> unavailableRooms = checkRoomsAvailability();
                if (unavailableRooms.Count > 0)
                {
                    RoomUnavailable(unavailableRooms);
                }
                else
                {
                    ScheduleRelocation();
                }
            }

        }

        private void RoomUnavailable(List<int> unavailableRooms)
        {
            AlternativeRelocationAppointments newWindow =
                        new AlternativeRelocationAppointments(unavailableRooms[0], hospitalMap, this);
            newWindow.Show();
        }

        private void ScheduleRelocation()
        {
            DateTime startDate =
                        DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                        + " " + StartTime.Text, "MM/dd/yyyy HH:mm", null);
            DateTime endDate =
                DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                + " " + EndTime.Text, "MM/dd/yyyy HH:mm", null);

            while (startDate < endDate)
            {

                examinationServerController.ScheduleExamination(startDate, 1, 2);
                startDate = startDate.AddMinutes(30);
            }
            MessageBox.Show("Relocation is successfully scheduled!", "");
            this.Close();
        }

        private List<int> checkRoomsAvailability()
        {
            DateTime startDate = DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy") + " " + StartTime.Text, "MM/dd/yyyy HH:mm", null);
            DateTime endDate = DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy") + " " + EndTime.Text, "MM/dd/yyyy HH:mm", null);

            TimeInterval timeInterval = new TimeInterval(startDate, endDate);

            EquipmentRelocationDto equipmentRelocationDto = new EquipmentRelocationDto()
            {
                SourceRoomId = (int)roomSearchComboBox.SelectedItem,
                DestinationRoomId = destinationRoomId,
                Amount = int.Parse(equipmentAmount.Text),
                EquipmentType = relocationEquipmentName,
                TimeInterval = timeInterval

            };
            return roomServerController.GetUnavailableRoomsIdsInTimeInterval(equipmentRelocationDto).ToList();
        }

        private void GetRoomsWithGivenEquipmentAmount()
        {
            List<Room> rooms = roomServerController.getRoomsByEquipmentType(relocationEquipmentName).ToList();
            List<EquipmentDto> equipments = equipmentServerController.GetEquipmentByType(relocationEquipmentName).ToList();
            FillObservableCollection(rooms, equipments);
        }

        private void FillObservableCollection(List<Room> rooms, List<EquipmentDto> equipments)
        {
            foreach (Room room in rooms)
            {
                foreach (EquipmentDto eqdto in equipments)
                {
                    if (eqdto.RoomId == room.Id)
                    {
                        roomsWithEquipmenAmount.Add(new RoomsWithChosenEquipmentAmount(room.Id, room.Name, eqdto.Quantity));
                    }
                }
            }
        }

        private int GetEquipmentAmountByRoomId(int id)
        {
            foreach(RoomsWithChosenEquipmentAmount roomWithEquipment in roomsWithEquipmenAmount)
            {
                if (roomWithEquipment.RoomId == id)
                {
                    return roomWithEquipment.EquipmentAmount;
                }
            }
            return 0;
        }

        private bool CheckAmount()
        {
            int number;
            if (int.TryParse(equipmentAmount.Text, out number))
            {
                if (int.Parse(equipmentAmount.Text) <= GetEquipmentAmountByRoomId((int)roomSearchComboBox.SelectedItem))
                {
                    return true;
                } else
                {
                    MessageBox.Show("There is not enough equipment in room!", "");
                    return false;
                }
            }
            MessageBox.Show("Amount must be number!", "");
            return false;
        }
    }
}
