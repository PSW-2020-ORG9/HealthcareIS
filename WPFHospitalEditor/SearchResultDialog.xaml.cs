using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Pages;
using WPFHospitalEditor.StrategyPattern;
using WPFHospitalEditor.Controller.Interface;
using System.Linq;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for SearchResultDialog.xaml
    /// </summary>
    public partial class SearchResultDialog : Window
    {
        private readonly MapObjectController mapObjectController = new MapObjectController();
        private readonly List<Button> advancedSearchButtons = new List<Button>();
        private readonly List<Button> scheduleButtons = new List<Button>();
        private readonly Dictionary<int, int> displayBtnRow = new Dictionary<int, int>();
        private readonly Dictionary<int, RecommendationDto> scheduleBtnRow = new Dictionary<int, RecommendationDto>();
        private readonly Dictionary<int, EquipmentRelocationDto> scheduleEquipmentRelocationBtnRow = new Dictionary<int, EquipmentRelocationDto>();
        private readonly SearchType searchType;
        private int firstContentRowNumber = 0;
        public static int selectedObjectId = -1;
        private Grid DynamicGrid;

        private IDoctorServerController doctorServerController = new DoctorServerController();
        private IExaminationServerController examinationServerController = new ExaminationServerController();

        public SearchResultDialog(List<SearchResultDTO> searchResults, SearchType searchType)
        {
            InitializeComponent();
            this.searchType = searchType;
            this.Height = AllConstants.SearchDialogHeight;
            ShowDynamicGrid();
            AddContent(searchResults);
        }

        private void AddContent(List<SearchResultDTO> searchResults)
        {
            int index = 0;
            foreach (SearchResultDTO result in searchResults)
            {
                displayBtnRow.Add(index, result.MapObjectId);
                if (this.searchType == SearchType.AppointmentSearch)
                    scheduleBtnRow.Add(index, ((AppointmentSearchResultDTO)result).RecommendationDto);
                else if (this.searchType == SearchType.EquipmentRelocationSearch)
                    scheduleEquipmentRelocationBtnRow.Add(index, ((EquipmentRelocationSearchResultDTO)result).EquipmentRelocationDto);
                CreateOneRow(50);
                string[] oneRowContents = result.Content.Split(AllConstants.ContentSeparator);
                CreateRowData(oneRowContents);
                index++;
            }
        }

        private void ShowDynamicGrid()
        {
            if (this.searchType == SearchType.MapObjectSearch)
            {
                MapObjectGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicMapObjectGrid;
            }
            else if (this.searchType == SearchType.EquipmentSearch || this.searchType == SearchType.MedicationSearch)
            {
                EquipmentAndMedicationGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicEquipmentAndMedicationGrid;
            }
            else if (this.searchType == SearchType.AppointmentSearch)
            {
                AppointmentGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicAppointmentGrid;
            }
            else if (this.searchType == SearchType.EquipmentRelocationSearch)
            {
                EquipmentRelocationGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicEquipmentRelocationGrid;
            }
            else
            {
                return;
            }
            scrollViewer.Content = DynamicGrid;
        }

        private void CreateRowData(string[] oneRowContents)
        {
            AddLabels(oneRowContents);
            if (searchType != SearchType.EquipmentRelocationSearch)
                AddAdvancedSearchButton();
            if (searchType == SearchType.AppointmentSearch || searchType == SearchType.EquipmentRelocationSearch)
                AddScheduleButton();

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
                    MapObject chosenMapObject = mapObjectController.GetMapObjectById(displayBtnRow[Grid.GetRow(advancedSearchBtn)]);
                    selectedObjectId = chosenMapObject.Id;
                    mapObjectController.Update(chosenMapObject);

                    if (chosenMapObject.MapObjectDescription == null)
                    {
                        CanvasService.AddObjectToCanvas(mapObjectController.GetOutterMapObjects(), HospitalMapPage.canvasHospitalMap);
                    }
                    else
                    {
                        DisplayBuildingAndFloorBasedOnSelectedObject(chosenMapObject.MapObjectDescription.FloorNumber, chosenMapObject.MapObjectDescription.BuildingId);
                    }
                    this.Close();
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
                if (searchType == SearchType.AppointmentSearch)
                {
                    if (scheduleBtnRow.ContainsKey(Grid.GetRow(scheduleBtn)))
                    {
                        RecommendationDto chosenRecommendation = scheduleBtnRow[Grid.GetRow(scheduleBtn)];
                        ScheduleWindow scheduleWindow = new ScheduleWindow(chosenRecommendation, this);
                        scheduleWindow.ShowDialog();
                    }
                }
                else if (searchType == SearchType.EquipmentRelocationSearch)
                {
                    if (scheduleEquipmentRelocationBtnRow.ContainsKey(Grid.GetRow(scheduleBtn)))
                    {
                        EquipmentRelocationDto equipmentRelocationDto = scheduleEquipmentRelocationBtnRow[Grid.GetRow(scheduleBtn)];

                        List<int> doctors = doctorServerController.GetDoctorsByRoomsAndShifts(equipmentRelocationDto).ToList();
                        foreach (int doctorId in doctors)
                        {
                            examinationServerController.ScheduleExamination(equipmentRelocationDto.TimeInterval.Start, doctorId, 100);
                        }
                        MessageBox.Show("Relocation is successfully scheduled!", "");
                        this.Close();
                    }
                }
            };
        }

        public void DisplayBuildingAndFloorBasedOnSelectedObject(int floorNumber, int buildingId)
        {
            BuildingPage searchedBuilding = new BuildingPage(buildingId, floorNumber);
            HospitalMainWindow window = HospitalMainWindow.GetInstance();
            window.ChangePage(searchedBuilding);
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            selectedObjectId = -1;
            CanvasService.AddObjectToCanvas(mapObjectController.GetOutterMapObjects(), HospitalMapPage.canvasHospitalMap);
            this.Close();
        }
    }
}
