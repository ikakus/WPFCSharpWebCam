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


namespace WPFCSharpWebCam
{
    //Design by Pongsakorn Poosankam
    class WebCam
    {
        private WebCamCapture webcam;
        private System.Windows.Controls.Image _FrameImage;
        private int FrameNumber = 30;
        public void InitializeWebCam(ref System.Windows.Controls.Image ImageControl)
        {
            webcam = new WebCamCapture();
            webcam.FrameNumber = ((ulong)(0ul));
            webcam.TimeToCapture_milliseconds = FrameNumber;
            webcam.ImageCaptured += new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
            _FrameImage = ImageControl;
        }

        TimeSpan timeInterval = new TimeSpan(0, 0, 1);
        DateTime lastCapturedTime = DateTime.MinValue;
        Bitmap lastBitmap;
        const float similarityThreshold = 0.5f;
        double compareLevel = 0.98;
        void webcam_ImageCaptured(object source, WebcamEventArgs e)
        {
            if (DateTime.Now > lastCapturedTime + timeInterval)
            {
                if (lastBitmap != null)
                {
                    Bitmap currentBitmap = (System.Drawing.Bitmap)e.WebCamImage;
                    //bool result = BitmapComparator.CompareImages(lastBitmap, currentBitmap,compareLevel,similarityThreshold);
                    //if (result)
                    //{
                    //    MessageBox.Show("lol" + compareLevel);
                    //   // compareLevel += 0.01;
                    //}
                    int result = BitmapComparator.CompareBitmaps(lastBitmap,currentBitmap,0,0);
                    int threshold = 76230;
                    if(result > threshold)
                    {
                        MessageBox.Show(result.ToString());
                    }
                    

                }
               
                lastCapturedTime = DateTime.Now;
                lastBitmap = (System.Drawing.Bitmap)e.WebCamImage;
               
            }
            //System.Drawing.Image bmp1 = Helper.LoadBitmap((System.Drawing.Bitmap)e.WebCamImage);   
            
            _FrameImage.Source = Helper.LoadBitmap((System.Drawing.Bitmap)e.WebCamImage);           
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
