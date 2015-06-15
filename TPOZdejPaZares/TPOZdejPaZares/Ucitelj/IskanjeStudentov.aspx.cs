using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares.Ucitelj
{
    public partial class IskanjeStudentov : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonIme_Click(object sender, EventArgs e)
        {
            string ime = inputIme.Text;
            string priimek = inputPriimek.Text;

            t8_2015Entities db = new t8_2015Entities();
            var studenti = (from s in db.Student
                            .Where(s => s.imeStudenta.ToString().StartsWith(ime))
                            .Where(s => s.priimekStudenta.ToString().StartsWith(priimek))
                            .Where(s => s.Vloge_idVloge == 1)
                            orderby s.priimekStudenta
                            select s)
                            .ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[4] {
                new DataColumn("Vpisna stevilka", typeof(int)),
                new DataColumn("Ime", typeof(String)),
                new DataColumn("Priimek", typeof(String)),
                new DataColumn("E-mail", typeof(String))
            });

            foreach (var item in studenti)
            {
                dataTable.Rows.Add(item.vpisnaStudenta, item.imeStudenta, item.priimekStudenta, item.mailStudenta);
            }
            ViewState["DataTable"] = dataTable;

            GridViewIme.DataSource = dataTable;
            GridViewIme.DataBind();

            if (GridViewIme.Rows.Count > 0)
            {
                LabelOpozorilo.Visible = false;
            }
            else
            {
                LabelOpozorilo.Visible = true;
            }

            GridViewIme.DataSource = dataTable;
            GridViewIme.DataBind();

        }

        protected void buttonVpisna_Click(object sender, EventArgs e)
        {
            string vpisna = inputVpisna.Text;
            t8_2015Entities db = new t8_2015Entities();
            var studenti = (from s in db.Student
                            .Where(s => s.vpisnaStudenta.ToString().StartsWith(vpisna))
                            .Where(s => s.Vloge_idVloge == 1)
                            orderby s.priimekStudenta
                            select s).ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[4] {
                new DataColumn("Vpisna številka", typeof(int)),
                new DataColumn("Ime", typeof(String)),
                new DataColumn("Priimek", typeof(String)),
                new DataColumn("E-mail", typeof(String))
            });

            foreach (var item in studenti)
            {
                dataTable.Rows.Add(item.vpisnaStudenta, item.imeStudenta, item.priimekStudenta, item.mailStudenta);
            }

            ViewState["DataTable"] = dataTable;

            GridViewIme.DataSource = dataTable;
            GridViewIme.DataBind();
            if (GridViewIme.Rows.Count > 0)
            {
                LabelOpozorilo.Visible = false;
            }
            else
            {
                LabelOpozorilo.Visible = true;
            }

            GridViewIme.DataSource = dataTable;
            GridViewIme.DataBind();
        }

        protected void GridViewIme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["vpisnaStudent00"] = GridViewIme.SelectedRow.Cells[1].Text;
            Response.Redirect("~/Ucitelj/PodrobnostiOStudentu");
        }

        protected void inputIme_TextChanged(object sender, EventArgs e)
        {
            buttonIme_Click(sender, e);
        }

        protected void inputPriimek_TextChanged(object sender, EventArgs e)
        {
            buttonIme_Click(sender, e);
        }

        protected void inputVpisna_TextChanged(object sender, EventArgs e)
        {
            buttonVpisna_Click(sender, e);
        }
    }
}