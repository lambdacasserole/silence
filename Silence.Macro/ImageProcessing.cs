using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Cuda;
using Emgu.CV.XFeatures2D;

namespace Silence.Macro
{
    public class ImageProcessing
    {
        public static System.Windows.Point? FindImageOnScreen(Bitmap image, int score)
        {
            try
            {
                //long score = 0;

                Rectangle rect = Screen.PrimaryScreen.Bounds;

                using (Bitmap bitmap = CaptureImage(Point.Empty, new Point(rect.Width, rect.Height), "", "", false))
                {
                    using (Image<Gray, Byte> imageCV = bitmap.ToImage<Bgr, Byte>().Convert<Gray, Byte>())
                    {

                        Mat screen = imageCV.Mat;

                        using (Mat modelImage = image.ToImage<Bgr, Byte>().Convert<Gray, Byte>().Mat)
                        {
                            Mat homography;
                            VectorOfKeyPoint modelKeyPoints;
                            VectorOfKeyPoint observedKeyPoints;

                            using (var matches = new VectorOfVectorOfDMatch())
                            {
                                int reqScore;
                                // Homography is used apart of the score for the mathing rect
                                Mat mask;
                                FindMatch(modelImage, screen, out modelKeyPoints, out observedKeyPoints, matches, out mask, out homography, out reqScore);
                                if (homography != null && reqScore >= score)
                                {
                                    return new System.Windows.Point((double)homography.GetData().GetValue(0, 2), (double)homography.GetData().GetValue(1, 2));
                                }
                                else
                                    return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                return null;
            }
        }

        /// <summary>
        /// Save screenshot to the file
        /// </summary>
        /// <param name="showCursor"></param>
        /// <param name="curSize"></param>
        /// <param name="curPos"></param>
        /// <param name="SourcePoint"></param>
        /// <param name="DestinationPoint"></param>
        /// <param name="SelectionRectangle"></param>
        /// <param name="FilePath"></param>
        /// <param name="extension"></param>
        /// <param name="saveToClipboard"></param>
        public static Bitmap CaptureImage(Point SourcePoint, Point DestinationPoint, string FilePath, string extension, bool saveToFile = true)
        {
            Rectangle rectangle = new Rectangle(SourcePoint.X, SourcePoint.Y, DestinationPoint.X - SourcePoint.X, DestinationPoint.Y - SourcePoint.Y);
            Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                System.Threading.Thread.Sleep(250);
                g.CopyFromScreen(SourcePoint, Point.Empty, rectangle.Size);
            }

            if (!saveToFile)
            {
                return bitmap;
            }
            else
            {
                switch (extension)
                {
                    case "bmp":
                        bitmap.Save(FilePath, ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bitmap.Save(FilePath, ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bitmap.Save(FilePath, ImageFormat.Gif);
                        break;
                    case "tiff":
                        bitmap.Save(FilePath, ImageFormat.Tiff);
                        break;
                    case "png":
                        bitmap.Save(FilePath, ImageFormat.Png);
                        break;
                    default:
                        bitmap.Save(FilePath, ImageFormat.Jpeg);
                        break;
                }

                return null;
            }
        }

        /// <summary>
        /// Detect image using SURF
        /// </summary>
        /// <param name="modelImage"></param>
        /// <param name="observedImage"></param>
        /// <param name="modelKeyPoints"></param>
        /// <param name="observedKeyPoints"></param>
        /// <param name="matches"></param>
        /// <param name="mask"></param>
        /// <param name="homography"></param>
        public static void FindMatch(Mat modelImage, Mat observedImage, out VectorOfKeyPoint modelKeyPoints, out VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches, out Mat mask, out Mat homography, out int score)
        {
            int k = 2;
            double uniquenessThreshold = 0.8;
            double hessianThresh = 300;

            homography = null;

            modelKeyPoints = new VectorOfKeyPoint();
            observedKeyPoints = new VectorOfKeyPoint();
             
            if (CudaInvoke.HasCuda)
            {
                CudaSURF surfCuda = new CudaSURF((float)hessianThresh);
                using (GpuMat gpuModelImage = new GpuMat(modelImage))
                //extract features from the object image
                using (GpuMat gpuModelKeyPoints = surfCuda.DetectKeyPointsRaw(gpuModelImage, null))
                using (GpuMat gpuModelDescriptors = surfCuda.ComputeDescriptorsRaw(gpuModelImage, null, gpuModelKeyPoints))
                using (CudaBFMatcher matcher = new CudaBFMatcher(DistanceType.L2))
                {
                    surfCuda.DownloadKeypoints(gpuModelKeyPoints, modelKeyPoints);

                    // extract features from the observed image
                    using (GpuMat gpuObservedImage = new GpuMat(observedImage))
                    using (GpuMat gpuObservedKeyPoints = surfCuda.DetectKeyPointsRaw(gpuObservedImage, null))
                    using (GpuMat gpuObservedDescriptors = surfCuda.ComputeDescriptorsRaw(gpuObservedImage, null, gpuObservedKeyPoints))
                    //using (GpuMat tmp = new GpuMat())
                    //using (Stream stream = new Stream())
                    {
                        matcher.KnnMatch(gpuObservedDescriptors, gpuModelDescriptors, matches, k);

                        surfCuda.DownloadKeypoints(gpuObservedKeyPoints, observedKeyPoints);

                        mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                        mask.SetTo(new MCvScalar(255));
                        Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                        score = 0;
                        for (int i = 0; i < matches.Size; i++)
                        {
                            if ((byte)mask.GetData().GetValue(i, 0) == 0) continue;
                            foreach (var e in matches[i].ToArray())
                                ++score;
                        }

                        int nonZeroCount = CvInvoke.CountNonZero(mask);
                        if (nonZeroCount >= 4)
                        {
                            nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints,
                               matches, mask, 1.5, 20);
                            if (nonZeroCount >= 4)
                                homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                                   observedKeyPoints, matches, mask, 2);
                        }
                    }
                }
            }
            else
            {
                using (UMat uModelImage = modelImage.GetUMat(AccessType.Read))
                using (UMat uObservedImage = observedImage.GetUMat(AccessType.Read))
                {
                    SURF surfCPU = new SURF(hessianThresh);
                    //extract features from the object image
                    UMat modelDescriptors = new UMat();
                    surfCPU.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);

                    // extract features from the observed image
                    UMat observedDescriptors = new UMat();
                    surfCPU.DetectAndCompute(uObservedImage, null, observedKeyPoints, observedDescriptors, false);
                    BFMatcher matcher = new BFMatcher(DistanceType.L2);
                    matcher.Add(modelDescriptors);

                    matcher.KnnMatch(observedDescriptors, matches, k, null);
                    mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                    mask.SetTo(new MCvScalar(255));
                    Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                    score = 0;
                    for (int i = 0; i < matches.Size; i++)
                    {
                        //if (mask.GetData(true)[0] == 0) continue;
                        foreach (var e in matches[i].ToArray())
                            ++score;
                    }

                    int nonZeroCount = CvInvoke.CountNonZero(mask);
                    if (nonZeroCount >= 4)
                    {
                        nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints,
                           matches, mask, 1.5, 20);
                        if (nonZeroCount >= 4)
                            homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                               observedKeyPoints, matches, mask, 2);
                    }
                }
            }
        }
    }
}
