using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for RoomRenovation.xaml
    /// </summary>
    public partial class RoomRenovation : Window
    {
        private IRoomServerController roomServerController = new RoomServerController();
        private IDoctorServerController doctorServerController = new DoctorServerController();
        private IExaminationServerController examinationServerController = new ExaminationServerController();

        int mapObjectId;
        DateTime startDate;
        DateTime endDate;
        int neighbourMapObjectId = -1;

        public RoomRenovation(int mapObjectId)
        {
            InitializeComponent();
            ComplexStackPanel.Visibility = Visibility.Hidden;
            this.mapObjectId = mapObjectId;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RenovateRoom(object sender, RoutedEventArgs e)
        {
            startDate =
                DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                + " " + StartTime.Text, "MM/dd/yyyy HH:mm", null);
            endDate =
                DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                + " " + EndTime.Text, "MM/dd/yyyy HH:mm", null);
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

        private void ShowAlternativeRenovationAppointments(List<int> unavailableRooms, RenovationDto renovationDto)
        {
            AlternativeRenovationAppointments newWindow =
            new AlternativeRenovationAppointments(unavailableRooms[0], this);
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
                    examinationServerController.ScheduleExamination(startDate, doctorId, AllConstants.PatientIdForRelocation);
                    startDate = startDate.AddMinutes(30);
                }
            }
            MessageBox.Show("Relocation is successfully scheduled!", "");
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
    }
}
