using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class Student : Osoba
    {
        static int licznik_studenci = 1;
        string numerAlbumu;

        Kierunek kierunek;
        List<Przedmiot> przedmioty;


        public string NumerAlbumu { get => numerAlbumu; set => numerAlbumu = value; }
        public Kierunek Kierunek { get => kierunek; set => kierunek = value; }


        public Student(string imie, string nazwisko, string pesel) : base(licznik_studenci,imie,nazwisko,pesel)
        {
            NumerAlbumu = utworz_album();
            przedmioty = new List<Przedmiot>();
            licznik_studenci++;
        }

        public void DodajPrzedmiot(Przedmiot p)
        {
            if (!przedmioty.Contains(p))
            {
                przedmioty.Add(p);
            }
            else
            {
                throw new Exception("Podany przedmiot został już dodany");
            }
        }

        public void UsunPrzedmiot(Przedmiot p)
        {   
            przedmioty.Remove(p);
        }
        public string utworz_album()
        {
            string result = licznik_studenci.ToString() + Pesel.Substring(7);
            return result;
        }   

    }
}
