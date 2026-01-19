using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class Uczelnia
    {
        List<Kierunek> kierunki;
        List<Student> studenci;
        List<Prowadzacy> prowadzacy;

        public List<Student> Studenci { get => studenci; set => studenci = value; }
        public List<Prowadzacy> Prowadzacy { get => prowadzacy; set => prowadzacy = value; }
        public List<Kierunek> Kierunki { get => kierunki; set => kierunki = value; }
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
            if (!Studenci.Contains(student))
            {
                Studenci.Add(student);
            }
            else
            {
                Console.WriteLine($"Student o numerze Pesel {student.NumerAlbumu} już istnieje w systemie!");
            }
               
        }
        public void UsunStudenta(Student student)
        {
            if (Studenci.Contains(student))
            {
                Studenci.Remove(student);
            }
            else
            {
                Console.WriteLine($"Student o Numerze Albumu {student.NumerAlbumu} nie istnieje w systemie!");
            }
                
        }
        public void DodajProwadzacego(Prowadzacy prowadzacy)
        {
            Prowadzacy.Add(prowadzacy);
        }
        public void UsunProwadzacego(Prowadzacy prowadzacy)
        {
            Prowadzacy.Remove(prowadzacy);
        }
        public void SortujStudentow()
        {
            Studenci.Sort();
        }

        public void SortujProwadzacych()
        {
            Prowadzacy.Sort();
        }

        public void SortujProwadzacychPoTytule()
        {
            
            Prowadzacy.Sort(new ProwadzacyTytulComparer());
        }
    }
}
