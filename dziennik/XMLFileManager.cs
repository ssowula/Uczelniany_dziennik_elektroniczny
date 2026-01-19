using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dziennik
{
    public static class XMLFileManager
    {
        public static void Zapisz(Uczelnia uczelnia, string nazwaPliku)
        {
            try
            {
                
                XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia));

                using (StreamWriter writer = new StreamWriter(nazwaPliku))
                {
                    serializer.Serialize(writer, uczelnia);
                }
                Console.WriteLine($"Pomyślnie zapisano dane do pliku: {nazwaPliku}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd zapisu: {ex.Message}");
            }
        }

        public static Uczelnia? Wczytaj(string nazwaPliku)
        {
            if (!File.Exists(nazwaPliku))
            {
                return new Uczelnia(); 
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia));

                using (StreamReader reader = new StreamReader(nazwaPliku))
                {
                    return (Uczelnia?)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu: {ex.Message}");
                return new Uczelnia();
            }
        }
    }
}
