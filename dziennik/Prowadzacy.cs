using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public enum EnumTytulNaukowy
    {
        Licencjat,
        Inzynier,
        Magister,
        MagisterInzynier,
        Doktor,
        DoktorHabilitowany,
        Profesor
    }

    public class Prowadzacy : Osoba, IComparable<Prowadzacy>, IEquatable<Prowadzacy>
    {
        EnumTytulNaukowy tytulNaukowy;
        static int licznik_prowadzacy = 1;

        public EnumTytulNaukowy TytulNaukowy { get => tytulNaukowy; set => tytulNaukowy = value; }
        public static int Licznik_prowadzacy { get => licznik_prowadzacy; set => licznik_prowadzacy = value; }

        public Prowadzacy() : base() { }
        public Prowadzacy(string imie, string nazwisko, string pesel, EnumTytulNaukowy tytulNaukowy): base(licznik_prowadzacy, imie, nazwisko, pesel)
        {
            TytulNaukowy = tytulNaukowy;
            licznik_prowadzacy++;
        }

        public int CompareTo(Prowadzacy? other)
        {
            return base.CompareTo(other);
        }

        public override string PobierzInformacje()
        {
            return $"[Prowadzący] {tytulNaukowy} {base.PobierzInformacje()}";
        }

        public bool Equals(Prowadzacy? other)
        {
            if(other == null) return false;
            return this.Pesel == other.Pesel;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Prowadzacy other)
            {
                return this.Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Pesel.GetHashCode();
        }

       public List<ProwadzonyPrzedmiot> ZnajdzPrzedmiotyProwadzacego(Uczelnia uczelnia)
       {
            List<ProwadzonyPrzedmiot> znalezionePrzedmioty = new List<ProwadzonyPrzedmiot>();
            foreach(var kierunek in uczelnia.Kierunki)
            {
                foreach(var semestr in kierunek.Semestry)
                {
                    foreach(var przedmiot in semestr.Przedmioty)
                    {
                        if(przedmiot.Prowadzacy != null && przedmiot.Prowadzacy.Equals(this))
                        {
                            znalezionePrzedmioty.Add(new ProwadzonyPrzedmiot(kierunek, semestr, przedmiot));
                        }
                    }
                }
            }
            return znalezionePrzedmioty;
       } 
    }

    internal class ProwadzacyTytulComparer : IComparer<Prowadzacy>
    {
        public int Compare(Prowadzacy? x, Prowadzacy? y)
        {
            if (x == null || y == null) return 0;

            
            int wynikTytul = y.TytulNaukowy.CompareTo(x.TytulNaukowy);

            if (wynikTytul != 0)
            {
                return wynikTytul;
            }

            return x.CompareTo(y);
        }
    }
}
