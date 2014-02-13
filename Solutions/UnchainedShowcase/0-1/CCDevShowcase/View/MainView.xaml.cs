using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CCDevShowcase.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            SetPointChartItemStyleButton.Click += SetPointChartItemStyleButton_Click;
        }

        void SetPointChartItemStyleButton_Click(object sender, RoutedEventArgs e)
        {
            MyPointChart.ItemContainerStyle = (SetPointChartItemStyleButton.IsChecked == true ?
                this.Resources["MySecondaryPointChartItemStyle"] :
                this.Resources["MyPointChartItemStyle"]) as Style;
        }
    }
}
