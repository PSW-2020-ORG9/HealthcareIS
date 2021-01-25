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
    /// Interaction logic for RoomInformation.xaml
    /// </summary>
    public partial class RoomInformation : Window
    {
        public RoomInformation()
        {
            InitializeComponent();
        }

        private void RenovationTypeSelection(object sender, SelectionChangedEventArgs e)
        {
            /*
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(RoomRenovation))
                {
                    if ((window as RoomRenovation).ComplexRenovationTypeComboBox.SelectedIndex == 1)
                    {
                        DividingStackPanel.Visibility = Visibility.Visible;
                        MergingStackPanel.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        DividingStackPanel.Visibility = Visibility.Hidden;
                        MergingStackPanel.Visibility = Visibility.Visible;
                    }
                }
            }
            */
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
