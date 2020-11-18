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

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for Building1.xaml
    /// </summary>
    public partial class Building1 : Window
    {
        AllMapObjects allMapObjects = new AllMapObjects();

        public Building1()
        {
            InitializeComponent();
            canvas.Children.Clear();
            addObjectToCanvas(AllMapObjects.allFirstBuildingFirstFloorObjects);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            HospitalMap window1 = new HospitalMap();
            this.Close();
            window1.ShowDialog();
        }

        public void addObjectToCanvas(List<MapObject> objectsToShow)
        {
            for (int i = 0; i < objectsToShow.Count; i++)
            {
                canvas.Children.Add(objectsToShow[i].rectangle);
                canvas.Children.Add(objectsToShow[i].name);
                canvas.Children.Add(objectsToShow[i].MapObjectDoor.rectangle);
            }
        }

        private void floor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canvas.Children.Clear();
            if (floor.SelectedIndex == 0)
            {
                addObjectToCanvas(AllMapObjects.allFirstBuildingFirstFloorObjects);
            }
            else if (floor.SelectedIndex == 1)
            {
                addObjectToCanvas(AllMapObjects.allFirstBuildingSecondFloorObjects);
            }
        }
    }
}
