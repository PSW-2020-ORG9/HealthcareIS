using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Service;
using System.Linq;
using HealthcareBase.Model.Schedule.SchedulingPreferences;
using HealthcareBase.Dto;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for SearchResultDialog.xaml
    /// </summary>
    public partial class SearchResultDialog : Window
    {
        private MapObjectController mapObjectController = new MapObjectController();

        private List<Button> advancedSearchButtons = new List<Button>();
        private HospitalMap hospitalMap;
        private Dictionary<int, MapObject> row;
        private int firstContentRowNumber = 0;
        public static int selectedObjectId = -1;
        private String[] contentRows;
        private Grid DynamicGrid;
        private const int STORAGEROOM_ID = 17;

        public SearchResultDialog(HospitalMap hospitalMap, SearchType searchType)
        {
            InitializeComponent();
            this.row = new Dictionary<int, MapObject>();
            this.Height = AllConstants.SearchDialogHeight;
            this.hospitalMap = hospitalMap;
            ShowDynamicGrid(searchType);
            SetContentRowsAndColumnsNumber(searchType);
            DefineDynamicGrid();        
        }

        private void ShowDynamicGrid(SearchType searchType)
        {
            if (searchType == SearchType.MapObjectSearch)
            {
                MapObjectGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicMapObjectGrid;
            }
            else if (searchType == SearchType.EquipmentSearch || searchType == SearchType.MedicationSearch)
            {
                EquipmentAndMedicationGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicEquipmentAndMedicationGrid;

            }
            else if (searchType == SearchType.AppointmentSearch)
            {
                AppointmentGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicAppointmentGrid;
            }
            else 
            {
                return;
            }
            scrollViewer.Content = DynamicGrid;

        }
        private void DefineDynamicGrid()
        {
            createRows();
            foreach (String oneRow in contentRows)
            {
                String[] oneRowContents = oneRow.Split(AllConstants.contentSeparator);
                createRowData(oneRowContents);
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
            for(int i = 0; i < oneRowContents.Length; i++)
            {
                Label label = new Label();
                label.Content = oneRowContents[i];
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(label, firstContentRowNumber);
                Grid.SetColumn(label, i);
                DynamicGrid.Children.Add(label);
            }
        }

        private void addAdvancedSearchButton()          
        {
            Button advancedSearchBtn = new Button();
            adjustAdvancedSearchButtonProperties(advancedSearchBtn);
            advancedSearchButtons.Add(advancedSearchBtn);

            Grid.SetRow(advancedSearchBtn, firstContentRowNumber);
            Grid.SetColumn(advancedSearchBtn, DynamicGrid.ColumnDefinitions.Count - 1);

            DynamicGrid.Children.Add(advancedSearchBtn);
            advancedSearchBtn.Click += (s, e) =>
            {
                
                if (row.ContainsKey(Grid.GetRow(advancedSearchBtn)))
                {
                    MapObject chosenMapObject = row[Grid.GetRow(advancedSearchBtn)];
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
                        List<MapObject> chosenBuilding = findMapObjectsInBuilding(building);
                        displayBuildingAndFloorBasedOnSelectedObject(chosenBuilding, int.Parse(floor), int.Parse(building));
                        
                        hospitalMap.Hide();
                        this.Close();
                    }
                }
            };
        }
        
        public void displayBuildingAndFloorBasedOnSelectedObject(List<MapObject> chosenBuilding,int  floor, int building)
        {
            Building buildingFromSearch = new Building(chosenBuilding, floor);
            Building.canvasBuilding.Children.Clear();
            CanvasService.addObjectToCanvas(getObjects(building.ToString(), floor.ToString()), Building.canvasBuilding);
            buildingFromSearch.Owner = hospitalMap;
            buildingFromSearch.Show();
        }

        public List<MapObject> findMapObjectsInBuilding(String building)
        {
            Tuple<String, String> buildingAndFloorIteration;
            String buildingId = "";
            List<MapObject> buildingObjects = new List<MapObject>();
            foreach (MapObject mapObjectIterate in mapObjectController.getAllMapObjects())
            {
                buildingAndFloorIteration = getBuildingAndFloor(mapObjectIterate);
                if (buildingAndFloorIteration != null)
                {
                    buildingId = buildingAndFloorIteration.Item1;
                    if (buildingId.Equals(building))
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
            Grid.SetColumn(separator, 0);
            Grid.SetColumnSpan(separator, DynamicGrid.ColumnDefinitions.Count);
            DynamicGrid.Children.Add(separator);
        }

        private void createOneRow(int height)
        {
            RowDefinition gridRow = new RowDefinition();
            gridRow.Height = new GridLength(height);
            DynamicGrid.RowDefinitions.Add(gridRow);
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
                    contentRows = MapObjectToContentRows();
                    break;
                case SearchType.EquipmentSearch:
                    contentRows = EquipmentToContentRows();
                    break;
                case SearchType.MedicationSearch:
                    contentRows = MedicationToContentRows();
                    break;
                case SearchType.AppointmentSearch:
                    contentRows = AppointmentToContentRows();
                    break;
                default:
                    break;
            }
        }

        private string[] EquipmentToContentRows()
        {
            string[] contentRows = new string[HospitalMap.equipmentSearchResult.Count()];
            for (int i = 0; i < HospitalMap.equipmentSearchResult.Count(); i++)
            {
                EquipmentDto equipmentDto = HospitalMap.equipmentSearchResult.ElementAt(i);
                MapObject mo = mapObjectController.findMapObjectById(equipmentDto.RoomId);
                contentRows[i] = equipmentDto.Quantity 
                                 + AllConstants.contentSeparator +
                                 MapObjectToRow(mo);
                                 row.Add(i, mo);
            }
            return contentRows;
        }
        private string[] MedicationToContentRows()
        {
            string[] contentRows = new string[HospitalMap.medicationSearchResult.Count()];
            MapObject mo = mapObjectController.findMapObjectById(STORAGEROOM_ID);
            for (int i = 0; i < HospitalMap.medicationSearchResult.Count(); i++)
            {
                MedicationDto medicationDto = HospitalMap.medicationSearchResult.ElementAt(i);
                contentRows[i] = medicationDto.Quantity
                                 + AllConstants.contentSeparator +
                                 MapObjectToRow(mo);
                                 row.Add(i, mo);
            }
            return contentRows;
        }

        private string MapObjectToRow(MapObject mo)
        {
            string result = mo.Name + AllConstants.contentSeparator
                            + Building.findBuilding(mo)
                            + AllConstants.contentSeparator + Building.findFloor(mo);
            return result;
        }

        private string[] MapObjectToContentRows()
        {
            string[] contentRows = new string[HospitalMap.searchResult.Count()];
            for (int i = 0; i < HospitalMap.searchResult.Count(); i++)
            {
                MapObject mo = HospitalMap.searchResult.ElementAt(i);
                contentRows[i] = MapObjectToRow(mo);
                row.Add(i, mo);
            }
            return contentRows;
        }

        private string[] AppointmentToContentRows()
        {
            string[] contentRows = new string[HospitalMap.appointmentSearchResult.Count()];
            for (int i = 0; i < HospitalMap.appointmentSearchResult.Count(); i++)
            {
                RecommendationDto recommendationDto = HospitalMap.appointmentSearchResult.ElementAt(i);
                MapObject mo = mapObjectController.findMapObjectById(recommendationDto.RoomId);
                string doctor = recommendationDto.Doctor.Person.Name + " " + recommendationDto.Doctor.Person.Surname;
                string timeInterval = recommendationDto.TimeInterval.Start.ToString() + "-" + recommendationDto.TimeInterval.End.ToString();
                contentRows[i] = mo.Name
                                + AllConstants.contentSeparator + doctor
                                 + AllConstants.contentSeparator + timeInterval;
                row.Add(i, mo);
            }
            return contentRows;
        }
    }
}
