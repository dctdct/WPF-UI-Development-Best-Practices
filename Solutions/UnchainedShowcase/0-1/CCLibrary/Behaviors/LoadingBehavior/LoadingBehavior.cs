using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Threading;

/*
* This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
* DO NOT ALTER OR REMOVE  THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
* 
* This file and the code contained in it are subject to the agreed contractual terms and conditions,
* in particular with regard to resale and publication.
*/
namespace CCLibrary.Behaviors
{
    /// <summary>
    /// <para>
    /// The class LoadingBehavior represents a loading behavior with adorner layer loading content
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
    /// <para>Date: 17.10.2013</para>
    /// </summary>
    public class LoadingBehavior : Behavior<FrameworkElement>
    {
        #region Members

        private LoadingBehaviorAdorner loadingBehaviorAdorner;
        private AdornerLayer loadingBehaviorAdornerLayer;
        private bool isAttached = false;

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Category description.
        /// </summary>
        private const string PROPERTY_CATEGORY = "Customer LoadingBehavior Properties";

        /// <summary>
        /// LoadingContentProperty - Gets or sets the loading content.
        /// </summary>
        public static readonly DependencyProperty LoadingContentProperty = DependencyProperty.Register("LoadingContent", typeof(object), typeof(LoadingBehavior), new FrameworkPropertyMetadata(null, LoadingContentPropertyChanged));
        
        /// <summary>
        /// LoadingContentProperty - Gets or sets the loading template.
        /// </summary>
        public static readonly DependencyProperty LoadingTemplateProperty = DependencyProperty.Register("LoadingTemplate", typeof(DataTemplate), typeof(LoadingBehavior), new FrameworkPropertyMetadata(null, LoadingContentTemplatePropertyChanged));

        /// <summary>
        /// LoadingDataContextProperty - Gets or sets the loading data context.
        /// </summary>
        public static readonly DependencyProperty LoadingDataContextProperty = DependencyProperty.Register("LoadingDataContext", typeof(object), typeof(LoadingBehavior), new FrameworkPropertyMetadata(null, LoadingDataContextPropertyChanged));
        
        /// <summary>
        /// IsLoadingProperty - Gets or sets the isloading state.
        /// </summary>
        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoadingBehavior), new FrameworkPropertyMetadata(false, IsLoadingPropertyChanged));
                
        #endregion

        #region PropertyChangedCallbacks

        /// <summary>
        /// Update LoadingBehavior Content
        /// </summary>
        /// <param name="d">LoadingBehavior</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void LoadingContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingBehavior self = d as LoadingBehavior;

            if (!self.isAttached)
                return;

            self.UpdateAdornerContent();
        }

        /// <summary>
        /// Update LoadingBehavior Content Template
        /// </summary>
        /// <param name="d">LoadingBehavior</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void LoadingContentTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingBehavior self = d as LoadingBehavior;

            if (!self.isAttached)
                return;

            self.UpdateAdornerContentTemplate();
        }

        /// <summary>
        /// Update data context
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void LoadingDataContextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingBehavior self = d as LoadingBehavior;

            if (!self.isAttached)
                return;

            self.UpdateAdornerContentDataContext();
        }

        /// <summary>
        /// Update adorner
        /// </summary>
        /// <param name="d">LoadingBehavior</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void IsLoadingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingBehavior self = d as LoadingBehavior;

            if (!self.isAttached)
                return;

            self.UpdateAdorner();
        }

        #endregion

        #region Events



        #endregion

        #region Handlers

        #endregion

        #region Ctor


        #endregion

        #region Init/Cleanup

        /// <summary>
        /// Init
        /// </summary>
        private void Init()
        {
            isAttached = true;

            loadingBehaviorAdornerLayer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
            loadingBehaviorAdorner = new LoadingBehaviorAdorner(this.AssociatedObject);

            UpdateAdorner();
            UpdateAdornerContent();
            UpdateAdornerContentTemplate();
            UpdateAdornerContentDataContext();
        }

        /// <summary>
        /// CleanUp
        /// </summary>
        private void CleanUp()
        {
            isAttached = false;

            loadingBehaviorAdornerLayer.Remove(loadingBehaviorAdorner);

            loadingBehaviorAdorner = null;
            loadingBehaviorAdornerLayer = null;
        }

        #endregion

        #region OverrideMethods

        /// <summary>
        /// Call Init
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.Dispatcher.BeginInvoke(new Action(() =>
            {
                Init();
            }), DispatcherPriority.Loaded);
        }

        /// <summary>
        /// Call Cleanup
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            CleanUp();
        }

        #endregion

        #region Methods

        /// <summary>
        /// UpdateAdornerContent
        /// </summary>
        private void UpdateAdornerContent()
        {
            loadingBehaviorAdorner.Content = LoadingContent ?? LoadingDataContext ?? this.AssociatedObject.DataContext;
        }
        
        /// <summary>
        /// UpdateAdornerContentTemplate
        /// </summary>
        private void UpdateAdornerContentTemplate()
        {
            loadingBehaviorAdorner.ContentTemplate = LoadingTemplate;
        }

        /// <summary>
        /// UpdateAdornerContentDataContext
        /// </summary>
        private void UpdateAdornerContentDataContext()
        {
            loadingBehaviorAdorner.DataContext = LoadingDataContext ?? this.AssociatedObject.DataContext;
        }

        /// <summary>
        /// UpdateAdorner
        /// </summary>
        private void UpdateAdorner()
        {
            if (!IsLoading)
                loadingBehaviorAdornerLayer.Remove(loadingBehaviorAdorner);
            else
                loadingBehaviorAdornerLayer.Add(loadingBehaviorAdorner);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the loading content.
        /// </summary>
        [Description("Gets or sets the loading content."), Category(PROPERTY_CATEGORY)]
        public object LoadingContent
        {
            get { return (object)GetValue(LoadingContentProperty); }
            set { SetValue(LoadingContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the loadingContent.
        /// </summary>
        [Description("Gets or sets the loading template."), Category(PROPERTY_CATEGORY)]
        public DataTemplate LoadingTemplate
        {
            get { return (DataTemplate)GetValue(LoadingTemplateProperty); }
            set { SetValue(LoadingTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the loading data context.
        /// </summary>
        [Description("Gets or sets the loading data context."), Category(PROPERTY_CATEGORY)]
        public object LoadingDataContext
        {
            get { return (object)GetValue(LoadingDataContextProperty); }
            set { SetValue(LoadingDataContextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the isloading state.
        /// </summary>
        [Description("Gets or sets the isloading state."), Category(PROPERTY_CATEGORY)]
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        #endregion
    }
}
