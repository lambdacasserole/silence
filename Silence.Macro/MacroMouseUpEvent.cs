using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace Silence.Macro
{

    /// <summary>
    /// Represents a mouse button up event that occurs during macro recording.
    /// </summary>
    class MacroMouseUpEvent : MacroMouseEvent
    {

        /// <summary>
        /// Gets or sets the mouse button associated with the event.
        /// </summary>
        public MouseButton Button { get; set; }

        /// <summary>
        /// Initialises a new instance of a macro mouse up event.
        /// </summary>
        /// <param name="location">The screen coordinates at which this event occurred.</param>
        /// <param name="button">The mouse button associated with the event.</param>
        public MacroMouseUpEvent(Point location, MouseButton button) 
            : base(location) 
        {
            Button = button;
        }

        /// <summary>
        /// Initialises a new instance of a macro mouse up event.
        /// </summary>
        /// <param name="element">The serialised XML element to initialise from.</param>
        public MacroMouseUpEvent(XmlElement element)
            : base(new Point(0, 0))
        {
            foreach (XmlElement current in element)
            {
                switch (current.Name)
                {
                    case "Location" :
                        Location = Point.Parse(current.InnerText);
                        break;
                    case "Button" :
                        Button = (MouseButton) Enum.Parse(typeof(MouseButton), current.InnerText);
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
            str.AppendLine("<MacroMouseUpEvent>");
            str.AppendLine("<Location>" + Location.ToString() + "</Location>");
            str.AppendLine("<Button>" + Button.ToString() + "</Button>");
            str.AppendLine("</MacroMouseUpEvent>");

            return str.ToString();
        }

    }

}
