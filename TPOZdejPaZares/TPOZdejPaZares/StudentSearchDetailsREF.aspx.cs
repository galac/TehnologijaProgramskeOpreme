using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares
{
    public partial class StudentSearchDetailsREF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["studentID"] = 1;

            if (Session["studentID"] == null)
                Response.Redirect("~/Student");

            int vpisnaStudenta = Convert.ToInt32(Session["studentID"]);
            t8_2015Entities db = new t8_2015Entities();
            var studenti = db.Student.ToList();

            var selectedStudent = from s in studenti
                                  where s.vpisnaStudenta == vpisnaStudenta
                                  select new
                                  {
                                      ImeInPriimek = s.imeStudenta + ' ' + s.priimekStudenta != null ? s.imeStudenta + ' ' + s.priimekStudenta : "",
                                      mailStudenta = s.mailStudenta != null ? s.mailStudenta : "",
                                      Telefon = s.Telefon != null ? s.Telefon : "",
                                      vpisnaStudenta = s.vpisnaStudenta != null ? s.vpisnaStudenta.ToString() : "",
                                      Naslov = s.Naslov != null ? s.Naslov : "",
                                      posta_idPosta = s.Posta_idPosta != null ? s.Posta_idPosta.ToString() : "",
                                      Posta = s.Posta != null ? s.Posta.postnaStevilka : "",
                                      Obcina = s.Obcina != null ? s.Obcina.imeObcine : "",
                                      Drzava = s.Drzava != null ? s.Drzava.imeDrzave : "",
                                      Spol =  s.Spol,
                                      s.EMSO,
                                      s.DavcnaStevilka,
                                      DatumRojstva = String.Format("{0:dd.MM.yyyy}", s.DatumRojstva),
                                      DrzavaRojstva = s.Drzava2 != null ? s.Drzava2.imeDrzave : "",
                                      Drzavljanstvo = s.Drzava1 != null ? s.Drzava1.imeDrzave : "",
                                      s.NaslovZacasni,
                                      ZacasnaPosta = s.Posta1 != null ? s.Posta1.postnaStevilka : "",
                                      ZacasnaPostnaStevilka = s.Posta1 != null ? s.Posta1.idPosta.ToString() : "",
                                      ZacasnaDrzava = s.Drzava3 != null ? s.Drzava3.imeDrzave : "",
                                      ZacasnaObcina = s.Obcina1 != null ? s.Obcina1.imeObcine : "",
                                      s.Vpis
                                  };

            DetailsView1.DataSource = selectedStudent.ToList();
            DetailsView1.DataBind();

            var vpisi = selectedStudent.ToList().Single().Vpis.ToList();

            LblErrorA.Visible = false;
            if (vpisi.Count < 1)
                LblErrorA.Visible = true;

            for(int i = 0; i < vpisi.Count; i++) {
                DetailsView dv = new DetailsView();
                dv.ID = "MyDv"+i;
                dv.AutoGenerateRows = false;
                dv.CssClass = "table table-striped";
                dv.FieldHeaderStyle.Font.Bold = true;

                BoundField bf1 = new BoundField();
                bf1.DataField = "Letnik";
                bf1.HeaderText = "Letnik študija";
                dv.Fields.Add(bf1);
                BoundField bf4 = new BoundField();
                bf4.DataField = "StudijskiProgram";
                bf4.HeaderText = "Študijski program";
                dv.Fields.Add(bf4);
                BoundField bf2 = new BoundField();
                bf2.DataField = "VrstaVpisa";
                bf2.HeaderText = "Vrsta vpisa";
                dv.Fields.Add(bf2);
                HyperLinkField hlf = new HyperLinkField();
                hlf.DataTextField = "Predmetnik";
                hlf.HeaderText = "Predmetnik";
                dv.Fields.Add(hlf);
                BoundField bf3 = new BoundField();
                bf3.DataField = "OblikaStudija";
                bf3.HeaderText = "Oblika študija";
                dv.Fields.Add(bf3);
                BoundField bf5 = new BoundField();
                bf5.DataField = "NacinStudija";
                bf5.HeaderText = "Način študija";
                dv.Fields.Add(bf5);

                dv.DataSource = new[] { new 
                        { 
                            Letnik = vpisi[i].Letnik != null ? vpisi[i].Letnik.idLetnik.ToString() : "", 
                            StudijskiProgram = vpisi[i].StudijskiProgram != null ? vpisi[i].StudijskiProgram.naziv : "",
                            VrstaVpisa = vpisi[i].VrstaVpisa != null ? vpisi[i].VrstaVpisa.opisVpisa : "",
                            Predmetnik = "",
                            OblikaStudija = vpisi[i].OblikaStudija != null ? vpisi[i].OblikaStudija.opisOblike : "",
                            NacinStudija = vpisi[i].NacinStudija != null ? vpisi[i].NacinStudija.opisNacina : ""
                        } 
                };

                dv.DataBind();
                HyperLink hl = new HyperLink();
                var builder = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port, "Referent/IzbiraPredmetovReferent.aspx","?idVpis=" + vpisi[i].idVpis);
                hl.Text = "predmetnik";
                hl.NavigateUrl = builder.ToString();
                dv.Rows[3].Cells[1].Controls.Add(hl);
                PlaceHolder1.Controls.Add(dv);
            }


            var sklepi = from s in db.Sklep
                         where s.Student.vpisnaStudenta == vpisnaStudenta
                         select new
                         {
                             s.Organ,
                             s.idSklep,
                             s.VsebinaSklepa,
                             s.DatumSprejetjaSklepa,
                             s.DatumVeljaveSklepa
                         };

            LblErrorB.Visible = false;
            if (sklepi.ToList().Count < 1)
                LblErrorB.Visible = true;
            else 
            {
                GV_sklepi.DataSource = sklepi.ToList();
                GV_sklepi.DataBind();
            }

            Session["studentID"] = null;
        }
    }
}