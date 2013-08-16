
namespace DOWILL.CopyCat.Lib
{
    /// <summary>
    /// Represent the BT peer information
    /// </summary>
    public interface IPeer
    {
        /// <summary>
        /// peer_id used by peer to identify with tracker. This key is not present if the no_peer_id extension is used (see below).
        /// </summary>
        string PeerID { get; set; }
        /// <summary>
        /// IP address of the client.
        /// </summary>
        string IP { get; set; }
        /// <summary>
        /// Port on with the client is listening for a connection.
        /// </summary>
        int Port { get; set; }
        /// <summary>
        /// Get binary encoded content
        /// </summary>
        /// <returns>binary encoded content</returns>
        byte[] GetBinary();
        /// <summary>
        /// Output content as bencoded string
        /// </summary>
        /// <returns>Bencoded string</returns>
        string ToString();
    }
}
