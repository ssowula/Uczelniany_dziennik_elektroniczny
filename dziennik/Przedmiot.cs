using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class Przedmiot
    {
        string nazwa;
        Prowadzacy prowadzacy;
        int ects;
       


        public string Nazwa { get => nazwa; set => nazwa = value; }
        public Prowadzacy Prowadzacy { get => prowadzacy; set => prowadzacy = value; }
        public int Ects { get => ects; set => ects = value; }
        

        public Przedmiot() 
        {
            Nazwa = string.Empty;
            Prowadzacy = null;
            Ects = 0;

        }

        public Przedmiot(string nazwa, Prowadzacy prowadzacy, int ects) : this()
        {
            Nazwa = nazwa;
            Prowadzacy = prowadzacy;
            Ects = ects;
        }
        

    }
}
