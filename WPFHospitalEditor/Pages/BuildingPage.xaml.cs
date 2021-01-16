using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.UserControls;
using System.Diagnostics;
using System.Windows.Media;

namespace WPFHospitalEditor.Pages
{
    /// <summary>
    /// Interaction logic for BuildingPage.xaml
    /// </summary>
    public partial class BuildingPage : Page
    {
        private Floor floor = new Floor();
        private readonly int id;
        private int selectedFloor; 

        public BuildingPage(int id, int selectedFloor = 0)
        {
            InitializeComponent();
            this.id = id;
            this.selectedFloor = selectedFloor;
            List<MapObject> mapObjectsInBuilding = new MapObjectController().GetAllBuildingMapObjects(id);

            InsertMapObjectsToFloor(mapObjectsInBuilding);
            SetFloorComboBox(mapObjectsInBuilding);
            SetFloorLegend();
            CanvasService.AddObjectToCanvas(floor.GetAllFloorMapObjects(), canvas);
        }

        private void InsertMapObjectsToFloor(List<MapObject> mapObjectsInBuidling)
        {
            foreach (MapObject mapObject in mapObjectsInBuidling)
            {
                int floorNumber = mapObject.MapObjectDescription.FloorNumber;
                if (floorNumber != selectedFloor) continue;

                if (IsMapObjectSelected(mapObject.Id))
                    mapObject.rectangle.Fill = Brushes.Red;
                
                floor.AddMapObject(mapObject);    
            }
        }

        private bool IsMapObjectSelected(int id)
        {
            return SearchResultDialog.selectedObjectId == id;
        }

        private void SetFloorComboBox(List<MapObject> mapObjectsInBuidling)
        {
            InsertFloorsInFloorCmb(mapObjectsInBuidling);
            floorCmb.SelectedIndex = selectedFloor;
        }

        private void InsertFloorsInFloorCmb(List<MapObject> mapObjectsInBuidling)
        {
            HashSet<int> floorNumbers = new HashSet<int>();
            foreach (MapObject mapObject in mapObjectsInBuidling)
            {
                int floorNum = mapObject.MapObjectDescription.FloorNumber;
                if (!floorNumbers.Contains(floorNum))
                {
                    floorNumbers.Add(floorNum);
                    floorCmb.Items.Add(floorNum + ". floor");
                }
            }
        }

        private void SetFloorLegend()
        {
            legend.Children.Add(new LegendUC(floor.GetAllFloorMapObjects()));
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            HospitalMainWindow window = HospitalMainWindow.GetInstance();
            window.ChangePage(new HospitalMapPage());
        }

        private void FloorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (floorCmb.SelectedIndex == selectedFloor) return;
            
            HospitalMainWindow window = HospitalMainWindow.GetInstance();
            window.ChangePage(new BuildingPage(id, floorCmb.SelectedIndex));
                     
        }
        private void SelectMapObject(object sender, MouseButtonEventArgs e)
        {
            MapObject chosenMapObject = CanvasService.CheckWhichObjectIsClicked(e, floor.GetAllFloorMapObjects(), this.canvas);
            if (chosenMapObject != null)
                OpenAdditionalInformationDialog(chosenMapObject);
        }

        private void OpenAdditionalInformationDialog(MapObject mapObject)
        {
            AdditionalInformation additionalInformation = new AdditionalInformation(mapObject, this);
            additionalInformation.ShowDialog();
        }
    }
}
