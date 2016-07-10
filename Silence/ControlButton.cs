using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Silence
{

    public class ControlButton : PictureBox
    {

        private Color _mouseOutBackgroundColor = Color.FromArgb(255, 37, 0, 64);

        private Color _mouseOverBackgroundColor = Color.FromArgb(255, 65, 33, 89);

        private Color _mouseDownBackgroundColor = Color.FromArgb(255, 44, 23, 61);

        private Color _shapeColor = Color.White;

        private Bitmap _shapeImage = null;

        private Bitmap _mouseOverImage = null;

        private Bitmap _mouseOutImage = null;

        private Bitmap _mouseDownImage = null;

        private MouseState _currentMouseState = MouseState.Out;

        private enum MouseState : int
        {

            Out = 0,

            Over = 1,

            Down = 2,
        }

        private MouseState CurrentMouseState
        {
            get
            {
                return _currentMouseState;
            }
            set
            {
                _currentMouseState = value;
                DisplayStateImage();
                DisplayStateImage();
            }
        }

        public Color MouseOverBackgroundColor
        {
            get
            {
                return _mouseOverBackgroundColor;
            }
            set
            {
                _mouseOverBackgroundColor = value;
                RenderImages();
                DisplayStateImage();
            }
        }

        public Color MouseOutBackgroundColor
        {
            get
            {
                return _mouseOutBackgroundColor;
            }
            set
            {
                _mouseOutBackgroundColor = value;
                RenderImages();
                DisplayStateImage();
            }
        }

        public Color MouseDownBackgroundColor
        {
            get
            {
                return _mouseDownBackgroundColor;
            }
            set
            {
                _mouseDownBackgroundColor = value;
                RenderImages();
                DisplayStateImage();
            }
        }

        public Bitmap ShapeImage
        {
            get
            {
                return _shapeImage;
            }
            set
            {
                _shapeImage = value;
                RenderImages();
                DisplayStateImage();
            }
        }

        public Color ShapeColor
        {
            get
            {
                return _shapeColor;
            }
            set
            {
                _shapeColor = value;
                RenderImages();
                DisplayStateImage();
            }
        }

        private void DisplayStateImage()
        {
            switch (_currentMouseState)
            {
                case MouseState.Out:
                    this.Image = _mouseOutImage;
                    break;
                case MouseState.Over:
                    this.Image = _mouseOverImage;
                    break;
                case MouseState.Down:
                    this.Image = _mouseDownImage;
                    break;
            }
        }

        private void RenderImages()
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(MouseOutBackgroundColor);
            if (!(ShapeImage == null))
            {
                for (int y = 0; (y
                            <= (ShapeImage.Height - 1)); y++)
                {
                    for (int x = 0; (x
                                <= (ShapeImage.Width - 1)); x++)
                    {
                        Color pixelColor = Color.FromArgb(ShapeImage.GetPixel(x, y).A, ShapeColor);
                        g.FillRectangle(new SolidBrush(pixelColor), new Rectangle(x, y, 1, 1));
                    }
                }
            }
            _mouseOutImage = new Bitmap(bmp);
            g.Clear(MouseOverBackgroundColor);
            if (!(ShapeImage == null))
            {
                for (int y = 0; (y
                            <= (ShapeImage.Height - 1)); y++)
                {
                    for (int x = 0; (x
                                <= (ShapeImage.Width - 1)); x++)
                    {
                        Color pixelColor = Color.FromArgb(ShapeImage.GetPixel(x, y).A, ShapeColor);
                        g.FillRectangle(new SolidBrush(pixelColor), new Rectangle(x, y, 1, 1));
                    }
                }
            }
            _mouseOverImage = new Bitmap(bmp);
            g.Clear(MouseDownBackgroundColor);
            if (!(ShapeImage == null))
            {
                for (int y = 0; (y
                            <= (ShapeImage.Height - 1)); y++)
                {
                    for (int x = 0; (x
                                <= (ShapeImage.Width - 1)); x++)
                    {
                        Color pixelColor = Color.FromArgb(ShapeImage.GetPixel(x, y).A, ShapeColor);
                        g.FillRectangle(new SolidBrush(pixelColor), new Rectangle(x, y, 1, 1));
                    }
                }
            }
            _mouseDownImage = new Bitmap(bmp);
            g.Dispose();
            bmp.Dispose();
        }

        private void ControlButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CurrentMouseState = MouseState.Down;
        }

        private void ControlButton_MouseEnter(object sender, System.EventArgs e)
        {
            CurrentMouseState = MouseState.Over;
        }

        private void ControlButton_MouseLeave(object sender, System.EventArgs e)
        {
            CurrentMouseState = MouseState.Out;
        }

        private void ControlButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CurrentMouseState = MouseState.Over;
        }

        private void ControlButton_SizeChanged(object sender, System.EventArgs e)
        {
            RenderImages();
            DisplayStateImage();
        }

        public ControlButton() 
            : base()
        {
            RenderImages();
            DisplayStateImage();

            this.MouseEnter += ControlButton_MouseEnter;
            this.MouseLeave += ControlButton_MouseLeave;
            this.MouseDown += ControlButton_MouseDown;
            this.MouseUp += ControlButton_MouseUp;
            this.SizeChanged += ControlButton_SizeChanged;
        }

    }
}
