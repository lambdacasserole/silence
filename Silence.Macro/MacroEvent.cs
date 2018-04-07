namespace Silence.Macro
{
    /// <summary>
    /// Represents an event that occurs during macro recording.
    /// </summary>
    public abstract class MacroEvent
    {
        /// <summary>
        /// Returns a representation of this object as an XML string.
        /// </summary>
        /// <returns></returns>
        public abstract string ToXml();
    }
}
