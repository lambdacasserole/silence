using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Interop;

namespace Silence.Hooking.Windows.Native
{
    /// <summary>
    /// Provides access to User32 library methods from managed code.
    /// </summary>
    public static class User32
    {
        /// <summary>
        /// Translates virtual-key messages into character messages. 
        /// </summary>
        /// <param name="msg">A pointer to an MSG structure that contains message information retrieved from the calling thread's message queue by using the GetMessage or PeekMessage function.</param>
        /// <returns></returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/ms644955(v=vs.85).aspx</remarks>
        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool TranslateMessage([In, Out] ref MSG msg);

        /// <summary>
        /// Dispatches a message to a window procedure.
        /// </summary>
        /// <param name="msg">A pointer to a structure that contains the message.</param>
        /// <returns></returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/ms644934(v=vs.85).aspx</remarks>
        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr DispatchMessage([In] ref MSG msg);

        /// <summary>
        /// Retrieves a message from the calling thread's message queue.
        /// </summary>
        /// <param name="lpMsg">A pointer to an MSG structure that receives message information from the thread's message queue.</param>
        /// <param name="hwnd">A handle to the window whose messages are to be retrieved. The window must belong to the current thread.</param>
        /// <param name="wMsgFilterMin">The integer value of the lowest message value to be retrieved.</param>
        /// <param name="wMsgFilterMax">The integer value of the highest message value to be retrieved.</param>
        /// <returns></returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/ms644936(v=vs.85).aspx</remarks>
        [SecurityCritical, SuppressUnmanagedCodeSecurity, DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetMessage(ref MSG lpMsg, Int32 hwnd, Int32 wMsgFilterMin, Int32 wMsgFilterMax);
    }
}
