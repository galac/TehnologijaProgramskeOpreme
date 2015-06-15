using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares.Referent
{
    public partial class KartotecniListReferent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();
            for (int i = PlaceHolder1.Controls.Count - 1; i >= 0; i--)
            {
                PlaceHolder1.Controls.RemoveAt(i);
            }
            //Session["uporabnikID"] = 5;

            int uporabnikID = (int)Session["studentekID"];
            Student uporabnik = (from s in db.Student
                                 where s.idStudent == uporabnikID
                                 select s).FirstOrDefault();

            if (uporabnik == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aaa", "<script type='text/javascript'>alert('Uporabnik ne obstaja.')\nwindow.location = 'UvodVKartotecniList.aspx'</script>", false);
                return;
            }

            if (uporabnik.Vpis.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aaa", "<script type='text/javascript'>alert('Uporabnik še ni bil nikoli vpisan.')\nwindow.location = 'UvodVKartotecniList.aspx'</script>", false);
                return;
            }

            string celaStran = "";
            try
            {
                using (StreamReader sr = new StreamReader(Server.MapPath("~/Studentek/KartotecniList.html")))
                {
                    celaStran = sr.ReadToEnd();
                    //celaStran = celaStran.Replace("CLIENTCLIENT", UpdatePanelPage.ClientID);
                }
            }
            catch (Exception ex)
            {

            }

            //začetek iteriranja preko vseh vpisov študenta
            List<Vpis> listaVpisov = uporabnik.Vpis.ToList();
            listaVpisov.Sort((x, y) => String.Compare(x.StudijskoLeto.Substring(0, 4), y.StudijskoLeto.Substring(0, 4)));
            HashSet<StudijskiProgram> hashStudijskihProgramov = new HashSet<StudijskiProgram>();
            foreach (Vpis vpis in listaVpisov)
            {
                hashStudijskihProgramov.Add(vpis.StudijskiProgram);
            }

            celaStran = celaStran.Replace("DATUM", DateTime.Now.Date.ToString("yyyy-MM-dd"));
            celaStran = celaStran.Replace("63120168, Novak Janez", uporabnik.vpisnaStudenta.ToString() + ", " + uporabnik.imeStudenta + " " + uporabnik.priimekStudenta);

            Label labelZacetek = new Label();
            labelZacetek.Text = celaStran;
            PlaceHolder1.Controls.Add(labelZacetek);

            foreach (StudijskiProgram izbranProgram in hashStudijskihProgramov)
            {
                string studijskiProgram = "";
                //to potrebno narediti ciklično
                try
                {
                    using (StreamReader sr = new StreamReader(Server.MapPath("~/Studentek/KartotecniListProgram.html")))
                    {
                        studijskiProgram = sr.ReadToEnd();
                        //celaStran = celaStran.Replace("CLIENTCLIENT", UpdatePanelPage.ClientID);
                    }
                }
                catch (Exception ex)
                {

                }

                studijskiProgram = studijskiProgram.Replace("STUDIJSKIPROGRAM", izbranProgram.naziv);

                Label labelProgram = new Label();
                labelProgram.Text = studijskiProgram;
                PlaceHolder1.Controls.Add(labelProgram);

                foreach (Vpis izbranVpis in listaVpisov)
                {
                    if (izbranProgram.idStudijskiProgram == izbranVpis.StudijskiProgram_idStudijskiProgram)
                    {

                        string preostanekStrani = "";
                        try
                        {
                            using (StreamReader sr = new StreamReader(Server.MapPath("~/Studentek/KartotecniListJedro.html")))
                            {
                                preostanekStrani = sr.ReadToEnd();
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        preostanekStrani = preostanekStrani.Replace("STUDIJSKOLETO", izbranVpis.StudijskoLeto);
                        preostanekStrani = preostanekStrani.Replace("LETNIK", izbranVpis.Letnik.letnik1.ToString());
                        preostanekStrani = preostanekStrani.Replace("VRSTAVPISA", izbranVpis.VrstaVpisa.opisVpisa);
                        preostanekStrani = preostanekStrani.Replace("NACIN", izbranVpis.NacinStudija.opisNacina);

                        //celaStran += studijskiProgram;
                        //celaStran += preostanekStrani;

                        Label LabelPage = new Label();
                        LabelPage.Text = preostanekStrani + "<br>";


                        DataTable dataTable = new DataTable();
                        dataTable.Columns.AddRange(new DataColumn[8] {
                            new DataColumn("Zap. št.", typeof(string)),
                            new DataColumn("Šifra", typeof(string)),
                            new DataColumn("Predmet", typeof(string)),
                            new DataColumn("KT/U", typeof(string)),
                            new DataColumn("Predavatelj(i)", typeof(string)),
                            new DataColumn("Datum", typeof(string)),
                            new DataColumn("Ocena", typeof(string)),
                            new DataColumn("Št. polaganj", typeof(string))
                        });
                        int stevec = 1;
                        int povprecjeOcen = 0;
                        int skupnoSteviloTock = 0;
                        int sestevekOcenIzpitov = 0;
                        int steviloOpravljenihIzpitov = 0;
                        foreach (VpisaniPredmet vpisaniPredmet in izbranVpis.VpisaniPredmet)
                        {
                            foreach (PrijavaNaIzpit prijavaNaIzpit in vpisaniPredmet.PrijavaNaIzpit)
                            {
                                int ocena = Convert.ToInt32(prijavaNaIzpit.OcenaIzpita);
                                if (ocena > 5)
                                {
                                    sestevekOcenIzpitov += ocena;
                                    steviloOpravljenihIzpitov++;
                                }
                            }
                            if (steviloOpravljenihIzpitov > 0)
                                povprecjeOcen = sestevekOcenIzpitov / steviloOpravljenihIzpitov;
                            Predmet predmet = vpisaniPredmet.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet;
                            skupnoSteviloTock += Convert.ToInt32(predmet.kreditneTocke);
                            IzvedbaPredmeta izvedbaPredmeta = vpisaniPredmet.IzvedbaPredmeta;

                            if (izvedbaPredmeta != null && predmet != null)
                            {

                                var prijaveNaIzpit = (from p in db.PrijavaNaIzpit
                                                      where p.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit < 7
                                                      && p.IzpitniRok.IzvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId == izvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId
                                                      && p.VpisaniPredmet.Vpis.Student_idStudent1 == uporabnikID
                                                      && p.VpisaniPredmet.IzvedbaPredmeta.StudijskoLeto_idStudijskoLeto == izvedbaPredmeta.StudijskoLeto_idStudijskoLeto
                                                      select p).ToList();
                                //List<PrijavaNaIzpit> prijaveNaIzpit = vpisaniPredmet.PrijavaNaIzpit.ToList();
                                prijaveNaIzpit.Sort((x, y) => (x.VpisaniPredmet.IzvedbaPredmeta.StudijskoLeto.idStudijskoLeto.CompareTo(y.VpisaniPredmet.IzvedbaPredmeta.StudijskoLeto.idStudijskoLeto)));
                                int stevecPrijavLetnika = 1;
                                int steviloVsehPrijav = (from p in db.PrijavaNaIzpit
                                                         where p.VpisaniPredmet.Vpis.Student_idStudent1 == uporabnikID
                                                         && p.StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit < 7
                                                         && p.VpisaniPredmet.IzvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId == izvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId
                                                         select p).ToList().Count;
                                int stevecVsehPrijav = 1;
                                PrijavaNaIzpit tempPrejsnjaPrijava = null;
                                if (prijaveNaIzpit.Count != 0)
                                {
                                    if (radioVsaPolaganja.Checked)
                                    {
                                        bool prviIzpisPredmeta = true;
                                        foreach (PrijavaNaIzpit prijavaNaIzpit in prijaveNaIzpit)
                                        {

                                            if (tempPrejsnjaPrijava != null && // tukaj iščemo, če je datum izpitnega roka v novem letniku
                                                tempPrejsnjaPrijava.VpisaniPredmet.IzvedbaPredmeta.StudijskoLeto.idStudijskoLeto != prijavaNaIzpit.VpisaniPredmet.IzvedbaPredmeta.StudijskoLeto.idStudijskoLeto)
                                            {
                                                stevecPrijavLetnika = 1;
                                            }
                                            if (prviIzpisPredmeta) // v tem primeru izpišemo sam vpisan predmet
                                            {
                                                dataTable.Rows.Add(stevec.ToString(), predmet.idPredmet.ToString(), predmet.imePredmeta, predmet.kreditneTocke.ToString(), (izvedbaPredmeta.Profesor.priimekProfesorja + " " + izvedbaPredmeta.Profesor.imeProfesorja),
                                                    prijavaNaIzpit.IzpitniRok.DatumIzpitnegaRoka.Value.ToString("yyyy-MM-dd"), prijavaNaIzpit.OcenaIzpita.ToString(), (stevecPrijavLetnika + "    " + stevecVsehPrijav));
                                                prviIzpisPredmeta = false;
                                            }
                                            else // izpišemo nadaljne vrstice, ki imajo samo roke
                                            {
                                                dataTable.Rows.Add("", "", "", "", (izvedbaPredmeta.Profesor.priimekProfesorja + " " + izvedbaPredmeta.Profesor.imeProfesorja),
                                                    prijavaNaIzpit.IzpitniRok.DatumIzpitnegaRoka.Value.ToString("yyyy-MM-dd"), prijavaNaIzpit.OcenaIzpita.ToString(), (stevecPrijavLetnika + "    " + stevecVsehPrijav));
                                            }
                                            stevecVsehPrijav++;
                                            stevecPrijavLetnika++;
                                            tempPrejsnjaPrijava = prijavaNaIzpit;
                                        }
                                    }
                                    else if (radioZadnjePolaganje.Checked)
                                    {
                                        PrijavaNaIzpit prijavaNaIzpit = prijaveNaIzpit[prijaveNaIzpit.Count - 1];

                                        dataTable.Rows.Add(stevec.ToString(), predmet.idPredmet.ToString(), predmet.imePredmeta, predmet.kreditneTocke.ToString(),
                                                    (izvedbaPredmeta.Profesor.priimekProfesorja + " " + izvedbaPredmeta.Profesor.imeProfesorja),
                                                    prijavaNaIzpit.IzpitniRok.DatumIzpitnegaRoka.Value.ToString("yyyy-MM-dd"), prijavaNaIzpit.OcenaIzpita.ToString(), steviloVsehPrijav);

                                        stevecPrijavLetnika++;
                                        tempPrejsnjaPrijava = prijavaNaIzpit;
                                    }
                                }
                                else
                                {
                                    dataTable.Rows.Add(stevec.ToString(), predmet.idPredmet.ToString(), predmet.imePredmeta, predmet.kreditneTocke.ToString(), "", "", "", "");
                                }
                                stevec++;
                            }
                        }

                        GridView novGridView = new GridView();

                        //novGridView.ID = "leftPanel";
                        novGridView.CssClass = "table table-striped leftPanel";
                        novGridView.DataSource = dataTable;
                        novGridView.DataBind();

                        PlaceHolder1.Controls.Add(LabelPage);
                        PlaceHolder1.Controls.Add(novGridView);

                        Label labelSkupnoSteviloTock = new Label();
                        labelSkupnoSteviloTock.CssClass = "label label-primary";
                        labelSkupnoSteviloTock.Text = "Skupno število kreditnih točk: " + skupnoSteviloTock.ToString() + "<br>";

                        Label labelPovprecjeIzpita = new Label();
                        labelPovprecjeIzpita.CssClass = "label label-primary";
                        labelPovprecjeIzpita.Text = "Povprečje izpitov: " + povprecjeOcen.ToString();

                        /*Label labelSkupnoSteviloTock = new Label();
                        labelSkupnoSteviloTock.CssClass = "form-group";*/
                        PlaceHolder1.Controls.Add(labelSkupnoSteviloTock);
                        PlaceHolder1.Controls.Add(labelPovprecjeIzpita);
                    }
                }
            }
        }

        protected void radioZadnjePolaganje_CheckedChanged(object sender, EventArgs e)
        {
            Page_Load(sender, e);
        }
    }
}