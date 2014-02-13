using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using CCDevShowcase.View;
using System.Reflection;
using System.Windows.Input;
using System.Diagnostics;
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
    /// The class MainWindowViewModel represents a SampleViewModel
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
    public class MainWindowViewModel : ViewModelBase
    {
        #region Members

        /// <summary>
        /// CacheViewsProperty source.
        /// </summary>
        private bool cacheViews;

        /// <summary>
        /// ViewsProperty source.
        /// </summary>
        private ObservableCollection<ViewViewModel> viewsSource;

        /// <summary>
        /// NavigateToViewCommandProperty source.
        /// </summary>
        private DelegateCommand navigateToViewCommand;

        #endregion

        #region Ctor

        /// <summary>
        /// Public MainWindowViewModel
        /// </summary>
        public MainWindowViewModel()
        {
            InitializeMenuListBoxSampleData();

            Views.CurrentChanging += ViewsCurrentChanging;

            Views.CurrentChanged += ViewsCurrentChanged;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Delete view if not cached!
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">CurrentChangingEventArgs</param>
        private void ViewsCurrentChanging(object sender, CurrentChangingEventArgs e)
        {
            var viewViewModel = Views.CurrentItem as ViewViewModel;

            if (!CacheViews)
                viewViewModel.View = null;
        }

        /// <summary>
        /// Call UpdateView
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void ViewsCurrentChanged(object sender, EventArgs e)
        {
            UpdateView((Views.CurrentItem as ViewViewModel).Name);
        }

        #endregion

        #region Commands

        /// <summary>
        /// check can execute OnNavigationViewCommand
        /// </summary>
        /// <param name="parameter">object</param>
        /// <returns>bool</returns>
        private bool CanExecuteNavigatToViewCommand(object parameter)
        {
            return (parameter != null) && (Type.GetType(Assembly.GetExecutingAssembly().GetName().Name + ".View." + parameter as string) != null);
        }

        /// <summary>
        /// Logic for the OnNavigateToViewCommand
        /// </summary>
        /// <param name="parameter">object</param>
        private void OnNavigateToViewCommand(object parameter)
        {
            String className = parameter as String;

            Type type = Type.GetType(Assembly.GetExecutingAssembly().GetName().Name + ".View." + className);

            var viewViewModel = viewsSource.First(v => v.Name.Equals(type.Name));

            if (viewViewModel.View == null)
            {
                viewViewModel.View = Activator.CreateInstance(type);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Init MenuListBoxSampleData (get all items in .View namespace) and move currentItem to startview res!
        /// </summary>
        private void InitializeMenuListBoxSampleData()
        {
            Type[] types = WPFHelpers.GetTypesInNamespace(Assembly.GetExecutingAssembly(), Assembly.GetExecutingAssembly().GetName().Name + ".View");

            viewsSource = new ObservableCollection<ViewViewModel>();

            foreach (var t in types)
            {
                viewsSource.Add(new ViewViewModel() { Name = t.Name });
            }

            Views = CollectionViewSource.GetDefaultView(viewsSource);
            Views.MoveCurrentTo(viewsSource.First(v => v.Name.Equals(Application.Current.FindResource("StartView") as string)));
        }

        /// <summary>
        /// Updates the view
        /// </summary>
        /// <param name="p">ViewName</param>
        private void UpdateView(string viewName)
        {
            if (NavigateToViewCommand.CanExecute(viewName))
            {
                NavigateToViewCommand.Execute(viewName);
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// CacheViewsProperty gets or sets the CacheViews.
        /// </summary>
        public bool CacheViews
        {
            get
            {
                return this.cacheViews;
            }

            set
            {
                if (value != this.cacheViews)
                {
                    this.cacheViews = value;

                    // if cached views false -> delete created views (except current item)
                    if (!this.cacheViews)
                    {
                        foreach (var v in viewsSource)
                        {
                            if (v != Views.CurrentItem)
                                v.View = null;
                        }
                    }

                    OnPropertyChanged("CacheViews");
                }
            }
        }

        /// <summary>
        /// ViewsProperty gets or sets the Views.
        /// </summary>
        public ICollectionView Views
        {
            get;
            private set;
        }

        /// <summary>
        /// NavigateToViewCommandProperty gets or sets the NavigateToViewCommand.
        /// </summary>
        public ICommand NavigateToViewCommand
        {
            get
            {
                if (navigateToViewCommand == null)
                {
                    navigateToViewCommand = new DelegateCommand(OnNavigateToViewCommand, CanExecuteNavigatToViewCommand);
                }

                return navigateToViewCommand;
            }
        }

        #endregion
    }
}
