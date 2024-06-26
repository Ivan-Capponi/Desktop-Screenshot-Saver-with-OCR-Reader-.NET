# Image Processing and OCR Tool

This project is a simple tool for capturing the desktop screen, cropping a specific region, performing OCR on the cropped image, and saving the resulting image to a file. The tool is implemented in C# and uses Tesseract for OCR and OpenCV for image processing.

## Features

- Capture the entire desktop screen.
- Convert the captured screen image to a byte array.
- Crop a specified region from the image defined by a quadrilateral.
- Perform OCR on the cropped image to extract text.
- Save the processed image to a file.

## Technologies Used

- C#
- Tesseract OCR
- OpenCV
- .NET Framework

## Requirements

- .NET Framework 4.7.2 or higher
- Tesseract 4.1 or higher
- OpenCVSharp4

## Getting Started

### Prerequisites

- Install .NET Framework from [here](https://dotnet.microsoft.com/download/dotnet-framework).
- Add the required NuGet packages:
  - Tesseract
  - OpenCvSharp4
  - OpenCvSharp4.runtime.win
  - System.Drawing.Common
  - System.Windows.Forms

### Installation

1. Clone the repository.
2. Open the project in Visual Studio.
3. Install the required NuGet packages by right-clicking on the project in Solution Explorer and selecting Manage NuGet Packages.

### Usage

1. Build and run the project.
2. The tool will:
   - Capture an image of the desktop.
   - Convert the image to a byte array.
   - Crop a specific region defined by a quadrilateral.
   - Perform OCR on the cropped region.
   - Save the cropped image as cropped_image.png.
   - The OCR result will be displayed in the console.
