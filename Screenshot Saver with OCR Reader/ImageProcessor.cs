using System;
using System.Drawing;
using System.IO;
using Tesseract;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Forms;
using Point = OpenCvSharp.Point;

namespace Screenshot_Saver_with_OCR_Reader
{
    /// <summary>
    /// Class that implements the IImageProcessor interface to process images.
    /// </summary>
    public class ImageProcessor : IImageProcessor
    {
        /// <summary>
        /// Captures an image of the entire desktop screen.
        /// </summary>
        /// <returns>A Bitmap object containing the captured image.</returns>
        public Bitmap CaptureDesktop()
        {
            // Get the dimensions of the primary screen
            Rectangle screenSize = Screen.PrimaryScreen.Bounds;

            // Create a bitmap with the size of the screen
            Bitmap bitmap = new Bitmap(screenSize.Width, screenSize.Height);

            // Create a Graphics object associated with the bitmap
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Copy the screen image to the bitmap
                g.CopyFromScreen(screenSize.Location, System.Drawing.Point.Empty, screenSize.Size);
            }

            return bitmap; // Return the bitmap containing the screenshot
        }

        /// <summary>
        /// Converts a Bitmap object to a byte array.
        /// </summary>
        /// <param name="bitmap">The bitmap to convert.</param>
        /// <returns>A byte array representing the image.</returns>
        public byte[] ImageToByteArray(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the bitmap to a MemoryStream in PNG format
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                // Return the byte array from the MemoryStream
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Crops a specified region from an image defined by a quadrilateral.
        /// </summary>
        /// <param name="imageBytes">Byte array representing the image.</param>
        /// <param name="quadrilateral">Array of points defining the quadrilateral.</param>
        /// <returns>A Bitmap object containing the cropped region.</returns>
        public Bitmap CropImage(byte[] imageBytes, Point[] quadrilateral)
        {
            // Load the image from the byte array
            using (var ms = new MemoryStream(imageBytes))
            {
                Bitmap bitmap = new Bitmap(ms);

                // Convert Bitmap to Mat
                Mat mat = BitmapConverter.ToMat(bitmap);

                // Define the source and destination points for perspective transformation
                Point2f[] srcPoints = Array.ConvertAll(quadrilateral, p => new Point2f(p.X, p.Y));
                Point2f[] dstPoints = new Point2f[]
                {
                new Point2f(0, 0),
                new Point2f(mat.Width, 0),
                new Point2f(mat.Width, mat.Height),
                new Point2f(0, mat.Height)
                };

                // Calculate the perspective transformation matrix
                Mat perspectiveMatrix = Cv2.GetPerspectiveTransform(srcPoints, dstPoints);

                // Apply the perspective transformation
                Mat warpedMat = new Mat();
                Cv2.WarpPerspective(mat, warpedMat, perspectiveMatrix, new OpenCvSharp.Size(mat.Width, mat.Height));

                // Convert Mat to Bitmap
                Bitmap croppedBitmap = BitmapConverter.ToBitmap(warpedMat);

                return croppedBitmap;
            }
        }

        /// <summary>
        /// Performs OCR on a specified image.
        /// </summary>
        /// <param name="bitmap">The image to perform OCR on.</param>
        /// <returns>The text recognized by OCR.</returns>
        public string PerformOcr(Bitmap bitmap)
        {
            // Create an instance of the Tesseract engine with the path to tessdata and the English language
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.TesseractAndLstm))
            {
                // Convert the Bitmap image to a Pix object usable by Tesseract
                using (var img = PixConverter.ToPix(bitmap))
                {
                    // Perform OCR on the image
                    using (var page = engine.Process(img))
                    {
                        // Return the recognized text
                        return page.GetText();
                    }
                }
            }
        }

        /// <summary>
        /// Saves the image to a file.
        /// </summary>
        /// <param name="bitmap">The image to save.</param>
        /// <param name="filePath">The file path where to save the image.</param>
        public void SaveImage(Bitmap bitmap, string filePath)
        {
            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }
    }

}
