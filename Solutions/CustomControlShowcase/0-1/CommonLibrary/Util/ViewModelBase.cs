using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Linq.Expressions;

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
    /// The class XObject represents a base class for creating derived ViewModel-Classes
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
    /// <para>Author: André Lanninger</para>
    /// <para>Date: 06.06.2012</para>
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Logic for PropertyChanged 
        /// </summary>
        /// <param name="propertyName">string</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        /// <summary>
        /// ThrowOnInvalidPropertyName-Property
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        /// <summary>
        /// VerifyPropertyName
        /// </summary>
        /// <param name="propertyName">string</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// PropertyChanged Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
