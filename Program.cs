﻿using System;
using System.Windows.Forms;
using LPRT.MVVP.View;
using View = LPRT.MVVP.View.View;

namespace LPRT
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new View());
        }
    }
}