﻿using System;
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

namespace WPFCSharpWebCam
{
    /// <summary>
    /// Design by Pongsakorn Poosankam
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        WebCam webcam;
        private void mainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            webcam = new WebCam();
            webcam.InitializeWebCam(ref imgVideo,this);
            

        }


        public int getTextboxInt(TextBox txtBox)
        {

            if (txtBox.Text != null && txtBox.Text != "")
            {
                return Convert.ToInt32(txtBox.Text);
            }
            else
            {
                return -1;
            }
           
        }
        private void bntStart_Click(object sender, RoutedEventArgs e)
        {
            
            webcam.Start();
        }

        private void bntStop_Click(object sender, RoutedEventArgs e)
        {
            webcam.Stop();
        }

        private void bntContinue_Click(object sender, RoutedEventArgs e)
        {
            
            webcam.Continue();
        }

        private void bntResolution_Click(object sender, RoutedEventArgs e)
        {
            webcam.ResolutionSetting();
        }

        private void bntSetting_Click(object sender, RoutedEventArgs e)
        {
            webcam.AdvanceSetting();
        }
    }
}
