using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Web.Security;


namespace TPOZdejPaZares
{
    public partial class LoginForm : System.Web.UI.Page
    {
        private static Boolean bPersistentCookie = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
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

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        protected string GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;

        }
        protected void Button_login_Click(object sender, EventArgs e)
        {
            string userIp = GetUser_IP();
             t8_2015Entities db = new t8_2015Entities();
             var pass = TextBoxPassword.Text;
             var user = TextBoxUser.Text;
             var stude = db.Student.ToList();
             Debug.Write(stude.Count());
            foreach (var stud in stude) {
                var studUser = stud.mailStudenta;
                 if (hdn.Value == "9999") 
                 {
                     HttpCookie myCookie = new HttpCookie("banningCookie");
                     myCookie.Value = "yes";
                     myCookie.Expires = DateTime.Now.AddMinutes(30);
                     Debug.Write("THIS IS THE HDN VALUEEEEE:" + hdn.Value);
                     Response.Cookies.Add(myCookie);
                     Response.Redirect("BlokiranIp.aspx");
                 }

                 if (user == studUser && pass == stud.gesloStudenta) 
                 {
                     FormsAuthentication.Initialize();
                   //  Response.Write("Uspesna prijava");
                    // Response.Redirect("Default.aspx");
                   //  Debug.Write("Uspesna Prijava");
                     //New Code
                     string cookiestr;
                     FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, user, DateTime.Now,
               DateTime.Now.AddMinutes(30), bPersistentCookie, stud.Vloge_idVloge.ToString() );
                     cookiestr = FormsAuthentication.Encrypt(tkt);
                     HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                     if (bPersistentCookie)
                         ck.Expires = tkt.Expiration;
                     ck.Path = FormsAuthentication.FormsCookiePath;
                     Response.Cookies.Add(ck);

                     string strRedirect;
                     strRedirect = Request["ReturnUrl"];
                     if (strRedirect == null)
                         strRedirect = "default.aspx";
                     Session["uporabnikID"] = stud.idStudent;
                     Response.Redirect(strRedirect, true);
                     
                 }
                 else if (user == studUser) 
                 {
                     label_napacnoGeslo.Text = "Napacno geslo!";
                     hdn.Value = "" + (Convert.ToInt32(hdn.Value) + 1);
                     Debug.Write("Napacno geslo");
                 }
                 else if (pass == stud.imeStudenta)
                 {
                     label_napacniUser.Text = "Napacno uporabnisko ime!";
                     hdn.Value = "" + (Convert.ToInt32(hdn.Value) + 1);
                     Debug.Write("Napacno up. ime");
                 }
                 else if (pass != "" || user != "")
                 {
                     hdn.Value = "" + (Convert.ToInt32(hdn.Value) + 1);
                 }
             }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("PozabljenoGeslo.aspx");
        }
    }
}