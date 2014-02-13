using CommonLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// The class ValueAsAngle represents the value as a angle
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
    public class ValueAsAngleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Count() != 3)
                return 0.0;

            double min, max, value = 0.0;

            if (values[0] == null || values[1] == null || values[2] == null)
                return 0.0;

            if (!double.TryParse(values[0].ToString(), out min) || 
                !double.TryParse(values[1].ToString(), out max) || 
                !double.TryParse(values[2].ToString(), out value))
                return 0.0;

            return MathHelpers.ValueAsAngle(min, max, value);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
