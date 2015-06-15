using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web.Security;

namespace TPOZdejPaZares
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ForgetLink.Visible = true;
            //LinkLogin.Visible = true;
            LinkPozabljenoG.Visible = true;
            bool vpisan = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (vpisan) {
                t8_2015Entities db = new t8_2015Entities();
               // ForgetLink.Visible = false;
                ButtonOdjava.Visible = true;
                LinkLogin.Visible = false;
                LinkPozabljenoG.Visible = false;
                LinkPonastaviG.Visible = true;
                string imeVloge = " ";
                var vloga = db.Vloge.ToList();
                if (Session["uporabnikID"] != null)
                {
                    int idUporabnik = (int)Session["uporabnikID"];
                    Student uporabnik = (Student)(from s in db.Student
                                                  where s.idStudent == idUporabnik
                                                  select s).FirstOrDefault();
                    labelIme.Text = uporabnik.imeStudenta + " " + uporabnik.priimekStudenta + ", ";
                    if (uporabnik.vpisnaStudenta != null)
                    {
                        labelVpisna.Text = uporabnik.vpisnaStudenta + ", ";
                    }
                }
                if (HttpContext.Current.User.IsInRole("1"))
                {
                    foreach( var vlo in vloga){
                        if (vlo.idVloge == 1) {
                            imeVloge = vlo.opisVloge;
                        }
                    }
                    LinkPI.Visible = true;
                    labelVloga.Text = imeVloge;
                    LinkVpisniListStudent.Visible = true;
                    LinkIzpisVpisniListStudent.Visible = true;
                    Link_Student_KartotecniListStudent.Visible = true;
                    
                }
                else if (HttpContext.Current.User.IsInRole("2"))
                {
                    foreach (var vlo in vloga)
                    {
                        if (vlo.idVloge == 2)
                        {
                            imeVloge = vlo.opisVloge;
                        }
                    }
                    LinkPP.Visible= true;
                    //LinkPP.Visible= true;
                    labelVloga.Text = imeVloge;
                    LinkIskanjeStudentov.Visible = true;
                    LinkUpSklepov.Visible = true;
                    LinkParsanje.Visible = true;
                    Link_Referent_IskanjeStudentov.Visible = true;
                    Link_Referent_Predmetnik.Visible = true;
                    Link_Referent_KartotecniListReferent.Visible = true;
                    LinkVpisniListReferent.Visible = true;
                    LinkPI.Visible = true;

                    LinkIzpisVpisniListReferent.Visible = true;
                    //Link_Referent_IzbiraPredmetovReferent.Visible = true;
                    Link_Referent_RezultatiPisnegaDelaIzpitaRef.Visible = true;
                    Link_Referent_SeznamPrijavljenihNaIzpitniRokRef.Visible = true;
                    Link_Referent_SeznamKoncnihOcenIzpitaRef.Visible = true;
                    Link_Referent_VnosRezultatovPisnegaDelaIzpitaInKoncnihOcen.Visible = true;
                    Link_Referent_Zetoni.Visible = true;
                    LinkPP.Visible = true;
                    //referent visible = true
                    Link_Referent_VnosIzpitnegaRoka.Visible = true;
                    Link_Ucitelj_SeznamVpisanih.Visible = true;
                }
                else if (HttpContext.Current.User.IsInRole("3"))
                {
                    foreach (var vlo in vloga)
                    {
                        if (vlo.idVloge == 3)
                        {
                            imeVloge = vlo.opisVloge;
                        }
                    }
                    labelVloga.Text = imeVloge;

                    //ucitelj visible.true
                    Link_Ucitelj_IskanjeStudentov.Visible = true;
                    Link_Ucitelj_SeznamVpisanih.Visible = true;
                    Link_Ucitelj_RezultatiPisnegaDelaIzpitaUcitelj.Visible = true;
                    Link_Ucitelj_SeznamPrijavljenihNaIzpitniRokUcitelj.Visible = true;
                    Link_Ucitelj_SeznamKoncnihOcenIzpitaUcitelj.Visible = true;
                    Link_Ucitelj_VnosRezultatovPisnegaDelaIzpitaInKoncnihOcen.Visible = true;
                }
            }
            if (!vpisan)
            {
                ButtonOdjava.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session["uporabnik"] = null;
            Response.Redirect("~/Default.aspx");
            LinkIskanjeStudentov.Visible = false;
            LinkUpSklepov.Visible = false;
            LinkParsanje.Visible = false;
            Link_Referent_Predmetnik.Visible = false;
            Link_Referent_Predmeti.Visible = false;
            LinkVpisniListReferent.Visible = false;
            LinkVpisniListStudent.Visible = false;
            LinkIzpisVpisniListReferent.Visible = false;
            LinkIzpisVpisniListStudent.Visible = false;
            Link_Student_KartotecniListStudent.Visible = false;

            ButtonOdjava.Visible = false;
            LinkLogin.Visible = true;
            LinkPozabljenoG.Visible = true;
            LinkPonastaviG.Visible = false;

            //referent visible.false
            Link_Referent_IskanjeStudentov.Visible = false;
            Link_Referent_VnosIzpitnegaRoka.Visible = false;
            Link_Ucitelj_SeznamVpisanih.Visible = false;
            //Link_Referent_IzbiraPredmetovReferent.Visible = false;
            Link_Referent_RezultatiPisnegaDelaIzpitaRef.Visible = false;
            Link_Referent_SeznamPrijavljenihNaIzpitniRokRef.Visible = false;
            Link_Referent_SeznamKoncnihOcenIzpitaRef.Visible = false;
            Link_Referent_KartotecniListReferent.Visible = false;
            Link_Referent_VnosRezultatovPisnegaDelaIzpitaInKoncnihOcen.Visible = false;
            Link_Referent_Zetoni.Visible = false;

            //ucitelj visible.false
            Link_Ucitelj_IskanjeStudentov.Visible = false;
            Link_Ucitelj_SeznamVpisanih.Visible = false;
            Link_Ucitelj_RezultatiPisnegaDelaIzpitaUcitelj.Visible = false;
            Link_Ucitelj_SeznamPrijavljenihNaIzpitniRokUcitelj.Visible = false;
            Link_Ucitelj_SeznamKoncnihOcenIzpitaUcitelj.Visible = false;
            Link_Ucitelj_VnosRezultatovPisnegaDelaIzpitaInKoncnihOcen.Visible = false;
        }
    }
}