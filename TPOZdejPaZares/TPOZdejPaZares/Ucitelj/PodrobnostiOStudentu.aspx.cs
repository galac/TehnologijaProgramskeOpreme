using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares.Ucitelj
{
    public partial class PodrobnostiOStudentu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) {
                if (Session["vpisnaStudent00"] == null)
                    Response.Redirect("~/Ucitelj/IskanjeStudentov");

                int vpisnaStudenta = Convert.ToInt32(Session["vpisnaStudent00"]);
                t8_2015Entities db = new t8_2015Entities();
                var studenti = db.Student.ToList();

                var selectedStudent = from s in studenti
                                      where s.vpisnaStudenta == vpisnaStudenta
                                      select new
                                      {
                                          ImeInPriimek = s.priimekStudenta + ' ' + s.imeStudenta != null ? s.priimekStudenta + ' ' + s.imeStudenta : "",
                                          mailStudenta = s.mailStudenta != null ? s.mailStudenta : "",
                                          Telefon = s.Telefon != null ? s.Telefon : "",
                                          vpisnaStudenta = s.vpisnaStudenta != null ? s.vpisnaStudenta.ToString() : ""
                                      };

                DetailsViewPRO.DataSource = selectedStudent.ToList();
                DetailsViewPRO.DataBind();

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

                LblErrorBpro.Visible = false;
                if (sklepi.ToList().Count < 1)
                    LblErrorBpro.Visible = true;
                else
                {
                    GV_sklepiPRO.DataSource = sklepi.ToList();
                    GV_sklepiPRO.DataBind();
                }

               var vpisaniPredmeti = from v in db.VpisaniPredmet
                                        where v.Vpis.Student.vpisnaStudenta == vpisnaStudenta
                                        select new
                                        {
                                            ImePredmeta = v.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta,
                                            ID_VP = "" + v.idVpisaniPredmet
                                        };

                LB_ErrorVpisaniP.Visible = false;
                if (vpisaniPredmeti.Count() < 1)
                {
                    LB_ErrorVpisaniP.Visible = true;
                    DDL_VpisaniPredmeti.Visible = false;
                    LB_ErrorVpisaniP.Text = "Študent pri zadnjem vpisu še ni prijavljen na noben predmet.";
                }
                else
                {
                    List<object> VPs = new List<object> { };
                    foreach (var i in vpisaniPredmeti)
                    {
                        var currentObj = new
                        {
                            DataValueField = i.ID_VP,
                            DataTextField = i.ImePredmeta
                        };
                        VPs.Add(currentObj);
                    }
                    var nedoloceno = new { DataTextField = "nedoločeno", DataValueField = "XXXX/XXXX" };
                    VPs.Insert(0, nedoloceno);
                    DDL_VpisaniPredmeti.Visible = true;
                    DDL_VpisaniPredmeti.DataSource = VPs;
                    DDL_VpisaniPredmeti.DataTextField = "DataTextField";
                    DDL_VpisaniPredmeti.DataValueField = "DataValueField";
                    DDL_VpisaniPredmeti.DataBind();
                    DDL_VpisaniPredmeti.SelectedValue = "XXXX/XXXX";
                    DDL_VpisaniPredmeti_SelectedIndexChanged(sender, e);
                }
                   
                
                Session["novaVpisna"] = Session["vpisnaStudent00"];
                Session["vpisnaStudent00"] = null;
            }
        }

        protected void DDL_VpisaniPredmeti_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_izpitniRoki.Visible = false;
            if (DDL_VpisaniPredmeti.SelectedValue == "XXXX/XXXX")
                return;

            t8_2015Entities db = new t8_2015Entities();
            int idVpisaniPredmet = Convert.ToInt32(DDL_VpisaniPredmeti.SelectedValue);
            var idIzvedbaPredmeta = db.VpisaniPredmet.Single(x => x.idVpisaniPredmet == idVpisaniPredmet).IzvedbaPredmeta_idIzvedbaPredmeta;
            
            
            var izpitniRokiRAW = (from i in db.IzpitniRok
                                  orderby i.VrstaIzpitnegaRoka descending, i.DatumIzpitnegaRoka
                                  where i.IzvedbaPredmeta_idIzvedbaPredmeta == idIzvedbaPredmeta
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

            GV_izpitniRoki.Visible = true;
            if (izpitniRoki.Count < 1)
            {
                var niRokov = new { DatumIzpitnegaRoka = "", IDIzpitniRok = "", IzvedbaPredmeta = "", Prostor = "", SteviloMest = "", VrstaIzpitnegaRoka = "", ZaporednaStevilka = "", IzvedbaPredmeta_idIzvedbaPredmeta = "", SteviloPrijav = "" };
                List<object> niRokovlist = new List<object>();
                niRokovlist.Insert(0, niRokov);
                GV_izpitniRoki.DataSource = niRokovlist;
                GV_izpitniRoki.DataBind();
                GV_izpitniRoki.Rows[0].Visible = false;
            }
            else
            {
                GV_izpitniRoki.DataSource = izpitniRoki;
                GV_izpitniRoki.DataBind();
                GV_izpitniRoki.Rows[0].Visible = true;
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmpasd", "<script type='text/javascript'>setDatepickers();</script>", false);

            var predmetID = db.VpisaniPredmet.Single(x => x.idVpisaniPredmet == idVpisaniPredmet).IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet_idPredmet1;

            var izvedbePredmeta = (from i in db.IzvedbaPredmeta
                                  where i.PredmetStudijskegaPrograma.Predmet_idPredmet1 == predmetID
                                  select new {
                                    i.idIzvedbaPredmeta,
                                    Prof1I = i.Profesor.imeProfesorja != null ? i.Profesor.imeProfesorja : "",
                                    Prof2I = i.Profesor1.imeProfesorja != null ? i.Profesor1.imeProfesorja : "",
                                    Prof3I = i.Profesor2.imeProfesorja != null ? i.Profesor2.imeProfesorja : "",
                                    Prof1P = i.Profesor.priimekProfesorja != null ? i.Profesor.priimekProfesorja : "",
                                    Prof2P = i.Profesor1.priimekProfesorja != null ? i.Profesor1.priimekProfesorja : "",
                                    Prof3P = i.Profesor2.priimekProfesorja != null ? i.Profesor2.priimekProfesorja : "",
                                    Leto = i.StudijskoLeto.studijskoLeto1
                                  }).AsEnumerable().Select(x => new
                                   {
                                       DataTextField = String.Format("{0}{1}{2}{3}{4}{5}; ({6})", x.Prof1I.Length > 0 ? x.Prof1I[0] + ". " : "", x.Prof1P, x.Prof2I.Length > 0 ? ", " + x.Prof2I[0] + ". " : "", x.Prof2P, x.Prof3I.Length > 0 ? ", " + x.Prof3I[0] + ". " : "", x.Prof3P, x.Leto),
                                       DataValueField = x.idIzvedbaPredmeta
                                   }).OrderBy(x => x.DataTextField);

            DropDownList DDL_izvedba = GV_izpitniRoki.FooterRow.FindControl("DDL_izvedba") as DropDownList;
            DDL_izvedba.DataSource = izvedbePredmeta;
            DDL_izvedba.DataTextField = "DataTextField";
            DDL_izvedba.DataValueField = "DataValueField";
            DDL_izvedba.DataBind();
        }

        protected void GV_izpitniRoki_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();
            int idVpisaniPredmet = Convert.ToInt32(DDL_VpisaniPredmeti.SelectedValue);
            
            if (e.CommandName == "Insert")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int rIndex = row.RowIndex;
                int idIzpitniRok = Convert.ToInt32(GV_izpitniRoki.DataKeys[rIndex].Values[0]);
                TextBox TB_ocena = GV_izpitniRoki.Rows[rIndex].FindControl("TB_KoncnaOcena") as TextBox;
                
                if(TB_ocena.Text == "") {
                    //label
                    return;
                }                    
                
                PrijavaNaIzpit novaPrijava = new PrijavaNaIzpit()
                {
                    DatumPrijave = DateTime.Now,
                    DatumSpremembe = DateTime.Now,
                    StatusPrijave = db.StatusPrijave.Find(3),
                    VpisaniPredmet = db.VpisaniPredmet.Find(idVpisaniPredmet),
                    OcenaIzpita = Convert.ToInt32(TB_ocena.Text) ,
                    IzpitniRok = db.IzpitniRok.Find(idIzpitniRok)
                };

                db.PrijavaNaIzpit.Add(novaPrijava);
                db.SaveChanges();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>alert('Vnos končne ocene na izbrani izpitni rok uspel.');</script>", false);
                DDL_VpisaniPredmeti.SelectedIndex = 0;
                DDL_VpisaniPredmeti_SelectedIndexChanged(sender, e);
            }
            else if (e.CommandName == "InsertFooter")
            {
                TextBox TB_ocena = GV_izpitniRoki.FooterRow.FindControl("TB_KoncnaOcenaF") as TextBox;
                TextBox TB_DatumIzpitnegaRoka = GV_izpitniRoki.FooterRow.FindControl("TB_DatumIzpitnegaRoka") as TextBox;
                DropDownList DDL_izvedba = GV_izpitniRoki.FooterRow.FindControl("DDL_izvedba") as DropDownList;

                if(TB_ocena.Text == "") {
                    //label
                    return;
                }

                DateTime datumIzpitnegaRoka = DateTime.ParseExact(TB_DatumIzpitnegaRoka.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                int idNovaIzvedba = Convert.ToInt32(DDL_izvedba.SelectedValue);
                VpisaniPredmet vpisaniPredmet = db.VpisaniPredmet.Find(idVpisaniPredmet);
                IzvedbaPredmeta izvedbaPredmeta = db.IzvedbaPredmeta.Find(idNovaIzvedba);
                vpisaniPredmet.IzvedbaPredmeta = izvedbaPredmeta;
                db.SaveChanges();

                IzpitniRok izpitniRok = new IzpitniRok
                {
                    DatumIzpitnegaRoka = datumIzpitnegaRoka,
                    VrstaIzpitnegaRoka = "mimo roka",
                    IzvedbaPredmeta = db.VpisaniPredmet.Find(idVpisaniPredmet).IzvedbaPredmeta,
                    Veljaven = 0
                };

                db.IzpitniRok.Add(izpitniRok);
                
                PrijavaNaIzpit novaPrijava = new PrijavaNaIzpit()
                {
                    DatumPrijave = DateTime.Now,
                    DatumSpremembe = DateTime.Now,
                    StatusPrijave = db.StatusPrijave.Find(3),
                    VpisaniPredmet = db.VpisaniPredmet.Find(idVpisaniPredmet),
                    OcenaIzpita = Convert.ToInt32(TB_ocena.Text) ,
                    IzpitniRok = izpitniRok
                };

                db.PrijavaNaIzpit.Add(novaPrijava);
                db.SaveChanges();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp1", "<script type='text/javascript'>alert('Vnos končne ocene mimo roka uspel.');</script>", false);
                DDL_VpisaniPredmeti.SelectedIndex = 0;
                DDL_VpisaniPredmeti_SelectedIndexChanged(sender, e);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmpasd", "<script type='text/javascript'>setDatepickers();</script>", false);
        }
    }
}