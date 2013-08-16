using System;
using System.Configuration;
using System.Web.UI;
using CopyCat.Web.Core;
using DOWILL.CopyCat.Lib;

namespace CopyCat.Web.Announce
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IServerResponse res = ServerResponseBase.GetServerResponseInstance();
            try
            {
                IPeerRequest rq = PeerRequestBase.GetRequestInstance(Request.RawUrl);

                Trace.Write("Request Object = " + rq);

                IPeer peer = PeerBase.GetPeer();
                peer.IP = (string.IsNullOrEmpty(rq.IP)) ? Request.UserHostAddress : rq.IP;
                peer.Port = rq.Port;
                peer.PeerID = rq.PeerID;

                IPeerPoolManager manager = PeerPoolManagerBase.GetPeerPoolManager();
                IPeerPool pool = manager.GetPeerPoolByInfoHash(rq.InfoHash);
                pool.AddPeer(peer);

                res.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["Interval"]);
                res.MinInterval = Convert.ToInt32(ConfigurationManager.AppSettings["MinInterval"]);

                foreach (IPeer p in pool.GetPeerList())
                {
                    if (p.IP == peer.IP && p.Port == peer.Port) continue;   // skip the request peer itself
                    res.Peers.Add(p);
                }

                Response.Clear();
                byte[] binRes = res.GetBinaryResponse();
                Trace.Write("binRes=" + HexEncoding.ToString(binRes));
                Response.BinaryWrite(binRes);
            }
            catch (Exception Ex)
            {
                Trace.Write("ERROR: " + Ex);
                res.FailureReason = Ex.ToString();
                Response.Clear();
                Response.Write(res);
            }
            Response.End();
        }
    }
}
