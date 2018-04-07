using System.Text;
using System.Xml;

namespace Silence.Macro
{
    /// <summary>
    /// Represents a key down event that occurs during macro recording.
    /// </summary>
    public class MacroKeyDownEvent : MacroKeyEvent
    {
        /// <summary>
        /// Initialises a new instance of a macro key down event.
        /// </summary>
        /// <param name="virtualKeyCode">The virtual key code associated with the event.</param>
        public MacroKeyDownEvent(int virtualKeyCode) : base(virtualKeyCode)
        {
            // Straight to superclass constructor.
        }

        /// <summary>
        /// Initialises a new instance of a macro key down event.
        /// </summary>
        /// <param name="element">The serialised XML element to initialise from.</param>
        // ReSharper disable once SuggestBaseTypeForParameter
        public MacroKeyDownEvent(XmlElement element) : base(0)
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
            var str = new StringBuilder();
            str.AppendLine("<MacroKeyDownEvent>");
            str.AppendLine($"<VirtualKeyCode>{VirtualKeyCode}</VirtualKeyCode>");
            str.AppendLine("</MacroKeyDownEvent>");

            return str.ToString();
        }
    }
}
