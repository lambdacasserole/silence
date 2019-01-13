using System;

namespace Silence.Hooking.Windows
{
    partial class HookManager
    {
        /// <summary>
        /// An application-defined or library-defined callback function used with the SetWindowsHookEx function. 
        /// </summary>
        /// <param name="nCode">Specifies whether the hook procedure must process the message.</param>
        /// <param name="wParam">Specifies whether the message was sent by the current thread.</param>
        /// <param name="lParam">A pointer to a CWPSTRUCT structure that contains details about the message.</param>
        /// <returns></returns>
        /// <remarks>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms644975(v=vs.85).aspx
        /// </remarks>
        private delegate int HookProc(int nCode, int wParam, IntPtr lParam);
    }
}
