using System;
using System.Windows;
using dziennik;

namespace DziennikGUI
{
    public partial class OknoDziekanat : Window
    {
        Uczelnia uczelnia;

        public OknoDziekanat(Uczelnia u)
        {
            InitializeComponent();
            this.uczelnia = u;
            OdswiezListeStudentow();
        }

        private void ButtonDodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string imie = txtImie.Text;
                string nazwisko = txtNazwisko.Text;
                string pesel = txtPesel.Text;

                Student nowyStudent = new Student(imie, nazwisko, pesel);
                uczelnia.DodajStudenta(nowyStudent);

                OdswiezListeStudentow();
                WyczyscPola();

                MessageBox.Show("Dodano studenta", "Sukces");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OdswiezListeStudentow()
        {
            listaStudentow.Items.Clear();
            foreach (var s in uczelnia.Studenci)
            {
                listaStudentow.Items.Add(s.PobierzInformacje());
            }
        }

        private void WyczyscPola()
        {
            txtImie.Clear();
            txtNazwisko.Clear();
            txtPesel.Clear();
        }

        private void MenuWyloguj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logowanie = new MainWindow();
            logowanie.Show();
            this.Close();
        }
    }
}