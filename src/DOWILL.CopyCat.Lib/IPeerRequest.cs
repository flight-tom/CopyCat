
namespace DOWILL.CopyCat.Lib
{
    /// <summary>
    /// Represent the peer request information
    /// </summary>
    public interface IPeerRequest
    {
        /// <summary>
        /// The requested URL
        /// </summary>
        string URL { get; set; }
        /// <summary>
        /// The 20 byte sha1 hash of the bencoded form of the info value from the metainfo file. Note that this is a substring of the metainfo file. Don't forget to URL-encode this.
        /// </summary>
        IInfoHash InfoHash { get; set; }
        /// <summary>
        /// A string of length 20 which this downloader uses as its id. Each downloader generates its own id at random at the start of a new download. Don't forget to URL-encode this.
        /// </summary>
        string PeerID { get; set; }
        /// <summary>
        /// An optional parameter giving the IP (or dns name) which this peer is at. Generally used for the origin if it's on the same machine as the tracker; otherwise it's not normally needed.
        /// </summary>
        string IP { get; set; }
        /// <summary>
        /// Port number this peer is listening on. Common behavior is for a downloader to try to listen on port 6881 and if that port is taken try 6882, then 6883, etc. and give up after 6889.
        /// </summary>
        int Port { get; set; }
        /// <summary>
        /// Total amount uploaded so far, represented in base ten in ASCII.
        /// </summary>
        long Uploaded { get; set; }
        /// <summary>
        /// Total amount downloaded so far, represented in base ten in ASCII.
        /// </summary>
        long Downloaded { get; set; }
        /// <summary>
        /// Number of bytes this client still has to download, represented in base ten in ASCII. Note that this can't be computed from downloaded and the file length since the client might be resuming an earlier download, and there's a chance that some of the downloaded data failed an integrity check and had to be re-downloaded.
        /// </summary>
        long Left { get; set; }
        /// <summary>
        /// Ask the tracker to 'not send the peer id information.
        /// </summary>
        int NoPeerID { get; set; }
        /// <summary>
        /// Indicate that the tracker can send the IP address list in a compact form (see below for a detailed description)
        /// </summary>
        int Compact { get; set; }
        /// <summary>
        /// Optional.
        /// An additional identification that is not shared with any users.
        /// It is intended to allow a client to prove their identity should their IP address change.
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// Optional key which maps to started, completed, or stopped (or empty, which is the same as not being present).
        /// If not present, this is one of the announcements done at regular intervals.
        /// An announcement using started is sent when a download first begins, and one using completed is sent when the download is complete.
        /// No completed is sent if the file was complete when started.
        /// Downloaders should send an announcement using 'stopped' when they cease downloading, if they can.
        /// </summary>
        PeerEvent Event { get; set; }
        /// <summary>
        /// Optional key tells the tracker how many addresses the client wants in the tracker's response. 
        /// The tracker does not have to supply that many.
        /// Default is 50.
        /// </summary>
        int Numwant { get; set; }
        /// <summary>
        /// Optional.
        /// If a previous announce contained a tracker id, it should be set here.
        /// </summary>
        string TrackerID { get; set; }
    }
}
