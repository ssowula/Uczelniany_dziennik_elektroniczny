using dziennik;

namespace Testy_Jednostkowe
{
    [TestClass]
    public sealed class StudentTest
    {
        [TestMethod]
        public void Student_NumerAlbumu()
        {
            int id = Student.Licznik_studenci;
            var student = new Student("Adam", "Łukasik", "12345678911");
            var numerAlbumu = student.NumerAlbumu;

            Assert.AreEqual($"{id.ToString()}8911", numerAlbumu);
        }
    }
}
