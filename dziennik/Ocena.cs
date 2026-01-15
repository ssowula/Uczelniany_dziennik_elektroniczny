using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    
    public class Ocena
    {
        Student student;
        Przedmiot przedmiot;
        int wartosc;
        DateTime dataWystawienia;
        public int Wartosc { get => wartosc; set => wartosc = value; }
        public DateTime DataWystawienia { get => dataWystawienia; set => dataWystawienia = value; }
        public Przedmiot Przedmiot { get => przedmiot; set => przedmiot = value; }
        public Student Student { get => student; set => student = value; }

        public Ocena(Student student, Przedmiot przedmiot, int wartosc, DateTime dataWystawienia)
        {
            Student = student;
            Przedmiot = przedmiot;
            Wartosc = wartosc;
            DataWystawienia = dataWystawienia;
        }

    }
}
