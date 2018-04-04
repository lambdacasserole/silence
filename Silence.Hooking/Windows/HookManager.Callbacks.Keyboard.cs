using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Silence.Hooking.Windows
{
    public partial class HookManager
    {
        /// <summary>
        /// This field is not objectively needed but we need to keep a reference to a delegate which will be 
        /// passed to unmanaged code to prevent the GC from cleaning it up.
        /// </summary>
        private HookProc windowsKeyboardDelegate;

        /// <summary>
        /// Stores the handle to the keyboard hook procedure.
        /// </summary>
        private int windowsKeyboardHookHandle;

        /// <summary>
        /// A callback function which will be called every time a keyboard activity is detected.
        /// </summary>
        /// <param name="nCode">Specifies whether the hook procedure must process the message.</param>
        /// <param name="wParam">Specifies whether the message was sent by the current thread.</param>
        /// <param name="lParam">A pointer to a CWPSTRUCT structure that contains details about the message.</param>
        /// <returns></returns>
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            // Indicates if any  underlying events handled the action.
            bool handled = false;

            if (nCode >= 0)
            {
                // Marshal the data from callback.
                KBDLLHOOKSTRUCT keyStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                // Key was pressed down.
                if (GlobalKeyDown != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    GlobalKeyEventHandlerArgs keyEventArgs = new GlobalKeyEventHandlerArgs(
                        keyStruct.vkCode, 
                        keyStruct.scanCode,
                        keyStruct.flags, 
                        keyStruct.time, 
                        keyStruct.dwExtraInfo);

                    GlobalKeyDown.Invoke(null, keyEventArgs);
                    handled = keyEventArgs.Handled;
                }

                // Key was released.
                if (GlobalKeyUp != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    GlobalKeyEventHandlerArgs keyEventArgs = new GlobalKeyEventHandlerArgs(
                            keyStruct.vkCode,
                            keyStruct.scanCode,
                            keyStruct.flags,
                            keyStruct.time,
                            keyStruct.dwExtraInfo);

                    GlobalKeyUp.Invoke(null, keyEventArgs);
                    handled = handled || keyEventArgs.Handled;
                }
            }

            // Exit method here without calling next hook if is event was handled.
            if (handled)
            {
                return -1;
            }

            // Call next hook.
            return CallNextHookEx(windowsKeyboardHookHandle, nCode, wParam, lParam);
        }

        /// <summary>
        /// Installs a keyboard hook if one is not installed already.
        /// </summary>
        private void EnsureSubscribedToGlobalKeyboardEvents()
        {
            // Install keyboard hook only if it is not installed and must be installed.
            if (windowsKeyboardHookHandle == 0)
            {
                // Keep a reference to avoid collection by the GC.
                windowsKeyboardDelegate = KeyboardHookProc;

                // Install hook.
                windowsKeyboardHookHandle = SetWindowsHookEx(WH_KEYBOARD_LL, windowsKeyboardDelegate, IntPtr.Zero, 0);

                // If failed.
                if (windowsKeyboardHookHandle == 0)
                {
                    // Do cleanup.
                    ForceUnsunscribeFromGlobalKeyboardEvents();

                    // Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Unsubscribes from the keyboard hook if no subscribers are registered.
        /// </summary>
        private void TryUnsubscribeFromGlobalKeyboardEvents()
        {
            // If no subsribers are registered unsubscribe from hook.
            if (GlobalKeyDown == null &&
                GlobalKeyUp == null)
            {
                ForceUnsunscribeFromGlobalKeyboardEvents();
            }
        }

        /// <summary>
        /// Uninstalls the keyboard hook.
        /// </summary>
        private void ForceUnsunscribeFromGlobalKeyboardEvents()
        {
            // Don't try to uninstall a null hook.
            if (windowsKeyboardHookHandle != 0)
            {
                // Uninstall hook.
                var result = UnhookWindowsHookEx(windowsKeyboardHookHandle);
                
                // Reset invalid handle.
                windowsKeyboardHookHandle = 0;
                
                // Free up for GC.
                windowsKeyboardDelegate = null;

                // If failed, an exception must be thrown.
                if (result == 0)
                {
                    //Initializes and throws a new instance of the Win32Exception class with the specified error. 
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }
    }
}
