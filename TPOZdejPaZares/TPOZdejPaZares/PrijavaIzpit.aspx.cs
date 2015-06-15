using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Web.Security;

namespace TPOZdejPaZares
{
    public partial class PrijavaIzpit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.IsInRole("1"))
                {
                    if (Session["uporabnikID"] != null) { UpdateTables((int)Session["uporabnikID"]); }      
                    
                }
                else if (HttpContext.Current.User.IsInRole("2"))
                {
                    t8_2015Entities db = new t8_2015Entities();
                    GVIzbiraStudenta.Visible = true;
                    GVIzbiraStudenta.Enabled = true;
                    var studenti = (from s in db.Student
                                    select new
                                    {
                                        imeStudenta = s.imeStudenta,
                                        priimekStudenta = s.priimekStudenta,
                                        mailStudenta = s.mailStudenta,
                                        vpisnaStudenta = s.vpisnaStudenta,
                                        idStudent = s.idStudent,
                                    }).ToList();
                    GVIzbiraStudenta.DataSource = studenti;
                    GVIzbiraStudenta.DataBind();
                    DivPrijava.Visible = false;
                }
                
            }
        }
        void UpdateTables(int id) {
            
            t8_2015Entities db = new t8_2015Entities();
            // int id =(int) Session["uporabnikID"];
            Console.Write("uporabnikov ID");
            Session["jePavzer"] = false;
            var vsiVpisi = db.Vpis.Where(i => i.Student_idStudent1 == id).OrderByDescending(i => i.Letnik_idLetnik).ToList();
            var vpis = vsiVpisi.FirstOrDefault();
            if (vpis != null)
            {
                
                DivPrijava.Visible = true;
                if (vpis.NacinStudija_idNacinStudija == 2)
                {
                    Session["jePavzer"] = true;
                }
                vsiVpisi.Remove(vsiVpisi.FirstOrDefault());
                List<object> listaStarih = new List<object> { };
                

                if (vpis.VpisaniPredmet != null)
                {
                    //   lista.AddRange(currVp.VpisaniPredmet);
                    foreach (var predmet in vpis.VpisaniPredmet)
                    {
                        var prijave = predmet.PrijavaNaIzpit.OrderByDescending(i => i.DatumPrijave).ToList();
                        var prijava = prijave.FirstOrDefault();
                        /* foreach (var prijava in predmet.PrijavaNaIzpit)
                        {
                            if (prijava.IzpitniRok.DatumIzpitnegaRoka <= DateTime.Now)
                            {*/
                        int ocenaI = 0;

                        if (prijava != null)
                        {
                            if (prijava.OcenaIzpita != null) ocenaI = (int)(prijava.OcenaIzpita);
                        }
                        var item = new
                        {
                            imePredmeta = predmet.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta != null ? predmet.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta : " ime predmeta",
                            izvajalecPredmeta = predmet.IzvedbaPredmeta.Profesor.imeProfesorja != null ? predmet.IzvedbaPredmeta.Profesor.imeProfesorja : " ime profesorja ",
                            izvajalecPriimek = predmet.IzvedbaPredmeta.Profesor.priimekProfesorja != null ? predmet.IzvedbaPredmeta.Profesor.priimekProfesorja : " priimek profesorja ",
                            idVpisaP = predmet.idVpisaniPredmet != null ? predmet.idVpisaniPredmet : 0,
                            ocena = ocenaI,

                        };
                        listaStarih.Add(item);
                        /*}
                    }*/
                    }
                }



                var izvedbePredmetov = (from p in db.VpisaniPredmet
                                        where p.Vpis_idVpis == vpis.idVpis
                                        select new
                                        {
                                            izv = p.IzvedbaPredmeta,
                                            idVpPredmet = p.idVpisaniPredmet,
                                        }).ToList();
                /*var izpitniRoki = (from p in izvedbePredmetov 
                                    join i in db.IzpitniRok on p equals i.IzvedbaPredmeta
                                    select new{
                                        imePredmeta = p.PredmetStudijskegaPrograma.Predmet.imePredmeta != null ? p.PredmetStudijskegaPrograma.Predmet.imePredmeta : " ime predmeta",
                                        izvajalecPredmeta = p.Profesor.imeProfesorja != null ? p.Profesor.imeProfesorja : " ime profesorja ",
                                        izvajalecPriimek = p.Profesor.priimekProfesorja != null ? p.Profesor.priimekProfesorja : " priimek profesorja ",
                                        vrstaRoka = i.VrstaIzpitnegaRoka!= null ? i.VrstaIzpitnegaRoka : " vrsta roka",
                                        Datum = i.DatumIzpitnegaRoka!= null ? i.DatumIzpitnegaRoka : DateTime.Now,             
                                    }).ToList();*/
                List<object> lista = new List<object> { };
                foreach (var izv in izvedbePredmetov)
                {
                    if (izv.izv.IzpitniRok == null) continue;
                    foreach (var rok in izv.izv.IzpitniRok)
                    {
                        if (rok.Veljaven > 0)
                        {
                            var item = new
                            {
                                imePredmeta = izv.izv.PredmetStudijskegaPrograma.Predmet.imePredmeta != null ? izv.izv.PredmetStudijskegaPrograma.Predmet.imePredmeta : " ime predmeta",
                                izvajalecPredmeta = izv.izv.Profesor.imeProfesorja != null ? izv.izv.Profesor.imeProfesorja : " ime profesorja ",
                                izvajalecPriimek = izv.izv.Profesor.priimekProfesorja != null ? izv.izv.Profesor.priimekProfesorja : " priimek profesorja ",
                                vrstaRoka = rok.VrstaIzpitnegaRoka != null ? rok.VrstaIzpitnegaRoka : " vrsta roka",
                                Datum = rok.DatumIzpitnegaRoka != null ? rok.DatumIzpitnegaRoka : DateTime.Now,
                                prostor = rok.PrijavaNaIzpit != null ? rok.Prostor : "ni določen",
                                idRoka = rok.IDIzpitniRok != null ? rok.IDIzpitniRok : 0,
                                idVpisaP = izv.idVpPredmet != null ? izv.idVpPredmet : 0,
                            };
                            lista.Add(item);
                        }
                    }
                }
                /*    foreach (var p in vpisaniPredmeti) { 
                        var rok = (from r in p.IzvedbaPredmeta)
                    }*/
                /*  var izvedbe = (from p in vpi
                                    join r in db.IzpitniRok on p equals r.
                                    where p.Vpis_idVpis == vpis.idVpis
                                    select new 
                                    {
                                        imePredmeta = p.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta != null ? p.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta : " ime predmeta",
                                        izvajalecPredmeta = p.IzvedbaPredmeta.Profesor.imeProfesorja != null ? p.IzvedbaPredmeta.Profesor.imeProfesorja : " ime profesorja ",
                                        izvajalecPriimek = p.IzvedbaPredmeta.Profesor.priimekProfesorja != null ? p.IzvedbaPredmeta.Profesor.priimekProfesorja : " priimek profesorja ",
                                        ocenaPredmeta = p.ocena != null ? p.ocena : 99,
                                    }
                                    ).ToList();*/
                /*     var query = from person in people
                                    join pet in pets on person equals pet.Owner
                                    select new { OwnerName = person.FirstName, PetName = pet.Name };*/
                //  lOutput.Text = "Dobili premdet : " + predmeti.f;
                GVRoki.DataSource = lista;
                GVStariRoki.DataSource = listaStarih;
                GVRoki.DataBind();
                GVStariRoki.DataBind();
                if (lista.Count > 0) GVRoki.SelectedIndex = 0;
                //  GridView2.DataSource = nepotrjeniStudenti;
                GVRoki_SelectedIndexChanged(this, EventArgs.Empty);
            }
            else {
                DivPrijava.Visible = false;
            }
        }
        protected void BPrijava_Click(object sender, EventArgs e)
        {
            bool prijavaObstaja = false; 
            t8_2015Entities db = new t8_2015Entities();
            int idRoka = (int) GVRoki.DataKeys[GVRoki.SelectedIndex].Values["idRoka"];
            int idVpisaP = (int) GVRoki.DataKeys[GVRoki.SelectedIndex].Values["idVpisaP"];
            var prijava = db.PrijavaNaIzpit.Where(i => i.VpisaniPredmet_IdVpisanipredmet == idVpisaP && i.IzpitniRok_IdIzpitniRok == idRoka).ToList().FirstOrDefault();
         //   Label1.Text = "id Roka "+ idRoka.ToString() + "; ID vpisa: " + idVpisaP.ToString();
            if (prijava == null)
            {
                VpisaniPredmet currVP = db.VpisaniPredmet.Find(idVpisaP);
                PrijavaNaIzpit novaPrijava = new PrijavaNaIzpit();
                novaPrijava.IzpitniRok_IdIzpitniRok = idRoka;
                novaPrijava.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit = 1;
                currVP.PrijavaNaIzpit.Add(novaPrijava);
            }
            else prijava.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit = 1;
            db.SaveChanges();
            //GVRoki.SelectedRow.BackColor = System.Drawing.Color.Aqua;
            GVRoki_SelectedIndexChanged(this, EventArgs.Empty);
        }
        private int lanskePrijave(int idPSP) {
            bool jePonavljalec = false;
            t8_2015Entities db = new t8_2015Entities();
            int steviloOpravljanj =0;
            // int id =(int) Session["uporabnikID"];
            int id = 5;
            Console.Write("uporabnikov ID");
            var vsiVpisi = db.Vpis.Where(i => i.Student_idStudent1 == id).OrderByDescending(i => i.Letnik_idLetnik).ToList();
            var vpis = vsiVpisi.FirstOrDefault();
            foreach (var iterVpis in vsiVpisi)
            {
                if (iterVpis.Letnik_idLetnik == vpis.Letnik_idLetnik) jePonavljalec = true;
            }
            if (jePonavljalec) LabelPrijava.Text = LabelPrijava.Text + "<strong> (Ponavljalec) </strong>.";
          //  List<VpisaniPredmet> lista = new List<VpisaniPredmet>();
            foreach(var currVp in vsiVpisi){
                if (currVp.VpisaniPredmet != null) { 
                 //   lista.AddRange(currVp.VpisaniPredmet);
                      foreach (var predmet in currVp.VpisaniPredmet) {
                          if (predmet.IzvedbaPredmeta.PredmetStudijskegaPrograma.IdPredmetStudijskegaPrograma == idPSP)
                          {
                              foreach(var prijava in predmet.PrijavaNaIzpit)
                            {
                                if (prijava.IzpitniRok.DatumIzpitnegaRoka <= DateTime.Now) {
                                    if(!jePonavljalec) steviloOpravljanj++;
                               //     Label2.Text = "Ocena:  " + prijava.OcenaIzpita + " ID :" + prijava.idPrijavaNaizpit;
                                } 
                            }
                          }
                          
                      }
                }
            }
            return steviloOpravljanj;
        }
        protected void GVRoki_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool buttonEnabled = false;
            if (HttpContext.Current.User.IsInRole("2"))
            {
                buttonEnabled = true;
            }
            bool jePavzer = (bool)Session["jePavzer"];
            BPrijava.Enabled = true;
            BOdjava.Enabled = false;
            LSpremembe.Text = "";
            BPrijava.CssClass = "btn btn-success";
            BOdjava.CssClass = "btn btn-danger";
            LabelPrijava.Text = " ";
            t8_2015Entities db = new t8_2015Entities();
            if (GVRoki.SelectedIndex > 0)
            {
                int idRoka = (int)GVRoki.DataKeys[GVRoki.SelectedIndex].Values["idRoka"];
                int idVpisaP = (int)GVRoki.DataKeys[GVRoki.SelectedIndex].Values["idVpisaP"];
                VpisaniPredmet currVP = db.VpisaniPredmet.Find(idVpisaP);
                int stOp = -1;
                stOp = lanskePrijave(currVP.IzvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId);
                var obstojecePrijave = db.PrijavaNaIzpit.Where(i => i.VpisaniPredmet_IdVpisanipredmet == idVpisaP).ToList();
                //   var starePrijave = db.PrijavaNaIzpit.Where(i => i.VpisaniPredmet_IdVpisanipredmet == idVpisaP && i.IzpitniRok.DatumIzpitnegaRoka  < DateTime.Now);
                var bodocePrijave = db.PrijavaNaIzpit.Where(i => i.VpisaniPredmet_IdVpisanipredmet == idVpisaP && i.IzpitniRok.DatumIzpitnegaRoka >= DateTime.Now).ToList();
                int iPredmeta = -1;
                var currRok = db.IzpitniRok.Find(idRoka);
                var sortirane = obstojecePrijave.OrderByDescending(i => i.IzpitniRok.DatumIzpitnegaRoka).ToList();
                PrijavaNaIzpit zadnjaPrijava = new PrijavaNaIzpit();
                zadnjaPrijava.OcenaIzpita = 0;
                int steviloPrijavLetos = obstojecePrijave.Count;
                if (sortirane.Count > 0) zadnjaPrijava = sortirane[0];
                if (obstojecePrijave.Count() > 0)
                {
                    iPredmeta = (int)obstojecePrijave.FirstOrDefault().VpisaniPredmet_IdVpisanipredmet;
                }
                if ((stOp < 3))
                {
                    BPrijava.Enabled = true;
                    BOdjava.Enabled = false;
                    BPrijava.CssClass = "btn btn-success";
                    BOdjava.CssClass = "btn btn-danger";
                    LabelPrijava.Text = "To bo vaše " + (stOp + 1).ToString() + ". opravljanje izpita";
                }
                //   int stOpravljanj = starePrijave.Count();
                else if (stOp >= 3 && stOp < 6)
                {
                    BPrijava.Enabled = true;
                    BOdjava.Enabled = false;
                    BPrijava.CssClass = "btn btn-success";
                    BOdjava.CssClass = "btn btn-danger";
                    LabelPrijava.Text = "To bo vaše " + (stOp + 1).ToString() + ".opravljanje izpita. <strong> Pozor! </strong> Ker ste predmet opravljali več kot trikrat morate za prijavo plačati 180€.";
                }
                else if (stOp >= 6)
                {
                    BPrijava.Enabled = buttonEnabled;
                    BOdjava.Enabled = false;
                    BPrijava.CssClass = "btn btn-danger";
                    if (buttonEnabled) BPrijava.CssClass = "btn btn-success";

                    BOdjava.CssClass = "btn btn-danger";
                    LabelPrijava.Text = "Predmeta ne morete več opravljati, presegli ste limit (6)";
                }
                bool prijavljen = false;
                int ocenaJe = 0;
                foreach (var prijava in obstojecePrijave)
                {
                    if (prijava != null)
                    {
                        if (prijava.OcenaIzpita != null) ocenaJe = (int)prijava.OcenaIzpita;
                        int comp = (int)prijava.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit;
                        DateTime rokOdjave = (System.DateTime)prijava.IzpitniRok.DatumIzpitnegaRoka;
                        rokOdjave = rokOdjave.AddDays(-1);


                        if (prijava.VpisaniPredmet_IdVpisanipredmet == iPredmeta && prijava.IzpitniRok_IdIzpitniRok != idRoka) // je prijava za predmet a ne za rok
                        {
                            if (prijava.IzpitniRok.DatumIzpitnegaRoka > DateTime.Now)
                            {
                                BPrijava.Enabled = buttonEnabled;
                                BOdjava.Enabled = false;
                                BPrijava.CssClass = "btn btn-danger";
                                if (buttonEnabled) BPrijava.CssClass = "btn btn-success";
                                BOdjava.CssClass = "btn btn-danger";
                                LabelPrijava.Text = "Prijava pri predmetu že obstaja";
                            }
                            else if (prijava.IzpitniRok.DatumIzpitnegaRoka <= DateTime.Now)
                            {
                                BPrijava.Enabled = buttonEnabled;
                                BOdjava.Enabled = false;
                                BPrijava.CssClass = "btn btn-danger";
                                if (buttonEnabled) BPrijava.CssClass = "btn btn-success";
                                BOdjava.CssClass = "btn btn-danger";
                                LabelPrijava.Text = "Zadnja prijava še nima zaključene ocene";
                            }
                            currRok = db.IzpitniRok.Find(idRoka);
                            DateTime rokRokov = (System.DateTime)prijava.IzpitniRok.DatumIzpitnegaRoka;
                          //  rokRokov = rokRokov.AddDays(7);
                            if (((DateTime)currRok.DatumIzpitnegaRoka - rokRokov).TotalDays < 14 )
                            {
                                BPrijava.Enabled = buttonEnabled;
                                BOdjava.Enabled = false;
                                BPrijava.CssClass = "btn btn-danger";
                                if (buttonEnabled) BPrijava.CssClass = "btn btn-success";
                                BOdjava.CssClass = "btn btn-danger";
                                LabelPrijava.Text = "Od zadnjega opravljanja je minilo premalo časa";
                            }
                        }
                        else if (prijava.VpisaniPredmet_IdVpisanipredmet == iPredmeta && prijava.IzpitniRok_IdIzpitniRok == idRoka && (comp == 1 || comp == 2 || comp == 3)) // je prijava za predmet in rok
                        {
                            prijavljen = true;
                            BPrijava.Enabled = false;
                            BPrijava.CssClass = "btn btn-danger";
                            LabelPrijava.Text = "Na ta rok ste prijavljeni.";
                            if (DateTime.Now < rokOdjave)
                            {
                                BOdjava.Enabled = true;
                                BOdjava.CssClass = "btn btn-success";
                                LabelPrijava.Text = LabelPrijava.Text + " Lahko se odjavite.";

                            }
                            else
                            {
                                BPrijava.Enabled = false;
                                BOdjava.Enabled = buttonEnabled;
                                BPrijava.CssClass = "btn btn-danger";
                                BOdjava.CssClass = "btn btn-danger";
                                if (buttonEnabled) BOdjava.CssClass = "btn btn-success";
                                LabelPrijava.Text = LabelPrijava.Text + "Rok za odjavo je potekel.";
                            }

                        }
                        else prijavljen = true;
                    }
                }
                if (steviloPrijavLetos >= 3 && !prijavljen)
                {
                    BPrijava.Enabled = buttonEnabled;
                    BOdjava.Enabled = false;
                    BPrijava.CssClass = "btn btn-danger";
                    if (buttonEnabled) BPrijava.CssClass = "btn btn-success";
                    BOdjava.CssClass = "btn btn-danger";
                    LabelPrijava.Text = "Letos ste predmet opravljali že trikrat!, Prijava ni možna";
                }
                if (ocenaJe > 5)
                {
                    BPrijava.Enabled = buttonEnabled;
                    BOdjava.Enabled = false;
                    BPrijava.CssClass = "btn btn-danger";
                    if (buttonEnabled) BPrijava.CssClass = "btn btn-success";
                    BOdjava.CssClass = "btn btn-danger";
                    LabelPrijava.Text = "Predmet ste že opravili!";
                }
                if (currRok != null)
                {
                    DateTime rokPrijave = (System.DateTime)currRok.DatumIzpitnegaRoka;
                    rokPrijave = rokPrijave.AddDays(-2);
                    if (DateTime.Now > rokPrijave)
                    {
                        BPrijava.Enabled = buttonEnabled;
                        BOdjava.Enabled = false;
                        BPrijava.CssClass = "btn btn-danger";
                        if (buttonEnabled) BPrijava.CssClass = "btn btn-success";
                        BOdjava.CssClass = "btn btn-danger";
                        LabelPrijava.Text = "Rok za Prijavo je potekel";
                    }
                }
                if (stOp >= 6 || steviloPrijavLetos >= 6)
                {
                    BPrijava.Enabled = buttonEnabled;
                    BOdjava.Enabled = false;
                    BPrijava.CssClass = "btn btn-danger";
                    if (buttonEnabled) BPrijava.CssClass = "btn btn-success";

                    BOdjava.CssClass = "btn btn-danger";
                    LabelPrijava.Text = "Predmeta ne morete več opravljati, presegli ste limit (6)";
                }
                if (jePavzer) LabelPrijava.Text = LabelPrijava.Text + "<strong> Pozor! </strong> Ker ste pavzer morate za izpit plačati 180€.";
                //    LSteviloPrijav.Text = "Stevilo opravljanj:" + stOp;
            }
        }

        protected void BOdjava_Click(object sender, EventArgs e)
        {

            t8_2015Entities db = new t8_2015Entities();
            int idRoka = (int)GVRoki.DataKeys[GVRoki.SelectedIndex].Values["idRoka"];
            int idVpisaP = (int)GVRoki.DataKeys[GVRoki.SelectedIndex].Values["idVpisaP"];
            //   Label1.Text = "id Roka "+ idRoka.ToString() + "; ID vpisa: " + idVpisaP.ToString();4
             VpisaniPredmet currVP = db.VpisaniPredmet.Find(idVpisaP);
            
            var obstojecePrijave = db.PrijavaNaIzpit.Where(i => i.VpisaniPredmet_IdVpisanipredmet == idVpisaP && i.IzpitniRok_IdIzpitniRok == idRoka).ToList().FirstOrDefault();
            obstojecePrijave.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit = 7;
            obstojecePrijave.DatumSpremembe = DateTime.Now;
            //if(obstojecePrijave != null) currVP.PrijavaNaIzpit.Remove(obstojecePrijave);
            db.SaveChanges();
            GVRoki_SelectedIndexChanged(this, EventArgs.Empty);
           // GVRoki.SelectedRow.BackColor = System.Drawing.Color.White;
        }

        protected void GVIzbiraStudenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTables((int)(GVIzbiraStudenta.SelectedValue));
        }
    }
}