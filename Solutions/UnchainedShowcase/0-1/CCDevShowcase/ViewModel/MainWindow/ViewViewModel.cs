using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CommonLibrary.Util;

/*
 * This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
 * DO NOT ALTER OR REMOVE  THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
 * 
 * This file and the code contained in it are subject to the agreed contractual terms and conditions, 
 * in particular with regard to resale and publication.
 */
namespace CCDevShowcase.ViewModel
{
    /// <summary>
    /// <para>
    /// The class ViewViewModel represents a view view model
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
    public class ViewViewModel : ViewModelBase
    {
        /// <summary>
        /// NameProperty source.
        /// </summary>
        private string name;

        /// <summary>
        /// ViewProperty source.
        /// </summary>
        private object view;

        /// <summary>
        /// NameProperty gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// ViewProperty gets or sets the View.
        /// </summary>
        public object View
        {
            get
            {
                return view;
            }

            set
            {
                if (value != this.view)
                {
                    this.view = value;

                    if (this.view == null)
                        Debug.WriteLine("Del_" + this.name);
                    else
                        Debug.WriteLine("Add_" + this.name);

                    Debug.WriteLine("------");

                    OnPropertyChanged("View");
                }
            }
        }

        /// <summary>
        /// Override to string for listbox ;)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
