using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dziennik
{
    public enum EnumTyp
    {   
        Zimowy,
        Letni 
    }
    public class Semestr
    {
        int rokAkademicki;
        EnumTyp typ;
        List<Przedmiot> przedmioty;

        public int RokAkademicki { get => rokAkademicki; set => rokAkademicki = value; }
        public List<Przedmiot> Przedmioty { get => przedmioty; set => przedmioty = value; }
        public EnumTyp Typ { get => typ; set => typ = value; }
        public Semestr()
        {
            rokAkademicki = 0;
            typ = EnumTyp.Zimowy;
            Przedmioty = new List<Przedmiot>();
        }
        public Semestr(int rokAkademicki, EnumTyp typ) : this()
        {
            RokAkademicki = rokAkademicki;
            Typ = typ;
        }
        public void DodajPrzedmiot(Przedmiot przedmiot)
        {
            
            bool istnieje = Przedmioty.Any(p => p.Nazwa.Equals(przedmiot.Nazwa, StringComparison.OrdinalIgnoreCase));

            if (istnieje)
            {
                throw new Exception($"Przedmiot o nazwie {przedmiot.Nazwa} już istnieje na tym semestrze!");
            }
            else
            {
                Przedmioty.Add(przedmiot);
                
            }
        }

        public void UsunPrzedmiot(Przedmiot przedmiot)
        {
            
            var doUsuniecia = Przedmioty.FirstOrDefault(p => p.Nazwa.Equals(przedmiot.Nazwa, StringComparison.OrdinalIgnoreCase));

            if (doUsuniecia != null)
            {
                Przedmioty.Remove(doUsuniecia);
            }
            else
            {
                throw new Exception($"Przedmiot o nazwie {przedmiot.Nazwa} nie istnieje na tym semestrze!");
            }
        }
        public string PobierzInformacjeS()
        {
            return $"Rok akademicki: {RokAkademicki}, Typ: {Typ}";
        }
    }

}
