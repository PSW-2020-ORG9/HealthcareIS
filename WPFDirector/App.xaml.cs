using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFHospitalEditor.Controller;

namespace WPFDirector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit_Director(object sender, ExitEventArgs e)
        {
            MapObjectController mapObjectController = new MapObjectController();
            mapObjectController.setAllSelectedFieldsToFalse();
        }
    }
}
