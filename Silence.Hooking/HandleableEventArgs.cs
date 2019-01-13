using System;

namespace Silence.Hooking
{
    /// <summary>
    /// Represents a set of event arguments handleable by a global hook.
    /// </summary>
    public abstract class HandleableEventArgs : EventArgs
    {
        /// <summary>
        /// A value indicating whether or not the event was handled.
        /// </summary>
        public bool Handled { get; set; }
    }
}
