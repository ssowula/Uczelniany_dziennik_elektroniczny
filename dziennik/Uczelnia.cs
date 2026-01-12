using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    internal class Uczelnia
    {
        List<Kierunek> kierunki;
        List<Student> studenci;
        List<Prowadzacy> prowadzacy;

        public List<Student> Studenci { get => studenci; set => studenci = value; }
        public List<Prowadzacy> Prowadzacy { get => prowadzacy; set => prowadzacy = value; }
        internal List<Kierunek> Kierunki { get => kierunki; set => kierunki = value; }
        public Uczelnia()
        {
            Kierunki = new List<Kierunek>();
            Studenci = new List<Student>();
            Prowadzacy = new List<Prowadzacy>();
        }
        public void DodajKierunek(Kierunek kierunek)
        {
            Kierunki.Add(kierunek);
        }
        public void UsunKierunek(Kierunek kierunek)
        {
            Kierunki.Remove(kierunek);
        }
        public void DodajStudenta(Student student)
        {
            Studenci.Add(student);
        }
        public void UsunStudenta(Student student)
        {
            Studenci.Remove(student);
        }
        public void DodajProwadzacego(Prowadzacy prowadzacy)
        {
            Prowadzacy.Add(prowadzacy);
        }
        public void UsunProwadzacego(Prowadzacy prowadzacy)
        {
            Prowadzacy.Remove(prowadzacy);
        }
    }
}
