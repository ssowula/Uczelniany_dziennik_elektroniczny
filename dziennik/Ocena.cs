using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class NiepoprawnaOcenaException : Exception
    {
        public NiepoprawnaOcenaException()
        {
        }

        public NiepoprawnaOcenaException(string? message) : base(message)
        {
        }
    }
    public class Ocena
    {
        
        Przedmiot przedmiot;
        double wartosc;
        DateTime dataWystawienia;
        public double Wartosc { get => wartosc; set { 
            if(value is 2 or 3 or 3.5 or 4 or 4.5 or 5)
                {
                    wartosc = value;
                }
            else
            {
                throw new NiepoprawnaOcenaException("Taka ocena nie istnieje");
            }   
            } }
        public DateTime DataWystawienia { get => dataWystawienia; set => dataWystawienia = value; }
        public Przedmiot Przedmiot { get => przedmiot; set => przedmiot = value; }

        public Ocena() { }

        public Ocena(Przedmiot przedmiot, double wartosc)
        {
            Przedmiot = przedmiot;
            Wartosc = wartosc;
            DataWystawienia = DateTime.Now;
        }

    }
}
