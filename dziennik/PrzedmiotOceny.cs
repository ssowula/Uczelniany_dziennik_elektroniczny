using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class PrzedmiotOceny
    {
        Przedmiot przedmiot;
        List<Ocena> oceny;

        public Przedmiot Przedmiot { get => przedmiot; set => przedmiot = value; }
        public List<Ocena> Oceny { get => oceny; set => oceny = value; }

        public PrzedmiotOceny()
        {
            Oceny = new List<Ocena>();
        }
        public PrzedmiotOceny(Przedmiot przedmiot) : this()
        {
            Przedmiot = przedmiot;
            
        }

        public double SredniaOcen()
        {
            if (Oceny.Count == 0)
            {
                return 0.0;
            }
            return Oceny.Average(o => o.Wartosc);
        }
    }
}
