using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class ProwadzonyPrzedmiot
    {
        public Kierunek Kierunek { get; set; }
        public Semestr Semestr { get; set; }
        public Przedmiot Przedmiot { get; set; }

        public ProwadzonyPrzedmiot(Kierunek kierunek, Semestr semestr, Przedmiot przedmiot)
        {
            Kierunek = kierunek;
            Semestr = semestr;
            Przedmiot = przedmiot;
        }

        public override string ToString()
        {
            return $"{Przedmiot.Nazwa} \n{Kierunek.NazwaKierunku} | {Semestr.Typ} {Semestr.RokAkademicki}";
        }
    }
}
