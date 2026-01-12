using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class Prowadzacy : Osoba
    {
        private string tytulNaukowy;
        static int licznik_prowadzacy = 1;

        public string TytulNaukowy { get => tytulNaukowy; set => tytulNaukowy = value; }
        public static int Licznik_prowadzacy { get => licznik_prowadzacy; set => licznik_prowadzacy = value; }

        public Prowadzacy(string imie, string nazwisko, string pesel, string tytulNaukowy): base(licznik_prowadzacy, imie, nazwisko, pesel)
        {
            TytulNaukowy = tytulNaukowy;
            licznik_prowadzacy++;
        }
    }
}
