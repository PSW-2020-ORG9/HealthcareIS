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
using System;

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
        public static int selectedObjectId = -1;
        private Grid DynamicGrid;

        private IDoctorServerController doctorServerController = new DoctorServerController();
        private IExaminationServerController examinationServerController = new ExaminationServerController();

        public SearchResultDialog(List<SearchResultDTO> searchResults, SearchType searchType)
        {
            InitializeComponent();
            this.searchType = searchType;
            ShowDynamicGrid();
            AddContent(searchResults);
        }

        private void ShowDynamicGrid()
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

        private void AddContent(List<SearchResultDTO> searchResults)
        {
            int row = 0;
            foreach (SearchResultDTO rowContent in searchResults)
            {
                CreateRow(50);
                SetRowContent(row, rowContent);
                AddSeparator(row);
                row++;
            }
        }
        private void CreateRow(int height)
        {
            RowDefinition gridRow = new RowDefinition();
            gridRow.Height = new GridLength(height);
            DynamicGrid.RowDefinitions.Add(gridRow);
        }
        private void SetRowContent(int row, SearchResultDTO rowContent)
        {
            InsertLabels(row, rowContent);
            InsertButtons(row, rowContent);
        }

        private void InsertLabels(int row, SearchResultDTO rowContent)
        {
            string[] labels = rowContent.Content.Split(AllConstants.ContentSeparator);
            for (int col = 0; col < labels.Length; col++)
            {
            Label label = CreateLabel(row, col, labels[col]);
            DynamicGrid.Children.Add(label);
            }
        }


        private void InsertButtons(int row, SearchResultDTO rowContent)
        {
            if (searchType != SearchType.EquipmentRelocationSearch)
                AddAdvancedSearchButton(row, rowContent.MapObjectId);

            if (searchType == SearchType.AppointmentSearch)
            {
                AppointmentSearchResultDTO appointment = (AppointmentSearchResultDTO)rowContent;
                AddScheduleExaminationButton(row, appointment.RecommendationDto);
            }
            else if (searchType == SearchType.EquipmentRelocationSearch)
            {
                EquipmentRelocationSearchResultDTO equipmentRelocation = (EquipmentRelocationSearchResultDTO)rowContent;
                AddScheduleRelocationButton(row, equipmentRelocation.EquipmentRelocationDto);
            }

        }
        private void AddAdvancedSearchButton(int row, int mapObjectId)
        {
            Button advancedSearchBtn = CreateAdvancedSearchButton();

            Grid.SetRow(advancedSearchBtn, row);
            Grid.SetColumn(advancedSearchBtn, DynamicGrid.ColumnDefinitions.Count - 1);
            DynamicGrid.Children.Add(advancedSearchBtn);

            advancedSearchBtn.Click += (s, e) =>
            {
                MapObject chosenMapObject = new MapObjectController().GetMapObjectById(mapObjectId);
                selectedObjectId = chosenMapObject.Id;

                if (chosenMapObject.MapObjectDescription == null)
                    ShowHospitalMapPage();
                else
                {
                    MapObjectDescription description = chosenMapObject.MapObjectDescription;
                    ShowBuildingPage(description.FloorNumber, description.BuildingId);
                }
                this.Close();
            };
        }

        private void AddScheduleExaminationButton(int row, RecommendationDto recommendation)
        {
            Button scheduleBtn = CreateScheduleButton();
            Grid.SetRow(scheduleBtn, row);
            Grid.SetColumn(scheduleBtn, DynamicGrid.ColumnDefinitions.Count - 2);
            DynamicGrid.Children.Add(scheduleBtn);

            scheduleBtn.Click += (s, e) =>
            {
                ScheduleWindow scheduleWindow = new ScheduleWindow(recommendation, this);
                scheduleWindow.ShowDialog();
            };
        }

        private void AddScheduleRelocationButton(int row, EquipmentRelocationDto relocation)
        {
            Button scheduleBtn = CreateScheduleButton();
            Grid.SetRow(scheduleBtn, row);
            Grid.SetColumn(scheduleBtn, DynamicGrid.ColumnDefinitions.Count - 2);
            DynamicGrid.Children.Add(scheduleBtn);

            scheduleBtn.Click += (s, e) =>
            {
                List<int> doctors = doctorServerController.GetDoctorsByRoomsAndShifts(relocation).ToList();
                foreach (int doctorId in doctors)
                {
                    examinationServerController.ScheduleExamination(relocation.TimeInterval.Start, doctorId, 100);
                }
                MessageBox.Show("Relocation is successfully scheduled!", "");
                this.Close();
            };
        }



        public void ShowBuildingPage(int floorNumber, int buildingId)
        {
            BuildingPage searchedBuilding = new BuildingPage(buildingId, floorNumber);
            HospitalMainWindow.GetInstance().ChangePage(searchedBuilding);
        }

        public void ShowHospitalMapPage()
        {
            HospitalMapPage hospitalMap = new HospitalMapPage();
            HospitalMainWindow.GetInstance().ChangePage(hospitalMap);
        }

        private Label CreateLabel(int row, int col, string content)
        {
            Label label = new Label();
            label.Content = content;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, col);
            return label;
        }

        private Button CreateAdvancedSearchButton()
        {
            Button btn = new Button();
            btn.Content = "+";
            btn.FontSize = 35;
            btn.FontWeight = FontWeights.UltraBold;
            btn.Width = 35;
            btn.VerticalContentAlignment = VerticalAlignment.Top;
            SetCommonButtonProperties(btn);
            return btn;
        }

        private Button CreateScheduleButton()
        {
            Button btn = new Button();
            btn.Content = "Schedule";
            btn.FontSize = 15;
            btn.FontWeight = FontWeights.Normal;
            btn.Width = 70;
            btn.VerticalContentAlignment = VerticalAlignment.Bottom;
            SetCommonButtonProperties(btn);
            return btn;
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

        private void AddSeparator(int row)
        {
            Separator separator = new Separator();
            separator.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(separator, row);
            Grid.SetColumn(separator, 0);
            Grid.SetColumnSpan(separator, DynamicGrid.ColumnDefinitions.Count);
            DynamicGrid.Children.Add(separator);
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            selectedObjectId = -1;
            this.Close();
        }
    }
}
