using System.Collections.Generic;
using System.Windows;
using WPFHospitalEditor;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.MapObjectModel;

namespace WPFPatient
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
            List<MapObject> allMapObjects = mapObjectController.getAllMapObjects();
            HospitalMap hospMap = new HospitalMap(allMapObjects, Role.Patient);
            this.Close();
            hospMap.ShowDialog();
        }
    }
}
