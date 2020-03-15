using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Silence.Macro
{
    class ImageProcessing
    {
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
        public static void CaptureImage(bool showCursor, Size curSize, Point curPos, Point SourcePoint, Point DestinationPoint, Rectangle SelectionRectangle, string FilePath, string extension, bool saveToClipboard = false)
        {
            using (Bitmap bitmap = new Bitmap(SelectionRectangle.Width, SelectionRectangle.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(SourcePoint, DestinationPoint, SelectionRectangle.Size);
                    if (showCursor)
                    {
                        Rectangle cursorBounds = new Rectangle(curPos, curSize);
                        Cursors.Default.Draw(g, cursorBounds);
                    }
                }

                if (saveToClipboard)
                {
                    Image img = (Image)bitmap;
                    Clipboard.SetImage(img);
                }
                else
                {
                    switch (extension)
                    {
                        case ".bmp":
                            bitmap.Save(FilePath, ImageFormat.Bmp);
                            break;
                        case ".jpg":
                            bitmap.Save(FilePath, ImageFormat.Jpeg);
                            break;
                        case ".gif":
                            bitmap.Save(FilePath, ImageFormat.Gif);
                            break;
                        case ".tiff":
                            bitmap.Save(FilePath, ImageFormat.Tiff);
                            break;
                        case ".png":
                            bitmap.Save(FilePath, ImageFormat.Png);
                            break;
                        default:
                            bitmap.Save(FilePath, ImageFormat.Jpeg);
                            break;
                    }
                }
            }
        }
    }
}
