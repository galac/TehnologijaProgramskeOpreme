using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares
{
    public partial class Predmeti : System.Web.UI.Page
    {
        t8_2015Entities db = new t8_2015Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                seznamPredmetovGVBind();
            }
        }

        protected void seznamPredmetovGVBind()
        {
            var predmeti = (from p in db.Predmet select p).ToList();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { 
                    new DataColumn("SifraPredmeta", typeof(String)),
                    new DataColumn("Predmet", typeof(String)),
                    new DataColumn("Kreditne", typeof(int)) });

            foreach (var item in predmeti)
            {
                dt.Rows.Add(item.idPredmet, item.imePredmeta, item.kreditneTocke);
            }


            seznamPredmetovGV.DataSource = dt;
            seznamPredmetovGV.DataBind();
        }

        protected void seznamPredmetovGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            seznamPredmetovGV.EditIndex = -1;
            seznamPredmetovGV.DataBind();
        }

        protected void seznamPredmetovGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)seznamPredmetovGV.Rows[e.RowIndex];
            int id = Convert.ToInt32(seznamPredmetovGV.DataKeys[e.RowIndex].Value);
            //Predmet p = (Predmet)(from p in db.Predmet where p.idPredmet == id select p).FirstOrDefault();
            db.Predmet.Remove((Predmet)(from p in db.Predmet where p.idPredmet == id select p).FirstOrDefault());
            db.SaveChanges();
            seznamPredmetovGVBind();
        }

        protected void seznamPredmetovGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(seznamPredmetovGV.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)seznamPredmetovGV.Rows[e.RowIndex];
            int idPredmet = Convert.ToInt32(((TextBox)row.Cells[0].Controls[0]).Text);
            String imePredmet = ((TextBox)row.Cells[1].Controls[0]).Text;
            int kreditneTockePredmet = Convert.ToInt32(((TextBox)row.Cells[2].Controls[0]).Text);

            Predmet p = (from x in db.Predmet where x.idPredmet == id select x).FirstOrDefault();
            p.imePredmeta = imePredmet;
            p.kreditneTocke = kreditneTockePredmet;
            if (p.idPredmet != idPredmet)
            {
                p.idPredmet = idPredmet;
                try {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    feedbackLB.Text = "Šifre predmeta ni mogoče spremeniti, ker je že v uporabi.";
                    feedbackLB.Visible = true;
                }
            }
            else
            {
                db.SaveChanges();
                feedbackLB.Text = "Predmet "+p.imePredmeta+" je bil posodobljen";
                feedbackLB.Visible = true;
            }
            seznamPredmetovGV.EditIndex = -1;
            seznamPredmetovGVBind();
        }

        protected void seznamPredmetovGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            seznamPredmetovGV.EditIndex = e.NewEditIndex;
            feedbackLB.Visible = false;
            seznamPredmetovGVBind();
        }

        protected void dodajPredmetButton_Click(object sender, EventArgs e)
        {
            int idPredmet = Int32.Parse(novPredmetSifraTB.Text);
            String imePredmeta = novPredmetImeTB.Text;
            int kreditneTocke = Int32.Parse(novPredmetKreditneTB.Text);
            Predmet p = new Predmet
            {
                idPredmet = idPredmet,
                imePredmeta = imePredmeta,
                kreditneTocke = kreditneTocke
            };
            db.Predmet.Add(p);
            db.SaveChanges();
            seznamPredmetovGVBind();
        }
    }
}