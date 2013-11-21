using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.IO;

namespace CCDevShowcase
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Startup.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {

            //Startup uri hack
            blendStartupHack();

            //Show Main Window
            CCDevShowcase.MainWindow.getInstance(true);

            //Base
            base.OnStartup(e);

        }

        /// <summary>
        /// Disable blend startup uri bug.
        /// </summary>
        private void blendStartupHack()
        {

            var type = typeof(Application);

            var startupUri = type.GetField("_startupUri", BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance);

            startupUri.SetValue(this, null);

        }

        /// <summary>
        /// Ensure folder.
        /// </summary>
        /// <param name="path"></param>
        protected static void ensureFolder(string path)
        {

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

        }

    }
}
