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
            var student = new Student("Adam", "Łukasik", "12345678911");
            var prowadzacy = new Prowadzacy("Jacek", "Wolak", "11122233311", EnumTytulNaukowy.Doktor);
            var przedmiot = new Przedmiot("Wstęp do analizy danych", prowadzacy, 3);
            double ocena = 8;

            Assert.ThrowsException<NiepoprawnaOcenaException>(() => { var o1 = new Ocena(student, przedmiot, ocena); });
           
        }
    }
}
