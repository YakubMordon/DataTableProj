// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Services.Helpers
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// RelayCommand, which implements <see cref="ICommand"/>.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Method for execution of command.
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        /// Method which checks if method can execute.
        /// </summary>
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">Execute method.</param>
        /// <param name="canExecute">CanExecute method.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return this._canExecute is null || this._canExecute(parameter);
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
    }
}
