using dziennik;

namespace Testy_Jednostkowe
{
    [TestClass]
    public sealed class StudentTest
    {
        [TestMethod]
        public void TestWalidacjaPeselu()
        {
            Assert.ThrowsException<ZlyPeselException>(() => { var student = new Student("Adam", "Łukasik", "123"); });
            Assert.ThrowsException<ZlyPeselException>(() => { var student = new Student("Adam", "Łukasik", "123abc"); });
        }

        [TestMethod]
        public void TestNumerAlbumu()
        {
            var student = new Student("Adam", "Łukasik", "12345678911");
            var numerAlbumu = student.NumerAlbumu;

            Assert.IsTrue(numerAlbumu.EndsWith("8911"));
        }

        [TestMethod]
        public void TestDodajUsunPrzedmiot()
        {
            var student = new Student("Adam", "Łukasik", "12345678911");

            var prowadzacy = new Prowadzacy("Jacek", "Wolak", "11122233311", EnumTytulNaukowy.Doktor);

            var przedmiot = new Przedmiot("Wstęp do analizy danych", prowadzacy, 3);

            student.DodajPrzedmiot(przedmiot);

            Assert.IsTrue(student.PrzedmiotyOceny.Any(x => x.Przedmiot == przedmiot));

            student.UsunPrzedmiot(przedmiot);

            Assert.IsFalse(student.PrzedmiotyOceny.Any(x => x.Przedmiot == przedmiot));
        }


        [TestMethod]
        public void TestCompareTo()
        {
            var s1 = new Student("Adam", "Łukasik", "12345678911");
            var s2 = new Student("Tomasz", "Król", "11122244412");

            Assert.IsTrue(s1.CompareTo(s2) != 0);
        }

        [TestMethod]
        public void TestDodajOcene()
        {
            var student = new Student("Adam", "Łukasik", "12345678911");

            var prowadzacy = new Prowadzacy("Jacek", "Wolak", "11122233311", EnumTytulNaukowy.Doktor);

            var przedmiot = new Przedmiot("Wstęp do analizy danych", prowadzacy, 3);

            student.DodajPrzedmiot(przedmiot);

            student.DodajOcene(przedmiot, 4.0);

            var zapis = student.PrzedmiotyOceny.First(x => x.Przedmiot == przedmiot);

            Assert.AreEqual(1, zapis.Oceny.Count);
            Assert.AreEqual(4.0, zapis.Oceny[0].Wartosc);

        }
    }

    [TestClass]
    public sealed class OcenaTest
    {
        [TestMethod]
        public void TestWalidacjaOceny()
        {
            var prowadzacy = new Prowadzacy("Jacek", "Wolak", "11122233311", EnumTytulNaukowy.Doktor);
            var przedmiot = new Przedmiot("Wstęp do analizy danych", prowadzacy, 3);
            double blednaOcena = 8.0;

            Assert.ThrowsException<NiepoprawnaOcenaException>(() => { var o1 = new Ocena(przedmiot, blednaOcena); });
        }
    }

    [TestClass]
    public sealed class ProwadzacyTest
    {
        [TestMethod]
        public void TestZnajdzPrzedmiotyProwadzacego()
        {
            Uczelnia uczelnia = new Uczelnia();

            var szukanyProwadzacy = new Prowadzacy("Jan", "Kowalski", "11111111111", EnumTytulNaukowy.Doktor);
            var innyProwadzacy = new Prowadzacy("Anna", "Nowak", "22222222222", EnumTytulNaukowy.Magister);

            var p1 = new Przedmiot("Matematyka", szukanyProwadzacy, 5);
            var p2 = new Przedmiot("Fizyka", innyProwadzacy, 3);
            var p3 = new Przedmiot("Informatyka", szukanyProwadzacy, 4);

            var semestr = new Semestr(2024, EnumTyp.Zimowy);
            semestr.DodajPrzedmiot(p1);
            semestr.DodajPrzedmiot(p2);
            semestr.DodajPrzedmiot(p3);

            var kierunek = new Kierunek("Informatyka");
            kierunek.DodajSemestr(semestr);

            uczelnia.DodajKierunek(kierunek);

            var test = szukanyProwadzacy.ZnajdzPrzedmiotyProwadzacego(uczelnia);

            Assert.AreEqual(2, test.Count);

            Assert.IsTrue(test.Any(x => x.Przedmiot.Nazwa == "Matematyka"));
            Assert.IsTrue(test.Any(x => x.Przedmiot.Nazwa == "Informatyka"));
            Assert.IsFalse(test.Any(x => x.Przedmiot.Nazwa == "Fizyka"));
        }
    }

    [TestClass]
    public sealed class PrzedmiotOcenyTest
    {
        [TestMethod]
        public void TestSredniaOcen()
        {
            var student = new Student("Adam", "Łukasik", "12345678911");
            var prowadzacy = new Prowadzacy("Jacek", "Wolak", "11122233311", EnumTytulNaukowy.Doktor);
            var przedmiot = new Przedmiot("Wstęp do analizy danych", prowadzacy, 6);

            student.DodajPrzedmiot(przedmiot);

            var przedmiotOceny = student.PrzedmiotyOceny.First(p => p.Przedmiot == przedmiot);

            Assert.AreEqual(0.0, przedmiotOceny.SredniaOcen());

            student.DodajOcene(przedmiot, 2.0);
            student.DodajOcene(przedmiot, 3.0);
            student.DodajOcene(przedmiot, 4.0);
            student.DodajOcene(przedmiot, 5.0);
            student.DodajOcene(przedmiot, 3.5);
            student.DodajOcene(przedmiot, 4.5);

            przedmiotOceny = student.PrzedmiotyOceny.First(p => p.Przedmiot == przedmiot);

            Assert.AreEqual(22.0 / 6.0, przedmiotOceny.SredniaOcen(), 0.001);
        }

    }

    [TestClass]
    public sealed class XMLFileManagerTest
    {
        const string test = "test_uczelnia_temp.xml";

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(test))
            {
                File.Delete(test);
            }
        }

        [TestMethod]
        public void TestZapiszWczytajXML()
        {
            Uczelnia uczelnia = new Uczelnia();

            var k1 = new Kierunek("Informatyka");
            var s1 = new Student("Jan", "Kowalski", "11122233312");
            var p1 = new Prowadzacy("Anna", "Nowak", "12345678911", EnumTytulNaukowy.Doktor);

            uczelnia.DodajKierunek(k1);
            uczelnia.DodajStudenta(s1);
            uczelnia.DodajProwadzacego(p1);

            XMLFileManager.Zapisz(uczelnia, test);

            Assert.IsTrue(File.Exists(test));

            Uczelnia? wczytanaUczelnia = XMLFileManager.Wczytaj(test);

            Assert.IsNotNull(wczytanaUczelnia);

            Assert.AreEqual(1, wczytanaUczelnia.Studenci.Count);
            Assert.AreEqual("Jan", wczytanaUczelnia.Studenci[0].Imie);
            Assert.AreEqual("Kowalski", wczytanaUczelnia.Studenci[0].Nazwisko);

            Assert.AreEqual(1, wczytanaUczelnia.Prowadzacy.Count);
            Assert.AreEqual(EnumTytulNaukowy.Doktor, wczytanaUczelnia.Prowadzacy[0].TytulNaukowy);

            Assert.AreEqual(1, wczytanaUczelnia.Kierunki.Count);
            Assert.AreEqual("Informatyka", wczytanaUczelnia.Kierunki[0].NazwaKierunku);
        }
    }
    [TestClass]
    public class UczelniaTest
    {

        [TestMethod]
        public void TestDodajKierunek_Sukces()
        {
            var uczelnia = new Uczelnia();
            var kierunek = new Kierunek("Informatyka");

            uczelnia.DodajKierunek(kierunek);

            Assert.AreEqual(1, uczelnia.Kierunki.Count);
        }


        [TestMethod]
        public void TestDodajStudenta_Sukces()
        {
            var uczelnia = new Uczelnia();

            var s1 = new Student("Jan", "Kowalski", "99010112345");

            uczelnia.DodajStudenta(s1);

            Assert.AreEqual(1, uczelnia.Studenci.Count);
        }

        [TestMethod]
        public void TestDodajStudenta_WyjatekDuplikat()
        {
            var uczelnia = new Uczelnia();
            var s1 = new Student("Jan", "Kowalski", "99010112345");

            var s2 = new Student("Jan", "Inny", "99010112345");

            uczelnia.DodajStudenta(s1);


            var ex = Assert.ThrowsException<Exception>(() => uczelnia.DodajStudenta(s2));
            Assert.IsTrue(ex.Message.Contains("już istnieje"));
        }


        [TestMethod]
        public void TestDodajProwadzacego_Sukces()
        {
            var uczelnia = new Uczelnia();

            var p1 = new Prowadzacy("Piotr", "Profesor", "55050511111", EnumTytulNaukowy.Profesor);

            uczelnia.DodajProwadzacego(p1);

            Assert.AreEqual(1, uczelnia.Prowadzacy.Count);
        }

        [TestMethod]
        public void TestDodajProwadzacego_WyjatekDuplikat()
        {
            var uczelnia = new Uczelnia();
            var p1 = new Prowadzacy("Piotr", "Nowak", "55050511111", EnumTytulNaukowy.Profesor);

            var p2 = new Prowadzacy("Adam", "Kowal", "55050511111", EnumTytulNaukowy.Doktor);

            uczelnia.DodajProwadzacego(p1);

            Assert.ThrowsException<Exception>(() => uczelnia.DodajProwadzacego(p2));
        }

        [TestMethod]
        public void TestSortujProwadzacychPoTytule()
        {
            var uczelnia = new Uczelnia();

            var pMgr = new Prowadzacy("Marek", "Magister", "11111111111", EnumTytulNaukowy.Magister);
            var pProf = new Prowadzacy("Paweł", "Profesor", "33333333333", EnumTytulNaukowy.Profesor);
            var pDr = new Prowadzacy("Damian", "Doktor", "22222222222", EnumTytulNaukowy.Doktor);

            uczelnia.DodajProwadzacego(pMgr);
            uczelnia.DodajProwadzacego(pProf);
            uczelnia.DodajProwadzacego(pDr);

            uczelnia.SortujProwadzacychPoTytule();

            Assert.AreEqual(EnumTytulNaukowy.Profesor, uczelnia.Prowadzacy[0].TytulNaukowy);
            Assert.AreEqual(EnumTytulNaukowy.Doktor, uczelnia.Prowadzacy[1].TytulNaukowy);
            Assert.AreEqual(EnumTytulNaukowy.Magister, uczelnia.Prowadzacy[2].TytulNaukowy);
        }



        [TestMethod]
        public void TestSortujStudentow_Alfabetycznie()
        {

            var uczelnia = new Uczelnia();


            var sZygmunt = new Student("Zygmunt", "Zarazek", "33333333333");
            var sAdam = new Student("Adam", "Adamski", "11111111111");
            var sKasia = new Student("Katarzyna", "Kowalska", "22222222222");


            uczelnia.DodajStudenta(sZygmunt);
            uczelnia.DodajStudenta(sAdam);
            uczelnia.DodajStudenta(sKasia);


            uczelnia.SortujStudentow();

            Assert.AreEqual("Adamski", uczelnia.Studenci[0].Nazwisko);
            Assert.AreEqual("Kowalska", uczelnia.Studenci[1].Nazwisko);
            Assert.AreEqual("Zarazek", uczelnia.Studenci[2].Nazwisko);
        }

        [TestMethod]
        public void TestSortujProwadzacych_Alfabetycznie()
        {

            var uczelnia = new Uczelnia();

            var p1 = new Prowadzacy("Jan", "Zieliński", "99999999999", EnumTytulNaukowy.Doktor);
            var p2 = new Prowadzacy("Anna", "Baka", "88888888888", EnumTytulNaukowy.Profesor);

            uczelnia.DodajProwadzacego(p1);
            uczelnia.DodajProwadzacego(p2);

            uczelnia.SortujProwadzacych();

            Assert.AreEqual("Baka", uczelnia.Prowadzacy[0].Nazwisko);
            Assert.AreEqual("Zieliński", uczelnia.Prowadzacy[1].Nazwisko);
        }

        [TestMethod]
        public void TestUsunKierunek_Sukces()
        {
            var uczelnia = new Uczelnia();
            var k = new Kierunek("Informatyka");
            uczelnia.DodajKierunek(k);

            uczelnia.UsunKierunek(k);

            Assert.AreEqual(0, uczelnia.Kierunki.Count);
        }

        [TestMethod]
        public void TestUsunKierunek_WyjatekNieistnieje()
        {

            var uczelnia = new Uczelnia();
            var k1 = new Kierunek("Informatyka");

            var ex = Assert.ThrowsException<Exception>(() => uczelnia.UsunKierunek(k1));
            Assert.IsTrue(ex.Message.Contains("nie istnieje"));
        }

        [TestMethod]
        public void TestUsunStudenta_Sukces()
        {
            var uczelnia = new Uczelnia();
            var s = new Student("Jan", "Testowy", "12312312312");
            uczelnia.DodajStudenta(s);

            uczelnia.UsunStudenta(s);

            Assert.AreEqual(0, uczelnia.Studenci.Count);
        }

        [TestMethod]
        public void TestUsunStudenta_WyjatekNieistnieje()
        {
            var uczelnia = new Uczelnia();
            var s = new Student("Jan", "Duch", "11122233344");

            var ex = Assert.ThrowsException<Exception>(() => uczelnia.UsunStudenta(s));
            Assert.IsTrue(ex.Message.Contains("nie istnieje"));
        }

        [TestMethod]
        public void TestUsunProwadzacego_Sukces()
        {
            var uczelnia = new Uczelnia();
            var p = new Prowadzacy("Piotr", "Wykładowca", "55566677788", EnumTytulNaukowy.Doktor);
            uczelnia.DodajProwadzacego(p);

            uczelnia.UsunProwadzacego(p);

            Assert.AreEqual(0, uczelnia.Prowadzacy.Count);
        }

        [TestMethod]
        public void TestUsunProwadzacego_WyjatekNieistnieje()
        {
            var uczelnia = new Uczelnia();
            var p = new Prowadzacy("Marek", "Nieznany", "99988877766", EnumTytulNaukowy.Magister);

            var ex = Assert.ThrowsException<Exception>(() => uczelnia.UsunProwadzacego(p));
            Assert.IsTrue(ex.Message.Contains("nie istnieje"));
        }
    }

    [TestClass]
    public class KierunekTest
    {
        [TestMethod]
        public void TestKonstruktor_PoprawnaInicjalizacja()
        {
            string nazwa = "Informatyka Stosowana";
            Kierunek kierunek = new Kierunek(nazwa);

            Assert.AreEqual(nazwa, kierunek.NazwaKierunku);
            Assert.IsNotNull(kierunek.Semestry, "Lista semestrów nie powinna być nullem po utworzeniu obiektu");
            Assert.AreEqual(0, kierunek.Semestry.Count, "Nowy kierunek powinien mieć pustą listę semestrów");
        }


        [TestMethod]
        public void TestDodajSemestr_Sukces()
        {
            Kierunek kierunek = new Kierunek("Automatyka");
            Semestr semestr = new Semestr(2024, EnumTyp.Zimowy);

            kierunek.DodajSemestr(semestr);

            Assert.AreEqual(1, kierunek.Semestry.Count);

            Assert.AreSame(semestr, kierunek.Semestry[0]);
        }

        [TestMethod]
        public void TestDodajSemestr_BladDuplikatu()
        {

            Kierunek kierunek = new Kierunek("Automatyka");

            Semestr s1 = new Semestr(2024, EnumTyp.Zimowy);
            Semestr s2 = new Semestr(2024, EnumTyp.Zimowy);

            kierunek.DodajSemestr(s1);

            var ex = Assert.ThrowsException<Exception>(() => kierunek.DodajSemestr(s2));
            Assert.IsTrue(ex.Message.Contains("już istnieje"));
        }

        [TestMethod]
        public void TestDodajSemestr_RozneSemestry()
        {

            Kierunek kierunek = new Kierunek("Budownictwo");
            Semestr zimowy = new Semestr(2024, EnumTyp.Zimowy);
            Semestr letni = new Semestr(2024, EnumTyp.Letni); 

            kierunek.DodajSemestr(zimowy);
            kierunek.DodajSemestr(letni);

            Assert.AreEqual(2, kierunek.Semestry.Count);
        }


        [TestMethod]
        public void TestUsunSemestr_Sukces()
        {
            Kierunek kierunek = new Kierunek("Fizyka");
            Semestr s1 = new Semestr(2023, EnumTyp.Letni);
            kierunek.DodajSemestr(s1);

            kierunek.UsunSemestr(s1);

            Assert.AreEqual(0, kierunek.Semestry.Count);
        }

        [TestMethod]
        public void TestUsunSemestr_BladNieistniejacego()
        {
            Kierunek kierunek = new Kierunek("Matematyka");
            Semestr s1 = new Semestr(2023, EnumTyp.Zimowy);

            var ex = Assert.ThrowsException<Exception>(() => kierunek.UsunSemestr(s1));
            Assert.IsTrue(ex.Message.Contains("nie został znaleziony"));
        }

    }
}
