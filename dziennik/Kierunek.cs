using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class Kierunek
    {
        static int licznikKierunku = 1;
        int idKierunku;
        string nazwaKierunku;
        List<Semestr> semestry;

        public int IdKierunku { get => idKierunku; }
        public string NazwaKierunku { get => nazwaKierunku; set => nazwaKierunku = value; }
        public List<Semestr> Semestry { get => semestry; set => semestry = value; }

        public Kierunek() 
        {
            NazwaKierunku = string.Empty;
            Semestry = new List<Semestr>();
        }
        public Kierunek(string nazwaKierunku):this()
        {
            this.idKierunku = licznikKierunku++;
            NazwaKierunku = nazwaKierunku;
            
        }

    }
}
