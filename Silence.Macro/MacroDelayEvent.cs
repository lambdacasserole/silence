using System.Text;
using System.Xml;

namespace Silence.Macro
{
    /// <summary>
    /// Represents a delay that occurs between user actions during recording of a macro.
    /// </summary>
    public class MacroDelayEvent : MacroEvent
    {
        /// <summary>
        /// Gets or sets the delay in ticks.
        /// </summary>
        public long Delay { get; set; }

        /// <summary>
        /// Initialises a new instance of a macro delay event.
        /// </summary>
        /// <param name="delay">The delay in ticks.</param>
        public MacroDelayEvent(long delay)
        {
            Delay = delay;
        }

        /// <summary>
        /// Initialises a new instance of a macro delay event.
        /// </summary>
        /// <param name="element">The serialised XML element to initialise from.</param>
        public MacroDelayEvent(XmlElement element)
        {
            foreach(XmlElement current in element) {
                switch(current.Name) {
                    case "Delay" :
                        Delay = int.Parse(current.InnerText);
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
            str.AppendLine("<MacroDelayEvent>");
            str.AppendLine($"<Delay>{Delay}</Delay>");
            str.AppendLine("</MacroDelayEvent>");

            return str.ToString();
        }
    }
}
