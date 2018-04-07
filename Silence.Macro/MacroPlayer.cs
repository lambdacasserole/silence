using Silence.Simulation;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace Silence.Macro
{
    /// <summary>
    /// Provides support for playback of recorded macros.
    /// </summary>
    public class MacroPlayer
    {
        /// <summary>
        /// Holds the mouse simulator that underlies this object.
        /// </summary>
        private readonly MouseSimulator _mouseSimulator;

        /// <summary>
        /// Holds the keyboard simulator that underlies this object.
        /// </summary>
        private readonly KeyboardSimulator _keyboardSimulator;

        /// <summary>
        /// Holds the thread that the macro is currently executing on.
        /// </summary>
        private Thread _macroThread;

        /// <summary>
        /// Holds whether or not the macro has been cancelled.
        /// </summary>
        private bool _cancelled;

        /// <summary>
        /// Holds the number of times the macro should repeat before ending.
        /// </summary>
        private int _repetitions;

        /// <summary>
        /// Gets the macro currently loaded into the player.
        /// </summary>
        public Macro CurrentMacro { get; private set; }

        /// <summary>
        /// Gets or sets the number of times the macro should repeat before ending.
        /// </summary>
        public int Repetitions
        {
            get => _repetitions;
            set => _repetitions = (value == 0 ? 1 : value);
        }

        /// <summary>
        /// Gets whether or not a macro is currently being played.
        /// </summary>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// Initialies a new instance of a macro player.
        /// </summary>
        public MacroPlayer()
        {
            _mouseSimulator = new MouseSimulator(new InputSimulator());
            _keyboardSimulator = new KeyboardSimulator(new InputSimulator());
            _repetitions = 1;
            _cancelled = false;
        }

        /// <summary>
        /// Cancels playback of the current macro.
        /// </summary>
        public void CancelPlayback()
        {
            _cancelled = IsPlaying;
        }

        /// <summary>
        /// Loads a macro into the player.
        /// </summary>
        /// <param name="macro">The macro to load into the player.</param>
        public void LoadMacro(Macro macro) 
        {
            CurrentMacro = macro;
        }

        /// <summary>
        /// Converts a point to absolute screen coordinates.
        /// </summary>
        /// <param name="point">The point to convert.</param>
        /// <returns></returns>
        private static Point ConvertPointToAbsolute(Point point)
        {
            return new Point(Convert.ToDouble(65535) * point.X / Convert.ToDouble(Screen.PrimaryScreen.Bounds.Width),
                Convert.ToDouble(65535) * point.Y / Convert.ToDouble(Screen.PrimaryScreen.Bounds.Height));
        }

        /// <summary>
        /// Plays the macro on the current thread.
        /// </summary>
        private void PlayMacro()
        {
            IsPlaying = true;

            // Repeat macro as required.
            for (var i = 0; i < Repetitions; i++)
            {
                // Loop through each macro event.
                foreach (var current in CurrentMacro.Events)
                {
                    // Cancel playback.
                    if (_cancelled)
                    {
                        _cancelled = false;
                        i = Repetitions;
                        break;
                    }

                    // Cast event to appropriate type.
                    switch (current)
                    {
                        case MacroDelayEvent delay:

                            // Delay event.
                            Thread.Sleep(new TimeSpan(delay.Delay));
                            break;
                        case MacroMouseMoveEvent mouseMove:

                            // Mouse move event.
                            var mouseMovePoint = ConvertPointToAbsolute(mouseMove.Location);
                            _mouseSimulator.MoveMouseTo(mouseMovePoint.X, mouseMovePoint.Y);
                            break;
                        case MacroMouseDownEvent mouseDown:

                            // Mouse down event.
                            var mouseDownPoint = ConvertPointToAbsolute(mouseDown.Location);
                            _mouseSimulator.MoveMouseTo(mouseDownPoint.X, mouseDownPoint.Y);
                            switch (mouseDown.Button)
                            {
                                case System.Windows.Input.MouseButton.Left:
                                    _mouseSimulator.LeftButtonDown();
                                    break;
                                case System.Windows.Input.MouseButton.Right:
                                    _mouseSimulator.RightButtonDown();
                                    break;
                            }
                            break;
                        case MacroMouseUpEvent mouseUp:

                            // Mouse up event.
                            var mouseUpPoint = ConvertPointToAbsolute(mouseUp.Location);
                            _mouseSimulator.MoveMouseTo(mouseUpPoint.X, mouseUpPoint.Y);
                            switch (mouseUp.Button)
                            {
                                case System.Windows.Input.MouseButton.Left:
                                    _mouseSimulator.LeftButtonUp();
                                    break;
                                case System.Windows.Input.MouseButton.Right:
                                    _mouseSimulator.RightButtonUp();
                                    break;
                            }
                            break;
                        case MacroKeyDownEvent keyDown:

                            // Key down event.
                            _keyboardSimulator.KeyDown((Simulation.Native.VirtualKeyCode) keyDown.VirtualKeyCode);
                            break;
                        case MacroKeyUpEvent keyUp:

                            // Key up event.
                            _keyboardSimulator.KeyUp((Simulation.Native.VirtualKeyCode) keyUp.VirtualKeyCode);
                            break;
                        case MacroMouseWheelEvent mouseWheel:

                            // Mouse wheel event.
                            var mouseWheelPoint = ConvertPointToAbsolute(mouseWheel.Location);
                            _mouseSimulator.MoveMouseTo(mouseWheelPoint.X, mouseWheelPoint.Y);
                            _mouseSimulator.VerticalScroll(mouseWheel.Delta / 120);
                            break;
                    }
                }
            }

            IsPlaying = false;
        }

        /// <summary>
        /// Plays the macro on a seperate background thread.
        /// </summary>
        public void PlayMacroAsync()
        {
            _macroThread = new Thread(PlayMacro);
            _macroThread.Start();
        }
    }
}
