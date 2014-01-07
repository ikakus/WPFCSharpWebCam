using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Media;
using WebCam_Capture;

namespace WPFCSharpWebCam
{
    class MotionDetection
    {

        private static TimeSpan timeInterval; //= new TimeSpan(0,0,0,0,1000);
        private static DateTime lastCapturedTime = DateTime.MinValue;
        private static Bitmap lastBitmap;

        public static void OldDetection(WebcamEventArgs e, Window1 wnd)
        {

            timeInterval = new TimeSpan(0, 0, 0, 0, wnd.getTextboxInt(wnd.timeIntervalTExtBox));

            if(lastBitmap!=null)
            {
                Bitmap currentBitmap =  (System.Drawing.Bitmap)e.WebCamImage;
                int result = BitmapComparator.CompareBitmaps(lastBitmap, currentBitmap, 0, 0);
                wnd.thresholdLable.Content = result.ToString();

            }

            if (DateTime.Now > lastCapturedTime + timeInterval)
            {
                if (lastBitmap != null)
                {
                    Bitmap currentBitmap =  (System.Drawing.Bitmap)e.WebCamImage;//capturedBitmap;

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
             
        }

        public static void Detection( Window1 wnd, Bitmap capturedBitmap)
        {
            timeInterval = new TimeSpan(0, 0, 0, wnd.getTextboxInt(wnd.timeIntervalTExtBox));
            if (DateTime.Now > lastCapturedTime + timeInterval)
            {
                if (lastBitmap != null)
                {
                    Bitmap currentBitmap = capturedBitmap;

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
                lastBitmap = capturedBitmap;

            }
               
        }

    }
}
