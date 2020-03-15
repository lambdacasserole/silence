using Silence.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Emgu.CV.UI;

namespace Silence.Macro
{
    class LuaCommands
    {
        /// <summary>
        /// Holds the mouse simulator that underlies this object.
        /// </summary>
        private MouseSimulator underlyingMouseSimulator;

        /// <summary>
        /// Holds the keyboard simulator that underlies this object.
        /// </summary>
        private KeyboardSimulator underlyingKeyboardSimulator;

        public LuaCommands()
        {
            underlyingMouseSimulator = new MouseSimulator(new InputSimulator());
            underlyingKeyboardSimulator = new KeyboardSimulator(new InputSimulator());
        }

        public void Delay(long delay)
        {
            Thread.Sleep(new TimeSpan(delay));
        }


        /// <summary>
        /// Converts a point to absolute screen coordinates.
        /// </summary>
        /// <param name="point">The point to convert.</param>
        /// <returns></returns>
        private Point ConvertPointToAbsolute(Point point)
        {
            return new Point((Convert.ToDouble(65535) * point.X) / Convert.ToDouble(Screen.PrimaryScreen.Bounds.Width),
                (Convert.ToDouble(65535) * point.Y) / Convert.ToDouble(Screen.PrimaryScreen.Bounds.Height));
        }

        /// <summary>
        /// Move mouse at the screen
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MouseMove(double x, double y)
        {
            Point absolutePoint = ConvertPointToAbsolute(new Point(x, y));
            underlyingMouseSimulator.MoveMouseTo(absolutePoint.X, absolutePoint.Y);
        }

        /// <summary>
        /// Mouse down event
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="button"></param>
        public void MouseDown(double x, double y, int button)
        {
            System.Windows.Input.MouseButton btn = (System.Windows.Input.MouseButton)Enum.Parse(typeof(System.Windows.Input.MouseButton), button.ToString());
            Point absolutePoint = ConvertPointToAbsolute(new Point(x, y));
            underlyingMouseSimulator.MoveMouseTo(absolutePoint.X, absolutePoint.Y);
            if (btn == System.Windows.Input.MouseButton.Left)
            {
                underlyingMouseSimulator.LeftButtonDown();
            }
            else if (btn == System.Windows.Input.MouseButton.Right)
            {
                underlyingMouseSimulator.RightButtonDown();
            }
        }

        /// <summary>
        /// Mouse up event
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="button"></param>
        public void MouseUp(double x, double y, int button)
        {
            System.Windows.Input.MouseButton btn = (System.Windows.Input.MouseButton)Enum.Parse(typeof(System.Windows.Input.MouseButton), button.ToString());
            Point absolutePoint = ConvertPointToAbsolute(new Point(x, y));
            underlyingMouseSimulator.MoveMouseTo(absolutePoint.X, absolutePoint.Y);
            if (btn == System.Windows.Input.MouseButton.Left)
            {
                underlyingMouseSimulator.LeftButtonUp();
            }
            else if (btn == System.Windows.Input.MouseButton.Right)
            {
                underlyingMouseSimulator.RightButtonUp();
            }
        }

        /// <summary>
        /// Mouse wheel event
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="delta"></param>
        public void MouseWheel(double x, double y, int delta)
        {
            Point absolutePoint = ConvertPointToAbsolute(new Point(x, y));
            underlyingMouseSimulator.MoveMouseTo(absolutePoint.X, absolutePoint.Y);
            underlyingMouseSimulator.VerticalScroll(delta / 120);
        }

        /// <summary>
        /// Keyboard down event
        /// </summary>
        /// <param name="key"></param>
        public void KeyDown(int key)
        {
            underlyingKeyboardSimulator.KeyDown((Simulation.Native.VirtualKeyCode)key);
        }

        /// <summary>
        /// Keyboard up event
        /// </summary>
        /// <param name="key"></param>
        public void KeyUp(int key)
        {
            underlyingKeyboardSimulator.KeyUp((Simulation.Native.VirtualKeyCode)key);
        }
    }

}
