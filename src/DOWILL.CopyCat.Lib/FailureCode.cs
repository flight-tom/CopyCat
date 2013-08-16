
namespace DOWILL.CopyCat.Lib
{
    /// <summary>
    /// Represent the error code from Tracker server
    /// </summary>
    public class FailureCode
    {
        protected FailureCode() { }
        /// <summary>
        /// Invalid request type: client request was not a HTTP GET.
        /// </summary>
        public const string ERR_INVALID_REQUEST = "100";
        /// <summary>
        /// Missing info_hash.
        /// </summary>
        public const string ERR_MISSING_INFO_HASH = "101";
        /// <summary>
        /// Missing peer_id.
        /// </summary>
        public const string ERR_MISSING_PEER_ID = "102";
        /// <summary>
        /// Missing port.
        /// </summary>
        public const string ERR_MISSING_PORT = "103";
        /// <summary>
        /// Invalid infohash: infohash is not 20 bytes long.
        /// </summary>
        public const string ERR_INVALID_INFO_HASH = "150";
        /// <summary>
        /// Invalid peerid: peerid is not 20 bytes long
        /// </summary>
        public const string ERR_INVALID_PEER_ID = "151";
        /// <summary>
        /// info_hash not found in the database. Sent only by trackers that do not automatically include new hashes into the database.
        /// </summary>
        public const string ERR_INFO_HASH_NOT_FOUND = "200";
        /// <summary>
        /// Client sent an eventless request before the specified time.
        /// </summary>
        public const string ERR_CLIENT_TIMEOUT = "500";
        /// <summary>
        /// Generic error
        /// </summary>
        public const string ERR_GENERIC_ERROR = "900";
    }
}
