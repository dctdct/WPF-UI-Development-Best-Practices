using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;

/*
* This file has been created by ERGOSIGN GmbH - All rights reserved - http://www.ergosign.de
* DO NOT ALTER OR REMOVE  THIS COPYRIGHT NOTICE OR THIS FILE HEADER.
* 
* This file and the code contained in it are subject to the agreed contractual terms and conditions,
* in particular with regard to resale and publication.
*/
namespace CommonLibrary.Util
{
    /// <summary>
    /// <para>
    /// The class MathHelpers represents a collection of math helper methods
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
    /// <para>Date: 09.11.2013</para>
    /// </summary>
    public static class MathHelpers
    {
        /// <summary>
        /// Converts an unit point to a pixel point (given the current drawing context)
        /// </summary>
        /// <param name="unitPoint">UnitValuePoint</param>
        /// <param name="drawingContext">DrawingContext</param>
        /// <returns>PixelValuePoint</returns>
        public static Point ConvertUnitToPixelPoint(Point unitPoint, DrawingContext drawingContext)
        {
            Point pixelPoint = new Point();

            pixelPoint.X = drawingContext.DrawingSize.Width / (drawingContext.XMaximum - drawingContext.XMinimum) * (unitPoint.X - drawingContext.XMinimum);
            pixelPoint.Y = drawingContext.DrawingSize.Height / (drawingContext.YMaximum - drawingContext.YMinimum) * (unitPoint.Y - drawingContext.YMinimum);

            return pixelPoint;
        }

        /// <summary>
        /// Converts an unit point collection to a pixel point collection (given the current drawing context)
        /// </summary>
        /// <param name="unitPoints">UnitValuePointCollection</param>
        /// <param name="drawingContext">DrawingContext</param>
        /// <returns>PixelValuePointCollection</returns>
        public static PointCollection ConvertUnitToPixelPointCollection(IEnumerable<Point> unitPoints, DrawingContext drawingContext)
        {
            PointCollection pixelPoints = new PointCollection();

            foreach (var unitPoint in unitPoints)
            {
                pixelPoints.Add(ConvertUnitToPixelPoint(unitPoint, drawingContext));
            }

            return pixelPoints;
        }
    }

    /// <summary>
    /// DrawingContextStruct
    /// </summary>
    public struct DrawingContext
    {
        public Size DrawingSize;
        public double XMinimum;
        public double XMaximum;
        public double YMinimum;
        public double YMaximum;
    }
}
