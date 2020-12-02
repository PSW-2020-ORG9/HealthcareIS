using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class EquipmentWindow : Window
    {
        public EquipmentWindow(String[] contentRows, String separator, Role role)
        {
            InitializeComponent();
            DynamicGridControl dynamicGridControl = new DynamicGridControl(contentRows, "=", role, true);
            DynamicGrid.Children.Add(dynamicGridControl);
            this.Height = (contentRows.Count() + 2) * 50 + 30;
            SetButtonsCommonAttributes(Close);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetButtonsCommonAttributes(Button button)
        {
            button.BorderThickness = new Thickness(0);
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Background = Brushes.SkyBlue;
            button.Width = AllConstants.additionalInformationsbuttonWidth;
            button.Height = AllConstants.additionalInformationsbuttonHeight;
            button.Foreground = Brushes.White;
        }
    }
}
