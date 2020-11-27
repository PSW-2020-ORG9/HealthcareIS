using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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


        public SearchResultDialog(HospitalMap hospitalMap, Role role)
        {
            this.Height = (HospitalMap.searchResult.Count + 1) * 50 + 10;
            InitializeComponent();
            CreateDynamicGrid();
            this.role = role;
            this.hospitalMap = hospitalMap;
        }

        private void CreateDynamicGrid()
        {
            createRows();
            createRowContent();
            Border.Child = DynamicGrid;
        }

        private void createRowContent()
        {
            row.Clear();
            int i = 2;
            foreach (MapObject mapObject in HospitalMap.searchResult)
            {
                if (mapObject.MapObjectType.Equals(MapObjectType.Building))
                {
                    createBuildingRowData(mapObject);
                    row.Add(i, mapObject);
                    i++;
                }
                else
                {
                    createRowData(mapObject);
                    row.Add(i, mapObject);
                    i++;
                }
            }
        }    

        private int createRowData(MapObject mapObject)
        {
            addLabels(mapObject);

            addAdvancedSearchButton();

            addSeparator();

            firstContentRowNumber++;

            return firstContentRowNumber;
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
                    row[Grid.GetRow(advancedSearch)].selected = true;
                    mapObjectController.update(row[Grid.GetRow(advancedSearch)]);
                    if (row[Grid.GetRow(advancedSearch)].Description.Equals(""))
                    {
                        CanvasService.addObjectToCanvas(mapObjectController.getOutterMapObjects(), HospitalMap.canvasHospitalMap);
                        this.Close();
                    }
                    else
                    {
                        string[] split = getBuildingAndFloor(row[Grid.GetRow(advancedSearch)]).Split("-");
                        List<MapObject> chosenBuilding = findBuilding(split[0]);
                        Building buildingFromSearch = new Building(chosenBuilding, int.Parse(split[1]), role);
                        Building.canvasBuilding.Children.Clear();
                        CanvasService.addObjectToCanvas(getObjects(split[0], split[1]), Building.canvasBuilding);
                        buildingFromSearch.Owner = hospitalMap;
                        buildingFromSearch.Show();
                        hospitalMap.Hide();
                        this.Close();
                    }
                }
            };
        }
        public List<MapObject> findBuilding(String building)
        {
            string[] split1;
            string[] split2;
            List<MapObject> buildingObjects = new List<MapObject>();
            foreach (MapObject mo in mapObjectController.getAllMapObjects())
            {
                split1 = mo.Description.Split("&");
                split2 = split1[0].Split("-");
                if (split2[0].Equals(building))
                {
                    buildingObjects.Add(mo);
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
            HospitalMap.searchResult.Clear();
            Close();
        }

        private String getBuildingAndFloor(MapObject mapObjectCheck)
        {
            String[] buildingAndFloor = mapObjectCheck.Description.Split("&");
            return buildingAndFloor[0];
        }

        private List<MapObject> getObjects(String building, String floor)
        {
            List<MapObject> objectsToDisplay = new List<MapObject>();
            List<MapObject> allMapObjects = mapObjectController.getAllMapObjects();
            foreach (MapObject mapObjectIteration in allMapObjects)
            {
                if (checkBuildingAndFloor(building, floor, mapObjectIteration))
                {
                    objectsToDisplay.Add(mapObjectIteration);
                }
            }
            return objectsToDisplay;
        }

        private bool checkBuildingAndFloor(String building, String floor, MapObject mapObjectForChecking)
        {
            string[] split = getBuildingAndFloor(mapObjectForChecking).Split("-");
            if (building.Equals(split[0]) && floor.Equals(split[1]))
            {
                return true;
            }
            return false;
        }
    }
}
