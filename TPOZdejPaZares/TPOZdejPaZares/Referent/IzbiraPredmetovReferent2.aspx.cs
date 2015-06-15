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
    public partial class IzbiraPredmetovReferent2 : System.Web.UI.Page
    {
        t8_2015Entities db = new t8_2015Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //izbira izbrinih predmetov - vsi predmeti iz vseh modulov in letnikov za 3. letnik
            //kako za drugi??
            //zgenerirati oz. dobiti idVpis za naslednji letnik, ne za prejšnji, in potem to dodati
            //pri kreiranju entitete VpisaniPredmet
            if (!IsPostBack)
            {
                int idVpis = Int32.Parse(Request.QueryString["idVpis"]);
                Vpis vpis = (Vpis)(from v in db.Vpis
                                   where v.idVpis == idVpis
                                   select v).FirstOrDefault();

                //samo v primeru, ko je nekdo spremenil GET metodo in dani idVpis ne obstaja
                if (vpis == null)
                {
                    var builder = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port, "Default.aspx");
                    Response.Redirect(builder.ToString(), true);
                }

                var vpisaniPredmeti = (from vp in db.VpisaniPredmet
                                       where vp.Vpis_idVpis == idVpis
                                       select vp).Distinct().ToList();

                if (vpisaniPredmeti.Count > 0)
                {
                    seznamPredmetovGV_Init();
                    prestejKreditneTocke();
                }
                else if (vpisaniPredmeti.Count == 0)
                {
                    novPredmetnik.Style.Add("display", "block");
                    Student student = (Student)vpis.Student;

                    int vpisnaStevilka = (int) student.vpisnaStudenta;

                    StudijskiProgram studijskiProgram = (StudijskiProgram)(from sp in db.StudijskiProgram
                                                                           where sp.idStudijskiProgram == vpis.StudijskiProgram_idStudijskiProgram
                                                                           select sp).FirstOrDefault();

                    if (vpis.Letnik.letnik1 == 2)
                    {
                        drugiLetnikDiv.Style.Add("display", "block");

                        //napolnimo strokovno izbirne predmete
                        var strokovniIzbirni = (from psp in db.PredmetStudijskegaPrograma
                                                where psp.Letnik_idLetnik == 2 &&
                                                      psp.SestavniDelPred_idSestavniDelPred == 1
                                                select psp).ToList();
                        foreach (var item in strokovniIzbirni)
                        {
                            drugiStrokovniIzbirniDDL.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
                        }
                        SortDDL_Predmeti(ref drugiStrokovniIzbirniDDL);
                        drugiStrokovniIzbirniDDL.DataBind();

                        //napolnimo prosto izbirne predmete z prosto izbirnimi in strokovno izbirnimi predmeti 2. letnika. Strokovni predmet, ki ga je študent izbral, se iz liste prosto izbirninh izloči ob selectedchange na strokovno izbrinem
                        var prostoIzbirni = (from psp in db.PredmetStudijskegaPrograma
                                             where psp.Letnik_idLetnik == 2 &&
                                                   (
                                                       psp.SestavniDelPred_idSestavniDelPred == 1 ||
                                                       psp.SestavniDelPred_idSestavniDelPred == 2
                                                   )
                                             select psp).Distinct().ToList();

                        foreach (var item in prostoIzbirni)
                        {
                            drugiProstoIzbirniDDL.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
                        }
                        SortDDL_Predmeti(ref drugiProstoIzbirniDDL);
                        drugiProstoIzbirniDDL.DataBind();
                    }
                    //vpis v 3. letnik
                    else if (vpis.Letnik.letnik1 == 3)
                    {

                        if (povprecnaOcena(vpisnaStevilka) >= 9.5)
                        {
                            tretjiLetnikPovprecjeDiv.Style.Add("display", "block");
                            var predmeti = (from psp in db.PredmetStudijskegaPrograma
                                            where (
                                                //odstrani prosto izbirne predmete v 2. letniku
                                                        psp.Letnik_idLetnik == 2 &&
                                                        psp.SestavniDelPred_idSestavniDelPred != 2
                                                   ) ||
                                                    psp.Letnik_idLetnik == 1 ||
                                                   (
                                                //odstrani obvezne predmete 3. letnika (EP, KVP, diplomska naloga)
                                                        psp.Letnik_idLetnik == 3 &&
                                                        psp.SestavniDelPred_idSestavniDelPred != 3
                                                    )
                                            select psp).Distinct().ToList();

                            //zberemo že narejene predmete študenta
                            var narejeniPredmeti = (from vp in db.VpisaniPredmet
                                                    //spremeni glede na podatke pridobljene iz viewstate al kakorkoli že bo
                                                    where vp.Vpis.Student.vpisnaStudenta == 63125444
                                                    select vp).Distinct().ToList();

                            List<PredmetStudijskegaPrograma> koncniSeznamPredmetov = new List<PredmetStudijskegaPrograma>();
                            //odstrani že narejene oz. vpisane predmete študenta
                            foreach (var item in predmeti)
                            {
                                bool equal = false;
                                foreach (var item2 in narejeniPredmeti)
                                {
                                    if (item.Equals(item2.PredmetStudijskegaPrograma))
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
                                tretjiPovprecjeCBL.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
                            }
                            SortCBL_Predmeti(ref tretjiPovprecjeCBL);
                            tretjiPovprecjeCBL.DataBind();
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
                        prviLetnik[0] += 1.0;
                        prviLetnik[1] += (double)ocena;
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

            if (idStrokovniPredmet != 0)
            {
                VpisaniPredmet vp = new VpisaniPredmet();
                vp.PredmetStudijskegaPrograma_idPredmetStudijskegaPrograma = idStrokovniPredmet;
                vp.Vpis_idVpis = Int32.Parse(Request.QueryString["idVpis"]);
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
                vp.PredmetStudijskegaPrograma_idPredmetStudijskegaPrograma = idProstiPredmet;
                vp.Vpis_idVpis = Int32.Parse(Request.QueryString["idVpis"]);
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
                    vp.PredmetStudijskegaPrograma_idPredmetStudijskegaPrograma = idProstiPredmet_3kt;
                    vp.Vpis_idVpis = Int32.Parse(Request.QueryString["idVpis"]);
                    db.VpisaniPredmet.Add(vp);
                }
                else
                {
                    drugiProstoIzbirni_3kt_ErrorLB.Visible = true;
                    check = false;
                }
            }

            if (check)
            {
                db.SaveChanges();
                //go to next page
            }
        }

        protected void drugiStrokovniIzbirniDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idStrokovni = Int32.Parse(drugiStrokovniIzbirniDDL.SelectedValue);
            int idProstoIzbirni = Int32.Parse(drugiProstoIzbirniDDL.SelectedValue);

            var prostoIzbirni = (from psp in db.PredmetStudijskegaPrograma
                                 where
                                     psp.Letnik_idLetnik == 2 &&
                                     (
                                        psp.SestavniDelPred_idSestavniDelPred == 1 ||
                                        psp.SestavniDelPred_idSestavniDelPred == 2
                                     )
                                 select psp).Distinct().ToList();
            if (idStrokovni != 0)
            {
                prostoIzbirni = (from psp in db.PredmetStudijskegaPrograma
                                 where
                                     psp.Letnik_idLetnik == 2 &&
                                     psp.IdPredmetStudijskegaPrograma != idStrokovni &&
                                     (
                                        psp.SestavniDelPred_idSestavniDelPred == 1 ||
                                        psp.SestavniDelPred_idSestavniDelPred == 2
                                     )
                                 select psp).Distinct().ToList();
            }



            drugiProstoIzbirniDDL.Items.Clear();
            foreach (var item in prostoIzbirni)
            {
                drugiProstoIzbirniDDL.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
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
                int ktPrviPredmet = (int)(from p in db.PredmetStudijskegaPrograma
                                          where p.IdPredmetStudijskegaPrograma == prviPredmet
                                          select p.Predmet.kreditneTocke).FirstOrDefault();
                if (ktPrviPredmet < 6)
                {
                    drugiProstoIzbirniLB_3kt.Visible = true;
                    drugiProstoIzbirniDDL_3kt.Visible = true;
                    int potrebneKt = 6 - ktPrviPredmet;

                    var predmeti = (from psp in db.PredmetStudijskegaPrograma
                                    where psp.Predmet.kreditneTocke == potrebneKt &&
                                          psp.Letnik_idLetnik == 2 &&
                                          psp.IdPredmetStudijskegaPrograma != prviPredmet &&
                                          (
                                            psp.SestavniDelPred_idSestavniDelPred == 1 ||
                                            psp.SestavniDelPred_idSestavniDelPred == 2
                                          )
                                    select psp).Distinct().ToList();

                    drugiProstoIzbirniDDL_3kt.Items.Clear();
                    foreach (var item in predmeti)
                    {
                        drugiProstoIzbirniDDL_3kt.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
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
            int idPrviModul = Int32.Parse(tretjiPrviModulDDL.SelectedValue);
            int idDrugiModul = Int32.Parse(tretjiDrugiModulDDL.SelectedValue);
            int idProstoIzbirni = -1;
            if (tretjiProstoIzbirniDDL.Items.Count > 0)
            {
                idProstoIzbirni = Int32.Parse(tretjiProstoIzbirniDDL.SelectedValue);
            }

            //zberemo vse možne predmete razen iz izbranih modulov ter prosto izbirnih predmetov 2. letnika
            var predmeti = (from psp in db.PredmetStudijskegaPrograma
                            where psp.SestavniDelPred_idSestavniDelPred != idPrviModul &&
                                    psp.SestavniDelPred_idSestavniDelPred != idDrugiModul &&
                                    (
                                        (
                                //odstrani prosto izbirne predmete v 2. letniku
                                            psp.Letnik_idLetnik == 2 &&
                                            psp.SestavniDelPred_idSestavniDelPred != 2
                                        ) ||
                                        psp.Letnik_idLetnik == 1 ||
                                        (
                                //odstrani obvezne predmete 3. letnika (EP, KVP, diplomska naloga)
                                            psp.Letnik_idLetnik == 3 &&
                                            psp.SestavniDelPred_idSestavniDelPred != 3
                                        )
                                    )
                            select psp).Distinct().ToList();

            //zberemo že narejene predmete študenta
            var narejeniPredmeti = (from vp in db.VpisaniPredmet
                                    //spremeni glede na podatke pridobljene iz viewstate al kakorkoli že bo
                                    where vp.Vpis.Student.vpisnaStudenta == 63125444
                                    select vp).Distinct().ToList();

            List<PredmetStudijskegaPrograma> koncniSeznamPredmetov = new List<PredmetStudijskegaPrograma>();
            //odstrani že narejene oz. vpisane predmete študenta
            foreach (var item in predmeti)
            {
                bool equal = false;
                foreach (var item2 in narejeniPredmeti)
                {
                    if (item.Equals(item2.PredmetStudijskegaPrograma))
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
                tretjiProstoIzbirniDDL.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
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

                int ktPrviPredmet = (int)(from p in db.PredmetStudijskegaPrograma
                                          where p.IdPredmetStudijskegaPrograma == idProstoIzbirni
                                          select p.Predmet.kreditneTocke).FirstOrDefault();
                if (ktPrviPredmet < 6)
                {
                    tretjiProstoIzbirniLB_3kt.Visible = true;
                    tretjiProstoIzbirniDDL_3kt.Visible = true;
                    int potrebneKt = 6 - ktPrviPredmet;

                    var predmetiDrugi = (from psp in db.PredmetStudijskegaPrograma
                                         where psp.Predmet.kreditneTocke == potrebneKt &&
                                               psp.IdPredmetStudijskegaPrograma != idProstoIzbirni &&
                                               psp.SestavniDelPred_idSestavniDelPred != idPrviModul &&
                                               psp.SestavniDelPred_idSestavniDelPred != idDrugiModul &&
                                             (
                                                 (
                                             //odstrani prosto izbirne predmete v 2. letniku
                                                     psp.Letnik_idLetnik == 2 &&
                                                     psp.SestavniDelPred_idSestavniDelPred != 2
                                                 ) ||
                                                 psp.Letnik_idLetnik == 1 ||
                                                 (
                                             //odstrani obvezne predmete 3. letnika (EP, KVP, diplomska naloga)
                                                     psp.Letnik_idLetnik == 3 &&
                                                     psp.SestavniDelPred_idSestavniDelPred != 3
                                                 )
                                             )
                                         select psp).Distinct().ToList();

                    List<PredmetStudijskegaPrograma> koncniSeznamPredmetov_3kt = new List<PredmetStudijskegaPrograma>();
                    //odstrani že narejene oz. vpisane predmete študenta
                    foreach (var item in predmetiDrugi)
                    {
                        bool equal = false;
                        foreach (var item2 in narejeniPredmeti)
                        {
                            if (item.Equals(item2.PredmetStudijskegaPrograma))
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
                        tretjiProstoIzbirniDDL_3kt.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
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
                var predmeti = (from psp in db.PredmetStudijskegaPrograma
                                where psp.SestavniDelPred_idSestavniDelPred == idPrviModul ||
                                      psp.SestavniDelPred_idSestavniDelPred == idDrugiModul ||
                                      psp.IdPredmetStudijskegaPrograma == idProstiPredmet ||
                                      psp.IdPredmetStudijskegaPrograma == idProstiPredmet_3kt ||
                                    //dodamo predmeta KVP, EP ter diplomski predmet
                                      psp.IdPredmetStudijskegaPrograma == 66 ||
                                      psp.IdPredmetStudijskegaPrograma == 114 ||
                                      psp.IdPredmetStudijskegaPrograma == 115

                                select psp).Distinct().ToList();

                foreach (var item in predmeti)
                {
                    VpisaniPredmet vp = new VpisaniPredmet();
                    vp.PredmetStudijskegaPrograma_idPredmetStudijskegaPrograma = item.IdPredmetStudijskegaPrograma;
                    vp.Vpis_idVpis = Int32.Parse(Request.QueryString["idVpis"]);
                    db.VpisaniPredmet.Add(vp);
                }

                db.SaveChanges();
                tretjiPrviModulDDL.SelectedIndex = 0;
                tretjiDrugiModulDDL.SelectedIndex = 0;
                tretjiProstoIzbirniDDL.SelectedIndex = 0;
                if (tretjiProstoIzbirniDDL_3kt.Items.Count > 0)
                {
                    tretjiProstoIzbirniDDL_3kt.SelectedIndex = 0;
                }
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

            //dodaj številko predmeta in jo shrani v value
            var predmeti = (from psp in db.PredmetStudijskegaPrograma select psp).ToList();
            for (int i = 0; i < textList.Count; i++)
            {
                int idPsp = Int32.Parse(valueList[i].ToString());
                foreach (var item in predmeti)
                {
                    if (item.IdPredmetStudijskegaPrograma == idPsp)
                    {
                        ListItem objItem = new ListItem(item.Predmet.idPredmet + " - " + textList[i].ToString(), valueList[i].ToString());
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
            var predmeti = (from psp in db.PredmetStudijskegaPrograma select psp).ToList();
            for (int i = 0; i < textList.Count; i++)
            {
                int idPsp = Int32.Parse(valueList[i].ToString());
                foreach (var item in predmeti)
                {
                    if (item.IdPredmetStudijskegaPrograma == idPsp)
                    {
                        ListItem objItem = new ListItem(item.Predmet.idPredmet + " - " + textList[i].ToString(), valueList[i].ToString());
                        objDDL.Items.Add(objItem);
                    }
                }
            }
        }

        protected void tretjiPovprecjeCBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            var splosenSeznamPredmetov = (from psp in db.PredmetStudijskegaPrograma
                                          select psp).Distinct().ToList();
            int kreditneTocke = 18;
            int stPredmetov_3kt = 0;
            foreach (ListItem item in tretjiPovprecjeCBL.Items)
            {
                if (item.Selected)
                {
                    foreach (var predmet in splosenSeznamPredmetov)
                    {
                        if (predmet.IdPredmetStudijskegaPrograma == Int32.Parse(item.Value))
                        {
                            kreditneTocke += (int)predmet.Predmet.kreditneTocke;
                            //preštejemo št. predmetov s 3 KT (max 2, ker je lahko 1 poljuben)
                            if ((int)predmet.Predmet.kreditneTocke == 3)
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
                        if (Int32.Parse(item.Value) == item2.IdPredmetStudijskegaPrograma && item2.Predmet.kreditneTocke < 6 && !item.Selected)
                        {
                            item.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void potrdiTretjiPovprecjeButton_Click(object sender, EventArgs e)
        {
            var splosenSeznamPredmetov = (from psp in db.PredmetStudijskegaPrograma
                                          select psp).Distinct().ToList();

            foreach (ListItem item in tretjiPovprecjeCBL.Items)
            {
                if (item.Selected)
                {
                    foreach (var predmet in splosenSeznamPredmetov)
                    {
                        //dodaj predmete, ki so označeni v Checkboxlist-i
                        if (predmet.IdPredmetStudijskegaPrograma == Int32.Parse(item.Value))
                        {
                            VpisaniPredmet vp = new VpisaniPredmet();
                            vp.PredmetStudijskegaPrograma_idPredmetStudijskegaPrograma = predmet.IdPredmetStudijskegaPrograma;
                            vp.Vpis_idVpis = Int32.Parse(Request.QueryString["idVpis"]);
                            db.VpisaniPredmet.Add(vp);
                        }
                    }
                }
            }

            //dodaj obvezne predmete v 3. letniku (EP, KVP, Diplomska)
            int[] idObveznih = {66, 114, 115};
            for (int i = 0; i < idObveznih.Length; i++)
            {
                VpisaniPredmet vp = new VpisaniPredmet();
                vp.PredmetStudijskegaPrograma_idPredmetStudijskegaPrograma = idObveznih[i];
                vp.Vpis_idVpis = Int32.Parse(Request.QueryString["idVpis"]);
                db.VpisaniPredmet.Add(vp);
            }
            db.SaveChanges();
        }

        //odstranimo EDIT in DELETE možnost pri predmetih, ki so že ocenjeni ali pa so obvezni
        //odstranimo DELETE možnost pri predmetih, ki niso ocenjeni in imajo 6 KT (v primeru da sta 2
        //izbirna predmeta (strokovni in prosti) s po 6 KT, ne smemo nobenega od njiju odstraniti)
        protected void seznamPredmetovGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                seznamPredmetovGV.Columns[6].Visible = false;
                seznamPredmetovGV.Columns[7].Visible = false;
                var dr = e.Row.DataItem as DataRowView;
                //preverimo, če ima oceno ali pa je obvezni predmet
                if (!dr["Ocena"].ToString().Equals("") || dr["SestavniDelPred"].ToString().Equals("3"))
                {
                    e.Row.Cells[9].Controls.Clear();
                    e.Row.Cells[10].Controls.Clear();
                }
                else if (dr["Ocena"].ToString().Equals("") && Int32.Parse(dr["Kreditne"].ToString()) == 6)
                {
                    e.Row.Cells[10].Controls.Clear();
                }
            }
        }

        protected void seznamPredmetovGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            spremeniPredmet.Style.Add("display", "block");
            spremeniPredmetnik.Style.Add("display", "none");
            int idPredmet = (int)seznamPredmetovGV.DataKeys[e.NewEditIndex].Values["IdPredmeta"];
            int idVpisaniPredmet = (int)seznamPredmetovGV.DataKeys[e.NewEditIndex].Values["IdVpisanegaPredmeta"];
            int idSestavniDelPred = (int)seznamPredmetovGV.DataKeys[e.NewEditIndex].Values["SestavniDelPred"];
            int kreditneTockePredmeta = (int)seznamPredmetovGV.DataKeys[e.NewEditIndex].Values["Kreditne"];
            int idVpis = Int32.Parse(Request.QueryString["idVpis"]);

            Vpis vpis = (Vpis)(from v in db.Vpis
                            where v.idVpis == idVpis
                            select v).FirstOrDefault();

            VpisaniPredmet vpisaniPredmet = (VpisaniPredmet)(from vp in db.VpisaniPredmet
                                                             where vp.idVpisaniPredmet == idVpisaniPredmet
                                                             select vp).FirstOrDefault();

            var predmeti = (from psp in db.PredmetStudijskegaPrograma
                            select psp).Distinct().ToList();
            if (vpis.Letnik_idLetnik == 2)
            {
                //preštejemo strokovno in prosto izbirne predmete
                int countStrokovnoIzbirnih = 0;
                int countProstiIzbirnih_3kt = 0;
                foreach (GridViewRow row in seznamPredmetovGV.Rows)
                {
                    if ((int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["SestavniDelPred"] == 1) countStrokovnoIzbirnih++;
                    else if ((int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["Kreditne"] < 6) countProstiIzbirnih_3kt++;
                }
                //če je samo en strokovno izbirni in točno tega spreminjamo, ponudi samo strokovno izbirne predmete
                if (idSestavniDelPred == 1 && countStrokovnoIzbirnih == 1)
                {
                    predmeti = (from psp in db.PredmetStudijskegaPrograma
                                where psp.Letnik_idLetnik == 2 &&
                                      psp.SestavniDelPred_idSestavniDelPred == 1
                                select psp).Distinct().ToList();

                    spremeniPredmetSeznamOpcijDDL.Items.Clear();
                    foreach (var item in predmeti)
                    {
                        spremeniPredmetSeznamOpcijDDL.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
                    }
                    SortDDL_Predmeti(ref spremeniPredmetSeznamOpcijDDL);
                    spremeniPredmetSeznamOpcijDDL.DataBind();
                }
                //če imamo 2 prosto izbirna predmeta s po 3 KT, in spreminjamo enega od njiju
                else if (kreditneTockePredmeta < 6 && idSestavniDelPred == 2 && countProstiIzbirnih_3kt == 2) 
                {
                    int potrebneKt = 6 - kreditneTockePredmeta;

                    //poiščemo ID drugega prosto izbirnega predmeta, da ga kasneje odstranimo iz DDL
                    int idDrugiProstiPredmet = -1;
                    foreach (GridViewRow row in seznamPredmetovGV.Rows)
                    {
                        if ((int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["SestavniDelPred"] == 2 &&
                            !((int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["IdVpisanegaPredmeta"] == idVpisaniPredmet))
                            idDrugiProstiPredmet = (int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["IdPredmeta"];
                    }

                    var prostoIzbirniPredmeti = (from psp in db.PredmetStudijskegaPrograma
                                                 where psp.Predmet.kreditneTocke == potrebneKt &&
                                                       psp.Letnik_idLetnik == 2 &&
                                                       psp.IdPredmetStudijskegaPrograma != idDrugiProstiPredmet
                                                 select psp).Distinct().ToList();

                    spremeniPredmetSeznamOpcijDDL.Items.Clear();
                    foreach (var item in prostoIzbirniPredmeti)
                    {
                        spremeniPredmetSeznamOpcijDDL.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
                    }
                    SortDDL_Predmeti(ref spremeniPredmetSeznamOpcijDDL);
                    spremeniPredmetSeznamOpcijDDL.DataBind();
                }
                //če imamo 2 strokovno izbirna predmeta ali pa 1 strokovno izbirni in 1 prosto izbirni z 3 KT (in urejamo zadnjega)
                else
                {
                    //preverimo, ali spreminjamo prosto ali strokovno izbirni predmet (v tem primeru sta bila izbrana 2 strokovno
                    //izbirna predmeta) in shranimo indeks (drugega) strokovno izbirnega predmeta
                    int idDrugiStrokovniPredmet = -1;
                    foreach (GridViewRow row in seznamPredmetovGV.Rows)
                    {
                        if ((int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["SestavniDelPred"] == 1 &&
                            !((int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["IdVpisanegaPredmeta"] == idVpisaniPredmet))
                            idDrugiStrokovniPredmet = (int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["IdPredmeta"];
                    }

                    //zberemo vse prosto in strokovno izbirne predmete 2. letnika razen strokovno izbirnega predmeta, ki je že izbran
                    //v drugi vrstici
                    var prostoIzbirniPredmeti = (from psp in db.PredmetStudijskegaPrograma
                                         where
                                             psp.Letnik_idLetnik == 2 &&
                                             psp.IdPredmetStudijskegaPrograma != idDrugiStrokovniPredmet &&
                                             (
                                                psp.SestavniDelPred_idSestavniDelPred == 1 ||
                                                psp.SestavniDelPred_idSestavniDelPred == 2
                                             )
                                         select psp).Distinct().ToList();

                    spremeniPredmetSeznamOpcijDDL.Items.Clear();
                    foreach (var item in prostoIzbirniPredmeti)
                    {
                        spremeniPredmetSeznamOpcijDDL.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
                    }
                    SortDDL_Predmeti(ref spremeniPredmetSeznamOpcijDDL);
                    spremeniPredmetSeznamOpcijDDL.DataBind();
                }
            }
        }

        protected void seznamPredmetovGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            seznamPredmetovGV.EditIndex = -1;
            seznamPredmetovGV_Init();
        }

        protected void potrdiSpremeniPredmet_Click(object sender, EventArgs e)
        {
            int novPredmetId = Int32.Parse(spremeniPredmetSeznamOpcijDDL.SelectedValue);
            if (novPredmetId == 0)
            {
                potrdiSpremeniPredmetErrorLB.Visible = true;
            }
            else
            {
                int idVpisaniPredmet = (int)seznamPredmetovGV.DataKeys[seznamPredmetovGV.EditIndex].Values["IdVpisanegaPredmeta"];
                VpisaniPredmet vpisaniPredmet = (VpisaniPredmet)(from vp in db.VpisaniPredmet
                                                                 where vp.idVpisaniPredmet == idVpisaniPredmet
                                                                 select vp).FirstOrDefault();

                vpisaniPredmet.PredmetStudijskegaPrograma_idPredmetStudijskegaPrograma = novPredmetId;
                db.SaveChanges();
                seznamPredmetovGV_RowCancelingEdit(null, null);
                spremeniPredmet.Style.Add("display", "none");
                spremeniPredmetnik.Style.Add("display", "block");
                potrdiSpremeniPredmetErrorLB.Visible = false;
            }
        }

        //inicializacija seznamPredmetovGV (kliče se po vsaki spremembi in odstranitvi vrstice)
        protected void seznamPredmetovGV_Init()
        {
            int idVpis = Int32.Parse(Request.QueryString["idVpis"]);
            spremeniPredmetnik.Style.Add("display", "block");
            spremeniPredmet.Style.Add("display", "none");
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

            foreach (var item in seznamPredmetov)
            {
                Predmet predmet = (Predmet)(from p in db.Predmet where p.idPredmet == item.PredmetStudijskegaPrograma.Predmet_idPredmet1 select p).First();
                int letnik = (int)(from l in db.Letnik where l.idLetnik == item.Vpis.Letnik_idLetnik select l.letnik1).First();
                SestavniDelPred sdp = (SestavniDelPred)(from x in db.SestavniDelPred where x.idSestavniDelPred == item.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred select x).First();
                String sestavniDelPredString = sdp.opisSestavnegaDela;
                if (sdp.jeModul == true)
                {
                    sestavniDelPredString += " (Modul)";
                }
                String ocena = "";
                if (item.ocena != null) ocena = item.ocena.ToString();
                dt.Rows.Add(predmet.idPredmet, predmet.imePredmeta, predmet.kreditneTocke, letnik, sestavniDelPredString, ocena, 
                    item.PredmetStudijskegaPrograma_idPredmetStudijskegaPrograma, item.idVpisaniPredmet, item.PredmetStudijskegaPrograma.SestavniDelPred_idSestavniDelPred);

            }
            seznamPredmetovGV.DataSource = dt;
            seznamPredmetovGV.DataBind();
            prestejKreditneTocke();
        }

        protected void prestejKreditneTocke()
        {
            int kt = 0;
            foreach (GridViewRow row in seznamPredmetovGV.Rows)
            {
                kt += (int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["Kreditne"];
            }
            if (kt == 60)
            {
                sestevekKreditnihTockLB.Text = "Študent ima " + kt + " kreditnih točk od 60 potrebnih.";
            }
            //ponudimo dodajanje novega predmeta v vrednosti KT, kolikor manjka do skupno 60 (lahko pa tudi spremenijo obstoječi
            //prosto izbirni na nekega z 6 KT)
            else
            {
                novPredmetDiv.Style.Add("display", "block");
                sestevekKreditnihTockLB.Text = "Študent ima premalo kreditnih točk (" + kt +"/60 KT). Dodajte nov predmet oziroma spremenite obstoječi prosto izbirni predmet!";

                int potrebneKt = 60 - kt;
                int idDrugiProstiPredmet = -1;
                foreach (GridViewRow row in seznamPredmetovGV.Rows)
                {
                    if ((int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["SestavniDelPred"] == 2)
                        idDrugiProstiPredmet = (int)seznamPredmetovGV.DataKeys[row.RowIndex].Values["IdPredmeta"];
                }

                var prostoIzbirniPredmeti = (from psp in db.PredmetStudijskegaPrograma
                                             where psp.Predmet.kreditneTocke == potrebneKt &&
                                                   psp.Letnik_idLetnik == 2 &&
                                                   psp.IdPredmetStudijskegaPrograma != idDrugiProstiPredmet
                                             select psp).Distinct().ToList();

                dodajProstoIzbirniPredmetDDL_3kt.Items.Clear();
                foreach (var item in prostoIzbirniPredmeti)
                {
                    dodajProstoIzbirniPredmetDDL_3kt.Items.Add(new ListItem(item.Predmet.imePredmeta, item.IdPredmetStudijskegaPrograma.ToString()));
                }
                SortDDL_Predmeti(ref dodajProstoIzbirniPredmetDDL_3kt);
                dodajProstoIzbirniPredmetDDL_3kt.DataBind();
            }

        }

        protected void zavrziSpremeniPredmet_Click(object sender, EventArgs e)
        {
            seznamPredmetovGV_RowCancelingEdit(null, null);
        }

        protected void seznamPredmetovGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)seznamPredmetovGV.DataKeys[e.RowIndex].Values["IdVpisanegaPredmeta"];
            VpisaniPredmet vpisaniPredmet = (VpisaniPredmet)(from vp in db.VpisaniPredmet
                                                 where vp.idVpisaniPredmet == id
                                                 select vp).FirstOrDefault();
            if (vpisaniPredmet != null)
            {
                db.VpisaniPredmet.Remove(vpisaniPredmet);
                db.SaveChanges();
                seznamPredmetovGV_Init();
            }
            else
            {
                Console.WriteLine("Povpraševanje v bazo ni uspelo, imamo null objekt.");
            }
        }

        //dodaj novi predmet
        protected void dodajProstoIzbirniPredmetBtn_Click(object sender, EventArgs e)
        {
            int idPredmet = Int32.Parse(dodajProstoIzbirniPredmetDDL_3kt.SelectedValue);
            if (idPredmet == 0)
            {
                dodajProstoIzbirniPredmetErrorLB.Visible = true;
            }
            else
            {
                PredmetStudijskegaPrograma psp = (PredmetStudijskegaPrograma)(from psp1 in db.PredmetStudijskegaPrograma
                                                                              where psp1.IdPredmetStudijskegaPrograma == idPredmet
                                                                              select psp1).FirstOrDefault();
                VpisaniPredmet vp = new VpisaniPredmet();
                vp.PredmetStudijskegaPrograma_idPredmetStudijskegaPrograma = idPredmet;
                vp.Vpis_idVpis = Int32.Parse(Request.QueryString["idVpis"]);
                vp.PredmetStudijskegaPrograma = psp;
                db.VpisaniPredmet.Add(vp);
                db.SaveChanges();
                seznamPredmetovGV_Init();
                novPredmetDiv.Style.Add("display", "none");
                dodajProstoIzbirniPredmetErrorLB.Visible = false;
            }
        }

        protected void spremeniPredmetSeznamOpcijDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Int32.Parse(spremeniPredmetSeznamOpcijDDL.SelectedValue);
            if (id != 0)
            {
                dodajProstoIzbirniPredmetErrorLB.Visible = false;
            }
        }

    }
}