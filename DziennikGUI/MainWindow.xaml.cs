using System;
using System.Windows;
using dziennik;

namespace DziennikGUI
{
    public partial class MainWindow : Window
    {

        Uczelnia uczelnia = new Uczelnia();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnZaloguj_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string haslo = txtHaslo.Password.Trim();

            if (login == "admin" && haslo == "admin")
            {
                OknoDziekanat okno = new OknoDziekanat();
                okno.Show();
                this.Close();
                return;
            }

            var student = uczelnia.Studenci.FirstOrDefault(s => s.NumerAlbumu == login && s.Pesel == haslo);

            if (student != null)
            {
                OknoStudent okno = new OknoStudent();
                okno.Show();
                this.Close();
                return;
            }

            var prowadzacy = uczelnia.Prowadzacy.FirstOrDefault(p => p.Nazwisko == login && p.Pesel == haslo);

            if (prowadzacy != null)
            {
                OknoProwadzacy okno = new OknoProwadzacy();
                okno.Show();
                this.Close();
                return;
            }
        }
    }
}