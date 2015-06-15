using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;


namespace TPOZdejPaZares
{
    public partial class ZajemVpisnegaLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["uporabnikID"] == null)
                {
                    Server.Transfer("LoginForm.aspx", true);
                }

                t8_2015Entities db = new t8_2015Entities();

                int uporabnikID = (int)Session["uporabnikID"];
                Student uporabnik = (from s in db.Student
                                     where s.idStudent == uporabnikID
                                     select s).FirstOrDefault();

                Zeton zetonUporabnika = (from z in db.Zeton
                                     where z.Student_idStudent == uporabnikID
                                     select z).FirstOrDefault();

                if (zetonUporabnika != null)
                {
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
                    if (maxLetoVpisa == DateTime.Now.Year)
                    {
                        if (zetonUporabnika.Izkoriscen == true)
                        {
                            Server.Transfer("LoginForm.aspx", true);
                        }
                    }
                    
                }

                var drzave = (from d in db.Drzava
                              select d).ToList();

                var obcine = (from o in db.Obcina
                              select o).ToList();

               /* int mejaIzobrazbe = 0;
                if (uporabnik.Klasius_idKlasius <= 4)
                {
                    mejaIzobrazbe = 5;
                }
                else
                {
                    mejaIzobrazbe = 9;
                }*/
                var studijskiProgrami = (from s in db.StudijskiProgram
                                         where (uporabnik.Klasius.stopnjaStudija_IdStopnjaStudija%4 >= (s.StudijskiProgram_idStopnjaStudija-1)%4)
                                         select s as StudijskiProgram).ToList();

                var vrsteVpisa = (from v in db.VrstaVpisa
                              select v as VrstaVpisa).ToList();

                var naciniStudijev = (from n in db.NacinStudija
                                    select n as NacinStudija).ToList();

                var oblikeStudijev = (from o in db.OblikaStudija
                                     select o as OblikaStudija).ToList();


                for (int i = vrsteVpisa.Count - 1; i >= 0; i--)
                {
                    if (vrsteVpisa[i].idVrstaVpisa == 98)
                        vrsteVpisa.RemoveAt(i);
                }

                
                int stSemestrov = Convert.ToInt32(Math.Round(Convert.ToDecimal((studijskiProgrami[0].stSemestrov)/2)));

                int[] letniki = new int[stSemestrov];
                for (int i = 0; i < stSemestrov; i++)
                {
                    letniki[i] = i+1;
                }


                DropDownDrzavljanstvo.DataValueField = "idDrzava";
                DropDownDrzavljanstvo.DataTextField = "imeDrzave";

                DropDownDrzavaRojstva.DataValueField = "idDrzava";
                DropDownDrzavaRojstva.DataTextField = "imeDrzave";

                DropDownDrzavaStalna.DataValueField = "idDrzava";
                DropDownDrzavaStalna.DataTextField = "imeDrzave";

                DropDownDrzavaZacasna.DataValueField = "idDrzava";
                DropDownDrzavaZacasna.DataTextField = "imeDrzave";


                DropDownObcinaRojstva.DataValueField = "idObcina";
                DropDownObcinaRojstva.DataTextField = "imeObcine";

                DropDownObcinaStalna.DataValueField = "idObcina";
                DropDownObcinaStalna.DataTextField = "imeObcine";

                DropDownObcinaZacasna.DataValueField = "idObcina";
                DropDownObcinaZacasna.DataTextField = "imeObcine";


                DropDownStudijskiProgram.DataValueField = "idStudijskiProgram";
                DropDownStudijskiProgram.DataTextField = "naziv";

                DropDownVrstaVpisa.DataValueField = "idVrstaVpisa";
                DropDownVrstaVpisa.DataTextField = "opisVpisa";

                DropDownOblikaStudija.DataValueField = "idOblikaStudija";
                DropDownOblikaStudija.DataTextField = "opisOblike";

                DropDownNacinStudija.DataValueField = "idNacinStudija";
                DropDownNacinStudija.DataTextField = "opisNacina";

                DropDownDrzavljanstvo.DataSource = drzave;
                DropDownDrzavaRojstva.DataSource = drzave;
                DropDownDrzavaStalna.DataSource = drzave;
                DropDownDrzavaZacasna.DataSource = drzave;

                DropDownObcinaRojstva.DataSource = obcine;
                DropDownObcinaStalna.DataSource = obcine;
                DropDownObcinaZacasna.DataSource = obcine;

                DropDownStudijskiProgram.DataSource = studijskiProgrami;
                DropDownLetnik.DataSource = letniki;
                DropDownVrstaVpisa.DataSource = vrsteVpisa;
                DropDownOblikaStudija.DataSource = oblikeStudijev;
                DropDownNacinStudija.DataSource = naciniStudijev;

                DropDownDrzavljanstvo.DataBind();
                DropDownDrzavaRojstva.DataBind();
                DropDownDrzavaStalna.DataBind();
                DropDownDrzavaZacasna.DataBind();

                DropDownObcinaRojstva.DataBind();
                DropDownObcinaStalna.DataBind();
                DropDownObcinaZacasna.DataBind();

                DropDownStudijskiProgram.DataBind();
                DropDownLetnik.DataBind();
                DropDownVrstaVpisa.DataBind();
                DropDownOblikaStudija.DataBind();
                DropDownNacinStudija.DataBind();

                DropDownDrzavljanstvo.SelectedValue = "705";
                DropDownDrzavaRojstva.SelectedValue = "705";
                DropDownDrzavaStalna.SelectedValue = "705";
                DropDownDrzavaZacasna.SelectedValue = "705";


                

                if (uporabnik.Klasius != null)
                    TextBoxIzobrazba.Text = uporabnik.Klasius.Opis;
                
                String vpisnaOdStudenta;
                if (uporabnik.vpisnaStudenta != null)
                    vpisnaOdStudenta = uporabnik.vpisnaStudenta.ToString();
                else
                {
                    int generacijaId = 0;

                    String novaVpisna = "63"+DateTime.Now.Year.ToString().Substring(2,2);
                    while (true)
                    {
                        String generacijaIDString = generacijaId.ToString().PadLeft(4, '0');
                        Student studentNovi = (from s in db.Student
                                                .Where(s => s.vpisnaStudenta.ToString().Equals(novaVpisna + generacijaIDString))
                                               select s).FirstOrDefault();
                        
                        if (studentNovi == null)
                        {
                            vpisnaOdStudenta = novaVpisna + generacijaIDString;
                            uporabnik.vpisnaStudenta = Convert.ToInt32(vpisnaOdStudenta);
                            //db.Student.Attach(uporabnik);
                            //using ()
                            //{
                          
                            db.SaveChanges();
                            //}
                            break;
                        }
                        else
                        {
                            generacijaId++;
                        }
                    }

                }
                Student student = (from s in db.Student
                                .Where(s => s.vpisnaStudenta.ToString().Equals(vpisnaOdStudenta))
                                select s)
                                .FirstOrDefault();

                if (student != null)
                {
                    vpisna.Text = vpisnaOdStudenta;
                    if (student.DatumRojstva != null)
                        datumRojstva.Text = student.DatumRojstva.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    if (student.DavcnaStevilka != null)
                        davcna.Text = student.DavcnaStevilka.ToString();
                    /*if (student.Drzava != null)
                    {
                        DropDownDrzavaStalna.Items.FindByText(student.Drzava.imeDrzave).Selected = true;
                    }*/
                    if (student.Drzava_idDrzava != null)
                    {
                        //DropDownDrzavaStalna.Items.FindByValue(student.Drzava_idDrzava.ToString()).Selected = true;
                        DropDownDrzavaStalna.SelectedValue = student.Drzava_idDrzava.ToString();
                        DropDownDrzavaStalna_SelectedIndexChanged(new Object(), new EventArgs());
                    }
                    if (student.Drzava_idDrzavaDrzavljanstvo != null)
                        DropDownDrzavljanstvo.SelectedValue = student.Drzava_idDrzavaDrzavljanstvo.ToString();
                    if (student.Drzava_idDrzavaRojstva != null)
                    {
                        DropDownDrzavaRojstva.SelectedValue = student.Drzava_idDrzavaRojstva.ToString();
                        DropDownDrzavaRojstva_SelectedIndexChanged(new Object(), new EventArgs());
                    }
                    if (student.Drzava_idDrzavaZacasna != null)
                    {
                        DropDownDrzavaZacasna.SelectedValue = student.Drzava_idDrzavaZacasna.ToString();
                        DropDownDrzavaZacasna_SelectedIndexChanged(new Object(), new EventArgs());
                    }
                    if (student.EMSO != null)
                        emso.Text = student.EMSO;
                    if (student.imeStudenta != null)
                        ime.Text = student.imeStudenta;
                    if (student.priimekStudenta != null)
                        priimek.Text = student.priimekStudenta;
                    if (student.Spol != null)
                    {
                        if (student.Spol.Equals("moški") || student.Spol.Equals("moski") || student.Spol.Equals("m"))
                            radioMoski.Checked = true;
                        else if (student.Spol.Equals("ženski") || student.Spol.Equals("ž") || student.Spol.Equals("z"))
                            radioZenski.Checked = true;
                    }
                    if (student.Telefon != null)
                        telefon.Text = student.Telefon;
                    if (student.VrocanjeStalnoBivalisce != null)
                    {
                        if (student.VrocanjeStalnoBivalisce == 1)
                            radioStalno.Checked = true;
                        else
                            radioZacasno.Checked = true;
                    }
                    if (student.mailStudenta != null)
                        email.Text = student.mailStudenta;
                    if (student.KrajRojstva != null)
                        krajRojstva.Text = student.KrajRojstva;
                    if (student.Obcina_idObcinaRojstva != null)
                        DropDownObcinaRojstva.SelectedValue = student.Obcina_idObcinaRojstva.ToString();
                    if (student.NaslovZacasni != null)
                        zacasniNaslov.Text = student.NaslovZacasni;
                    if (student.Obcina_idObcinaZacasna != null)
                        DropDownObcinaZacasna.SelectedValue = student.Obcina_idObcinaZacasna.ToString();
                    if (student.Posta_idPostaZacasna != null)
                        zacasnaPosta.Text = student.Posta_idPostaZacasna.ToString();
                    if (student.Naslov != null)
                        stalniNaslov.Text = student.Naslov;
                    if (student.Obcina_idObcina != null)
                        DropDownObcinaStalna.SelectedValue = student.Obcina.ToString();
                    if (student.Posta_idPosta != null)
                        stalnaPosta.Text = student.Posta_idPosta.ToString();
                }
            }
        }

        protected void DropDownDrzavaRojstva_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DropDownDrzavaRojstva.SelectedValue.Equals("705"))
            {
                
                DropDownObcinaRojstva.Visible = false;
            }
            else
            {
                
                DropDownObcinaRojstva.Visible = true;
            }
        }

        protected void DropDownDrzavaStalna_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DropDownDrzavaStalna.SelectedValue.Equals("705"))
            {
                stalnaPosta.Visible = false;
                DropDownObcinaStalna.Visible = false;
            }
            else
            {
                stalnaPosta.Visible = true;
                DropDownObcinaStalna.Visible = true;
            }
        }

        protected void DropDownDrzavaZacasna_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!DropDownDrzavaZacasna.SelectedValue.Equals("705"))
            {
                DropDownObcinaZacasna.Visible = false;
                zacasnaPosta.Visible = false;
            }
            else
            {
                DropDownObcinaZacasna.Visible = true;
                zacasnaPosta.Visible = true;
            }
        }

        protected void buttonNadaljuj_Click(object sender, EventArgs e)
        {
            TextBoxNapake.Text = "";
            TextBoxNapake.Visible = true;
            //TextBoxNapake.Text += "wdwdw";
            Page.Validate();
            
            if (!Page.IsValid) 
                return;
            if (Regex.IsMatch(ime.Text, @"^[a-zA-Z]+$") == false)
            {
                return;
            }
            if (Regex.IsMatch(priimek.Text, @"^[a-zA-Z]+$") == false)
            {
                return;
            }
            if (emso.Text.Substring(0, 2).Equals(datumRojstva.Text.Substring(8, 2)) &&
                emso.Text.Substring(2, 2).Equals(datumRojstva.Text.Substring(5, 2)) &&
                emso.Text.Substring(4, 3).Equals(datumRojstva.Text.Substring(1, 3))) 
            {
                if (radioMoski.Checked && Convert.ToInt32(emso.Text.Substring(9, 3)) < 500 ||
                    radioZenski.Checked && Convert.ToInt32(emso.Text.Substring(9, 3)) >= 500)
                {
                    Char[] charStevila = emso.Text.ToCharArray();
                    int[] stevila = Array.ConvertAll(charStevila, c => (int)Char.GetNumericValue(c));
                    int sestevek = stevila[0] * 7 + stevila[1] * 6 + stevila[2] * 5 + stevila[3] * 4 +
                        stevila[4] * 3 + stevila[5] * 2 + stevila[6] * 7 + stevila[7] * 6 + stevila[8] * 5 +
                        stevila[9] * 4 + stevila[10] * 3 + stevila[11] * 2;
                    int ostanek = sestevek % 11;

                    if ((11 - ostanek) == stevila[12]) {
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            /*else
            {
                return;
            }*/
            t8_2015Entities db = new t8_2015Entities();

            int uporabnikID = (int)Session["uporabnikID"];
            Student uporabnik = (from s in db.Student
                                 where s.idStudent == uporabnikID
                                 select s).FirstOrDefault();
            
            if (uporabnik.Vpis.Count == 0 && Convert.ToInt32(DropDownLetnik.SelectedValue) != 1)
            {
                TextBoxNapake.Text += "Ker se vpisujete prvič, morate izbrati 1. letnik. ";
                return;
            }
            int maxLetoVpisa = 0;
            Vpis maxVpis = null;
            foreach (Vpis vpis in uporabnik.Vpis)
            {
                int letoVpisa = Convert.ToInt32(vpis.StudijskoLeto.Substring(0,4));
                if (letoVpisa > maxLetoVpisa)
                {
                    maxLetoVpisa = letoVpisa;
                    maxVpis = vpis;
                }
            }

            if (uporabnik.Vpis.Count != 0)
            {
                if (Convert.ToInt32(DropDownLetnik.SelectedValue) != 1)
                {
                    int prejsnjiLetnik = Convert.ToInt32(maxVpis.Letnik.letnik1);
                    if (Convert.ToInt32(DropDownLetnik.SelectedValue) > prejsnjiLetnik+1)
                    {
                        TextBoxNapake.Text += "Izbrali ste previsok letnik. ";
                        return;
                    }
                }
            }

            if (Convert.ToInt32(DropDownVrstaVpisa.SelectedValue) == 1)
            {
                if (uporabnik.Vpis.Count == 0 && Convert.ToInt32(DropDownLetnik.SelectedValue) != 1)
                {
                    TextBoxNapake.Text += "Ker se vpisujete prvič, morate izbrati 1. letnik. ";
                    return;
                }
            }
            if (Convert.ToInt32(DropDownVrstaVpisa.SelectedValue) == 2)
            {
                if (maxVpis.Letnik_idLetnik != Convert.ToInt32(DropDownLetnik.SelectedValue)
                    || maxVpis.StudijskiProgram_idStudijskiProgram != Convert.ToInt32(DropDownStudijskiProgram.SelectedValue))
                {
                    TextBoxNapake.Text += "Ob ponovnem vpisu morate meti izbran isti letnik in študijski program kot prej. ";
                    return;
                }
                int stLetnikov = Convert.ToInt32(Math.Round(Convert.ToDecimal((maxVpis.StudijskiProgram.stSemestrov) / 2)));
                if (stLetnikov == Convert.ToInt32(DropDownLetnik.SelectedValue)) {
                    TextBoxNapake.Text += "Ob ponovnem vpisu ne morete izbrati zadnjega letnika. ";
                }
            }
            if (Convert.ToInt32(DropDownVrstaVpisa.SelectedValue) == 5) {
                int stLetnikov = Convert.ToInt32(Math.Round(Convert.ToDecimal((maxVpis.StudijskiProgram.stSemestrov) / 2)));
                if (stLetnikov == Convert.ToInt32(DropDownLetnik.SelectedValue)) {
                    TextBoxNapake.Text += "Ob vpisu po merilih za prehode v višji letnik ne morete izbrati zadnjega letnika. ";
                }
            }

            Vpis novVpis = new Vpis();
            novVpis.StudijskiProgram_idStudijskiProgram = Convert.ToInt32(DropDownStudijskiProgram.SelectedValue);
            novVpis.Letnik_idLetnik = Convert.ToInt32(DropDownLetnik.SelectedValue);
            novVpis.NacinStudija_idNacinStudija = Convert.ToInt32(DropDownNacinStudija.SelectedValue);
            novVpis.OblikaStudija_idOblikaStudija = Convert.ToInt32(DropDownOblikaStudija.SelectedValue);
            novVpis.Student_idStudent1 = uporabnikID;
            novVpis.StudijskoLeto = DateTime.Now.Year + "/" + (DateTime.Now.Year + 1);
            novVpis.VrstaVpisa_idVrstaVpisa = Convert.ToInt32(DropDownVrstaVpisa.SelectedValue);
            novVpis.Potrjen = 0;

            Zeton novZeton = new Zeton();
            novZeton.Izkoriscen = true;
            novZeton.idZeton = novVpis.idVpis;
            novZeton.Student_idStudent = uporabnikID;

            db.Vpis.Add(novVpis);
            db.Zeton.Add(novZeton);

            if (emso.Text != "")
                uporabnik.EMSO = emso.Text;
            if (davcna.Text != "")
                uporabnik.DavcnaStevilka = Convert.ToInt64(davcna.Text);
            if (ime.Text != "")
                uporabnik.imeStudenta = ime.Text;
            if (priimek.Text != "")
                uporabnik.priimekStudenta = priimek.Text;
            if (radioMoski.Checked)
                uporabnik.Spol = "m";
            else
                uporabnik.Spol = "ž";
            if (email.Text != "")
                uporabnik.mailStudenta = email.Text;
            if (telefon.Text != "")
                uporabnik.Telefon = telefon.Text;
            uporabnik.Drzava_idDrzava = Convert.ToInt32(DropDownDrzavljanstvo.SelectedValue);
            if (datumRojstva.Text != "")
                
                uporabnik.DatumRojstva = Convert.ToDateTime(datumRojstva.Text);
            if (krajRojstva.Text != "")
                uporabnik.KrajRojstva = krajRojstva.Text;
            uporabnik.Obcina_idObcinaRojstva = Convert.ToInt32(DropDownObcinaRojstva.SelectedValue);
            uporabnik.Drzava_idDrzavaRojstva = Convert.ToInt32(DropDownDrzavaRojstva.SelectedValue);
            if (radioStalno.Checked)
                uporabnik.VrocanjeStalnoBivalisce = 1;
            else
                uporabnik.VrocanjeStalnoBivalisce = 0;
            if (stalniNaslov.Text != "")
                uporabnik.Naslov = stalniNaslov.Text;
            uporabnik.Obcina_idObcina = Convert.ToInt32(DropDownObcinaStalna.SelectedValue);
            uporabnik.Drzava_idDrzava = Convert.ToInt32(DropDownDrzavaStalna.SelectedValue);
            if (stalnaPosta.Text != "")
                uporabnik.Posta_idPosta = Convert.ToInt32(stalnaPosta.Text);
            if (zacasniNaslov.Text != "")
                uporabnik.NaslovZacasni = zacasniNaslov.Text;
            uporabnik.Obcina_idObcinaZacasna = Convert.ToInt32(DropDownObcinaZacasna.SelectedValue);
            uporabnik.Drzava_idDrzavaZacasna = Convert.ToInt32(DropDownDrzavaZacasna.SelectedValue);
            if (zacasnaPosta.Text != "")
                uporabnik.Posta_idPostaZacasna = Convert.ToInt32(zacasnaPosta.Text);

            Session["vpisId"]=novVpis.idVpis;

            db.SaveChanges();
            Server.Transfer("IzbiraPredmetov.aspx", true);
        }

        protected void DropDownStudijskiProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            t8_2015Entities db = new t8_2015Entities();

            int izbran = Convert.ToInt32(DropDownStudijskiProgram.SelectedValue);
            var studijskiProgram = (from s in db.StudijskiProgram
                                    where s.StudijskiProgram_idStopnjaStudija == izbran
                                    select s as StudijskiProgram).FirstOrDefault();

            int stLetnikov = Convert.ToInt32(Math.Round(Convert.ToDecimal((studijskiProgram.stSemestrov) / 2)));

            int[] letniki = new int[stLetnikov];
            for (int i = 0; i < stLetnikov; i++)
            {
                letniki[i] = i + 1;
            }

            DropDownLetnik.DataSource = letniki;
            DropDownLetnik.DataBind();
        }

        
    }
}