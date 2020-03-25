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
    public class MacroWaitImageEvent : MacroEvent
    {

        /// <summary>
        /// Gets or sets the image location
        /// </summary>
        public string FileLocation { get; set; }
        public int Score { get; set; }
        public int WaitMs { get; set; }

        /// <summary>
        /// Initialises a new instance of a macro wait image event.
        /// </summary>
        /// <param name="location">File location.</param>
        public MacroWaitImageEvent(string location, int score = 100, int waitms = 10000)
        {
            FileLocation = location;
            Score = score;
            WaitMs = waitms;
        }

        public override string ToXml()
        {
            throw new NotImplementedException();
        }
    }

}
