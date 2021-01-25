using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for RoomRenovation.xaml
    /// </summary>
    public partial class RoomRenovation : Window
    {
        private IRoomServerController roomServerController = new RoomServerController();
        private IMapObjectController mapObjectController = new MapObjectController();
        private IDoctorServerController doctorServerController = new DoctorServerController();
        private IRenovationServerController renovationServerController = new RenovationServerController();

        int mapObjectId;
        DateTime startDate;
        DateTime endDate;
        TimeInterval timeInterval;
        int neighbourMapObjectId = -1;

        public RoomRenovation(int mapObjectId)
        {
            InitializeComponent();
            this.mapObjectId = mapObjectId;
            SetRoomsComboBox();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetRoomsComboBox()
        {
            List<MapObject> neighborMapObjects = mapObjectController.GetNeighborMapObjects(mapObjectId);
            foreach (MapObject mo in neighborMapObjects)
            {
                DestinationRoomComboBox.Items.Add(mo.Id);
            }
        }

        private void RenovateRoom(object sender, RoutedEventArgs e)
        {
            setDestinationRoomId();
            setDates();
            try
            {
                TimeInterval timeInterval = new TimeInterval(startDate, endDate);
                RenovationDto renovationDto = CreateRenovationDto(timeInterval);
                SchedulingDto schDto = renovationDto.toSchedulingDto();
                List<int> unavailableRooms = roomServerController.GetUnavailableRooms(schDto).ToList();
                if (unavailableRooms.Count > 0)
                {
                    ShowAlternativeRenovationAppointments(unavailableRooms, renovationDto);
                }
                else
                {
                    ScheduleRenovation(schDto);
                }
            }
            catch
            {
                MessageBox.Show("End time must be after start time!", "");
            }
        }

        private void setDestinationRoomId()
        {
            if (DestinationRoomComboBox.SelectedIndex != 0)
                neighbourMapObjectId = int.Parse(DestinationRoomComboBox.SelectedItem.ToString());
        }

        private void setDates()
        {
            startDate =
                DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                + AllConstants.ShiftStart, "MM/dd/yyyy HH:mm", null);
            endDate =
                DateTime.ParseExact(endDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                + AllConstants.ShiftEnd, "MM/dd/yyyy HH:mm", null);

            timeInterval = new TimeInterval(startDate, endDate);
        }

        private void ShowAlternativeRenovationAppointments(List<int> unavailableRooms, RenovationDto renovationDto)
        {
            SchedulingDto dto = renovationDto.toSchedulingDto();
            AlternativeRenovationAppointments newWindow =
            new AlternativeRenovationAppointments(unavailableRooms[0], this, dto);
            newWindow.Show();
        }

        private void ScheduleRenovation(SchedulingDto schDto)
        {

            List<int> doctors = doctorServerController.GetDoctorsByRoomsAndShifts(schDto).ToList();
            foreach (int doctorId in doctors)
            {
                renovationServerController.ScheduleRenovation(timeInterval, doctorId, AllConstants.PatientIdForRenovation);                
            }
            MessageBox.Show("Renovation is successfully scheduled!", "");
            this.Close();
        }

        private RenovationDto CreateRenovationDto(TimeInterval timeInterval)
        {
            RenovationDto renovationDto = new RenovationDto()
            {
                SourceRoomId = mapObjectId,
                DestinationRoomId = neighbourMapObjectId,
                TimeInterval = timeInterval

            };
            return renovationDto;
        }

        private void RenovationTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComplexStackPanel != null)
            {
                if (RenovationTypeComboBox.Text.Equals("Basic"))
                {
                    ComplexStackPanel.Visibility = Visibility.Visible;
                }
                else if(RenovationTypeComboBox.Text.Equals("Complex"))
                {
                    ComplexStackPanel.Visibility = Visibility.Hidden;
                    DestinationRoomComboBox.SelectedIndex = 0;
                }
            }
        }

        private void ComplexRenovationTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RoomStackPanel != null)
            {
                if (ComplexRenovationTypeComboBox.SelectedIndex == 1)
                {
                    RoomStackPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    RoomStackPanel.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
