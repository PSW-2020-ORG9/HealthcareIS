﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Service;
using System.Linq;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for SearchResultDialog.xaml
    /// </summary>
    public partial class SearchResultDialog : Window
    {
        MapObjectController mapObjectController = new MapObjectController();

        List<Button> advanceSearchButtons = new List<Button>();
        private HospitalMap hospitalMap;

        private Dictionary<int, MapObject> row = new Dictionary<int, MapObject>();
        int firstContentRowNumber = 2;
        public static int selectedObjectId = -1;
        private int columnsNumber;

        String[] contentRows;


        public SearchResultDialog(HospitalMap hospitalMap, SearchType searchType)
        {
            this.Height = AllConstants.SearchDialogHeight;
            InitializeComponent();
            SetContentRowsAndColumnsNumber(searchType);
            DefineDynamicGrid();
            this.hospitalMap = hospitalMap;
        }

        private void DefineDynamicGrid()
        {
            createRows();
            createRowContent();
            scrollViewer.Content = DynamicGrid;
        }

        private void createRowContent()
        {
            row.Clear();
            int key = 2;
            foreach (String oneRow in contentRows)
            {
                String []oneRowContents = oneRow.Split(AllConstants.contentSeparator);
                createRowData(oneRowContents);
                row.Add(key, mapObjectController.findMapObjectById(int.Parse(oneRowContents[4])));
                key++;
            }
        }    

        private void createRowData(string[] oneRowContents)
        {
            addLabels(oneRowContents);
            addAdvancedSearchButton();
            addSeparator();
            firstContentRowNumber++;
        }

        private void addLabels(string[] oneRowContents)
        {
            for(int i = 1; i <= columnsNumber; i++)
            {
                Label label = new Label();
                adjustLabelProperties(oneRowContents, label, i);

                Grid.SetRow(label, firstContentRowNumber);
                Grid.SetColumn(label, i);

                DynamicGrid.Children.Add(label);
            }
        }

        private void adjustLabelProperties(string[] oneRowContents, Label label, int i)
        {
            if(columnsNumber == 2)
            {
                i = i + 3;
            }
            switch(i)
            {
                case 1:
                case 4:
                    {
                        label.Content = oneRowContents[0];
                    }
                    break;
                case 2:
                    {
                        label.Content = oneRowContents[2];
                    }
                    break;
                case 3:
                    {
                        label.Content = oneRowContents[3];
                    }
                    break;
                case 5:
                    {
                        label.Content = oneRowContents[1];
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
                        displayBuildingAndFloorBasedOnSelectedObject(chosenBuilding, int.Parse(floor), HospitalMap.role, int.Parse(building));
                        
                        hospitalMap.Hide();
                        this.Close();
                    }
                }
            };
        }
        
        public void displayBuildingAndFloorBasedOnSelectedObject(List<MapObject> chosenBuilding,int  floor, Role role, int building)
        {
            Building buildingFromSearch = new Building(chosenBuilding, floor);
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

        private void createOneRow(int height)
        {
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(height);
            DynamicGrid.RowDefinitions.Add(gridRow1);
        }

        private void createRows()
        {
            for (int i = 0; i < contentRows.Count(); i++)
            {
                createOneRow(50);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            selectedObjectId = -1;
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

        private void SetContentRowsAndColumnsNumber(SearchType searchType)
        {
            switch (searchType)
            {
                case SearchType.MapObjectSearch:
                    {
                        setWindowForMapObjects();
                    }
                    break;
                case SearchType.EquipmentSearch:
                    {
                        setWindowForEquipment();
                    }
                    break;
                default:
                    break;
            }
        }

        private void setWindowForMapObjects()
        {
            columnsNumber = 3;
            contentRows = MapObjectToContentRows();
            FirstColumnHeader.Content = "Name";
            SecondColumnHeader.Content = "Building";
            ThirdColumnHeader.Content = "Floor";
        }

        private void setWindowForEquipment()
        {
            columnsNumber = 2;
            contentRows = EquipmentToContentRows();
            FirstColumnHeader.Content = "Name";
            SecondColumnHeader.Content = "Amount";
        }

        private string[] EquipmentToContentRows()
        {
            string[] contentRows = new string[HospitalMap.equipmentSearchResult.Count()];
            for (int i = 0; i < HospitalMap.equipmentSearchResult.Count(); i++)
            {
                MapObject mo = mapObjectController.findMapObjectById(HospitalMap.equipmentSearchResult.ElementAt(i).RoomId);
                contentRows[i] = mo.Name + AllConstants.contentSeparator 
                    + HospitalMap.equipmentSearchResult.ElementAt(i).Quantity 
                    + AllConstants.contentSeparator + Building.findBuilding(mo) 
                    + AllConstants.contentSeparator + Building.findFloor(mo) 
                    + AllConstants.contentSeparator + mo.Id;
            }
            return contentRows;
        }

        private string[] MapObjectToContentRows()
        {
            string[] contentRows = new string[HospitalMap.searchResult.Count()];
            for (int i = 0; i < HospitalMap.searchResult.Count(); i++)
            {
                contentRows[i] = HospitalMap.searchResult.ElementAt(i).Name + AllConstants.contentSeparator 
                    + "0"
                    + AllConstants.contentSeparator + Building.findBuilding(HospitalMap.searchResult.ElementAt(i)) 
                    + AllConstants.contentSeparator + Building.findFloor(HospitalMap.searchResult.ElementAt(i)) 
                    + AllConstants.contentSeparator + HospitalMap.searchResult.ElementAt(i).Id;
            }
            return contentRows;
        }
    }
}
