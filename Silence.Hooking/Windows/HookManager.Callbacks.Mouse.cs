using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Silence.Hooking.Windows
{
    public partial class HookManager
    {
        /* This file contains documentation from MSDN governed according to the license agreement:
         * https://msdn.microsoft.com/en-us/cc300389.aspx
         *
         * Such documentation is reproduced here as a reasonable measure taken to document the API that this software uses
         * in order to faciliate development, as permitted under the license agreement (Section 3). The full version of
         * such documentation is linked to in the remarks section of each relevant method's documentation block.
         *
         * Such documentation falls under the copyright notice:
         * © 2013 Microsoft Corporation. All rights reserved.
         */

        /// <summary>
        /// This field is not objectively needed but we need to keep a reference to a delegate which will be
        /// passed to unmanaged code to prevent the GC from cleaning it up.
        /// </summary>
        private HookProc _windowsMouseDelegate;

        /// <summary>
        /// Stores the handle to the mouse hook procedure.
        /// </summary>
        private int _windowsMouseHookHandle;

        /// <summary>
        /// Retrieved the high-order word from a 32-bit integer.
        /// </summary>
        /// <param name="num">The integer from which to retrieve the high-order word.</param>
        /// <returns></returns>
        private static short GetHighOrderWord(int num)
        {
            return (short) ((num >> 16) & 0xffff);
        }

        /// <summary>
        /// A callback function which will be called every time a mouse activity is detected.
        /// </summary>
        /// <param name="nCode">Specifies whether the hook procedure must process the message.</param>
        /// <param name="wParam">Specifies whether the message was sent by the current thread.</param>
        /// <param name="lParam">A pointer to a CWPSTRUCT structure that contains details about the message.</param>
        /// <returns>If code is less than zero, the hook procedure must return the value returned by CallNextHookEx.</returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/ms644975(v=vs.85).aspx</remarks>
        private int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // Marshal the data from callback.
                var mouseHookStruct = (MSLLHOOKSTRUCT) Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                // Detect mouse button used.
                var button = MouseButton.Middle;
                switch (wParam)
                {
                    case WM_RBUTTONDBLCLK:
                    case WM_RBUTTONDOWN:
                    case WM_RBUTTONUP:
                        button = MouseButton.Right;
                        break;
                    case WM_LBUTTONDBLCLK:
                    case WM_LBUTTONDOWN:
                    case WM_LBUTTONUP:
                        button = MouseButton.Left;
                        break;
                }

                // Generate event.
                var mouseEventArgs = new GlobalMouseEventHandlerArgs(
                    new System.Windows.Point(mouseHookStruct.pt.x, mouseHookStruct.pt.y), button,
                    mouseHookStruct.mouseData, mouseHookStruct.flags, mouseHookStruct.time, mouseHookStruct.dwExtraInfo,
                    GetHighOrderWord(mouseHookStruct.mouseData));

                // Mouse button up.
                if (GlobalMouseUp != null && (wParam == WM_RBUTTONUP || wParam == WM_LBUTTONUP))
                {
                    GlobalMouseUp.Invoke(null, mouseEventArgs);
                }

                // Mouse button down.
                if (GlobalMouseDown != null && (wParam == WM_RBUTTONDOWN || wParam == WM_LBUTTONDOWN))
                {
                    GlobalMouseDown.Invoke(null, mouseEventArgs);
                }

                // The mouse wheel was moved.
                if (GlobalMouseWheel != null && wParam == WM_MOUSEWHEEL)
                {
                    GlobalMouseWheel.Invoke(null, mouseEventArgs);
                }

                // If there is a listener for mouse movement and a mouse movement occured.
                if ((GlobalMouseMove != null) && wParam == WM_MOUSEMOVE)
                {
                    GlobalMouseMove.Invoke(null, mouseEventArgs);
                }

                // Exit method here without calling next hook if is event was handled.
                if (mouseEventArgs.Handled)
                {
                    return -1;
                }
            }

            // Call next hook.
            return CallNextHookEx(_windowsMouseHookHandle, nCode, wParam, lParam);
        }

        /// <summary>
        /// Installs a mouse hook if one is not installed already.
        /// </summary>
        private void EnsureSubscribedToGlobalMouseEvents()
        {
            // Install mouse hook only if it is not installed already.
            if (_windowsMouseHookHandle == 0)
            {
                // Keep a reference to avoid collection by the GC.
                _windowsMouseDelegate = MouseHookProc;

                // Install hook.
                _windowsMouseHookHandle = SetWindowsHookEx(WH_MOUSE_LL, _windowsMouseDelegate, IntPtr.Zero, 0);

                // If failed.
                if (_windowsMouseHookHandle == 0)
                {
                    // Do cleanup.
                    ForceUnsunscribeFromGlobalMouseEvents();

                    // Initializes and throws a new instance of the Win32Exception class with the specified error.
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Unsubscribes from the mouse hook if no subscribers are registered.
        /// </summary>
        private void TryUnsubscribeFromGlobalMouseEvents()
        {
            // If no subsribers are registered unsubscribe from hook.
            if (GlobalMouseDown == null && GlobalMouseMove == null && GlobalMouseUp == null && GlobalMouseWheel == null)
            {
                ForceUnsunscribeFromGlobalMouseEvents();
            }
        }

        /// <summary>
        /// Uninstalls the mouse hook.
        /// </summary>
        private void ForceUnsunscribeFromGlobalMouseEvents()
        {
            // Don't try to uninstall a null hook.
            if (_windowsMouseHookHandle != 0)
            {
                // Uninstall hook
                var result = UnhookWindowsHookEx(_windowsMouseHookHandle);

                // Reset invalid handle.
                _windowsMouseHookHandle = 0;

                // Free up for GC.
                _windowsMouseDelegate = null;

                // If failed, an exception must be thrown.
                if (result == 0)
                {
                    // Initializes and throws a new instance of the Win32Exception class with the specified error.
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }
    }
}
