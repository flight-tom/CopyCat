using System;
using System.Data;
using System.IO;
using System.Web.UI;

namespace CopyCat.Web
{
    public partial class WebForm1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TorrentTable = new DataTable("torrents");
                TorrentTable.Columns.Add("FileName", typeof(string));
                TorrentTable.Columns.Add("URL", typeof(string));
                TorrentTable.Columns.Add("UploadedDate", typeof(DateTime));
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("torrents"));
                foreach (FileInfo file in dir.GetFiles())
                {
                    TorrentTable.Rows.Add(file.Name, "torrents/" + file.Name, file.LastWriteTime);
                }
            }
            Bind();
        }

        private void Bind()
        {
            TorrentGrid.DataSource = TorrentTable;
            TorrentGrid.DataBind();
        }

        private DataTable TorrentTable
        {
            get { return (DataTable)ViewState["torrent_table"]; }
            set { ViewState["torrent_table"] = value; }
        }
    }
}
