using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Silence.Macro
{

    /// <summary>
    /// Represents a mouse event that occurs during recording of a macro.
    /// </summary>
    public abstract class MacroMouseEvent : MacroEvent
    {

        /// <summary>
        /// Gets or sets the screen coordinates at which this event occurred.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Initialises a new instance of a macro mouse event.
        /// </summary>
        /// <param name="location">The screen coordinates at which the event occured.</param>
        public MacroMouseEvent(Point location)
        {
            Location = location;
        }

    }

}
