using System;

namespace Silence.Simulation.Native
{
    /// <summary>
    /// Specifies various aspects of a keystroke.
    /// </summary>
    /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646271(v=vs.85).aspx</remarks>
    [Flags]
    internal enum KeyboardFlag : uint
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
        /// If specified, the scan code was preceded by a prefix byte that has the value 0xE0 (224).
        /// </summary>
        ExtendedKey = 0x0001,

        /// <summary>
        /// If specified, the key is being released.
        /// </summary>
        KeyUp = 0x0002,

        /// <summary>
        /// If specified, wScan identifies the key and wVk is ignored.
        /// </summary>
        Unicode = 0x0004,

        /// <summary>
        /// If specified, the system synthesizes a VK_PACKET keystroke.
        /// </summary>
        ScanCode = 0x0008,
    }
}
