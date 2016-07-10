using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Silence.Macro
{

    /// <summary>
    /// Represents a key up event that occurs during macro recording.
    /// </summary>
    public class MacroKeyUpEvent : MacroKeyEvent
    {

        /// <summary>
        /// Initialises a new instance of a macro key up event.
        /// </summary>
        /// <param name="virtualKeyCode">The virtual key code associated with the event.</param>
        public MacroKeyUpEvent(int virtualKeyCode)
            : base(virtualKeyCode)
        {
            // Straight to superclass constructor.
        }

        /// <summary>
        /// Initialises a new instance of a macro key up event.
        /// </summary>
        /// <param name="element">The serialised XML element to initialise from.</param>
        public MacroKeyUpEvent(XmlElement element)
            : base(0)
        {
            foreach (XmlElement current in element)
            {
                switch (current.Name)
                {
                    case "VirtualKeyCode":
                        VirtualKeyCode = int.Parse(current.InnerText);
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
            str.AppendLine("<MacroKeyUpEvent>");
            str.AppendLine("<VirtualKeyCode>" + VirtualKeyCode.ToString() + "</VirtualKeyCode>");
            str.AppendLine("</MacroKeyUpEvent>");

            return str.ToString();
        }

    }

}
