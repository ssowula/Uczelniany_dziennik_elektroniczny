using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    abstract class Osoba
    {
        int id;
        string imie;
        string nazwisko;

        public int Id { get => id; }
        public string Imie { get => imie; set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }

        public Osoba(int id, string imie, string nazwisko) 
        {
            this.id = id;
            Imie = imie;
            Nazwisko = nazwisko;
        }
    }
}
