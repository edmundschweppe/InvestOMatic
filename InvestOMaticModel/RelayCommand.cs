using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InvestOMaticModel
{
    /// <summary>
    /// A class to make creating ICommand objects easier
    /// </summary>
    /// <remarks>Based on the RelayCommand object in the MvvmLight toolkit 
    /// (http://mvvmlight.codeplex.com/SourceControl/latest#GalaSoft.MvvmLight/GalaSoft.MvvmLight (PCL)/Command/RelayCommand.cs)
    /// </remarks>
    public class RelayCommand : ICommand
    {
        private readonly Action _executeThis;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Creates a new ICommand implementation that relays to the supplied methods
        /// </summary>
        /// <param name="execute">Zero-argument, void-returning method to execute</param>
        /// <param name="canExecute">Zero-argument, bool-returing method which determines whther or not the method can be executed</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _executeThis = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Whether or not this command can be executed
        /// </summary>
        /// <param name="parameter">Required by ICommand but ignored in this implementation</param>
        /// <returns></returns>
        /// <remarks>Note that the parameter is ignored in this implementation</remarks>
        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Required by ICommand but ignored in this implementation</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _executeThis();
            }
        }
    }
}
