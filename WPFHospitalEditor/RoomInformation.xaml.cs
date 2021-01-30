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
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.StrategyPattern;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for RoomInformation.xaml
    /// </summary>
    public partial class RoomInformation : Window
    {

        private IRoomServerController roomServerController = new RoomServerController();
        private IMapObjectController mapObjectController = new MapObjectController();
        private SchedulingDto schDto;
        private Room createdRoom;
        private bool equipmentRelocated = false;
        public RoomInformation(SchedulingDto schDto)
        {
            this.schDto = schDto;
            InitializeComponent();
        }

        private void RenovationTypeSelection(object sender, SelectionChangedEventArgs e)
        {
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(RoomRenovation))
                {
                    if ((window as RoomRenovation).ComplexRenovationTypeComboBox.SelectedIndex == 1)
                    {
                        DividingStackPanel.Visibility = Visibility.Visible;
                        MergingStackPanel.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        DividingStackPanel.Visibility = Visibility.Hidden;
                        MergingStackPanel.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Renovation is not succesfully scheduled");
            this.Close();
        }

        private void OkBtnClick(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(RoomRenovation))
                {

                    if ((window as RoomRenovation).ComplexRenovationTypeComboBox.Text.Equals("Separate rooms"))
                    {
                        if (equipmentRelocated)
                        {
                            MessageBox.Show("Renovation is succesfully scheduled");
                            this.Close();
                        }
                        else
                            MessageBox.Show("Relocate equipment first!");
                    }
                    else
                    {
                        MessageBox.Show("Renovation is succesfully scheduled");
                        this.Close();
                    }

                }
            }
        }

        private int FindAvailableId()
        {
            int maxId = 1;
            IEnumerable<Room> allRooms = roomServerController.GetAllRooms();
            foreach (Room room in allRooms)
            {
                if (maxId < room.Id)
                {
                    maxId = room.Id;
                }
            }
            return ++maxId;
        }

        private void SeparateEquipmentClick(object sender, RoutedEventArgs e)
        {
            if(Room2Name.Text.Equals("") || WorkTime2.Text.Equals(""))
            {
                MessageBox.Show("Enter Room2 data");
            }
            else
            {
                equipmentRelocated = true;
                int id = FindAvailableId();
                CreateRoomDto createRoomDto = new CreateRoomDto()
                {
                    id = id,
                    name = Room2Name.Text
                };
                createdRoom = roomServerController.CreateRoom(createRoomDto);
                schDto.DestinationRoomId = id;
                ISearchResultStrategy strategy = new SearchResultStrategy(new EquipmentSeparation(schDto));
                SearchResultDialog equipmentRelocationDialog = new SearchResultDialog(strategy.GetSearchResult(), SearchType.EquipmentSeparation, schDto);
                equipmentRelocationDialog.ShowDialog();
            }           
        }
    }
}
