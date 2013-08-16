using System.Text;
using System;

namespace DOWILL.CopyCat.Lib
{
    /// <summary>
    /// Represent the BT peer information base class
    /// </summary>
    public class PeerBase : IPeer
    {
        #region common bencoding constants
        protected const string CONST_BASIC_STRING_FORMAT = "{0}:{1}";
        protected const string CONST_INTEGER_FORMAT = "i{0}e";
        protected const string CONST_DICTIONARY_FORMAT = "d{0}e";
        #endregion

        #region peer fields names
        protected const string CONST_FLD_PEER_ID = "peer id";
        protected const string CONST_FLD_IP = "ip";
        protected const string CONST_FLD_PORT = "port";
        #endregion

        /// <summary>
        /// Get a peer instance 
        /// </summary>
        /// <returns></returns>
        public static IPeer GetPeer()
        {
            IPeer peer = new PeerBase();
            return peer;
        }

        protected PeerBase() { }
        /// <summary>
        /// peer_id used by peer to identify with tracker. This key is not present if the no_peer_id extension is used (see below).
        /// </summary>
        public virtual string PeerID { get; set; }
        /// <summary>
        /// IP address of the client.
        /// </summary>
        public virtual string IP { get; set; }
        /// <summary>
        /// Port on with the client is listening for a connection.
        /// </summary>
        public virtual int Port { get; set; }
        /// <summary>
        /// Output content as bencoded string
        /// </summary>
        /// <returns>Bencoded string</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            // peer id(string)
            if (!string.IsNullOrEmpty(PeerID))
            {
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_PEER_ID.Length, CONST_FLD_PEER_ID));
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, PeerID.Length, PeerID));
            }
            // ip(string)
            if (!string.IsNullOrEmpty(IP))
            {
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_IP.Length, CONST_FLD_IP));
                sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, IP.Length, IP));
            }
            // port(integer)
            sb.Append(string.Format(CONST_BASIC_STRING_FORMAT, CONST_FLD_PORT.Length, CONST_FLD_PORT));
            sb.Append(string.Format(CONST_INTEGER_FORMAT, Port));
            return string.Format(CONST_DICTIONARY_FORMAT, sb);
        }
        /// <summary>
        /// Get binary encoded content
        /// </summary>
        /// <returns>binary encoded content</returns>
        public byte[] GetBinary()
        {
            byte[] bin = new byte[6];
            // IP bytes (len = 4)
            int i = 0;
            foreach (string s in IP.Split('.'))
            {
                bin[i] = Convert.ToByte(s);
                i++;
            }
            // Port bytes (len = 2)
            bin[4] = Convert.ToByte(Port / 256);
            bin[5] = Convert.ToByte(Port % 256);
            return bin;
        }
    }
}
