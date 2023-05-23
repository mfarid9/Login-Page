using DevExpress.Xpf.Core;
using Login_Page.View.Control;
using Login_Page.ViewModel.Home;
using System.Windows;
using System.Windows.Controls;

namespace Login_Page
{
    /// <summary>
    /// Interaction logic for CtlStartPage.xaml
    /// </summary>
    public partial class CtlStartPage : Window
    {
        public CtlStartPage()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
            CtlHome ctlHome = new CtlHome();
            usercontrol.Content = ctlHome;
            home.ClickMode = ClickMode.Press;
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            home.ClickMode = ClickMode.Release;
            about.ClickMode = ClickMode.Release;
            proudect.ClickMode = ClickMode.Release;
            contect.ClickMode = ClickMode.Release;
            switch (((SimpleButton)sender).Name)
            {
                case "home":
                    usercontrol.Content = new CtlHome();
                    home.ClickMode = ClickMode.Press;

                    break;

                case "about":
                    usercontrol.Content = new CtlAbout();
                    about.ClickMode = ClickMode.Press;

                    break;

                case "proudect":
                    usercontrol.Content = new CtlProudect();
                    proudect.ClickMode = ClickMode.Press;
                    break;

                case "contect":
                    usercontrol.Content = new CtlAbout();
                    contect.ClickMode = ClickMode.Press;
                    break;
                default:
                    break;
            }
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
