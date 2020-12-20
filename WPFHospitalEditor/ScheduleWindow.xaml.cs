using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Model.Users.Patient;
using System;
using System.Collections.Generic;
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

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        RecommendationDto recommendationDto;
        List<Patient> allPatients = new List<Patient>();

        public ScheduleWindow(RecommendationDto recommendationDto)
        {
            InitializeComponent();
        }
    }
}
