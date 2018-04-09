using System.Runtime.InteropServices;

namespace Silence.Simulation.Native
{
#pragma warning disable 649
    /// <summary>
    /// The combined/overlayed structure that includes Mouse, Keyboard and Hardware Input message data (see: http://msdn.microsoft.com/en-us/library/ms646270(VS.85).aspx)
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct MouseKeyboardHardwareInput
    {
        /// <summary>
        /// The <see cref="MOUSEINPUT"/> definition.
        /// </summary>
        [FieldOffset(0)]
        public MOUSEINPUT Mouse;

        /// <summary>
        /// The <see cref="KeyboardInput"/> definition.
        /// </summary>
        [FieldOffset(0)]
        public KeyboardInput Keyboard;

        /// <summary>
        /// The <see cref="HardwareInput"/> definition.
        /// </summary>
        [FieldOffset(0)]
        public HardwareInput Hardware;
    }
#pragma warning restore 649
}
