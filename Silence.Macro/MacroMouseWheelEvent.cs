using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;

namespace Silence.Macro
{

    /// <summary>
    /// Represents a mouse wheel event that occurs during macro recording.
    /// </summary>
    class MacroMouseWheelEvent : MacroMouseEvent
    {

        /// <summary>
        /// Gets or sets the delta value of the wheel rotation.
        /// </summary>
        public int Delta { get; set; }

        /// <summary>
        /// Initialises a new instance of a mouse wheel event.
        /// </summary>
        /// <param name="location">The screen coordinates at which this event occurred.</param>
        /// <param name="delta">The delta value of the wheel rotation.</param>
        public MacroMouseWheelEvent(Point location, int delta) : base(location)
        {
            Delta = delta;
        }

        /// <summary>
        /// Initialises a new instance of a mouse wheel event.
        /// </summary>
        /// <param name="element">The serialised XML element to initialise from.</param>
        public MacroMouseWheelEvent(XmlElement element) 
            : base(new Point(0, 0))
        {
            foreach(XmlElement current in element) 
            {
                switch (current.Name)
                {
                    case "Location" :
                        Location = Point.Parse(current.InnerText);
                        break;
                    case "Delta" :
                        Delta = int.Parse(current.InnerText);
                        break;
                }
            }
        }

        /// <summary>
        /// Serialises this object to an XML string for saving.
        /// </summary>
        /// <returns></returns>
        public override string ToXml()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("<MacroMouseWheelEvent>");
            str.AppendLine("<Location>" + Location.ToString() + "</Location>");
            str.AppendLine("<Delta>" + Delta.ToString() + "</Delta>");
            str.AppendLine("</MacroMouseWheelEvent>");

            return str.ToString();
        }

    }

}
