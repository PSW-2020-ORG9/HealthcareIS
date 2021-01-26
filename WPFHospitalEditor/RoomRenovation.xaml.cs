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
        private IExaminationServerController examinationServerController = new ExaminationServerController();

        int mapObjectId;
        TimeInterval timeInterval;
        int neighbourMapObjectId = -1;
        DateTime startDate;
        DateTime endDate;

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
            DateTime startDate =
                DateTime.ParseExact(startDatePicker.SelectedDate.Value.ToString("MM/dd/yyyy")
                + AllConstants.ShiftStart, "MM/dd/yyyy HH:mm", null);
            DateTime endDate =
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
            if (RenovationTypeComboBox.Text.Equals("Complex"))
            {
                RoomInformation roomInformation = new RoomInformation();

                if (ComplexRenovationTypeComboBox.Text.Equals("Separate room"))
                {
                    roomInformation.MergingStackPanel.Visibility = Visibility.Hidden;
                    roomInformation.DividingStackPanel.Visibility = Visibility.Visible;
                    roomInformation.Room1Name.Text = mapObjectController.GetMapObjectById(mapObjectId).Name;
                    int workTime = mapObjectController.GetMapObjectById(mapObjectId).MapObjectDescription.Information.Split("=")[1].Length;
                    roomInformation.WorkTime1.Text = mapObjectController.GetMapObjectById(mapObjectId).MapObjectDescription.Information.Split("=")[1].Substring(0, workTime - 1);

                }
                else if (ComplexRenovationTypeComboBox.Text.Equals("Join rooms"))
                {
                    roomInformation.DividingStackPanel.Visibility = Visibility.Hidden;
                    roomInformation.MergingStackPanel.Visibility = Visibility.Visible;
                    roomInformation.RoomName.Text = mapObjectController.GetMapObjectById(mapObjectId).Name;
                    int workTime = mapObjectController.GetMapObjectById(mapObjectId).MapObjectDescription.Information.Split("=")[1].Length;
                    roomInformation.WorkTime.Text = mapObjectController.GetMapObjectById(mapObjectId).MapObjectDescription.Information.Split("=")[1].Substring(0, workTime - 1);
                    List<int> doctors1 = doctorServerController.GetDoctorsByRoomsAndShifts(schDto).ToList();
                    foreach (int doctorId in doctors1)
                    {
                        startDate = schDto.TimeInterval.Start.AddDays(1);
                        examinationServerController.ScheduleExamination(startDate, doctorId, AllConstants.PatientIdForRelocation);
                    }

                }
                
                roomInformation.ShowDialog();
                
            }
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
                }
            }
        }

        private void ComplexRenovationTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RoomStackPanel != null)
            {
                if (ComplexRenovationTypeComboBox.Text.Equals("Separate room"))
                {
                    RoomStackPanel.Visibility = Visibility.Visible;
                }
                else if (ComplexRenovationTypeComboBox.Text.Equals("Join rooms"))
                {
                    RoomStackPanel.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
