using System;
using System.Drawing;

namespace Screenshot_Saver_with_OCR_Reader
{
    class Program
    {
        static void Main()
        {
            IImageProcessor imageProcessor = new ImageProcessor();

            // Step 1: Capture the desktop image
            Bitmap screenshots = imageProcessor.CaptureDesktop();

            // Step 2: Convert the image to a byte array
            byte[] imageBytes = imageProcessor.ImageToByteArray(screenshots);

            // Step 3: Define the quadrilateral (four points)
            OpenCvSharp.Point[] quadrilateral = new OpenCvSharp.Point[]
            {
                new OpenCvSharp.Point(100, 100), // Top-left
                new OpenCvSharp.Point(400, 100), // Top-right
                new OpenCvSharp.Point(400, 400), // Bottom-right
                new OpenCvSharp.Point(100, 400) // Bottom-left
            };

            // Step 4: Crop the region specified by the quadrilateral
            Bitmap croppedImage = imageProcessor.CropImage(imageBytes, quadrilateral);

            // Step 5: Perform OCR on the cropped region
            string ocrResult = imageProcessor.PerformOcr(croppedImage);

            // Step 6: Save the image to the project folder
            imageProcessor.SaveImage(croppedImage, "cropped_image.png");

            // Output the OCR result
            Console.WriteLine("OCR Result:");
            Console.WriteLine(ocrResult);
            Console.ReadLine();
        }
    }
}
