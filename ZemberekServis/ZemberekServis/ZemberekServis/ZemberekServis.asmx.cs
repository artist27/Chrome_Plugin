using net.zemberek.araclar.turkce;
using net.zemberek.erisim;
using net.zemberek.tr.yapi;
using net.zemberek.yapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

namespace ZemberekServis
{
    public class KelimeFrekans
    {
        public int Frekans { get; set; }
        public string Kelime { get; set; }
    }
    /// <summary>
    /// Summary description for ZemberekServis
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ZemberekServis : System.Web.Services.WebService
    {
        static string[] stopwords = new string[] {  "acaba",
                                                    "altı",
                                                    "ama",
                                                    "ancak",
                                                    "artık",
                                                    "asla",
                                                    "aslında",
                                                    "az",
                                                    "bana",
                                                    "bazen",
                                                    "bazı",
                                                    "bazıları",
                                                    "bazısı",
                                                    "belki",
                                                    "ben",
                                                    "beni",
                                                    "benim",
                                                    "beş",
                                                    "bile",
                                                    "bir",
                                                    "birçoğu",
                                                    "birçok",
                                                    "birçokları",
                                                    "biri",
                                                    "birisi",
                                                    "birkaç",
                                                    "birkaçı",
                                                    "birşey",
                                                    "birşeyi",
                                                    "biz",
                                                    "bize",
                                                    "bizi",
                                                    "bizim",
                                                    "böyle",
                                                    "böylece",
                                                    "bu",
                                                    "buna",
                                                    "bunda",
                                                    "bundan",
                                                    "bunu",
                                                    "bunun",
                                                    "burada",
                                                    "bütün",
                                                    "çoğu",
                                                    "çoğuna",
                                                    "çoğunu",
                                                    "çok",
                                                    "çünkü",
                                                    "da",
                                                    "daha",
                                                    "de",
                                                    "değil",
                                                    "demek",
                                                    "diğer",
                                                    "diğeri",
                                                    "diğerleri",
                                                    "diye",
                                                    "dokuz",
                                                    "dolayı",
                                                    "dört",
                                                    "elbette",
                                                    "en",
                                                    "fakat",
                                                    "falan",
                                                    "felan",
                                                    "filan",
                                                    "gene",
                                                    "gibi",
                                                    "hâlâ",
                                                    "hangi",
                                                    "hangisi",
                                                    "hani",
                                                    "hatta",
                                                    "hem",
                                                    "henüz",
                                                    "hep",
                                                    "hepsi",
                                                    "hepsine",
                                                    "hepsini",
                                                    "her",
                                                    "her biri",
                                                    "herkes",
                                                    "herkese",
                                                    "herkesi",
                                                    "hiç",
                                                    "hiç kimse",
                                                    "hiçbiri",
                                                    "hiçbirine",
                                                    "hiçbirini",
                                                    "için",
                                                    "içinde",
                                                    "iki",
                                                    "ile",
                                                    "ise",
                                                    "işte",
                                                    "kaç",
                                                    "kadar",
                                                    "kendi",
                                                    "kendine",
                                                    "kendini",
                                                    "ki",
                                                    "kim",
                                                    "kime",
                                                    "kimi",
                                                    "kimin",
                                                    "kimisi",
                                                    "madem",
                                                    "mı",
                                                    "mi",
                                                    "mu",
                                                    "mü",
                                                    "nasıl",
                                                    "ne",
                                                    "ne kadar",
                                                    "ne zaman",
                                                    "neden",
                                                    "nedir",
                                                    "nerde",
                                                    "nerede",
                                                    "nereden",
                                                    "nereye",
                                                    "nesi",
                                                    "neyse",
                                                    "niçin",
                                                    "niye",
                                                    "on",
                                                    "ona",
                                                    "ondan",
                                                    "onlar",
                                                    "onlara",
                                                    "onlardan",
                                                    "onların",
                                                    "onların",
                                                    "onu",
                                                    "onun",
                                                    "orada",
                                                    "oysa",
                                                    "oysaki",
                                                    "öbürü",
                                                    "ön",
                                                    "önce",
                                                    "ötürü",
                                                    "öyle",
                                                    "rağmen",
                                                    "sana",
                                                    "sekiz",
                                                    "sen",
                                                    "senden",
                                                    "seni",
                                                    "senin",
                                                    "siz",
                                                    "sizden",
                                                    "size",
                                                    "sizi",
                                                    "sizin",
                                                    "son",
                                                    "sonra",
                                                    "şayet",
                                                    "şey",
                                                    "şeyden",
                                                    "şeye",
                                                    "şeyi",
                                                    "şeyler",
                                                    "şimdi",
                                                    "şöyle",
                                                    "şu",
                                                    "şuna",
                                                    "şunda",
                                                    "şundan",
                                                    "şunlar",
                                                    "şunu",
                                                    "şunun",
                                                    "tabi",
                                                    "tamam",
                                                    "tüm",
                                                    "tümü",
                                                    "üç",
                                                    "üzere",
                                                    "var",
                                                    "ve",
                                                    "veya",
                                                    "veyahut",
                                                    "ya",
                                                    "ya da",
                                                    "yani",
                                                    "yedi",
                                                    "yerine",
                                                    "yine",
                                                    "yoksa",
                                                    "zaten",
                                                    "zira"};

        static string[] noktalama = new string[] { ".", ";", ",", ":", "?", "!", "\"", "[", "]", "{", "}" };

        [WebMethod]
        public void CumleAyristir(string cumle)
        {
            cumle = Server.UrlDecode(cumle);
            //cumle = "Anayasa, bir devletin yönetim biçimini belirtir. Toplumların ülke üzerindeki egemenlik haklarının, bireylerin temel haklarının hangi koşullar  altında devlet tarafından kullanılabileceğini belirleyen temel kanunlardır.Devletin temel kurumlarının nasıl işleyeceğini belirler. Genel olarak genel hükümler, temel hak ve özgürlükler, bireylerin topluma karşı görev ve sorumlulukları ile yasama, yürütme, yargı gibi anayasal devlet organlarını tanımlayan bölümlere sahiptir";
            List<KelimeFrekans> kelimeler = KelimeAyristir(cumle);
            for (int i = 0; i < kelimeler.Count; i++)
                KelimeKaydet(kelimeler[i].Kelime);
        }

        List<KelimeFrekans> KelimeAyristir(string cumle)
        {

            List<KelimeFrekans> kelimefrekanslar = new List<KelimeFrekans>();

            for (int i = 0; i < noktalama.Length; i++)
            {
                cumle = cumle.Replace(noktalama[i], " ");
            }

            cumle = cumle.Replace("-", "");
            cumle = cumle.Replace("—", "");
            cumle = cumle.Replace("/", " ");
            cumle = cumle.Replace("\\", " ");
            cumle = cumle.Replace("  ", " ");
            cumle = cumle.Replace("   ", " ");
            cumle = cumle.Trim();

            string[] kelimeler = cumle.Split(' ');

            List<string> temizanaliz = new List<string>();

            for (int i = 0; i < kelimeler.Length; i++)
            {
                if (!stopwords.Contains(kelimeler[i]))
                {
                    temizanaliz.Add(kelimeler[i]);
                }
            }

            Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());

            Kelime[] kokdurumlar;

            for (int i = 0; i < temizanaliz.Count; i++)
            {
                try
                {
                    kokdurumlar = zemberek.kelimeCozumle(temizanaliz[i]);
                }
                catch
                {
                    continue;
                }

                for (int j = 0; j < kokdurumlar.Length; j++)
                {
                    if (kokdurumlar[j].kok().tip() == KelimeTipi.ISIM)
                    {
                        System.Diagnostics.Debug.WriteLine(kokdurumlar[j].kok().icerik());
                        kelimefrekanslar.Add(new KelimeFrekans { Kelime = kokdurumlar[j].kok().icerik(), Frekans = 1 });
                        break;
                    }
                }
            }

            return kelimefrekanslar;
        }

        void KelimeKaydet(string kelime)
        {
            FileStream fs = new FileStream(Server.MapPath("kelimeler.txt"), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamReader sr;
            StreamWriter sw;

            List<KelimeFrekans> kelimeler = new List<KelimeFrekans>();

            bool kontrol = false;

            if (fs.Length != 0)
            {
                sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    string k = sr.ReadLine();
                    string[] s = k.Split(' ');
                    kelimeler.Add(new KelimeFrekans { Kelime = s[0], Frekans = int.Parse(s[1]) });
                }

                sr.Close();
            }
            else
            {
                sw = new StreamWriter(fs);
                sw.WriteLine(kelime + " 1");
                sw.Flush();
                sw.Close();
                return;
            }

            for (int i = 0; i < kelimeler.Count; i++)
            {
                if (kelimeler[i].Kelime == kelime)
                {
                    kelimeler[i].Frekans++;
                    kontrol = true;
                    break;
                }
            }

            if (!kontrol)
                kelimeler.Add(new KelimeFrekans { Kelime = kelime, Frekans = 1 });

            KelimeFrekans[] kf = kelimeler.ToArray();

            fs = new FileStream(Server.MapPath("kelimeler.txt"), FileMode.Truncate, FileAccess.ReadWrite);
            sw = new StreamWriter(fs);

            for (int i = 0; i < kf.Length; i++)
            {
                sw.WriteLine(kf[i].Kelime + " " + kf[i].Frekans.ToString());
            }

            sw.Flush();
            sw.Close();
        }

        [WebMethod]
        public string[] EnYuksek5FrekansliKelime(string metin)
        {
            metin = Server.UrlDecode(metin);
            //metin = "Anayasa, bir devletin yönetim biçimini belirtir. Toplumların ülke üzerindeki egemenlik haklarının, bireylerin temel haklarının hangi koşullar  altında devlet tarafından kullanılabileceğini belirleyen temel kanunlardır.Devletin temel kurumlarının nasıl işleyeceğini belirler. Genel olarak genel hükümler, temel hak ve özgürlükler, bireylerin topluma karşı görev ve sorumlulukları ile yasama, yürütme, yargı gibi anayasal devlet organlarını tanımlayan bölümlere sahiptir";
            if (File.Exists(Server.MapPath("kelimeler.txt")))
            {
                string[] enyuksek5 = null;
                List<KelimeFrekans> kelimefrekanslar = KelimeAyristir(metin);
                List<KelimeFrekans> dosyakelimefrekanslar = new List<KelimeFrekans>();
                FileStream fs = new FileStream(Server.MapPath("kelimeler.txt"), FileMode.Open, FileAccess.Read);
                if (fs.Length != 0)
                {
                    StreamReader sr = new StreamReader(fs);
                    while (!sr.EndOfStream)
                    {
                        string s = sr.ReadLine();
                        string[] ss = s.Split(' ');
                        dosyakelimefrekanslar.Add(new KelimeFrekans { Kelime = ss[0], Frekans = int.Parse(ss[1]) });
                    }
                    sr.Close();

                    List<KelimeFrekans> ayikla_pirincin_tasini = new List<KelimeFrekans>();
                    bool control = false;

                    for (int i = 0; i < kelimefrekanslar.Count; i++)
                    {
                        for (int j = 0; j < ayikla_pirincin_tasini.Count; j++)
                        {
                            if (ayikla_pirincin_tasini[j].Kelime == kelimefrekanslar[i].Kelime)
                                control = true;
                        }
                        if (!control)
                            ayikla_pirincin_tasini.Add(kelimefrekanslar[i]);
                        control = false;
                    }

                    control = false;

                    for (int i = 0; i < ayikla_pirincin_tasini.Count; i++)
                    {
                        for (int j = 0; j < dosyakelimefrekanslar.Count; j++)
                        {
                            if (ayikla_pirincin_tasini[i].Kelime == dosyakelimefrekanslar[j].Kelime)
                            {
                                ayikla_pirincin_tasini[i].Frekans = dosyakelimefrekanslar[j].Frekans;
                                control = true;
                            }
                        }

                        if (!control)
                        {
                            ayikla_pirincin_tasini.RemoveAt(i);
                            i = i > 0 ? i - 1 : 0;
                        }

                        control = false;
                    }

                    control = false;

                    if (ayikla_pirincin_tasini.Count == 1)
                    {
                        for (int i = 0; i < dosyakelimefrekanslar.Count; i++)
                        {
                            if (ayikla_pirincin_tasini[0].Kelime == dosyakelimefrekanslar[i].Kelime)
                                control = true;

                        }
                        if (!control)
                            ayikla_pirincin_tasini.Clear();
                    }




                    KelimeFrekans[] kf = ayikla_pirincin_tasini.ToArray();

                    for (int i = 0; i < kf.Length; i++)
                    {
                        for (int j = i + 1; j < kf.Length; j++)
                        {
                            if (kf[i].Frekans < kf[j].Frekans)
                            {
                                KelimeFrekans t = kf[i];
                                kf[i] = kf[j];
                                kf[j] = t;
                            }
                        }
                    }

                    enyuksek5 = new string[5];

                    for (int i = 0; i < enyuksek5.Length && i < kf.Length; i++)
                        enyuksek5[i] = kf[i].Kelime;

                }
                return enyuksek5;
            }
            else
                return null;
        }
    }
}
