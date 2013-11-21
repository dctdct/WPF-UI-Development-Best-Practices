using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

/*
 * This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
 * DO NOT ALTER OR REMOVE THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
 * 
 * This file and the code contained in it are subject to the agreed contractual terms and conditions, 
 * in particular with regard to resale and publication.
 */
namespace CommonLibrary.Util
{
    /// <summary>
    /// <para>
    /// The class DelegateCommand represents a implementation of ICommand
    /// </para>
    /// 
    /// <para>
    /// Class history:
    /// <list type="bullet">
    ///     <item>
    ///         <description>0.1: First release, working (André Lanninger).</description>
    ///     </item>
    /// </list>
    /// </para>
    /// 
    /// <para>Author: André Lanninger, from: http://www.wpftutorial.net/delegatecommand.html</para>
    /// <para>Date: 06.06.2012</para>
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// Predicate _canExecute
        /// </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Action _execute
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        /// Eventhandler CanExecuteChanged
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Ctor for DelegateCommand
        /// </summary>
        /// <param name="execute">Action</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Ctor for DelegateCommand
        /// </summary>
        /// <param name="execute">Action</param>
        /// <param name="canExecute">Predicate</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Returns wheter the Command can be executed or not
        /// </summary>
        /// <param name="parameter">object</param>
        /// <returns>bool</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">object</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Raises CanExecuteChanged
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}

