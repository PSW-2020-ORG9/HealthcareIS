using System;
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
        private MapObjectController mapObjectController = new MapObjectController();

        private List<Button> advancedSearchButtons = new List<Button>();
        private HospitalMap hospitalMap;

        private Dictionary<int, MapObject> row = new Dictionary<int, MapObject>();
        private int firstContentRowNumber = 2;
        public static int selectedObjectId = -1;
        private int columnsNumber;
        private SearchType searchType;

        private String[] contentRows;


        private const int COL_NAME = 1;
        private const int COL_AMOUNT = 0;
        private const int COL_BUILDING = 2;
        private const int COL_FLOOR = 3;
        private const int COL_ID = 4;
        private const int STORAGEROOM_ID = 17;
        private const int COL_DOCTOR = 5;
        private const int COL_TIMEINTERVAL = 6;


        public SearchResultDialog(HospitalMap hospitalMap, SearchType searchType)
        {
            this.Height = AllConstants.SearchDialogHeight;
            this.searchType = searchType;
            InitializeComponent();
            SetContentRowsAndColumnsNumber();
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
                row.Add(key, mapObjectController.findMapObjectById(int.Parse(oneRowContents[COL_ID])));
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
            if(searchType == SearchType.EquipmentSearch || searchType == SearchType.MedicationSearch)
            {
                i = i + 3;
            }
            else if (searchType == SearchType.AppointmentSearch)
            {
                i = i + 5;
            }
            switch (i)
            {
                case 6:
                case 1:
                case 4:
                    {
                        label.Content = oneRowContents[COL_NAME];
                    }
                    break;
                case 2:
                    {
                        label.Content = oneRowContents[COL_BUILDING];
                    }
                    break;
                case 3:
                    {
                        label.Content = oneRowContents[COL_FLOOR];
                    }
                    break;
                case 5:
                    {
                        label.Content = oneRowContents[COL_AMOUNT];
                    }
                    break;
                case 7:
                    {
                        label.Content = oneRowContents[COL_DOCTOR];
                    }
                    break;
                case 8:
                    {
                        label.Content = oneRowContents[COL_TIMEINTERVAL];
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

            advancedSearchButtons.Add(advancedSearch);

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

        private void SetContentRowsAndColumnsNumber()
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
                        setWindowForEquipmentAndMedication();
                        contentRows = EquipmentToContentRows();
                    }
                    break;
                case SearchType.MedicationSearch:
                    {
                        setWindowForEquipmentAndMedication();
                        contentRows = MedicationToContentRows();
                    }
                    break;
                case SearchType.AppointmentSearch:
                    {
                        setWindowForAppointmentSearch();
                        contentRows = AppointmentToContentRows();
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

        private void setWindowForEquipmentAndMedication()
        {
            columnsNumber = 2;
            FirstColumnHeader.Content = "Name";
            SecondColumnHeader.Content = "Amount";
        }

        private void setWindowForAppointmentSearch()
        {
            columnsNumber = 3;
            FirstColumnHeader.Content = "Room";
            SecondColumnHeader.Content = "Doctor";
            ThirdColumnHeader.Content = "Time interval";
        }

        private string[] EquipmentToContentRows()
        {
            string[] contentRows = new string[HospitalMap.equipmentSearchResult.Count()];
            for (int i = 0; i < HospitalMap.equipmentSearchResult.Count(); i++)
            {
                MapObject mo = mapObjectController.findMapObjectById(HospitalMap.equipmentSearchResult.ElementAt(i).RoomId);
                contentRows[i] = HospitalMap.equipmentSearchResult.ElementAt(i).Quantity 
                                 + AllConstants.contentSeparator +
                                 MapObjectToRow(mo);
            }
            return contentRows;
        }
        private string[] MedicationToContentRows()
        {
            string[] contentRows = new string[HospitalMap.medicationSearchResult.Count()];
            MapObject mo = mapObjectController.findMapObjectById(STORAGEROOM_ID);
            for (int i = 0; i < HospitalMap.medicationSearchResult.Count(); i++)
            {
                contentRows[i] = HospitalMap.medicationSearchResult.ElementAt(i).Quantity
                                 + AllConstants.contentSeparator +
                                 MapObjectToRow(mo);
            }
            return contentRows;
        }

        private string MapObjectToRow(MapObject mo)
        {
            string result = mo.Name + AllConstants.contentSeparator
                            + Building.findBuilding(mo)
                            + AllConstants.contentSeparator + Building.findFloor(mo)
                            + AllConstants.contentSeparator + mo.Id;
            return result;
        }

        private string[] MapObjectToContentRows()
        {
            string[] contentRows = new string[HospitalMap.searchResult.Count()];
            for (int i = 0; i < HospitalMap.searchResult.Count(); i++)
            {
                MapObject mo = HospitalMap.searchResult.ElementAt(i);
                contentRows[i] = "0" + AllConstants.contentSeparator
                                  + MapObjectToRow(mo);
            }
            return contentRows;
        }

        private string[] AppointmentToContentRows()
        {
            string[] contentRows = new string[HospitalMap.appointemntSearchResult.Count()];
            for (int i = 0; i < HospitalMap.appointemntSearchResult.Count(); i++)
            {
                MapObject mo = mapObjectController.findMapObjectById(HospitalMap.appointemntSearchResult.ElementAt(i).RoomId);
                string doctor = HospitalMap.appointemntSearchResult.ElementAt(i).Doctor.Person.Name + " " + HospitalMap.appointemntSearchResult.ElementAt(i).Doctor.Person.Surname;
                string timeInterval = HospitalMap.appointemntSearchResult.ElementAt(i).TimeInterval.Start.ToString() + "-" + HospitalMap.appointemntSearchResult.ElementAt(i).TimeInterval.End.ToString();
                contentRows[i] = "0"
                                 + AllConstants.contentSeparator
                                 + MapObjectToRow(mo)
                                 + AllConstants.contentSeparator + doctor
                                 + AllConstants.contentSeparator + timeInterval;
            }
            return contentRows;
        }
    }
}
