using System;
using System.Windows.Input;
using System.Collections.Generic;

namespace Silence.Hooking.Windows
{
    /// <summary>
    /// Provides events and methods for easily managing global input events regardless of application focus.
    /// </summary>
    public partial class HookManager : IDisposable 
    {
        /// <summary>
        /// Raised whenever the global mouse hook detects a mouse move action.
        /// </summary>
        private event GlobalMouseEventHandler GlobalMouseMove;

        /// <summary>
        /// Raised whenever the global mouse hook detects a mouse down action.
        /// </summary>
        private event GlobalMouseEventHandler GlobalMouseDown;
        
        /// <summary>
        /// Raised whenever the global mouse hook detects a mouse up action.
        /// </summary>
        private event GlobalMouseEventHandler GlobalMouseUp;

        /// <summary>
        /// Raised whenever the global mouse hook detects a mouse wheel action.
        /// </summary>
        private event GlobalMouseEventHandler GlobalMouseWheel;

        /// <summary>
        /// Raised whenever the global keyboard hook detects a key up action.
        /// </summary>
        private event GlobalKeyEventHandler GlobalKeyUp;
        
        /// <summary>
        /// Raised whenever the global keyboard hook detects a key down action.
        /// </summary>
        private event GlobalKeyEventHandler GlobalKeyDown;
        
        /// <summary>
        /// Raised whenever the global mouse hook detects a mouse move action.
        /// </summary>
        public event GlobalMouseEventHandler MouseMove
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                GlobalMouseMove += value;
            }
            remove
            {
                GlobalMouseMove -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        /// Raised whenever the global mouse hook detects a mouse down action.
        /// </summary>
        public event GlobalMouseEventHandler MouseDown
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                GlobalMouseDown += value;
            }
            remove
            {
                GlobalMouseDown -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        /// Raised whenever the global mouse hook detects a mouse up action.
        /// </summary>
        public event GlobalMouseEventHandler MouseUp
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                GlobalMouseUp += value;
            }
            remove
            {
                GlobalMouseUp -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        /// Raised whenever the global mouse hook detects a mouse wheel action.
        /// </summary>
        public event GlobalMouseEventHandler MouseWheel
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                GlobalMouseWheel += value;
            }
            remove
            {
                GlobalMouseWheel -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        /// Raised whenever the global keyboard hook detects a key up action.
        /// </summary>
        public event GlobalKeyEventHandler KeyUp
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                GlobalKeyUp += value;
            }
            remove
            {
                GlobalKeyUp -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }

        /// <summary>
        /// Raised whenever the global keyboard hook detects a key down action.
        /// </summary>
        public event GlobalKeyEventHandler KeyDown
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                GlobalKeyDown += value;
            }
            remove
            {
                GlobalKeyDown -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }
    
        /// <summary>
        /// Unsubscribes from all hooks and releases all resources associated with this object.
        /// </summary>
        public void Dispose()
        {

            // Force unsubscription from all hooks.
            ForceUnsunscribeFromGlobalKeyboardEvents();
            ForceUnsunscribeFromGlobalMouseEvents();

        }
    }
}
