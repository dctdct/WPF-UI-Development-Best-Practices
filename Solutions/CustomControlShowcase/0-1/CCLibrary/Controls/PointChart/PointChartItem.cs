using CommonLibrary.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

/*
* This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
* DO NOT ALTER OR REMOVE  THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
* 
* This file and the code contained in it are subject to the agreed contractual terms and conditions,
* in particular with regard to resale and publication.
*/
namespace CCLibrary.Controls.PointChart
{
    /// <summary>
    /// <para>
    /// The class PointChartItem represents a selectable point chart item
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
    /// <para>Date: 16.10.2013</para>
    /// </summary>
    [Description("The class PointChartItem represents a selectable point chart item")]
    public class PointChartItem : ListBoxItem
    {
        #region Members

        private bool internalIsLoaded = false;

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Category description.
        /// </summary>
        private const string PROPERTY_CATEGORY = "Customer PointChartItem Properties";

        /// <summary>
        /// XUnitValueProperty - Gets or sets the x unit value.
        /// </summary>
        public static readonly DependencyProperty XUnitValueProperty = DependencyProperty.Register("XUnitValue", typeof(double), typeof(PointChartItem), new FrameworkPropertyMetadata(double.NaN, PointPropertyChanged));
        
        /// <summary>
        /// YUnitValueProperty - Gets or sets the y unit value.
        /// </summary>
        public static readonly DependencyProperty YUnitValueProperty = DependencyProperty.Register("YUnitValue", typeof(double), typeof(PointChartItem), new FrameworkPropertyMetadata(double.NaN, PointPropertyChanged));

        #endregion

        #region PropertyChangedCallbacks

        /// <summary>
        /// Recalc x y in pixel
        /// </summary>
        /// <param name="d">PointChartItem</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void PointPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PointChartItem self = d as PointChartItem;

            if (!self.internalIsLoaded)
                return;

            self.RaiseEvent(new RoutedEventArgs(PointChartItem.UnitPointChangedEvent, self));
        }

        #endregion

        #region Events

        /// <summary>
        /// UnitPointChangedEvent - This event occurs when the x or y unit value has changed.
        /// </summary>
        public static readonly RoutedEvent UnitPointChangedEvent = EventManager.RegisterRoutedEvent("UnitPointChanged", RoutingStrategy.Bubble, typeof(UnitPointChangedEventHandler), typeof(PointChartItem));
        
        /// <summary>
        /// Delegate event handler for UnitPointChangedEvent.
        /// </summary>
        /// <param name="sender">PointChartItem</param>
        /// <param name="args">RoutedEventArgs</param>
        public delegate void UnitPointChangedEventHandler(object sender, RoutedEventArgs args);

        /// <summary>
        /// UnitPointChangedEvent handler.
        /// </summary>
        [Description("This event occurs when the x or y unit value has changed.")]
        public event UnitPointChangedEventHandler UnitPointChanged
        {
            add { AddHandler(UnitPointChangedEvent, value); }
            remove { RemoveHandler(UnitPointChangedEvent, value); }
        }

        #endregion

        #region Handlers



        #endregion

        #region Ctor

        /// <summary>
        /// public ctor
        /// </summary>
        public PointChartItem()
        {
            //this.Loaded += PointChartItemLoaded;
            this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (internalIsLoaded)
                        return;

                    internalIsLoaded = true;

                    RaiseEvent(new RoutedEventArgs(PointChartItem.UnitPointChangedEvent, this)); 
                }), DispatcherPriority.Loaded);
        }

        /// <summary>
        /// static ctor
        /// </summary>
        static PointChartItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PointChartItem), new FrameworkPropertyMetadata(typeof(PointChartItem)));
        }

        #endregion

        #region Init/Cleanup

        /*
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointChartItemLoaded(object sender, RoutedEventArgs e)
        {
            if (internalIsLoaded)
                return;

            internalIsLoaded = true;

            RaiseEvent(new RoutedEventArgs(PointChartItem.UnitPointChangedEvent, this));
        }
         * */

        #endregion

        #region OverrideMethods

        /// <summary>
        /// UpdateMargins
        /// </summary>
        /// <param name="sizeInfo">SizeChangedInfo</param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            Margin = new Thickness(-ActualWidth / 2, -ActualHeight / 2, 0, 0);
        }

        #endregion

        #region Methods



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the x unit value.
        /// </summary>
        [Description("Gets or sets the x unit value."), Category(PROPERTY_CATEGORY)]
        public double XUnitValue
        {
            get { return (double)GetValue(XUnitValueProperty); }
            set { SetValue(XUnitValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the y unit value.
        /// </summary>
        [Description("Gets or sets the y unit value."), Category(PROPERTY_CATEGORY)]
        public double YUnitValue
        {
            get { return (double)GetValue(YUnitValueProperty); }
            set { SetValue(YUnitValueProperty, value); }
        }

        #endregion
    }
}
