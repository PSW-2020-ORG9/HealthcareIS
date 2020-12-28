using System;
using System.Linq;
using System.Windows;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class EquipmentAndMedicationWindow : Window
    {
        public EquipmentAndMedicationWindow(String[] contentRows)
        {
            InitializeComponent();
            DynamicGridControl dynamicGridControl = new DynamicGridControl(contentRows, true);
            DynamicGrid.Children.Add(dynamicGridControl);
            this.Height = (contentRows.Count() + 1) * 50 + 30;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RealocateEquipment(object sender, RoutedEventArgs e)
        {

        }
    }
}
