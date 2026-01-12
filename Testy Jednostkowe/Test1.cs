using dziennik;

namespace Testy_Jednostkowe
{
    [TestClass]
    public sealed class StudentTest
    {
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

            Assert.IsTrue(student.przedmioty.Contains(przedmiot));
        }

        [TestMethod]
        public void TestUsunPrzedmiot()
        {
            var student = new Student("Adam", "Łukasik", "12345678911");

            var prowadzacy = new Prowadzacy("Jacek", "Wolak", "11122233311", EnumTytulNaukowy.Doktor);

            var przedmiot = new Przedmiot("Wstęp do analizy danych", prowadzacy, 3);

            student.DodajPrzedmiot(przedmiot);

            Assert.IsTrue(student.przedmioty.Contains(przedmiot));

            student.UsunPrzedmiot(przedmiot);

            Assert.IsFalse(student.przedmioty.Contains(przedmiot));
        }
    }
}
