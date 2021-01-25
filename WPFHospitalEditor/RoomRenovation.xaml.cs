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
        private IExaminationServerController examinationServerController = new ExaminationServerController();

        int mapObjectId;
        DateTime startDate;
        DateTime endDate;
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
            List<MapObject> mapObjects = mapObjectController.GetNeigbourMapObjects(mapObjectId);
            foreach (MapObject mo in mapObjects)
            {
                SecondRoomComboBox.Items.Add(mo.Id);
            }
        }

        private void RenovateRoom(object sender, RoutedEventArgs e)
        {
            setSecondRoomId();
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

        private void setSecondRoomId()
        {
            if (SecondRoomComboBox.SelectedIndex != 0)
                neighbourMapObjectId = int.Parse(SecondRoomComboBox.SelectedItem.ToString());
        }

        private void setDates()
        {
            startDate =
                DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                + AllConstants.ShiftStart, "MM/dd/yyyy HH:mm", null);
            endDate =
                DateTime.ParseExact(endDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                + AllConstants.ShiftEnd, "MM/dd/yyyy HH:mm", null);
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
                startDate = schDto.TimeInterval.Start;
                while (startDate < endDate)
                {
                    examinationServerController.ScheduleExamination(startDate, doctorId, AllConstants.PatientIdForRenovation);
                    startDate = startDate.AddMinutes(30);
                    if(startDate.Hour == AllConstants.ShiftEndHour)
                    {
                        startDate = startDate.AddHours(16);
                    }
                }
            }
            MessageBox.Show("Renovation is successfully scheduled!", "");
            this.Close();
        }

        private RenovationDto CreateRenovationDto(TimeInterval timeInterval)
        {
            RenovationDto renovationDto = new RenovationDto()
            {
                FirstRoomId = mapObjectId,
                SecondRoomId = neighbourMapObjectId,
                TimeInterval = timeInterval

            };
            return renovationDto;
        }

        private void RenovationTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComplexStackPanel != null)
            {
                if (RenovationTypeComboBox.SelectedIndex == 1)
                {
                    ComplexStackPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    ComplexStackPanel.Visibility = Visibility.Hidden;
                    SecondRoomComboBox.SelectedIndex = 0;
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
