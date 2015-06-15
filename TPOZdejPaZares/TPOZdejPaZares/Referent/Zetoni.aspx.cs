using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares
{
    public partial class Zetoni : System.Web.UI.Page
    {
        t8_2015Entities db = new t8_2015Entities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonIme_Click(object sender, EventArgs e)
        {
            string ime = inputIme.Text;
            string priimek = inputPriimek.Text;
            zetoniStudentaGV.Visible = false;
            novZetonDiv.Style.Add("display", "none");
                
            t8_2015Entities db = new t8_2015Entities();
            var studenti = (from s in db.Student
                            .Where(s => s.imeStudenta.ToString().StartsWith(ime))
                            .Where(s => s.priimekStudenta.ToString().StartsWith(priimek))
                            select s)
                            .ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[4] {
                new DataColumn("vpisnaStevilka", typeof(int)),
                new DataColumn("ime", typeof(String)),
                new DataColumn("priimek", typeof(String)),
                new DataColumn("idStudent", typeof(int))
            });

            foreach (var item in studenti)
            {
                dataTable.Rows.Add(item.vpisnaStudenta, item.imeStudenta, item.priimekStudenta, item.idStudent);
            }
            ViewState["DataTable"] = dataTable;

            GridViewIme.DataSource = dataTable;
            GridViewIme.DataBind();
            if (GridViewIme.Rows.Count > 0)
            {
                LabelOpozorilo.Visible = false;
            }
            else
            {
                LabelOpozorilo.Text = "Najden ni bil nobeden študent.";
                LabelOpozorilo.Visible = true;
            }

            GridViewIme.DataSource = dataTable;
            GridViewIme.DataBind();
            
        }

        protected void buttonVpisna_Click(object sender, EventArgs e)
        {
            zetoniStudentaGV.Visible = false;
            novZetonDiv.Style.Add("display", "none");
            string vpisna = inputVpisna.Text;
            t8_2015Entities db = new t8_2015Entities();
            var studenti = (from s in db.Student
                            .Where(s => s.vpisnaStudenta.ToString().StartsWith(vpisna))
                            select s).ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[4] {
                new DataColumn("vpisnaStevilka", typeof(int)),
                new DataColumn("ime", typeof(String)),
                new DataColumn("priimek", typeof(String)),
                new DataColumn("idStudent", typeof(int))
            });

            foreach (var item in studenti)
            {
                dataTable.Rows.Add(item.vpisnaStudenta, item.imeStudenta, item.priimekStudenta, item.idStudent);
            }

            ViewState["DataTable"] = dataTable;

            GridViewIme.DataSource = dataTable;
            GridViewIme.DataBind();
            if (GridViewIme.Rows.Count > 0)
            {
                LabelOpozorilo.Visible = false;
            }
            else
            {
                LabelOpozorilo.Text = "Najden ni bil nobeden študent.";
                LabelOpozorilo.Visible = true;
            }

            GridViewIme.DataSource = dataTable;
            GridViewIme.DataBind();
        }

        protected void GridViewIme_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idStudent = (int)GridViewIme.DataKeys[GridViewIme.SelectedIndex].Values["idStudent"];
            int vpisna = (int)GridViewIme.DataKeys[GridViewIme.SelectedIndex].Values["vpisnaStevilka"];
            var zetoni = (from z in db.Zeton
                          where z.Student_idStudent == idStudent
                          select z).ToList();
            LabelOpozorilo.Visible = false;

            var vsiVpisi = db.Vpis.Where(i => i.Student_idStudent1 == idStudent).OrderByDescending(i => i.Letnik_idLetnik).ToList();
            Vpis vpis = vsiVpisi.Count > 0 ? (Vpis)vsiVpisi.FirstOrDefault() : null;
            Boolean prostaIzbiraPredmetov = povprecnaOcena(vpisna) >= 8.5 ? true : false;

                DataTable dataTable = new DataTable();
                dataTable.Columns.AddRange(new DataColumn[8] {
                    new DataColumn("StudijskiProgram", typeof(String)),
                    new DataColumn("Letnik", typeof(int)),
                    new DataColumn("VrstaVpisa", typeof(String)),
                    new DataColumn("NacinStudija", typeof(String)),
                    new DataColumn("OblikaStudija", typeof(String)),
                    new DataColumn("prostaIzbira", typeof(String)),
                    new DataColumn("izkoriscen", typeof(String)),
                    new DataColumn("idZeton", typeof(int))
                });

                var studijskiProgrami = (from sp in db.StudijskiProgram
                                         select sp).ToList();

                var letniki = (from l in db.Letnik
                               select l).ToList();

                var vrsteVpisa = (from vv in db.VrstaVpisa
                                  select vv).ToList();

                var naciniStudija = (from nc in db.NacinStudija
                                     select nc).ToList();

                var oblikeStudija = (from os in db.OblikaStudija
                                     select os).ToList();

                if (zetoni.Count == 0)
                {
                    //LabelOpozorilo.Text = "Najden ni bil noben žeton.";
                    //LabelOpozorilo.Visible = true;
                    zetoniStudentaGV.Visible = false;
                    zetoniStudentaGV.DataSource = null;
                    napolniDodajNovZeton();
                    novZetonDiv.Style.Add("display", "block");
                    return;
                }

                foreach (var z in zetoni)
                {
                        String pogoji = prostaIzbiraPredmetov ? "DA" : "NE";
                        String izkoriscen = (bool)z.Izkoriscen ? "DA" : "NE";
                        String studijskiProgram = "";
                        String vrstaVpisa = "";
                        String nacinStudija = "";
                        String oblikaStudija = "";
                        foreach (StudijskiProgram item in studijskiProgrami)
                        {
                            if (item.idStudijskiProgram == z.studijskiProgram) studijskiProgram = item.sifraKratka + " " + item.naziv;
                        }

                        foreach (VrstaVpisa item in vrsteVpisa)
                        {
                            if (item.idVrstaVpisa == z.vrstaVpisa) vrstaVpisa = item.opisVpisa;
                        }

                        foreach (NacinStudija item in naciniStudija)
                        {
                            if (item.idNacinStudija == z.nacinStudija) nacinStudija = item.opisNacina;
                        }

                        foreach (OblikaStudija item in oblikeStudija)
                        {
                            if (item.idOblikaStudija == z.oblikaStudija) oblikaStudija = item.opisOblike;
                        }

                        dataTable.Rows.Add(studijskiProgram, z.letnik, vrstaVpisa, nacinStudija, oblikaStudija, pogoji, izkoriscen, z.idZeton);
                }

                zetoniStudentaGV.Visible = true;
                zetoniStudentaGV.DataSource = dataTable;
                zetoniStudentaGV.DataBind();

                for (int i = 0; i < zetoniStudentaGV.Rows.Count; i++)
                {
                    if (i == zetoniStudentaGV.EditIndex)
                        continue;
                    bool isEnabled = true;
                    Label izkoriscenLB = zetoniStudentaGV.Rows[i].FindControl("izkoriscenLB") as Label;
                    if (izkoriscenLB.Text.Equals("DA")) isEnabled = false;
                    (zetoniStudentaGV.Rows[i].FindControl("LB_uredi") as LinkButton).CssClass = isEnabled ? "btn btn-info" : "btn btn-info disabled";
                    (zetoniStudentaGV.Rows[i].FindControl("LB_uredi") as LinkButton).Enabled = isEnabled;
                    (zetoniStudentaGV.Rows[i].FindControl("LB_izbrisi") as LinkButton).CssClass = isEnabled ? "btn btn-danger" : "btn btn-danger disabled";
                    (zetoniStudentaGV.Rows[i].FindControl("LB_izbrisi") as LinkButton).Enabled = isEnabled;
                }
                napolniDodajZeton();
        }


        protected void GridViewIme_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewIme.Columns[3].Visible = false;
            }
        }
        protected void zetoniStudentaGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                zetoniStudentaGV.Columns[7].Visible = false;
                    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    {
                        int idZeton = (int)zetoniStudentaGV.DataKeys[zetoniStudentaGV.EditIndex].Values["IdZeton"];
                        Zeton zeton = (Zeton)(from z in db.Zeton
                                          where z.idZeton == idZeton
                                          select z).FirstOrDefault();
                        //študijski program
                        DropDownList studijskiProgramDDL = (DropDownList)e.Row.FindControl("studijskiProgramDDL");

                        var studijskiProgrami = (from sp in db.StudijskiProgram
                                                 select sp).ToList();
                        int index = -1;
                        foreach (var item in studijskiProgrami)
                        {
                            studijskiProgramDDL.Items.Add(new ListItem(item.sifraKratka + " - " + item.naziv, item.idStudijskiProgram.ToString()));
                            if (item.idStudijskiProgram == zeton.studijskiProgram)
                            {
                                index = studijskiProgramDDL.Items.Count - 1;
                            }
                        }
                        studijskiProgramDDL.DataBind();
                        studijskiProgramDDL.SelectedIndex = index;

                        //letnik
                        DropDownList letnikDDL = (DropDownList)e.Row.FindControl("letnikDDL");
                        var letniki = (from l in db.Letnik
                                       select l).ToList();

                        int index2 = -1;
                        foreach (var item in letniki)
                        {
                            letnikDDL.Items.Add(new ListItem(item.idLetnik.ToString(), item.idLetnik.ToString()));
                            if (item.idLetnik == zeton.letnik)
                            {
                                index2 = letnikDDL.Items.Count - 1;
                            }
                        }
                        letnikDDL.DataBind();
                        letnikDDL.SelectedIndex = index2;

                        //vrsta vpisa
                        DropDownList vrstaVpisaDDL = (DropDownList)e.Row.FindControl("vrstaVpisaDDL");
                        var vrsteVpisa = (from vv in db.VrstaVpisa
                                          select vv).ToList();

                        int index3 = -1;
                        foreach (var item in vrsteVpisa)
                        {
                            vrstaVpisaDDL.Items.Add(new ListItem(item.opisVpisa, item.idVrstaVpisa.ToString()));
                            if (item.idVrstaVpisa == zeton.vrstaVpisa)
                            {
                                index3 = vrstaVpisaDDL.Items.Count - 1;
                            }
                        }
                        vrstaVpisaDDL.DataBind();
                        vrstaVpisaDDL.SelectedIndex = index3;

                        //način študija
                        DropDownList nacinStudijaDDL = (DropDownList)e.Row.FindControl("nacinStudijaDDL");
                        var naciniStudija = (from nc in db.NacinStudija
                                             select nc).ToList();

                        int index4 = -1;
                        foreach (var item in naciniStudija)
                        {
                            nacinStudijaDDL.Items.Add(new ListItem(item.opisNacina, item.idNacinStudija.ToString()));
                            if (item.idNacinStudija == zeton.nacinStudija)
                            {
                                index4 = nacinStudijaDDL.Items.Count - 1;
                            }
                        }
                        nacinStudijaDDL.DataBind();
                        nacinStudijaDDL.SelectedIndex = index4;

                        //oblika študija
                        DropDownList oblikaStudijaDDL = (DropDownList)e.Row.FindControl("oblikaStudijaDDL");
                        var oblikeStudija = (from os in db.OblikaStudija
                                             select os).ToList();

                        int index5 = -1;
                        foreach (var item in oblikeStudija)
                        {
                            oblikaStudijaDDL.Items.Add(new ListItem(item.opisOblike, item.idOblikaStudija.ToString()));
                            if (item.idOblikaStudija == zeton.oblikaStudija)
                            {
                                index5 = oblikaStudijaDDL.Items.Count - 1;
                            }
                        }
                        oblikaStudijaDDL.DataBind();
                        oblikaStudijaDDL.SelectedIndex = index5;

                        //prosta izbira
                        DropDownList prostaIzbiraDDL = (DropDownList)e.Row.FindControl("prostaIzbiraDDL");
                        prostaIzbiraDDL.Items.Add(new ListItem("NE", "0"));
                        prostaIzbiraDDL.Items.Add(new ListItem("DA", "1"));
                        prostaIzbiraDDL.DataBind();
                        prostaIzbiraDDL.SelectedIndex = (int)zeton.prostaIzbira;

                        //izkoriščen
                        DropDownList izkoriscenDDL = (DropDownList)e.Row.FindControl("izkoriscenDDL");
                        izkoriscenDDL.Items.Add(new ListItem("NE", "0"));
                        izkoriscenDDL.Items.Add(new ListItem("DA", "1"));
                        izkoriscenDDL.DataBind();
                        izkoriscenDDL.SelectedIndex = (bool)zeton.Izkoriscen ? 1 : 0;
                    }

                //}
            }
        }
        protected void zetoniStudentaGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)zetoniStudentaGV.DataKeys[e.RowIndex].Values["IdZeton"];
            Zeton zeton = (Zeton)(from z in db.Zeton
                                  where z.idZeton == id
                                  select z).FirstOrDefault();
            if (zeton != null)
            {
                db.Zeton.Remove(zeton);
                db.SaveChanges();
                GridViewIme_SelectedIndexChanged(null, null);
                //seznamPredmetovGV_Init();
            }
            else
            {
                Console.WriteLine("Povpraševanje v bazo ni uspelo, imamo null objekt.");
            }
        }

        protected void zetoniStudentaGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        protected void zetoniStudentaGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            zetoniStudentaGV.EditIndex = e.NewEditIndex;
            GridViewIme_SelectedIndexChanged(null, null);
        }

        protected Double povprecnaOcena(int vpisna)
        {
            Student student = (Student)(from s in db.Student
                                        where s.vpisnaStudenta == vpisna
                                        select s).FirstOrDefault();
            var vpisSeznam = (from v in db.Vpis
                              where
                                  v.Student_idStudent1 == student.idStudent
                              select v).ToList();

            double[] prviLetnik = { 0.0, 0.0 };
            double[] drugiLetnik = { 0.0, 0.0 };

            foreach (var item in vpisSeznam)
            {
                var ocenePredmetov = (from vp in db.VpisaniPredmet
                                      where vp.Vpis_idVpis == item.idVpis
                                      select vp.ocena).ToList();
                if (item.Letnik_idLetnik == 1)
                {
                    foreach (var ocena in ocenePredmetov)
                    {
                        if (ocena != null)
                        {
                            prviLetnik[0] += 1.0;
                            prviLetnik[1] += (double)ocena;
                        }
                    }
                }
                else if (item.Letnik_idLetnik == 2)
                {
                    foreach (var ocena in ocenePredmetov)
                    {
                        if (ocena != null)
                        {
                            drugiLetnik[0] += 1.0;
                            drugiLetnik[1] += (double)ocena;
                        }
                    }
                }
            }
            double[] povpPoLetniku = new double[2];
            povpPoLetniku[0] = prviLetnik[1] / prviLetnik[0];
            povpPoLetniku[1] = drugiLetnik[1] / drugiLetnik[0];

            if (prviLetnik[0] == 10 && drugiLetnik[0] == 10)
            {
                return (povpPoLetniku[0] + povpPoLetniku[1]) / 2;
            }
            else
            {
                return 1;
            }
        }

        protected void zetoniStudentaGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idZeton = (int)zetoniStudentaGV.DataKeys[zetoniStudentaGV.EditIndex].Values["IdZeton"];
            Zeton zeton = (Zeton)(from z in db.Zeton
                                  where z.idZeton == idZeton
                                  select z).FirstOrDefault();
            //študijski program
            DropDownList studijskiProgramDDL = (DropDownList)zetoniStudentaGV.Rows[e.RowIndex].FindControl("studijskiProgramDDL");
            zeton.studijskiProgram = Int32.Parse(studijskiProgramDDL.SelectedValue);
            DropDownList letnikDDL = (DropDownList)zetoniStudentaGV.Rows[e.RowIndex].FindControl("letnikDDL");
            zeton.letnik = Int32.Parse(letnikDDL.SelectedValue);
            DropDownList vrstaVpisaDDL = (DropDownList)zetoniStudentaGV.Rows[e.RowIndex].FindControl("vrstaVpisaDDL");
            zeton.vrstaVpisa = Int32.Parse(vrstaVpisaDDL.SelectedValue);
            DropDownList nacinStudijaDDL = (DropDownList)zetoniStudentaGV.Rows[e.RowIndex].FindControl("nacinStudijaDDL");
            zeton.nacinStudija = Int32.Parse(nacinStudijaDDL.SelectedValue);
            DropDownList oblikaStudijaDDL = (DropDownList)zetoniStudentaGV.Rows[e.RowIndex].FindControl("oblikaStudijaDDL");
            zeton.oblikaStudija = Int32.Parse(oblikaStudijaDDL.SelectedValue);
            DropDownList prostaIzbiraDDL = (DropDownList)zetoniStudentaGV.Rows[e.RowIndex].FindControl("prostaIzbiraDDL");
            zeton.prostaIzbira = Int32.Parse(prostaIzbiraDDL.SelectedValue);
            DropDownList izkoriscenDDL = (DropDownList)zetoniStudentaGV.Rows[e.RowIndex].FindControl("izkoriscenDDL");
            zeton.Izkoriscen = Int32.Parse(izkoriscenDDL.SelectedValue) == 1 ? true : false;

            db.SaveChanges();
            zetoniStudentaGV.EditIndex = -1;
            GridViewIme_SelectedIndexChanged(null, null);
        }

        protected void zetoniStudentaGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idStudent = (int)GridViewIme.DataKeys[GridViewIme.SelectedIndex].Values["idStudent"];
            Student student = (Student)(from s in db.Student
                                        where s.idStudent == idStudent
                                        select s).FirstOrDefault();
            Zeton zeton = new Zeton();
            zeton.Student = student;
            DropDownList studijskiProgramDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newStudijskiProgramDDL");
            zeton.studijskiProgram = Int32.Parse(studijskiProgramDDL.SelectedValue);
            DropDownList letnikDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newLetnikDDL");
            zeton.letnik = Int32.Parse(letnikDDL.SelectedValue);
            DropDownList vrstaVpisaDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newVrstaVpisaDDL");
            zeton.vrstaVpisa = Int32.Parse(vrstaVpisaDDL.SelectedValue);
            DropDownList nacinStudijaDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newNacinStudijaDDL");
            zeton.nacinStudija = Int32.Parse(nacinStudijaDDL.SelectedValue);
            DropDownList oblikaStudijaDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newOblikaStudijaDDL");
            zeton.oblikaStudija = Int32.Parse(oblikaStudijaDDL.SelectedValue);
            DropDownList prostaIzbiraDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newProstaIzbiraDDL");
            zeton.prostaIzbira = Int32.Parse(prostaIzbiraDDL.SelectedValue);
            DropDownList izkoriscenDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newIzkoriscenDDL");
            zeton.Izkoriscen = Int32.Parse(izkoriscenDDL.SelectedValue) == 1 ? true : false;

            db.Zeton.Add(zeton);
            db.SaveChanges();
            zetoniStudentaGV.SelectedIndex = -1;
            GridViewIme_SelectedIndexChanged(null, null);
        }

        protected void napolniDodajZeton()
        {
            DropDownList studijskiProgramDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newStudijskiProgramDDL");

            var studijskiProgrami = (from sp in db.StudijskiProgram
                                     select sp).ToList();
            studijskiProgramDDL.Items.Clear();
            foreach (var item in studijskiProgrami)
            {
                studijskiProgramDDL.Items.Add(new ListItem(item.sifraKratka + " - " + item.naziv, item.idStudijskiProgram.ToString()));

            }
            studijskiProgramDDL.DataBind();

            //letnik
            DropDownList letnikDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newLetnikDDL");
            var letniki = (from l in db.Letnik
                           select l).ToList();

            letnikDDL.Items.Clear();
            foreach (var item in letniki)
            {
                letnikDDL.Items.Add(new ListItem(item.idLetnik.ToString(), item.idLetnik.ToString()));
            }
            letnikDDL.DataBind();

            //vrsta vpisa
            DropDownList vrstaVpisaDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newVrstaVpisaDDL");
            var vrsteVpisa = (from vv in db.VrstaVpisa
                              select vv).ToList();

            vrstaVpisaDDL.Items.Clear();
            foreach (var item in vrsteVpisa)
            {
                vrstaVpisaDDL.Items.Add(new ListItem(item.opisVpisa, item.idVrstaVpisa.ToString()));
            }
            vrstaVpisaDDL.DataBind();

            //način študija
            DropDownList nacinStudijaDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newNacinStudijaDDL");
            var naciniStudija = (from nc in db.NacinStudija
                                 select nc).ToList();

            nacinStudijaDDL.Items.Clear();
            foreach (var item in naciniStudija)
            {
                nacinStudijaDDL.Items.Add(new ListItem(item.opisNacina, item.idNacinStudija.ToString()));
            }
            nacinStudijaDDL.DataBind();

            //oblika študija
            DropDownList oblikaStudijaDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newOblikaStudijaDDL");
            var oblikeStudija = (from os in db.OblikaStudija
                                 select os).ToList();

            oblikaStudijaDDL.Items.Clear();
            foreach (var item in oblikeStudija)
            {
                oblikaStudijaDDL.Items.Add(new ListItem(item.opisOblike, item.idOblikaStudija.ToString()));
            }
            oblikaStudijaDDL.DataBind();

            //prosta izbira
            DropDownList prostaIzbiraDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newProstaIzbiraDDL");
            prostaIzbiraDDL.Items.Clear();
            prostaIzbiraDDL.Items.Add(new ListItem("NE", "0"));
            prostaIzbiraDDL.Items.Add(new ListItem("DA", "1"));
            prostaIzbiraDDL.DataBind();

            //izkoriščen
            DropDownList izkoriscenDDL = (DropDownList)zetoniStudentaGV.FooterRow.FindControl("newIzkoriscenDDL");
            izkoriscenDDL.Items.Clear();
            izkoriscenDDL.Items.Add(new ListItem("NE", "0"));
            izkoriscenDDL.Items.Add(new ListItem("DA", "1"));
            izkoriscenDDL.DataBind();
        }

        protected void napolniDodajNovZeton()
        {
            var studijskiProgrami = (from sp in db.StudijskiProgram
                                     select sp).ToList();

            novStudijskiProgramDDL.Items.Clear();
            foreach (var item in studijskiProgrami)
            {
                novStudijskiProgramDDL.Items.Add(new ListItem(item.sifraKratka + " - " + item.naziv, item.idStudijskiProgram.ToString()));
            }
            novStudijskiProgramDDL.DataBind();

            //letnik
            var letniki = (from l in db.Letnik
                           select l).ToList();

            novLetnikDDL.Items.Clear();
            foreach (var item in letniki)
            {
                novLetnikDDL.Items.Add(new ListItem(item.idLetnik.ToString(), item.idLetnik.ToString()));
            }
            novLetnikDDL.DataBind();

            //vrsta vpisa
            var vrsteVpisa = (from vv in db.VrstaVpisa
                              select vv).ToList();

            novVrstaVpisaDDL.Items.Clear();
            foreach (var item in vrsteVpisa)
            {
                novVrstaVpisaDDL.Items.Add(new ListItem(item.opisVpisa, item.idVrstaVpisa.ToString()));
            }
            novVrstaVpisaDDL.DataBind();

            //način študija
            var naciniStudija = (from nc in db.NacinStudija
                                 select nc).ToList();

            novNacinStudijaDDL.Items.Clear();
            foreach (var item in naciniStudija)
            {
                novNacinStudijaDDL.Items.Add(new ListItem(item.opisNacina, item.idNacinStudija.ToString()));
            }
            novNacinStudijaDDL.DataBind();

            //oblika študija
            var oblikeStudija = (from os in db.OblikaStudija
                                 select os).ToList();

            novOblikaStudijaDDL.Items.Clear();
            foreach (var item in oblikeStudija)
            {
                novOblikaStudijaDDL.Items.Add(new ListItem(item.opisOblike, item.idOblikaStudija.ToString()));
            }
            novOblikaStudijaDDL.DataBind();

            //prosta izbira
            novProstaIzbiraDDL.Items.Clear();
            novProstaIzbiraDDL.Items.Add(new ListItem("NE", "0"));
            novProstaIzbiraDDL.Items.Add(new ListItem("DA", "1"));
            novProstaIzbiraDDL.DataBind();

            //izkoriščen
            novIzkoriscenDDL.Items.Clear();
            novIzkoriscenDDL.Items.Add(new ListItem("NE", "0"));
            novIzkoriscenDDL.Items.Add(new ListItem("DA", "1"));
            novIzkoriscenDDL.DataBind();
        }

        protected void dodajNovZeton_Click(object sender, EventArgs e)
        {
            int idStudent = (int)GridViewIme.DataKeys[GridViewIme.SelectedIndex].Values["idStudent"];
            Student student = (Student)(from s in db.Student
                                        where s.idStudent == idStudent
                                        select s).FirstOrDefault();
            Zeton zeton = new Zeton();
            zeton.Student = student;
            zeton.studijskiProgram = Int32.Parse(novStudijskiProgramDDL.SelectedValue);
            zeton.letnik = Int32.Parse(novLetnikDDL.SelectedValue);
            zeton.vrstaVpisa = Int32.Parse(novVrstaVpisaDDL.SelectedValue);
            zeton.nacinStudija = Int32.Parse(novNacinStudijaDDL.SelectedValue);
            zeton.oblikaStudija = Int32.Parse(novOblikaStudijaDDL.SelectedValue);
            zeton.prostaIzbira = Int32.Parse(novProstaIzbiraDDL.SelectedValue);
            zeton.Izkoriscen = Int32.Parse(novIzkoriscenDDL.SelectedValue) == 1 ? true : false;

            db.Zeton.Add(zeton);
            db.SaveChanges();
            zetoniStudentaGV.SelectedIndex = -1;
            GridViewIme_SelectedIndexChanged(null, null);
            novZetonDiv.Style.Add("display", "none");
        }
    }
}