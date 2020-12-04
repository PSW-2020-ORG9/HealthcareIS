using HealthcareBase.Dto;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for EquipmentSearchResultDialog.xaml
    /// </summary>
    public partial class EquipmentSearchResultDialog : Window
    {
        List<Button> advanceSearchButtons = new List<Button>();
        private Dictionary<int, MapObject> row = new Dictionary<int, MapObject>();
        int firstContentRowNumber = 2;
        IMapObjectController mapObjectController = new MapObjectController();
        private HospitalMap hospitalMap;
        private Role role;
        ScrollViewer viewer = new ScrollViewer();

        public EquipmentSearchResultDialog(HospitalMap hospitalMap, Role role)
        {
            this.Height = AllConstants.SearchDialogHeight;
            InitializeComponent();
            DefineDynamicGrid();
            this.role = role;
            this.hospitalMap = hospitalMap;
        }

        private void DefineDynamicGrid()
        {
            createRows();
            createRowContent();
            viewer.Content = DynamicGrid;
            viewer.BorderBrush = Brushes.AliceBlue;
            Border.Child = viewer;
        }

        private void createRowContent()
        {
            row.Clear();
            int key = 2;
            foreach (EquipmentDto equipmentInRoom in HospitalMap.equipmentSearchResult)
            {
                createRowData(equipmentInRoom);
                row.Add(key, mapObjectController.findMapObjectById(equipmentInRoom.RoomId));
                key++;
            }
        }

        private int createRowData(EquipmentDto equipmentInRoom)
        {
            addLabels(equipmentInRoom);

            addAdvancedSearchButton();

            addSeparator();

            firstContentRowNumber++;

            return firstContentRowNumber;
        }

        private void addLabels(EquipmentDto equipmentInRoom)
        {
            for (int i = 1; i <= 2; i++)
            {
                Label label = new Label();

                adjustLabelProperties(equipmentInRoom, label, i);

                Grid.SetRow(label, firstContentRowNumber);
                Grid.SetColumn(label, i);

                DynamicGrid.Children.Add(label);
            }
        }

        private void adjustLabelProperties(EquipmentDto equipmentInRoom, Label label, int i)
        {
            switch (i)
            {
                case 1:
                    {
                        label.Content = mapObjectController.findMapObjectById(equipmentInRoom.RoomId).Name;
                    }
                    break;
                case 2:
                    {
                        label.Content = equipmentInRoom.Quantity;
                    }
                    break;
                default:
                    break;
            }

            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
        }

        private void addAdvancedSearchButton()
        {
            Button advancedSearch = new Button();

            adjustAdvancedSearchButtonProperties(advancedSearch);

            advanceSearchButtons.Add(advancedSearch);

            Grid.SetRow(advancedSearch, firstContentRowNumber);
            Grid.SetColumn(advancedSearch, 3);

            DynamicGrid.Children.Add(advancedSearch);
            advancedSearch.Click += (s, e) =>
            {

                if (row.ContainsKey(Grid.GetRow(advancedSearch)))
                {
                    MapObject chosenMapObject = row[Grid.GetRow(advancedSearch)];
                    SearchResultDialog.selectedObjectId = chosenMapObject.Id;

                    String building = getBuildingAndFloor(chosenMapObject).Item1;
                    String floor = getBuildingAndFloor(chosenMapObject).Item2;
                    List<MapObject> chosenBuilding = findBuilding(building);
                    displayBuildingAndFloorBasedOnSelectedObject(chosenBuilding, int.Parse(floor), role, int.Parse(building));

                    hospitalMap.Hide();
                    this.Close();
                }
            };
        }

        public void displayBuildingAndFloorBasedOnSelectedObject(List<MapObject> chosenBuilding, int floor, Role role, int building)
        {
            Building buildingFromSearch = new Building(chosenBuilding, floor, role);
            Building.canvasBuilding.Children.Clear();
            CanvasService.addObjectToCanvas(getObjects(building.ToString(), floor.ToString()), Building.canvasBuilding);
            buildingFromSearch.Owner = hospitalMap;
            buildingFromSearch.Show();
        }
        public List<MapObject> findBuilding(String building)
        {
            Tuple<String, String> buildingAndFloorIteration;
            String buildingIterate = "";
            List<MapObject> buildingObjects = new List<MapObject>();
            foreach (MapObject mapObjectIterate in mapObjectController.getAllMapObjects())
            {
                buildingAndFloorIteration = getBuildingAndFloor(mapObjectIterate);
                if (buildingAndFloorIteration != null)
                {
                    buildingIterate = buildingAndFloorIteration.Item1;
                    if (buildingIterate.Equals(building))
                    {
                        buildingObjects.Add(mapObjectIterate);
                    }
                }
            }
            return buildingObjects;
        }

        private static void adjustAdvancedSearchButtonProperties(Button advancedSearch)
        {
            advancedSearch.HorizontalAlignment = HorizontalAlignment.Center;
            advancedSearch.VerticalAlignment = VerticalAlignment.Center;
            advancedSearch.Content = "+";
            advancedSearch.FontSize = 35;
            advancedSearch.Foreground = Brushes.White;
            advancedSearch.Background = Brushes.SkyBlue;
            advancedSearch.FontWeight = FontWeights.UltraBold;
            advancedSearch.Width = 35;
            advancedSearch.Height = 35;
            advancedSearch.HorizontalContentAlignment = HorizontalAlignment.Center;
            advancedSearch.VerticalContentAlignment = VerticalAlignment.Top;
            advancedSearch.BorderThickness = new Thickness(0);
            advancedSearch.Padding = new Thickness(0, -10, 0, 0);
        }

        private void addSeparator()
        {
            Separator separator = new Separator();

            separator.VerticalAlignment = VerticalAlignment.Bottom;

            Grid.SetRow(separator, firstContentRowNumber);
            Grid.SetColumn(separator, 1);
            Grid.SetColumnSpan(separator, 3);

            DynamicGrid.Children.Add(separator);
        }

        private void createOneRow(int height)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(height);
            DynamicGrid.RowDefinitions.Add(gridRow1);
        }

        private void createRows()
        {
            for (int i = 0; i < HospitalMap.equipmentSearchResult.Count; i++)
            {
                createOneRow(40);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SearchResultDialog.selectedObjectId = -1;
            CanvasService.addObjectToCanvas(mapObjectController.getOutterMapObjects(), HospitalMap.canvasHospitalMap);
            Close();
        }
        private Tuple<String, String> getBuildingAndFloor(MapObject mapObjectCheck)
        {
            if (!mapObjectCheck.Description.Equals(""))
            {
                String[] buildingAndFloor = mapObjectCheck.Description.Split("&");
                String[] buildingAndFloorSplited = buildingAndFloor[0].Split("-");
                return Tuple.Create(buildingAndFloorSplited[0], buildingAndFloorSplited[1]);
            }

            return null;
        }

        private List<MapObject> getObjects(String building, String floor)
        {
            List<MapObject> objectsToDisplay = new List<MapObject>();
            List<MapObject> allMapObjects = mapObjectController.getAllMapObjects();
            foreach (MapObject mapObjectIteration in allMapObjects)
            {
                if (isBuildingAndFloorEqual(building, floor, mapObjectIteration))
                {
                    objectsToDisplay.Add(mapObjectIteration);
                }
            }
            return objectsToDisplay;
        }

        private bool isBuildingAndFloorEqual(String building, String floor, MapObject mapObjectForChecking)
        {

            Tuple<String, String> buildingAndFloorForChecking = getBuildingAndFloor(mapObjectForChecking);

            if (buildingAndFloorForChecking != null)
            {
                String buildingForChecking = buildingAndFloorForChecking.Item1;
                String floorForChecking = buildingAndFloorForChecking.Item2;

                if (building.Equals(buildingForChecking) && floor.Equals(floorForChecking))
                {
                    return true;
                }
            }
            return false;
        }
    }
}