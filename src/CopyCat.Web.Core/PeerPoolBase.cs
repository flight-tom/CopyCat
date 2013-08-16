using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DOWILL.CopyCat.Lib;

namespace CopyCat.Web.Core
{
    public class PeerPoolBase : IPeerPool
    {
        protected readonly HashSet<string> _peerSet = new HashSet<string>();
        protected readonly Dictionary<string, KeyValuePair<IPeer, DateTime>> _peerDict = new Dictionary<string, KeyValuePair<IPeer, DateTime>>();
        protected readonly List<IPeer> _peerList = new List<IPeer>();
        protected readonly Thread _cleanerThread = null;

        internal PeerPoolBase()
        {
            PoolCreateTime = DateTime.Now;
            LastTimeAccess = DateTime.Now;
            _cleanerThread = new Thread(new ThreadStart(cleanExpirePeer));
            _cleanerThread.Start();
        }
        ~PeerPoolBase()
        {
            Dispose();
        }
        /// <summary>
        /// Pool creating time
        /// </summary>
        public DateTime PoolCreateTime { get; protected set; }
        /// <summary>
        /// The last access time to this pool
        /// </summary>
        public DateTime LastTimeAccess { get; protected set; }
        /// <summary>
        /// Indicate if this peer has been existing in this peer pool
        /// </summary>
        /// <param name="peer">Peer information</param>
        /// <returns>TRUE for existing</returns>
        public bool IsExistingPeerByThisTorrent(IPeer peer)
        {
            return _peerSet.Contains(peer.ToString());
        }
        /// <summary>
        /// Insert a peer in pool if this peer is not existing
        /// </summary>
        /// <param name="peer">Peer information</param>
        public void AddPeer(IPeer peer)
        {
            lock (this)
            {
                // Update the last access time
                LastTimeAccess = DateTime.Now;
                string peer_str = peer.ToString();
                if (IsExistingPeerByThisTorrent(peer))
                {
                    // Update peer life time
                    if (_peerDict.ContainsKey(peer_str))
                    {
                        _peerDict[peer_str] = new KeyValuePair<IPeer, DateTime>(peer, DateTime.Now);
                    }
                }
                else
                {
                    // Add new peer in pool
                    _peerSet.Add(peer_str);
                    _peerDict.Add(peer_str, new KeyValuePair<IPeer, DateTime>(peer, DateTime.Now));
                    _peerList.Add(peer);
                }
                Debug.WriteLine(string.Format("[{0}] : _peerSet.Count={1}", this.GetType().FullName, _peerSet.Count));
            }
        }
        /// <summary>
        /// Get peer list in this pool
        /// </summary>
        /// <returns>Peer list</returns>
        public IPeer[] GetPeerList()
        {
            lock (this)
            {
                // Update the last access time
                LastTimeAccess = DateTime.Now;
            }
            return _peerList.ToArray();
        }

        protected virtual void cleanExpirePeer()
        {
            while (true)
            {
                try
                {
                    lock (this)
                    {
                        foreach (KeyValuePair<string, KeyValuePair<IPeer, DateTime>> p in _peerDict)
                        {
                            if (DateTime.Now.Subtract(p.Value.Value) > TimeSpan.FromHours(1))
                            {
                                _peerDict.Remove(p.Key);
                                _peerSet.Remove(p.Key);
                                _peerList.Remove(p.Value.Key);
                            }
                        }
                    }
                    GC.Collect();
                    Thread.Sleep(60 * 1000);
                }
                catch (ThreadAbortException)
                {
                    _peerDict.Clear();
                    _peerList.Clear();
                    _peerSet.Clear();
                    // Exit the looping thread
                    break;
                }
                catch (Exception Ex)
                {
                    Trace.WriteLine(string.Format("[{0}] : {1} - {2}", this.GetType().FullName, DateTime.Now, Ex));
                    Thread.Sleep(60 * 1000);
                }
            }
        }

        public void Dispose()
        {
            _cleanerThread.Abort();
        }
    }
}
