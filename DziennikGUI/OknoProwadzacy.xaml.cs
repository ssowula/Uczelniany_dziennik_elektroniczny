using dziennik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DziennikGUI
{
    /// <summary>
    /// Logika interakcji dla klasy OknoProwadzacy.xaml
    /// </summary>
    public partial class OknoProwadzacy : Window
    {
        private Prowadzacy zalogowanyProwadzacy;
        private Uczelnia uczelnia;
        public OknoProwadzacy(Prowadzacy prowadzacy, Uczelnia uczelnia)
        {
            InitializeComponent();
            zalogowanyProwadzacy = prowadzacy;
            this.DataContext = zalogowanyProwadzacy;
            this.uczelnia = uczelnia;
            odswiezListePrzedmiotow();
        }

        public void WyswietlProwadzacego()
        {
            if (zalogowanyProwadzacy != null)
            {

            }
        }

        public void odswiezListePrzedmiotow()
        {
            if (zalogowanyProwadzacy != null && uczelnia != null)
            {
                var przedmioty = zalogowanyProwadzacy.ZnajdzPrzedmiotyProwadzacego(uczelnia);
                listaPrzedmiotow.ItemsSource = przedmioty;
            }
        }

        private void listaPrzedmiotow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybrany = listaPrzedmiotow.SelectedItem as ProwadzonyPrzedmiot;

            if (wybrany == null) return;

            panelDanePrzedmiotu.DataContext = wybrany;

            var studenci = uczelnia.Studenci
                .Where(s => s.PrzedmiotyOceny.Any(po =>
                    po.Przedmiot.Nazwa == wybrany.Przedmiot.Nazwa &&
                    po.Przedmiot.Prowadzacy.Pesel == zalogowanyProwadzacy.Pesel))
                .ToList();

            var studentyDoWyswietlenia = studenci.Select(s =>
            {
                var przedmiotOceny = s.PrzedmiotyOceny.FirstOrDefault(po => po.Przedmiot.Nazwa == wybrany.Przedmiot.Nazwa);

                return new StudentOcenyPrzedmiot
                {
                    ImieNazwisko = $"{s.Imie} {s.Nazwisko}",
                    NumerAlbumu = s.NumerAlbumu,
                    Oceny = przedmiotOceny != null && przedmiotOceny.Oceny.Any()
                        ? string.Join("; ", przedmiotOceny.Oceny.Select(o => o.Wartosc))
                        : "Brak",
                    Srednia = przedmiotOceny?.SredniaOcen() ?? 0
                };
            }).ToList();

            listaStudentow.ItemsSource = studentyDoWyswietlenia;
        }

        private void listaStudentow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybranyStudent = listaStudentow.SelectedItem as StudentOcenyPrzedmiot;

            if (wybranyStudent == null)
            {
                btnDodajOcene.IsEnabled = false;
                btnEdytujOcene.IsEnabled = false;
            }
            else
            {
                btnDodajOcene.IsEnabled = true;
                btnEdytujOcene.IsEnabled = true;
            }


        }

        private void btnDodajOcene_Click(object sender, RoutedEventArgs e)
        {
            var wybranyPrzedmiot = listaPrzedmiotow.SelectedItem as ProwadzonyPrzedmiot;
            var wybranyStudent = listaStudentow.SelectedItem as StudentOcenyPrzedmiot;

            if (wybranyPrzedmiot == null || wybranyStudent == null) return;

            var oknoDodawaniaOceny = new OknoDodawaniaOceny(uczelnia, zalogowanyProwadzacy, wybranyStudent.NumerAlbumu, wybranyPrzedmiot.Przedmiot.Nazwa);
            oknoDodawaniaOceny.ShowDialog();

            listaPrzedmiotow_SelectionChanged(null, null);
        }

        private void btnEdytuj_Ocene(object sender, RoutedEventArgs e)
        {
            var wybranyPrzedmiot = listaPrzedmiotow.SelectedItem as ProwadzonyPrzedmiot;
            var wybranyStudent = listaStudentow.SelectedItem as StudentOcenyPrzedmiot;

            if (wybranyPrzedmiot == null || wybranyStudent == null) return;

            var prawdziwyStudent = uczelnia.Studenci.FirstOrDefault(s => s.NumerAlbumu == wybranyStudent.NumerAlbumu);
            if (prawdziwyStudent == null) return;

            var prawdziwyPrzedmiot = wybranyPrzedmiot.Przedmiot;

            var oknoZarzadzaniaOcenami = new OknoZarzadzaniaOcenami(prawdziwyStudent, prawdziwyPrzedmiot);
            oknoZarzadzaniaOcenami.ShowDialog();

            listaPrzedmiotow_SelectionChanged(null, null);
        }
        private void BtnWyloguj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logowanie = new MainWindow();
            logowanie.Show();
            this.Close();
        }
    }
}
