using dziennik;
using System;
using System.Linq;
using System.Windows;

namespace DziennikGUI
{
    public partial class OknoZarzadzaniaOcenami : Window
    {
        private Student student;
        private Przedmiot przedmiot;

        public OknoZarzadzaniaOcenami(Student student, Przedmiot przedmiot)
        {
            InitializeComponent();
            this.student = student;
            this.przedmiot = przedmiot;

            txtStudent.Text = $"Student: {student.Imie} {student.Nazwisko}";
            txtPrzedmiot.Text = $"Przedmiot: {przedmiot.Nazwa}";

            OdswiezListe();
        }

        private void OdswiezListe()
        {
            var przedmiotOceny = student.PrzedmiotyOceny.FirstOrDefault(po => po.Przedmiot == przedmiot);
            
            if (przedmiotOceny != null)
            {
                listaOcen.ItemsSource = null;
                listaOcen.ItemsSource = przedmiotOceny.Oceny.ToList(); 
            }
        }

        private void btnUsun_Click(object sender, RoutedEventArgs e)
        {
            var wybranaOcena = listaOcen.SelectedItem as Ocena;

            if (wybranaOcena == null)
            {
                MessageBox.Show("Proszę zaznaczyć ocenę do usunięcia.");
                return;
            }

            var wynik = MessageBox.Show($"Czy na pewno chcesz usunąć ocenę {wybranaOcena.Wartosc}?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (wynik == MessageBoxResult.Yes)
            {
                try
                {
                    student.UsunOcene(przedmiot, wybranaOcena);
                    
                    MessageBox.Show("Ocena została usunięta.");
                    OdswiezListe();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd: {ex.Message}");
                }
            }
        }

        private void btnZamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
