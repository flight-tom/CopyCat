using System;
using System.Collections.Generic;
using System.Text;

namespace DOWILL.CopyCat.Lib
{
    public class ServerResponseBase : IServerResponse
    {
        #region common bencoding constants
        protected const string CONST_BASIC_STRING_FORMAT = "{0}:{1}";
        protected const string CONST_LIST_FORMAT = "l{0}e";
        protected const string CONST_INTEGER_FORMAT = "i{0}e";
        protected const string CONST_DICTIONARY_FORMAT = "d{0}e";
        #endregion

        #region server response fields
        protected const string CONST_FLD_FAILURE_REASON = "failure reason";
        protected const string CONST_FLD_WARNING_MEG = "warning message";
        protected const string CONST_FLD_INTERVAL = "interval";
        protected const string CONST_FLD_MIN_INTERVAL = "min interval";
        protected const string CONST_FLD_TRACKER_ID = "tracker id";
        protected const string CONST_FLD_COMPLETE = "complete";
        protected const string CONST_FLD_INCOMPLETE = "incomplete";
        protected const string CONST_FLD_PEERS = "peers";
        #endregion

        public static IServerResponse GetServerResponseInstance()
        {
            ServerResponseBase serverRes = new ServerResponseBase();
            return serverRes;
        }

        protected ServerResponseBase()
        {
            Peers = new List<IPeer>();
        }
        /// <summary>
        /// If present, then no other keys may be present.
        /// The value is a human-readable error message as to why the request failed (string).
        /// </summary>
        public virtual string FailureReason { get; set; }
        /// <summary>
        /// (new, optional) Similar to failure reason, but the response still gets processed normally.
        /// The warning message is shown just like an error.
        /// </summary>
        public virtual string WarningMessage { get; set; }
        /// <summary>
        ///  Interval in seconds that the client should wait between sending regular requests to the tracker
        /// </summary>
        public virtual int Interval { get; set; }
        /// <summary>
        /// (optional) Minimum announce interval.
        /// If present clients must not reannounce more frequently than this.
        /// </summary>
        public virtual int MinInterval { get; set; }
        /// <summary>
        /// A string that the client should send back on its next announcements.
        /// If absent and a previous announce sent a tracker id, do not discard the old value; keep using it.
        /// </summary>
        public virtual string TrackerID { get; set; }
        /// <summary>
        /// number of peers with the entire file, i.e. seeders (integer)
        /// </summary>
        public virtual int Complete { get; set; }
        /// <summary>
        /// number of non-seeder peers, aka "leechers" (integer)
        /// </summary>
        public virtual int Incomplete { get; set; }
        /// <summary>
        /// List of dictionaries corresponding to peers.
        /// </summary>
        public virtual IList<IPeer> Peers { get; protected set; }
        /// <summary>
        /// Output content as bencoded string
        /// </summary>
        /// <returns>Bencoded string</returns>
        public override string ToString()
        {
            StringBuilder sb = getResponseBuilder();
            if (string.IsNullOrEmpty(FailureReason))
            {
                if (Peers.Count > 0)
                {
                    StringBuilder peer_sb = new StringBuilder();
                    foreach (IPeer peer in Peers)
                    {
                        peer_sb.Append(peer);
                    }
                    sb.Append(string.Format(CONST_LIST_FORMAT, peer_sb));
                }
                else
                {
                    sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, 0, string.Empty));
                }
            }
            return string.Format(CONST_DICTIONARY_FORMAT, sb);
        }
        /// <summary>
        /// Output the response string use byte array.
        /// </summary>
        public byte[] GetBinaryResponse()
        {
            byte[] response = null;
            StringBuilder sb = getResponseBuilder();
            if (string.IsNullOrEmpty(FailureReason) || 0 == Peers.Count)
            {
                const int peer_data_length = 6;
                sb.Append(string.Format("{0}:", peer_data_length * Peers.Count));

                response = Encoding.ASCII.GetBytes(sb.ToString());
                List<byte> blist = new List<byte>(response);
                System.Diagnostics.Debug.Assert(blist.Count == response.Length,
                    "List has different count than response byte array length!!",
                    string.Format("blist.Count={0} / response.Length={1}", blist.Count, response.Length));
                blist.Insert(0, Encoding.ASCII.GetBytes("d")[0]);

                foreach (IPeer peer in Peers)
                {
                    foreach (byte b in peer.GetBinary())
                    {
                        blist.Add(b);
                    }
                }
                blist.Add(Encoding.ASCII.GetBytes("e")[0]);
                response = blist.ToArray();
            }
            else
            {
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, 0, string.Empty));
                response = Encoding.Default.GetBytes(string.Format(CONST_DICTIONARY_FORMAT, sb));
            }
            return response;
        }

        private StringBuilder getResponseBuilder()
        {
            StringBuilder sb = new StringBuilder();
            // failure reason
            if (!string.IsNullOrEmpty(FailureReason))
            {
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_FAILURE_REASON.Length, CONST_FLD_FAILURE_REASON));
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, FailureReason.Length, FailureReason));
            }
            else
            {
                // warning message
                if (!string.IsNullOrEmpty(WarningMessage))
                {
                    sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_WARNING_MEG.Length, CONST_FLD_WARNING_MEG));
                    sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, WarningMessage.Length, WarningMessage));
                }
                // complete
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_COMPLETE.Length, CONST_FLD_COMPLETE));
                sb.Append(string.Format(CONST_INTEGER_FORMAT, Complete));
                // incomplete
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_INCOMPLETE.Length, CONST_FLD_INCOMPLETE));
                sb.Append(string.Format(CONST_INTEGER_FORMAT, Incomplete));
                // interval
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_INTERVAL.Length, CONST_FLD_INTERVAL));
                sb.Append(string.Format(CONST_INTEGER_FORMAT, Interval));
                // min interval
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_MIN_INTERVAL.Length, CONST_FLD_MIN_INTERVAL));
                sb.Append(string.Format(CONST_INTEGER_FORMAT, MinInterval));
                // tracker id
                if (!string.IsNullOrEmpty(TrackerID))
                {
                    sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_TRACKER_ID.Length, CONST_FLD_TRACKER_ID));
                    sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, TrackerID.Length, TrackerID));
                }
                // peers
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_PEERS.Length, CONST_FLD_PEERS));
            }
            return sb;
        }
    }
}