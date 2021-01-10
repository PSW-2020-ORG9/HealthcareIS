using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Controller;
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
        private HospitalMap hospitalMap;
        private DynamicGridControl dynamicGridControl;

        public AdditionalInformation(MapObject mapObject, Building building, HospitalMap hospitalMap)
        {
            InitializeComponent();
            this.mapObject = mapObject;
            this.building = building;
            this.hospitalMap = hospitalMap;
            SetDynamicGrid();
            InitializeTitle();
            SetButtonsVisibility();
        }

        private void SetDynamicGrid()
        {
            dynamicGridControl = new DynamicGridControl(mapObject.MapObjectDescription.GetInformation(), IsPatientLogged());
            DynamicGrid.Children.Add(this.dynamicGridControl);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            mapObject.MapObjectDescription.Information = dynamicGridControl.GetAllContent();
            mapObject.Name = this.Title.Text;
            mapObject.nameOnMap.Text = mapObject.Name;
            UpdateAdditionalInformation();
            this.Close();
        }

        private void RefreshMap()
        {
            building.canvas.Children.Clear();
            Floor floor = building.GetBuildingFloor(mapObject.MapObjectDescription.FloorNumber);
            CanvasService.AddObjectToCanvas(floor.GetAllFloorMapObjects(), building.canvas);
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
                Title.IsReadOnly = true;
        }

        private void BtnEquipment_Click(object sender, RoutedEventArgs e)
        {
            ContentRowsStrategy strategy = new ContentRowsStrategy(new EquipmentContentRows(mapObject.Id));
            EquipmentAndMedicationWindow equipment = new EquipmentAndMedicationWindow(strategy.GetContentRows(), mapObject.Id, hospitalMap);
            equipment.Show();
            this.Close();
        }
        private void BtnMedications_Click(object sender, RoutedEventArgs e)
        {
            ContentRowsStrategy strategy = new ContentRowsStrategy(new MedicationContentRows(mapObject.Id));
            EquipmentAndMedicationWindow medications = new EquipmentAndMedicationWindow(strategy.GetContentRows(), mapObject.Id, hospitalMap);
            medications.Show();
            this.Close();
        }

        private void SetButtonsVisibility()
        {
            if (!mapObject.ContainsEquipment() || IsPatientLogged()) Equipment.Visibility = Visibility.Hidden;
            if (!mapObject.ContainsMedication() || IsPatientLogged()) Medication.Visibility = Visibility.Hidden;
        }

        private Boolean IsPatientLogged()
        {
            return (HospitalMap.role == Role.Patient);
        }
    }
}
