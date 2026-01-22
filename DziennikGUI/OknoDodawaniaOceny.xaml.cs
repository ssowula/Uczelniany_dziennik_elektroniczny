using dziennik;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DziennikGUI
{
    public partial class OknoDodawaniaOceny : Window
    {
        private Uczelnia uczelnia;
        private Prowadzacy prowadzacy;
        private string numerAlbumu;
        private string nazwaPrzedmiotu;

        public OknoDodawaniaOceny(Uczelnia uczelnia, Prowadzacy prowadzacy, string numerAlbumu, string nazwaPrzedmiotu)
        {
            InitializeComponent();
            this.uczelnia = uczelnia;
            this.prowadzacy = prowadzacy;
            this.numerAlbumu = numerAlbumu;
            this.nazwaPrzedmiotu = nazwaPrzedmiotu;

            var student = uczelnia.Studenci.FirstOrDefault(s => s.NumerAlbumu == numerAlbumu);
            if (student != null)
            {
                txtStudentInfo.Text = $"Student: {student.Imie} {student.Nazwisko} ({numerAlbumu})";
            }
            txtPrzedmiotInfo.Text = $"Przedmiot: {nazwaPrzedmiotu}";
        }

        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
            if (comboOcena.SelectedItem == null)
            {
                MessageBox.Show("Proszę wybrać ocenę.");
                return;
            }

            string ocenaTekst = (comboOcena.SelectedItem as ComboBoxItem).Content.ToString();
            double ocenaWartosc = double.Parse(ocenaTekst, System.Globalization.CultureInfo.InvariantCulture);

            var student = uczelnia.Studenci.FirstOrDefault(s => s.NumerAlbumu == numerAlbumu);
            if (student != null)
            {
                
                var przedmiotOceny = student.PrzedmiotyOceny.FirstOrDefault(po => po.Przedmiot.Nazwa == nazwaPrzedmiotu);
                
                if (przedmiotOceny != null)
                {
                    try
                    {
                        student.DodajOcene(przedmiotOceny.Przedmiot, ocenaWartosc);
                        MessageBox.Show("Ocena została dodana.");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Błąd podczas dodawania oceny: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Nie znaleziono przedmiotu u studenta.");
                }
            }
            else
            {
                MessageBox.Show("Nie znaleziono studenta.");
            }
        }

        private void btnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
