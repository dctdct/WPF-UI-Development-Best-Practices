using System;
using System.Collections.Generic;
using System.ComponentModel;
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
namespace CCLibrary.Controls.HighlightableTextBlock
{
    /// <summary>
    /// <para>
    /// The class HighlightableTextBlock represents a highlightable Text Block
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
    /// <para>Date: 09.10.2013</para>
    /// </summary>
    public class HighlightableTextBlock : Control
    {
        #region Members
        
        #endregion

        #region DependencyProperties

        /// <summary>
        /// Category description.
        /// </summary>
        private const string PROPERTY_CATEGORY = "Customer HighlightableTextBlock Properties";

        /// <summary>
        /// TextProperty - Gets or sets the text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HighlightableTextBlock), new FrameworkPropertyMetadata(string.Empty, OnHighlightingPropertyChanged));

        /// <summary>
        /// HighlightStringProperty - Gets or sets the the highlight string.
        /// </summary>
        public static readonly DependencyProperty HighlightStringProperty = DependencyProperty.Register("HighlightString", typeof(string), typeof(HighlightableTextBlock), new FrameworkPropertyMetadata(string.Empty, OnHighlightingPropertyChanged));

        /// <summary>
        /// HighlightFontWeightProperty - Gets or sets the highlight font weight.
        /// </summary>
        public static readonly DependencyProperty HighlightFontWeightProperty = DependencyProperty.Register("HighlightFontWeight", typeof(FontWeight), typeof(HighlightableTextBlock), new FrameworkPropertyMetadata(FontWeights.Bold));

        /// <summary>
        /// HighlightFontStyleProperty - Gets or sets the highlight font style.
        /// </summary>
        public static readonly DependencyProperty HighlightFontStyleProperty = DependencyProperty.Register("HighlightFontStyle", typeof(FontStyle), typeof(HighlightableTextBlock), new FrameworkPropertyMetadata(FontStyles.Normal));

        /// <summary>
        /// HighlightForegroundProperty - Gets or sets the highlight foreground.
        /// </summary>
        public static readonly DependencyProperty HighlightForegroundProperty = DependencyProperty.Register("HighlightForeground", typeof(Brush), typeof(HighlightableTextBlock), new FrameworkPropertyMetadata(Brushes.Blue));

        /// <summary>
        /// Key for FirstSubStringProperty.
        /// </summary>
        public static DependencyPropertyKey FirstSubStringPropertyKey = DependencyProperty.RegisterReadOnly("FirstSubString", typeof(string), typeof(HighlightableTextBlock), new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// FirstSubStringProperty - Gets or sets the first sub string (not highlighted).
        /// </summary>
        public static readonly DependencyProperty FirstSubStringProperty = FirstSubStringPropertyKey.DependencyProperty;

        /// <summary>
        /// Key for MidSubStringProperty.
        /// </summary>
        public static DependencyPropertyKey MidSubStringPropertyKey = DependencyProperty.RegisterReadOnly("MidSubString", typeof(string), typeof(HighlightableTextBlock), new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// MidSubStringProperty - Gets or sets the mid sub string (highlighted).
        /// </summary>
        public static readonly DependencyProperty MidSubStringProperty = MidSubStringPropertyKey.DependencyProperty;

        /// <summary>
        /// Key for LastSubStringProperty.
        /// </summary>
        public static DependencyPropertyKey LastSubStringPropertyKey = DependencyProperty.RegisterReadOnly("LastSubString", typeof(string), typeof(HighlightableTextBlock), new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// LastSubStringProperty - Gets or sets the last sub string (not highlighted).
        /// </summary>
        public static readonly DependencyProperty LastSubStringProperty = LastSubStringPropertyKey.DependencyProperty;

        #endregion

        #region AttachedProperties

        /// <summary>
        /// AttachedHighlightStringProperty - Gets or sets the highlight string.
        /// </summary>
        public static readonly DependencyProperty AttachedHighlightStringProperty = DependencyProperty.RegisterAttached("AttachedHighlightString", typeof(string), typeof(HighlightableTextBlock), new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// Gets the AttachedHighlightString.
        /// </summary>
        [Description("Gets the AttachedHighlightString."), Category(PROPERTY_CATEGORY)]
        public static string GetAttachedHighlightString(UIElement element)
        {
            return (string)element.GetValue(AttachedHighlightStringProperty);
        }

        /// <summary>
        /// Sets the AttachedHighlightString.
        /// </summary>
        [Description("Sets the AttachedHighlightString."), Category(PROPERTY_CATEGORY)]
        public static void SetAttachedHighlightString(UIElement element, string value)
        {
            element.SetValue(AttachedHighlightStringProperty, value);
        }

        #endregion

        #region Ctor

        /// <summary>
        /// static ctor
        /// </summary>
        static HighlightableTextBlock()
        { 
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HighlightableTextBlock), new FrameworkPropertyMetadata(typeof(HighlightableTextBlock)));
        }

        #endregion

        #region PropertyChangedCallbacks

        /// <summary>
        /// UpdateHighlighting
        /// </summary>
        /// <param name="d">HighlightableTextBlock</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void OnHighlightingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HighlightableTextBlock self = d as HighlightableTextBlock;
            
            self.UpdateHighlighting();
        }

        /// <summary>
        /// Update highlighting
        /// </summary>
        private void UpdateHighlighting()
        {
            FirstSubString = string.Empty;
            MidSubString = string.Empty;
            LastSubString = string.Empty;

            if (string.IsNullOrEmpty(Text) || string.IsNullOrEmpty(HighlightString))
            {
                FirstSubString = Text;
            }
            else
            {
                int index = Text.IndexOf(HighlightString, StringComparison.InvariantCultureIgnoreCase);
                int count = HighlightString.Length;

                if (index < 0)
                {
                    FirstSubString = Text;
                    return;
                }

                if (index > 0)
                {
                    FirstSubString = Text.Substring(0, index);
                }

                MidSubString = Text.Substring(index, count);

                if (index + count < Text.Length)
                {
                    LastSubString = Text.Substring(index + count, Text.Length - (index + count));
                }
            }

        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text property.
        /// </summary>
        [Description("Gets or sets the text property."), Category(PROPERTY_CATEGORY)]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the the highlight string.
        /// </summary>
        [Description("Gets or sets the the highlight string."), Category(PROPERTY_CATEGORY)]
        public string HighlightString
        {
            get { return (string)GetValue(HighlightStringProperty); }
            set { SetValue(HighlightStringProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highlight font weight.
        /// </summary>
        [Description("Gets or sets the highlight font weight."), Category(PROPERTY_CATEGORY)]
        public FontWeight HighlightFontWeight
        {
            get { return (FontWeight)GetValue(HighlightFontWeightProperty); }
            set { SetValue(HighlightFontWeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highlight font style.
        /// </summary>
        [Description("Gets or sets the highlight font style."), Category(PROPERTY_CATEGORY)]
        public FontStyle HighlightFontStyle
        {
            get { return (FontStyle)GetValue(HighlightFontStyleProperty); }
            set { SetValue(HighlightFontStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highlight foreground.
        /// </summary>
        [Description("Gets or sets the highlight foreground."), Category(PROPERTY_CATEGORY)]
        public Brush HighlightForeground
        {
            get { return (Brush)GetValue(HighlightForegroundProperty); }
            set { SetValue(HighlightForegroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the first sub string (not highlighted).
        /// </summary>
        [Description("Gets or sets the first sub string (not highlighted)."), Category(PROPERTY_CATEGORY)]
        public string FirstSubString
        {
            get { return (string)GetValue(FirstSubStringProperty); }
            private set { SetValue(FirstSubStringPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the mid sub string (highlighted).
        /// </summary>
        [Description("Gets or sets the mid sub string (highlighted)."), Category(PROPERTY_CATEGORY)]
        public string MidSubString
        {
            get { return (string)GetValue(MidSubStringProperty); }
            private set { SetValue(MidSubStringPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the last sub string (not highlighted).
        /// </summary>
        [Description("Gets or sets the last sub string (not highlighted)."), Category(PROPERTY_CATEGORY)]
        public string LastSubString
        {
            get { return (string)GetValue(LastSubStringProperty); }
            private set { SetValue(LastSubStringPropertyKey, value); }
        }

        #endregion
    }
}
