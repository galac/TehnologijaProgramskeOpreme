using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares.Ucitelj
{
    public partial class SeznamPrijavljenihNaIzpitniRok : System.Web.UI.Page
    {
        t8_2015Entities db = new t8_2015Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                DDL_Years.SelectedValue = "XXXX/XXXX";
                
                // priprava drugega 
                DDL_SubjectList.Items.Clear();
                DDL_SubjectList.Items.Add(new System.Web.UI.WebControls.ListItem("nedoločeno", "XXXXX"));

                GV_subjects.SelectedIndex = -1;
                header31.Visible = false;
                GV_subjects.Visible = false;
                header41.Visible = false;
                L_Error.Visible = false;
                GV_izpitni_roki.Visible = false;
                buttonPdf.Visible = false;
                buttonCsv.Visible = false;
            }
        }        

        protected void DDL_Years_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_subjects.SelectedIndex = -1;
            header31.Visible = false;
            GV_subjects.Visible = false;
            header41.Visible = false;
            L_Error.Visible = false;
            GV_izpitni_roki.Visible = false;
            buttonPdf.Visible = false;
            buttonCsv.Visible = false;

            if (DDL_Years.SelectedValue == "XXXX/XXXX") {
                DDL_SubjectList.Items.Clear();
                DDL_SubjectList.Items.Add(new System.Web.UI.WebControls.ListItem("nedoločeno", "XXXXX"));
                return;
            }

            int studijskoLetoID = Convert.ToInt32(DDL_Years.SelectedValue);
            
            if (Session["uporabnikID"] == null)
                Response.Redirect("~/LoginForm.aspx");

            int idUcitelja = Convert.ToInt32(Session["uporabnikID"]);

            String priimekUcitelja = (from s in db.Student
                               where s.idStudent == idUcitelja
                               select s.priimekStudenta).FirstOrDefault();

            Profesor profesor = (from p in db.Profesor
                                 where p.priimekProfesorja.Equals(priimekUcitelja)
                                 select p).FirstOrDefault();

            var relevantniPredmeti = (((  from i in db.IzvedbaPredmeta
                                        where i.StudijskoLeto_idStudijskoLeto == studijskoLetoID &&
                                        (
                                              //vzamemo samo predmete, ki jih poucuje profesor
                                              i.Profesor_idProfesor == profesor.idProfesor ||
                                              i.Profesor_idProfesor1 == profesor.idProfesor ||
                                              i.Profesor_idProfesor2 == profesor.idProfesor
                                        )
                                        select new {  
                                            DataTextField = i.PredmetStudijskegaPrograma.Predmet.imePredmeta + "  ("+ i.PredmetStudijskegaPrograma.Predmet.idPredmet+")",
                                            DataValueField = ""+i.PredmetStudijskegaPrograma.Predmet.idPredmet
                                        }).Distinct()).OrderBy(i => i.DataTextField)).ToList();

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
            GV_izpitni_roki.Visible = false;
            buttonPdf.Visible = false;
            buttonCsv.Visible = false;

            if (DDL_SubjectList.SelectedValue == "XXXXX") {
                return;
            }
                

            
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
            L_Error.Visible = false;
            header31.Visible = true;
            GV_izpitni_roki.Visible = false;
            buttonPdf.Visible = false;
            buttonCsv.Visible = false;
            L_header31.Text = "Seznam izpitnih rokov za predmet \""+DDL_SubjectList.SelectedItem+"\"";
            int izvedbaPredmetaID = Convert.ToInt32(GV_subjects.DataKeys[GV_subjects.SelectedIndex].Values[0]);
            int stevecRokov = 0;

            var roki = (from rok in db.IzpitniRok
                        where rok.IzvedbaPredmeta_idIzvedbaPredmeta == izvedbaPredmetaID
                        select rok).ToList();

            
            List<object> dupes = new List<object> { };
            foreach (var rok in roki)
            //foreach (var rok in izpitniRoki)
            {
                if (rok.Veljaven == 1)
                {
                    int stPrijav = (from pni in db.PrijavaNaIzpit
                                    where pni.IzpitniRok_IdIzpitniRok == rok.IDIzpitniRok
                                    select pni).Distinct().ToList().Count;
                    var currentObj = new
                    {
                        IdIzpitnegaRoka = rok.IDIzpitniRok,
                        Zaporedna = ++stevecRokov,
                        DatumIzpitnegaRoka = rok.DatumIzpitnegaRoka.ToString(),
                        VrstaIzpitnegaRoka = rok.VrstaIzpitnegaRoka,
                        Prostor = rok.Prostor,
                        SteviloPrijavljenih = stPrijav,
                        SteviloMest = rok.SteviloMest
                    };
                    dupes.Add(currentObj);
                }
            }

            if (dupes.Count < 1) {
                L_Error.Text = "Razpisan ni noben izpitni rok.";
                L_Error.Visible = true;
                return;
            }
            
            //order list comparator

            GV_izpitni_roki.Visible = true;
            GV_izpitni_roki.DataSource = dupes;
            GV_izpitni_roki.DataBind();
        }

        protected void GV_izpitni_roki_SelectedIndexChanged(object sender, EventArgs e)
        {
            L_Error51.Visible = false;
            header51.Visible = true;
            GV_prijavljeni.Visible = false;
            buttonPdf.Visible = false;
            buttonCsv.Visible = false;
            L_header51.Text = "Seznam prijavljenih na izpitni rok \"" + DDL_SubjectList.SelectedItem + "\"";
            int idIzpitniRok = Convert.ToInt32(GV_izpitni_roki.DataKeys[GV_izpitni_roki.SelectedIndex].Values["IdIzpitnegaRoka"]);
            int stevecPrijavljenih = 0;

            IzpitniRok ip = (IzpitniRok)(from ip1 in db.IzpitniRok
                                         where ip1.IDIzpitniRok == idIzpitniRok
                                         select ip1).FirstOrDefault();

            var seznamPrijavljenih = (from pni in db.PrijavaNaIzpit
                                      where pni.IzpitniRok_IdIzpitniRok == idIzpitniRok
                                      select pni.VpisaniPredmet.Vpis.Student).Distinct().ToList().OrderBy(i => i.priimekStudenta);

            List<object> dupes = new List<object> { };
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] { 
                                        new DataColumn("Zaporedna", typeof(int)),
                                        new DataColumn("VpisnaStevilka", typeof(String)),
                                        new DataColumn("Priimek", typeof(String)),
                                        new DataColumn("Ime", typeof(String)),
                                        new DataColumn("StudijskoLeto", typeof(String)),
                                        new DataColumn("ZapStPolaganja", typeof(int)) });
            foreach (var student in seznamPrijavljenih)
            {
                //int psp = (from vp in db.VpisaniPredmet
                //           where vp.Vpis.Student.vpisnaStudenta == student.vpisnaStudenta &&
                //                vp.IzvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId == ip.IzvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId &&
                //                vp.PrijavaNaIzpit.
                //           select vp.PrijavaNaIzpit).Distinct().ToList().Count;
                int stPolaganj = (from pni in db.PrijavaNaIzpit
                                  where pni.VpisaniPredmet.Vpis.Student.vpisnaStudenta == student.vpisnaStudenta &&
                                        pni.VpisaniPredmet.IzvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId == ip.IzvedbaPredmeta.PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId
                                  select pni).Distinct().ToList().Count;
                //var currentObj = new
                //{
                //    Zaporedna = ++stevecPrijavljenih,
                //    VpisnaStevilka = student.vpisnaStudenta != null ? student.vpisnaStudenta.ToString() : "",
                //    Priimek = (student.priimekStudenta != null ? student.priimekStudenta : ""),
                //    Ime = (student.imeStudenta != null ? student.imeStudenta : ""),
                //    StudijskoLeto = ip.IzvedbaPredmeta.StudijskoLeto != null ? ip.IzvedbaPredmeta.StudijskoLeto.studijskoLeto1.ToString() : "",
                //    ZapStPolaganja = ++stPolaganj
                //};
                    int zap = ++stevecPrijavljenih;
                    String vpisnaStevilka = student.vpisnaStudenta != null ? student.vpisnaStudenta.ToString() : "";
                    String priimek = (student.priimekStudenta != null ? student.priimekStudenta : "");
                    String ime = (student.imeStudenta != null ? student.imeStudenta : "");
                    String studijskoLeto = ip.IzvedbaPredmeta.StudijskoLeto != null ? ip.IzvedbaPredmeta.StudijskoLeto.studijskoLeto1.ToString() : "";
                    int zapStPolaganja = ++stPolaganj;
                //dupes.Add(currentObj);
                dt.Rows.Add(zap, vpisnaStevilka, priimek, ime, studijskoLeto, zapStPolaganja);
            }

            //if (dupes.Count < 1)
            if (dt.Rows.Count < 1)
            {
                L_Error51.Text = "Na izpitni rok ni prijavljen noben študent.";
                L_Error51.Visible = true;
                return;
            }

            //order list comparator
            GV_prijavljeni.Visible = true;
            buttonCsv.Visible = true;
            buttonPdf.Visible = true;
            //GV_prijavljeni.DataSource = dupes;
            GV_prijavljeni.DataSource = dt;
            GV_prijavljeni.DataBind();
            ViewState["seznamPrijavljenih"] = dt;
            ViewState["idIzpitniRok"] = idIzpitniRok;
            //ViewState["datumInUra"] = ip.DatumIzpitnegaRoka;
        }

        protected void GV_prijavljeni_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["vpisnaStudent00"] = GV_prijavljeni.SelectedRow.Cells[1].Text;
            Response.Redirect("~/Ucitelj/PodrobnostiOStudentu");
        }

        protected void buttonExportToPdf_click(object sender, EventArgs e)
        {
            GridView GridView1 = new GridView();
            //Arial podpira šumnike
            BaseFont bfBold = BaseFont.CreateFont("c:/windows/fonts/arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont bfNormal = BaseFont.CreateFont("c:/windows/fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            DataTable dt = (DataTable)ViewState["seznamPrijavljenih"];
            GridView1.DataSource = dt;
            GridView1.DataBind();

            GridView1.AllowPaging = false;
            GridView1.AutoGenerateColumns = true;
            Debug.Print("" + dt.Columns.Count + " column od gridview: " + GridView1.Columns.Count);
            if (dt.Columns.Count <= 0)
            {
                Debug.Print("jeba");
            }
            else
            {
                PdfPTable table = new PdfPTable(dt.Columns.Count);
                table.SetWidths(new int[] { 5, 15, 20, 15, 15, 15 });
                Phrase pFirst = new Phrase("Št.");
                pFirst.Font.Size = 15;
                PdfPCell cellFirst = new PdfPCell(pFirst);
                cellFirst.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellFirst.HorizontalAlignment = Element.ALIGN_CENTER;
                String[] headers = { "Št.", "Vpisna številka", "Priimek", "Ime", "Študijsko leto poslušanja", "Zap. št. polaganja" };
                for (int i = 0; i < headers.Length; i++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(headers[i]));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                //Transfer rows from GridView to table
                int count = 0;
                BaseColor alternateRowColor = new BaseColor(210, 228, 223);
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        PdfPCell cell = new PdfPCell();
                        //dodamo piko zaporedni številki
                        if (dc.ColumnName == "Zaporedna") cell = new PdfPCell(new Phrase(dr[dc].ToString() + "."));
                        else cell = new PdfPCell(new Phrase(dr[dc].ToString(), new Font(bfNormal, 12)));

                        //pobarvamo vsako drugo vrstico
                        if (count % 2 == 1)
                        {
                            cell.BackgroundColor = alternateRowColor;
                        }
                        cell.PaddingBottom = 4;
                        cell.PaddingLeft = 5;
                        cell.PaddingRight = 5;
                        cell.PaddingTop = 4;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                    count++;
                }
                int idIzpitniRok = (int)ViewState["idIzpitniRok"];
                IzpitniRok ip = (from i in db.IzpitniRok
                                 where i.IDIzpitniRok == idIzpitniRok
                                 select i).FirstOrDefault();

                Chunk imePredmeta = new Chunk(ip.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.imePredmeta + " (" +
                                              ip.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet.idPredmet + ")", new Font(bfNormal, 12));
                //imePredmeta.SetUnderline(0.5f, -1.5f);
                DateTime dat = ip.DatumIzpitnegaRoka.Value;
                String dat2 = Convert.ToDateTime(dat).ToShortDateString();
                Chunk datum = new Chunk(dat2, new Font(bfBold, 12));
                //datum.SetUnderline(0.5f, -1.5f);
                Chunk ura = new Chunk(ip.DatumIzpitnegaRoka.Value.TimeOfDay.ToString(), new Font(bfBold, 12));
                //ura.SetUnderline(0.5f, -1.5f);
                Chunk prostor = new Chunk(ip.Prostor, new Font(bfBold, 12));
                //prostor.SetUnderline(0.5f, -1.5f);
                String prviProf = ip.IzvedbaPredmeta.Profesor.imeProfesorja + " " + ip.IzvedbaPredmeta.Profesor.priimekProfesorja;
                Chunk profesorPrvi = new Chunk(prviProf, new Font(bfBold,12));
                //profesorPrvi.SetUnderline(0.5f, -1.5f);

                Phrase glava = new Phrase("Izpit za predmet ", new Font(bfNormal,12));
                glava.Add(imePredmeta);
                glava.Add(" bo potekal ");
                glava.Add(datum);
                glava.Add(" ob ");
                glava.Add(ura);
                glava.Add(" v prostoru ");
                glava.Add(prostor);
                glava.Add(". ");


                //String glava = "Izpit za predmet " + imePredmeta + " bo potekal " + ip.DatumIzpitnegaRoka + " v prostoru " + ip.Prostor + ". ";
                if (ip.IzvedbaPredmeta.Profesor_idProfesor1 == null) {
                    glava.Add("Prisoten bo profesor ");
                    glava.Add(profesorPrvi);
                    glava.Add(".");
                } else if (ip.IzvedbaPredmeta.Profesor_idProfesor2 == null) {
                    String drugiProf = ip.IzvedbaPredmeta.Profesor1.imeProfesorja + " " + ip.IzvedbaPredmeta.Profesor1.priimekProfesorja;
                    Chunk profesorDrugi = new Chunk(drugiProf, new Font(bfBold, 12));
                    glava.Add("Prisotna bosta profesor ");
                    glava.Add(profesorPrvi);
                    glava.Add(" in profesor ");
                    glava.Add(profesorDrugi);
                    glava.Add(".");
                } else {
                    String drugiProf = ip.IzvedbaPredmeta.Profesor1.imeProfesorja + " " + ip.IzvedbaPredmeta.Profesor1.priimekProfesorja;
                    Chunk profesorDrugi = new Chunk(drugiProf, new Font(bfBold, 12));
                    String tretjiProf = ip.IzvedbaPredmeta.Profesor2.imeProfesorja + " " + ip.IzvedbaPredmeta.Profesor2.priimekProfesorja;
                    Chunk profesorTretji = new Chunk(tretjiProf, new Font(bfBold, 12));
                    glava.Add("Prisotni bodo profesor ");
                    glava.Add(profesorPrvi);
                    glava.Add(", profesor ");
                    glava.Add(profesorDrugi);
                    glava.Add(" in profesor ");
                    glava.Add(profesorTretji);
                    glava.Add(".");
                }

                Paragraph pp = new Paragraph();
                Phrase p = new Phrase(glava);

                PdfPTable pdfTab = new PdfPTable(1);
                PdfPCell cell1 = new PdfPCell(p);
                cell1.PaddingBottom = 7;
                cell1.PaddingLeft = 7;
                cell1.PaddingRight = 3;
                cell1.PaddingTop = 6;
                cell1.Border = 0;
                cell1.BorderColor = BaseColor.WHITE;
                pdfTab.AddCell(cell1);
                pdfTab.DefaultCell.Border = Rectangle.NO_BORDER;
                pdfTab.SpacingAfter = 15;

                //Create the PDF Document
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 130f, 0f);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                ITextEvents ite = new ITextEvents();
                ite.Header = L_header51.Text;
                pdfWriter.PageEvent = ite;
                pdfDoc.Open();
                pdfDoc.Add(pdfTab);
                pdfDoc.Add(table);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;" +
                                               "filename=FRI_izpis.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();

                //response.ContentType = "application/pdf";
                //response.AddHeader("content-disposition", "attachment;filename=DataTable.pdf");
                //StringWriter sw = new StringWriter();
                //HtmlTextWriter hw = new HtmlTextWriter(sw);
                //GridView1.RenderControl(hw);
                //StringReader sr = new StringReader(sw.ToString());
                //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                //pdfDoc.AddHeader("Header", "testni header, deluje?");
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //PdfWriter.GetInstance(pdfDoc, response.OutputStream);
                //pdfDoc.Open();
                //htmlparser.Parse(sr);
                //pdfDoc.Close();
                //response.Write(pdfDoc);
                //response.End();

            }

            //_Default.exportToPdf(GetDataTable(GV_prijavljeni), Response, L_header51.Text, new int[] { 5, 15, 20, 15, 15, 15  });
        }

        protected void buttonExportToCsv_click(object sender, EventArgs e)
        {
            _Default.exportToCsv(GetDataTable(GV_prijavljeni), Response);
        }

        protected DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null) {
                for (int i = 1; i < dtg.HeaderRow.Cells.Count-1; i++) {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows) {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 1; i < row.Cells.Count-1; i++) {
                    dr[i-1] = row.Cells[i].Text;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void GV_izpitni_roki_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GV_izpitni_roki.Columns[0].Visible = false;
            }
        }
    }
}