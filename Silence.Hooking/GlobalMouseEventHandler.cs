namespace Silence.Hooking
{
    /// <summary>
    /// Represents a global mouse event handler.
    /// </summary>
    /// <param name="sender">The object that initially raised the event.</param>
    /// <param name="args">An event arguments class that contains the event data.</param>
    public delegate void GlobalMouseEventHandler(object sender, GlobalMouseEventHandlerArgs args);
}
