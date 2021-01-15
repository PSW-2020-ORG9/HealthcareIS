using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Service;
using System.Windows.Media;
using WPFHospitalEditor.Pages;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.UserControls;

namespace WPFHospitalEditor.Pages
{
    /// <summary>
    /// Interaction logic for BuildingPage.xaml
    /// </summary>
    public partial class BuildingPage : Page
    {
        private Dictionary<int, Floor> buildingFloors = new Dictionary<int, Floor>();
        MapObjectController mapObjectController = new MapObjectController();
        public static Canvas canvasBuilding;
        private int id;

        public BuildingPage(int id, int selectedFloor = 0)
        {
            InitializeComponent();
            ClearAll();
            this.id = id;
            PopulateBuildingFloors();
            SetFloorComboBox();
            floor.SelectedIndex = selectedFloor;
            canvasBuilding = canvas;
        }

        public Floor GetBuildingFloor(int floorNumber)
        {
            return buildingFloors[floorNumber];
        }
        private void PopulateBuildingFloors()
        {
            foreach (MapObject mapObject in mapObjectController.GetAllBuildingMapObjects(id))
            {
                int index = mapObject.MapObjectDescription.FloorNumber;
                if (!buildingFloors.ContainsKey(index))
                    buildingFloors.Add(index, new Floor());

                if (IsMapObjectSelected(mapObject.Id))
                    mapObject.rectangle.Fill = Brushes.Red;

                buildingFloors[index].AddMapObject(mapObject);
            }
        }

        private bool IsMapObjectSelected(int id)
        {
            return SearchResultDialog.selectedObjectId == id;
        }

        private void SetFloorComboBox()
        {
            for (int i = 0; i < buildingFloors.Count; i++)
                floor.Items.Add(i + ". floor");
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            ClearAll();
            CanvasService.AddObjectToCanvas(mapObjectController.GetOutterMapObjects(), HospitalMapPage.canvasHospitalMap);
            HospitalMainWindow window = HospitalMainWindow.GetInstance();
            window.ChangePage(new HospitalMapPage());
        }

        private void FloorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearAll();
            int index = floor.SelectedIndex;
            if (buildingFloors.Count == 0) return;
            CanvasService.AddObjectToCanvas(buildingFloors[index].GetAllFloorMapObjects(), canvas);
            legend.Children.Add(new LegendUC(buildingFloors[index].GetAllFloorMapObjects()));
        }

        private void ClearAll()
        {
            if (legend == null) return;
            canvas.Children.Clear();
            legend.Children.Clear();
        }

        private void SelectMapObject(object sender, MouseButtonEventArgs e)
        {
            int index = floor.SelectedIndex;
            MapObject chosenMapObject = CanvasService.CheckWhichObjectIsClicked(e, buildingFloors[index].GetAllFloorMapObjects(), this.canvas);
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
