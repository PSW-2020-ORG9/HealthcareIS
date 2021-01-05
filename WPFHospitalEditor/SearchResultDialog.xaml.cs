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
        private List<Button> scheduleButtons = new List<Button>();
        private HospitalMap hospitalMap;
        private Dictionary<int, MapObject> displayBtnRow;
        private Dictionary<int, RecommendationDto> scheduleBtnRow;
        private SearchType searchType;
        private int firstContentRowNumber = 0;
        public static int selectedObjectId = -1;
        private String[] contentRows;
        private Grid DynamicGrid;
        private const int STORAGEROOM_ID = 17;

        public SearchResultDialog(HospitalMap hospitalMap, SearchType searchType)
        {
            InitializeComponent();
            this.searchType = searchType;
            this.displayBtnRow = new Dictionary<int, MapObject>();
            this.scheduleBtnRow = new Dictionary<int, RecommendationDto>();
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
            CreateRows();
            foreach (String oneRow in contentRows)
            {
                String[] oneRowContents = oneRow.Split(AllConstants.ContentSeparator);
                CreateRowData(oneRowContents);
            }
        }

        private void CreateRowData(string[] oneRowContents)
        {
            AddLabels(oneRowContents);
            AddAdvancedSearchButton();
            if (searchType == SearchType.AppointmentSearch)
            {
                AddScheduleButton();
            }

            AddSeparator();
            firstContentRowNumber++;
        }

        private void AddLabels(string[] oneRowContents)
        {
            for (int i = 0; i < oneRowContents.Length; i++)
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

        private void AddAdvancedSearchButton()
        {
            Button advancedSearchBtn = new Button();
            SetAdvancedSearchButtonProperties(advancedSearchBtn);
            SetCommonButtonProperties(advancedSearchBtn);
            advancedSearchButtons.Add(advancedSearchBtn);

            Grid.SetRow(advancedSearchBtn, firstContentRowNumber);
            Grid.SetColumn(advancedSearchBtn, DynamicGrid.ColumnDefinitions.Count - 1);

            DynamicGrid.Children.Add(advancedSearchBtn);
            advancedSearchBtn.Click += (s, e) =>
            {
                if (displayBtnRow.ContainsKey(Grid.GetRow(advancedSearchBtn)))
                {
                    MapObject chosenMapObject = displayBtnRow[Grid.GetRow(advancedSearchBtn)];
                    selectedObjectId = chosenMapObject.Id;
                    mapObjectController.Update(chosenMapObject);

                    if (chosenMapObject.MapObjectDescription == null)
                    {
                        CanvasService.AddObjectToCanvas(mapObjectController.GetOutterMapObjects(), HospitalMap.canvasHospitalMap);
                        this.Close();
                    }
                    else
                    {
                        DisplayBuildingAndFloorBasedOnSelectedObject(chosenMapObject.MapObjectDescription.FloorNumber, chosenMapObject.MapObjectDescription.BuildingId);

                        hospitalMap.Hide();
                        this.Close();
                    }
                }
            };
        }

        private void AddScheduleButton()
        {
            Button scheduleBtn = new Button();
            SetScheduleButtonProperties(scheduleBtn);
            SetCommonButtonProperties(scheduleBtn);
            scheduleButtons.Add(scheduleBtn);
            Grid.SetRow(scheduleBtn, firstContentRowNumber);
            Grid.SetColumn(scheduleBtn, DynamicGrid.ColumnDefinitions.Count - 2);
            DynamicGrid.Children.Add(scheduleBtn);

            scheduleBtn.Click += (s, e) =>
            {
                if (scheduleBtnRow.ContainsKey(Grid.GetRow(scheduleBtn)))
                {
                    RecommendationDto chosenRecommendation = scheduleBtnRow[Grid.GetRow(scheduleBtn)];
                    ScheduleWindow scheduleWindow = new ScheduleWindow(chosenRecommendation);
                    scheduleWindow.ShowDialog();
                    this.Close();
                }
            };
        }

        public void DisplayBuildingAndFloorBasedOnSelectedObject(int floorNumber, int buildingId)
        {
            Building searchedBuilding = new Building(buildingId, floorNumber);
            searchedBuilding.Owner = hospitalMap;
            searchedBuilding.Show();
        }

        private static void SetAdvancedSearchButtonProperties(Button advancedSearch)
        {
            advancedSearch.Content = "+";
            advancedSearch.FontSize = 35;
            advancedSearch.FontWeight = FontWeights.UltraBold;
            advancedSearch.Width = 35;
            advancedSearch.VerticalContentAlignment = VerticalAlignment.Top;
        }

        private static void SetScheduleButtonProperties(Button scheduleBtn)
        {
            scheduleBtn.Content = "Schedule";
            scheduleBtn.FontSize = 15;
            scheduleBtn.FontWeight = FontWeights.Normal;
            scheduleBtn.Width = 70;
            scheduleBtn.VerticalContentAlignment = VerticalAlignment.Bottom;
        }

        private void SetCommonButtonProperties(Button button)
        {
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Foreground = Brushes.White;
            button.Background = Brushes.SkyBlue;
            button.Height = 35;
            button.HorizontalContentAlignment = HorizontalAlignment.Center;
            button.BorderThickness = new Thickness(0);
            button.Padding = new Thickness(0, -10, 0, 0);
        }

        private void AddSeparator()
        {
            Separator separator = new Separator();
            separator.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(separator, firstContentRowNumber);
            Grid.SetColumn(separator, 0);
            Grid.SetColumnSpan(separator, DynamicGrid.ColumnDefinitions.Count);
            DynamicGrid.Children.Add(separator);
        }

        private void CreateOneRow(int height)
        {
            RowDefinition gridRow = new RowDefinition();
            gridRow.Height = new GridLength(height);
            DynamicGrid.RowDefinitions.Add(gridRow);
        }

        private void CreateRows()
        {
            for (int i = 0; i < contentRows.Count(); i++)
            {
                CreateOneRow(50);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            selectedObjectId = -1;
            CanvasService.AddObjectToCanvas(mapObjectController.GetOutterMapObjects(), HospitalMap.canvasHospitalMap);
            Close();
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
            string[] equipmentContentRows = new string[HospitalMap.equipmentSearchResult.Count()];
            for (int i = 0; i < HospitalMap.equipmentSearchResult.Count(); i++)
            {
                EquipmentDto equipmentDto = HospitalMap.equipmentSearchResult.ElementAt(i);
                MapObject mo = mapObjectController.GetMapObjectById(equipmentDto.RoomId);
                equipmentContentRows[i] = equipmentDto.Quantity
                                 + AllConstants.ContentSeparator +
                                 MapObjectToRow(mo);
                displayBtnRow.Add(i, mo);
            }
            return equipmentContentRows;
        }
        private string[] MedicationToContentRows()
        {
            string[] medicationContentRows = new string[HospitalMap.medicationSearchResult.Count()];
            MapObject mo = mapObjectController.GetMapObjectById(STORAGEROOM_ID);
            for (int i = 0; i < HospitalMap.medicationSearchResult.Count(); i++)
            {
                MedicationDto medicationDto = HospitalMap.medicationSearchResult.ElementAt(i);
                medicationContentRows[i] = medicationDto.Quantity
                                 + AllConstants.ContentSeparator +
                                 MapObjectToRow(mo);
                displayBtnRow.Add(i, mo);
            }
            return medicationContentRows;
        }

        private string MapObjectToRow(MapObject mo)
        {
            string result = mo.Name + AllConstants.ContentSeparator
                            + mo.MapObjectDescription.BuildingId
                            + AllConstants.ContentSeparator + mo.MapObjectDescription.FloorNumber;
            return result;
        }

        private string[] MapObjectToContentRows()
        {
            string[] mapObjectContentRows = new string[HospitalMap.searchResult.Count()];
            for (int i = 0; i < HospitalMap.searchResult.Count(); i++)
            {
                MapObject mo = HospitalMap.searchResult.ElementAt(i);
                mapObjectContentRows[i] = MapObjectToRow(mo);
                displayBtnRow.Add(i, mo);
            }
            return mapObjectContentRows;
        }

        private string[] AppointmentToContentRows()
        {
            string[] appointmentContentRows = new string[HospitalMap.appointmentSearchResult.Count()];
            for (int i = 0; i < HospitalMap.appointmentSearchResult.Count(); i++)
            {
                RecommendationDto recommendationDto = HospitalMap.appointmentSearchResult.ElementAt(i);
                MapObject mo = mapObjectController.GetMapObjectById(recommendationDto.RoomId);
                string doctor = recommendationDto.Doctor.Person.Name + " " + recommendationDto.Doctor.Person.Surname;
                string timeInterval = recommendationDto.TimeInterval.Start.ToString() + "-" + recommendationDto.TimeInterval.End.ToString();
                appointmentContentRows[i] = mo.Name
                                + AllConstants.ContentSeparator + doctor
                                 + AllConstants.ContentSeparator + timeInterval;
                displayBtnRow.Add(i, mo);
                scheduleBtnRow.Add(i, recommendationDto);
            }
            return appointmentContentRows;
        }
    }
}
