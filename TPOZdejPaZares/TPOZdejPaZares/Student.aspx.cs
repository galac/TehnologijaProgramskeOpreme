using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares
{
    public partial class Student1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonIme_Click(object sender, EventArgs e)
        {
            /*MySqlParameter parIme = new MySqlParameter("@ime", MySqlDbType.VarChar, 100);
            MySqlParameter parPriimek = new MySqlParameter("@priimek", MySqlDbType.VarChar, 100);
            cmd.Parameters.Add(parIme);
            cmd.Parameters.Add(parPriimek);*/
            string ime = inputIme.Text;
            string priimek = inputPriimek.Text;
                
            t8_2015Entities db = new t8_2015Entities();
            var studenti = (from s in db.Student
                            .Where(s => s.imeStudenta.ToString().StartsWith(ime))
                            .Where(s => s.priimekStudenta.ToString().StartsWith(priimek))
                            select s)
                            .ToList();
            //cmd.CommandText = "Select s.vpisnaStudenta AS 'Vpisna stevilka', s.imeStudenta AS 'Ime', s.priimekStudenta AS 'Priimek', s.mailStudenta AS 'E-mail' FROM Student s WHERE s.vpisnaStudenta LIKE '" + vpisna + "%'";

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
                buttonPdf.Visible = true;
                buttonCsv.Visible = true;
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
            //string connectionString = "server=slavnik.fri.uni-lj.si;user id=mh6946;persistsecurityinfo=True;database=t8_2015;password=j7vukcgreh";
            //MySqlConnection conn = new MySqlConnection(connectionString);
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.Connection = conn;
            string vpisna = inputVpisna.Text;
            t8_2015Entities db = new t8_2015Entities();
            var studenti = (from s in db.Student
                            .Where(s => s.vpisnaStudenta.ToString().StartsWith(vpisna))
                            select s).ToList();
            //cmd.CommandText = "Select s.vpisnaStudenta AS 'Vpisna stevilka', s.imeStudenta AS 'Ime', s.priimekStudenta AS 'Priimek', s.mailStudenta AS 'E-mail' FROM Student s WHERE s.vpisnaStudenta LIKE '" + vpisna + "%'";
                
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

            //MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            //da.Fill(dataTable);
            ViewState["DataTable"] = dataTable;

            GridViewIme.DataSource = dataTable;
            GridViewIme.DataBind();
            if (GridViewIme.Rows.Count > 0)
            {
                LabelOpozorilo.Visible = false;
                buttonPdf.Visible = true;
                buttonCsv.Visible = true;
            }
            else
            {
                LabelOpozorilo.Visible = true;
            }

            GridViewIme.DataSource = dataTable;
            GridViewIme.DataBind();
        }

        protected void buttonExportToPdf_click(object sender, EventArgs e)
        {
            _Default.exportToPdf((DataTable)ViewState["DataTable"], Response, "Tabela študentov", new int[] { 7, 15, 20, 20, 38 });
        }

        protected void buttonExportToCsv_click(object sender, EventArgs e)
        {
            _Default.exportToCsv((DataTable)ViewState["DataTable"], Response);
        }

        protected void GridViewIme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["studentID"] = GridViewIme.SelectedRow.Cells[1].Text;
            Response.Redirect("~/StudentSearchDetailsREF");
        }
    }
}