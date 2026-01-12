using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    internal class Kierunek
    {
        int idKierunku;
        string nazwaKierunku;
        List<Przedmiot> przedmioty;

        public int IdKierunku { get => idKierunku; set => idKierunku = value; }
        public string NazwaKierunku { get => nazwaKierunku; set => nazwaKierunku = value; }
        public Kierunek() 
        {
            IdKierunku = 0;
            NazwaKierunku = string.Empty;
        }
        public Kierunek(int idKierunku, string nazwaKierunku):this()
        {
            IdKierunku ++;
            NazwaKierunku = nazwaKierunku;
        }
    }
}
