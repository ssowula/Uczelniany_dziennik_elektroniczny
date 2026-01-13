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
        public void TestDodajPrzedmiot()
        {
            var student = new Student("Adam", "Łukasik", "12345678911");

            var prowadzacy = new Prowadzacy("Jacek", "Wolak", "11122233311", EnumTytulNaukowy.Doktor);

            var przedmiot = new Przedmiot("Wstęp do analizy danych", prowadzacy, 3);

            student.DodajPrzedmiot(przedmiot);

            Assert.IsTrue(student.Przedmioty.Contains(przedmiot));
        }

        [TestMethod]
        public void TestUsunPrzedmiot()
        {
            var student = new Student("Adam", "Łukasik", "12345678911");

            var prowadzacy = new Prowadzacy("Jacek", "Wolak", "11122233311", EnumTytulNaukowy.Doktor);

            var przedmiot = new Przedmiot("Wstęp do analizy danych", prowadzacy, 3);

            student.DodajPrzedmiot(przedmiot);

            Assert.IsTrue(student.Przedmioty.Contains(przedmiot));

            student.UsunPrzedmiot(przedmiot);

            Assert.IsFalse(student.Przedmioty.Contains(przedmiot));
        }

        [TestMethod]
        public void TestCompareTo()
        {
            var s1 = new Student("Adam", "Łukasik", "12345678911");
            var s2 = new Student("Tomasz", "Król", "11122244412");

            Assert.IsTrue(s1.CompareTo(s2) != 0);
        }
    }
}
