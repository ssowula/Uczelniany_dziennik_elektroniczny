using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class PrzedmiotOceny
    {
        private Przedmiot przedmiot;
        private List<Ocena> oceny;

        public Przedmiot Przedmiot { get => przedmiot; set => przedmiot = value; }
        public List<Ocena> Oceny { get => oceny; set => oceny = value; }

        public PrzedmiotOceny(Przedmiot przedmiot)
        {
            Przedmiot = przedmiot;
            Oceny = new List<Ocena>();
        }
    }
}
