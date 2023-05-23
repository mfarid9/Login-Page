using Login_Page.View.Control;
using Login_Page.ViewModel.Home.Command;
using System.Windows.Controls;
using System.Windows.Input;

namespace Login_Page.ViewModel.Home
{
    public class HomeViewModel : Binding
    {

        public HomeViewModel()
        {
            ChangeMenuCommand = new ChangeMenuClass(this);
        }

        public ICommand ChangeMenuCommand { get; private set; }

        #region Prop
        private UserControl menucontrol;

        public UserControl Menucontrol
        {
            get { return menucontrol; }
            set { menucontrol = value; OnPropertyChanged(); }
        }
        #endregion

        public void ChangeMenu(string menu)
        {
            switch (menu)
            {
                case "Home":
                    Menucontrol = new CtlHome();
                    break;
                case "About":
                    Menucontrol = new CtlAbout();
                    break;
                case "Proudect":
                    Menucontrol = new CtlProudect();
                    break;
                default:
                    break;

            }
        }
    }
}
