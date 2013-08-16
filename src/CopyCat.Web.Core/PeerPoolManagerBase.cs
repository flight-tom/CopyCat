using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DOWILL.CopyCat.Lib;

namespace CopyCat.Web.Core
{
    public class PeerPoolManagerBase : IPeerPoolManager
    {
        protected static readonly PeerPoolManagerBase _singletonInstance = new PeerPoolManagerBase();
        protected readonly Dictionary<string, KeyValuePair<IPeerPool, DateTime>> _peerPoolDict = new Dictionary<string, KeyValuePair<IPeerPool, DateTime>>();
        protected readonly Thread _cleanerThread = null;

        protected PeerPoolManagerBase()
        {
            _cleanerThread = new Thread(new ThreadStart(cleanExpirePeer));
            _cleanerThread.Start();
        }

        ~PeerPoolManagerBase()
        {
            Dispose();
        }

        public static IPeerPoolManager GetPeerPoolManager()
        {
            return _singletonInstance;
        }
        /// <summary>
        /// Get the existing peer pool by torrent MD5 info_hash, 
        /// or create a new peer pool and return if the info_hash is not existing.
        /// </summary>
        /// <param name="infoHash">Torrent MD5 info_hash</param>
        /// <returns>The existing peer pool or new peer pool</returns>
        public virtual IPeerPool GetPeerPoolByInfoHash(IInfoHash infoHash)
        {
            IPeerPool peerPool = null;
            lock (this)
            {
                if (_peerPoolDict.ContainsKey(infoHash.Hex))
                {
                    peerPool = _peerPoolDict[infoHash.Hex].Key;
                    _peerPoolDict[infoHash.Hex] = new KeyValuePair<IPeerPool, DateTime>(peerPool, DateTime.Now);

                }
                else
                {
                    peerPool = new PeerPoolBase();
                    _peerPoolDict.Add(infoHash.Hex, new KeyValuePair<IPeerPool, DateTime>(peerPool, DateTime.Now));
                }
                Debug.WriteLine(string.Format("[{0}] : _peerPoolDict.Count={1}", this.GetType().FullName, _peerPoolDict.Count));
            }
            return peerPool;
        }

        protected virtual void cleanExpirePeer()
        {
            while (true)
            {
                try
                {
                    lock (this)
                    {
                        foreach (KeyValuePair<string, KeyValuePair<IPeerPool, DateTime>> p in _peerPoolDict)
                        {
                            if (DateTime.Now.Subtract(p.Value.Value) > TimeSpan.FromHours(1))
                            {
                                _peerPoolDict.Remove(p.Key);
                                p.Value.Key.Dispose();
                            }
                        }
                    }
                    GC.Collect();
                    Thread.Sleep(60 * 1000);
                }
                catch (ThreadAbortException)
                {
                    _peerPoolDict.Clear();
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