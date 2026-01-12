using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;

namespace dziennik
{
    public class ZlyPeselException : Exception
    {
        public ZlyPeselException()
        {
        }

        public ZlyPeselException(string? message) : base(message)
        {
        }
    }
    abstract class Osoba
    {
        int id;
        string imie;
        string nazwisko;
        string pesel;

        public int Id { get => id; }
        public string Imie { get => imie; set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        public string Pesel { get => pesel; set 
            {
                if (Regex.IsMatch(value, @"^\d{11}$"))
                {
                    pesel = value;
                }
                else
                {
                    throw new ZlyPeselException("Podano nieprawidłowy pesel");
                }
            }
        }

        public Osoba() { }

        public Osoba(int id, string imie, string nazwisko, string pesel)
        {
            this.id = id;
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = pesel;
        }
    }
}
