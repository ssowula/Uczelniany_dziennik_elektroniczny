using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public enum EnumTyp
    {   
        Zima,
        Lato 
    }
    public class Semestr
    {
        int rokAkademicki;
        EnumTyp typ;
        List<Przedmiot> przedmioty;

        public int RokAkademicki { get => rokAkademicki; set => rokAkademicki = value; }
        public List<Przedmiot> Przedmioty { get => przedmioty; set => przedmioty = value; }
        internal EnumTyp Typ { get => typ; set => typ = value; }
        public Semestr()
        {
            rokAkademicki = 0;
            typ = EnumTyp.Zima;
            Przedmioty = new List<Przedmiot>();
        }
        public Semestr(int rokAkademicki, EnumTyp typ) : this()
        {
            RokAkademicki = rokAkademicki;
            Typ = typ;
        }
        public void DodajPrzedmiot(Przedmiot przedmiot)
        {
            Przedmioty.Add(przedmiot);
        }
        public void UsunPrzedmiot(Przedmiot przedmiot)
        {
            Przedmioty.Remove(przedmiot);
        }
    }

}
