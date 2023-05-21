using DevExpress.Xpf.Core;
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
                    home.ClickMode = ClickMode.Press;

                    break;

                case "about":
                    about.ClickMode = ClickMode.Press;

                    break;

                case "proudect":
                    proudect.ClickMode = ClickMode.Press;
                    break;

                case "contect":

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
