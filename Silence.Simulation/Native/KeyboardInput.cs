using System;

namespace Silence.Simulation.Native
{
    /// <summary>
    /// Contains information about a simulated keyboard event.
    /// </summary>
    /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646271(v=vs.85).aspx</remarks>
    internal struct KeyboardInput
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
        /// Specifies a virtual-key code. 
        /// </summary>
        public ushort wVk;

        /// <summary>
        /// Specifies a hardware scan code for the key. 
        /// </summary>
        public ushort wScan;

        /// <summary>
        /// Specifies various aspects of a keystroke. 
        /// </summary>
        public uint dwFlags;

        /// <summary>
        /// Time stamp for the event, in milliseconds.
        /// </summary>
        public uint time;

        /// <summary>
        /// Specifies an additional value associated with the keystroke.
        /// </summary>
        public IntPtr dwExtraInfo;
    }
}
