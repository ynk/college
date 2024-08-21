﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Events
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e) 
        {
            MainWindow wnd = new MainWindow();
            // The OpenFile() method is just an example of what you could do with the
            // parameter. The method should be declared on your MainWindow class, where
            // you could use a range of methods to process the passed file path
            if(e.Args.Length==1)
                wnd.OpenFile(e.Args[0]);
            wnd.Show();
        }
    }
}
