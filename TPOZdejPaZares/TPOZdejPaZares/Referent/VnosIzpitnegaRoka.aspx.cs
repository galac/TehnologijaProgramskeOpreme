using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares.Referent
{
    public partial class VnosIzpitnegaRoka : System.Web.UI.Page
    {
        private bool setDDL_izpitniRok = false;

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

                GV_subjects.SelectedIndex = -1;
                header31.Visible = false;
                GV_subjects.Visible = false;
                header41.Visible = false;
                L_Error.Visible = false;
                GV_izpitniRoki.Visible = true;

                Session["rowEditing"] = false;
                DDL_Years_SelectedIndexChanged(sender, e);
            }
        }

        protected void DDL_Years_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_subjects.SelectedIndex = -1;
            header31.Visible = false;
            GV_subjects.Visible = false;
            header41.Visible = false;
            L_Error.Visible = false;
            GV_izpitniRoki.Visible = false;

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

            if (relevantniPredmeti.Count < 1) {
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
                                       Izvajalci = String.Format("{0}.{1}, {2}{3}, {4}{5}", x.Prof1I.Length > 0 ? x.Prof1I[0]+". " : "", x.Prof1P, x.Prof2I.Length > 0 ? x.Prof2I[0]+". " : "", x.Prof2P, x.Prof3I.Length > 0 ? x.Prof3I[0]+". " : "", x.Prof3P),
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
            //Izpis.Text += "GV_subject_SelectedIndexChanged";

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
                    SteviloMest = i.SteviloMest != 0 ? i.SteviloMest+"" : "/",
                    i.VrstaIzpitnegaRoka,
                    ZaporednaStevilka = (i.VrstaIzpitnegaRoka == "redni" ? stevecRedni++ : stevecIzredni++),
                    SteviloPrijav = i.PrijavaNaIzpit.Where(x => x.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit < 7).Count()
                };
                izpitniRoki.Add(currentObj);
            }

            GV_izpitniRoki.Visible = true;
            if (izpitniRoki.Count < 1)
            {
                var niRokov = new { DatumIzpitnegaRoka = "", IDIzpitniRok = "", IzvedbaPredmeta = "", Prostor = "", SteviloMest = "", VrstaIzpitnegaRoka = "", ZaporednaStevilka = "", IzvedbaPredmeta_idIzvedbaPredmeta = "", SteviloPrijav =""};
                List<object> niRokovlist =  new List<object>();
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

            if (!setDDL_izpitniRok)
            {
                this.setDDL_izpitniRok = true;
                DropDownList DDL_vrstaRoka = GV_izpitniRoki.FooterRow.FindControl("DDL_vrstaRoka") as DropDownList;
                DDL_vrstaRoka.Items.Clear();

                DDL_vrstaRoka.Items.Add(new System.Web.UI.WebControls.ListItem("izredni", "izredni"));
                DDL_vrstaRoka.Items.Add(new System.Web.UI.WebControls.ListItem("redni", "redni"));
            }
            
            if(Session["rowEditing"] != null) {
                if (Session["rowEditing"].Equals("true"))
                {
                    GV_izpitniRoki_SetFooterEnability(false);
                    Session["rowEditing"] = false;                
                }
                else
                {
                    GV_izpitniRoki_SetFooterEnability(true);
                }
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setDatepickers();</script>", false);
        }

        protected void GV_izpitniRoki_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox TB_DatumIzpitnegaRoka = GV_izpitniRoki.FooterRow.FindControl("TB_DatumIzpitnegaRoka") as TextBox;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setDatepickers();</script>", false);
            Label L_ConfirmErr = GV_izpitniRoki.FooterRow.FindControl("L_ConfirmErr") as Label;
            L_ConfirmErr.Visible = false;
                        
            TextBox TB_Ura = GV_izpitniRoki.FooterRow.FindControl("TB_Ura") as TextBox;
            TextBox TB_Prostor = GV_izpitniRoki.FooterRow.FindControl("TB_Prostor") as TextBox;
            TextBox TB_SteviloMest = GV_izpitniRoki.FooterRow.FindControl("TB_SteviloMest") as TextBox;
            DropDownList DDL_vrstaRoka = GV_izpitniRoki.FooterRow.FindControl("DDL_vrstaRoka") as DropDownList;
            

            if (GV_izpitniRoki.SelectedIndex == -1)
            {
                //Izpis.Text += DDL_vrstaRoka.SelectedItem.Text + " " + TB_DatumIzpitnegaRoka.Text + " " + TB_Ura.Text + " " + TB_Prostor.Text + " " + TB_SteviloMest.Text;

                int izvedbaPredmeta = Convert.ToInt32(GV_subjects.DataKeys[GV_subjects.SelectedIndex].Values[0]);
                string vrstaRoka = DDL_vrstaRoka.SelectedItem.Text;
                DateTime datumIzpitnegaRoka = DateTime.ParseExact(TB_DatumIzpitnegaRoka.Text + TB_Ura.Text, "dd.MM.yyyyhh:mm tt", CultureInfo.InvariantCulture);
                string prostor = TB_Prostor.Text;
                int steviloMest = 0;
                if (TB_SteviloMest.Text != "")
                    steviloMest = Convert.ToInt32(TB_SteviloMest.Text);

                //Izpis.Text += datumIzpitnegaRoka + "";
                t8_2015Entities db = new t8_2015Entities();
                
                if (DateTime.Today > datumIzpitnegaRoka)
                {
                    L_ConfirmErr.Text = "Vnesite ustrezni datum.";
                    L_ConfirmErr.Visible = true;
                    return;
                }
                
                var profIzvedbe = (from i in db.IzvedbaPredmeta
                                    where i.idIzvedbaPredmeta == izvedbaPredmeta
                                    select new
                                    {
                                        Izvedbe = i.Profesor.IzvedbaPredmeta
                                    }).Single();

                var prof1Izvedbe = (from i in db.IzvedbaPredmeta
                                   where i.idIzvedbaPredmeta == izvedbaPredmeta
                                   select new
                                   {
                                       Izvedbe = i.Profesor1.IzvedbaPredmeta1
                                   }).Single();

                var prof2Izvedbe = (from i in db.IzvedbaPredmeta
                                   where i.idIzvedbaPredmeta == izvedbaPredmeta
                                   select new
                                   {
                                       Izvedbe = i.Profesor2.IzvedbaPredmeta2
                                   }).Single();

                bool profProst = jeIzvajalecProst(profIzvedbe.Izvedbe, datumIzpitnegaRoka, -1);
                bool prof1Prost = jeIzvajalecProst(prof1Izvedbe.Izvedbe, datumIzpitnegaRoka, -1);
                bool prof2Prost = jeIzvajalecProst(prof2Izvedbe.Izvedbe, datumIzpitnegaRoka, -1);
                //Izpis.Text += profProst + " " + prof1Prost + " " + prof2Prost;

                if (!profProst && !prof1Prost && !prof2Prost) {
                    L_ConfirmErr.Text = "Vsi izvajalci zasedeni.";
                    L_ConfirmErr.Visible = true;
                    return;
                }                
                
                IzpitniRok izpitniRok = new IzpitniRok
                {
                    DatumIzpitnegaRoka = datumIzpitnegaRoka,
                    VrstaIzpitnegaRoka = vrstaRoka,
                    Prostor = prostor,
                    SteviloMest = steviloMest,
                    IzvedbaPredmeta = (from i in db.IzvedbaPredmeta
                                       where i.idIzvedbaPredmeta == izvedbaPredmeta
                                       select i).Single(),
                    Veljaven = 1
                };

                db.IzpitniRok.Add(izpitniRok);

                try
                {
                    db.SaveChanges();
                    GV_subjects_SelectedIndexChanged(sender, e);
                }
                catch (Exception ex)
                {
                    L_ConfirmErr.Text = "Napaka pri shranjevanju.";
                    L_ConfirmErr.Visible = true;
                }   
            }
        }

        protected void GV_izpitniRoki_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setDatepickers();</script>", false);
            //Izpis.Text += "edit "+GV_izpitniRoki.EditIndex;            
            
            GridViewRow editedRow =  GV_izpitniRoki.Rows[e.NewEditIndex];
            string L_VrstaIzpitnegaRoka = (editedRow.FindControl("L_VrstaIzpitnegaRoka") as Label).Text;
            string L_DatumIzpitnegaRoka = (editedRow.FindControl("L_DatumIzpitnegaRoka") as Label).Text.Replace(" ","");
            DateTime L_UraD = DateTime.ParseExact((editedRow.FindControl("L_Ura") as Label).Text, "hh:mm", CultureInfo.InvariantCulture);
            string L_Ura = String.Format("{0:hh:mm tt}", L_UraD);
            string L_Prostor = (editedRow.FindControl("L_Prostor") as Label).Text;
            string L_SteviloMest = (editedRow.FindControl("L_SteviloMest") as Label).Text;
            
            if (DateTime.ParseExact(L_DatumIzpitnegaRoka, "dd.MM.yyyy", CultureInfo.InvariantCulture).Date < DateTime.Today.Date)
            {
                e.Cancel = true;
                GV_subjects_SelectedIndexChanged(sender, e);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp2", "<script type='text/javascript'>alert('Izpitni rok je bil že izveden ("+L_DatumIzpitnegaRoka+"). Urejanje ni mogoče.');</script>", false);
                return;
            }
                        
            Session["rowEditing"] = "true";
            GV_izpitniRoki.EditIndex = e.NewEditIndex;            
            GV_subjects_SelectedIndexChanged(sender, e);            

            DropDownList DDL_vrstaRoka1 = GV_izpitniRoki.Rows[e.NewEditIndex].FindControl("DDL_vrstaRoka1") as DropDownList;
            DDL_vrstaRoka1.Items.Clear();
            DDL_vrstaRoka1.Items.Add(new System.Web.UI.WebControls.ListItem("izredni", "izredni"));
            DDL_vrstaRoka1.Items.Add(new System.Web.UI.WebControls.ListItem("redni", "redni"));
            DDL_vrstaRoka1.SelectedValue = L_VrstaIzpitnegaRoka;

            TextBox TB_DatumIzpitnegaRoka = GV_izpitniRoki.Rows[e.NewEditIndex].FindControl("TB_DatumIzpitnegaRokaE") as TextBox;
            (GV_izpitniRoki.Rows[e.NewEditIndex].FindControl("TB_UraE") as TextBox).Text = L_Ura;
            (GV_izpitniRoki.Rows[e.NewEditIndex].FindControl("TB_SteviloMestE") as TextBox).Text = L_SteviloMest != "/" ? L_SteviloMest : "";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp4", "<script type='text/javascript'>$('#"+TB_DatumIzpitnegaRoka.ClientID+"').val('"+L_DatumIzpitnegaRoka+"');</script>", false);
        }


        protected void GV_izpitniRoki_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setDatepickers();</script>", false);            
            t8_2015Entities db = new t8_2015Entities();
            
            int idIzpitniRok = Convert.ToInt32(GV_izpitniRoki.DataKeys[e.RowIndex].Values[0]);

            if (IzpitniRokHasOcene(db.IzpitniRok.Single(x => x.IDIzpitniRok == idIzpitniRok))) {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp3", "<script type='text/javascript'>alert('Izpitni rok ima vpisane ocene. Ukinitev ni mogoča.');</script>", false);
                return;
            }

            string L_DatumIzpitnegaRoka = (GV_izpitniRoki.Rows[e.RowIndex].FindControl("L_DatumIzpitnegaRoka") as Label).Text.Replace(" ", "");
            if (DateTime.ParseExact(L_DatumIzpitnegaRoka, "dd.MM.yyyy", CultureInfo.InvariantCulture).Date < DateTime.Today.Date)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp3", "<script type='text/javascript'>alert('Izpitni rok je bil že izveden (" + L_DatumIzpitnegaRoka + "). Ukinitev ni mogoča.');</script>", false);
                return;
            }

            Session["deletedRowID"] = e.RowIndex;          

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "confirmDelete",
            "if(confirm('Ste prepričani, da želite ukiniti izpitni rok: " + (GV_izpitniRoki.Rows[e.RowIndex].FindControl("L_VrstaIzpitnegaRoka") as Label).Text.ToUpper() + " " + (GV_izpitniRoki.Rows[e.RowIndex].FindControl("L_DatumIzpitnegaRoka") as Label).Text.ToUpper() + ",  s številom prijav: " + (GV_izpitniRoki.Rows[e.RowIndex].FindControl("L_SteviloPrijav") as Label).Text + "?')){" 
            +"document.getElementById('"+B_potrditevIzbrisa.ClientID+"').click(); "
            +"} ;",
            true);

            GV_subjects_SelectedIndexChanged(sender, e); 
        }

        protected bool jeIzvajalecProst(ICollection<IzvedbaPredmeta> izvedbe, DateTime datumIzpitnegaRoka, int idIzpitniRok) {
            if (izvedbe.Count == 0)
                return false;

            foreach (var izvedba in izvedbe)
            {
                foreach (var rok in izvedba.IzpitniRok)
                {
                    if (!rok.DatumIzpitnegaRoka.HasValue || rok.IDIzpitniRok == idIzpitniRok)
                        continue;

                    if (rok.DatumIzpitnegaRoka.Value.Date == datumIzpitnegaRoka.Date)
                        return false;
                }
            }

            return true;
        }

        protected void B_potrditevIzbrisa_Click(object sender, EventArgs e)
        {          
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setDatepickers();</script>", false);
            Label L_ConfirmErr = GV_izpitniRoki.FooterRow.FindControl("L_ConfirmErr") as Label;
            L_ConfirmErr.Visible = false;

            t8_2015Entities db = new t8_2015Entities();
            int IDIzpitniRok = Convert.ToInt32(GV_izpitniRoki.DataKeys[Convert.ToInt32(Session["deletedRowID"])].Values[0]);

            IzpitniRok izpitniRok = (from i in db.IzpitniRok
                                     where i.IDIzpitniRok == IDIzpitniRok
                                     select i).Single();

            if (izpitniRok.PrijavaNaIzpit.Count > 0)
            {
                string mailingLista = "";
                foreach (var prijava in izpitniRok.PrijavaNaIzpit)
                {
                    mailingLista += ", "+prijava.VpisaniPredmet.Vpis.Student.mailStudenta;
                }
                using (StreamReader reader = File.OpenText(Server.MapPath("/Referent/email_UkinitevRoka.htm"))) // Path to your 
                {
                    MailMessage msg = new MailMessage();
                    msg.Bcc.Add(mailingLista);
                    msg.From = new MailAddress("referat@spletni-student.si", "Spletni študent", System.Text.Encoding.UTF8);
                    msg.Subject = "Obvestilo o ukinitvi izpitnega roka pri predmetu " + izpitniRok.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta + " (" + izpitniRok.DatumIzpitnegaRoka.Value.Date.ToLongDateString() + ")";
                    msg.SubjectEncoding = System.Text.Encoding.UTF8;
                    msg.Body = zgradiEmail(
                        reader.ReadToEnd(), 
                        izpitniRok.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta.ToUpper(),
                        izpitniRok.VrstaIzpitnegaRoka.ToUpper()+"  "+izpitniRok.DatumIzpitnegaRoka.Value.Date.ToLongDateString(),
                        "referata"
                    ); 
                    msg.BodyEncoding = System.Text.Encoding.UTF8;
                    msg.IsBodyHtml = true;
                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("tpoprojekt.test@gmail.com", "tpotpo1!");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;

                    //Izpis.Text += "poslan mail";
                    //Izpis.Text += mailingLista;

                    try
                    {
                        client.Send(msg);
                    }
                    catch (Exception ex)
                    {
                        Exception ex2 = ex;
                        string errorMessage = string.Empty;
                        while (ex2 != null)
                        {
                            errorMessage += ex2.ToString();
                            ex2 = ex2.InnerException;
                        }
                    }
                }
            }

            foreach (var ir in izpitniRok.PrijavaNaIzpit) {
                PrijavaNaIzpit prijavaNaIzpit = ir as PrijavaNaIzpit;
                ir.DatumSpremembe = DateTime.Now;
                ir.StatusPrijave = db.StatusPrijave.Where(p => p.idStatusPrijave == 8).Single();
                db.SaveChanges();
            }

            izpitniRok.Veljaven = 0;

            try
            {
                db.SaveChanges();
                GV_subjects_SelectedIndexChanged(sender, e);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdasd", "<script type='text/javascript'>alert('Izpitni rok je bil uspešno ukinjen. Prijavljeni študenti so bili obveščeni.');</script>", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aaa", "<script type='text/javascript'>alert('Napaka pri brisanju roka.')</script>", false);
                L_ConfirmErr.Text = "Napaka pri brisanju.";
                L_ConfirmErr.Visible = true;
            }
        }

        protected string zgradiEmail(string email, string predmet, string rok, string rola)
        {
            email = email.Replace("DATUMDATUM", DateTime.Now.Date.ToLongDateString());
            email = email.Replace("PREDMETPREDMET", predmet);
            email = email.Replace("ROKROKROK", rok);
            email = email.Replace("REFERENTREFERENT", rola);

            return email;
        }

        protected string zgradiEmailSprememba(string email, string predmet, string rok, string rola, string vrstaRoka, string datumRoka, string uraRoka, string prostor, string stMest)
        {
            email = email.Replace("DATUMDATUM", DateTime.Now.Date.ToLongDateString());
            email = email.Replace("PREDMETPREDMET", predmet);
            email = email.Replace("ROKROKROK", rok);            
            email = email.Replace("REFERENTREFERENT", rola);
            email = email.Replace("VRSTAROKA", vrstaRoka);
            email = email.Replace("DATUMROKA", datumRoka);
            email = email.Replace("URAURAURA", uraRoka);
            email = email.Replace("PROSTORPROSTOR", prostor);
            email = email.Replace("STEVILOMEST", stMest);

            return email;
        }

        protected void LinkButton_Command(Object sender, CommandEventArgs e) {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setDatepickers();</script>", false); 

            GV_subjects_SelectedIndexChanged(sender, e);
        }

        protected void GV_izpitniRoki_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Izpis.Text += " rowUpdating";

            TextBox TB_DatumIzpitnegaRoka = GV_izpitniRoki.Rows[e.RowIndex].FindControl("TB_DatumIzpitnegaRokaE") as TextBox;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setDatepickers();</script>", false);
            Label L_ConfirmErr = GV_izpitniRoki.Rows[e.RowIndex].FindControl("L_ConfirmErrE") as Label;
            L_ConfirmErr.Visible = false;

            TextBox TB_Ura = GV_izpitniRoki.Rows[e.RowIndex].FindControl("TB_UraE") as TextBox;
            TextBox TB_Prostor = GV_izpitniRoki.Rows[e.RowIndex].FindControl("TB_ProstorE") as TextBox;
            TextBox TB_SteviloMest = GV_izpitniRoki.Rows[e.RowIndex].FindControl("TB_SteviloMestE") as TextBox;
            DropDownList DDL_vrstaRoka = GV_izpitniRoki.Rows[e.RowIndex].FindControl("DDL_vrstaRoka1") as DropDownList;

            int idIzpitniRok = Convert.ToInt32(GV_izpitniRoki.DataKeys[e.RowIndex].Values[0]);
            int izvedbaPredmeta = Convert.ToInt32(GV_subjects.DataKeys[GV_subjects.SelectedIndex].Values[0]);
            string vrstaRoka = DDL_vrstaRoka.SelectedItem.Text;
            DateTime datumIzpitnegaRoka = DateTime.ParseExact(TB_DatumIzpitnegaRoka.Text + TB_Ura.Text, "dd.MM.yyyyhh:mm tt", CultureInfo.InvariantCulture);
            string prostor = TB_Prostor.Text;
            int steviloMest = 0;
            if (TB_SteviloMest.Text != "")
                steviloMest = Convert.ToInt32(TB_SteviloMest.Text);

            t8_2015Entities db = new t8_2015Entities();
            
            if (DateTime.Today > datumIzpitnegaRoka)
            {
                L_ConfirmErr.Text = "Vnesite ustrezni datum.";
                L_ConfirmErr.Visible = true;
                return;
            }

            if (steviloMest < db.IzpitniRok.Single(x => x.IDIzpitniRok == idIzpitniRok).PrijavaNaIzpit.Count && steviloMest != 0)
            {
                L_ConfirmErr.Text = "Več prijav kot mest.";
                L_ConfirmErr.Visible = true;
                return;
            }

            var profIzvedbe = (from i in db.IzvedbaPredmeta
                                where i.idIzvedbaPredmeta == izvedbaPredmeta
                                select new
                                {
                                    Izvedbe = i.Profesor.IzvedbaPredmeta
                                }).Single();

            var prof1Izvedbe = (from i in db.IzvedbaPredmeta
                                where i.idIzvedbaPredmeta == izvedbaPredmeta
                                select new
                                {
                                    Izvedbe = i.Profesor1.IzvedbaPredmeta1
                                }).Single();

            var prof2Izvedbe = (from i in db.IzvedbaPredmeta
                                where i.idIzvedbaPredmeta == izvedbaPredmeta
                                select new
                                {
                                    Izvedbe = i.Profesor2.IzvedbaPredmeta2
                                }).Single();

            bool profProst = jeIzvajalecProst(profIzvedbe.Izvedbe, datumIzpitnegaRoka, idIzpitniRok);
            bool prof1Prost = jeIzvajalecProst(prof1Izvedbe.Izvedbe, datumIzpitnegaRoka, idIzpitniRok);
            bool prof2Prost = jeIzvajalecProst(prof2Izvedbe.Izvedbe, datumIzpitnegaRoka, idIzpitniRok);

            if (!profProst && !prof1Prost && !prof2Prost) {
                L_ConfirmErr.Text = "Vsi izvajalci zasedeni.";
                L_ConfirmErr.Visible = true;
                return;
            }

            Session["datumIzpitnegaRoka"] = TB_DatumIzpitnegaRoka.Text + TB_Ura.Text;
            Session["vrstaRoka"] = vrstaRoka;
            Session["prostor"] = prostor;
            Session["steviloMest"] = steviloMest;
            Session["idIzpitniRok"] = idIzpitniRok;

            IzpitniRok izpitniRok = db.IzpitniRok.Single(x => x.IDIzpitniRok == idIzpitniRok);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "confirmDelete",
            "if(confirm('Ste prepričani, da želite spremeniti izpitni rok "+izpitniRok.VrstaIzpitnegaRoka.ToUpper()+" "+izpitniRok.DatumIzpitnegaRoka.Value.ToLongDateString()+" na: " + vrstaRoka.ToUpper() + " " + datumIzpitnegaRoka.ToLongDateString() + ",  s številom prijav: " + izpitniRok.PrijavaNaIzpit.Count + "?')){"
            + "document.getElementById('" + B_potrditevSpremembe.ClientID + "').click(); "
            + "} ;",
            true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp4", "<script type='text/javascript'>$('#" + TB_DatumIzpitnegaRoka.ClientID + "').val('" + TB_DatumIzpitnegaRoka.Text + "');</script>", false);
        
            
        }

        protected void GV_izpitniRoki_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_subjects_SelectedIndexChanged(sender, e);
        }

        protected void GV_izpitniRoki_SetFooterEnability(bool isEnabled) {
            (GV_izpitniRoki.FooterRow.FindControl("TB_DatumIzpitnegaRoka") as TextBox).ReadOnly = !isEnabled;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>setDatepickers();</script>", false);
            (GV_izpitniRoki.FooterRow.FindControl("L_ConfirmErr") as Label).Visible = false;
            (GV_izpitniRoki.FooterRow.FindControl("TB_Ura") as TextBox).ReadOnly = !isEnabled;
            (GV_izpitniRoki.FooterRow.FindControl("TB_Prostor") as TextBox).ReadOnly = !isEnabled;
            (GV_izpitniRoki.FooterRow.FindControl("TB_SteviloMest") as TextBox).ReadOnly = !isEnabled;
            (GV_izpitniRoki.FooterRow.FindControl("DDL_vrstaRoka") as DropDownList).Enabled = isEnabled;
            (GV_izpitniRoki.FooterRow.FindControl("LB_dodaj") as LinkButton).CssClass = isEnabled ? "btn btn-success" : "btn btn-success disabled";
            (GV_izpitniRoki.FooterRow.FindControl("LB_preklici") as LinkButton).CssClass = isEnabled ? "btn btn-danger" : "btn btn-danger disabled";

            for(int i = 0; i < GV_izpitniRoki.Rows.Count; i++) {
                    if(i == GV_izpitniRoki.EditIndex)
                        continue;
                       
                (GV_izpitniRoki.Rows[i].FindControl("LB_uredi") as LinkButton).CssClass = isEnabled ? "btn btn-info" : "btn btn-info disabled";
                (GV_izpitniRoki.Rows[i].FindControl("LB_izbrisi") as LinkButton).CssClass = isEnabled ? "btn btn-danger" : "btn btn-danger disabled";                
            }
        }

        protected bool IzpitniRokHasOcene(IzpitniRok izpitniRok) {
            foreach (var prijava in izpitniRok.PrijavaNaIzpit) {
                if (prijava.OcenaIzpita != null)
                    return true;
            }

            return false;
        }

        protected void B_potrditevSpremembe_Click(object sender, EventArgs e) {
            //Izpis.Text += " B_potrditevSpremembe_Click" + Session["idIzpitniRok"];
            
            t8_2015Entities db = new t8_2015Entities();
            int idIzpitniRok = Convert.ToInt32(Session["idIzpitniRok"]);

            IzpitniRok izpitniRok = db.IzpitniRok.Single(x => x.IDIzpitniRok == idIzpitniRok);
            

            if (izpitniRok.PrijavaNaIzpit.Count > 0)
            {
                string mailingLista = "";
                foreach (var prijava in izpitniRok.PrijavaNaIzpit)
                {
                    mailingLista += ", " + prijava.VpisaniPredmet.Vpis.Student.mailStudenta;
                }
                using (StreamReader reader = File.OpenText(Server.MapPath("/Referent/email_SpremembaRoka.htm"))) // Path to your 
                {
                    MailMessage msg = new MailMessage();
                    msg.Bcc.Add(mailingLista);
                    msg.From = new MailAddress("referat@spletni-student.si", "Spletni študent", System.Text.Encoding.UTF8);
                    msg.Subject = "Obvestilo o spremembi izpitnega roka pri predmetu " + izpitniRok.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta + " (" + izpitniRok.DatumIzpitnegaRoka.Value.Date.ToLongDateString() + ")";
                    msg.SubjectEncoding = System.Text.Encoding.UTF8;
                    msg.Body = zgradiEmailSprememba(
                        reader.ReadToEnd(),
                        izpitniRok.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta.ToUpper(),
                        izpitniRok.VrstaIzpitnegaRoka.ToUpper() + "  " + izpitniRok.DatumIzpitnegaRoka.Value.Date.ToLongDateString(),
                        "referata",
                        Session["vrstaRoka"].ToString(),
                        DateTime.ParseExact(Session["datumIzpitnegaRoka"].ToString(), "dd.MM.yyyyhh:mm tt", CultureInfo.InvariantCulture).ToLongDateString(),
                        String.Format("{0:hh:mm}", DateTime.ParseExact(Session["datumIzpitnegaRoka"].ToString(), "dd.MM.yyyyhh:mm tt", CultureInfo.InvariantCulture)),
                        Session["prostor"].ToString(),
                        Session["steviloMest"] != null ? (Session["steviloMest"].ToString().Equals("0") ? "ni omejitev" : Session["steviloMest"].ToString()) : "ni omejitev"
                    );
                    msg.BodyEncoding = System.Text.Encoding.UTF8;
                    msg.IsBodyHtml = true;
                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("tpoprojekt.test@gmail.com", "tpotpo1!");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;

                    try
                    {
                        client.Send(msg);
                    }
                    catch (Exception ex)
                    {
                        Exception ex2 = ex;
                        string errorMessage = string.Empty;
                        while (ex2 != null)
                        {
                            errorMessage += ex2.ToString();
                            ex2 = ex2.InnerException;
                        }
                    }
                }
            }

            izpitniRok.DatumIzpitnegaRoka = DateTime.ParseExact(Session["datumIzpitnegaRoka"].ToString(), "dd.MM.yyyyhh:mm tt", CultureInfo.InvariantCulture);  
            izpitniRok.VrstaIzpitnegaRoka = Session["vrstaRoka"].ToString();
            izpitniRok.Prostor = Session["prostor"].ToString();
            izpitniRok.SteviloMest = Session["steviloMest"] != null ? Convert.ToInt32(Session["steviloMest"]) : 0;

            foreach (var ir in izpitniRok.PrijavaNaIzpit)
            {
                PrijavaNaIzpit prijavaNaIzpit = ir as PrijavaNaIzpit;
                ir.DatumSpremembe = DateTime.Now;
                ir.StatusPrijave = db.StatusPrijave.Where(p => p.idStatusPrijave == 5).Single();
                db.SaveChanges();
            }

            try
            {
                db.SaveChanges();
                GV_subjects_SelectedIndexChanged(sender, e);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdasd", "<script type='text/javascript'>alert('Izpitni rok je bil uspešno posodobljen. Prijavljeni študenti (" + izpitniRok.PrijavaNaIzpit.Count + ") so bili obveščeni o spremembi.');</script>", false);
           
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aaa", "<script type='text/javascript'>alert('Napaka pri posodobitvi roka.')</script>", false);
            }            
        }
    }
}