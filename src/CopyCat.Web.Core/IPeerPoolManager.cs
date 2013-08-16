using System;
using DOWILL.CopyCat.Lib;

namespace CopyCat.Web.Core
{
    public interface IPeerPoolManager : IDisposable
    {
        /// <summary>
        /// Get the existing peer pool by torrent MD5 info_hash, 
        /// or create a new peer pool and return if the info_hash is not existing.
        /// </summary>
        /// <param name="infoHash">Torrent MD5 info_hash</param>
        /// <returns>The existing peer pool or new peer pool</returns>
        IPeerPool GetPeerPoolByInfoHash(IInfoHash infoHash);
    }
}
