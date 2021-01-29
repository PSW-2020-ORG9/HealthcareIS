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
using WPFHospitalEditor.Model;
using System.DirectoryServices;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for SearchResultDialog.xaml
    /// </summary>
    public partial class SearchResultDialog : Window
    {
        private int destAmount;
        private int sourceAmount;
        private readonly SearchType searchType;
        public static int selectedObjectId = -1;
        private Grid DynamicGrid;
        private IMapObjectController mapObjectController = new MapObjectController();
        private IDoctorServerController doctorServerController = new DoctorServerController();
        private IExaminationServerController examinationServerController = new ExaminationServerController();
        private IRenovationServerController renovationServerController = new RenovationServerController();
        private TimeInterval timeInterval;
        private DateTime startDate;
        private SchedulingDto schDto;
        private List<SearchResultDTO> searchResults;
        public SearchResultDialog(List<SearchResultDTO> searchResults, SearchType searchType)
        {
            InitializeComponent();
            this.searchType = searchType;
            ShowDynamicGrid();
            AddContent(searchResults);
        }

        public SearchResultDialog(List<SearchResultDTO> searchResults, SearchType searchType, SchedulingDto schDto)
        {
            InitializeComponent();
            this.schDto = schDto;
            this.searchType = searchType;
            this.searchResults = searchResults;
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
            else if (searchType == SearchType.EquipmentRelocationSearch)
            {
                EquipmentRelocationGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicEquipmentRelocationGrid;
            }
            else if (searchType == SearchType.RoomRenovationSearch)
            {
                RenovationAppointmentsGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicRenovationAppointmentsGrid;
            }
            else if (searchType == SearchType.BasicRoomRenovationSearch)
            {
                BasicRenovationAppointmentsGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicBasicRenovationAppointmentsGrid;
            }
            else if (searchType == SearchType.EquipmentSeparation)
            {
                EquipmentSeparationGrid.Visibility = Visibility.Visible;
                DynamicGrid = DynamicEquipmentSeparationGrid;
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
            if(searchType == SearchType.EquipmentSeparation)
                InsertTextBoxesAndLabels(row, rowContent);
            else
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
            if(searchType == SearchType.EquipmentRelocationSearch)
            {
                EquipmentRelocationSearchResultDTO equipmentRelocation = (EquipmentRelocationSearchResultDTO)rowContent;
                AddScheduleRelocationButton(row, equipmentRelocation.EquipmentRelocationDto);
            }
            else if(searchType == SearchType.RoomRenovationSearch || searchType == SearchType.BasicRoomRenovationSearch)
            {
                RenovationSearchResultDTO renovationAppointments = (RenovationSearchResultDTO)rowContent;
                AddScheduleRenovationButton(row, renovationAppointments.RenovationDto);
            }
            else if(searchType == SearchType.EquipmentSeparation)
            {
                EquipmentSeparationSearchResultDTO equipmentSeparation = (EquipmentSeparationSearchResultDTO)rowContent;
                AddMinusButton(row, equipmentSeparation.EquipmentSeparationDto);
                AddPlusButton(row, equipmentSeparation.EquipmentSeparationDto);
            }
            else
            {
                AddAdvancedSearchButton(row, rowContent.MapObjectId);
                if (searchType == SearchType.AppointmentSearch)
                {
                    AppointmentSearchResultDTO appointment = (AppointmentSearchResultDTO)rowContent;
                    AddScheduleExaminationButton(row, appointment.RecommendationDto);
                }
            }
        }

        private void InsertTextBoxesAndLabels(int row, SearchResultDTO rowContent)
        {
            string[] labels = rowContent.Content.Split(AllConstants.ContentSeparator);
            for (int col = 0; col < labels.Length; col++)
            {
                if (col % 2 == 0)
                {
                    Label label = CreateLabel(row, col, labels[col]);
                    DynamicGrid.Children.Add(label);
                }
                else
                {
                    TextBox textBox = CreateEquipmentAmountTextBox(row,col,labels[col]);
                    DynamicGrid.Children.Add(textBox);
                }

            }
        }

        private void AddMinusButton(int row, EquipmentSeparationDto equipmentSeparation)
        {
            Button minus = CreateMinusButton();
            Grid.SetRow(minus, row);
            Grid.SetColumn(minus, DynamicGrid.ColumnDefinitions.Count - 2);
            DynamicGrid.Children.Add(minus);
            int column = DynamicGrid.ColumnDefinitions.Count - 2;

            minus.Click += (s, e) =>
            {
                equipmentSeparation.DestinationQuantity--;
                equipmentSeparation.SourceQuantity++;
                destAmount = equipmentSeparation.DestinationQuantity;
                sourceAmount = equipmentSeparation.SourceQuantity;
                
            };
        }

        private void AddPlusButton(int row, EquipmentSeparationDto equipmentSeparation)
        {
            Button plus = CreatePlusButton();
            Grid.SetRow(plus, row);
            Grid.SetColumn(plus, DynamicGrid.ColumnDefinitions.Count - 1);
            DynamicGrid.Children.Add(plus);

            plus.Click += (s, e) =>
            {
                equipmentSeparation.DestinationQuantity++;
                equipmentSeparation.SourceQuantity--;
                destAmount = equipmentSeparation.DestinationQuantity;
                sourceAmount = equipmentSeparation.SourceQuantity;
            };
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

            SchedulingDto schedulingDto = relocation.toSchedulingDto();

            scheduleBtn.Click += (s, e) =>
            {

                List<int> doctors = new DoctorServerController().GetDoctorsByRoomsAndShifts(relocation.toSchedulingDto()).ToList();
                IExaminationServerController examinationServerController = new ExaminationServerController();
                foreach (int doctorId in doctors)
                {
                    examinationServerController.ScheduleExamination(relocation.TimeInterval.Start, doctorId, AllConstants.PatientIdForRelocation);
                }
                MessageBox.Show("Relocation is successfully scheduled!");              
                this.Close();
            };
        }

        private void AddScheduleRenovationButton(int row, RenovationDto renovation)
        {
            Button scheduleBtn = CreateScheduleButton();
            Grid.SetRow(scheduleBtn, row);
            Grid.SetColumn(scheduleBtn, DynamicGrid.ColumnDefinitions.Count - 2);
            DynamicGrid.Children.Add(scheduleBtn);

            SchedulingDto schDto = renovation.toSchedulingDto();
            timeInterval = new TimeInterval(schDto.TimeInterval.Start, schDto.TimeInterval.End);

            scheduleBtn.Click += (s, e) =>
            {
                List<int> doctors = doctorServerController.GetDoctorsByRoomsAndShifts(schDto).ToList();
                foreach (int doctorId in doctors)
                {
                    renovationServerController.ScheduleRenovation(renovation.TimeInterval, doctorId, AllConstants.PatientIdForRenovation);
                }
                MessageBox.Show("Renovation is successfully scheduled!");
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(RoomRenovation))
                    {

                        if ((window as RoomRenovation).RenovationTypeComboBox.Text.Equals("Complex"))
                        {
                            RoomInformation roomInformation = new RoomInformation(schDto);

                            if ((window as RoomRenovation).ComplexRenovationTypeComboBox.Text.Equals("Separate room"))
                            {
                                roomInformation.MergingStackPanel.Visibility = Visibility.Hidden;
                                roomInformation.DividingStackPanel.Visibility = Visibility.Visible;
                                roomInformation.Room1Name.Text = mapObjectController.GetMapObjectById(renovation.SourceRoomId).Name;
                                int workTime = mapObjectController.GetMapObjectById(renovation.SourceRoomId).MapObjectDescription.Information.Split("=")[1].Length;
                                roomInformation.WorkTime1.Text = mapObjectController.GetMapObjectById(renovation.SourceRoomId).MapObjectDescription.Information.Split("=")[1].Substring(0, workTime - 1);

                            }
                            else if ((window as RoomRenovation).ComplexRenovationTypeComboBox.Text.Equals("Join rooms"))
                            {
                                roomInformation.DividingStackPanel.Visibility = Visibility.Hidden;
                                roomInformation.MergingStackPanel.Visibility = Visibility.Visible;
                                roomInformation.RoomName.Text = mapObjectController.GetMapObjectById(renovation.SourceRoomId).Name;
                                int workTime = mapObjectController.GetMapObjectById(renovation.SourceRoomId).MapObjectDescription.Information.Split("=")[1].Length;
                                roomInformation.WorkTime.Text = mapObjectController.GetMapObjectById(renovation.SourceRoomId).MapObjectDescription.Information.Split("=")[1].Substring(0, workTime - 1);
                                List<int> doctors1 = doctorServerController.GetDoctorsByRoomsAndShifts(schDto).ToList();
                                foreach (int doctorId in doctors1)
                                {
                                    startDate = schDto.TimeInterval.Start.AddDays(1);
                                    examinationServerController.ScheduleExamination(startDate, doctorId, AllConstants.PatientIdForRelocation);
                                }

                            }
                            roomInformation.ShowDialog();
                        }
                    }
                }
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

        private Button CreatePlusButton()
        {
            Button btn = new Button();
            btn.Content = "+";
            btn.FontWeight = FontWeights.UltraBold;
            btn.FontSize = 35;
            btn.Height = 35;
            btn.Width = 35;
            btn.VerticalContentAlignment = VerticalAlignment.Top;
            SetCommonButtonProperties(btn);
            return btn;
        }

        private Button CreateMinusButton()
        {
            Button btn = new Button();
            btn.Content = "-";
            btn.FontWeight = FontWeights.UltraBold;
            btn.FontSize = 35;
            btn.Height = 35;
            btn.Width = 35;
            btn.VerticalContentAlignment = VerticalAlignment.Top;
            SetCommonButtonProperties(btn);
            return btn;
        }

        private TextBox CreateEquipmentAmountTextBox(int row, int col, string content)
        {
            TextBox textBox = new TextBox();
            textBox.Width = 30;
            textBox.Height = 20;
            textBox.Text = content;
            textBox.VerticalContentAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(textBox, row);
            Grid.SetColumn(textBox, col);
            return textBox;
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

        private void FinishRelocationClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DTO:\nSource:" + schDto.SourceRoomId + "\nDestination:" + schDto.DestinationRoomId+"\nStart:"+schDto.TimeInterval.Start);
            foreach(SearchResultDTO searchRes in searchResults)
            {
                MessageBox.Show("ULAZI U PRVI FOR");
                List<int> doctors = doctorServerController.GetDoctorsByRoomsAndShifts(schDto).ToList();
                int i = 1;
                foreach (int doctorId in doctors)
                {
                    MessageBox.Show("ZAKAZUJE " + i + ". PUT");
                    examinationServerController.ScheduleExamination(schDto.TimeInterval.Start, doctorId, AllConstants.PatientIdForRelocation);
                    i++;
                }
            }
            MessageBox.Show("Equipment succesfully relocated");
            this.Close();
        }
    }
}
