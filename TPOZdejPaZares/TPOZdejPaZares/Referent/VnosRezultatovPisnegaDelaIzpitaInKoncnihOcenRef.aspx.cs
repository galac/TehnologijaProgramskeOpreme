using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares.Referent
{
    public partial class VnosRezultatovPisnegaDelaIzpitaInKoncnihOcenRef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();

            if (!IsPostBack)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("sl-SI");

                // polnenje lista
                var studijskaLeta = (from s in db.StudijskoLeto
                                     orderby s.idStudijskoLeto
                                     select new
                                     {
                                         DataTextField = s.studijskoLeto1,
                                         DataValueField = "" + s.idStudijskoLeto
                                     }).ToList();

                var nedoloceno = new { DataTextField = "nedoločeno", DataValueField = "XXXX/XXXX" };
                studijskaLeta.Insert(0, nedoloceno);

                DDL_Years.DataSource = studijskaLeta;
                DDL_Years.DataTextField = "DataTextField";
                DDL_Years.DataValueField = "DataValueField";
                DDL_Years.DataBind();
                DDL_Years.SelectedValue = studijskaLeta.Single(x => x.DataTextField == String.Format("{0:yyyy}/{1:yyyy}", DateTime.Now, DateTime.Now.AddYears(1))).DataValueField;

                // priprava drugega 
                DDL_SubjectList.Items.Clear();
                DDL_SubjectList.Items.Add(new System.Web.UI.WebControls.ListItem("nedoločeno", "XXXXX"));               

                DDL_Years_SelectedIndexChanged(sender, e);
            }
        }


        //************************************************************************************************//
        //                                      EVENT HANDLERS                                            //
        //************************************************************************************************//


        protected void DDL_Years_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_subjects.SelectedIndex = -1;
            header31.Visible = false;
            GV_subjects.Visible = false;

            header41.Visible = false;
            L_Error.Visible = false;
            GV_izpitniRoki.Visible = false;
            GV_izpitniRoki.SelectedIndex = -1;

            GV_seznamStudentov_O.Visible = false;
            LB_errorSeznam.Visible = false;
            L_headerSeznamStudentov.Visible = false;

            if (DDL_Years.SelectedValue == "XXXX/XXXX")
            {
                DDL_SubjectList.Items.Clear();
                DDL_SubjectList.Items.Add(new System.Web.UI.WebControls.ListItem("nedoločeno", "XXXXX"));
                return;
            }

            int studijskoLetoID = Convert.ToInt32(DDL_Years.SelectedValue);

            t8_2015Entities db = new t8_2015Entities();

            var relevantniPredmeti = (((from i in db.IzvedbaPredmeta
                                        where i.StudijskoLeto_idStudijskoLeto == studijskoLetoID
                                        select new
                                        {
                                            DataTextField = i.PredmetStudijskegaPrograma.Predmet.imePredmeta + "  (" + i.PredmetStudijskegaPrograma.Predmet.idPredmet + ")",
                                            DataValueField = "" + i.PredmetStudijskegaPrograma.Predmet.idPredmet
                                        }).Distinct()).OrderBy(i => i.DataTextField)).ToList();

            if (relevantniPredmeti.Count < 1)
            {
                return;
            }

            var nedolocen = new { DataTextField = "nedoločeno", DataValueField = "XXXXX" };
            relevantniPredmeti.Insert(0, nedolocen);

            DDL_SubjectList.DataSource = relevantniPredmeti;
            DDL_SubjectList.DataTextField = "DataTextField";
            DDL_SubjectList.DataValueField = "DataValueField";
            DDL_SubjectList.DataBind();
            DDL_SubjectList.SelectedValue = "XXXXX";
        }

        protected void DDL_SubjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_subjects.SelectedIndex = -1;
            header31.Visible = false;
            GV_subjects.Visible = false;

            header41.Visible = false;
            L_Error.Visible = false;
            GV_izpitniRoki.Visible = false;
            GV_izpitniRoki.SelectedIndex = -1;

            GV_seznamStudentov_O.Visible = false;
            LB_errorSeznam.Visible = false;
            L_headerSeznamStudentov.Visible = false;

            if (DDL_SubjectList.SelectedValue == "XXXXX")
            {
                return;
            }

            t8_2015Entities db = new t8_2015Entities();
            int predmetID = Convert.ToInt32(DDL_SubjectList.SelectedValue);
            int studijskoLetoID = Convert.ToInt32(DDL_Years.SelectedValue);


            var izvedbePredmeta = (from i in db.IzvedbaPredmeta
                                   where i.PredmetStudijskegaPrograma.Predmet.idPredmet == predmetID
                                   && i.StudijskoLeto_idStudijskoLeto == studijskoLetoID
                                   select new
                                   {
                                       StudijskiProgram = i.PredmetStudijskegaPrograma.StudijskiProgram.naziv != null ? i.PredmetStudijskegaPrograma.StudijskiProgram.naziv : " ",
                                       Letnik = i.PredmetStudijskegaPrograma.Letnik.letnik1 != null ? i.PredmetStudijskegaPrograma.Letnik.letnik1.ToString() : " ",
                                       SestavniDelPred = i.PredmetStudijskegaPrograma.SestavniDelPred.opisSestavnegaDela != null ? i.PredmetStudijskegaPrograma.SestavniDelPred.opisSestavnegaDela : " ",
                                       KreditneTocke = i.PredmetStudijskegaPrograma.Predmet.kreditneTocke != null ? i.PredmetStudijskegaPrograma.Predmet.kreditneTocke.ToString() : " ",
                                       Prof1I = i.Profesor.imeProfesorja != null ? i.Profesor.imeProfesorja : "",
                                       Prof2I = i.Profesor1.imeProfesorja != null ? i.Profesor1.imeProfesorja : "",
                                       Prof3I = i.Profesor2.imeProfesorja != null ? i.Profesor2.imeProfesorja : "",
                                       Prof1P = i.Profesor.priimekProfesorja != null ? i.Profesor.priimekProfesorja : "",
                                       Prof2P = i.Profesor1.priimekProfesorja != null ? i.Profesor1.priimekProfesorja : "",
                                       Prof3P = i.Profesor2.priimekProfesorja != null ? i.Profesor2.priimekProfesorja : "",
                                       i.idIzvedbaPredmeta
                                   }).AsEnumerable().Select(x => new
                                   {
                                       x.StudijskiProgram,
                                       x.Letnik,
                                       x.SestavniDelPred,
                                       x.KreditneTocke,
                                       Izvajalci = String.Format("{0}.{1}, {2}{3}, {4}{5}", x.Prof1I.Length > 0 ? x.Prof1I[0] + ". " : "", x.Prof1P, x.Prof2I.Length > 0 ? x.Prof2I[0] + ". " : "", x.Prof2P, x.Prof3I.Length > 0 ? x.Prof3I[0] + ". " : "", x.Prof3P),
                                       x.idIzvedbaPredmeta
                                   });


            GV_subjects.DataSource = izvedbePredmeta.ToList();
            GV_subjects.DataBind();

            header41.Visible = true;
            GV_subjects.Visible = true;
        }

        protected void GV_subjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ponastavitve
            L_Error.Visible = false;
            header31.Visible = true;
            GV_izpitniRoki.Visible = false;
            GV_izpitniRoki.SelectedIndex = -1;

            GV_seznamStudentov_O.Visible = false;
            LB_errorSeznam.Visible = false;
            L_headerSeznamStudentov.Visible = false;

            if (Session["rowEditing"] != null)
                if (!Session["rowEditing"].Equals("true"))
                {
                    GV_izpitniRoki.EditIndex = -1;
                }
            
            L_header31.Text = "Seznam izpitnih rokov za predmet \"" + DDL_SubjectList.SelectedItem + "\"";
            int izvedbaPredmetaID = Convert.ToInt32(GV_subjects.DataKeys[GV_subjects.SelectedIndex].Values[0]);
            
            t8_2015Entities db = new t8_2015Entities();

            var izpitniRokiRAW = (from i in db.IzpitniRok
                                  orderby i.VrstaIzpitnegaRoka descending, i.DatumIzpitnegaRoka
                                  where i.IzvedbaPredmeta_idIzvedbaPredmeta == izvedbaPredmetaID
                                  where i.Veljaven == 1
                                  select i).ToList();

            int stevecRedni = 1;
            int stevecIzredni = 1;
            List<object> izpitniRoki = new List<object> { };
            foreach (var i in izpitniRokiRAW)
            {
                var currentObj = new
                {
                    i.DatumIzpitnegaRoka,
                    i.IDIzpitniRok,
                    i.IzvedbaPredmeta,
                    i.IzvedbaPredmeta_idIzvedbaPredmeta,
                    i.Prostor,
                    SteviloMest = i.SteviloMest != 0 ? i.SteviloMest + "" : "/",
                    i.VrstaIzpitnegaRoka,
                    ZaporednaStevilka = (i.VrstaIzpitnegaRoka == "redni" ? stevecRedni++ : stevecIzredni++),
                    SteviloPrijav = i.PrijavaNaIzpit.Where(x => x.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit < 7).Count()
                };
                izpitniRoki.Add(currentObj);
            }

            if (izpitniRoki.Count < 1)
            {
                L_Error.Text = "Izbrana izvedba študijskega programa trenutno nima razpisanih izpitnih rokov.";
                L_Error.Visible = true;
                return;
            }

            GV_izpitniRoki.Visible = true;
            GV_izpitniRoki.DataSource = izpitniRoki;
            GV_izpitniRoki.DataBind();
            this.GV_izpitniRoki_SetLinkButtons();
        }

        protected void GV_izpitniRoki_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // ponastavitve
            GV_seznamStudentov_O.Visible = false;
            LB_errorSeznam.Visible = false;
           
            // handlanje LinkButton-ov
            string[] zlom = e.CommandArgument.ToString().Split(';');
            int idIzpitniRok = Convert.ToInt32(zlom[0]);
            t8_2015Entities db = new t8_2015Entities();

            // za roke, ki še niso bili izvedeni
            if (e.CommandName == "CMD_ocene")
            {
                GV_seznamStudentov_O_DataBind(idIzpitniRok);
            }

            GV_izpitniRoki.SelectedIndex = Convert.ToInt32(zlom[1]);
            L_headerSeznamStudentov.Visible = true;
            L_headerSeznamStudentov.Text = "Seznam študentov na izpitnem roku " + (GV_izpitniRoki.Rows[GV_izpitniRoki.SelectedIndex].FindControl("L_VrstaIzpitnegaRoka") as Label).Text.ToUpper() + " " + (GV_izpitniRoki.Rows[GV_izpitniRoki.SelectedIndex].FindControl("L_DatumIzpitnegaRoka") as Label).Text;
        }  

               
        protected void GV_seznamStudentov_O_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // ponastavitve
            GV_seznamStudentov_O.Visible = false;
            LB_errorSeznam.Visible = false;

            // handlanje LinkButton-ov
            t8_2015Entities db = new t8_2015Entities();

            // za vračanje prijav
            if (e.CommandName == "CMD_vnosOcenO")
            {
                int stevecVP = 0;

                foreach (GridViewRow row in GV_seznamStudentov_O.Rows)
                {
                    TextBox TB_VnosOcenO = row.FindControl("TB_VnosOcenO") as TextBox;
                    TextBox TB_VnosTock = row.FindControl("TB_VnosTock") as TextBox;

                    // ne naredi nič če sta obe polji prazni
                    if (TB_VnosOcenO.Text == "" && TB_VnosTock.Text == "")
                        continue;

                    // odjavi študenta - popravi status prijave če je neveljavna
                    else if (TB_VnosOcenO.Text.ToLower() == "vp" || TB_VnosTock.Text.ToLower() == "vp") {

                        int idPrijava = Convert.ToInt32(GV_seznamStudentov_O.DataKeys[row.RowIndex].Values[1]);
                        PrijavaNaIzpit prijava = (from i in db.PrijavaNaIzpit
                                                  where i.idPrijavaNaizpit == idPrijava
                                                  select i).Single();

                        prijava.StatusPrijave = db.StatusPrijave.Find(10);

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception) { }
                        stevecVP++;
                    }

                    // drugače shranimo vnos
                    else {
                        int idPrijava = Convert.ToInt32(GV_seznamStudentov_O.DataKeys[row.RowIndex].Values[1]);
                        PrijavaNaIzpit prijava = (from i in db.PrijavaNaIzpit
                                                  where i.idPrijavaNaizpit == idPrijava
                                                  select i).Single();
                        VpisaniPredmet vpisaniPredmet = db.VpisaniPredmet.Where(x => x.idVpisaniPredmet == prijava.VpisaniPredmet_IdVpisanipredmet).Single();

                        if (TB_VnosTock.Text != "")
                            prijava.StTock = Convert.ToInt32(TB_VnosTock.Text);
                        if (TB_VnosOcenO.Text != "")
                        {
                            prijava.OcenaIzpita = Convert.ToInt32(TB_VnosOcenO.Text);
                            vpisaniPredmet.ocena = Convert.ToInt32(TB_VnosOcenO.Text);
                        }

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception) { }
                    }                   
                }

                int idPrijava0 = Convert.ToInt32(GV_seznamStudentov_O.DataKeys[0].Values[1]);
                int idIzpitniRok = Convert.ToInt32(db.PrijavaNaIzpit.Single(x => x.idPrijavaNaizpit == idPrijava0).IzpitniRok_IdIzpitniRok);
                GV_seznamStudentov_O_DataBind(idIzpitniRok);
                
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aaabbb", "<script type='text/javascript'>alert('Vpisane točke pisnega dela izpita in končne ocene so bile uspešno vnešene. Št. vrnjenih prijav je enako "+stevecVP+".')</script>", false);
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setDatepickers();</script>", false); 
        }
        
       
        //************************************************************************************************//
        //                                      CUSTOM FUNCTIONS                                          //
        //************************************************************************************************//

        protected void GV_izpitniRoki_SetLinkButtons() {
            foreach (GridViewRow row in GV_izpitniRoki.Rows) {
                LinkButton LB_prikaziSeznam = row.FindControl("LB_prikaziSeznam") as LinkButton;
                Label L_DatumIzpitnegaRoka = row.FindControl("L_DatumIzpitnegaRoka") as Label;
                Label L_Ura = row.FindControl("L_Ura") as Label;
                Label L_rokiInfo = row.FindControl("L_rokiInfo") as Label;

                DateTime datumUraRoka = DateTime.ParseExact(L_DatumIzpitnegaRoka.Text + L_Ura.Text, "dd. MM. yyyyhh:mm", CultureInfo.InvariantCulture);

                if (datumUraRoka > DateTime.Now) {
                    LB_prikaziSeznam.Visible = false;
                    L_rokiInfo.Visible = true;
                }
                else {
                    LB_prikaziSeznam.Visible = true;
                    L_rokiInfo.Visible = false;
                    LB_prikaziSeznam.Text = "VNOS TOČK IN KONČNIH OCEN";
                    LB_prikaziSeznam.CssClass = "btn btn-success";
                    LB_prikaziSeznam.CommandName = "CMD_ocene";
                }
                LB_prikaziSeznam.CommandArgument = GV_izpitniRoki.DataKeys[row.RowIndex].Values[0]+";"+row.RowIndex;
            }
        }             
    
        protected void GV_seznamStudentov_O_DataBind(int idIzpitniRok)
        {
            t8_2015Entities db = new t8_2015Entities();

            var izpitniRok = (from i in db.IzpitniRok
                              where i.IDIzpitniRok == idIzpitniRok
                              select i).Single();

            var seznamPrijavljenih = (from pni in db.PrijavaNaIzpit
                                      where pni.IzpitniRok_IdIzpitniRok == idIzpitniRok
                                      && pni.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit < 7 //|| pni.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit == 10)
                                      select new
                                      {
                                          Student = pni.VpisaniPredmet.Vpis.Student,
                                          Status = pni.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit == 10 ? "VP" : "aktivna",
                                          IdPrijavaNaIzpit = pni.idPrijavaNaizpit,
                                          pni.OcenaIzpita,
                                          pni.StTock
                                      }).Distinct().ToList().OrderBy(i => i.Student.priimekStudenta);

            int stevec = 1;
            List<object> koncniSeznam = new List<object> { };
            string LetoPoslusanja = izpitniRok.IzvedbaPredmeta.StudijskoLeto != null ? izpitniRok.IzvedbaPredmeta.StudijskoLeto.studijskoLeto1 : "";

            foreach (var s in seznamPrijavljenih)
            {
                int stPolaganj = (from pni in db.PrijavaNaIzpit
                                  where pni.VpisaniPredmet.Vpis.Student.vpisnaStudenta == s.Student.vpisnaStudenta &&
                                        pni.VpisaniPredmet.IzvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId == izpitniRok.IzvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId
                                  select pni).Distinct().ToList().Count + 1;

                var currentObj = new
                {
                    ZaporednaStevilka = stevec++,
                    VpisnaStevilka = s.Student.vpisnaStudenta != null ? s.Student.vpisnaStudenta.ToString() : "",
                    PriimekInIme = (s.Student.priimekStudenta != null ? s.Student.priimekStudenta : "") + " " + (s.Student.imeStudenta != null ? s.Student.imeStudenta : ""),
                    StudijskoLeto = izpitniRok.IzvedbaPredmeta.StudijskoLeto != null ? izpitniRok.IzvedbaPredmeta.StudijskoLeto.studijskoLeto1.ToString() : "",
                    Polaganje = stPolaganj,
                    s.Student.idStudent,
                    s.Status,
                    s.IdPrijavaNaIzpit,
                    s.OcenaIzpita,
                    s.StTock
                };
                koncniSeznam.Add(currentObj);
            }

            if (koncniSeznam.Count < 1)
            {
                LB_errorSeznam.Text = "Na izbranem roku ni bilo prijavljenih študentov oziroma so bile njihove prijave vrnjene.";
                LB_errorSeznam.Visible = true;
                return;
            }

            GV_seznamStudentov_O.DataSource = koncniSeznam;
            GV_seznamStudentov_O.DataBind();
            GV_seznamStudentov_O.Visible = true;

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setDatepickers();</script>", false);            
        }      
    }
}