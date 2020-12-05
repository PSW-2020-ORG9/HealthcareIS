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

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for AdditionalInformation.xaml
    /// </summary>
    public partial class AdditionalInformation : Window
    {
        IMapObjectController mapObjectController = new MapObjectController();
        IEquipmentServerController equipmentServerController = new EquipmentServerController();
        IMedicationServerController medicationServerController = new MedicationServerController();

        private MapObject mapObject;
        private Building building;
        private string[] descriptionParts;
        private string[] contentRows;
        private MapObject oldMapObject;
        private Role role;
        private IEnumerable<EquipmentDto> allEquipment;
        private IEnumerable<MedicationDto> allMedications;
        private DynamicGridControl gridControl;

        public AdditionalInformation(MapObject mapObject, Building building)
        {
            InitializeComponent();
            this.mapObject = mapObject;
            this.building = building;
            this.descriptionParts = mapObject.Description.Split("&");
            this.contentRows = descriptionParts[1].Split(";");
            this.oldMapObject = mapObject;
            this.role = HospitalMap.role;
            this.allEquipment = equipmentServerController.GetEquipmentByRoomId(mapObject.Id);
            this.allMedications = medicationServerController.GetAllMedication();
            DynamicGridControl dynamicGridControl = new DynamicGridControl(contentRows, IsPatientLogged());
            DynamicGrid.Children.Add(dynamicGridControl);         
            this.gridControl = dynamicGridControl;
            this.Height = (contentRows.Length +1) * 50 + 60;       
            SetNameCommonAttributes();
            setButtonsVisibility();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            string description = gridControl.GetAllContent();
            mapObject.Description = descriptionParts[0] + "&" + description;       
            mapObject.Name = this.Name.Text;
            mapObject.nameOnMap.Text = mapObject.Name;
            UpdateAdditionalInformation();
            mapObject.Description = mapObject.Description.Substring(0, mapObject.Description.Length - 1);
            this.Close();
        }

        private void RefreshMap()
        {
            building.floorBuildingObjects.Remove(oldMapObject);
            building.floorBuildingObjects.Add(mapObject);
            building.canvas.Children.Clear();
            CanvasService.addObjectToCanvas(building.floorBuildingObjects, building.canvas);
        }      

        private void UpdateAdditionalInformation()
        {
            mapObjectController.update(mapObject);
            RefreshMap();
        }

        private void SetNameCommonAttributes()
        {
            BrushConverter bc = new BrushConverter();
            Name.Text = mapObject.Name;
            Name.Background = (Brush)bc.ConvertFrom("#FFC6F5F8");
            Name.FontSize = 18;
            Name.FontWeight = FontWeights.Bold;
            Name.Foreground = new SolidColorBrush(Colors.Black);
            Name.VerticalAlignment = VerticalAlignment.Center;
            Name.HorizontalAlignment = HorizontalAlignment.Center;
            if (role.Equals(Role.Patient))
            {
                Name.IsReadOnly = true;
            }
        }

        private void BtnEquipment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentAndMedicationWindow equipment = new EquipmentAndMedicationWindow(EquipmentToContentRows());
            equipment.ShowDialog();
        }
        private void BtnMedications_Click(object sender, RoutedEventArgs e)
        {
            EquipmentAndMedicationWindow medications = new EquipmentAndMedicationWindow(MedicationsToContentRows());
            medications.ShowDialog();
        }

        private string[] EquipmentToContentRows()
        {
            string[] contentRows = new string [allEquipment.Count()];
            for (int i = 0; i < allEquipment.Count(); i++)
            {
                contentRows[i] = allEquipment.ElementAt(i).Name + AllConstants.contentSeparator + allEquipment.ElementAt(i).Quantity;
            }
            return contentRows;
        }

        private string[] MedicationsToContentRows()
        {
            string[] contentRows = new string[allMedications.Count()];
            for (int i = 0; i < allMedications.Count(); i++)
            {
                contentRows[i] = allMedications.ElementAt(i).Name + AllConstants.contentSeparator + allMedications.ElementAt(i).Quantity;
            }
            return contentRows;
        }

        private void setButtonsVisibility()
        {
            if (allEquipment.Count() == 0 || isMapObjectTypeStorageRoom() || IsPatientLogged()) Equipment.Visibility = Visibility.Hidden;
            if (allMedications.Count() == 0 || !isMapObjectTypeStorageRoom() || IsPatientLogged()) Medication.Visibility = Visibility.Hidden;
        }

        private Boolean IsPatientLogged()
        {
            if (role == Role.Patient) return true;
            return false;
        }

        private bool isMapObjectTypeStorageRoom()
        {
            if (mapObject.MapObjectType == MapObjectType.StorageRoom) return true;
            return false;
        } 
    }
}
