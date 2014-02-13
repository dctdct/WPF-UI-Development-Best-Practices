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
    public static class MathHelpers
    {
        public static Point ConvertUnitToPixelPoint(Point unitPoint, DrawingContext drawingContext)
        {
            Point pixelPoint = new Point();

            pixelPoint.X = drawingContext.DrawingSize.Width / (drawingContext.XMaximum - drawingContext.XMinimum) * (unitPoint.X - drawingContext.XMinimum);
            pixelPoint.Y = drawingContext.DrawingSize.Height / (drawingContext.YMaximum - drawingContext.YMinimum) * (unitPoint.Y - drawingContext.YMinimum);

            return pixelPoint;
        }

        public static PointCollection ConvertUnitToPixelPointCollection(IEnumerable<Point> unitPoints, DrawingContext drawingContext)
        {
            PointCollection pixelPoints = new PointCollection();

            foreach (var unitPoint in unitPoints)
            {
                pixelPoints.Add(ConvertUnitToPixelPoint(unitPoint, drawingContext));
            }

            return pixelPoints;
        }

        public static double ValueAsAngle(double minimum, double maximum, double value)
        {
            return Math.Max(0, Math.Min(((value - minimum) / (maximum - minimum)) * 360.0, 360.0));
        }

        public static double ValueInTickFrequencyAndRange(double minimum, double maximum, double tickFrequency, double value)
        {
            return Math.Max(minimum, Math.Min(NearestMultiple(value, tickFrequency), maximum));
        }

        public static double NearestMultiple(double value, double div)
        {
            return (int)Math.Round((value / div)) * div;
        }
    }

    public struct DrawingContext
    {
        public Size DrawingSize;
        public double XMinimum;
        public double XMaximum;
        public double YMinimum;
        public double YMaximum;
    }
}
