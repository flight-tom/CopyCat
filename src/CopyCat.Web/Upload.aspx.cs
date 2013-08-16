using System;
using System.Web.UI;

namespace CopyCat.Web
{
    public partial class Upload : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                TorrentUploader.SaveAs(Server.MapPath("torrents/" + TorrentUploader.FileName));
                lblResult.Text = "Upload success!";
            }
            catch (Exception Ex)
            {
                lblResult.Text = Ex.ToString();
            }
            finally
            {
                lblResult.Visible = true;
            }
        }
    }
}
