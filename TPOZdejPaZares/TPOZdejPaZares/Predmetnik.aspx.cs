using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares
{
    public partial class Predmetnik : System.Web.UI.Page
    {
        t8_2015Entities db = new t8_2015Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var studijskiProgram = (from StudijskiProgram in db.StudijskiProgram select StudijskiProgram.naziv).Distinct().ToList();
                StudijskiProgramDDL.DataSource = studijskiProgram;
                StudijskiProgramDDL.DataBind();

                var letnik = (from Letnik in db.Letnik select Letnik.letnik1).Distinct().ToList();
                LetnikDDL.DataSource = letnik;
                LetnikDDL.DataBind();
                LetnikDDL_Edit.DataSource = letnik;
                LetnikDDL_Edit.DataBind();

                var sdpList = (from sdp in db.SestavniDelPred select sdp).Distinct().ToList();
                List<String> seznamSdp = new List<String>();
                foreach (var item in sdpList)
                {
                    if (item.jeModul == true)
                    {
                        SestavniDelPredDDL.Items.Add(item.opisSestavnegaDela + " (Modul)");
                        SestavniDelPredDDL_Edit.Items.Add(item.opisSestavnegaDela + " (Modul)");
                    }
                    else
                    {
                        SestavniDelPredDDL.Items.Add(item.opisSestavnegaDela);
                        SestavniDelPredDDL_Edit.Items.Add(item.opisSestavnegaDela);
                    }
                }

                var seznamPredmetov = (from p in db.Predmet select p.imePredmeta).ToList();
                seznamVsehPredmetovDDL.DataSource = seznamPredmetov;
                seznamVsehPredmetovDDL.DataBind();
            }
        }

        protected void StudijskiProgramDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = StudijskiProgramDDL.SelectedIndex + 1;
            StudijskiProgram spTest = (StudijskiProgram)(from sp in db.StudijskiProgram where sp.idStudijskiProgram == n select sp).First();

            var seznamPredmetov = (from psp in db.PredmetStudijskegaPrograma where psp.StudijskiProgram_idStudijskiProgram1 == spTest.idStudijskiProgram select psp).Distinct().ToList();

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] { 
                            new DataColumn("SifraPredmeta", typeof(int)),
                            new DataColumn("Predmet", typeof(String)),
                            new DataColumn("Kreditne", typeof(int)),
                            new DataColumn("Letnik", typeof(int)),
                            new DataColumn("Del predmeta", typeof(String)),
                            new DataColumn("IdStudPredmeta", typeof(int)) });

            foreach (var item in seznamPredmetov)
            {
                Predmet predmet = (Predmet) (from p in db.Predmet where p.idPredmet == item.Predmet_idPredmet1 select p).First();
                int letnik = (int)(from l in db.Letnik where l.idLetnik == item.Letnik_idLetnik select l.letnik1).First();
                SestavniDelPred sdp = (SestavniDelPred)(from x in db.SestavniDelPred where x.idSestavniDelPred == item.SestavniDelPred_idSestavniDelPred select x).First();
                String sestavniDelPredString = sdp.opisSestavnegaDela;
                if (sdp.jeModul == true)
                {
                    sestavniDelPredString += " (Modul)";
                }
                dt.Rows.Add(predmet.idPredmet, predmet.imePredmeta, predmet.kreditneTocke, letnik, sestavniDelPredString, item.IdPredmetStudijskegaPrograma);
            }
            seznamPredmetovGV.DataSource = dt;
            seznamPredmetovGV.DataBind();
            seznamPredmetovGV.Visible = true;
            dodajPredmet.Style.Add("display", "block");
        }



        protected void seznamPredmetovGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int) seznamPredmetovGV.DataKeys[e.RowIndex].Values["IdStudPredmeta"];
            PredmetStudijskegaPrograma psp = (PredmetStudijskegaPrograma)(from p in db.PredmetStudijskegaPrograma where p.IdPredmetStudijskegaPrograma == id select p).FirstOrDefault();
            if (psp != null)
            {
                feedbackLB.Text = "Predmet " + psp.Predmet.imePredmeta + " je bil uspešno odstranjen iz predmetnika študijskega programa.";
                feedbackLB.Visible = true;
                db.PredmetStudijskegaPrograma.Remove(psp);
                db.SaveChanges();
                StudijskiProgramDDL_SelectedIndexChanged(null,null);
            }
            else
            {
                Console.WriteLine("Povpraševanje v bazo ni uspelo, imamo null objekt.");
            }
        }

        protected void SestavniDelPredDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            LetnikDDL.Visible = false;
            int selectedIndex = SestavniDelPredDDL.SelectedIndex;
            var letniki = (from l in db.Letnik select l.letnik1).ToList();
            Boolean visible = true;
            if (selectedIndex < 3)
            {
                letniki = (from l in db.Letnik select l.letnik1).ToList();
            }
            else if (selectedIndex >= 3)
            {
                letniki = (from l in db.Letnik where l.letnik1 == 3 select l.letnik1).ToList();
            }
            if (letniki != null)
            {
                LetnikDDL.DataSource = letniki;
                LetnikDDL.DataBind();
                LetnikDDL.Visible = true;
            }

        }

        protected void SestavniDelPredDDL_Edit_SelectedIndexChanged(object sender, EventArgs e)
        {
            LetnikDDL_Edit.Visible = false;
            int selectedIndex = SestavniDelPredDDL_Edit.SelectedIndex;
            var letniki = (from l in db.Letnik select l.letnik1).ToList();
            Boolean visible = true;
            if (selectedIndex < 3) 
            {
                letniki = (from l in db.Letnik select l.letnik1).ToList();
            }
            else if (selectedIndex >= 3)
            {
                letniki = (from l in db.Letnik where l.letnik1 == 3 select l.letnik1).ToList();
            }
            if (letniki != null)
            {
                LetnikDDL_Edit.DataSource = letniki;
                LetnikDDL_Edit.DataBind();
                LetnikDDL_Edit.Visible = true;
            }
        }

        protected void dodajPredmetButton_Click(object sender, EventArgs e)
        {
            String imePredmeta = seznamVsehPredmetovDDL.SelectedValue;
            var vsiPredmeti = (from p in db.Predmet select p).ToList();
            int idPredmet = -1;
            foreach (var item in vsiPredmeti)
            {
                if (item.imePredmeta.Equals(imePredmeta))
                {
                    idPredmet = item.idPredmet;
                }
            }

            int idSestavniDelPred = SestavniDelPredDDL.SelectedIndex + 1;
            int letnik = -1;
            if (LetnikDDL.Visible == true) {
                letnik = Int32.Parse(LetnikDDL.SelectedValue);
            }

            PredmetStudijskegaPrograma psp = new PredmetStudijskegaPrograma();
            psp.Predmet_idPredmet1 = idPredmet;
            psp.StudijskiProgram_idStudijskiProgram1 = StudijskiProgramDDL.SelectedIndex + 1;       psp.SestavniDelPred_idSestavniDelPred = idSestavniDelPred;
            if (letnik > 0)
            {
                psp.Letnik_idLetnik = letnik;
            }

            db.PredmetStudijskegaPrograma.Add(psp);
            db.SaveChanges();
            SestavniDelPredDDL.SelectedIndex = -1;
            LetnikDDL.Visible = false;
            feedbackLB.Text = "Predmet " + imePredmeta + " je bil uspešno dodan";
            feedbackLB.Visible = true;
            StudijskiProgramDDL_SelectedIndexChanged(null, null);
            //Response.Redirect(Request.Url.AbsoluteUri);


        }

        protected void seznamPredmetovGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            feedbackLB.Visible = false;
            seznamPredmetovGV.Visible = false;
            dodajPredmet.Style.Add("display", "none");
            editPredmet.Style.Add("display", "block");
            int id = (int)seznamPredmetovGV.DataKeys[e.NewEditIndex].Values["IdStudPredmeta"];
            PredmetStudijskegaPrograma psp = (PredmetStudijskegaPrograma)(from p in db.PredmetStudijskegaPrograma where p.IdPredmetStudijskegaPrograma == id select p).FirstOrDefault();
            hiddenIdPredmetStudPrograma_Edit.Value = id + "";
            editPredmetImeTB.Text = psp.Predmet.imePredmeta;
            editPredmetSifraTB.Text = psp.Predmet.idPredmet.ToString();
            editPredmetKreditneTB.Text = psp.Predmet.kreditneTocke.ToString();
            SestavniDelPredDDL_Edit.SelectedIndex = psp.SestavniDelPred_idSestavniDelPred - 1;
            if (psp.Letnik != null)
            {
                LetnikDDL_Edit.Visible = true;
                if (LetnikDDL_Edit.Items.Count == 1)
                {
                    LetnikDDL_Edit.SelectedIndex = 1;
                }
                else if (LetnikDDL_Edit.Items.Count == 2)
                {
                    LetnikDDL_Edit.SelectedIndex = psp.Letnik_idLetnik - 2;
                }
                else
                {
                    LetnikDDL_Edit.SelectedIndex = psp.Letnik_idLetnik - 1;
                }
            }

        }

        protected void editPredmetButton_Click(object sender, EventArgs e)
        {
            int idPredmetStudProg = Int32.Parse(hiddenIdPredmetStudPrograma_Edit.Value);
            String imePredmeta = editPredmetImeTB.Text;

            int idSestavniDelPred = SestavniDelPredDDL_Edit.SelectedIndex + 1;
            int letnik = -1;
            if (LetnikDDL_Edit.Visible == true)
            {
                letnik = Int32.Parse(LetnikDDL_Edit.SelectedValue);
            }

            PredmetStudijskegaPrograma psp = (PredmetStudijskegaPrograma)(from x in db.PredmetStudijskegaPrograma where x.IdPredmetStudijskegaPrograma == idPredmetStudProg select x).FirstOrDefault();
            psp.SestavniDelPred_idSestavniDelPred = idSestavniDelPred;
            if (letnik > 0)
            {
                psp.Letnik_idLetnik = letnik;
            }

            Predmet p = psp.Predmet;
            db.SaveChanges();

            feedbackLB.Text = "Predmet " + imePredmeta + " je bil uspešno spremenjen.";
            feedbackLB.Visible = true;
            seznamPredmetovGV.Visible = true;
            editPredmet.Style.Add("display", "none");
            dodajPredmet.Style.Add("display", "block");
            seznamPredmetovGV.EditIndex = -1;
            StudijskiProgramDDL_SelectedIndexChanged(null, null);
        }

        protected void seznamPredmetovGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            cancelEditPredmetButton_Click(null, null);
        }

        protected void seznamPredmetovGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            editPredmetButton_Click(null, null);
        }

        protected void cancelEditPredmetButton_Click(object sender, EventArgs e)
        {
            seznamPredmetovGV.Visible = true;
            editPredmet.Style.Add("display", "none");
            dodajPredmet.Style.Add("display", "block");
            feedbackLB.Visible = false;
            seznamPredmetovGV.EditIndex = -1;
            StudijskiProgramDDL_SelectedIndexChanged(null, null);
        }


    }
}