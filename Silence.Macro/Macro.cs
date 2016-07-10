using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Silence.Macro
{

    /// <summary>
    /// Represents a recorded sequence of mouse and keyboard events.
    /// </summary>
    public class Macro
    {

        /// <summary>
        /// Holds the list of events that comprise this macro.
        /// </summary>
        private List<MacroEvent> events;

        /// <summary>
        /// Gets the list of events that comprise this macro as an array.
        /// </summary>
        public MacroEvent[] Events {
            get
            {
                return (events == null ? null : events.ToArray());
            }
        }

        /// <summary>
        /// Initialises a new instance of a macro.
        /// </summary>
        public Macro()
        {
            events = new List<MacroEvent>();
        }

        /// <summary>
        /// Appends an event to the end of this macro.
        /// </summary>
        /// <param name="newEvent">The event to append.</param>
        public void AddEvent(MacroEvent newEvent)
        {
            events.Add(newEvent);
        }

        /// <summary>
        /// Removes all events from this macro.
        /// </summary>
        public void ClearEvents()
        {
            events.Clear();
        }

        /// <summary>
        /// Serialises this object to an XML string for saving.
        /// </summary>
        /// <returns></returns>
        public string ToXml()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("<SilenceMacro>");
            foreach (MacroEvent current in events)
            {
                str.AppendLine(current.ToXml());
            }
            str.AppendLine("</SilenceMacro>");

            return str.ToString();
        }

        /// <summary>
        /// Saves this macro to a file at the specified path.
        /// </summary>
        /// <param name="path">The path to save to.</param>
        public void Save(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ToXml());

            xmlDoc.Save(path);
        }

        /// <summary>
        /// Loads the macro file at the specifed path into this object.
        /// </summary>
        /// <param name="path">The path to load the macro from.</param>
        public void Load(string path)
        {
            ClearEvents();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            foreach (XmlElement current in xmlDoc.DocumentElement)
            {
                switch (current.Name)
                {
                    case "MacroKeyDownEvent" :
                        AddEvent(new MacroKeyDownEvent(current));
                        break;
                    case "MacroKeyUpEvent":
                        AddEvent(new MacroKeyUpEvent(current));
                        break;
                    case "MacroMouseDownEvent":
                        AddEvent(new MacroMouseDownEvent(current));
                        break;
                    case "MacroMouseUpEvent":
                        AddEvent(new MacroMouseUpEvent(current));
                        break;
                    case "MacroMouseMoveEvent":
                        AddEvent(new MacroMouseMoveEvent(current));
                        break;
                    case "MacroMouseWheelEvent":
                        AddEvent(new MacroMouseWheelEvent(current));
                        break;
                    case "MacroDelayEvent":
                        AddEvent(new MacroDelayEvent(current));
                        break;
                }
            }
        }

    }

}
