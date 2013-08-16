using System;
using System.Web.UI;

namespace CopyCat.Web
{
    public partial class BT : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblIP.Text = Request.UserHostAddress;
        }
    }
}
