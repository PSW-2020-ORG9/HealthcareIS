using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for AppointmentAnalysisWindow.xaml
    /// </summary>
    public partial class AppointmentAnalysisWindow : Window
    {
        private readonly IExaminationServerController examinationServerController = new ExaminationServerController();
        private readonly ISchedulingServerController schedulingController = new SchedulingServerController();
        public ObservableCollection<ExaminationWithAvailableReschedulingDto> examinationWithAvailableRescheduling { get; set; }
        public AppointmentAnalysisWindow(int specialtyId)
        {
            InitializeComponent();
            FillObservableCollection(specialtyId);
            this.DataContext = this;
        }

        private void FillObservableCollection(int specialtyId)
        {
            examinationWithAvailableRescheduling = new ObservableCollection<ExaminationWithAvailableReschedulingDto>();
            List<Examination> examinations = examinationServerController.GetBySpecialtyId(specialtyId).ToList();
            foreach (Examination exam in examinations) {
                ExaminationWithAvailableReschedulingDto examination = new ExaminationWithAvailableReschedulingDto(exam.Id,exam.PatientId, exam.DoctorId, exam.Priority, exam.TimeInterval.Start, DateTime.Now, exam.RequiredSpecialtyId);
                examinationWithAvailableRescheduling.Add(examination);
                examinationSearchComboBox.Items.Add(exam.Id);
            }

            foreach (ExaminationWithAvailableReschedulingDto exam in examinationWithAvailableRescheduling) {
                RecommendationRequestDto recommendationRequestDto = new RecommendationRequestDto {
                    DoctorId = exam.DoctorId,
                    Preference = RecommendationPreference.Time,
                    SpecialtyId = exam.RequiredSpecialtyId,
                    TimeInterval = new TimeInterval(DateTime.Now.AddHours(1), DateTime.Now.AddDays(5))
                };
                List<RecommendationDto> recommendationDtos = schedulingController.GetAppointments(recommendationRequestDto);
                exam.ReschedulingDate = recommendationDtos[0].TimeInterval.Start;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Cancel_Examination(object sender, RoutedEventArgs e)
        {
            int examinationId = int.Parse(examinationSearchComboBox.Text);
            string cancel = examinationServerController.Cancel(examinationId);
            if (!cancel.Equals("BadRequest"))
            {
                MessageBox.Show("Examination is successfuly CANCELED!");
            }
            else {
                MessageBox.Show("Examination cannot be CANCELED!");
            }
            ExaminationWithAvailableReschedulingDto examForScheduling = null;
            foreach (ExaminationWithAvailableReschedulingDto exam in examinationWithAvailableRescheduling) {
                if (exam.ExaminationId == examinationId) {
                    examForScheduling = exam;
                    break;
                }
            }
            RescheduleExamination(examForScheduling);
        }

        private void RescheduleExamination(ExaminationWithAvailableReschedulingDto exam) {
            Examination examination = examinationServerController.ScheduleEmergencyExamination(exam.ReschedulingDate, exam.DoctorId, exam.PatientId);
            if (examination != null)
            {
                MessageBox.Show("Examination is successfuly SCHEDULED!");
            }
            else {
                MessageBox.Show("Examination CANNOT be scheduled!");
            }
        }
    }
}
