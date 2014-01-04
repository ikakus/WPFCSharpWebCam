using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AForge.Imaging;
using System.IO;

namespace WPFCSharpWebCam
{
    class BitmapComparator
    {
        public static void CompareBitmaps(Bitmap bmp1, Bitmap bmp2)
        {
             if(bmp1.Width == bmp2.Width && bmp1.Height == bmp2.Height)
             {

             }
        }

        private const string BitMapExtension = ".bmp";
        public static Boolean CompareImages(string image, string targetImage, double compareLevel, string filepath, float similarityThreshold)
        {
            // Load images into bitmaps
            var imageOne = new Bitmap(image);
            var imageTwo = new Bitmap(targetImage);
            
            var newBitmap1 = ChangePixelFormat(new Bitmap(imageOne), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var newBitmap2 = ChangePixelFormat(new Bitmap(imageTwo), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            newBitmap1 = SaveBitmapToFile(newBitmap1, filepath, image, BitMapExtension);
            newBitmap2 = SaveBitmapToFile(newBitmap2, filepath, targetImage, BitMapExtension);

            // Setup the AForge library
            var tm = new ExhaustiveTemplateMatching(similarityThreshold);

            // Process the images
            var results = tm.ProcessImage(newBitmap1, newBitmap2);

            // Compare the results, 0 indicates no match so return false
            if (results.Length <= 0)
            {
                return false;
            }

            // Return true if similarity score is equal or greater than the comparison level
            var match = results[0].Similarity >= compareLevel;

            return match;
        }

        private static Bitmap ChangePixelFormat(Bitmap inputImage, System.Drawing.Imaging.PixelFormat newFormat)
        {
            return (inputImage.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height), newFormat));
        }
        private static Bitmap SaveBitmapToFile(Bitmap image, string filepath, string name, string extension)
        {
            var savePath = string.Concat(filepath, "\\", Path.GetFileNameWithoutExtension(name), extension);

            image.Save(savePath, System.Drawing.Imaging.ImageFormat.Bmp);

            return image;
        }

    }
}
