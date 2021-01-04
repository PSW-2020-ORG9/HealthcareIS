using System.Collections.Generic;
using System.Windows;
using WPFHospitalEditor;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.MapObjectModel;

namespace WPFDirector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void hospitalMapPressed(object sender, RoutedEventArgs e)
        {
            MapObjectController mapObjectController = new MapObjectController();
            List<MapObject> allMapObjects = mapObjectController.GetAllMapObjects();
            HospitalMap hospMap = new HospitalMap(allMapObjects, Role.Director);
            this.Close();
            hospMap.ShowDialog();
        }
    }
}
