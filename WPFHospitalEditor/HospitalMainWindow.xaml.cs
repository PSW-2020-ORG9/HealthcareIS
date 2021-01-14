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
        public static Role role;
        public static HospitalMainWindow instance = null;

        private HospitalMainWindow(Role role)
        {
            InitializeComponent();
            HospitalMainWindow.role = role;
            ChangePage(new HospitalMapPage());
        }

        public static HospitalMainWindow GetInstance(Role role)
        {
            if (instance == null)
            {
                instance = new HospitalMainWindow(role);
            }
            return instance;
        }

        public void ChangePage(Page page)
        {
            MainFrame.Content = page;
        }
    }
}
