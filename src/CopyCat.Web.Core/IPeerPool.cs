using System;
using System.Collections.Generic;
using DOWILL.CopyCat.Lib;

namespace CopyCat.Web.Core
{
    public interface IPeerPool : IDisposable
    {
        /// <summary>
        /// Pool creating time
        /// </summary>
        DateTime PoolCreateTime { get; }
        /// <summary>
        /// The last access time to this pool
        /// </summary>
        DateTime LastTimeAccess { get; }
        /// <summary>
        /// Indicate if this peer has been existing in this peer pool
        /// </summary>
        /// <param name="peer">Peer information</param>
        /// <returns>TRUE for existing</returns>
        bool IsExistingPeerByThisTorrent(IPeer peer);
        /// <summary>
        /// Insert a peer in pool if this peer is not existing
        /// </summary>
        /// <param name="peer">Peer information</param>
        void AddPeer(IPeer peer);
        /// <summary>
        /// Get peer list in this pool
        /// </summary>
        /// <returns>Peer list</returns>
        IPeer[] GetPeerList();
    }
}
