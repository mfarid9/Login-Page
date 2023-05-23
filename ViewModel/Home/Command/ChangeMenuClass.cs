using System;
using System.Windows.Input;

namespace Login_Page.ViewModel.Home.Command
{
    public class ChangeMenuClass : ICommand
    {
        HomeViewModel vm;
        public ChangeMenuClass(HomeViewModel vm)
        {
            this.vm = vm;
        }
        public event EventHandler CanExecuteChanged

        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            vm.ChangeMenu(parameter.ToString());


        }
    }
}
