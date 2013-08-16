using System;
using System.Collections.Specialized;
using System.Web;

namespace DOWILL.CopyCat.Lib
{
    /// <summary>
    /// Represent the BT peer information base class
    /// </summary>
    public class PeerRequestBase : IPeerRequest
    {
        protected PeerRequestBase() { }
        /// <summary>
        /// Get a peer request instance 
        /// </summary>
        /// <returns></returns>
        public static IPeerRequest GetRequestInstance(string request)
        {
            IPeerRequest rq = new PeerRequestBase();

            // Parsing the url querystring
            // request sample could be like:
            // /BTTrackerWeb/announce/default.aspx?info_hash=N%F2%F0%B7%9DDaJ%07V6%2C%8Fg%06bG%28%7B%A3&peer_id=T03H-----B4TFlEPeGbw&port=43080&uploaded=0&downloaded=0&left=2397974471&no_peer_id=1&compact=1&event=started&key=QEiO-U HTTP/1.1
            string[] tmp = request.Split('?');
            if (tmp.Length > 0) rq.URL = tmp[0];        // Extract URL

            #region Extract info_hash
            const string CONST_INFO_HASH = "info_hash=";
            int idx_info_hash_bgn = tmp[1].IndexOf(CONST_INFO_HASH) + CONST_INFO_HASH.Length;
            string hash_string = tmp[1].Substring(idx_info_hash_bgn, tmp[1].IndexOf("&", idx_info_hash_bgn) - idx_info_hash_bgn);
            rq.InfoHash = new InfoHashBase(HttpUtility.UrlDecodeToBytes(hash_string));
            #endregion

            NameValueCollection url_param = HttpUtility.ParseQueryString(tmp[1]);

            rq.PeerID = url_param["peer_id"];
            rq.Port = (null == url_param["port"]) ? 0 : int.Parse(url_param["port"]);
            rq.Uploaded = (null == url_param["uploaded"]) ? 0 : long.Parse(url_param["uploaded"]);
            rq.Downloaded = (null == url_param["downloaded"]) ? 0 : long.Parse(url_param["downloaded"]);
            rq.Left = (null == url_param["left"]) ? 0 : long.Parse(url_param["left"]);
            rq.NoPeerID = (null == url_param["no_peer_id"]) ? 0 : int.Parse(url_param["no_peer_id"]);
            rq.Compact = (null == url_param["compact"]) ? 0 : int.Parse(url_param["compact"]);
            rq.Event = string.IsNullOrEmpty(url_param["event"]) ? PeerEvent.started : (PeerEvent)Enum.Parse(typeof(PeerEvent), url_param["event"]);
            rq.Key = url_param["key"];
            return rq;
        }
        /// <summary>
        /// The requested URL
        /// </summary>
        public virtual string URL { get; set; }
        /// <summary>
        /// The 20 byte sha1 hash of the bencoded form of the info value from the metainfo file. Note that this is a substring of the metainfo file. Don't forget to URL-encode this.
        /// </summary>
        public virtual IInfoHash InfoHash { get; set; }
        /// <summary>
        /// A string of length 20 which this downloader uses as its id. Each downloader generates its own id at random at the start of a new download. Don't forget to URL-encode this.
        /// </summary>
        public virtual string PeerID { get; set; }
        /// <summary>
        /// An optional parameter giving the IP (or dns name) which this peer is at. Generally used for the origin if it's on the same machine as the tracker; otherwise it's not normally needed.
        /// </summary>
        public virtual string IP { get; set; }
        /// <summary>
        /// Port number this peer is listening on. Common behavior is for a downloader to try to listen on port 6881 and if that port is taken try 6882, then 6883, etc. and give up after 6889.
        /// </summary>
        public virtual int Port { get; set; }
        /// <summary>
        /// Total amount uploaded so far, represented in base ten in ASCII.
        /// </summary>
        public virtual long Uploaded { get; set; }
        /// <summary>
        /// Total amount downloaded so far, represented in base ten in ASCII.
        /// </summary>
        public virtual long Downloaded { get; set; }
        /// <summary>
        /// Number of bytes this client still has to download, represented in base ten in ASCII. Note that this can't be computed from downloaded and the file length since the client might be resuming an earlier download, and there's a chance that some of the downloaded data failed an integrity check and had to be re-downloaded.
        /// </summary>
        public virtual long Left { get; set; }
        /// <summary>
        /// Ask the tracker to 'not send the peer id information.
        /// </summary>
        public virtual int NoPeerID { get; set; }
        /// <summary>
        /// Indicate that the tracker can send the IP address list in a compact form (see below for a detailed description)
        /// </summary>
        public virtual int Compact { get; set; }
        /// <summary>
        /// Optional.
        /// An additional identification that is not shared with any users.
        /// It is intended to allow a client to prove their identity should their IP address change.
        /// </summary>
        public virtual string Key { get; set; }
        /// <summary>
        /// Optional key which maps to started, completed, or stopped (or empty, which is the same as not being present). If not present, this is one of the announcements done at regular intervals. An announcement using started is sent when a download first begins, and one using completed is sent when the download is complete. No completed is sent if the file was complete when started. Downloaders should send an announcement using 'stopped' when they cease downloading, if they can.
        /// </summary>
        public virtual PeerEvent Event { get; set; }
        /// <summary>
        /// Optional key tells the tracker how many addresses the client wants in the tracker's response. The tracker does not have to supply that many. Default is 50.
        /// </summary>
        public virtual int Numwant { get; set; }
        /// <summary>
        /// Represent the url querystring contains the request information.
        /// </summary>
        /// <returns>The URL querystring contains the request information.</returns>
        public override string ToString()
        {
            return string.Format("GET {0}?info_hash={1}&peer_id={2}&port={3}&uploaded={4}&downloaded={5}&lef={6}&no_peer_id={7}&compact={8}&key={9}",
                URL, InfoHash, PeerID, Port, Uploaded, Downloaded, Left, NoPeerID, Compact, Key);
        }
        /// <summary>
        /// Optional.
        /// If a previous announce contained a tracker id, it should be set here.
        /// </summary>
        public virtual string TrackerID { get; set; }
    }
}
