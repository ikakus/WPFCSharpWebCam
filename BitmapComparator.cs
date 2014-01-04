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


      
        public static int CompareBitmaps(Bitmap image, Bitmap targetImage, double compareLevel, float similarityThreshold)
        {

            if (image.Width == targetImage.Width && image.Height == targetImage.Height)
            {
                LockBitmap lockedImage = new LockBitmap(image);
                LockBitmap lockedTargetImage = new LockBitmap(targetImage);

                lockedImage.LockBits();
                lockedTargetImage.LockBits();
                int diffCount=0;

                for(int i =0; i<lockedImage.Width;i++)
                {
                    for(int j=0;j<lockedImage.Height;j++)
                    {
                        if (lockedImage.GetPixel(i, j) != lockedTargetImage.GetPixel(i, j))
                        {
                            diffCount++;
                        }
                    }
                }
                lockedImage.UnlockBits();
                lockedTargetImage.UnlockBits();
                return diffCount;
            }
            return -1;
        }
        
        private const string BitMapExtension = ".bmp";
        public static Boolean CompareImages(Bitmap image, Bitmap targetImage, double compareLevel, float similarityThreshold)
        {
            // Load images into bitmaps
            var imageOne = new Bitmap(image);
            var imageTwo = new Bitmap(targetImage);
            
            var newBitmap1 = ChangePixelFormat(new Bitmap(imageOne), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var newBitmap2 = ChangePixelFormat(new Bitmap(imageTwo), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // newBitmap1 = SaveBitmapToFile(newBitmap1, filepath, image, BitMapExtension);
           // newBitmap2 = SaveBitmapToFile(newBitmap2, filepath, targetImage, BitMapExtension);

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
