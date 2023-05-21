﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace Login_Page
{
    /// <summary>
    /// Interaction logic for CtlSplashScreen.xaml
    /// </summary>
    public partial class CtlSplashScreen : Window
    {
        public CtlSplashScreen()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(40);
                Console.WriteLine("Loading : " + i + "%");
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;

            if (progressBar.Value == 100)
            {
                MainWindow mywindow = new MainWindow();
                Close();
                mywindow.ShowDialog();
            }
        }
    }
}
