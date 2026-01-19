using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public class Student : Osoba, IComparable<Student>, IEquatable<Student>, ICloneable, IRaportowalny
    {
        static int licznik_studenci = 1;
        string numerAlbumu;

        Kierunek kierunek;
        List<PrzedmiotOceny> przedmiotyOceny;


        public string NumerAlbumu { get => numerAlbumu; set => numerAlbumu = value; }
        public Kierunek Kierunek { get => kierunek; set => kierunek = value; }
        public static int Licznik_studenci { get => licznik_studenci;}
        public List<PrzedmiotOceny> PrzedmiotyOceny { get => przedmiotyOceny; }
        public Student() : base()
        {
            przedmiotyOceny = new List<PrzedmiotOceny>();
        }

        public Student(string imie, string nazwisko, string pesel) : base(licznik_studenci,imie,nazwisko,pesel)
        {
            przedmiotyOceny = new List<PrzedmiotOceny>();
            NumerAlbumu = utworz_album();
            licznik_studenci++;
        }

        public void DodajPrzedmiot(Przedmiot p)
        {
            bool zapisany = przedmiotyOceny.Any(x => x.Przedmiot == p);

            if (!zapisany)
            {
                PrzedmiotOceny nowyZapis = new PrzedmiotOceny(p);
                przedmiotyOceny.Add(nowyZapis);
            }
        }

        public void UsunPrzedmiot(Przedmiot p)
        {
            var doUsuniecia = przedmiotyOceny.FirstOrDefault(x => x.Przedmiot == p);

            if (doUsuniecia != null)
            {
                przedmiotyOceny.Remove(doUsuniecia);
            }
        }
        public string utworz_album()
        {
            string result = licznik_studenci.ToString() + Pesel.Substring(7);
            return result;
        }
        public int CompareTo(Student? other)
        {
            return base.CompareTo(other);
        }

        public void DodajOcene(Przedmiot p, double wartosc)
        {
            var przedmiot = przedmiotyOceny.FirstOrDefault(x=>x.Przedmiot == p);

            if (przedmiot != null)
            {
                Ocena ocena = new Ocena(p, wartosc);
                przedmiot.Oceny.Add(ocena);
            }
            else
            {
                throw new Exception("Student nie jest zapisany na ten przedmiot");
            }
        }

        public override string PobierzInformacje()
        {
            return $"[Student] {base.PobierzInformacje()}, numer albumu: {NumerAlbumu}";
        }

        public bool Equals(Student? other)
        {
            if(other == null) return false;
            return this.Pesel == other.Pesel;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Student other)
            {
                return this.Equals(other); 
            }
            return false;
        }

        
        public override int GetHashCode()
        {
            
            return NumerAlbumu.GetHashCode();
        }

        public object Clone()
        {
            Student kopia = (Student)this.MemberwiseClone();
            kopia.przedmiotyOceny = new List<PrzedmiotOceny>();
            foreach (var po in this.przedmiotyOceny)
            {
                PrzedmiotOceny kopiaPo = new PrzedmiotOceny();
                kopiaPo.Przedmiot = po.Przedmiot;
                kopiaPo.Oceny = new List<Ocena>();
                foreach (var ocena in po.Oceny)
                {
                    Ocena kopiaOcena = new Ocena();
                    kopiaOcena.Przedmiot = ocena.Przedmiot;
                    kopiaOcena.Wartosc = ocena.Wartosc;
                    kopiaPo.Oceny.Add(kopiaOcena);
                }
                kopia.przedmiotyOceny.Add(kopiaPo);
            }
            return kopia;
        }

        public string GenerujRaport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" ==== RAPORT ==== ");
            sb.AppendLine($"{PobierzInformacje()}");
            sb.AppendLine(" == OCENY == ");
            foreach (var po in przedmiotyOceny)
            {
                sb.AppendLine($"Przedmiot: {po.Przedmiot.Nazwa}");
                sb.AppendLine($"Prowadzący: {po.Przedmiot.Prowadzacy.Nazwisko}");
                double srednia = po.SredniaOcen();
                if(srednia > 0) {
                    sb.AppendLine($"Średnia ocen: {srednia:F2}");
                }
                else
                {
                    sb.AppendLine("Średnia: Brak ocen");
                }
                sb.AppendLine("Oceny:");
                foreach (var ocena in po.Oceny)
                {
                    sb.AppendLine($" - Ocena: {ocena.Wartosc}");
                }
                sb.AppendLine("----------------");

            }
            var srednia_cal = przedmiotyOceny.SelectMany(x => x.Oceny).ToList();
            if (srednia_cal.Count > 0)
            {
                double srednia_ogolna = srednia_cal.Average(o => o.Wartosc);
                sb.AppendLine($"ŚREDNIA CAŁKOWITA: {srednia_ogolna:F2}");
            }
            return sb.ToString();
        }
    }
}
