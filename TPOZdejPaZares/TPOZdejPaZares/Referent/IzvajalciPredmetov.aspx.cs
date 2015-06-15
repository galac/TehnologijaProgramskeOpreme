using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares.Referent
{
    public partial class IzvajalciPredmetov : System.Web.UI.Page
    {
        t8_2015Entities db = new t8_2015Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uporabnikID"] == null)
                Response.Redirect("~/LoginForm.aspx");
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

                GV_izvajalci.SelectedIndex = -1;
                GV_izvajalci.Visible = false;
                header41.Visible = false;
            }
        }        

        protected void DDL_Years_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_izvajalci.SelectedIndex = -1;
            header41.Visible = false;

            if (DDL_Years.SelectedValue == "XXXX/XXXX")
            {
                DDL_SubjectList.Items.Clear();
                DDL_SubjectList.Items.Add(new System.Web.UI.WebControls.ListItem("nedoločeno", "XXXXX"));
                return;
            }

            int studijskoLetoID = Convert.ToInt32(DDL_Years.SelectedValue);

            var relevantniPredmeti = (from i in db.IzvedbaPredmeta
                                        where i.StudijskoLeto_idStudijskoLeto == studijskoLetoID
                                        select i).ToList();

            DDL_SubjectList.Items.Clear();
            foreach (var item in relevantniPredmeti)
            {
                DDL_SubjectList.Items.Add(new ListItem(item.PredmetStudijskegaPrograma.Predmet.imePredmeta, item.idIzvedbaPredmeta.ToString()));
            }

            SortDDL_Predmeti(ref DDL_SubjectList);
            DDL_SubjectList.DataBind();
            //var nedolocen = new { DataTextField = "nedoločeno", DataValueField = "XXXXX" };
            //relevantniPredmeti.Insert(0, nedolocen);

            //DDL_SubjectList.DataSource = relevantniPredmeti;
            //DDL_SubjectList.DataTextField = "DataTextField";
            //DDL_SubjectList.DataValueField = "DataValueField";
            //DDL_SubjectList.DataBind();
            DDL_SubjectList.SelectedIndex = 0;
        }

        protected void DDL_SubjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_izvajalci.SelectedIndex = -1;
            GV_izvajalci.Visible = false;
            header41.Visible = false;
            DDL_seznam_profesorjev.Visible = false;
            dodajIzvajalcaBTN.Visible = false;

            if (DDL_SubjectList.SelectedValue == "0") {
                return;
            }
                
            int predmetID = Convert.ToInt32(DDL_SubjectList.SelectedValue);
            int studijskoLetoID = Convert.ToInt32(DDL_Years.SelectedValue);
            IzvedbaPredmeta izvedba = (IzvedbaPredmeta)(from ip in db.IzvedbaPredmeta
                                                        where ip.idIzvedbaPredmeta == predmetID
                                                        select ip).FirstOrDefault();

            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[3] {
                new DataColumn("ime", typeof(String)),
                new DataColumn("priimek", typeof(String)),
                new DataColumn("idProfesor", typeof(int))
            });

            dataTable.Rows.Add(izvedba.Profesor.imeProfesorja, izvedba.Profesor.priimekProfesorja, izvedba.Profesor_idProfesor);
            if (izvedba.Profesor1 != null)
                {
                    dataTable.Rows.Add(izvedba.Profesor1.imeProfesorja, izvedba.Profesor1.priimekProfesorja, izvedba.Profesor_idProfesor1);
                }
            if (izvedba.Profesor2 != null)
                {
                    dataTable.Rows.Add(izvedba.Profesor2.imeProfesorja, izvedba.Profesor2.priimekProfesorja, izvedba.Profesor_idProfesor2);
                }
            else
            {
                var profesorji = (from p in db.Profesor
                                  where p.idProfesor != izvedba.Profesor_idProfesor 
                                  select p).ToList();
                if (izvedba.Profesor2 != null) 
                {
                    profesorji = (from p in db.Profesor
                                  where p.idProfesor != izvedba.Profesor_idProfesor &&
                                        p.idProfesor != izvedba.Profesor_idProfesor1 &&
                                        p.idProfesor != izvedba.Profesor_idProfesor2
                                  select p).ToList();
                }
                else if (izvedba.Profesor1 != null)
                {
                    profesorji = (from p in db.Profesor
                                  where p.idProfesor != izvedba.Profesor_idProfesor &&
                                        p.idProfesor != izvedba.Profesor_idProfesor1
                                  select p).ToList();
                }
                DDL_seznam_profesorjev.Items.Clear();
                foreach (var p in profesorji)
                {
                    DDL_seznam_profesorjev.Items.Add(new ListItem(p.imeProfesorja + " " + p.priimekProfesorja, p.idProfesor.ToString()));
                }
                SortDDL_Profesor(ref DDL_seznam_profesorjev);
                DDL_seznam_profesorjev.DataBind();
                DDL_seznam_profesorjev.Visible = true;
                dodajIzvajalcaBTN.Visible = true;
            }

            GV_izvajalci.DataSource = dataTable;
            GV_izvajalci.Visible = true;
            GV_izvajalci.DataBind();
        }

        protected void GV_izvajalci_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int st = ((DataTable)GV_izvajalci.DataSource).Rows.Count;
                if (e.Row.RowIndex == 0 && st == 1)
                {
                    e.Row.Cells[3].Controls.Clear();
                    //e.Row.Cells[4].Controls.Clear();
                }
                GV_izvajalci.Columns[2].Visible = false;
            }
        }

        protected void dodajIzvajalcaBTN_Click(object sender, EventArgs e)
        {
            int idProfesor = Convert.ToInt32(DDL_seznam_profesorjev.SelectedValue);
            int predmetID = Convert.ToInt32(DDL_SubjectList.SelectedValue);
            IzvedbaPredmeta izvedba = (IzvedbaPredmeta)(from ip in db.IzvedbaPredmeta
                                                        where ip.idIzvedbaPredmeta == predmetID
                                                        select ip).FirstOrDefault();
            Profesor profesor = (Profesor)(from p in db.Profesor
                                           where p.idProfesor == idProfesor
                                           select p).FirstOrDefault();
            if (izvedba.Profesor1 == null)
            {
                izvedba.Profesor1 = profesor;
            }
            else if (izvedba.Profesor2 == null)
            {
                izvedba.Profesor2 = profesor;
            }

            db.SaveChanges();
            DDL_SubjectList_SelectedIndexChanged(null, null);
        }
        protected void GV_izvajalci_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)GV_izvajalci.DataKeys[e.RowIndex].Values["idProfesor"];
            int predmetID = Convert.ToInt32(DDL_SubjectList.SelectedValue);
            IzvedbaPredmeta izvedba = (IzvedbaPredmeta)(from ip in db.IzvedbaPredmeta
                                                        where ip.idIzvedbaPredmeta == predmetID
                                                        select ip).FirstOrDefault();

            Profesor profesor = (Profesor)(from p in db.Profesor
                                    where p.idProfesor == id
                                    select p).FirstOrDefault();
            if (izvedba.Profesor == profesor)
            {
                izvedba.Profesor = null;
                izvedba.Profesor = izvedba.Profesor1;
                izvedba.Profesor1 = null;
                if (izvedba.Profesor2 != null)
                {
                    izvedba.Profesor1 = izvedba.Profesor2;
                    izvedba.Profesor2 = null;
                }
            }
            else if (izvedba.Profesor1 == profesor)
            {
                izvedba.Profesor1 = null;
                izvedba.Profesor1 = izvedba.Profesor2;
                izvedba.Profesor2 = null;
            }
            else if (izvedba.Profesor2 == profesor)
            {
                izvedba.Profesor2 = null;
            }

            db.SaveChanges();
            DDL_SubjectList_SelectedIndexChanged(null, null);
        }

        private void SortDDL_Predmeti(ref DropDownList objDDL)
        {
            ArrayList textList = new ArrayList();
            ArrayList valueList = new ArrayList();

            foreach (ListItem li in objDDL.Items)
            {
                textList.Add(li.Text);
            }

            textList.Sort();

            foreach (object item in textList)
            {
                string value = objDDL.Items.FindByText(item.ToString()).Value;
                valueList.Add(value);
            }
            objDDL.Items.Clear();

            //dodaj številko predmeta in jo shrani v value
            var predmeti = (from ip in db.IzvedbaPredmeta select ip).Distinct().ToList();
            for (int i = 0; i < textList.Count; i++)
            {
                int idIp = Int32.Parse(valueList[i].ToString());
                foreach (var item in predmeti)
                {
                    if (item.idIzvedbaPredmeta == idIp)
                    {
                        ListItem objItem = new ListItem(item.PredmetStudijskegaPrograma.Predmet.idPredmet + " - " + textList[i].ToString(), valueList[i].ToString());
                        objDDL.Items.Add(objItem);
                    }
                }
            }
            objDDL.Items.Insert(0, new ListItem("Izberi predmet", "0"));
        }

        private void SortDDL_Profesor(ref DropDownList objDDL)
        {
            ArrayList textList = new ArrayList();
            ArrayList valueList = new ArrayList();


            foreach (ListItem li in objDDL.Items)
            {
                textList.Add(li.Text);
            }

            textList.Sort();

            foreach (object item in textList)
            {
                string value = objDDL.Items.FindByText(item.ToString()).Value;
                valueList.Add(value);
            }
            objDDL.Items.Clear();

            for (int i = 0; i < textList.Count; i++)
            {
                ListItem objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
                objDDL.Items.Add(objItem);
            }
            objDDL.Items.Insert(0, new ListItem("Izberi", "0"));
        }
    }
}