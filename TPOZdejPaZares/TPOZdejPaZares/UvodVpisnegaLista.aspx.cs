using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares
{
    public partial class UvodVpisnegaLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonNov_Click(object sender, EventArgs e)
        {
            Server.Transfer("ZajemVpisnegaLista.aspx", true);
        }

        protected void buttonVpisna_Click(object sender, EventArgs e)
        {
            Session["vpisnaStevilka"] = inputVpisna.Text;
            Server.Transfer("ZajemVpisnegaLista.aspx", true);
        }
    }
}