using System;
using System.Linq;
using System.Windows;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class EquipmentAndMedicationWindow : Window
    {
        private IEquipmentTypeServerController equipmentTypeServerController = new EquipmentTypeServerController();
        int mapObjectId;

        public EquipmentAndMedicationWindow(String[] contentRows, int mapObjectId)
        {
            InitializeComponent();
            if (LoggedUser.Role != Role.Director) RelocationStackPanel.Visibility = Visibility.Hidden;
            this.mapObjectId = mapObjectId;
            SetEquipmentTypeComboBox();
            DynamicGridControl dynamicGridControl = new DynamicGridControl(contentRows, true);
            DynamicGrid.Children.Add(dynamicGridControl);
            this.Height = (contentRows.Count() + 1) * 50 + 40;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetEquipmentTypeComboBox()
        {
            foreach (EquipmentTypeDto eqTypeDto in equipmentTypeServerController.GetAllEquipmentTypes())
            {
                relocationEquipmentComboBox.Items.Add(eqTypeDto.Name);
            }
            emptyEquipmentComboBox.Content = AllConstants.EmptyComboBox;
        }

        private void RelocateEquipment(object sender, RoutedEventArgs e)
        {
            if (relocationEquipmentComboBox.SelectedIndex != 0)
            {
                EquipmentRelocation equipmentRelocation = new EquipmentRelocation(relocationEquipmentComboBox.SelectedItem.ToString(), mapObjectId);
                equipmentRelocation.Show();
                this.Close();
            }
        }
    }
}
