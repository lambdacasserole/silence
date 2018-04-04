using System.Runtime.InteropServices;

namespace Silence.Hooking.Windows
{
    public partial class HookManager
    {
        /// <summary>
        /// The POINT structure defines the x- and y- coordinates of a point.
        /// </summary>
        /// <remarks>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd162805(v=vs.85).aspx
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        private struct Point
        {
            /// <summary>
            /// The x-coordinate of the point.
            /// </summary>
            public int x;

            /// <summary>
            /// The y-coordinate of the point.
            /// </summary>
            public int y;
        }

        /// <summary>
        /// Contains information about a low-level mouse input event. 
        /// <remarks>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms644970(v=vs.85).aspx
        /// </remarks>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            /// <summary>
            /// The x- and y-coordinates of the cursor, in screen coordinates.
            /// </summary>
            public Point pt;

            /// <summary>
            /// Contains information about mouse X buttons or the mouse wheel if relevant.
            /// </summary>
            public int mouseData;

            /// <summary>
            /// The event-injected flag. 
            /// </summary>
            public int flags;

            /// <summary>
            /// The time stamp for this message.
            /// </summary>
            public int time;

            /// <summary>
            /// Additional information associated with the message.
            /// </summary>
            public int dwExtraInfo;
        }

        /// <summary>
        /// Contains information about a low-level keyboard input event.
        /// </summary>
        /// <remarks>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms644967(v=vs.85).aspx
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            /// <summary>
            /// A virtual-key code. The code must be a value in the range 1 to 254.
            /// </summary>
            public int vkCode;

            /// <summary>
            /// A hardware scan code for the key.
            /// </summary>
            public int scanCode;

            /// <summary>
            /// The extended-key flag, event-injected flag, context code, and transition-state flag. 
            /// </summary>
            public int flags;

            /// <summary>
            /// The time stamp for this message.
            /// </summary>
            public int time;

            /// <summary>
            /// Additional information associated with the message. 
            /// </summary>
            public int dwExtraInfo;
        }
    }
}
