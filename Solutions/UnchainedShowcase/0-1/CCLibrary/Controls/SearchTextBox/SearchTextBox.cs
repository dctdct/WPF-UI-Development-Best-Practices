using Microsoft.Windows.Design.PropertyEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

/*
* This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
* DO NOT ALTER OR REMOVE  THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
* 
* This file and the code contained in it are subject to the agreed contractual terms and conditions,
* in particular with regard to resale and publication.
*/
namespace CCLibrary.Controls.SearchTextBox
{
    /// <summary>
    /// <para>
    /// The class SearchTextBox represents a textbox with watermark and clear text command
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
    [Description("The class SearchTextBox represents a textbox with watermark and clear text command")]
    public class SearchTextBox : TextBox 
    {
        #region Members



        #endregion

        #region DependencyProperties

        /// <summary>
        /// Category description.
        /// </summary>
        private const string PROPERTY_CATEGORY = "Customer SearchTextBox Properties";

        /// <summary>
        /// WatermarkProperty - Gets or sets the watermark.
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(SearchTextBox), new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// WatermarkFontStyleProperty - Gets or sets the watermark font style.
        /// </summary>
        public static readonly DependencyProperty WatermarkFontStyleProperty = DependencyProperty.Register("WatermarkFontStyle", typeof(FontStyle), typeof(SearchTextBox), new FrameworkPropertyMetadata(FontStyles.Normal));

        /// <summary>
        /// WatermarkFontWeightProperty - Gets or sets the watermark font weight.
        /// </summary>
        public static readonly DependencyProperty WatermarkFontWeightProperty = DependencyProperty.Register("WatermarkFontWeight", typeof(FontWeight), typeof(SearchTextBox), new FrameworkPropertyMetadata(FontWeights.Bold));

        /// <summary>
        /// WatermarkForegroundProperty - Gets or sets the watermark foreground.
        /// </summary>
        public static readonly DependencyProperty WatermarkForegroundProperty = DependencyProperty.Register("WatermarkForeground", typeof(Brush), typeof(SearchTextBox), new FrameworkPropertyMetadata(Brushes.Black));

        #endregion

        #region PropertyChangedCallbacks



        #endregion

        #region Events



        #endregion

        #region Commands

		/// <summary>
		/// ClearTextCommand - This command clears the text property.
		/// </summary>
		public static readonly RoutedCommand ClearTextCommand = new RoutedCommand("ClearTextCommand", typeof(SearchTextBox));

		/// <summary>
		/// OnClearTextCommand handler.
		/// </summary>
		/// <param name="sender">SearchTextBox</param>
		/// <param name="e">ExecutedRoutedEventArgs</param>
        private static void ExecuteClearTextCommand(object sender, ExecutedRoutedEventArgs e)
		{
			SearchTextBox self = sender as SearchTextBox;
			self.Clear();
		}

        /// <summary>
        /// CanExecuteClearCommand handler.
        /// </summary>
        /// <param name="sender">SearchTextBox</param>
        /// <param name="e">CanExecuteRoutedEventArgs</param>
        private static void CanExecuteClearCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            SearchTextBox self = sender as SearchTextBox;
            e.CanExecute = self.CanClearText();
        }

        #endregion

        #region Handlers



        #endregion

        #region Ctor

        /// <summary>
        /// static ctor
        /// </summary>
        static SearchTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchTextBox), new FrameworkPropertyMetadata(typeof(SearchTextBox)));
            CommandManager.RegisterClassCommandBinding(typeof(SearchTextBox), new CommandBinding(ClearTextCommand, ExecuteClearTextCommand, CanExecuteClearCommand));
        }

        #endregion

        #region Init/Cleanup



        #endregion

        #region OverrideMethods

        /// <summary>
        /// OnTextChanged
        /// </summary>
        /// <param name="e">TextChangedEventArgs</param>
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            // for even faster can execute change
            //CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Clear on ESC
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Escape)
                Clear();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Can Clear the text property
        /// </summary>
        /// <returns>bool</returns>
        private bool CanClearText()
        {
            return !string.IsNullOrEmpty(Text);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the watermark.
        /// </summary>
        [Description("Gets or sets the watermark."), Category(PROPERTY_CATEGORY)]
        [AlternateContentProperty]
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        /// <summary>
        /// Gets or sets the watermark font style.
        /// </summary>
        [Description("Gets or sets the watermark font style."), Category(PROPERTY_CATEGORY)]
        public FontStyle WatermarkFontStyle
        {
            get { return (FontStyle)GetValue(WatermarkFontStyleProperty); }
            set { SetValue(WatermarkFontStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the watermark font weight.
        /// </summary>
        [Description("Gets or sets the watermark font weight."), Category(PROPERTY_CATEGORY)]
        public FontWeight WatermarkFontWeight
        {
            get { return (FontWeight)GetValue(WatermarkFontWeightProperty); }
            set { SetValue(WatermarkFontWeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the watermark foreground.
        /// </summary>
        [Description("Gets or sets the watermark foreground."), Category(PROPERTY_CATEGORY)]
        public Brush WatermarkForeground
        {
            get { return (Brush)GetValue(WatermarkForegroundProperty); }
            set { SetValue(WatermarkForegroundProperty, value); }
        }

        #endregion
    }
}
