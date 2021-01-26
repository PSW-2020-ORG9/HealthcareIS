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

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for RoomInformation.xaml
    /// </summary>
    public partial class RoomInformation : Window
    {

        private IRoomServerController roomServerController = new RoomServerController();
        private IMapObjectController mapObjectController = new MapObjectController();
        public RoomInformation()
        {
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
            this.Close();
        }

        private void OkBtnClick(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(RoomRenovation))
                {
                    if ((window as RoomRenovation).ComplexRenovationTypeComboBox.Text.Equals("Separate room"))
                    {
                        int id = FindAvailableId();
                        CreateRoomDto createRoomDto = new CreateRoomDto()
                        {
                            id = id,
                            name = Room2Name.Text
                        };
                        roomServerController.CreateRoom(createRoomDto);
                        MessageBox.Show("Room " + Room2Name.Text + " succesfully created!");
                    }
                    else if ((window as RoomRenovation).ComplexRenovationTypeComboBox.Text.Equals("Join rooms"))
                    {
                        MessageBox.Show("Relocation is successfully scheduled!");
                    }
                }
            }
            this.Close();
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
    }
}
