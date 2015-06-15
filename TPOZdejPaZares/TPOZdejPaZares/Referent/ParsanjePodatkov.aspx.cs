using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares
{
    public partial class ParsanjePodatkov : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Parse_Click(object sender, EventArgs e)
        {
            LblError.Text = "";
            LblInfo.Text = "";

            if (!FileForParseUpload.HasFile)
            {
                LblError.Text = "Nobena datoteka ni izbrana!";
                return;
            }

            // process file
            Stream theStream = FileForParseUpload.PostedFile.InputStream;
            t8_2015Entities db = new t8_2015Entities();
            int stevecSucces = 0;
            int stevecFail = 0;            
               
            string line;
            int idS = 13;
            using (StreamReader sr = new StreamReader(theStream, Encoding.UTF8, true))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length != 127)
                        continue;

                    string name = line.Substring(0, 30).Trim();
                    string surname = line.Substring(30, 30).Trim();
                    string course = line.Substring(60, 7).Trim();
                    string email = line.Substring(67, 60).Trim();                      

                    idS++;
                        
                    int count = (from s in db.Student
                                    where s.mailStudenta == email
                                    select s).Count();
                    if (count > 0)
                    {
                        stevecFail++;
                        continue;
                    }
                    stevecSucces++;

                    Prijava prijava = new Prijava();
                    prijava.imeStudenta = name;
                    prijava.priimekStudenta = surname;
                    prijava.mailStudent = email;
                    prijava.gesloStudent = "test123";

                    Student student = new Student();
                    student.imeStudenta = name;
                    student.priimekStudenta = surname;
                    student.mailStudenta = email;
                    student.gesloStudenta = "test123";
                    student.Klasius = db.Klasius.Find(15002);
                    student.Vloge = db.Vloge.Find(1);

                    db.Prijava.Add(prijava);
                    db.Student.Add(student);
                    db.SaveChanges();
                }
            }
            if (stevecSucces > 0)
                LblInfo.Text = "Vnesli ste " + stevecSucces + " novih prijav.";
            if (stevecFail > 0)
                LblError.Text = "Zaradi podvojenih vnosov ni bilo vnešenih " + stevecFail + " prijav!";
        }
    }
}