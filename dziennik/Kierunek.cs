using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class Kierunek
    {
        int idKierunku;
        string nazwaKierunku;
        List<Przedmiot> przedmioty;

        public int IdKierunku { get => idKierunku; set => idKierunku = value; }
        public string NazwaKierunku { get => nazwaKierunku; set => nazwaKierunku = value; }
        public List<Przedmiot> Przedmioty { get => przedmioty; set => przedmioty = value; }
        public Kierunek(int idKierunku, string nazwaKierunku)
        {
            IdKierunku = idKierunku;
            NazwaKierunku = nazwaKierunku;
            Przedmioty = new List<Przedmiot>();
        }
        public void DodajPrzedmiot(Przedmiot przedmiot)
        {
            Przedmioty.Add(przedmiot);
        }
        public void UsunPrzedmiot(Przedmiot przedmiot)
        {
            Przedmioty.Remove(przedmiot);
        }

    }
}
