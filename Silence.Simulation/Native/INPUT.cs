namespace Silence.Simulation.Native
{
    /// <summary>
    /// Used by SendInput to store information for synthesizing input events such as keystrokes, mouse movement, and mouse clicks.
    /// </summary>
    /// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646270(v=vs.85).aspx</remarks>
    internal struct Input
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
        /// The type of the input event.
        /// </summary>
        public uint type;

        /// <summary>
        /// The data structure that contains information about the simulated mouse, keyboard or hardware event.
        /// </summary>
        public MouseKeyboardHardwareInput data;
    }
}
