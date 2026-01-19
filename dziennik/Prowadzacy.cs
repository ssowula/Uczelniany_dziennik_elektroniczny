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
    public class Prowadzacy : Osoba, IComparable<Prowadzacy>
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
