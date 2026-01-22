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

            Assert.AreEqual(22.0 / 6.0, przedmiotOceny.SredniaOcen(),0.001);
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
}
