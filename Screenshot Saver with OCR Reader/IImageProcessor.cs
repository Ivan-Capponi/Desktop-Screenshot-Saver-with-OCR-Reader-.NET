using System.Drawing;

namespace Screenshot_Saver_with_OCR_Reader
{
    /// <summary>
    /// Interface for processing images, including methods to capture, convert, crop, perform OCR, and save images.
    /// </summary>
    public interface IImageProcessor
    {
        /// <summary>
        /// Captures an image of the entire desktop screen.
        /// </summary>
        /// <returns>A Bitmap object containing the captured image.</returns>
        Bitmap CaptureDesktop();

        /// <summary>
        /// Converts a Bitmap object to a byte array.
        /// </summary>
        /// <param name="bitmap">The bitmap to convert.</param>
        /// <returns>A byte array representing the image.</returns>
        byte[] ImageToByteArray(Bitmap bitmap);

        /// <summary>
        /// Crops a specified region from an image defined by a quadrilateral.
        /// </summary>
        /// <param name="imageBytes">Byte array representing the image.</param>
        /// <param name="quadrilateral">Array of points defining the quadrilateral.</param>
        /// <returns>A Bitmap object containing the cropped region.</returns>
        Bitmap CropImage(byte[] imageBytes, OpenCvSharp.Point[] quadrilateral);

        /// <summary>
        /// Crops a specified region from an image file defined by a quadrilateral.
        /// </summary>
        /// <param name="filePath">The file path of the image to crop.</param>
        /// <param name="quadrilateral">Array of points defining the quadrilateral.</param>
        /// <returns>A Bitmap object containing the cropped region.</returns>
        Bitmap CropImageFromFile(string filePath, OpenCvSharp.Point[] quadrilateral);

        /// <summary>
        /// Performs OCR on a specified image.
        /// </summary>
        /// <param name="bitmap">The image to perform OCR on.</param>
        /// <returns>The text recognized by OCR.</returns>
        string PerformOcr(Bitmap bitmap);

        /// <summary>
        /// Saves the image to a file.
        /// </summary>
        /// <param name="bitmap">The image to save.</param>
        /// <param name="filePath">The file path where to save the image.</param>
        void SaveImage(Bitmap bitmap, string filePath);
    }
}
