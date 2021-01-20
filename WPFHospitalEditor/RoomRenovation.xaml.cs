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
    /// Interaction logic for RoomRenovation.xaml
    /// </summary>
    public partial class RoomRenovation : Window
    {
        int mapObjectId;

        public RoomRenovation(int mapObjectId)
        {
            InitializeComponent();
            this.mapObjectId = mapObjectId;
        }

        private void ScheduleRenovation(object sender, RoutedEventArgs e)
        {
            
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
