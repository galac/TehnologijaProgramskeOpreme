using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Net.Mail;

namespace TPOZdejPaZares
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookies = Request.Cookies["banningCookie"];
            if (cookies != null)
            {
                Debug.Write("Found a cookie!");
                if (cookies.Value == "yes")
                {
                    Response.Redirect("BlokiranIp.aspx");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();
             var user = TextBox_User.Text;
            var ime = TextBox_ime.Text;
            var priimek = TextBox_priimek.Text;
             var stude = db.Student.ToList();
             Debug.Write(stude.Count());
             if (hdn.Value == "3")
             {
                 HttpCookie myCookie = new HttpCookie("banningCookie");
                 myCookie.Value = "yes";
                 myCookie.Expires = DateTime.Now.AddMinutes(30);
                 Debug.Write("THIS IS THE HDN VALUEEEEE:" + hdn.Value);
                 Response.Cookies.Add(myCookie);
                 Response.Redirect("BlokiranIp.aspx");
             }
             foreach (var stud in stude)
             {
                     var studUser = Convert.ToString(stud.idStudent);
                     if (user == studUser && ime == stud.imeStudenta && priimek == stud.priimekStudenta)
                     {

                         MailMessage msg = new MailMessage();
                         string novPas = System.Web.Security.Membership.GeneratePassword(8, 2);
                         stud.gesloStudenta = novPas;
                         db.SaveChanges();
                         msg.To.Add(stud.mailStudenta);
                         msg.From = new MailAddress("tpoprojekt.test@gmail.com", "Hello", System.Text.Encoding.UTF8);
                         msg.Subject = "Novo geslo";
                         msg.SubjectEncoding = System.Text.Encoding.UTF8;
                         msg.Body = "Vase novo geslo je: " + novPas;
                         msg.BodyEncoding = System.Text.Encoding.UTF8;
                         msg.IsBodyHtml = true;
                         msg.Priority = MailPriority.High;
                         SmtpClient client = new SmtpClient();
                         client.Credentials = new System.Net.NetworkCredential("tpoprojekt.test@gmail.com", "tpotpo1!");
                         client.Port = 587;
                         client.Host = "smtp.gmail.com";
                         client.EnableSsl = true;
                         try
                         {
                             client.Send(msg);
                             //    Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
                         }
                         catch (Exception ex)
                         {
                             Exception ex2 = ex;
                             string errorMessage = string.Empty;
                             while (ex2 != null)
                             {
                                 errorMessage += ex2.ToString();
                                 ex2 = ex2.InnerException;
                             }
                             // Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
                         }

                     }
                     else
                     { 
                         label_pozabljeno.Text = "Tega uporabnika ni v sistemu";
                         hdn.Value = "" + (Convert.ToInt32(hdn.Value) + 1);
                     }
             }
        }
    }
}