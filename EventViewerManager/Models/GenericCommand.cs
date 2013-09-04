
namespace EventViewerManager.Models
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Provide generic implementation of the ICommand interface
    /// </summary>
    internal class GenericCommand : ICommand
    {
        /// <summary> Instance of the Action to perform on execute </summary>
        private Action<object> execute;

        /// <summary> Instance of the Func to perform on canExecute</summary>
        private Func<object, bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public GenericCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return this.canExecute(parameter);
            }

            return false;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (this.execute != null && this.CanExecute(parameter))
            {
                this.execute(parameter);
            }
        }
    }
}