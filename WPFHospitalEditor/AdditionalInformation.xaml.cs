using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Controller;
using HealthcareBase.Dto;
using System.Linq;
using System;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.StrategyPattern;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for AdditionalInformation.xaml
    /// </summary>
    public partial class AdditionalInformation : Window
    {

        private MapObject mapObject;
        private Building building;
        private DynamicGridControl dynamicGridControl;

        public AdditionalInformation(MapObject mapObject, Building building)
        {
            InitializeComponent();
            this.mapObject = mapObject;
            this.building = building;
            dynamicGridControl = new DynamicGridControl(getInfo(), IsPatientLogged());
            DynamicGrid.Children.Add(this.dynamicGridControl);            
            InitializeTitle();
            SetButtonsVisibility();
        }

        private string[] getInfo()
        {
            string[] descriptionParts = mapObject.Description.Split("&");
            return descriptionParts[1].Split(";");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            mapObject.Description = mapObject.Description.Split("&")[0] + "&" + dynamicGridControl.GetAllContent();
            mapObject.Name = this.Title.Text;
            mapObject.nameOnMap.Text = mapObject.Name;
            UpdateAdditionalInformation();
            mapObject.Description = mapObject.Description.Substring(0, mapObject.Description.Length - 1);
            this.Close();
        }

        private void RefreshMap()
        {
            building.canvas.Children.Clear();
            CanvasService.AddObjectToCanvas(building.floorBuildingObjects, building.canvas);
        }      

        private void UpdateAdditionalInformation()
        {
            IMapObjectController mapObjectController = new MapObjectController();
            mapObjectController.Update(mapObject);
            RefreshMap();
        }

        private void InitializeTitle()
        {
            Title.Text = mapObject.Name;
            if (HospitalMap.role.Equals(Role.Patient))
            {
                Title.IsReadOnly = true;
            }
        }

        private void BtnEquipment_Click(object sender, RoutedEventArgs e)
        {
            ContentRowsStrategy strategy = new ContentRowsStrategy(new EquipmentContentRows(mapObject.Id));
            EquipmentAndMedicationWindow equipment = new EquipmentAndMedicationWindow(strategy.GetContentRows());
            equipment.ShowDialog();
        }
        private void BtnMedications_Click(object sender, RoutedEventArgs e)
        {
            ContentRowsStrategy strategy = new ContentRowsStrategy(new MedicationContentRows(mapObject.Id));
            EquipmentAndMedicationWindow medications = new EquipmentAndMedicationWindow(strategy.GetContentRows());
            medications.ShowDialog();
        }

        private void SetButtonsVisibility()
        {
            if (!mapObject.ContainsEquipment() || IsPatientLogged()) Equipment.Visibility = Visibility.Hidden;
            if (!mapObject.ContainsMedication() || IsPatientLogged()) Medication.Visibility = Visibility.Hidden;
        }

        private Boolean IsPatientLogged()
        {
            if (HospitalMap.role == Role.Patient) return true;
            return false;
        }

    }
}
