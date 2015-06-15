using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;

namespace TPOZdejPaZares
{
    public partial class PotrditevVpisa : System.Web.UI.Page
    {
        public void skrijAliPokazi(int potrjen)
        {
            if (potrjen == 0)
            {

                bPotrdiVpis.Visible = true;
                bPotrdiloOVpisu.Visible = false;
            }
            else
            {
                bPotrdiVpis.Visible = false;
                bPotrdiloOVpisu.Visible = true;
            }
        }
       
        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

            DetailsView1.Visible = true;
            GridView3.SelectedIndex = -1;
            t8_2015Entities db = new t8_2015Entities();
            var infoOVpisu = (from vp in db.Vpis
                              where vp.idVpis == (int)(GridView2.SelectedValue)
                              select new
                              {
                                  idVpis = vp.idVpis,
                                  VrstaVpisa_idVrstaVpisa = vp.VrstaVpisa.opisVpisa,
                                  OblikaStudija_idOblikaStudija = vp.OblikaStudija.opisOblike,
                                  Letnik_idLetnik = vp.Letnik.idLetnik,
                                  StudijskiProgram_idStudijskiProgram = vp.StudijskiProgram.naziv,
                                  NacinStudija_idNacinStudija = vp.NacinStudija.opisNacina,
                                  Student_idStudent1 = vp.Student_idStudent1,
                                  Potrjen = vp.Potrjen,
                                  StudijskoLetoDV = vp.StudijskoLeto
                              }).ToList();
            DetailsView1.DataSource = infoOVpisu;
            DetailsView1.DataBind();
           // var izbrani = GridView2.SelectedRow.Cells[3].Text;
            //Console.Write(izbrani.ToString());
            var value = GridView2.SelectedValue;
            Debug.Write(" \n GRID VIEW 2 SELECTED INDEX CHANGED, SELECTED VALUE:" + value);
            var indexSt = int.Parse(value.ToString());
            //     Debug.Write(indexSt);
            //  var vpis = (from v in db.Vpis where v.Student_idStudent1 == indexSt && ma)
            Vpis currVpis = db.Vpis.Find(GridView2.SelectedValue);
            Label1.Text = db.StudijskiProgram.Find(currVpis.StudijskiProgram_idStudijskiProgram).naziv;
            Label2.Text = "Letnik " + currVpis.Letnik_idLetnik.ToString();
            Label3.Text = db.NacinStudija.Find(currVpis.NacinStudija_idNacinStudija).opisNacina;
            skrijAliPokazi(currVpis.Potrjen);
        /*    var vpisi = db.Vpis.Where(x => x.Student_idStudent1 == indexSt && x.Potrjen == 0).ToList();
            if (vpisi.FirstOrDefault() != null)
            {
                var vpis = vpisi.OrderByDescending(x => x.Letnik_idLetnik).First();
                if (vpis != null)
                {
                    Debug.Write(vpis.ToString());
                    Label1.Text = db.StudijskiProgram.Find(vpis.StudijskiProgram_idStudijskiProgram).naziv;
                    Label2.Text = "Letnik " + vpis.Letnik_idLetnik.ToString();
                    Label3.Text = db.NacinStudija.Find(vpis.NacinStudija_idNacinStudija).opisNacina;
                    skrijAliPokazi(vpis.Potrjen);
                }
            }*/
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            t8_2015Entities db = new t8_2015Entities();
            var nepotrjeniStudenti = (from i in db.Vpis
                                        where i.Potrjen == 0
                                        select new
                                        {
                                            ime = i.Student.imeStudenta != null ? i.Student.imeStudenta : "",
                                            priimek = i.Student.priimekStudenta != null ? i.Student.priimekStudenta : "",
                                            vpisnaStevilka = i.Student.vpisnaStudenta.ToString() != null ? i.Student.vpisnaStudenta.ToString() : "",
                                            idStudent = i.Student.idStudent != null ? i.Student.idStudent : 0,
                                            idVpisa = i.idVpis != null ? i.idVpis : 0,
                                        }).ToList();
            GridView2.DataSource = nepotrjeniStudenti;
            GridView2.DataBind();

            var potrjeniStudenti = (from i in db.Vpis
                                        where i.Potrjen >= 1 && (i.StudijskoLeto == "2014/2015" || i.StudijskoLeto == "2015/2016")
                                        select new
                                        {
                                            ime = i.Student.imeStudenta != null ? i.Student.imeStudenta : "",
                                            priimek = i.Student.priimekStudenta != null ? i.Student.priimekStudenta : "",
                                            vpisnaStevilka = i.Student.vpisnaStudenta.ToString() != null ? i.Student.vpisnaStudenta.ToString() : "",
                                            idStudent = i.Student.idStudent != null ? i.Student.idStudent : 0,
                                            idVpisa = i.idVpis != null ? i.idVpis : 0,
                                        }).ToList();
            GridView3.DataSource = potrjeniStudenti;
            GridView3.DataBind();
        }

        protected void bPotrdiVpis_Click(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();
            Vpis vpis = db.Vpis.Find(GridView2.SelectedValue);
            Debug.Write("Potrjen: " + vpis.Potrjen);
            vpis.Potrjen=1;
            skrijAliPokazi(vpis.Potrjen);
            db.SaveChanges();
        }

        protected void bPotrdiloOVpisu_Click(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();
            Student currS;
            Vpis currV = null;
            if (GridView3.SelectedIndex != -1)
            {
                currV = db.Vpis.Find((int)GridView3.SelectedValue);
            }
            else if (GridView2.SelectedIndex != -1) currV = db.Vpis.Find((int)GridView2.SelectedValue);

            if (currV != null)
            {
                currS = db.Student.Find(currV.Student_idStudent1);
                // Label1.Text = db.StudijskiProgram.Find(vpis.StudijskiProgram_idStudijskiProgram).naziv;
                // Label2.Text = "Letnik " + vpis.Letnik_idLetnik.ToString();
                // Label3.Text = db.NacinStudija.Find(vpis.NacinStudija_idNacinStudija).opisNacina;
                DataTable luka = new DataTable();
                luka.Clear();
                luka.Columns.Add("Opis");
                luka.Columns.Add("Vsebina");
                /*  var doc = new Document();
                  string path = Server.MapPath("PDFs");
                  var idVpisa = DetailsView1.SelectedValue.ToString();
                  PdfWriter.GetInstance(doc, new FileStream(path + "/Vpis" +idVpisa +".pdf", FileMode.Create));
                  doc.Open();
                  doc.Add(new Paragraph("My first PDF"));*/
                PdfPTable table = new PdfPTable(2);
                PdfPCell cell = new PdfPCell(new Phrase("Potrdilo o vpisu"));
                cell.Colspan = 2;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                addToTable(luka, "Potrjujemo, da je študent: ", (currS.priimekStudenta + ", " + currS.imeStudenta));
                addToTable(luka, "z vpisno številko: ", currS.vpisnaStudenta.ToString());
                addToTable(luka, "rojen-a dne: ", currS.DatumRojstva.ToString());
                addToTable(luka, "v kraju: ", db.Obcina.Find(currS.Obcina_idObcina).imeObcine);
                addToTable(luka, "EMŠO: ", currS.EMSO);
                addToTable(luka, "Vpisan-a v: ", currV.Letnik_idLetnik.ToString() + ". letnik");
                addToTable(luka, "V študijskem letu: ", "2014/15");
                addToTable(luka, "Način študija", db.NacinStudija.Find(currV.NacinStudija_idNacinStudija).opisNacina);
                addToTable(luka, "Študijski program: ", db.StudijskiProgram.Find(currV.StudijskiProgram_idStudijskiProgram).naziv);

                /*doc.Add(table);
                doc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;" +
                                               "filename=Vpis" +idVpisa +".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(doc);
                Response.End();*/
                _Default.exportToPdf(luka, Response, "Potrdilo o vpisu", new int[] { 5, 20, 20 });
            }
        }
        void addToTable(DataTable tabela, String opis, String vsebina) {
            DataRow row = tabela.NewRow();
            row["Opis"] = opis;
            row["Vsebina"] = vsebina;
            tabela.Rows.Add(row);
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();
            GridView2.SelectedIndex = -1;
            DetailsView1.Visible = true;
            var infoOVpisu = (from vp in db.Vpis 
                              where vp.idVpis== (int) (GridView3.SelectedValue)
                               select new {
                                    idVpis = vp.idVpis,
                                    VrstaVpisa_idVrstaVpisa = vp.VrstaVpisa.opisVpisa,
                                    OblikaStudija_idOblikaStudija = vp.OblikaStudija.opisOblike,
                                    Letnik_idLetnik = vp.Letnik.idLetnik,
                                    StudijskiProgram_idStudijskiProgram = vp.StudijskiProgram.naziv,
                                    NacinStudija_idNacinStudija = vp.NacinStudija.opisNacina,
                                    Student_idStudent1 = vp.Student_idStudent1,
                                    Potrjen = vp.Potrjen,
                                    StudijskoLetoDV = vp.StudijskoLeto
                               }).ToList();
            DetailsView1.DataSource = infoOVpisu;
            DetailsView1.DataBind();
            // var izbrani = GridView2.SelectedRow.Cells[3].Text;
            //Console.Write(izbrani.ToString());
            var value = GridView3.SelectedValue;
            Debug.Write(" \n GRID VIEW 3 SELECTED INDEX CHANGED, SELECTED VALUE:" + value);
            var indexSt = int.Parse(value.ToString());
            //     Debug.Write(indexSt);
            //  var vpis = (from v in db.Vpis where v.Student_idStudent1 == indexSt && ma)
          //  var vpisi = db.Vpis.Where(x => x.Student_idStudent1 == indexSt).ToList();
            Vpis currVpis = db.Vpis.Find(GridView3.SelectedValue);
            Label1.Text = db.StudijskiProgram.Find(currVpis.StudijskiProgram_idStudijskiProgram).naziv;
            Label2.Text = "Letnik " + currVpis.Letnik_idLetnik.ToString();
            Label3.Text = db.NacinStudija.Find(currVpis.NacinStudija_idNacinStudija).opisNacina;
            skrijAliPokazi(currVpis.Potrjen);
        /*    if (vskrijAliPokazi(vpis.Potrjen);pisi.FirstOrDefault() != null)
            {
                var vpis = vpisi.OrderByDescending(x => x.Letnik_idLetnik).First();
                if (vpis != null)
                {
                    Debug.Write(vpis.ToString());
                    Label1.Text = db.StudijskiProgram.Find(vpis.StudijskiProgram_idStudijskiProgram).naziv;
                    Label2.Text = "Letnik " + vpis.Letnik_idLetnik.ToString();
                    Label3.Text = db.NacinStudija.Find(vpis.NacinStudija_idNacinStudija).opisNacina;
                    skrijAliPokazi(vpis.Potrjen);
                    
                }
            }*/
        }      
    }
}