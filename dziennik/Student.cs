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
        public string NumerAlbumu { get => numerAlbumu; set => numerAlbumu = value; }



        public Student(string imie, string nazwisko, string pesel) : base(licznik_studenci,imie,nazwisko,pesel)
        {
            NumerAlbumu = utworz_album();
            licznik_studenci++;
        }

        public string utworz_album()
        {
            string result = licznik_studenci.ToString() + Pesel.Substring(7);
            return result;
        }   

    }
}
