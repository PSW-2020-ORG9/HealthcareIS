using System.Windows;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Controller;
using System;
using WPFHospitalEditor.StrategyPattern;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for AdditionalInformation.xaml
    /// </summary>
    public partial class AdditionalInformation : Window
    {
        private readonly MapObject mapObject;
        private DynamicGridControl dynamicGridControl;

        public AdditionalInformation(MapObject mapObject)
        {
            InitializeComponent();
            this.mapObject = mapObject;
            SetDynamicGrid();
            InitializeTitle();
            SetButtonsVisibility();
            if (!LoggedUser.RoleEquals(Role.Director)) Renovation.Visibility = Visibility.Hidden;
        }

        private void SetDynamicGrid()
        {
            dynamicGridControl = new DynamicGridControl(mapObject.MapObjectDescription.GetInformation(), IsPatientLogged());
            DynamicGrid.Children.Add(this.dynamicGridControl);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DoneClick(object sender, RoutedEventArgs e)
        {
            mapObject.MapObjectDescription.Information = dynamicGridControl.GetAllContent();
            mapObject.Name = this.Title.Text;
            mapObject.nameOnMap.Text = mapObject.Name;
            UpdateAdditionalInformation();
            this.Close();
        }

        private void UpdateAdditionalInformation()
        {
            IMapObjectController mapObjectController = new MapObjectController();
            mapObjectController.Update(mapObject);
            
        }

        private void InitializeTitle()
        {
            Title.Text = mapObject.Name;
            if (IsPatientLogged())
                Title.IsReadOnly = true;
        }

        private void BtnEquipmentClick(object sender, RoutedEventArgs e)
        {
            IContentRowsStrategy strategy = new ContentRowsStrategy(new EquipmentContentRows(mapObject.Id));
            EquipmentAndMedicationDialog equipment = new EquipmentAndMedicationDialog(strategy.GetContentRows());
            equipment.ShowDialog();
            this.Close();
        }
        private void BtnMedicationsClick(object sender, RoutedEventArgs e)
        {
            IContentRowsStrategy strategy = new ContentRowsStrategy(new MedicationContentRows(mapObject.Id));
            EquipmentAndMedicationDialog medications = new EquipmentAndMedicationDialog(strategy.GetContentRows());
            medications.ShowDialog();
            this.Close();
        }

        private void BtnRenovationClick(object sender, RoutedEventArgs e)
        {
            RoomRenovation roomRenovationWindow = new RoomRenovation(mapObject.Id);
            roomRenovationWindow.ShowDialog();
            this.Close();
        }

        private void SetButtonsVisibility()
        {
            if (!mapObject.ContainsEquipment() || IsPatientLogged()) Equipment.Visibility = Visibility.Hidden;
            if (!mapObject.ContainsMedication() || IsPatientLogged()) Medication.Visibility = Visibility.Hidden;
        }

        private Boolean IsPatientLogged()
        {
            return LoggedUser.RoleEquals(Role.Patient);
        }
    }
}
