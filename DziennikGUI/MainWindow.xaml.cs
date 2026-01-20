using System;
using System.Linq;
using System.Windows;
using dziennik;

namespace DziennikGUI
{
    public partial class MainWindow : Window
    {
        Uczelnia uczelnia;

        public MainWindow()
        {
            InitializeComponent();

            uczelnia = GeneratorDanych.ZaladujDaneTestowe();

            if (uczelnia.Studenci.Count > 0)
            {
                var s = uczelnia.Studenci[0];
                this.Title = $"Login: {s.NumerAlbumu} | Hasło: {s.Pesel}";

                txtLogin.Text = s.NumerAlbumu;
                txtHaslo.Password = s.Pesel;
            }
        }

        private void BtnZaloguj_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string haslo = txtHaslo.Password.Trim();

            if (login == "admin" && haslo == "admin")
            {
                OknoDziekanat okno = new OknoDziekanat(uczelnia);
                okno.Show();
                this.Close();
                return;
            }

            var student = uczelnia.Studenci.FirstOrDefault(s => s.NumerAlbumu == login && s.Pesel == haslo);
            if (student != null)
            {
                OknoStudent okno = new OknoStudent(student);
                okno.Show();
                this.Close();
                return;
            }

            var prowadzacy = uczelnia.Prowadzacy.FirstOrDefault(p => p.Nazwisko == login && p.Pesel == haslo);
            if (prowadzacy != null)
            {
                OknoProwadzacy okno = new OknoProwadzacy(prowadzacy, uczelnia);
                okno.Show();
                this.Close();
                return;
            }

            MessageBox.Show("Błędne dane logowania", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}