using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

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
        /// Installs a hook procedure that monitors low-level mouse input events.
        /// </summary>
        private const int WH_MOUSE_LL = 14;

        /// <summary>
        /// Installs a hook procedure that monitors low-level keyboard input events.
        /// </summary>
        private const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// Installs a hook procedure that monitors mouse messages.
        /// </summary>
        private const int WH_MOUSE = 7;

        /// <summary>
        /// Installs a hook procedure that monitors keystroke messages.
        /// </summary>
        private const int WH_KEYBOARD = 2;

        /// <summary>
        /// Posted to a window when the cursor moves. 
        /// </summary>
        private const int WM_MOUSEMOVE = 0x200;

        /// <summary>
        /// Posted when the user presses the left mouse button.
        /// </summary>
        private const int WM_LBUTTONDOWN = 0x201;

        /// <summary>
        /// Posted when the user presses the right mouse button.
        /// </summary>
        private const int WM_RBUTTONDOWN = 0x204;

        /// <summary>
        /// Posted when the user presses the middle mouse button.
        /// </summary>
        private const int WM_MBUTTONDOWN = 0x207;

        /// <summary>
        /// Posted when the user releases the left mouse button.
        /// </summary>
        private const int WM_LBUTTONUP = 0x202;

        /// <summary>
        /// Posted when the user releases the right mouse button.
        /// </summary>
        private const int WM_RBUTTONUP = 0x205;

        /// <summary>
        /// Posted when the user releases the middle mouse button.
        /// </summary>
        private const int WM_MBUTTONUP = 0x208;

        /// <summary>
        /// Posted when the user double-clicks the left mouse button.
        /// </summary>
        private const int WM_LBUTTONDBLCLK = 0x203;

        /// <summary>
        /// Posted when the user double-clicks the right mouse button.
        /// </summary>
        private const int WM_RBUTTONDBLCLK = 0x206;

        /// <summary>
        /// Posted when the user presses the right mouse button.
        /// </summary>
        private const int WM_MBUTTONDBLCLK = 0x209;

        /// <summary>
        /// Posted when the user presses the mouse wheel. 
        /// </summary>
        private const int WM_MOUSEWHEEL = 0x020A;

        /// <summary>
        /// Posted to the window with the keyboard focus when a nonsystem key is pressed.
        /// </summary>
        private const int WM_KEYDOWN = 0x100;

        /// <summary>
        /// Posted to the window with the keyboard focus when a nonsystem key is released.
        /// </summary>
        private const int WM_KEYUP = 0x101;

        /// <summary>
        /// Posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu 
        /// bar) or holds down the ALT key and then presses another key.
        /// </summary>
        private const int WM_SYSKEYDOWN = 0x104;

        /// <summary>
        /// Posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key
        /// was held down. 
        /// </summary>
        private const int WM_SYSKEYUP = 0x105;

        /// <summary>
        /// The virtual-key code for shift.
        /// </summary>
        private const byte VK_SHIFT = 0x10;

        /// <summary>
        /// The virtual-key code for caps lock.
        /// </summary>
        private const byte VK_CAPITAL = 0x14;

        /// <summary>
        /// The virtual-key code for num lock.
        /// </summary>
        private const byte VK_NUMLOCK = 0x90;

        /// <summary>
        /// Passes the hook information to the next hook procedure in the current hook chain.
        /// </summary>
        /// <param name="idHook">This parameter is ignored.</param>
        /// <param name="nCode">The hook code passed to the current hook procedure.</param>
        /// <param name="wParam">The wParam value passed to the current hook procedure.</param>
        /// <param name="lParam">The lParam value passed to the current hook procedure.</param>
        /// <returns>This value is returned by the next hook procedure in the chain.</returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms644974(v=vs.85).aspx</remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        /// <summary>
        /// Installs an application-defined hook procedure into a hook chain.
        /// </summary>
        /// <param name="idHook">The type of hook procedure to be installed.</param>
        /// <param name="lpfn">A pointer to the hook procedure.</param>
        /// <param name="hMod">A handle to the DLL containing the hook procedure pointed to by the lpfn parameter.</param>
        /// <param name="dwThreadId">The identifier of the thread with which the hook procedure is to be associated.</param>
        /// <returns>If the function succeeds, the return value is the handle to the hook procedure.</returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms644990(v=vs.85).aspx</remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        /// <summary>
        /// Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function. 
        /// </summary>
        /// <param name="idHook">Handle to the hook to be removed.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms644993(v=vs.85).aspx</remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int UnhookWindowsHookEx(int idHook);

        /// <summary>
        /// Retrieves the current double-click time for the mouse.
        /// </summary>
        /// <returns>The return value specifies the current double-click time, in milliseconds.</returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646258(v=vs.85).aspx</remarks>
        [DllImport("user32")]
        public static extern int GetDoubleClickTime();

        /// <summary>
        /// Translates the specified virtual-key code and keyboard state to the corresponding character or characters.
        /// </summary>
        /// <param name="uVirtKey">The virtual-key code to be translated.</param>
        /// <param name="uScanCode">The hardware scan code of the key to be translated.</param>
        /// <param name="lpbKeyState">A pointer to a 256-byte array that contains the current keyboard state.</param>
        /// <param name="lpwTransKey"> Pointer to the buffer that receives the translated character or characters.</param>
        /// <param name="fuState">Specifies whether a menu is active.</param>
        /// <returns>If the specified key is a dead key, the return value is negative.</returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646316(v=vs.85).aspx</remarks>
        [DllImport("user32")]
        private static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        /// <summary>
        /// Copies the status of the 256 virtual keys to the specified buffer.
        /// </summary>
        /// <param name="pbKeyState">Pointer to a 256-byte array that contains keyboard key states.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646299(v=vs.85).aspx</remarks>
        [DllImport("user32")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        /// <summary>
        /// Retrieves the status of the specified virtual key.
        /// </summary>
        /// <param name="vKey">A virtual key.</param>
        /// <returns>The return value specifies the status of the specified virtual key.</returns>
        /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646301(v=vs.85).aspx</remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);
    }
}
