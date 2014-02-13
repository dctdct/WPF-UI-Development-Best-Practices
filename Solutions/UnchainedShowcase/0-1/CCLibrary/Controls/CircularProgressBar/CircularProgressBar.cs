using CommonLibrary.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/*
* This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
* DO NOT ALTER OR REMOVE  THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
* 
* This file and the code contained in it are subject to the agreed contractual terms and conditions,
* in particular with regard to resale and publication.
*/
namespace CCLibrary.Controls.CircularProgressBar
{
    /// <summary>
    /// <para>
    /// The class CircularProgressBar represents a circular progress bar
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
    /// <para>Date: 25.09.2013</para>
    /// </summary>
    public class CircularProgressBar : ProgressBar
    {
        #region Members

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Category description.
        /// </summary>
        private const string PROPERTY_CATEGORY = "Customer CircularProgressBar Properties";

        /// <summary>
        /// Key for ValueAsAngleProperty.
        /// </summary>
        public static DependencyPropertyKey ValueAsAnglePropertyKey = DependencyProperty.RegisterReadOnly("ValueAsAngle", typeof(double), typeof(CircularProgressBar), new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// ValueAsAngleProperty - Gets or sets the.
        /// </summary>
        public static readonly DependencyProperty ValueAsAngleProperty = ValueAsAnglePropertyKey.DependencyProperty;

        /// <summary>
        /// ProgressBackgroundProperty - Gets or sets the ProgressBackground.
        /// </summary>
        public static readonly DependencyProperty ProgressBackgroundProperty = DependencyProperty.Register("ProgressBackground", typeof(Brush), typeof(CircularProgressBar), new FrameworkPropertyMetadata(null));

        #endregion

        #region Ctor

        /// <summary>
        /// static ctor
        /// </summary>
        static CircularProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircularProgressBar), new FrameworkPropertyMetadata(typeof(CircularProgressBar)));
        }

        #endregion

        #region OverrideMethods

        /// <summary>
        /// Calc Value as Angle
        /// </summary>
        /// <param name="oldValue">oldValue</param>
        /// <param name="newValue">newValue</param>
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            UpdateValueAsAngle();
        }

        /// <summary>
        /// Calc Value as Angle
        /// </summary>
        /// <param name="oldMaximum">oldMaximum</param>
        /// <param name="newMaximum">newMaximum</param>
        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);

            UpdateValueAsAngle();
        }

        /// <summary>
        /// Calc Value As Angle
        /// </summary>
        /// <param name="oldMinimum">oldMinimum</param>
        /// <param name="newMinimum">newMinimum</param>
        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);

            UpdateValueAsAngle();
        }

        /// <summary>
        /// Recalc Value As Angle
        /// </summary>
        private void UpdateValueAsAngle()
        {
            ValueAsAngle = MathHelpers.ValueAsAngle(Minimum, Maximum, Value);
        }

        #endregion

        #region Methods



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the.
        /// </summary>
        [Description("Gets or sets the."), Category(PROPERTY_CATEGORY)]
        public double ValueAsAngle
        {
            get { return (double)GetValue(ValueAsAngleProperty); }
            private set { SetValue(ValueAsAnglePropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the ProgressBackground.
        /// </summary>
        [Description("Gets or sets the ProgressBackground."), Category(PROPERTY_CATEGORY)]
        public Brush ProgressBackground
        {
            get { return (Brush)GetValue(ProgressBackgroundProperty); }
            set { SetValue(ProgressBackgroundProperty, value); }
        }

        #endregion
    }
}
