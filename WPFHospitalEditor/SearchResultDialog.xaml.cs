using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for SearchResultDialog.xaml
    /// </summary>
    public partial class SearchResultDialog : Window
    {
        List<Button> advanceSearchButtons = new List<Button>();
        private Dictionary<int, MapObject> row = new Dictionary<int, MapObject>();
        int firstContentRowNumber = 2;
        MapObjectController mapObjectController = new MapObjectController();
        private HospitalMap hospitalMap;
        private Role role;
        public static int selectedObjectId = -1;

        public SearchResultDialog(HospitalMap hospitalMap, Role role)
        {
            this.Height = (HospitalMap.searchResult.Count + 1) * 50 + 10;
            InitializeComponent();
            DefineDynamicGrid();
            this.role = role;
            this.hospitalMap = hospitalMap;
        }

        private void DefineDynamicGrid()
        {
            createRows();
            createRowContent();
            Border.Child = DynamicGrid;
        }

        private void createRowContent()
        {
            row.Clear();
            int key = 2;
            foreach (MapObject mapObject in HospitalMap.searchResult)
            {
                if (mapObject.MapObjectType.Equals(MapObjectType.Building))
                {
                    createBuildingRowData(mapObject);
                }
                else
                {
                    createRowData(mapObject);                 
                }
                row.Add(key, mapObject);
                key++;
            }
        }    

        private void createRowData(MapObject mapObject)
        {
            addLabels(mapObject);

            addAdvancedSearchButton();

            addSeparator();

            firstContentRowNumber++;

        }

        private void addLabels(MapObject mapObject)
        {
            for(int i = 1; i <= 3; i++)
            {
                Label label = new Label();

                adjustLabelProperties(mapObject, label, i);

                Grid.SetRow(label, firstContentRowNumber);
                Grid.SetColumn(label, i);

                DynamicGrid.Children.Add(label);
            }
        }

        private void adjustLabelProperties(MapObject mapObject, Label label, int i)
        {
            switch(i)
            {
                case 1:
                    {
                        label.Content = mapObject.Name;
                    }
                    break;
                case 2:
                    {
                        label.Content = Building.findBuilding(mapObject);
                    }
                    break;
                case 3:
                    {
                        label.Content = Building.findFloor(mapObject);
                    }
                    break;
                default:
                    break;
            }

            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
        }

        private void addNameLabel(MapObject mapObject)
        {
            Label name = new Label();

            adjustNameLabelProperties(mapObject, name);

            Grid.SetRow(name, firstContentRowNumber);
            Grid.SetColumn(name, 1);

            DynamicGrid.Children.Add(name);
        }

        private static void adjustNameLabelProperties(MapObject mapObject, Label name)
        {
            name.Content = mapObject.Name;
            name.HorizontalAlignment = HorizontalAlignment.Center;
            name.VerticalAlignment = VerticalAlignment.Center;
        }

        private void addAdvancedSearchButton()          
        {
            Button advancedSearch = new Button();

            adjustAdvancedSearchButtonProperties(advancedSearch);

            advanceSearchButtons.Add(advancedSearch);

            Grid.SetRow(advancedSearch, firstContentRowNumber);
            Grid.SetColumn(advancedSearch, 4);

            DynamicGrid.Children.Add(advancedSearch);
            advancedSearch.Click += (s, e) =>
            {
                
                if (row.ContainsKey(Grid.GetRow(advancedSearch)))
                {
                    MapObject chosenMapObject = row[Grid.GetRow(advancedSearch)];
                    selectedObjectId = chosenMapObject.Id;
                    mapObjectController.update(chosenMapObject);

                    if (chosenMapObject.Description.Equals(""))
                    {
                        CanvasService.addObjectToCanvas(mapObjectController.getOutterMapObjects(), HospitalMap.canvasHospitalMap);
                        this.Close();
                    }
                    else
                    {
                        String building = getBuildingAndFloor(chosenMapObject).Item1;
                        String floor = getBuildingAndFloor(chosenMapObject).Item2;
                        List<MapObject> chosenBuilding = findBuilding(building);
                        displayBuildingAndFloorBasedOnSelectedObject(chosenBuilding, int.Parse(floor), role, int.Parse(building));
                        
                        hospitalMap.Hide();
                        this.Close();
                    }
                }
            };
        }
        
        public void displayBuildingAndFloorBasedOnSelectedObject(List<MapObject> chosenBuilding,int  floor, Role role, int building)
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
            Grid.SetColumnSpan(separator, 4);

            DynamicGrid.Children.Add(separator);
        }

        private int createBuildingRowData(MapObject mapObject)
        {
            addNameLabel(mapObject);

            addAdvancedSearchButton();

            addSeparator();

            firstContentRowNumber++;

            return firstContentRowNumber;
        }

        private void createOneRow(int height)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(height);
            DynamicGrid.RowDefinitions.Add(gridRow1);
        }

        private void createRows()
        {
            for (int i = 0; i < HospitalMap.searchResult.Count; i++)
            {
                createOneRow(50);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            selectedObjectId = -1;
            CanvasService.addObjectToCanvas(mapObjectController.getOutterMapObjects(), HospitalMap.canvasHospitalMap);
            HospitalMap.searchResult.Clear();
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
