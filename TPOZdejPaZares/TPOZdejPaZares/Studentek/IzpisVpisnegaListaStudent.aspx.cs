using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares.Studentek
{
    public partial class IzpisVpisnegaListaStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                t8_2015Entities db = new t8_2015Entities();
                //Session["uporabnikID"] = 6;

                int uporabnikID = (int)Session["uporabnikID"];
                Student uporabnik = (from s in db.Student
                                     where s.idStudent == uporabnikID
                                     select s).FirstOrDefault();

                if (uporabnik.Vpis.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aaa", "<script type='text/javascript'>alert('Ta uporabnik ni bil še nikoli vpisan v katerikoli predmet. Preusmeritev na zajem vpisnega lista.')\nwindow.location = 'ZajemVpisnegaListaStudent.aspx'</script>", false);
                    return;
                }

                string celaStran = "";
                try
                {
                    using (StreamReader sr = new StreamReader(Server.MapPath("~/Studentek/PorociloFiltered.htm")))
                    {
                        celaStran = sr.ReadToEnd();
                        //celaStran = celaStran.Replace("CLIENTCLIENT", UpdatePanelPage.ClientID);
                    }
                }
                catch (Exception ex)
                {

                }
                int maxLetoVpisa = 0;
                Vpis maxVpis = null;
                foreach (Vpis vpis in uporabnik.Vpis)
                {
                    int letoVpisa = Convert.ToInt32(vpis.StudijskoLeto.Substring(0, 4));
                    if (letoVpisa > maxLetoVpisa)
                    {
                        maxLetoVpisa = letoVpisa;
                        maxVpis = vpis;
                    }
                }

                celaStran = celaStran.Replace("VPISNA", uporabnik.vpisnaStudenta == null ? "" : uporabnik.vpisnaStudenta.ToString());
                celaStran = celaStran.Replace("PRIIMEKIME", uporabnik.priimekStudenta + ", " + uporabnik.imeStudenta);
                celaStran = celaStran.Replace("DATUMROJSTVA", (uporabnik.DatumRojstva == null) ? "" : uporabnik.DatumRojstva.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                celaStran = celaStran.Replace("KRAJROJSTVA", (uporabnik.KrajRojstva == null) ? "" : uporabnik.KrajRojstva);
                //celaStran = celaStran.Replace("KRAJROJSTVA", uporabnik.KrajRojstva);
                celaStran = celaStran.Replace("DRZAVAOBCINAROJSTVA", ((uporabnik.Drzava2 == null) ? "" : (uporabnik.Drzava2.imeDrzave + ", ")) + ((uporabnik.Obcina1 == null) ? "" : uporabnik.Obcina1.imeObcine));
                celaStran = celaStran.Replace("DRZAVLJANSTVO", (uporabnik.Drzava1 == null) ? "" : uporabnik.Drzava1.imeDrzave);

                celaStran = celaStran.Replace("SPOL", (uporabnik.Spol == null) ? "" : uporabnik.Spol);
                celaStran = celaStran.Replace("EMSO", (uporabnik.EMSO == null) ? "" : uporabnik.EMSO);
                celaStran = celaStran.Replace("DAVCNASTEVILKA", (uporabnik.DavcnaStevilka == null) ? "" : uporabnik.DavcnaStevilka.ToString());
                celaStran = celaStran.Replace("EMAIL", (uporabnik.mailStudenta == null) ? "" : uporabnik.mailStudenta);
                celaStran = celaStran.Replace("TELEFON", (uporabnik.Telefon == null) ? "" : uporabnik.Telefon);

                celaStran = celaStran.Replace("CHECK1", (uporabnik.VrocanjeStalnoBivalisce == 1) ? "<input type='checkbox' checked>" : "<input type='checkbox'>");
                celaStran = celaStran.Replace("CHECK2", (uporabnik.VrocanjeStalnoBivalisce == 0) ? "<input type='checkbox' checked>" : "<input type='checkbox'>");

                celaStran = celaStran.Replace("NASLOV1", (uporabnik.Naslov == null) ? "" : uporabnik.Naslov);
                celaStran = celaStran.Replace("NASLOV2", (uporabnik.NaslovZacasni == null) ? "" : uporabnik.NaslovZacasni);

                celaStran = celaStran.Replace("DRZAVAOBCINA1", ((uporabnik.Drzava == null) ? "" : (uporabnik.Drzava.imeDrzave + ", ")) + ((uporabnik.Obcina == null) ? "" : uporabnik.Obcina.imeObcine));
                celaStran = celaStran.Replace("DRZAVAOBCINA2", ((uporabnik.Drzava3 == null) ? "" : (uporabnik.Drzava3.imeDrzave + ", ")) + ((uporabnik.Obcina2 == null) ? "" : uporabnik.Obcina2.imeObcine));

                celaStran = celaStran.Replace("STUDIJSKIPROGRAM1", (maxVpis.StudijskiProgram == null) ? "" : maxVpis.StudijskiProgram.naziv);
                celaStran = celaStran.Replace("USMERITEV", "Računalništvo");
                celaStran = celaStran.Replace("KRAJIZVAJANJA", "Ljubljana");
                celaStran = celaStran.Replace("IZBIRNASKUPINA", "");
                celaStran = celaStran.Replace("VRSTASTUDIJA", (maxVpis.StudijskiProgram.stopnjaStudija == null) ? "" : maxVpis.StudijskiProgram.stopnjaStudija.naziv + " (" + maxVpis.StudijskiProgram.stopnjaStudija.stopnja + ")");
                celaStran = celaStran.Replace("VRSTAVPISA", (maxVpis.VrstaVpisa == null) ? "" : maxVpis.VrstaVpisa.opisVpisa);
                celaStran = celaStran.Replace("LETNIK", (maxVpis.Letnik == null) ? "" : maxVpis.Letnik.letnik1.ToString() + ".");
                celaStran = celaStran.Replace("NACINOBLIKASTUDIJA", ((maxVpis.NacinStudija == null) ? "" : (maxVpis.NacinStudija.opisNacina + ", ")) + ((maxVpis.OblikaStudija == null) ? "" : maxVpis.OblikaStudija.opisOblike));

                int minLetoVpisa = 9000;
                Vpis minVpis = null;
                foreach (Vpis vpis in uporabnik.Vpis)
                {
                    int letoVpisa = Convert.ToInt32(vpis.StudijskoLeto.Substring(0, 4));
                    if (letoVpisa < minLetoVpisa)
                    {
                        minLetoVpisa = letoVpisa;
                        minVpis = vpis;
                    }
                }

                celaStran = celaStran.Replace("LETOPRVEGAVPISA", (minVpis.StudijskoLeto == null) ? "" : minVpis.StudijskoLeto);
                celaStran = celaStran.Replace("STUDIJSKIPROGRAM2", (maxVpis.StudijskiProgram == null) ? "" : maxVpis.StudijskiProgram.naziv);

                LabelPage.Text = celaStran;





                //cmd.CommandText = "Select s.vpisnaStudenta AS 'Vpisna stevilka', s.imeStudenta AS 'Ime', s.priimekStudenta AS 'Priimek', s.mailStudenta AS 'E-mail' FROM Student s WHERE s.vpisnaStudenta LIKE '" + vpisna + "%'";

                DataTable dataTable = new DataTable();
                dataTable.Columns.AddRange(new DataColumn[3] {
                    new DataColumn("Učitelj", typeof(string)),
                    new DataColumn("Učna enota", typeof(string)),
                    new DataColumn("Število KT", typeof(int))
                });

                foreach (VpisaniPredmet vpisaniPredmet in maxVpis.VpisaniPredmet)
                {
                    IzvedbaPredmeta izvedbaPredmeta = vpisaniPredmet.IzvedbaPredmeta;
                    if (izvedbaPredmeta != null)
                    {
                        string profesorji = izvedbaPredmeta.Profesor.imeProfesorja + " " + izvedbaPredmeta.Profesor.priimekProfesorja;
                        if (izvedbaPredmeta.Profesor1 != null)
                        {
                            profesorji += ", " + izvedbaPredmeta.Profesor1.imeProfesorja + " " + izvedbaPredmeta.Profesor1.priimekProfesorja;
                        }
                        if (izvedbaPredmeta.Profesor2 != null)
                        {
                            profesorji += ", " + izvedbaPredmeta.Profesor2.imeProfesorja + " " + izvedbaPredmeta.Profesor2.priimekProfesorja;
                        }


                        dataTable.Rows.Add(profesorji, izvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta, izvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.kreditneTocke);
                    }
                }


                GridViewPage.DataSource = dataTable;
                GridViewPage.DataBind();
            }
        }
        
    }
}