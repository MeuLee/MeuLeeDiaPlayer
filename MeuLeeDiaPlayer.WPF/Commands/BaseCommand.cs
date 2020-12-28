using System;
using System.Windows.Input;

namespace MeuLeeDiaPlayer.WPF.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);
    }
}
