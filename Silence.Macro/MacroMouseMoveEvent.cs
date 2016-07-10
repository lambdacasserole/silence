using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;

namespace Silence.Macro
{

    /// <summary>
    /// Represents a mouse move event that occurs during macro recording.
    /// </summary>
    class MacroMouseMoveEvent : MacroMouseEvent
    {

        /// <summary>
        /// Initialises a new instance of a mouse move event.
        /// </summary>
        /// <param name="location">The screen coordinates at which this event occurred.</param>
        public MacroMouseMoveEvent(Point location) : base(location)
        {
            // Straight to superclass constructor.
        }

        /// <summary>
        /// Initialises a new instance of a macro mouse move event.
        /// </summary>
        /// <param name="element">The serialised XML element to initialise from.</param>
        public MacroMouseMoveEvent(XmlElement element)
            : base(new Point(0, 0))
        {
            foreach (XmlElement current in element)
            {
                switch (current.Name)
                {
                    case "Location" :
                        Location = Point.Parse(current.InnerText);
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
            str.AppendLine("<MacroMouseMoveEvent>");
            str.AppendLine("<Location>" + Location.ToString() + "</Location>");
            str.AppendLine("</MacroMouseMoveEvent>");

            return str.ToString();
        }

    }

}
