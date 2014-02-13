using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

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
    /// The class LoadingBehaviorAdorner represents a simple adorner layer
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
    public class LoadingBehaviorAdorner : Adorner
    {
        #region Members

        private List<UIElement> logicalChilds;
        private ContentControl contentControl;

        #endregion

        #region PropertyChangedCallbacks



        #endregion

        #region Events



        #endregion

        #region Handlers



        #endregion

        #region Ctor

        /// <summary>
        /// public ctor
        /// </summary>
        /// <param name="adornedElement">UIElement</param>
        public LoadingBehaviorAdorner(UIElement adornedElement) : base(adornedElement)
        {
            contentControl = new ContentControl() { Background = Brushes.Transparent };

            logicalChilds = new List<UIElement>(1);
            logicalChilds.Add(contentControl);

            this.AddVisualChild(contentControl);
            this.AddLogicalChild(contentControl);
        }

        #endregion

        #region Init/Cleanup



        #endregion

        #region OverrideMethods

        /// <summary>
        /// Arrange children.
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            contentControl.Arrange(new Rect(finalSize));
            return finalSize;
        }

        /// <summary>
        /// Get the visual children count.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return logicalChilds.Count; }
        }

        /// <summary>
        /// Get children by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            return logicalChilds[index];
        }

        /// <summary>
        /// Enumerator for logical children.
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get { return logicalChilds.GetEnumerator(); }
        }

        #endregion

        #region Methods



        #endregion

        #region Properties

        /// <summary>
        /// DataContextProperty - Gets or sets the data context.
        /// </summary>
        public object ContentDataContext
        {
            get { return contentControl.DataContext; }
            set { contentControl.DataContext = value; }
        }

        /// <summary>
        /// ContentProperty - Gets or sets the content.
        /// </summary>
        public object Content
        {
            get { return contentControl.Content; }
            set { contentControl.Content = value; }
        }

        /// <summary>
        /// ContentTemplateProperty - Gets or sets the content template.
        /// </summary>
        public DataTemplate ContentTemplate
        {
            get { return contentControl.ContentTemplate; }
            set { contentControl.ContentTemplate = value; }
        }

        #endregion
    }
}
