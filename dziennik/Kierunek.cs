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

        public int IdKierunku { get => idKierunku; set => idKierunku = value; }
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

        public void DodajSemestr(Semestr semestr)
        {
            
            bool istnieje = Semestry.Any(s => s.RokAkademicki == semestr.RokAkademicki && s.Typ == semestr.Typ);

            if (istnieje)
            {
                throw new Exception($"Semestr {semestr.Typ} {semestr.RokAkademicki} już istnieje na tym kierunku!");
            }
            else
            {
                Semestry.Add(semestr);
            }
        }

        public void UsunSemestr(Semestr semestr)
        {
            
            var doUsuniecia = Semestry.FirstOrDefault(s => s.RokAkademicki == semestr.RokAkademicki && s.Typ == semestr.Typ);

            if (doUsuniecia != null)
            {
                Semestry.Remove(doUsuniecia);
            }
            else
            {
                throw new Exception($"Semestr {semestr.Typ} {semestr.RokAkademicki} nie został znaleziony!");
            }
        }

    }
}
