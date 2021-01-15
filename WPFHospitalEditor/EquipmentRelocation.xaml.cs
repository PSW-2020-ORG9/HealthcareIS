﻿using System;
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
        private IDoctorServerController doctorServerController = new DoctorServerController();
        private IEquipmentServerController equipmentServerController = new EquipmentServerController();
        private IExaminationServerController examinationServerController = new ExaminationServerController();
        public ObservableCollection<RoomWithChosenEquipmentAmount> roomsWithEquipmentAmount { get; set; }
        private int destinationRoomId;
        private string relocationEquipmentName;
        private DateTime startDate;
        private DateTime endDate;

        public EquipmentRelocation(string relocationEquipmentName, int roomId)
        {
            SearchResultDialog.selectedObjectId = -1;
            InitializeComponent();
            equipmentName.Text += relocationEquipmentName;
            this.destinationRoomId = roomId;
            this.relocationEquipmentName = relocationEquipmentName;
            FillObservableCollection();
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
                if (room.Id != destinationRoomId)
                    roomSearchComboBox.Items.Add(room.Id);
            }
        }

        private void RelocateEquipment(object sender, RoutedEventArgs e)
        {
            startDate =
                DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                + " " + StartTime.Text, "MM/dd/yyyy HH:mm", null);
            endDate =
                DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                + " " + EndTime.Text, "MM/dd/yyyy HH:mm", null);
            TimeInterval timeInterval = new TimeInterval(startDate, endDate);

            if (AmountIsValid() && timeInterval.IsValid())
            {
                EquipmentRelocationDto eqRelDto = CreateEquipmentRelocationDto(timeInterval);
                List<int> unavailableRooms = roomServerController.GetUnavailableRoomsIdsInTimeInterval(eqRelDto).ToList();
                if (unavailableRooms.Count > 0)
                {
                    ShowAlternativeRelocationAppointments(unavailableRooms);
                }
                else
                {
                    ScheduleRelocation(eqRelDto);
                }
            }

        }

        private void ShowAlternativeRelocationAppointments(List<int> unavailableRooms)
        {
            AlternativeRelocationAppointments newWindow =
            new AlternativeRelocationAppointments(unavailableRooms[0], this);
            newWindow.Show();
        }

        private void ScheduleRelocation(EquipmentRelocationDto eqRelDto)
        {

            List<int> doctors = doctorServerController.GetDoctorsByRoomsAndShifts(eqRelDto).ToList();
            foreach (int doctorId in doctors)
            {
                startDate = eqRelDto.TimeInterval.Start;
                while (startDate < endDate)
                {
                    examinationServerController.ScheduleExamination(startDate, doctorId, 2);
                    startDate = startDate.AddMinutes(30);
                }
            }
            MessageBox.Show("Relocation is successfully scheduled!", "");
            this.Close();
        }

        private EquipmentRelocationDto CreateEquipmentRelocationDto(TimeInterval timeInterval)
        {
            EquipmentRelocationDto equipmentRelocationDto = new EquipmentRelocationDto()
            {
                SourceRoomId = (int)roomSearchComboBox.SelectedItem,
                DestinationRoomId = destinationRoomId,
                Amount = int.Parse(equipmentAmount.Text),
                EquipmentType = relocationEquipmentName,
                TimeInterval = timeInterval

            };
            return equipmentRelocationDto;
        }

        private void FillObservableCollection()
        {
            roomsWithEquipmentAmount = new ObservableCollection<RoomWithChosenEquipmentAmount>();
            List<Room> rooms = roomServerController.getRoomsByEquipmentType(relocationEquipmentName).ToList();
            List<EquipmentDto> equipments = equipmentServerController.GetEquipmentByType(relocationEquipmentName).ToList();
            foreach (Room room in rooms)
            {
                foreach (EquipmentDto eqdto in equipments)
                {
                    if (eqdto.RoomId == room.Id && room.Id != destinationRoomId)
                    {
                        roomsWithEquipmentAmount.Add(new RoomWithChosenEquipmentAmount(room.Id, room.Name, eqdto.Quantity));
                    }
                }
            }
        }

        private int GetEquipmentAmountByRoomId(int id)
        {
            foreach (RoomWithChosenEquipmentAmount roomWithEquipment in roomsWithEquipmentAmount)
            {
                if (roomWithEquipment.RoomId == id)
                {
                    return roomWithEquipment.EquipmentAmount;
                }
            }
            return 0;
        }

        private bool AmountIsValid()
        {
            int number;
            if (int.TryParse(equipmentAmount.Text, out number))
            {
                if (int.Parse(equipmentAmount.Text) <= GetEquipmentAmountByRoomId((int)roomSearchComboBox.SelectedItem))
                {
                    return true;
                }
                else
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
