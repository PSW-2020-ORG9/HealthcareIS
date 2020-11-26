﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFHospitalEditor.Repository;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor
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
            HospitalMap startWindow = new HospitalMap(allMapObjects);
            this.Close();
            startWindow.ShowDialog();
        }
    }
}
