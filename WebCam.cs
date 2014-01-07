using System;
using System.IO;
using System.Linq;
using System.Text;
using WebCam_Capture;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;


namespace WPFCSharpWebCam
{
    //Design by Pongsakorn Poosankam
    class WebCam
    {
        private WebCamCapture webcam;
        private System.Windows.Controls.Image _FrameImage;
        private int FrameNumber = 30;
        Window1 wnd;
        public void InitializeWebCam(ref System.Windows.Controls.Image ImageControl, Window1 window)
        {
            webcam = new WebCamCapture();
            webcam.FrameNumber = ((ulong)(0ul));
            webcam.TimeToCapture_milliseconds = FrameNumber;
            webcam.ImageCaptured += new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
            _FrameImage = ImageControl;
            wnd = window;
        }

        TimeSpan timeInterval;// = new TimeSpan(0, 0, 2);
        DateTime lastCapturedTime = DateTime.MinValue;
        Bitmap lastBitmap;

        void webcam_ImageCaptured(object source, WebcamEventArgs e)
        {
            MotionDetection(e);
            _FrameImage.Source = Helper.LoadBitmap((System.Drawing.Bitmap)e.WebCamImage);           
        }

        private void MotionDetection(WebcamEventArgs e)
        {
            timeInterval = new TimeSpan(0, 0, 0, wnd.getTextboxInt(wnd.timeIntervalTExtBox));
            if (lastBitmap != null)
            {
                Bitmap currentBitmap = (System.Drawing.Bitmap)e.WebCamImage;
                int result = BitmapComparator.CompareBitmaps(lastBitmap, currentBitmap, 0, 0);
                wnd.thresholdLable.Content = result.ToString();
            }

            if (DateTime.Now > lastCapturedTime + timeInterval)
            {
                if (lastBitmap != null)
                {
                    Bitmap currentBitmap = (System.Drawing.Bitmap)e.WebCamImage;

                    int result = BitmapComparator.CompareBitmaps(lastBitmap, currentBitmap, 0, 0);
                    int threshold = wnd.getTextboxInt(wnd.thresholdTextbox);

                    wnd.thresholdLable.Content = result.ToString();

                    if (result > threshold)
                    {

                        wnd.IndicatoRectangle.Fill = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {

                        wnd.IndicatoRectangle.Fill = new SolidColorBrush(Colors.Chartreuse);
                    }


                }

                lastCapturedTime = DateTime.Now;
                lastBitmap = (System.Drawing.Bitmap)e.WebCamImage;

            }
            //System.Drawing.Image bmp1 = Helper.LoadBitmap((System.Drawing.Bitmap)e.WebCamImage);   

        }

        public void Start()
        {
            webcam.TimeToCapture_milliseconds = FrameNumber;
            webcam.Start(0);
        }

        public void Stop()
        {
            webcam.Stop();
        }

        public void Continue()
        {
            // change the capture time frame
            webcam.TimeToCapture_milliseconds = FrameNumber;

            // resume the video capture from the stop
            webcam.Start(this.webcam.FrameNumber);
        }

        public void ResolutionSetting()
        {
            webcam.Config();
        }

        public void AdvanceSetting()
        {
            webcam.Config2();
        }

    }
}
