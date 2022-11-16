namespace ProjetkPP_UMG_Net7
{
    using System.Reflection.Metadata.Ecma335;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;

#pragma warning disable C8604

    public static class FunkcjeAuto
    {
        /// <summary>
        /// <para>Dostępne foldery :</para>
        /// <para>DaneLogowania,  UrządzeniaInfo,  AbonamentyInfo,  PakietyInfo,  UrządzeniaKlienta,  AbonamentyKlienta,  PakietyKlienta, Klienci</para>
        /// </summary>
        /// <param name="nazwaFolderu"></param>
        /// <param name="IDklienta"></param>
        /// <param name="rozszerzenie"></param>
        /// <returns></returns>
        public static string ŚcieżkaFolderu(string nazwaFolderu, int IDklienta = 1)
        {
            string ścieżka = Directory.GetCurrentDirectory();

            switch (nazwaFolderu)
            {
                case "DaneLogowania":
                case "UrządzeniaInfo":
                case "AbonamentyInfo":
                case "PakietyInfo":
                case "Klienci":
                    ścieżka = Path.Combine(ścieżka, nazwaFolderu);
                    break;

                case "UrządzeniaKlienta":
                case "AbonamentyKlienta":
                case "PakietyKlienta":
                    ścieżka = Path.Combine(ścieżka, ("Klienci\\" + IDklienta + "\\" + nazwaFolderu));
                    break;
            }

            return ścieżka;
        }

        /// <summary>
        /// <para>Dostępne foldery :</para>
        /// <para>DaneLogowania,  UrządzeniaInfo,  AbonamentyInfo,  PakietyInfo,  UrządzeniaKlienta,  AbonamentyKlienta,  PakietyKlienta, Klienci</para>
        /// </summary>
        /// <param name="nazwaFolderu"></param>
        /// <param name="IDklienta"></param>
        /// <param name="rozszerzenie"></param>
        /// <returns></returns>
        public static int LiczbaPlików(string nazwaFolderu, int IDklienta = 1, string rozszerzenie = "*.xml")
        {
            string ścieżka = ŚcieżkaFolderu(nazwaFolderu, IDklienta);
            return Directory.GetFiles(ścieżka, rozszerzenie).Length;
        }
        public static int LiczbaKlientów()
        {
            string ścieżka = ŚcieżkaFolderu("Klienci");

            return Directory.GetDirectories(ścieżka).Length;
        }
        public static int LiczbaUrzytkowników()
        {
            string ścieżka = ŚcieżkaFolderu("DaneLogowania");

            return Directory.GetFiles(ścieżka, "*.xml").Length;
        }

        /// <summary>
        /// <para>Lista ID plików (lub folderów w przypadku opcji "Klienci") w wybranym folderze</para>
        /// <para>Dostępne foldery :</para>
        /// <para>DaneLogowania,  UrządzeniaInfo,  AbonamentyInfo,  PakietyInfo,  UrządzeniaKlienta,  AbonamentyKlienta,  PakietyKlienta, Klienci</para>
        /// </summary>
        /// <returns></returns>
        public static int[] ListaID(string folder)
        {
            int[] lista;
            string ścieżka = ŚcieżkaFolderu(folder);
            string[] nazwy;

            if (folder != "Klienci")
            {
                nazwy = Directory.GetFiles(ścieżka, "*.xml");
            }
            else
            {
                nazwy = Directory.GetDirectories(ścieżka);
            }

            lista = new int[nazwy.Length];
            int i = 0;

            foreach (string nazwa in nazwy)
            {
                string nazwa2 = nazwa.Replace(@".xml", "");
                string nazwa3 = nazwa2.Replace(ścieżka + @"\", "");
                lista[i++] = int.Parse(nazwa3);
            }

            return lista;
        }

        public static int NajwiększeIDUrzytkowników()                   // ID z danych logowania; pracownicy i admini nie mają własnego folderu klient
        {
            int ID = 0;       // minimalne ID - 1
            string ścieżka = ŚcieżkaFolderu("DaneLogowania");

            if (Directory.Exists(ścieżka))
            {
                foreach (string pk in Directory.GetFiles(ścieżka, "*.xml"))
                {
                    string FN = pk.Replace(".xml", "");
                    FN = FN.Replace(ścieżka + "\\", "");

                    int id1 = int.Parse(FN);
                    if (ID < id1)
                    {
                        ID = id1;
                    }
                }
            }

            return ID;
        }

        public static int NajwiększeIDKlientów()
        {
            int ID = 0;       // minimalne ID - 1
            string ścieżka = ŚcieżkaFolderu("Klienci");

            foreach (string kl in Directory.GetDirectories(ścieżka))
            {
                string FN = kl.Replace(".xml", "");
                FN = FN.Replace(ścieżka + "\\", "");

                if (int.Parse(FN) > ID)
                {
                    ID = int.Parse(FN);
                }
            }
            return ID;
        }

        /// <summary>
        /// Rodzaje przedmiotów : Urządzenia, Abonamenty, Pakiety
        /// </summary>
        /// <param name="IDklienta"></param>
        /// <param name="rodzajPrzedmiodu"></param>
        /// <returns></returns>
        public static int NajwiększeIDPrzedmiotuKlienta(string rodzajPrzedmiotu, int IDklienta)
        {
            int ID = 0;       // minimalne ID - 1
            string ścieżka = ŚcieżkaFolderu((rodzajPrzedmiotu + "Klienta"), IDklienta);

            if (Directory.Exists(ścieżka))
            {
                foreach (string pk in Directory.GetFiles(ścieżka, "*.xml"))
                {
                    string FN = pk.Replace(".xml", "");
                    FN = FN.Replace(ścieżka + "\\", "");

                    int id1 = int.Parse(FN);
                    if (ID < id1)
                    {
                        ID = id1;
                    }
                }
            }

            return ID;
        }

        /// <summary>
        /// Rodzaje przedmiotów : Urządzenia, Abonamenty, Pakiety
        /// </summary>
        /// <param name="IDklienta"></param>
        /// <param name="rodzajPrzedmiodu"></param>
        /// <returns></returns>
        public static int NajwiększeIDOferty(string rodzajPrzedmiotu)
        {
            int ID = 0;       // minimalne ID - 1
            string ścieżka = ŚcieżkaFolderu(rodzajPrzedmiotu + "Info");

            if (Directory.Exists(ścieżka))
            {
                foreach (string pk in Directory.GetFiles(ścieżka, "*.xml"))
                {
                    string FN = pk.Replace(".xml", "");
                    FN = FN.Replace(ścieżka + "\\", "");

                    int id1 = int.Parse(FN);
                    if (ID < id1)
                    {
                        ID = id1;
                    }
                }
            }

            return ID;
        }

        public static void CzyIstniejąWszystkieFoldery()
        {
            string aktualnaŚcieżka = Directory.GetCurrentDirectory();
            string[] nazwyFolderów = { "DaneLogowania", "UrządzeniaInfo", "AbonamentyInfo", "PakietyInfo", "Klienci" };

            foreach (string nazwaF in nazwyFolderów)
            {
                if (!Directory.Exists(aktualnaŚcieżka + @"\" + nazwaF))
                {
                    Directory.CreateDirectory(aktualnaŚcieżka + @"\" + nazwaF);
                }
            }
        }

        /// <summary>
        /// <para>Tworzy plik z załączonej struktury danych, podać kolejno : Strukturę do zapisania, ID służące jako nazwa, ID klienta jeśli jest potrzebne.   </para>
        /// </summary>
        /// <param name="Iteracja"></param>
        /// <param name="ID"></param>
        /// <param name="IDKlienta"></param>
        public static void ZapiszPlik(object Iteracja, int ID, int IDKlienta = -1)
        {
            ZapisywaniePlików zp = new(Iteracja, ID, IDKlienta);
        }

        /// <summary>
        /// <para>Ładuje plik, podać kolejno : nazwę folderu, nazwę folderu, ID służące jako nazwa, ID klienta jeśli jest potrzebnea(dla 3 ostatnich folderów).   </para>
        /// <para>Dostępne foldery :</para>
        /// <para>DaneLogowania,  UrządzeniaInfo,  AbonamentyInfo,  PakietyInfo,  UrządzeniaKlienta,  AbonamentyKlienta,  PakietyKlienta</para>
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="ID"></param>
        /// <param name="IDKlienta"></param>
        /// 
        public static object WczytajPlik(string folder, int ID, int IDKlienta = -1)
        {
            ŁadowaniePlików łp = new(folder, ID, IDKlienta);
            return łp.WczytanyPlik;
        }

        #region WczytywanieWszystkiego
        /// <summary>
        /// <para>Ładuje wszystkie pliki, wystarczy podać pusta tabelą odpowiedniego typu</para>
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="ID"></param>
        /// <param name="IDKlienta"></param>
        public static DaneLogowania[] WczytajWszystkiePliki(DaneLogowania[] pustaInstancja)
        {
            Console.Title = "Telefonia komorkowa";
            string typPliku = "DaneLogowania";
            ŁadowanieWszystkichPlików łwp = new(typPliku);
            pustaInstancja = new DaneLogowania[łwp.ListaDanych.Length];
            int i = 0;

            foreach (object o in łwp.ListaDanych)
            {
                pustaInstancja[i] = (DaneLogowania)o;
                i++;
            }
            return pustaInstancja;
        }

        /// <summary>
        /// <para>Ładuje wszystkie pliki, wystarczy podać pusta tabelą odpowiedniego typu</para>
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="ID"></param>
        /// <param name="IDKlienta"></param>
        public static UrządzeniaInfo[] WczytajWszystkiePliki(UrządzeniaInfo[] pustaInstancja)
        {
            string typPliku = "UrządzeniaInfo";
            ŁadowanieWszystkichPlików łwp = new(typPliku);
            pustaInstancja = new UrządzeniaInfo[łwp.ListaDanych.Length];
            int i = 0;

            foreach (object o in łwp.ListaDanych)
            {
                pustaInstancja[i] = (UrządzeniaInfo)o;
                i++;
            }
            return pustaInstancja;
        }

        /// <summary>
        /// <para>Ładuje wszystkie pliki, wystarczy podać pusta tabelą odpowiedniego typu</para>
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="ID"></param>
        /// <param name="IDKlienta"></param>
        public static AbonamentyInfo[] WczytajWszystkiePliki(AbonamentyInfo[] pustaInstancja)
        {
            string typPliku = "AbonamentyInfo";
            ŁadowanieWszystkichPlików łwp = new(typPliku);
            pustaInstancja = new AbonamentyInfo[łwp.ListaDanych.Length];
            int i = 0;

            foreach (object o in łwp.ListaDanych)
            {
                pustaInstancja[i] = (AbonamentyInfo)o;
                i++;
            }
            return pustaInstancja;
        }

        /// <summary>
        /// <para>Ładuje wszystkie pliki, wystarczy podać pusta tabelą odpowiedniego typu</para>
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="ID"></param>
        /// <param name="IDKlienta"></param>
        public static PakietyInfo[] WczytajWszystkiePliki(PakietyInfo[] pustaInstancja)
        {
            string typPliku = "PakietyInfo";
            ŁadowanieWszystkichPlików łwp = new(typPliku);
            pustaInstancja = new PakietyInfo[łwp.ListaDanych.Length];
            int i = 0;

            foreach (object o in łwp.ListaDanych)
            {
                pustaInstancja[i] = (PakietyInfo)o;
                i++;
            }
            return pustaInstancja;
        }

        /// <summary>
        /// <para>Ładuje wszystkie pliki, wystarczy podać pusta tabelą odpowiedniego typu</para>
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="ID"></param>
        /// <param name="IDKlienta"></param>
        public static UrządzenieKlienta[] WczytajWszystkiePliki(UrządzenieKlienta[] pustaInstancja, int IDKlienta)
        {
            string typPliku = "UrządzeniaKlienta";
            ŁadowanieWszystkichPlików łwp = new(typPliku);
            pustaInstancja = new UrządzenieKlienta[łwp.ListaDanych.Length];
            int i = 0;

            foreach (object o in łwp.ListaDanych)
            {
                pustaInstancja[i] = (UrządzenieKlienta)o;
                i++;
            }
            return pustaInstancja;
        }

        /// <summary>
        /// <para>Ładuje wszystkie pliki, wystarczy podać pusta tabelą odpowiedniego typu</para>
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="ID"></param>
        /// <param name="IDKlienta"></param>
        public static AbonamentKlienta[] WczytajWszystkiePliki(AbonamentKlienta[] pustaInstancja, int IDKlienta)
        {
            string typPliku = "AbonamentyKlienta";
            ŁadowanieWszystkichPlików łwp = new(typPliku);
            pustaInstancja = new AbonamentKlienta[łwp.ListaDanych.Length];
            int i = 0;

            foreach (object o in łwp.ListaDanych)
            {
                pustaInstancja[i] = (AbonamentKlienta)o;
                i++;
            }
            return pustaInstancja;
        }

        /// <summary>
        /// <para>Ładuje wszystkie pliki, wystarczy podać pusta tabelą odpowiedniego typu</para>
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="ID"></param>
        /// <param name="IDKlienta"></param>
        public static PakietKlienta[] WczytajWszystkiePliki(PakietKlienta[] pustaInstancja, int IDKlienta)
        {
            string typPliku = "PakietyKlienta";
            ŁadowanieWszystkichPlików łwp = new(typPliku);
            pustaInstancja = new PakietKlienta[łwp.ListaDanych.Length];
            int i = 0;

            foreach (object o in łwp.ListaDanych)
            {
                pustaInstancja[i] = (PakietKlienta)o;
                i++;
            }
            return pustaInstancja;
        }
        #endregion
    }

    #region Struktury danych
    [Serializable]
    public class UrządzeniaInfo
    {
        public UrządzeniaInfo()
        {
            this.IDvalue = FunkcjeAuto.NajwiększeIDOferty("Urządzenia") + 1;
        }
        public UrządzeniaInfo(int tempID)
        {
            this.IDvalue = tempID;
        }

        private int IDvalue = 1;

        public int ID
        {
            get { return IDvalue; }
            set { IDvalue = value; }
        }

        public double Cena;
        public string Nazwa;
        public string Wytwórca;
        public string[]? Kolory;                                        // niewymagane
        public string[]? Warianty;                                      // niewymagane
    }
    [Serializable]
    public class AbonamentyInfo
    {
        public AbonamentyInfo()
        {
            this.IDvalue = FunkcjeAuto.NajwiększeIDOferty("Abonamenty") + 1;
        }
        public AbonamentyInfo(int tempID)
        {
            this.IDvalue = tempID;
        }

        private int IDvalue = 1;

        public int ID
        {
            get { return IDvalue; }
            set { IDvalue = value; }
        }

        public string Nazwa;
        public string CzęstotliwośćRozliczania;                         // zakładam możliwości: tyg, msc, rok (każde ma 3 litery dla łatwości póżniejszego sprawdzania)
        public double Cena;
        public double LimitInternetu = 0;                               // 0 dla braku, -1 dla nielimitowanego, >0 dla normalneog limitu
        public double[] LimityInternetu = { 0, 0 };                       // prędkość przed i po wyczerpaniu limitu
    }
    [Serializable]
    public class PakietyInfo
    {
        public PakietyInfo()
        {
            this.IDvalue = FunkcjeAuto.NajwiększeIDOferty("Pakiety") + 1;
        }
        public PakietyInfo(int tempID)
        {
            this.IDvalue = tempID;
        }

        private int IDvalue = 1;

        public int ID
        {
            get { return IDvalue; }
            set { IDvalue = value; }
        }

        public double Cena;
        public string Nazwa;
        public int[] TelefonyID;                                        // ID brane z oferty urządzeń
        public int AbonamentID = 0;                                     // ID oferyty przypisanego przy zakupie do telefonów abonamentu
        public double CzasTrwania = 0;                                  // Na ile opłaca abonament, wyrażane w ilości "cykli" abonamentu (np. tygodni jeśli opłacany tygodniowo)
        public double Przecena = 0;                                     // przecena na abonament, w ułamku diesiętnym, 0 dla braku
    }
    [Serializable]
    public class DaneLogowania
    {
        public DaneLogowania()
        {
            this.IDvalue = FunkcjeAuto.NajwiększeIDUrzytkowników() + 1;
        }
        public DaneLogowania(int tempID)
        {
            this.IDvalue = tempID;
        }

        private int IDvalue = 1;

        public int ID
        {
            get { return IDvalue; }
            set { IDvalue = value; }
        }

        public string Login { get; set; }
        public string Hasło { get; set; }
        public string Imię;
        public string Nazwisko;
        public string Email;
        public DateTime DataUrodzenia;
        public bool Admin = false;
    }
    [Serializable]
    public class UrządzenieKlienta
    {
        public UrządzenieKlienta() { }
        public UrządzenieKlienta(int IDKlientaLubPrzedmiotu, bool podanoIDKlienta, int IDOferty)
        {
            if (podanoIDKlienta)
            {
                IDKlientaLubPrzedmiotu = FunkcjeAuto.NajwiększeIDPrzedmiotuKlienta("Urządzenia", IDKlientaLubPrzedmiotu) + 1;
            }
            this.IDvalue = IDKlientaLubPrzedmiotu;
            this.IDOferty = IDOferty;
        }

        private int IDvalue = 1;

        public int ID
        {
            get { return IDvalue; }
            set { IDvalue = value; }
        }

        public int IDOferty;                                            // ID oferty(info) z której pochodzi ten telefon
        public string Kolor = "czarny";                                 // przykładowo
        public string Wariant = "normalny";                             // przykładowo
        public int IDAbonamentu = 0;                                    // jaki ma przypisany abonament (jesli ma)
        public int IDPakietu = 0;                                       // jaki ma przypisany pakiet (jesli ma)
    }
    [Serializable]
    public class AbonamentKlienta
    {
        public AbonamentKlienta() { }
        public AbonamentKlienta(int IDKlientaLubPrzedmiotu, bool podanoIDKlienta, int IDOferty)
        {
            if (podanoIDKlienta)
            {
                IDKlientaLubPrzedmiotu = FunkcjeAuto.NajwiększeIDPrzedmiotuKlienta("Abonamenty", IDKlientaLubPrzedmiotu) + 1;
            }
            this.IDvalue = IDKlientaLubPrzedmiotu;
            this.IDOferty = IDOferty;
            this.NumerTelefonu[0] = 1;
            for (int i = 8; i < this.NumerTelefonu.Length - 1; i++)
            {
                this.NumerTelefonu[i + 1] = 0;
            }
        }

        private int IDvalue = 1;

        public int ID
        {
            get { return IDvalue; }
            set { IDvalue = value; }
        }

        public int IDOferty;                                            // ID oferty(info) z której pochodzi ten abonament
        public int[] NumerTelefonu = new int[9];
        public int NaIleOpłaconoDoPrzodu = 0;                           // czy i na ile opłacone do przodu - ( <0 = zaległość z zapłatą, 0 = za bierząco z opłatą, >0 = opłacone do przodu, np. przez pakiet)
        public DateTime DataNastępnejOpłaty;                            // jeśli opłacono wynosi 0 lub <0, to normalna data zwględem zakupu; jeśli >0, to o ileś okresów płacenia do przodu
        public int IDPakietu = 0;                                       // jaki ma przypisany pakiet (jesli ma)
    }
    [Serializable]
    public class PakietKlienta
    {
        public PakietKlienta() { }
        public PakietKlienta(int IDKlientaLubPrzedmiotu, bool podanoIDKlienta, int IDOferty)
        {
            if (podanoIDKlienta)
            {
                IDKlientaLubPrzedmiotu = FunkcjeAuto.NajwiększeIDPrzedmiotuKlienta("Pakiety", IDKlientaLubPrzedmiotu) + 1;
            }
            this.IDvalue = IDKlientaLubPrzedmiotu;
            this.IDOferty = IDOferty;
        }

        private int IDvalue = 1;

        public int ID
        {
            get { return IDvalue; }
            set { IDvalue = value; }
        }

        public int IDOferty;                                            // ID oferty(info) z której pochodzi ten abonament
    }

    public struct WszystkieDaneKlienta
    {
        public DaneLogowania daneLog;
        public UrządzenieKlienta[] urządzenia;
        public AbonamentKlienta[] abonamenty;
        public PakietKlienta pakiety;
    }
    #endregion

    #region KlasyŁadowaniaIZapisywania
    public class ZapisywaniePlików
    {
        public ZapisywaniePlików(object Iteracja, int ID, int IDKlienta = -1)
        {
            Type typ = Iteracja.GetType();
            XmlSerializer serializer = new(typ);
            string folder = "";

            if (typ.Equals(typeof(DaneLogowania)))
            {
                folder = "DaneLogowania";
            }
            else if (typ.Equals(typeof(UrządzeniaInfo)))
            {
                folder = "UrządzeniaInfo";
            }
            else if (typ.Equals(typeof(AbonamentyInfo)))
            {
                folder = "AbonamentyInfo";
            }
            else if (typ.Equals(typeof(PakietyInfo)))
            {
                folder = "PakietyInfo";
            }
            else if (typ.Equals(typeof(UrządzenieKlienta)))
            {
                folder = "UrządzeniaKlienta";
            }
            else if (typ.Equals(typeof(AbonamentKlienta)))
            {
                folder = "AbonamentyKlienta";
            }
            else if (typ.Equals(typeof(PakietKlienta)))
            {
                folder = "PakietyKlienta";
            }

            string ścieżka = FunkcjeAuto.ŚcieżkaFolderu(folder, IDKlienta);
            Stream stream = new FileStream($@"{ścieżka}\{ID}.xml", FileMode.Create, FileAccess.Write, FileShare.None);

            serializer.Serialize(stream, Iteracja);

            stream.Close();
        }
    }
    public class ŁadowaniePlików
    {
        public object? WczytanyPlik;

        public ŁadowaniePlików(string folder, int ID, int IDKlienta = -1)
        {
            string ścieżka = FunkcjeAuto.ŚcieżkaFolderu(folder, IDKlienta);
            XmlSerializer serializer;
            try
            {
                Stream stream = new FileStream($@"{ścieżka}\{ID}.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                switch (folder)
                {
                    case "DaneLogowania":
                        serializer = new XmlSerializer(typeof(DaneLogowania));
                        this.WczytanyPlik = serializer.Deserialize(stream);
                        break;

                    case "UrządzeniaInfo":
                        serializer = new XmlSerializer(typeof(UrządzeniaInfo));
                        this.WczytanyPlik = serializer.Deserialize(stream);
                        break;

                    case "AbonamentyInfo":
                        serializer = new XmlSerializer(typeof(AbonamentyInfo));
                        this.WczytanyPlik = serializer.Deserialize(stream);
                        break;

                    case "PakietyInfo":
                        serializer = new XmlSerializer(typeof(PakietyInfo));
                        this.WczytanyPlik = serializer.Deserialize(stream);
                        break;

                    case "UrządzeniaKlienta":
                        serializer = new XmlSerializer(typeof(UrządzenieKlienta));
                        this.WczytanyPlik = serializer.Deserialize(stream);
                        break;

                    case "AbonamentyKlienta":
                        serializer = new XmlSerializer(typeof(AbonamentKlienta));
                        this.WczytanyPlik = serializer.Deserialize(stream);
                        break;

                    case "PakietyKlienta":
                        serializer = new XmlSerializer(typeof(PakietKlienta));
                        this.WczytanyPlik = serializer.Deserialize(stream);
                        break;
                }

                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd wczytywania pliku, \n" + e.Message);
            }
        }
    }
    public class ŁadowanieWszystkichPlików
    {
        public object?[] ListaDanych;
        public ŁadowanieWszystkichPlików(string typPliku, int IDKlienta = -1)
        {
            ŁadowaniePlików łp;
            int[] listaID = FunkcjeAuto.ListaID(typPliku);
            this.ListaDanych = new object[listaID.Length];
            int i = 0;

            switch (typPliku)
            {
                case "DaneLogowania":
                    {
                        while (i < listaID.Length)
                        {
                            łp = new ŁadowaniePlików(typPliku, listaID[i], IDKlienta);
                            this.ListaDanych[i] = łp.WczytanyPlik;
                            i++;
                        }
                        break;
                    }
                case "UrządzeniaInfo":
                    {
                        while (i < listaID.Length)
                        {
                            łp = new ŁadowaniePlików(typPliku, listaID[i], IDKlienta);
                            this.ListaDanych[i] = łp.WczytanyPlik;
                            i++;
                        }
                        break;
                    }
                case "AbonamentyInfo":
                    {
                        while (i < listaID.Length)
                        {
                            łp = new ŁadowaniePlików(typPliku, listaID[i], IDKlienta);
                            this.ListaDanych[i] = łp.WczytanyPlik;
                            i++;
                        }
                        break;
                    }
                case "PakietyInfo":
                    {
                        while (i < listaID.Length)
                        {
                            łp = new ŁadowaniePlików(typPliku, listaID[i], IDKlienta);
                            this.ListaDanych[i] = łp.WczytanyPlik;
                            i++;
                        }
                        break;
                    }
                case "UrządzeniaKlienta":
                    {
                        while (i < listaID.Length)
                        {
                            łp = new ŁadowaniePlików(typPliku, listaID[i], IDKlienta);
                            this.ListaDanych[i] = łp.WczytanyPlik;
                            i++;
                        }
                        break;
                    }
                case "AbonamentyKlienta":
                    {
                        while (i < listaID.Length)
                        {
                            łp = new ŁadowaniePlików(typPliku, listaID[i], IDKlienta);
                            this.ListaDanych[i] = łp.WczytanyPlik;
                            i++;
                        }
                        break;
                    }
                case "PakietyKlienta":
                    {
                        while (i < listaID.Length)
                        {
                            łp = new ŁadowaniePlików(typPliku, listaID[i], IDKlienta);
                            this.ListaDanych[i] = łp.WczytanyPlik;
                            i++;
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("  Nie istnieje taki typ pliku");
                        break;
                    }
            }

        }
    }
    #endregion

    internal class Program
    {
        private static readonly string KodAdminów = "123";

        public static void CzyszczenieEkranu()
        {
            Console.Clear();
            Console.WriteLine();
        }
        public static string ZobaczKod()
        {
            return KodAdminów;
        }
        public static void TworzenieUrzytkownika(DaneLogowania dane, int ID, bool tworzyćKlienta)
        {
            FunkcjeAuto.ZapiszPlik(dane, ID);

            if (tworzyćKlienta)
            {
                Directory.CreateDirectory(FunkcjeAuto.ŚcieżkaFolderu("UrządzeniaKlienta", ID));
                Directory.CreateDirectory(FunkcjeAuto.ŚcieżkaFolderu("AbonamentyKlienta", ID));
                Directory.CreateDirectory(FunkcjeAuto.ŚcieżkaFolderu("PakietyKlienta", ID));
            }
        }
        public static DaneLogowania? DodajUżytkownika()
        {
            DaneLogowania nowyU = new();
            DaneLogowania[] listaDL = new DaneLogowania[1];
            listaDL = FunkcjeAuto.WczytajWszystkiePliki(listaDL);
            string[] listaLoginów = new string[listaDL.Length];
            string[] listaEmaili = new string[listaDL.Length];
            int i = 0;
            bool powtórka;

            foreach (DaneLogowania dana in listaDL)
            {
                listaLoginów[i] = dana.Login;
                listaEmaili[i] = dana.Email;
                i++;
            }

            i = 0;


            Console.WriteLine("\n Tworzenie nowego użytkownika: ");

            do
            {
                powtórka = false;

                Console.Write("\n\n\tPodaj login: ");
                nowyU.Login = Console.ReadLine();

                foreach (string login in listaLoginów)
                {
                    if (login == nowyU.Login)
                    {
                        powtórka = true;
                        Console.Write("\n\n Login zajęty. ");

                        if ((i++) % 3 == 0)
                        {
                            Console.Write("Jeśli chcesz wyjsc wcisnij Escape (esc): ");
                            ConsoleKeyInfo kij = Console.ReadKey(true);

                            if (kij.Key == ConsoleKey.Escape)
                            {
                                return null;
                            }
                        }
                        break;
                    }
                }
            }
            while (powtórka);

            Console.Write("\n  Podaj hasło : ");
            nowyU.Hasło = Console.ReadLine();

            i = 0;

            Console.Write("\n  Podaj imię : ");
            nowyU.Imię = Console.ReadLine();

            Console.Write("\n  Podaj nazwisko : ");
            nowyU.Nazwisko = Console.ReadLine();

            do
            {
                powtórka = false;

                Console.Write("\n  Podaj email : ");
                nowyU.Email = Console.ReadLine();

                foreach (string mail in listaEmaili)
                {
                    if (mail == nowyU.Email)
                    {
                        powtórka = true;
                        Console.WriteLine("\n    Email zajęty");

                        if ((i++) % 3 == 0)
                        {
                            Console.Write("      Wciśnij escape (esc) aby wyjść");
                            ConsoleKeyInfo kij = Console.ReadKey(true);

                            if (kij.Key == ConsoleKey.Escape)
                            {
                                return null;
                            }
                        }
                        break;
                    }
                }
            }
            while (powtórka);

            do
            {
                try
                {
                    powtórka = false;
                    Console.Write("\n  Podaj rok urodzenia : ");
                    nowyU.DataUrodzenia = nowyU.DataUrodzenia.AddYears(int.Parse(Console.ReadLine()) - 1);
                }
                catch (Exception)
                {
                    powtórka = true;
                }

            }
            while (powtórka);

            do
            {
                try
                {
                    powtórka = false;
                    Console.Write("\n  Podaj mieisąc urodzenia : ");
                    nowyU.DataUrodzenia = nowyU.DataUrodzenia.AddMonths(int.Parse(Console.ReadLine()) - 1);
                }
                catch (Exception)
                {
                    powtórka = true;
                }

            }
            while (powtórka);

            do
            {
                try
                {
                    powtórka = false;
                    Console.Write("\n  Podaj dzień urodzenia : ");
                    nowyU.DataUrodzenia = nowyU.DataUrodzenia.AddDays(int.Parse(Console.ReadLine()) - 1);
                }
                catch (Exception)
                {
                    powtórka = true;
                }

            }
            while (powtórka);

            i = 0;

            Console.Write("\n  Zarejestrować jako obsługa? Pamiętaj, że obsługa nie ma folderu Klienta(Y/N) : ");
            string admin = Console.ReadLine();

            if (admin.ToUpper() == "Y")
            {

                while (true)
                {
                    if (i >= 5)
                    {
                        Console.WriteLine("    Przekroczono liczbę prób, rejestrowanie jako klient");
                        break;
                    }

                    Console.Write("\n  Podaj kod : ");
                    string? kod = Console.ReadLine();
                    i++;

                    if (kod == ZobaczKod())
                    {
                        nowyU.Admin = true;
                        break;
                    }
                    else if (kod == "exit")
                    {
                        break;
                    }
                    else if (i % 3 == 1)
                    {
                        Console.Write("      Kontunuować? Wpisz \"exit\" aby wyjść ");
                        ConsoleKeyInfo kij = Console.ReadKey(true);

                        if (kij.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }
                }
            }

            TworzenieUrzytkownika(nowyU, nowyU.ID, !nowyU.Admin);

            return nowyU;
        }
        public static DaneLogowania? Login()
        {
            DaneLogowania[] listaDL = null;
            listaDL = FunkcjeAuto.WczytajWszystkiePliki(listaDL);
            DaneLogowania? danaZalogowania = new();
            bool powtórka;

            CzyszczenieEkranu();
            Thread.Sleep(100);
            Console.WriteLine("\n Wpisz \"utwórz\" w miejsce loginu lub hasła, aby stworzyć nowy profil ");
            Console.WriteLine();

            do
            {
                powtórka = false;

                Console.Write("\n\tPodaj login: ");
                string? login = Console.ReadLine();

                if (login == "utwórz")
                {
                    CzyszczenieEkranu();
                    Thread.Sleep(100);
                    danaZalogowania = DodajUżytkownika();
                    if (danaZalogowania == null)
                    {

                        Console.Write("\n\n Zrezygnowano z tworzenia użytkownika, kontynuować logowanie? (Y/N): ");
                        string kij = Console.ReadLine();
                        Console.WriteLine("\n");

                        if (kij.ToLower() != "n")
                        {
                            powtórka = true;
                            continue;
                        }
                    }
                    break;
                }

                Console.Write("\n\tPodaj hasło: ");
                string? hasło = Console.ReadLine();

                if (hasło == "utwórz")
                {
                    danaZalogowania = DodajUżytkownika();
                    if (danaZalogowania == null)
                    {

                        Console.Write(" Zrezygnowano z tworzenia urzytkownika, kontynuować logowanie? (Y/N) ");
                        ConsoleKeyInfo kij = Console.ReadKey();
                        Console.WriteLine();

                        if (kij.Key != ConsoleKey.N)
                        {
                            powtórka = true;
                            continue;
                        }
                    }
                    break;
                }

                Console.WriteLine();

                if (listaDL.Length == 0)
                {
                    Console.WriteLine(" Nie istnieją żadni urzytkownicy, utwórz nowego urzytkownika podając \"utwórz\" w miejsce loginu lub hasła ");
                    powtórka = true;
                }
                else if (!powtórka)
                {
                    foreach (DaneLogowania dana in listaDL)
                    {
                        if (dana.Login == login && dana.Hasło == hasło)
                        {
                            danaZalogowania = dana;
                            powtórka = false;
                            return danaZalogowania;
                        }
                        else
                        {
                            powtórka = true;
                        }
                    }

                    Console.WriteLine("\n Błedny login lub hasło\n");
                }
            }
            while (powtórka);

            return danaZalogowania;
        }

        #region Funckcje Użytkowników
        public static void TwojeInformacje(ref DaneLogowania twojeDane)             // 1
        {
            CzyszczenieEkranu();
            Thread.Sleep(100);

            Console.Write(" Menu -> Wyświetl lub modyfikuj informacje o sobie\n\n\n");

            Console.Write(" Twoje dane:\n\n");

            Console.WriteLine("\n\tImie: " + twojeDane.Imię);
            Console.WriteLine("\n\tNazwisko: " + twojeDane.Nazwisko);
            Console.WriteLine($"\n\tData urodzenia: {twojeDane.DataUrodzenia.Day}-{twojeDane.DataUrodzenia.Month}-{twojeDane.DataUrodzenia.Year}");
            Console.WriteLine($"\n\tEmail: {twojeDane.Email}");

            string wybor;
            string komunikat = "\n\n Czy chcesz edytowac dane? (Y/N): ";

            do
            {

                Console.Write(komunikat);
                wybor = Console.ReadLine();
                CzyszczenieEkranu();
                Thread.Sleep(100);

                if (wybor.ToUpper() == "Y")
                {
                    bool powtórka = false;
                    Console.Write(" Menu -> Wyświetl lub modyfikuj informacje o sobie\n\n\n");
                    Console.Write(" Które dane chcesz edytowac?\n\n\t1. Imie\n\t2. Nazwisko\n\t3. Data urodzenia\n\t4. Email\n\n Wybierz numer z listy: ");
                    int coEdytowac = int.Parse(Console.ReadLine());
                    CzyszczenieEkranu();
                    Thread.Sleep(100);

                    Console.Write(" Menu -> Wyświetl lub modyfikuj informacje o sobie -> edycja danych\n\n\n");

                    switch (coEdytowac)
                    {
                        case 1:

                            Console.Write("    Wprowadz imie: ");
                            twojeDane.Imię = Console.ReadLine();
                            Console.WriteLine();
                            break;

                        case 2:

                            Console.Write("    Wprowadz nazwisko: ");
                            twojeDane.Nazwisko = Console.ReadLine();
                            Console.WriteLine();
                            break;

                        case 3:

                            DateTime czas = new DateTime();
                            twojeDane.DataUrodzenia = czas;


                            do
                            {
                                try
                                {
                                    powtórka = false;
                                    Console.Write("    Wprowadz datę: \n\n\trok urodzenia: ");
                                    twojeDane.DataUrodzenia = twojeDane.DataUrodzenia.AddYears(int.Parse(Console.ReadLine()) - 1);
                                }
                                catch (Exception)
                                {
                                    powtórka = true;
                                }

                            }
                            while (powtórka);

                            do
                            {
                                try
                                {
                                    powtórka = false;
                                    Console.Write("\n\tmiesiac urodzenia: ");
                                    twojeDane.DataUrodzenia = twojeDane.DataUrodzenia.AddMonths(int.Parse(Console.ReadLine()) - 1);
                                }
                                catch (Exception)
                                {
                                    powtórka = true;
                                }

                            }
                            while (powtórka);

                            do
                            {
                                try
                                {
                                    powtórka = false;
                                    Console.Write("\n\tdzien urodzenia: ");
                                    twojeDane.DataUrodzenia = twojeDane.DataUrodzenia.AddDays(int.Parse(Console.ReadLine()) - 1);
                                    Console.WriteLine();
                                }
                                catch (Exception)
                                {
                                    powtórka = true;
                                }

                            }
                            while (powtórka);
                            break;

                        case 4:

                            Console.Write("    Wprowadz email: ");
                            twojeDane.Email = Console.ReadLine();
                            Console.WriteLine();
                            break;



                    }
                    komunikat = "\n Czy chcesz edytować inne dane? (Y/N): ";
                }
            } while (wybor != "N");

            DaneLogowania doZapisania = twojeDane;
            FunkcjeAuto.ZapiszPlik(doZapisania, doZapisania.ID);

        }
        public static void InfoOTwoichUrz(DaneLogowania twojeDane)                  // 2
        {
            Console.WriteLine("Wyświetlanie informacji o twoich urządzeniach.\n");

        }
        public static void InfoOTwoichAbo(DaneLogowania twojeDane)                  // 3
        {
            Console.WriteLine("Wyświetlanie informacji o twoich abonamentach.\n");

        }
        public static void InfoOTwoichPak(DaneLogowania twojeDane)                  // 4
        {
            Console.WriteLine("Wyświetlanie informacji o twoich pakietach.\n");

        }
        public static void InfoOOfertachUrz()                                       // 5
        {
            UrządzeniaInfo[] ui = new UrządzeniaInfo[3];
            ui = FunkcjeAuto.WczytajWszystkiePliki(ui);

            Console.WriteLine("Wyświetlanie dostępnych ofert Urządzeń");

        }
        public static void InfoOOfertachAbo()                                       // 6
        {
            Console.WriteLine("Wyświetlanie dostępnych ofert Abonamntów");

        }
        public static void InfoOOfertachPak()                                       // 7
        {
            Console.WriteLine("Wyświetlanie dostępnych ofert Pakietów");

        }
        #endregion

        #region Funkcje Admina
        public static void InfoOUrzKlientów()                                       // 1
        {
            Console.WriteLine("Wyświetlanie informacji o urządzeniach klientów");

            DaneLogowania[] daneLog = new DaneLogowania[1];
            daneLog = FunkcjeAuto.WczytajWszystkiePliki(daneLog);

            int[] listaIDKlientów;
            List<int> tempListaID = new List<int>();

            foreach (DaneLogowania dl in daneLog)
            {
                if (!dl.Admin)
                {
                    tempListaID.Append(dl.ID);
                }
            }

            listaIDKlientów = tempListaID.ToArray();

            if (listaIDKlientów.Length == 0)
            {
                CzyszczenieEkranu();
                Console.WriteLine("Nie istnieją żadni Klienci");
                Thread.Sleep(2000);
                CzyszczenieEkranu();
                return;
            }

            WszystkieDaneKlienta[] daneKlientów;


        }
        public static void InfoOAboKlientów()                                       // 2
        {
            Console.WriteLine("Wyświetlanie informacji o abonamntach klientów");

        }
        public static void InfoOPakKlientów()                                       // 3
        {
            Console.WriteLine("Wyświetlanie informacji o pakietach klientów");

        }
        public static void ModyfikacjaOfertUrz()                                    // 4
        {
            Console.WriteLine("Modyfikujacja dostepnych ofert Urządzeń");

        }
        public static void ModyfikacjaOfertAbo()                                    // 5
        {
            Console.WriteLine("Modyfikujacja dostepnych ofert Abonamentów");

        }
        public static void ModyfikacjaOfertPak()                                    // 6
        {
            Console.WriteLine("Modyfikujacja dostepnych ofert Pakietów");

        }
        public static void DodajOfertęUrz()                                         // 7
        {

            Console.WriteLine("Dodawanie nowej oferty Urzadzenia");

            UrządzeniaInfo noweUrz = new();
            UrządzeniaInfo[] listaUrz = new UrządzeniaInfo[1];
            listaUrz = FunkcjeAuto.WczytajWszystkiePliki(listaUrz);
            string[] listaNazw = new string[listaUrz.Length];
            int i = 0;
            bool powtórka;

            foreach (UrządzeniaInfo dana in listaUrz)
            {
                listaNazw[i] = dana.Nazwa;
                i++;
            }

            i = 0;



            Console.WriteLine("\n\n W dowolnym momencie wpisz \"wyjdz\" aby wyjść do menu");
            Console.WriteLine("\n Tworzenie nowego urzadzenia : ");

            do
            {
                powtórka = false;

                Console.Write("\n  Podaj nazwe: ");
                noweUrz.Nazwa = Console.ReadLine();

                if (noweUrz.Nazwa.ToLower() == "wyjdz")
                {
                    return;
                }

                foreach (string nazwa in listaNazw)
                {
                    if (nazwa == noweUrz.Nazwa)
                    {
                        powtórka = true;
                        Console.Write("\n    Nazwa juz istnieje");

                        break;
                    }
                }
            }
            while (powtórka);

            do
            {
                try
                {
                    powtórka = false;
                    Console.Write("\n  Podaj cene: ");
                    string wprowadzono = Console.ReadLine();


                    if (wprowadzono.ToLower() == "wyjdz")
                    {
                        return;
                    }

                    noweUrz.Cena = int.Parse(wprowadzono);

                    if (noweUrz.Cena < 0)
                    {
                        powtórka = true;
                    }
                }
                catch (Exception)
                {
                    powtórka = true;
                }

            }
            while (powtórka);

            i = 0;

            Console.Write("\n  Podaj wytwórce: ");
            noweUrz.Wytwórca = Console.ReadLine();

            if (noweUrz.Wytwórca.ToLower() == "wyjdz")
            {
                return;
            }

            Console.Write("\n  Podaj liczbę wariantów: ");
            int ilośćWariantów = int.Parse(Console.ReadLine());
            noweUrz.Warianty = new string[ilośćWariantów];

            for (int j = 0; j < ilośćWariantów; j++)
            {
                Console.Write($"Podaj {j + 1} wariant:");
                noweUrz.Warianty[j] = Console.ReadLine();

                if (noweUrz.Warianty[j].ToLower() == "wyjdz")
                {
                    return;
                }
            }

            Console.Write("\n  Podaj liczbę kolorów: ");
            int ilośćKolorów = int.Parse(Console.ReadLine());
            noweUrz.Kolory = new string[ilośćKolorów];

            for (int j = 0; j < ilośćKolorów; j++)
            {
                Console.Write($"Podaj {j + 1} wariant: ");
                noweUrz.Kolory[j] = Console.ReadLine();

                if (noweUrz.Kolory[j].ToLower() == "wyjdz")
                {
                    return;
                }
            }

            FunkcjeAuto.ZapiszPlik(noweUrz, noweUrz.ID);



        }
        public static void DodajOfertęAbo()                                         // 8
        {
            Console.WriteLine("Dodawanie nowej oferty Abonamentu");

        }
        public static void DodajOfertęPak()                                         // 9
        {
            Console.WriteLine("Dodawanie nowej oferty Pakietu");

        }
        #endregion

        static void Main(string[] args)
        {

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            bool zostań = true;

            FunkcjeAuto.CzyIstniejąWszystkieFoldery();

            do
            {
                DaneLogowania? zalogowanyUrzytkownik = Login();

                if (zalogowanyUrzytkownik == null)
                {
                    Console.WriteLine("Anulowano logowanie");
                    return;
                }

                do
                {
                    zostań = true;
                    CzyszczenieEkranu();

                    if (!zalogowanyUrzytkownik.Admin)
                    {
                        Console.Write(" Start -> Menu \t\t |Zalogowano jako: " + zalogowanyUrzytkownik.Imię + "|");
                        Console.WriteLine("\n\n\n Dostępne opcje: " +
                            "\n\n\t 1 - Wyświetl lub modyfikuj informacje o sobie" +
                            "\n\t 2 - Wyświetl informacje o swoich urządzeniach" +
                            "\n\t 3 - Wyświetl informacje o swoich abonamntach" +
                            "\n\t 4 - Wyświetl informacje o swoich pakietach \n" +

                            "\n\t 5 - Pokaż dostepne oferty Urządzeń" +
                            "\n\t 6 - Pokaż dostepne oferty Abonamentów" +
                            "\n\t 7 - Pokaż dostepne oferty Pakietów \n" +

                            "\n\t Wyloguj - Wyloguj się ze swojego konta " +
                            "\n\t Wyjdz - Wyjdź z programu");
                        Console.Write("\n\n\t");

                        string? opcja = Console.ReadLine();
                        if (opcja == null)
                        {
                            opcja = "";
                        }

                        opcja = opcja.Replace(" ", "");
                        opcja = opcja.ToLower();

                        switch (opcja)
                        {
                            case ("1"):
                                {
                                    TwojeInformacje(ref zalogowanyUrzytkownik);
                                    break;
                                }
                            case ("2"):
                                {
                                    InfoOTwoichUrz(zalogowanyUrzytkownik);
                                    break;
                                }
                            case ("3"):
                                {
                                    InfoOTwoichAbo(zalogowanyUrzytkownik);
                                    break;
                                }
                            case ("4"):
                                {
                                    InfoOTwoichPak(zalogowanyUrzytkownik);
                                    break;
                                }
                            case ("5"):
                                {
                                    InfoOOfertachUrz();
                                    break;
                                }
                            case ("6"):
                                {
                                    InfoOOfertachAbo();
                                    break;
                                }
                            case ("7"):
                                {
                                    InfoOOfertachPak();
                                    break;
                                }
                            case ("wyloguj"):
                                {
                                    zostań = false;
                                    break;
                                }
                            case ("wyjdz"):
                                {
                                    zostań = false;
                                    return;
                                }
                            default:
                                {
                                    Console.WriteLine("Błędna komenda");
                                    break;
                                }
                        }
                    }
                    else
                    {
                        Console.Write(" Start -> Menu \t\t |Zalogowano jako Admin: " + zalogowanyUrzytkownik.Imię + "|");
                        Console.WriteLine("\n\n\n Dostępne opcje: " +
                            "\n\n\t 1 - Wyświetl informacje o urządzeniach klientów" +
                            "\n\t 2 - Wyświetl informacje o abonamntach klientów" +
                            "\n\t 3 - Wyświetl informacje o pakietach klientów \n" +

                            "\n\t 4 - Modyfikuj dostepne oferty Urządzeń" +
                            "\n\t 5 - Modyfikuj dostepne oferty Abonamentów" +
                            "\n\t 6 - Modyfikuj dostepne oferty Pakietów \n" +

                            "\n\t 7 - Dodaj nową ofertę Urządzenia" +
                            "\n\t 8 - Dodaj nową ofertę Abonamentu" +
                            "\n\t 9 - Dodaj nową ofertę Pakietu \n" +

                            "\n\t Wyloguj - Wyloguj się ze swojego konta " +
                            "\n\t Wyjdź - Wyjdź z programu");
                        Console.Write("\n\n\t");

                        string? opcja = Console.ReadLine();
                        if (opcja == null)
                        {
                            opcja = "";
                        }

                        opcja = opcja.Replace("ź", "z");
                        opcja = opcja.Replace(" ", "");
                        opcja = opcja.ToLower();

                        switch (opcja)
                        {
                            case ("1"):
                                {
                                    InfoOUrzKlientów();
                                    break;
                                }
                            case ("2"):
                                {
                                    InfoOAboKlientów();
                                    break;
                                }
                            case ("3"):
                                {
                                    InfoOPakKlientów();
                                    break;
                                }
                            case ("4"):
                                {
                                    ModyfikacjaOfertUrz();
                                    break;
                                }
                            case ("5"):
                                {
                                    ModyfikacjaOfertAbo();
                                    break;
                                }
                            case ("6"):
                                {
                                    ModyfikacjaOfertPak();
                                    break;
                                }
                            case ("7"):
                                {
                                    DodajOfertęUrz();
                                    break;
                                }
                            case ("8"):
                                {
                                    DodajOfertęAbo();
                                    break;
                                }
                            case ("9"):
                                {
                                    DodajOfertęPak();
                                    break;
                                }
                            case ("wyloguj"):
                                {
                                    zostań = false;
                                    zalogowanyUrzytkownik = null;
                                    break;
                                }
                            case ("wyjdz"):
                                {
                                    zostań = false;
                                    return;
                                }
                            default:
                                {
                                    Console.WriteLine("Błędna komenda");
                                    break;
                                }
                        }
                    }

                }
                while (zostań);
            }
            while (true);

        }
    }
}