using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

/*
* This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
* DO NOT ALTER OR REMOVE  THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
* 
* This file and the code contained in it are subject to the agreed contractual terms and conditions,
* in particular with regard to resale and publication.
*/
namespace CCDevStyling.AttachedProperties
{
    /// <summary>
    /// <para>
    /// The class AttachedProperties represents a collection of attached properties
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
    public class AttachedProperties
    {
        #region Members



        #endregion

        #region DependencyProperties

        /// <summary>
        /// Category description.
        /// </summary>
        private const string PROPERTY_CATEGORY = "Customer AttachedProperties Properties";

        /// <summary>
        /// WatermarkProperty - Gets or sets the watermark.
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(AttachedProperties), new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// WatermarkFontStyleProperty - Gets or sets the watermark font style.
        /// </summary>
        public static readonly DependencyProperty WatermarkFontStyleProperty = DependencyProperty.RegisterAttached("WatermarkFontStyle", typeof(FontStyle), typeof(AttachedProperties), new FrameworkPropertyMetadata(FontStyles.Italic));

        /// <summary>
        /// IsSearchResultProperty - Gets or sets the is search result state.
        /// </summary>
        public static readonly DependencyProperty IsSearchResultProperty = DependencyProperty.RegisterAttached("IsSearchResult", typeof(bool?), typeof(AttachedProperties), new FrameworkPropertyMetadata(null));
        
        #endregion

        #region PropertyChangedCallbacks



        #endregion

        #region Events



        #endregion

        #region Handlers



        #endregion

        #region Ctor



        #endregion

        #region Init/Cleanup



        #endregion

        #region OverrideMethods



        #endregion

        #region Methods



        #endregion

        #region Properties

        /// <summary>
        /// Gets the Watermark.
        /// </summary>
        [Description("Gets the Watermark."), Category(PROPERTY_CATEGORY)]
        public static string GetWatermark(UIElement element)
        {
            return (string)element.GetValue(WatermarkProperty);
        }

        /// <summary>
        /// Sets the Watermark.
        /// </summary>
        [Description("Sets the Watermark."), Category(PROPERTY_CATEGORY)]
        public static void SetWatermark(UIElement element, string value)
        {
            element.SetValue(WatermarkProperty, value);
        }

        /// <summary>
        /// Gets the WatermarkFontStyle.
        /// </summary>
        [Description("Gets the WatermarkFontStyle."), Category(PROPERTY_CATEGORY)]
        public static FontStyle GetWatermarkFontStyle(UIElement element)
        {
            return (FontStyle)element.GetValue(WatermarkFontStyleProperty);
        }

        /// <summary>
        /// Sets the WatermarkFontStyle.
        /// </summary>
        [Description("Sets the WatermarkFontStyle."), Category(PROPERTY_CATEGORY)]
        public static void SetWatermarkFontStyle(UIElement element, FontStyle value)
        {
            element.SetValue(WatermarkFontStyleProperty, value);
        }

        /// <summary>
        /// Gets the IsSearchResult.
        /// </summary>
        [Description("Gets the IsSearchResult."), Category(PROPERTY_CATEGORY)]
        public static bool? GetIsSearchResult(UIElement element)
        {
            return (bool?)element.GetValue(IsSearchResultProperty);
        }

        /// <summary>
        /// Sets the IsSearchResult.
        /// </summary>
        [Description("Sets the IsSearchResult."), Category(PROPERTY_CATEGORY)]
        public static void SetIsSearchResult(UIElement element, bool? value)
        {
            element.SetValue(IsSearchResultProperty, value);
        }

        #endregion
    }
}
