
namespace DOWILL.CopyCat.Lib
{
    /// <summary>
    /// If specified, must be one of started, completed, stopped, (or empty which is the same as not being specified). 
    /// If not specified, then this request is one performed at regular intervals
    /// </summary>
    public enum PeerEvent
    {
        /// <summary>
        /// The first request to the tracker must include the event key with this value.
        /// </summary>
        started,
        /// <summary>
        /// Must be sent to the tracker if the client is shutting down gracefully.
        /// </summary>
        stopped,
        /// <summary>
        /// Must be sent to the tracker when the download completes. 
        /// However, must not be sent if the download was already 100% complete when the client started. 
        /// Presumably, this is to allow the tracker to increment the "completed downloads" metric based solely on this event.
        /// </summary>
        completed
    }
}
