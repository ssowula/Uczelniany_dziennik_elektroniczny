using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    internal class Ocena
    {
        Student student { get; }
        Przedmiot przedmiot { get; }
        int wartosc;
        DateTime dataWystawienia;
        public int Wartosc { get => wartosc; set => wartosc = value; }
        public DateTime DataWystawienia { get => dataWystawienia; set => dataWystawienia = value; }
        public Ocena(Student student, Przedmiot przedmiot, int wartosc, DateTime dataWystawienia)
        {
            this.student = student;
            this.przedmiot = przedmiot;
            Wartosc = wartosc;
            DataWystawienia = dataWystawienia;
        }

    }
}
