using System;
using System.Windows;
using dziennik;

namespace DziennikGUI
{
    public partial class OknoDziekanat : Window
    {
        Uczelnia uczelnia = new Uczelnia();
        public OknoDziekanat()
        {
            InitializeComponent();
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

                OdswiezListe();

                txtImie.Clear();
                txtNazwisko.Clear();
                txtPesel.Clear();

                MessageBox.Show("Dodano studenta!", "Sukces");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OdswiezListe()
        {
            listaStudentow.Items.Clear();

            foreach (var student in uczelnia.Studenci)
            {
                listaStudentow.Items.Add(student.PobierzInformacje());
            }
        }
    }
}