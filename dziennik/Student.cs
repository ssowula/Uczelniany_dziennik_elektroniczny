using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    internal class Student : Osoba
    {
        static int licznik_studenci = 1;
        string numerAlbumu;

        public Student(string imie, string nazwisko) : base(licznik_studenci,imie,nazwisko)
        {
            licznik_studenci++;
        }

        public string utworz_album()
        {
            string
        }

    }
}
