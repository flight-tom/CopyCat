using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web.Services;

namespace CopyCat.Web
{
    /// <summary>
    /// Summary description for TorrentManager
    /// </summary>
    [WebService(Namespace = "http://dowill.blogspot.com/projects/copy-cat")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TorrentManager : WebService
    {
        [WebMethod]
        public DataTable GetTorrentList()
        {
            DataTable TorrentTable = new DataTable("torrents");
            TorrentTable.Columns.Add("FileName", typeof(string));
            TorrentTable.Columns.Add("URL", typeof(string));
            TorrentTable.Columns.Add("UploadedDate", typeof(DateTime));
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("torrents"));
            foreach (FileInfo file in dir.GetFiles())
            {
                TorrentTable.Rows.Add(file.Name, "torrents/" + file.Name, file.LastWriteTime);
            }
            return TorrentTable;
        }
    }
}
