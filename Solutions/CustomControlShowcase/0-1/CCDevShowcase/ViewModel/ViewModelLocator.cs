using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Reflection;
using System.Collections.ObjectModel;
using CommonLibrary.Util;
using CCDevShowcase.ViewModel.Samples;

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
    /// The class ViewModelLocator represents a sample View Model Locator
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
    public class ViewModelLocator : ViewModelBase
    {
        #region StaticMembers
        
        /// <summary>
        /// SampleViewModelProperty source.
        /// </summary>
        private static SampleViewModel sampleViewModel;

        #endregion

        #region Members


        #endregion

        #region Ctor

        public ViewModelLocator()
        {
        }

        #endregion

        #region Methods



        #endregion

        #region Properties

        /// <summary>
        /// SampleViewModel gets or sets the MainWindowViewModel.
        /// </summary>
        public object SampleViewModel
        {
            get
            {
                if (sampleViewModel == null)
                    sampleViewModel = new SampleViewModel();

                return sampleViewModel;
            }
        }

        #endregion
    }
}
