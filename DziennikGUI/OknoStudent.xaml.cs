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
            txtData.Text = $"Data: {DateTime.Now.ToString("dd.MM.yyyy")}";
            txtNumeralbumu.Text = $"Numer albumu: {zalogowanyStudent.NumerAlbumu}";

            listaOcen.Items.Clear();
            

            foreach (var przedmiot in zalogowanyStudent.PrzedmiotyOceny)
            {
                string nazwaPrzedmiotu =przedmiot.Przedmiot.Nazwa;
                string ects = $"({przedmiot.Przedmiot.Ects} ECTS)";

                string linia = $"{nazwaPrzedmiotu} {ects}";
                listaOcen.Items.Add(linia);
                double srednia = przedmiot.SredniaOcen();
                listaOcen.Items.Add($"   Średnia ocen: {srednia:F2}");

                if (przedmiot.Oceny.Count > 0)
                {
                    string ocenyTekst = "   Oceny: \n";
                    foreach (var ocena in przedmiot.Oceny)
                    {
                        ocenyTekst += $"{ocena.Wartosc} [{ocena.DataWystawienia.ToShortDateString()}] \n";
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