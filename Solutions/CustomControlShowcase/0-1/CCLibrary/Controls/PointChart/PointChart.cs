using CommonLibrary.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
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
    /// The class PointChart represents a selectable point chart
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
    [TemplatePart(Name = "PART_DrawingArea", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Line", Type = typeof(Polyline))]
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(PointChartItem))]
    [Description("The class PointChart represents a selectable point chart")]
    public class PointChart : ListBox
    {
        #region Members

        private bool internalIsLoaded = false;

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Category description.
        /// </summary>
        private const string PROPERTY_CATEGORY = "Customer PointChart Properties";

        /// <summary>
        /// LineBrushProperty - Gets or sets the line brush.
        /// </summary>
        public static readonly DependencyProperty LineBrushProperty = DependencyProperty.Register("LineBrush", typeof(Brush), typeof(PointChart), new FrameworkPropertyMetadata(Brushes.Black));

        /// <summary>
        /// LineThicknessProperty - Gets or sets the line thickness.
        /// </summary>
        public static readonly DependencyProperty LineThicknessProperty = DependencyProperty.Register("LineThickness", typeof(double), typeof(PointChart), new FrameworkPropertyMetadata(1.0));

        /// <summary>
        /// XMinimumProperty - Gets or sets the XMinimum.
        /// </summary>
        public static readonly DependencyProperty XMinimumProperty = DependencyProperty.Register("XMinimum", typeof(double), typeof(PointChart), new FrameworkPropertyMetadata(0.0, RangeChanged));

        /// <summary>
        /// XMaximumProperty - Gets or sets the XMaximum.
        /// </summary>
        public static readonly DependencyProperty XMaximumProperty = DependencyProperty.Register("XMaximum", typeof(double), typeof(PointChart), new FrameworkPropertyMetadata(100.0, RangeChanged));

        /// <summary>
        /// YMinimumProperty - Gets or sets the YMinimum.
        /// </summary>
        public static readonly DependencyProperty YMinimumProperty = DependencyProperty.Register("YMinimum", typeof(double), typeof(PointChart), new FrameworkPropertyMetadata(0.0, RangeChanged));

        /// <summary>
        /// YMaximumProperty - Gets or sets the YMaximum.
        /// </summary>
        public static readonly DependencyProperty YMaximumProperty = DependencyProperty.Register("YMaximum", typeof(double), typeof(PointChart), new FrameworkPropertyMetadata(100.0, RangeChanged));

        /// <summary>
        /// SelectionChangedCommandProperty - Gets or sets the selection changed command.
        /// </summary>
        public static readonly DependencyProperty SelectionChangedCommandProperty = DependencyProperty.Register("SelectionChangedCommand", typeof(ICommand), typeof(PointChart), new FrameworkPropertyMetadata(null));

        #endregion

        #region PropertyChangedCallbacks

        /// <summary>
        /// Recalc position
        /// </summary>
        /// <param name="d">PointChart</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void RangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PointChart self = d as PointChart;

            if (!self.internalIsLoaded)
                return;

            self.UpdateAllPoints();
        }

        #endregion

        #region Events



        #endregion

        #region Handlers



        #endregion

        #region ClassHandlers

        /// <summary>
        /// Position PointChartItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnItemUnitPointChangedEvent(object sender, RoutedEventArgs e)
        {
            PointChart pointChart = sender as PointChart;
            PointChartItem pointChartItem = e.OriginalSource as PointChartItem;

            pointChart.UpdatePosition(pointChartItem, pointChart.ItemContainerGenerator.IndexFromContainer(pointChartItem));
        }

        #endregion

        #region Ctor

        /// <summary>
        /// public ctor
        /// </summary>
        public PointChart()
        {
            //this.Loaded += PointChartLoaded;

            this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (internalIsLoaded)
                        return;

                    internalIsLoaded = true;

                    Init();
                }), DispatcherPriority.Loaded);
        }

        /// <summary>
        /// static ctor
        /// </summary>
        static PointChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PointChart), new FrameworkPropertyMetadata(typeof(PointChart)));

            EventManager.RegisterClassHandler(typeof(PointChart), PointChartItem.UnitPointChangedEvent, new RoutedEventHandler(OnItemUnitPointChangedEvent));
        }

        #endregion

        #region Init/Cleanup

        /// <summary>
        /// init
        /// </summary>
        private void Init()
        {
            if (!internalIsLoaded)
                return;

            UpdateAllPoints();
        }

        #endregion

        #region OverrideMethods

        /// <summary>
        /// Get Template PART_-Elements
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PartDrawingArea = GetTemplateChild("PART_DrawingArea") as FrameworkElement;
            PartLine = GetTemplateChild("PART_Line") as Polyline;

            Init();
        }
        
        /// <summary>
        /// Draw Line
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if(PartLine == null)
                return;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    if(e.OldStartingIndex < PartLine.Points.Count)
                        PartLine.Points.RemoveAt(e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Add:
                    PartLine.Points.Insert(e.NewStartingIndex, new Point());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    PartLine.Points.Clear();
                    for (int i = 0; i < Items.Count; i++)
                        PartLine.Points.Add(new Point());
                    break;
            }
        }

        /// <summary>
        /// Recalc PixelPoints
        /// </summary>
        /// <param name="sizeInfo"></param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            if (!internalIsLoaded)
                return;

            UpdateAllPoints();
        }

        /// <summary>
        /// IsItemItsOwnContainerOverride
        /// </summary>
        /// <param name="item">object</param>
        /// <returns>bool</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is PointChartItem;
        }

        /// <summary>
        /// create new pointchartitem
        /// </summary>
        /// <returns>PointChartItem</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new PointChartItem();
        }

        /// <summary>
        /// execute selectionchangedcommand
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (SelectionChangedCommand == null)
                return;

            SelectionChangedCommand.Execute(SelectedItem);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update point chart item position
        /// </summary>
        /// <param name="pointChartItem">PointChartItem</param>
        private void UpdatePosition(PointChartItem pointChartItem, int index)
        {
            if (PartDrawingArea == null || !internalIsLoaded)
                return;

            //Debug.WriteLine("UpdateSingle");

            Point newPos = MathHelpers.ConvertUnitToPixelPoint(
                new Point(pointChartItem.XUnitValue, pointChartItem.YUnitValue), 
                new CommonLibrary.Util.DrawingContext() 
                { 
                    DrawingSize = PartDrawingArea.RenderSize, 
                    XMaximum = XMaximum, 
                    XMinimum = XMinimum, 
                    YMaximum = YMaximum, 
                    YMinimum = YMinimum });

            Canvas.SetLeft(pointChartItem, newPos.X);
            Canvas.SetTop(pointChartItem, newPos.Y);

            if (PartLine == null)
                return;

            if(index < PartLine.Points.Count)
                PartLine.Points.RemoveAt(index);

            PartLine.Points.Insert(index, newPos);

            //Debug.WriteLine("Draw");
        }

        /// <summary>
        /// update all points
        /// </summary>
        private void UpdateAllPoints()
        {
            //Debug.WriteLine("UpdateAll");
            for (int i = 0; i < Items.Count; i++)
            {
                var container = ItemContainerGenerator.ContainerFromIndex(i) as PointChartItem;

                UpdatePosition(container, i);
            }
            //Debug.WriteLine("------------------------------");
        }

        #endregion

        #region Properties

        /// <summary>
        /// PART_DrawingArea
        /// </summary>
        protected FrameworkElement PartDrawingArea
        {
            get;
            private set;
        }

        /// <summary>
        /// PART_Line
        /// </summary>
        protected Polyline PartLine
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the line brush.
        /// </summary>
        [Description("Gets or sets the line brush."), Category(PROPERTY_CATEGORY)]
        public Brush LineBrush
        {
            get { return (Brush)GetValue(LineBrushProperty); }
            set { SetValue(LineBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the line thickness.
        /// </summary>
        [Description("Gets or sets the line thickness."), Category(PROPERTY_CATEGORY)]
        public double LineThickness
        {
            get { return (double)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the XMinimum.
        /// </summary>
        [Description("Gets or sets the XMinimum."), Category(PROPERTY_CATEGORY)]
        public double XMinimum
        {
            get { return (double)GetValue(XMinimumProperty); }
            set { SetValue(XMinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the XMaximum.
        /// </summary>
        [Description("Gets or sets the XMaximum."), Category(PROPERTY_CATEGORY)]
        public double XMaximum
        {
            get { return (double)GetValue(XMaximumProperty); }
            set { SetValue(XMaximumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the YMinimum.
        /// </summary>
        [Description("Gets or sets the YMinimum."), Category(PROPERTY_CATEGORY)]
        public double YMinimum
        {
            get { return (double)GetValue(YMinimumProperty); }
            set { SetValue(YMinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the YMaximum.
        /// </summary>
        [Description("Gets or sets the YMaximum."), Category(PROPERTY_CATEGORY)]
        public double YMaximum
        {
            get { return (double)GetValue(YMaximumProperty); }
            set { SetValue(YMaximumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selection changed command.
        /// </summary>
        [Description("Gets or sets the selection changed command."), Category(PROPERTY_CATEGORY)]
        public ICommand SelectionChangedCommand
        {
            get { return (ICommand)GetValue(SelectionChangedCommandProperty); }
            set { SetValue(SelectionChangedCommandProperty, value); }
        }

        #endregion
    }
}
