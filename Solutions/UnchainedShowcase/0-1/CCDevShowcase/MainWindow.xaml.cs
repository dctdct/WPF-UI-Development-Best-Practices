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
using System.Diagnostics;

namespace CCDevShowcase
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Singleton.
        /// </summary>
        private static MainWindow instance = null;

        /// <summary>
        /// Ctor.
        /// </summary>
        private MainWindow()
        {

            InitializeComponent();

            ShowFocusStyleCheckBox.Checked += ShowFocusStyleCheckBoxChecked;
            ShowFocusStyleCheckBox.Unchecked += ShowFocusStyleCheckBoxUnChecked;
        }

        /// <summary>
        /// Get a reference.
        /// </summary>
        /// <returns></returns>
        public static MainWindow getInstance(bool show = false)
        {

            if (instance == null)
            {

                instance = new MainWindow();

                if (show)
                    instance.Show();

            }

            return instance;

        }

        private string FindNameFromResource(ResourceDictionary dictionary, object resourceItem)
        {
            string result = null;

            foreach (object key in dictionary.Keys)
            {
                if (dictionary[key] == resourceItem)
                {
                    return key.ToString();
                }
            }

            foreach (var dict in dictionary.MergedDictionaries)
            {
                result = FindNameFromResource(dict, resourceItem);

                if (result != null)
                    return result;
            }

            return null;
        }

        private void ShowFocusStyleCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            this.PreviewGotKeyboardFocus += MainWindowPreviewGotKeyboardFocus;
        }

        private void ShowFocusStyleCheckBoxUnChecked(object sender, RoutedEventArgs e)
        {
            this.PreviewGotKeyboardFocus -= MainWindowPreviewGotKeyboardFocus;
        }

        private void MainWindowPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            FrameworkElement fe = e.OriginalSource as FrameworkElement;

            string styleName = FindNameFromResource(Application.Current.Resources, fe.Style);

            Debug.WriteLine("Control: {0} ||||| Style: {1}", fe, styleName);
        }
    }

}
