using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares.Ucitelj
{
    public partial class SeznamVpisanih : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();

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
                GV_students.Visible = false;
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
            GV_students.Visible = false;
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
            
            t8_2015Entities db = new t8_2015Entities();

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
            GV_students.Visible = false;
            buttonPdf.Visible = false;
            buttonCsv.Visible = false;

            if (DDL_SubjectList.SelectedValue == "XXXXX") {
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
            L_Error.Visible = false;
            header31.Visible = true;
            GV_students.Visible = false;
            buttonPdf.Visible = false;
            buttonCsv.Visible = false;
            L_header31.Text = "Seznam vpisanih v predmet \""+DDL_SubjectList.SelectedItem+"\"";
            int izvedbaPredmetaID = Convert.ToInt32(GV_subjects.DataKeys[GV_subjects.SelectedIndex].Values[0]);
            int stevecStudentov = 0;
            
            t8_2015Entities db = new t8_2015Entities();

            var vpisaniPredmet = (from i in db.IzvedbaPredmeta
                                   where i.idIzvedbaPredmeta == izvedbaPredmetaID
                                   select new
                                   {
                                       Beta = i.VpisaniPredmet
                                   }).Single();                                 
            
            List<object> dupes = new List<object> { };
            foreach (var vp in vpisaniPredmet.Beta.OrderBy(i => i.Vpis.Student.priimekStudenta))
            {
                var currentObj = new {
                    Zaporedna = ++stevecStudentov,
                    VpisnaStevilka = vp.Vpis.Student.vpisnaStudenta != null ? vp.Vpis.Student.vpisnaStudenta.ToString() : "",
                    PriimekInIme = (vp.Vpis.Student.priimekStudenta != null ? vp.Vpis.Student.priimekStudenta : "") + " " + (vp.Vpis.Student.imeStudenta != null ? vp.Vpis.Student.imeStudenta : ""),
                    VrstaVpisa = vp.Vpis.VrstaVpisa.opisVpisa != null ? vp.Vpis.VrstaVpisa.opisVpisa : ""
                };
                dupes.Add(currentObj);
            }

            if (dupes.Count < 1) {
                L_Error.Text = "Na predmet ni vpisan noben študent.";
                L_Error.Visible = true;
                return;
            }
            
            //order list comparator


            GV_students.Visible = true;
            buttonPdf.Visible = true;
            buttonCsv.Visible = true;
            GV_students.DataSource = dupes;
            GV_students.DataBind();
        }

        protected void buttonExportToPdf_click(object sender, EventArgs e)
        {
            _Default.exportToPdf(GetDataTable(GV_students), Response, L_header31.Text, new int[] { 7, 15, 25, 30 });
        }

        protected void buttonExportToCsv_click(object sender, EventArgs e)
        {
            _Default.exportToCsv(GetDataTable(GV_students), Response);
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

        protected void GV_students_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["vpisnaStudent00"] = GV_students.SelectedRow.Cells[1].Text;
            Response.Redirect("~/Ucitelj/PodrobnostiOStudentu");
        }
    }
}