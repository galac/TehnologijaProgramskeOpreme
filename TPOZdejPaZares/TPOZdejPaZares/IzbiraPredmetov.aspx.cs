using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares
{
    public partial class IzbiraPredmetov : System.Web.UI.Page
    {
        t8_2015Entities db = new t8_2015Entities();
        int idVpis = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                idVpis = 0;
                if (Session["vpisId"] != null) {
                    idVpis = (int)Session["vpisId"];
                    ViewState.Add("idVpis", idVpis);
                }
                else if (Request.QueryString["idVpis"] != null)
                {
                    idVpis = Int32.Parse(Request.QueryString["idVpis"]);
                    ViewState.Add("idVpis", idVpis);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }

                Vpis vpis = (Vpis)(from v in db.Vpis
                                   where v.idVpis == idVpis
                                   select v).FirstOrDefault();

                StudijskiProgram studijskiProgram = (StudijskiProgram)(from sp in db.StudijskiProgram
                                                                       where sp.idStudijskiProgram == vpis.StudijskiProgram_idStudijskiProgram
                                                                       select sp).FirstOrDefault();
                if (vpis.Letnik.letnik1 == 1)
                {
                    var obvezniPredmetiPrviLetnik = (from ip in db.IzvedbaPredmeta
                                                     where ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 1 &&
                                                           ip.PredmetStudijskegaPrograma.StudijskiProgram_idStudijskiProgram1 == studijskiProgram.idStudijskiProgram &&
                                                           ip.StudijskoLeto.studijskoLeto1.Equals(vpis.StudijskoLeto)
                                                     select ip).Distinct().ToList();

                    foreach (IzvedbaPredmeta item in obvezniPredmetiPrviLetnik)
                    {
                        VpisaniPredmet vp = new VpisaniPredmet();
                        vp.IzvedbaPredmeta_idIzvedbaPredmeta = item.idIzvedbaPredmeta;
                        vp.Vpis_idVpis = vpis.idVpis;
                        db.VpisaniPredmet.Add(vp);
                    }
                    db.SaveChanges();
                    predmetnikPrviLetnik.Style.Add("display", "block");
                    predmetnikPrviLetnikGV.Visible = true;
                    var seznamPredmetov = (from vp in db.VpisaniPredmet
                                           where vp.Vpis_idVpis == idVpis
                                           select vp).Distinct().ToList();

                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[9] { 
                                        new DataColumn("SifraPredmeta", typeof(int)),
                                        new DataColumn("Predmet", typeof(String)),
                                        new DataColumn("Kreditne", typeof(int)),
                                        new DataColumn("Letnik", typeof(int)),
                                        new DataColumn("Del predmeta", typeof(String)),
                                        new DataColumn("Ocena", typeof(String)),
                                        new DataColumn("IdPredmeta", typeof(int)),
                                        new DataColumn("IdVpisanegaPredmeta", typeof(int)),
                                        new DataColumn("SestavniDelPred", typeof(int)) });
                    vpisniListBTN.Visible = true;

                    foreach (var item in seznamPredmetov)
                    {
                        Predmet predmet = (Predmet)(from p in db.Predmet where p.idPredmet == item.IzvedbaPredmeta.PredmetStudijskegaPrograma.Predmet_idPredmet1 select p).First();
                        int letnik = (int)(from l in db.Letnik where l.idLetnik == item.Vpis.Letnik_idLetnik select l.letnik1).First();
                        SestavniDelPred sdp = (SestavniDelPred)(from x in db.SestavniDelPred where x.idSestavniDelPred == item.IzvedbaPredmeta.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred select x).First();
                        String sestavniDelPredString = sdp.opisSestavnegaDela;
                        if (sdp.jeModul == true)
                        {
                            sestavniDelPredString += " (Modul)";
                        }
                        String ocena = "";
                        if (item.ocena != null) ocena = item.ocena.ToString();
                        dt.Rows.Add(predmet.idPredmet, predmet.imePredmeta, predmet.kreditneTocke, letnik, sestavniDelPredString, ocena,
                            item.IzvedbaPredmeta_idIzvedbaPredmeta, item.idVpisaniPredmet, item.IzvedbaPredmeta.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred);

                    }
                    predmetnikPrviLetnikGV.DataSource = dt;
                    predmetnikPrviLetnikGV.DataBind();
                }
                else if (vpis.Letnik.letnik1 == 2)
                {
                    drugiLetnikDiv.Style.Add("display", "block");

                    //napolnimo strokovno izbirne predmete
                    var strokovniIzbirni = (from ip in db.IzvedbaPredmeta 
                                            where ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 2 &&
                                                  ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 1 &&
                                                  ip.PredmetStudijskegaPrograma.StudijskiProgram_idStudijskiProgram1 == studijskiProgram.idStudijskiProgram &&
                                                  ip.StudijskoLeto_idStudijskoLeto == 11
                                            select ip).Distinct().ToList();

                    //var strokovniIzbirni = (from psp in db.PredmetStudijskegaPrograma
                    //                        where psp.Letnik_idLetnik == 2 &&
                    //                              psp.SestavniDelPred_idSestavniDelPred == 1
                    //                        select psp).ToList();
                    foreach (var item in strokovniIzbirni)
                    {
                        drugiStrokovniIzbirniDDL.Items.Add(new ListItem(item.PredmetStudijskegaPrograma.Predmet.imePredmeta, item.idIzvedbaPredmeta.ToString()));
                    }
                    SortDDL_Predmeti(ref drugiStrokovniIzbirniDDL);
                    drugiStrokovniIzbirniDDL.DataBind();

                    //napolnimo prosto izbirne predmete z prosto izbirnimi in strokovno izbirnimi predmeti 2. letnika. Strokovni predmet, ki ga je študent izbral, se iz liste prosto izbirninh izloči ob selectedchange na strokovno izbrinem
                    var prostoIzbirni = (from ip in db.IzvedbaPredmeta
                                         where ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 2 &&
                                               ip.PredmetStudijskegaPrograma.StudijskiProgram_idStudijskiProgram1 == studijskiProgram.idStudijskiProgram &&
                                               ip.StudijskoLeto_idStudijskoLeto == 11 &&
                                               (
                                                   ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred  == 1 ||
                                                   ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 2
                                               )
                                         select ip).Distinct().ToList();

                    foreach (var item in prostoIzbirni)
                    {
                        drugiProstoIzbirniDDL.Items.Add(new ListItem(item.PredmetStudijskegaPrograma.Predmet.imePredmeta, item.idIzvedbaPredmeta.ToString()));
                    }
                    SortDDL_Predmeti(ref drugiProstoIzbirniDDL);
                    drugiProstoIzbirniDDL.DataBind();
                }
                //vpis v 3. letnik
                else if (vpis.Letnik.letnik1 == 3)
                {
                    double povp = povprecnaOcena((int)vpis.Student.vpisnaStudenta);
                    Zeton z = (Zeton)(from zeton in db.Zeton
                                      where zeton.Vpis_idVpis == idVpis
                                      select zeton).FirstOrDefault();
                    if (povprecnaOcena((int)vpis.Student.vpisnaStudenta) >= 8.5 || z.prostaIzbira == 1)
                    {
                        tretjiLetnikPovprecjeDiv.Style.Add("display", "block");
                        var predmeti = (from ip in db.IzvedbaPredmeta
                                        where  ip.PredmetStudijskegaPrograma.StudijskiProgram_idStudijskiProgram1 ==    studijskiProgram.idStudijskiProgram &&
                                                ip.StudijskoLeto_idStudijskoLeto == 11 &&
                                                (
                                                    (
                                                    //odstrani prosto izbirne predmete v 2. letniku
                                                        ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 2 &&
                                                        ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred != 2
                                                   ) ||
                                                    ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 1 ||
                                                    //dodaj obvezne predmete 3. letnika
                                                    ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 3
                                                )
                                        select ip).Distinct().ToList();

                        int vpisnaStudenta = (int)vpis.Student.vpisnaStudenta;
                        //zberemo že narejene predmete študenta
                        var narejeniPredmeti = (from vp in db.VpisaniPredmet
                                                //spremeni glede na podatke pridobljene iz viewstate al kakorkoli že bo
                                                where vp.Vpis.Student.vpisnaStudenta == vpisnaStudenta
                                                select vp).Distinct().ToList();

                        List<IzvedbaPredmeta> koncniSeznamPredmetov = new List<IzvedbaPredmeta>();
                        //odstrani že narejene oz. vpisane predmete študenta
                        foreach (var item in predmeti)
                        {
                            bool equal = false;
                            foreach (var item2 in narejeniPredmeti)
                            {
                                if (item.Equals(item2.IzvedbaPredmeta))
                                {
                                    equal = true;
                                    break;
                                }
                            }
                            if (!equal)
                            {
                                koncniSeznamPredmetov.Add(item);
                            }
                        }
                        foreach (var item in koncniSeznamPredmetov)
                        {
                            tretjiPovprecjeCBL.Items.Add(new ListItem(item.PredmetStudijskegaPrograma.Predmet.imePredmeta, item.idIzvedbaPredmeta.ToString()));
                        }
                        SortCBL_Predmeti(ref tretjiPovprecjeCBL);
                        tretjiPovprecjeCBL.DataBind();
                        var obvezniPredmeti = (from ip in db.IzvedbaPredmeta
                                               where ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 3 &&
                                                     ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 3 &&
                                                     ip.PredmetStudijskegaPrograma.StudijskiProgram_idStudijskiProgram1 == studijskiProgram.idStudijskiProgram &&
                                                     ip.StudijskoLeto_idStudijskoLeto == 11
                                               select ip).Distinct().ToList();
                        foreach (ListItem item in tretjiPovprecjeCBL.Items)
                        {
                            foreach (IzvedbaPredmeta p in obvezniPredmeti)
                            {
                                if (Int32.Parse(item.Value) == p.idIzvedbaPredmeta)
                                {
                                    item.Selected = true;
                                    item.Enabled = false;
                                }
                            }
                        }
                        kTockeLB.Text = "Imate 18 kreditnih točk od 60 potrebnih";
                    }
                    else
                    {
                        tretjiLetnikDiv.Style.Add("display", "block");
                        var moduli = (from sdp in db.SestavniDelPred
                                      where sdp.jeModul == true
                                      select sdp).ToList();
                        foreach (var item in moduli)
                        {
                            tretjiPrviModulDDL.Items.Add(new ListItem(item.opisSestavnegaDela.ToString(), item.idSestavniDelPred.ToString()));
                            tretjiDrugiModulDDL.Items.Add(new ListItem(item.opisSestavnegaDela.ToString(), item.idSestavniDelPred.ToString()));
                        }
                        SortDDL_Moduli(ref tretjiPrviModulDDL);
                        SortDDL_Moduli(ref tretjiDrugiModulDDL);
                        tretjiPrviModulDDL.DataBind();
                        tretjiDrugiModulDDL.DataBind();
                        tretjiProstoIzbirniDDL_SelectedIndexChanged(null, null);
                    }
                }
            }
        }

        //izračuna povprečno oceno študenta glede na vpisane predmete (VpisaniPredmet), torej neodvisno od 
        //št. ponavljanj letnika. Računa samo za prvi in drugi letnik in vrne izračunano povprečno oceno.
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

            return (povpPoLetniku[0] + povpPoLetniku[1]) / 2;
        }

        protected void potrdiDrugiButton_Click(object sender, EventArgs e)
        {
            Boolean check = true;
            int idStrokovniPredmet = Int32.Parse(drugiStrokovniIzbirniDDL.SelectedValue);
            int idProstiPredmet = Int32.Parse(drugiProstoIzbirniDDL.SelectedValue);
            idVpis = (int)ViewState["idVpis"];
            Vpis vpis = (Vpis)(from v in db.Vpis
                               where v.idVpis == idVpis
                               select v).FirstOrDefault();

            if (idStrokovniPredmet != 0)
            {
                VpisaniPredmet vp = new VpisaniPredmet();
                vp.IzvedbaPredmeta_idIzvedbaPredmeta = idStrokovniPredmet;
                //SPREMENITI OZ. PRILAGODITI ID VPISA!!!!
                vp.Vpis_idVpis = idVpis;
                db.VpisaniPredmet.Add(vp);
            }
            else
            {
                drugiStrokovniIzbirniErrorLB.Visible = true;
                check = false;
            }

            if (idProstiPredmet != 0)
            {
                VpisaniPredmet vp = new VpisaniPredmet();
                vp.IzvedbaPredmeta_idIzvedbaPredmeta = idProstiPredmet;
                //SPREMENITI OZ. PRILAGODITI ID VPISA!!!!
                vp.Vpis_idVpis = idVpis;
                db.VpisaniPredmet.Add(vp);
            }
            else
            {
                drugiProstoIzbirniErrorLB.Visible = true;
                check = false;
            }


            if (drugiProstoIzbirniDDL_3kt.Visible == true)
            {
                if (!drugiProstoIzbirniDDL_3kt.SelectedValue.Equals("0"))
                {
                    int idProstiPredmet_3kt = Int32.Parse(drugiProstoIzbirniDDL_3kt.SelectedValue);
                    VpisaniPredmet vp = new VpisaniPredmet();
                    vp.IzvedbaPredmeta_idIzvedbaPredmeta = idProstiPredmet_3kt;
                    //SPREMENITI OZ. PRILAGODITI ID VPISA!!!!
                    vp.Vpis_idVpis = idVpis;
                    db.VpisaniPredmet.Add(vp);
                }
                else
                {
                    drugiProstoIzbirni_3kt_ErrorLB.Visible = true;
                    check = false;
                }
            }

            //dodamo obvezne predmete drugega letnika
            //PREVERI ČE DELA???
            var obvezniPredmetiDrugiLetnik = (from ip in db.IzvedbaPredmeta
                                              where ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 2 &&
                                                    ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 3 &&
                                                    ip.PredmetStudijskegaPrograma.StudijskiProgram_idStudijskiProgram1 == vpis.StudijskiProgram_idStudijskiProgram &&
                                                    ip.StudijskoLeto.studijskoLeto1.Equals(vpis.StudijskoLeto)
                                              select ip).Distinct().ToList();
            foreach (IzvedbaPredmeta item in obvezniPredmetiDrugiLetnik)
            {
                VpisaniPredmet vp = new VpisaniPredmet();
                vp.IzvedbaPredmeta_idIzvedbaPredmeta = item.idIzvedbaPredmeta;
                //SPREMENITI OZ. PRILAGODITI ID VPISA!!!!
                vp.Vpis_idVpis = idVpis;
                db.VpisaniPredmet.Add(vp);
            }

            if (check)
            {
                db.SaveChanges();
                Response.Redirect("Studentek/IzpisVpisnegaListaStudent.aspx");
                //go to next page
            }
        }

        protected void drugiStrokovniIzbirniDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            idVpis = (int)ViewState["idVpis"];
            int idStrokovni = Int32.Parse(drugiStrokovniIzbirniDDL.SelectedValue);
            int idProstoIzbirni = Int32.Parse(drugiProstoIzbirniDDL.SelectedValue);
            Vpis vpis = (Vpis)(from v in db.Vpis
                               where v.idVpis == idVpis
                               select v).FirstOrDefault();

            var prostoIzbirni = (from ip in db.IzvedbaPredmeta
                                 where
                                     ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 2 &&
                                     ip.StudijskoLeto_idStudijskoLeto == 11 &&
                                     (
                                        ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 1 ||
                                        ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 2
                                     )
                                 select ip).Distinct().ToList();
            if (idStrokovni != 0)
            {
                prostoIzbirni = (from ip in db.IzvedbaPredmeta
                                 where
                                     ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 2 &&
                                     ip.idIzvedbaPredmeta != idStrokovni &&
                                     ip.StudijskoLeto_idStudijskoLeto == 11 &&
                                     (
                                        ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 1 ||
                                        ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 2
                                     )
                                 select ip).Distinct().ToList();
            }



            drugiProstoIzbirniDDL.Items.Clear();
            foreach (var item in prostoIzbirni)
            {
                drugiProstoIzbirniDDL.Items.Add(new ListItem(item.PredmetStudijskegaPrograma.Predmet.imePredmeta, item.idIzvedbaPredmeta.ToString()));
            }
            SortDDL_Predmeti(ref drugiProstoIzbirniDDL);
            drugiProstoIzbirniDDL.DataBind();

            if (idProstoIzbirni != idStrokovni)
            {
                for (int i = 0; i < drugiProstoIzbirniDDL.Items.Count; i++)
                {
                    if (drugiProstoIzbirniDDL.Items[i].Value.Equals(idProstoIzbirni + ""))
                    {
                        drugiProstoIzbirniDDL.SelectedIndex = i;
                    }
                }
            }
            else
            {
                drugiProstoIzbirniDDL.SelectedIndex = -1;
            }
            drugiStrokovniIzbirniErrorLB.Visible = false;
        }

        //Ce je prosto izbirni manj kot 6 KT, omogoči izbiro drugega prosto izbirnega predmeta, vrednega toliko KT kolikor 
        //manjka do skupno 6 KT.
        protected void drugiProstoIzbirniDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int prviPredmet = Int32.Parse(drugiProstoIzbirniDDL.SelectedValue);
            if (prviPredmet != 0)
            {
                int ktPrviPredmet = (int)(from ip in db.IzvedbaPredmeta
                                          where ip.idIzvedbaPredmeta == prviPredmet
                                          select ip.PredmetStudijskegaPrograma.Predmet.kreditneTocke).FirstOrDefault();
                if (ktPrviPredmet < 6)
                {
                    drugiProstoIzbirniLB_3kt.Visible = true;
                    drugiProstoIzbirniDDL_3kt.Visible = true;
                    int potrebneKt = 6 - ktPrviPredmet;

                    var predmeti = (from ip in db.IzvedbaPredmeta
                                    where ip.PredmetStudijskegaPrograma.Predmet.kreditneTocke == potrebneKt &&
                                          ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 2 &&
                                          ip.StudijskoLeto_idStudijskoLeto == 11 &&
                                          ip.idIzvedbaPredmeta != prviPredmet &&
                                          (
                                            ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 1 ||
                                            ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 2
                                          )
                                    select ip).Distinct().ToList();

                    drugiProstoIzbirniDDL_3kt.Items.Clear();
                    foreach (var item in predmeti)
                    {
                        drugiProstoIzbirniDDL_3kt.Items.Add(new ListItem(item.PredmetStudijskegaPrograma.Predmet.imePredmeta, item.idIzvedbaPredmeta.ToString()));
                    }
                    SortDDL_Predmeti(ref drugiProstoIzbirniDDL_3kt);
                    drugiProstoIzbirniDDL_3kt.DataBind();
                }
                else if (ktPrviPredmet == 6)
                {
                    drugiProstoIzbirniLB_3kt.Visible = false;
                    drugiProstoIzbirniDDL_3kt.Visible = false;
                }
                drugiProstoIzbirniErrorLB.Visible = false;
            }
            else
            {
                drugiProstoIzbirniLB_3kt.Visible = false;
                drugiProstoIzbirniDDL_3kt.Visible = false;
                drugiProstoIzbirni_3kt_ErrorLB.Visible = false;
            }
        }

        protected void drugiProstoIzbirniDDL_3kt_SelectedIndexChanged(object sender, EventArgs e)
        {
            drugiProstoIzbirni_3kt_ErrorLB.Visible = false;
        }

        protected void tretjiPrviModulDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPrviModul = Int32.Parse(tretjiPrviModulDDL.SelectedValue);
            int idDrugiModul = Int32.Parse(tretjiDrugiModulDDL.SelectedValue);

            var vsiModuli = (from sdp in db.SestavniDelPred
                             where sdp.jeModul == true
                             select sdp).Distinct().ToList();
            if (idPrviModul != 0)
            {
                vsiModuli = (from sdp in db.SestavniDelPred
                             where sdp.jeModul == true &&
                             sdp.idSestavniDelPred != idPrviModul
                             select sdp).Distinct().ToList();
            }

            tretjiDrugiModulDDL.Items.Clear();
            foreach (var item in vsiModuli)
            {
                tretjiDrugiModulDDL.Items.Add(new ListItem(item.opisSestavnegaDela, item.idSestavniDelPred.ToString()));
            }
            SortDDL_Moduli(ref tretjiDrugiModulDDL);
            tretjiDrugiModulDDL.DataBind();

            if (idDrugiModul != idPrviModul)
            {
                for (int i = 0; i < tretjiDrugiModulDDL.Items.Count; i++)
                {
                    if (tretjiDrugiModulDDL.Items[i].Value.Equals(idDrugiModul + ""))
                    {
                        tretjiDrugiModulDDL.SelectedIndex = i;
                    }
                }
            }
            else
            {
                tretjiDrugiModulDDL.SelectedIndex = -1;
            }
            tretjiPrviModulErrorLB.Visible = false;
            tretjiProstoIzbirniDDL_SelectedIndexChanged(null, null);
        }

        protected void tretjiDrugiModulDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPrviModul = Int32.Parse(tretjiPrviModulDDL.SelectedValue);
            int idDrugiModul = Int32.Parse(tretjiDrugiModulDDL.SelectedValue);

            var vsiModuli = (from sdp in db.SestavniDelPred
                             where sdp.jeModul == true
                             select sdp).Distinct().ToList();
            if (idDrugiModul != 0)
            {
                vsiModuli = (from sdp in db.SestavniDelPred
                             where sdp.jeModul == true &&
                             sdp.idSestavniDelPred != idDrugiModul
                             select sdp).Distinct().ToList();
            }

            tretjiPrviModulDDL.Items.Clear();
            foreach (var item in vsiModuli)
            {
                tretjiPrviModulDDL.Items.Add(new ListItem(item.opisSestavnegaDela, item.idSestavniDelPred.ToString()));
            }
            SortDDL_Moduli(ref tretjiPrviModulDDL);
            tretjiPrviModulDDL.DataBind();

            if (idDrugiModul != idPrviModul)
            {
                for (int i = 0; i < tretjiPrviModulDDL.Items.Count; i++)
                {
                    if (tretjiPrviModulDDL.Items[i].Value.Equals(idPrviModul + ""))
                    {
                        tretjiPrviModulDDL.SelectedIndex = i;
                    }
                }
            }
            else
            {
                tretjiPrviModulDDL.SelectedIndex = -1;
            }
            tretjiDrugiModulErrorLB.Visible = false;
            tretjiProstoIzbirniDDL_SelectedIndexChanged(null, null);
        }

        protected void tretjiProstoIzbirniDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            idVpis = (int)ViewState["idVpis"];
            int idPrviModul = Int32.Parse(tretjiPrviModulDDL.SelectedValue);
            int idDrugiModul = Int32.Parse(tretjiDrugiModulDDL.SelectedValue);
            int idProstoIzbirni = -1;
            if (tretjiProstoIzbirniDDL.Items.Count > 0)
            {
                idProstoIzbirni = Int32.Parse(tretjiProstoIzbirniDDL.SelectedValue);
            }

            Vpis vpis = (Vpis)(from v in db.Vpis
                               where v.idVpis == idVpis
                               select v).FirstOrDefault();

            StudijskiProgram studijskiProgram = (StudijskiProgram)(from sp in db.StudijskiProgram
                                                                   where sp.idStudijskiProgram == vpis.StudijskiProgram_idStudijskiProgram
                                                                   select sp).FirstOrDefault();

            //zberemo vse možne predmete razen iz izbranih modulov ter prosto izbirnih predmetov 2. letnika
            var predmeti = (from ip in db.IzvedbaPredmeta
                            where ip.PredmetStudijskegaPrograma.StudijskiProgram_idStudijskiProgram1 == studijskiProgram.idStudijskiProgram &&
                                  ip.StudijskoLeto_idStudijskoLeto == 11 &&
                                  (
                                        ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred != idPrviModul &&
                                        ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred != idDrugiModul &&
                                        (
                                            (
                                    //odstrani prosto izbirne predmete v 2. letniku
                                                ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 2 &&
                                                ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred != 2
                                            ) ||
                                            ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 1 ||
                                            (
                                    //odstrani obvezne predmete 3. letnika (EP, KVP, diplomska naloga)
                                                ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 3 &&
                                                ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred != 3
                                            )
                                        )
                                    )
                            select ip).Distinct().ToList();

            int vpisnaStudenta = (int)(from v in db.Vpis
                                  where v.idVpis == idVpis
                                  select v.Student.vpisnaStudenta).FirstOrDefault();
            //zberemo že narejene predmete študenta
            var narejeniPredmeti = (from vp in db.VpisaniPredmet
                                    //spremeni glede na podatke pridobljene iz viewstate al kakorkoli že bo
                                    where vp.Vpis.Student.vpisnaStudenta == vpisnaStudenta
                                    select vp).Distinct().ToList();

            List<IzvedbaPredmeta> koncniSeznamPredmetov = new List<IzvedbaPredmeta>();
            //odstrani že narejene oz. vpisane predmete študenta
            foreach (var item in predmeti)
            {
                bool equal = false;
                foreach (var item2 in narejeniPredmeti)
                {
                    if (item.Equals(item2.IzvedbaPredmeta))
                    {
                        equal = true;
                        break;
                    }
                }
                if (!equal)
                {
                    koncniSeznamPredmetov.Add(item);
                }
            }

            tretjiProstoIzbirniDDL.Items.Clear();
            foreach (var item in koncniSeznamPredmetov)
            {
                tretjiProstoIzbirniDDL.Items.Add(new ListItem(item.PredmetStudijskegaPrograma.Predmet.imePredmeta, item.idIzvedbaPredmeta.ToString()));
            }
            SortDDL_Predmeti(ref tretjiProstoIzbirniDDL);
            tretjiProstoIzbirniDDL.DataBind();

            if (!(idProstoIzbirni < 1))
            {
                int index = -1;
                for (int i = 0; i < tretjiProstoIzbirniDDL.Items.Count; i++)
                {
                    if (tretjiProstoIzbirniDDL.Items[i].Value.Equals(idProstoIzbirni + ""))
                    {
                        index = i;
                    }
                }
                tretjiProstoIzbirniDDL.SelectedIndex = index;

                int ktPrviPredmet = (int)(from ip in db.IzvedbaPredmeta
                                          where ip.idIzvedbaPredmeta == idProstoIzbirni &&
                                          ip.StudijskoLeto_idStudijskoLeto == 11
                                          select ip.PredmetStudijskegaPrograma.Predmet.kreditneTocke).FirstOrDefault();
                if (ktPrviPredmet < 6)
                {
                    tretjiProstoIzbirniLB_3kt.Visible = true;
                    tretjiProstoIzbirniDDL_3kt.Visible = true;
                    int potrebneKt = 6 - ktPrviPredmet;

                    var predmetiDrugi = (from ip in db.IzvedbaPredmeta
                                         where ip.PredmetStudijskegaPrograma.Predmet.kreditneTocke == potrebneKt &&
                                               ip.StudijskoLeto_idStudijskoLeto == 11 &&
                                               ip.PredmetStudijskegaPrograma.IdPredmetStudijskegaPrograma != idProstoIzbirni &&
                                               ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred != idPrviModul &&
                                               ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred != idDrugiModul &&
                                             (
                                                 (
                                             //odstrani prosto izbirne predmete v 2. letniku
                                                     ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 2 &&
                                                     ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred != 2
                                                 ) ||
                                                 ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 1 ||
                                                 (
                                             //odstrani obvezne predmete 3. letnika (EP, KVP, diplomska naloga)
                                                     ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 3 &&
                                                     ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred != 3
                                                 )
                                             )
                                         select ip).Distinct().ToList();

                    List<IzvedbaPredmeta> koncniSeznamPredmetov_3kt = new List<IzvedbaPredmeta>();
                    //odstrani že narejene oz. vpisane predmete študenta
                    foreach (var item in predmetiDrugi)
                    {
                        bool equal = false;
                        foreach (var item2 in narejeniPredmeti)
                        {
                            if (item.Equals(item2.IzvedbaPredmeta))
                            {
                                equal = true;
                                break;
                            }
                        }
                        if (!equal)
                        {
                            koncniSeznamPredmetov_3kt.Add(item);
                        }
                    }

                    tretjiProstoIzbirniDDL_3kt.Items.Clear();
                    foreach (var item in predmetiDrugi)
                    {
                        tretjiProstoIzbirniDDL_3kt.Items.Add(new ListItem(item.PredmetStudijskegaPrograma.Predmet.imePredmeta, item.idIzvedbaPredmeta.ToString()));
                    }
                    SortDDL_Predmeti(ref tretjiProstoIzbirniDDL_3kt);
                    tretjiProstoIzbirniDDL_3kt.DataBind();
                }
                else if (ktPrviPredmet == 6)
                {
                    tretjiProstoIzbirniLB_3kt.Visible = false;
                    tretjiProstoIzbirniDDL_3kt.Visible = false;
                }
                tretjiProstoIzbirniErrorLB.Visible = false;
            }
            else
            {
                tretjiProstoIzbirniLB_3kt.Visible = false;
                tretjiProstoIzbirniDDL_3kt.Visible = false;
                tretjiProstoIzbirni_3kt_ErrorLB.Visible = false;
            }
        }

        protected void tretjiProstoIzbirniDDL_3kt_SelectedIndexChanged(object sender, EventArgs e)
        {
            tretjiProstoIzbirni_3kt_ErrorLB.Visible = false;
        }

        protected void potrdiTretjiButton_Click(object sender, EventArgs e)
        {
            idVpis = (int)ViewState["idVpis"];
            int idPrviModul = Int32.Parse(tretjiPrviModulDDL.SelectedValue);
            int idDrugiModul = Int32.Parse(tretjiDrugiModulDDL.SelectedValue);
            int idProstiPredmet = Int32.Parse(tretjiProstoIzbirniDDL.SelectedValue);
            int idProstiPredmet_3kt = -1;
            if (tretjiProstoIzbirniDDL_3kt.Items.Count > 0)
            {
                idProstiPredmet_3kt = Int32.Parse(tretjiProstoIzbirniDDL_3kt.SelectedValue); ;
            }

            bool check = true;
            if (idPrviModul == 0)
            {
                tretjiPrviModulErrorLB.Visible = true;
                check = false;
            }
            if (idDrugiModul == 0)
            {
                tretjiDrugiModulErrorLB.Visible = true;
                check = false;
            }
            if (idProstiPredmet == 0)
            {
                tretjiProstoIzbirniErrorLB.Visible = true;
                check = false;
            }
            if (tretjiProstoIzbirniDDL_3kt.Visible == true && idProstiPredmet_3kt == 0)
            {
                tretjiProstoIzbirni_3kt_ErrorLB.Visible = true;
                check = false;
            }

            if (check)
            {
                var predmeti = (from ip in db.IzvedbaPredmeta
                                where ip.StudijskoLeto_idStudijskoLeto == 11 && 
                                (
                                ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == idPrviModul ||
                                      ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == idDrugiModul ||
                                      ip.idIzvedbaPredmeta == idProstiPredmet ||
                                      ip.idIzvedbaPredmeta == idProstiPredmet_3kt ||
                                    //dodamo obvezne predmete za 3. letnik
                                      ip.PredmetStudijskegaPrograma.Letnik_idLetnik == 3 && ip.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred == 3 
                                )

                                select ip).Distinct().ToList();

                foreach (var item in predmeti)
                {
                    VpisaniPredmet vp = new VpisaniPredmet();
                    vp.IzvedbaPredmeta_idIzvedbaPredmeta = item.idIzvedbaPredmeta;
                    //SPREMENITI OZ. PRILAGODITI ID VPISA!!!!
                    vp.Vpis_idVpis = idVpis;
                    db.VpisaniPredmet.Add(vp);
                }

                db.SaveChanges();
                Response.Redirect("Studentek/IzpisVpisnegaListaStudent.aspx");
                //tretjiPrviModulDDL.SelectedIndex = 0;
                //tretjiDrugiModulDDL.SelectedIndex = 0;
                //tretjiProstoIzbirniDDL.SelectedIndex = 0;
                //if (tretjiProstoIzbirniDDL_3kt.Items.Count > 0)
                //{
                //    tretjiProstoIzbirniDDL_3kt.SelectedIndex = 0;
                //}
            }
        }

        //Ob izbiri strokovnega predmeta v 2. letniku, odstrani ta strokovni predmet iz možnih prostoizbirnih predmetov.
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

            //dodaj id izvedbe predmeta in jo shrani v value
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
            objDDL.Items.Insert(0, new ListItem("Izberi", "0"));
        }

        private void SortDDL_Moduli(ref DropDownList objDDL)
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
        private void SortCBL_Predmeti(ref CheckBoxList objDDL)
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
            var predmeti = (from ip in db.IzvedbaPredmeta select ip).ToList();
            for (int i = 0; i < textList.Count; i++)
            {
                int idPsp = Int32.Parse(valueList[i].ToString());
                foreach (var item in predmeti)
                {
                    if (item.idIzvedbaPredmeta == idPsp)
                    {
                        ListItem objItem = new ListItem(item.PredmetStudijskegaPrograma.Predmet.idPredmet + " - " + textList[i].ToString(), valueList[i].ToString());
                        objDDL.Items.Add(objItem);
                    }
                }
            }
        }

        protected void tretjiPovprecjeCBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            var splosenSeznamPredmetov = (from ip in db.IzvedbaPredmeta
                                          select ip).Distinct().ToList();
            int kreditneTocke = 0;
            int stPredmetov_3kt = 0;
            foreach (ListItem item in tretjiPovprecjeCBL.Items)
            {
                if (item.Selected)
                {
                    foreach (var predmet in splosenSeznamPredmetov)
                    {
                        if (predmet.idIzvedbaPredmeta == Int32.Parse(item.Value))
                        {
                            kreditneTocke += (int)predmet.PredmetStudijskegaPrograma.Predmet.kreditneTocke;
                            //preštejemo št. predmetov s 3 KT (max 2, ker je lahko 1 poljuben)
                            if ((int)predmet.PredmetStudijskegaPrograma.Predmet.kreditneTocke == 3)
                            {
                                stPredmetov_3kt++;
                            }
                        }
                    }
                }
            }

            kTockeLB.Text = "Imate " + kreditneTocke + " kreditnih točk od 60 potrebnih";

            //če je izbranih dovolj predmetov (60 KT), zamrzni neizbrane checkbox-e. če je manj kot 60, jih odmrzni.
            var enabledCheckbox = true;
            if (kreditneTocke == 60)
            {
                enabledCheckbox = false;
            }
            foreach (ListItem item in tretjiPovprecjeCBL.Items)
            {
                if (!item.Selected)
                {
                    item.Enabled = enabledCheckbox;
                }
            }

            //prepreči izbor več kot dveh predmetov z manj kot 6 KT
            if (stPredmetov_3kt == 2)
            {
                foreach (ListItem item in tretjiPovprecjeCBL.Items)
                {
                    foreach (var item2 in splosenSeznamPredmetov)
                    {
                        if (Int32.Parse(item.Value) == item2.idIzvedbaPredmeta && item2.PredmetStudijskegaPrograma.Predmet.kreditneTocke < 6 && !item.Selected)
                        {
                            item.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void potrdiTretjiPovprecjeButton_Click(object sender, EventArgs e)
        {
            idVpis = (int)ViewState["idVpis"];
            var splosenSeznamPredmetov = (from ip in db.IzvedbaPredmeta
                                          select ip).Distinct().ToList();

            foreach (ListItem item in tretjiPovprecjeCBL.Items)
            {
                if (item.Selected)
                {
                    foreach (var predmet in splosenSeznamPredmetov)
                    {
                        //dodaj predmete, ki so označeni v Checkboxlist-i
                        if (predmet.idIzvedbaPredmeta == Int32.Parse(item.Value))
                        {
                            VpisaniPredmet vp = new VpisaniPredmet();
                            vp.IzvedbaPredmeta_idIzvedbaPredmeta = predmet.idIzvedbaPredmeta;
                            //SPREMENITI OZ. PRILAGODITI ID VPISA!!!!
                            vp.Vpis_idVpis = idVpis;
                            db.VpisaniPredmet.Add(vp);
                        }
                    }
                }
            }
            db.SaveChanges();
            Response.Redirect("Studentek/IzpisVpisnegaListaStudent.aspx");
        }

        protected void predmetnikPrviLetnikGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                predmetnikPrviLetnikGV.Columns[6].Visible = false;
                predmetnikPrviLetnikGV.Columns[7].Visible = false;
                predmetnikPrviLetnikGV.Columns[8].Visible = false;
                var dr = e.Row.DataItem as DataRowView;
            }
        }

        protected void vpisniListBTN_Click(object sender, EventArgs e)
        {
            Response.Redirect("Studentek/IzpisVpisnegaListaStudent.aspx");
        }
    }
}