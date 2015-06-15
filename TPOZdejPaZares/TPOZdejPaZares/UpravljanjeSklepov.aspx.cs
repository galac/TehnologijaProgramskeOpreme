using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace TPOZdejPaZares.Referent
{
    public partial class UpravljanjeSklepov : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();

            var studentje = (from stud in db.Student
                             select new
                             {
                                 idStudent = stud.idStudent,
                                 imeStudenta = stud.imeStudenta,
                                 priimekStudenta = stud.priimekStudenta,
                                 vpisnaStudenta = stud.vpisnaStudenta,
                             }).ToList();
            GridView1.DataSource =studentje;
            GridView1.DataBind();
            if (GridView1.SelectedIndex == -1)
            {
                Button2.Visible = false;
                GridView2.Visible = true;
            }
            else Button2.Visible = true;
        }

        protected void EntityDataSource1_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();
            var izbrani = GridView1.SelectedRow.Cells[3].Text;
            /* Debug.Write("ID STUDENTA.-.--------------------------------------------------------------------------------" + izbrani);
            */
            var sklepi = (from s in db.Sklep
                            where s.Student_idStudent == (int)GridView1.SelectedValue
                           select new {
                               idSklep = s.idSklep,
                               VsebinaSklepa = s.VsebinaSklepa,
                               Student_IdStudenta = s.Student_idStudent,
                                Student_idStudenta = s.Student_idStudent,
                                Organ = s.Organ,
                                DatumSprejetjaSklepa = s.DatumSprejetjaSklepa,
                                DatumVeljave = s.DatumVeljaveSklepa,
                                DatumVeljaveSklepa = s.DatumVeljaveSklepa,
                           
                           }).ToList();

            
            
             GridView2.DataSource = sklepi;
             GridView2.DataBind();
            GridView2.Visible = true;
        }

        protected void Sklepi_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(vsebinaInput.Text) && !string.IsNullOrWhiteSpace(datumInput.Text) && !string.IsNullOrWhiteSpace(organInput.Text) && GridView1.SelectedIndex!=-1)
            {
                t8_2015Entities db = new t8_2015Entities();
                var noviSklep = new Sklep();
                noviSklep.Student_IdStudenta = (int)GridView1.SelectedValue;
                noviSklep.VsebinaSklepa = vsebinaInput.Text;
                noviSklep.Organ = vsebinaInput.Text;
                noviSklep.DatumSprejetjaSklepa = Convert.ToDateTime(datumInput.Text);
                noviSklep.DatumVeljaveSklepa = Convert.ToDateTime(VeljavnostInput.Text);
                db.Sklep.Add(noviSklep);
                db.SaveChanges();
                Response.Redirect("UpravljanjeSklepov.aspx");
            }
        }

        protected void vsebinaInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}