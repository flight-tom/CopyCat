using System.Collections.Generic;

namespace DOWILL.CopyCat.Lib
{
    public interface IServerResponse
    {
        /// <summary>
        /// If present, then no other keys may be present.
        /// The value is a human-readable error message as to why the request failed (string).
        /// </summary>
        string FailureReason { get; set; }
        /// <summary>
        /// (new, optional) Similar to failure reason, but the response still gets processed normally.
        /// The warning message is shown just like an error.
        /// </summary>
        string WarningMessage { get; set; }
        /// <summary>
        ///  Interval in seconds that the client should wait between sending regular requests to the tracker
        /// </summary>
        int Interval { get; set; }
        /// <summary>
        /// (optional) Minimum announce interval.
        /// If present clients must not reannounce more frequently than this.
        /// </summary>
        int MinInterval { get; set; }
        /// <summary>
        /// A string that the client should send back on its next announcements.
        /// If absent and a previous announce sent a tracker id, do not discard the old value; keep using it.
        /// </summary>
        string TrackerID { get; set; }
        /// <summary>
        /// number of peers with the entire file, i.e. seeders (integer)
        /// </summary>
        int Complete { get; set; }
        /// <summary>
        /// number of non-seeder peers, aka "leechers" (integer)
        /// </summary>
        int Incomplete { get; set; }
        /// <summary>
        /// List of dictionaries corresponding to peers.
        /// </summary>
        IList<IPeer> Peers { get; }
        /// <summary>
        /// Output the response string use byte array.
        /// </summary>
        byte[] GetBinaryResponse();
    }
}
