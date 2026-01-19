using System;
using System.Collections.Generic;
using System.Linq;

namespace dziennik
{
    public static class GeneratorDanych
    {
        public static Uczelnia ZaladujDaneTestowe()
        {
            Uczelnia u = new Uczelnia();

            Prowadzacy p1 = new Prowadzacy("Jan", "Kowalski", "12345678910", EnumTytulNaukowy.Doktor);
            Prowadzacy p2 = new Prowadzacy("Anna", "Nowak", "11122233310", EnumTytulNaukowy.Profesor);

            u.DodajProwadzacego(p1);
            u.DodajProwadzacego(p2);

            Przedmiot matma = new Przedmiot("Matematyka", p1, 5);
            Przedmiot prog = new Przedmiot("Programowanie Obiektowe", p2, 6);
            Przedmiot bazy = new Przedmiot("Bazy Danych", p1, 4);

            Student s1 = new Student("Adam", "Testowy", "12312312310");
            u.DodajStudenta(s1);

            s1.DodajPrzedmiot(matma);       
            s1.DodajOcene(matma, 5.0);      
            s1.DodajOcene(matma, 3.5);

            s1.DodajPrzedmiot(prog);
            s1.DodajOcene(prog, 4.0);

            Student s2 = new Student("Ewa", "Pusta", "12312312311");
            u.DodajStudenta(s2);

            s2.DodajPrzedmiot(bazy);

            return u;
        }
    }
}