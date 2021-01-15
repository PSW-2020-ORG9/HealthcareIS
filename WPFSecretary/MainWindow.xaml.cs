using System.Windows;
using WPFHospitalEditor;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Model;

namespace WPFSecretary
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

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            LoginCredentials loginCredentials = new LoginCredentials(emailTextBox.Text, passwordTextBox.Password);
            HospitalMainWindow window = HospitalMainWindow.GetInstance();
            string cookie = loginCredentials.Login(loginCredentials);
            if (cookie != null)
            {
                LoggedUser loggedUser = new LoggedUser(loginCredentials, Role.Secretary, cookie);
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
