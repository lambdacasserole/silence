using System;

namespace Silence.Hooking
{

    /// <summary>
    /// Represents a set of event arguments passed into the handler for a global key event.
    /// </summary>
    public class GlobalKeyEventHandlerArgs : HandleableEventArgs
    {

        /// <summary>
        /// A virtual-key code. The code must be a value in the range 1 to 254.
        /// </summary>
        public int VirtualKeyCode { get; private set; }

        /// <summary>
        /// A hardware scan code for the key.
        /// </summary>
        public int ScanCode { get; private set; }

        /// <summary>
        /// The extended-key flag, event-injected flag, context code, and transition-state flag.
        /// </summary>
        public int Flags { get; private set; }

        /// <summary>
        /// The time stamp for this message.
        /// </summary>
        public int Time { get; private set; }

        /// <summary>
        /// Additional information associated with the message. 
        /// </summary>
        public int ExtraInfo { get; private set; }

        /// <summary>
        /// Initialises a new instance of a set of global key event handler arguments.
        /// </summary>
        /// <param name="virtualKeyCode">A virtual-key code. The code must be a value in the range 1 to 254.</param>
        /// <param name="scanCode">A hardware scan code for the key.</param>
        /// <param name="flags">The extended-key flag, event-injected flag, context code, and transition-state flag.</param>
        /// <param name="time">The time stamp for this message.</param>
        /// <param name="extraInfo">Additional information associated with the message.</param>
        public GlobalKeyEventHandlerArgs(int virtualKeyCode, int scanCode, int flags, int time, int extraInfo)
        {
            
            VirtualKeyCode = virtualKeyCode;
            ScanCode = scanCode;
            Flags = flags;
            Time = time;
            ExtraInfo = extraInfo;

        }

    }

}
