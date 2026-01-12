using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    enum EnumTyp 
    {   Zima,
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
    }

}
