using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for RoomScheduledAppointments.xaml
    /// </summary>
    public partial class RoomScheduledExaminations : Window
    {
        private IExaminationServerController examinationServerController = new ExaminationServerController();

        private int roomId;
        public ObservableCollection<RoomScheduledAppointmentDto> roomExaminations { get; set; }
        public ObservableCollection<RoomScheduledAppointmentDto> roomRelocations { get; set; }
        public ObservableCollection<RoomScheduledAppointmentDto> roomRenovations { get; set; }

        public RoomScheduledExaminations(int roomId)
        {
            InitializeComponent();
            this.roomId = roomId;
            FillObservableCollection();
            SetExaminationIdsComboBox();
            this.DataContext = this;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelAppointment(object sender, RoutedEventArgs e)
        {
            if (appointmentCancelComboBox.SelectedIndex != 0)
            {
                string cancel = examinationServerController.Cancel(int.Parse(appointmentCancelComboBox.SelectedItem.ToString()));
                if (!cancel.Equals("BadRequest"))
                {
                    MessageBox.Show("Examination is successfuly CANCELED!");
                    ClearObservableCollectionsAndComboBox(int.Parse(appointmentCancelComboBox.SelectedItem.ToString()));
                    SetExaminationIdsComboBox();
                }
                else
                    MessageBox.Show("Examination cannot be CANCELED!");
            } else
                MessageBox.Show("You didn't pick any examination id!");
        }

        private void ClearObservableCollectionsAndComboBox(int examinationId)
        {
            RemoveDeletedExamination(examinationId);
            appointmentCancelComboBox.Items.Clear();
            appointmentCancelComboBox.Items.Add("None");
            appointmentCancelComboBox.SelectedIndex = 0;
        }

        private void RemoveDeletedExamination(int examinationId)
        {
            if (FindAndDeleteByExaminationId(roomExaminations, examinationId)) return;
            if (FindAndDeleteByExaminationId(roomRelocations, examinationId)) return;
            FindAndDeleteByExaminationId(roomRenovations, examinationId);
        }

        private bool FindAndDeleteByExaminationId(ObservableCollection<RoomScheduledAppointmentDto> rsaDtos, int examinationId)
        {
            foreach (RoomScheduledAppointmentDto rsaDto in rsaDtos)
            {
                if (rsaDto.ExaminationId == examinationId)
                {
                    rsaDtos.Remove(rsaDto);
                    return true;
                }
            }
            return false;
        }

        private void FillObservableCollection()
        {
            CreateEmptyObservableCollections();
            List<Examination> allRoomAppointments = examinationServerController.getByRoomId(roomId).ToList();
            foreach (Examination examination in allRoomAppointments)
            {
                if(!examination.IsCanceled)
                {
                    RoomScheduledAppointmentDto rsaDto = CreateRoomScheduledAppointmentDto(examination);
                    if (examination.PatientId == AllConstants.PatientIdForRelocation)
                        roomRelocations.Add(rsaDto);
                    else if (examination.PatientId == AllConstants.PatientIdForRenovation)
                        roomRenovations.Add(rsaDto);
                    else
                        roomExaminations.Add(rsaDto);
                }
            }
        }

        private RoomScheduledAppointmentDto CreateRoomScheduledAppointmentDto(Examination examination)
        {
            RoomScheduledAppointmentDto rsaDto;
            rsaDto = new RoomScheduledAppointmentDto
            {
                ExaminationId = examination.Id,
                StartTime = examination.TimeInterval.Start,
                EndTime = examination.TimeInterval.End,
                DoctorId = examination.DoctorId,
                PatientId = examination.PatientId
            };
            return rsaDto;
        }

        private void CreateEmptyObservableCollections()
        {
            roomExaminations = new ObservableCollection<RoomScheduledAppointmentDto>();
            roomRelocations = new ObservableCollection<RoomScheduledAppointmentDto>();
            roomRenovations = new ObservableCollection<RoomScheduledAppointmentDto>();
        }

        private void AppointmentTypeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(scrollViewer1 != null)
            {
                if (appointmentTypeComboBox.SelectedIndex == 0)
                {
                    scrollViewer1.Visibility = Visibility.Visible;
                    scrollViewer2.Visibility = Visibility.Collapsed;
                    scrollViewer3.Visibility = Visibility.Collapsed;
                }
                else if (appointmentTypeComboBox.SelectedIndex == 1)
                {
                    scrollViewer1.Visibility = Visibility.Collapsed;
                    scrollViewer2.Visibility = Visibility.Visible;
                    scrollViewer3.Visibility = Visibility.Collapsed;
                }
                else
                {
                    scrollViewer1.Visibility = Visibility.Collapsed;
                    scrollViewer2.Visibility = Visibility.Collapsed;
                    scrollViewer3.Visibility = Visibility.Visible;
                }
            }
        }

        private void SetExaminationIdsComboBox()
        {
            AddToExaminationIdsComboBox(roomExaminations);
            AddToExaminationIdsComboBox(roomRelocations);
            AddToExaminationIdsComboBox(roomRenovations);
        }

        private void AddToExaminationIdsComboBox(ObservableCollection<RoomScheduledAppointmentDto> rsaDtos)
        {
            foreach (RoomScheduledAppointmentDto rsaDto in rsaDtos)
                appointmentCancelComboBox.Items.Add(rsaDto.ExaminationId);
        }
    }
}
