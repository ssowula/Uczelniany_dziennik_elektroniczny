using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class Student : Osoba, IComparable<Student>
    {
        static int licznik_studenci = 1;
        string numerAlbumu;

        Kierunek kierunek;
        List<PrzedmiotOceny> przedmiotyOceny;


        public string NumerAlbumu { get => numerAlbumu; set => numerAlbumu = value; }
        public Kierunek Kierunek { get => kierunek; set => kierunek = value; }
        public static int Licznik_studenci { get => licznik_studenci;}
        public List<PrzedmiotOceny> PrzedmiotyOceny { get => przedmiotyOceny; }
        public Student() : base()
        {
            przedmiotyOceny = new List<PrzedmiotOceny>();
        }

        public Student(string imie, string nazwisko, string pesel) : base(licznik_studenci,imie,nazwisko,pesel)
        {
            NumerAlbumu = utworz_album();
            licznik_studenci++;
        }

        public void DodajPrzedmiot(Przedmiot p)
        {
            bool zapisany = przedmiotyOceny.Any(x => x.Przedmiot == p);

            if (!zapisany)
            {
                PrzedmiotOceny nowyZapis = new PrzedmiotOceny(p);
                przedmiotyOceny.Add(nowyZapis);
            }
        }

        public void UsunPrzedmiot(Przedmiot p)
        {
            var doUsuniecia = przedmiotyOceny.FirstOrDefault(x => x.Przedmiot == p);

            if (doUsuniecia != null)
            {
                przedmiotyOceny.Remove(doUsuniecia);
            }
        }
        public string utworz_album()
        {
            string result = licznik_studenci.ToString() + Pesel.Substring(7);
            return result;
        }
        public int CompareTo(Student? other)
        {
            return base.CompareTo(other);
        }

        public void DodajOcene(Przedmiot p, double wartosc)
        {
            var przedmiot = przedmiotyOceny.FirstOrDefault(x=>x.Przedmiot == p);

            if (przedmiot != null)
            {
                Ocena ocena = new Ocena(p, wartosc);
                przedmiot.Oceny.Add(ocena);
            }
            else
            {
                throw new Exception("Student nie jest zapisany na ten przedmiot");
            }
        }
    }
}
