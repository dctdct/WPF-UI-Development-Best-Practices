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
    }

}
