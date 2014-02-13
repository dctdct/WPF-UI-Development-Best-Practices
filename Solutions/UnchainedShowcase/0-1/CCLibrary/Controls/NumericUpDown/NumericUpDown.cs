using CommonLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CCLibrary.Controls.NumericUpDown
{
    [TemplatePart(Name="PART_TextBox", Type=typeof(TextBox))]
    public class NumericUpDown : Slider
    {
        private bool internalIsLoaded;

        private static object CoerceValueProperty(DependencyObject d, object baseValue)
        {
            NumericUpDown self = d as NumericUpDown;

            return self.InternalCoerceValueProperty((double)baseValue);
        }

        public NumericUpDown()
        {
            //this.Loaded += NumericUpDown_Loaded;

            this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (internalIsLoaded)
                        return;

                    internalIsLoaded = true;
                    InternalCoerceValueProperty();
                }), DispatcherPriority.Loaded);
        }

        static NumericUpDown()
        {
            ValueProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(0.0, null, CoerceValueProperty));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PartTextBox = GetTemplateChild("PART_TextBox") as TextBox;
            PartTextBox.TextChanged += PartTextBox_TextChanged;
        }

        /*
        void NumericUpDown_Loaded(object sender, RoutedEventArgs e)
        {
            if (internalIsLoaded)
                return;

            internalIsLoaded = true;
            InternalCoerceValueProperty();
        }*/

        void PartTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double doubleValue;

            if (string.IsNullOrEmpty(PartTextBox.Text))
            {
                //PartTextBox.Text = Minimum.ToString();
                //PartTextBox.CaretIndex = PartTextBox.Text.Length;
                return;
            }

            if (!double.TryParse(PartTextBox.Text, out doubleValue))
            {
                PartTextBox.Text = Value.ToString();
                PartTextBox.CaretIndex = PartTextBox.Text.Length;
            }
            else
            {
                Value = InternalCoerceValueProperty(doubleValue);
            }
        }

        private double InternalCoerceValueProperty(double value = double.NaN)
        {
            if (double.IsNaN(value))
                value = Value;

            double newValue = MathHelpers.ValueInTickFrequencyAndRange(Minimum, Maximum, TickFrequency, value);

            if (PartTextBox != null)
            {
                double textBoxValue;

                if (!double.TryParse(PartTextBox.Text, out textBoxValue) || !double.Equals(textBoxValue, newValue))
                {
                    PartTextBox.Text = newValue.ToString();
                    PartTextBox.CaretIndex = PartTextBox.Text.Length;
                }
            }

            return newValue;
        }
    
        public  TextBox PartTextBox { get; set; }
    }
}
