using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares.Referent
{
    public partial class UvodVKartotecniList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonVpisna_Click(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();

            int vpisna = Convert.ToInt32(inputVpisna.Text);
            Student uporabnik = (from s in db.Student
                                 where s.vpisnaStudenta == vpisna
                                 select s).FirstOrDefault();

            Session["studentekID"] = uporabnik.idStudent;
            Server.Transfer("KartotecniListReferent.aspx", true);
        }
    }
}