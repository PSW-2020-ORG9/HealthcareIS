﻿using HealthcareBase.Dto;
using System;
using System.Linq;
using System.Windows;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class EquipmentAndMedicationWindow : Window
    {
        private MapObject mapObject;
        private IEquipmentTypeServerController equipmentTypeServerController = new EquipmentTypeServerController();
        public EquipmentAndMedicationWindow(String[] contentRows, MapObject mapObject)
        {
            InitializeComponent();
            this.mapObject = mapObject;
            SetEquipmentTypeComboBox();
            DynamicGridControl dynamicGridControl = new DynamicGridControl(contentRows, true);
            DynamicGrid.Children.Add(dynamicGridControl);
            this.Height = (contentRows.Count() + 1) * 50 + 30 + 100;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
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
                EquipmentRelocation equipmentRelocation = new EquipmentRelocation(relocationEquipmentComboBox.SelectedItem.ToString());
                equipmentRelocation.Show();
            }
        }
    }
}
