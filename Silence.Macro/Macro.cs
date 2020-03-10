using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Silence.Macro
{

    /// <summary>
    /// Represents a recorded sequence of mouse and keyboard events.
    /// </summary>
    public class Macro
    {

        /// <summary>
        /// Macro file path
        /// </summary>
        public string MacroFile;

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
        /// Saves this macro to a file at the specified path.
        /// </summary>
        /// <param name="path">The path to save to.</param>
        public void Save(string path)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("luanet.load_assembly('mscorlib')");
            str.AppendLine("luanet.load_assembly('Silence.Macro', 'Silence.Macro')");
            str.AppendLine("LuaCommands=luanet.import_type('Silence.Macro.LuaCommands')");
            str.AppendLine("LC=LuaCommands()");
            str.AppendLine("");
            str.AppendLine("function inputRecords()");

            foreach (MacroEvent current in events)
            {
                if (current is MacroDelayEvent)
                {
                    // Delay event.
                    MacroDelayEvent castEvent = (MacroDelayEvent)current;
                    str.AppendLine(string.Format("  LC:Delay({0})", castEvent.Delay.ToString()));
                }
                else if (current is MacroMouseMoveEvent)
                {
                    // Mouse move event.
                    MacroMouseMoveEvent castEvent = (MacroMouseMoveEvent)current;
                    str.AppendLine(string.Format("  LC:MouseMove({0}, {1})", castEvent.Location.X, castEvent.Location.Y));
                }
                else if (current is MacroMouseDownEvent)
                {
                    // Mouse down event.
                    MacroMouseDownEvent castEvent = (MacroMouseDownEvent)current;
                    str.AppendLine(string.Format("  LC:MouseDown({0}, {1}, {2})", castEvent.Location.X, castEvent.Location.Y, (int)castEvent.Button));
                }
                else if (current is MacroMouseUpEvent)
                {
                    // Mouse up event.
                    MacroMouseUpEvent castEvent = (MacroMouseUpEvent)current;
                    str.AppendLine(string.Format("  LC:MouseUp({0}, {1}, {2})", castEvent.Location.X, castEvent.Location.Y, (int)castEvent.Button));
                }
                else if (current is MacroKeyDownEvent)
                {
                    // Key down event.
                    MacroKeyDownEvent castEvent = (MacroKeyDownEvent)current;
                    str.AppendLine(string.Format("  LC:KeyDown({0})", castEvent.VirtualKeyCode));
                }
                else if (current is MacroKeyUpEvent)
                {
                    // Key up event.
                    MacroKeyUpEvent castEvent = (MacroKeyUpEvent)current;
                    str.AppendLine(string.Format("  LC:KeyUp({0})", castEvent.VirtualKeyCode));
                }
                else if (current is MacroMouseWheelEvent)
                {
                    // Mouse wheel event.
                    MacroMouseWheelEvent castEvent = (MacroMouseWheelEvent)current;
                    str.AppendLine(string.Format("  LC:MouseWheel({0}, {1}, {2})", castEvent.Location.X, castEvent.Location.Y, castEvent.Delta));
                }
            }

            str.AppendLine("end");
            str.AppendLine("inputRecords()");

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@path))
            {
                file.Write(str);
            }

            MacroFile = path;
        }

        public void SaveTemp()
        {
            string tempPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\temp.lua";
            Save(tempPath);
        }

        /// <summary>
        /// Loads the macro file at the specifed path into this object.
        /// </summary>
        /// <param name="path">The path to load the macro from.</param>
        public void Load(string path)
        {
            ClearEvents();
            MacroFile = path;
        }

    }

}
