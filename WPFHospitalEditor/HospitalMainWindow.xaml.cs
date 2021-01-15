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
using WPFHospitalEditor.Pages;

namespace WPFHospitalEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HospitalMainWindow : Window
    {
        public static HospitalMainWindow instance = null;

        private HospitalMainWindow()
        {
            InitializeComponent();
            ChangePage(new HospitalMapPage());
        }

        public static HospitalMainWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new HospitalMainWindow();
            }
            return instance;
        }

        public void ChangePage(Page page)
        {
            MainFrame.Content = page;
        }
    }
}
