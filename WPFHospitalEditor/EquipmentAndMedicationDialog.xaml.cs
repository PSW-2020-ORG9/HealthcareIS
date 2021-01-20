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
    public partial class EquipmentAndMedicationDialog : Window
    {
        private readonly IEquipmentTypeServerController equipmentTypeServerController = new EquipmentTypeServerController();
        private readonly int mapObjectId;

        public EquipmentAndMedicationDialog(AdditionalInformationDTO information)
        {
            InitializeComponent();
            this.mapObjectId = information.MapObjectId;
            if (LoggedUser.RoleEquals(Role.Director)) 
            { 
                EnableRelocation();
            }
            DynamicGridControl dynamicGridControl = new DynamicGridControl(information.ContentRows, true);
            DynamicGrid.Children.Add(dynamicGridControl);
            this.Height = (information.ContentRows.Count() + 1) * 50 + 40;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EnableRelocation()
        {
            RelocationStackPanel.Visibility = Visibility.Visible;
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
