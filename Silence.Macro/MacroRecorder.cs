using Silence.Hooking;
using Silence.Hooking.Windows;
using System;

namespace Silence.Macro
{
    /// <summary>
    /// Allows recording sequences of mouse and keyboard events using global input hooks.
    /// </summary>
    public class MacroRecorder : IDisposable
    {
        /// <summary>
        /// Holds the underlying mouse/keyboard hook.
        /// </summary>
        private readonly HookManager _hook;

        /// <summary>
        /// Holds the time in ticks that the last event occurred.
        /// </summary>
        private long _lastEventTime;

        /// <summary>
        /// Gets the macro currently being recorded.
        /// </summary>
        public Macro CurrentMacro { get; private set; }

        /// <summary>
        /// Gets whether or not the macro recorder is currently running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Initialises a new instance of a macro recorder.
        /// </summary>
        public MacroRecorder()
        {
            _hook = new HookManager();

            _hook.KeyDown += HookKeyDown;
            _hook.KeyUp += HookKeyUp;
            _hook.MouseDown += HookMouseDown;
            _hook.MouseUp += HookMouseUp;
            _hook.MouseMove += HookMouseMove;
            _hook.MouseWheel += HookMouseWheel;
        }

        /// <summary>
        /// Releases all resources associated with this object.
        /// </summary>
        public void Dispose()
        {
            _hook.Dispose();
        }

        /// <summary>
        /// Adds a delay event to the current macro representing the time in ticks since this method was last called.
        /// </summary>
        private void AddDelayEvent()
        {
            var timeNow = DateTime.Now.Ticks;
            CurrentMacro.AddEvent(new MacroDelayEvent(timeNow - _lastEventTime));
            _lastEventTime = timeNow;
        }

        /// <summary>
        /// Loads a macro into the macro recorder.
        /// </summary>
        /// <param name="macro">The macro to load.</param>
        public void LoadMacro(Macro macro)
        {
            CurrentMacro = macro;
        }

        /// <summary>
        /// Clears the current macro from the macro recorder.
        /// </summary>
        public void Clear()
        {
            CurrentMacro = new Macro();
        }

        /// <summary>
        /// Starts recording mouse and keyboard events.
        /// </summary>
        public void StartRecording()
        {
            if (CurrentMacro == null)
            {
                Clear();
            }
            _lastEventTime = DateTime.Now.Ticks;
            IsRunning = true;
        }

        /// <summary>
        /// Stops recording mouse and keyboard events.
        /// </summary>
        public void StopRecording()
        {
            IsRunning = false;
        }

        private void HookKeyDown(object sender, GlobalKeyEventHandlerArgs e)
        {
            if (IsRunning)
            {
                AddDelayEvent();
                CurrentMacro.AddEvent(new MacroKeyDownEvent(e.VirtualKeyCode));
            }
        }

        private void HookKeyUp(object sender, GlobalKeyEventHandlerArgs e)
        {
            if (IsRunning)
            {
                AddDelayEvent();
                CurrentMacro.AddEvent(new MacroKeyUpEvent(e.VirtualKeyCode));
            }
        }

        private void HookMouseDown(object sender, GlobalMouseEventHandlerArgs e)
        {
            if (IsRunning)
            {
                AddDelayEvent();
                CurrentMacro.AddEvent(new MacroMouseDownEvent(e.Point, e.Button));
            }
        }

        private void HookMouseUp(object sender, GlobalMouseEventHandlerArgs e)
        {
            if (IsRunning)
            {
                AddDelayEvent();
                CurrentMacro.AddEvent(new MacroMouseUpEvent(e.Point, e.Button));
            }
        }

        private void HookMouseMove(object sender, GlobalMouseEventHandlerArgs e)
        {
            if (IsRunning)
            {
                AddDelayEvent();
                CurrentMacro.AddEvent(new MacroMouseMoveEvent(e.Point));
            }
        }

        private void HookMouseWheel(object sender, GlobalMouseEventHandlerArgs e)
        {
            if (IsRunning)
            {
                AddDelayEvent();
                CurrentMacro.AddEvent(new MacroMouseWheelEvent(e.Point, e.Delta));
            }
        }
    }
}
