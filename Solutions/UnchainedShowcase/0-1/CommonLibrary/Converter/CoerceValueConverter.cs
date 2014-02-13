using CommonLibrary.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

/*
* This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
* DO NOT ALTER OR REMOVE  THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
* 
* This file and the code contained in it are subject to the agreed contractual terms and conditions,
* in particular with regard to resale and publication.
*/
namespace CommonLibrary.Converter
{
    /// <summary>
    /// <para>
    /// The class CoerceValueConverter represents the value in the specified tick frequency and range
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
    public class CoerceValueConverter : IMultiValueConverter
    {
        /*
        public object Convert(object values[], Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(parameter is Slider))
                return value;

            Slider slider = parameter as Slider;

            slider.Value = MathHelpers.ValueInTickFrequencyAndRange(slider.Minimum, slider.Maximum, slider.TickFrequency, slider.Value);

            return slider.Value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(parameter is Slider))
                return value;

            Slider slider = parameter as Slider;

            slider.Value = MathHelpers.ValueInTickFrequencyAndRange(slider.Minimum, slider.Maximum, slider.TickFrequency, slider.Value);

            return parameter;
        }*/

        private Slider slider;
        private TextBox textBox;
        private bool internalUpdate = false;
    
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (internalUpdate)
                return slider.Value.ToString();

            Debug.WriteLine("Convert");

            double min, max, tickFrequency, value = 0.0;

            if (values[0] == null || values[1] == null || values[2] == null || values[3] == null || !(values[4] is Slider))
                return "0.0";

            slider = values[4] as Slider;
            textBox = values[5] as TextBox;

            if (!double.TryParse(values[0].ToString(), out min) || !double.TryParse(values[1].ToString(), out max)|| !double.TryParse(values[2].ToString(), out tickFrequency ) || !double.TryParse(values[3].ToString(), out value))
                return "0.0";

            internalUpdate = true;
            slider.Value = MathHelpers.ValueInTickFrequencyAndRange(min, max, tickFrequency, value);
            internalUpdate = false;

            return slider.Value.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            Debug.WriteLine("ConvertBack");
            object[] values = new object[targetTypes.Count()];
            values[0] = slider.Minimum;
            values[1] = slider.Maximum;
            values[2] = slider.TickFrequency;

            double doubleValue;

            if (value != null && double.TryParse(value.ToString(), out doubleValue))
                values[3] = MathHelpers.ValueInTickFrequencyAndRange(slider.Minimum, slider.Maximum, slider.TickFrequency, doubleValue);
            else
                values[3] = MathHelpers.ValueInTickFrequencyAndRange(slider.Minimum, slider.Maximum, slider.TickFrequency, slider.Value);

            return values;

            //return Array.ConvertAll<Type, Object>(targetTypes, t => value);
        }
    }
}
