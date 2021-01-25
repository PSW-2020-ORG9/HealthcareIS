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

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for RoomScheduledExaminations.xaml
    /// </summary>
    public partial class RoomScheduledExaminations : Window
    {
        public RoomScheduledExaminations()
        {
            InitializeComponent();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelAppointment(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
