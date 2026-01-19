using System;
using System.Windows;
using dziennik;

namespace DziennikGUI
{
    public partial class OknoStudent : Window
    {
        Student zalogowanyStudent;

        public OknoStudent(Student student)
        {
            InitializeComponent();

            zalogowanyStudent = student;

            ZaladujDane();
        }

        private void ZaladujDane()
        {
            txtPowitanie.Text = $"Witaj, {zalogowanyStudent.Imie} {zalogowanyStudent.Nazwisko}!";

            listaOcen.Items.Clear();


            foreach (var przedmiot in zalogowanyStudent.PrzedmiotyOceny)
            {
                string nazwaPrzedmiotu =przedmiot.Przedmiot.Nazwa;
                string ects = $"({przedmiot.Przedmiot.Ects} ECTS)";

                string linia = $"{nazwaPrzedmiotu} {ects}";
                listaOcen.Items.Add(linia);

                if (przedmiot.Oceny.Count > 0)
                {
                    string ocenyTekst = "   Oceny: ";
                    foreach (var ocena in przedmiot.Oceny)
                    {
                        ocenyTekst += $"{ocena.Wartosc}, ";
                    }
                    listaOcen.Items.Add(ocenyTekst.TrimEnd(',', ' '));
                }
                else
                {
                    listaOcen.Items.Add("   Brak ocen.");
                }

                listaOcen.Items.Add("");
            }

            if (listaOcen.Items.Count == 0)
            {
                listaOcen.Items.Add("Nie jesteś zapisany na żadne przedmioty.");
            }
        }

        private void BtnWyloguj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logowanie = new MainWindow();
            logowanie.Show();
            this.Close();
        }
    }
}