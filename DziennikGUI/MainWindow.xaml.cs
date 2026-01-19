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

        void ButtonDodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string imie = txtImie.Text;
                string nazwisko = txtNazwisko.Text;
                string pesel = txtPesel.Text;

                Student nowyStudent = new Student(imie, nazwisko, pesel);

                uczelnia.DodajStudenta(nowyStudent);

                OdswiezListe();
                WyczyscPola();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void OdswiezListe()
        {
            listaStudentow.Items.Clear();
            foreach (var student in uczelnia.Studenci)
            {
                listaStudentow.Items.Add(student.PobierzInformacje());
            }
        }

        void WyczyscPola()
        {
            txtImie.Clear();
            txtNazwisko.Clear();
            txtPesel.Clear();
        }
    }
}