using System;

namespace Silence.Hooking.Windows
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

    partial class HookManager
    {
        /// <summary>
        /// An application-defined or library-defined callback function used with the SetWindowsHookEx function. 
        /// </summary>
        /// <param name="nCode">Specifies whether the hook procedure must process the message.</param>
        /// <param name="wParam">Specifies whether the message was sent by the current thread.</param>
        /// <param name="lParam">A pointer to a CWPSTRUCT structure that contains details about the message.</param>
        /// <returns></returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/ms644975(v=vs.85).aspx</remarks>
        private delegate int HookProc(int nCode, int wParam, IntPtr lParam);
    }
}
