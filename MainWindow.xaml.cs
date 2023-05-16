using System.Windows;
using System.Windows.Controls;



namespace Login_Page
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            home.ClickMode = ClickMode.Press;
        }

        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {

            if (txtuser.Text == "admin" && txtpass.Text == "123")
            {

                // TODO: Authenticate the user and login them to your application.


                grid.Visibility = Visibility.Collapsed;

                // Close this window.
                //this.Close();
            }
            else
            {

                MessageBox.Show("Invalid username or password.");
            }
        }


        private void home_Click(object sender, RoutedEventArgs e)
        {

            switch (sender)
            {
                case "home":
                    home.ClickMode = ClickMode.Press;
                    about.ClickMode = ClickMode.Release;
                    proudect.ClickMode = ClickMode.Release;
                    contect.ClickMode = ClickMode.Release;
                    break;

                case "about":
                    home.ClickMode = ClickMode.Release;
                    about.ClickMode = ClickMode.Press;
                    proudect.ClickMode = ClickMode.Release;
                    contect.ClickMode = ClickMode.Release;
                    break;

                case "proudect":
                    home.ClickMode = ClickMode.Release;
                    about.ClickMode = ClickMode.Release;
                    proudect.ClickMode = ClickMode.Press;
                    contect.ClickMode = ClickMode.Release;
                    break;

                case "contect":
                    home.ClickMode = ClickMode.Release;
                    about.ClickMode = ClickMode.Release;
                    proudect.ClickMode = ClickMode.Release;
                    contect.ClickMode = ClickMode.Press;
                    break;
                default:
                    break;
            }
        }
    }
}
