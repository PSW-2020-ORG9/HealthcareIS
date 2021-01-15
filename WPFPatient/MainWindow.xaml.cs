using System.Collections.Generic;
using System.Windows;
using WPFHospitalEditor;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Model;

namespace WPFPatient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUserServerController userServerController = new UserServerController();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            LoggedUser user = new LoggedUser(emailTextBox.Text, passwordTextBox.Password);
            LoggedUser.Role = Role.Patient;
            string loginStatus = userServerController.Login(user.Credentials);
            HospitalMainWindow window = HospitalMainWindow.GetInstance();
            if (!loginStatus.Equals("BadRequest"))
            {
                this.Close();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("You have enetered wrong email or password!");
            }
        }
    }
}
