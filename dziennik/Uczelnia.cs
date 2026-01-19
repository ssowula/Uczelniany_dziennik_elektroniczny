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
            bool istnieje = Kierunki.Any(k => k.NazwaKierunku.Equals(kierunek.NazwaKierunku, StringComparison.OrdinalIgnoreCase));
            if (!istnieje)
            {
                Kierunki.Add(kierunek);
            }
            else
            {
                throw new Exception($"Kierunek o nazwie {kierunek.NazwaKierunku} już istnieje w systemie!");
            }
        }
        public void UsunKierunek(Kierunek kierunek)
        {
            var doUsuniecia = Kierunki.FirstOrDefault(k => k.NazwaKierunku.Equals(kierunek.NazwaKierunku, StringComparison.OrdinalIgnoreCase));

            if (doUsuniecia != null)
            {
                Kierunki.Remove(doUsuniecia); 
            }
            else
            {
                throw new Exception($"Kierunek o nazwie {kierunek.NazwaKierunku} nie istnieje w systemie!");
            }
        }
        public void DodajStudenta(Student student)
        {
            
            if (!Studenci.Contains(student))
            {
                Studenci.Add(student);
            }
            else
            {
                throw new Exception($"Student o numerze Pesel {student.Pesel} już istnieje w systemie!");
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
                throw new Exception($"Student o numerze Pesel {student.Pesel} nie istnieje w systemie!");
            }
                
        }
        public void DodajProwadzacego(Prowadzacy prowadzacy)
        {
            if(!Prowadzacy.Contains(prowadzacy))
            {
                Prowadzacy.Add(prowadzacy);
            }
            else
            {
                throw new Exception($"Prowadzący o numerze Pesel {prowadzacy.Pesel} już istnieje w systemie!");
            }
        }
        public void UsunProwadzacego(Prowadzacy prowadzacy)
        {
            if (Prowadzacy.Contains(prowadzacy))
            {
                Prowadzacy.Remove(prowadzacy);
            }
            else
            {
                throw new Exception($"Prowadzący o numerze Pesel {prowadzacy.Pesel} nie istnieje w systemie!");
            }
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
