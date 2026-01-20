using dziennik;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace DziennikGUI
{
    public partial class OknoDziekanat : Window
    {
        Uczelnia uczelnia;
        #region
        private List<Semestr> _tymczasoweSemestryKierunku = new List<Semestr>();
        private Semestr _aktualnieTworzonySemestr = new Semestr();
        public ObservableCollection<Kierunek> ListaKierunkow { get; set; }
        #endregion
        public OknoDziekanat(Uczelnia u)
        {
            InitializeComponent();
            this.uczelnia = u;
            OdswiezListeStudentow();
            ListaKierunkow = new ObservableCollection<Kierunek>();
            cmbKierunkow.ItemsSource = ListaKierunkow;
            cmbKierunkow.DisplayMemberPath = "NazwaKierunku";
            OdswiezlisteKierunkow();
            cmbTypSemestru.ItemsSource = Enum.GetValues(typeof(EnumTyp));
            cmbTypSemestru.SelectedIndex = 0;

            cmbProwadzacy.ItemsSource = uczelnia.Prowadzacy;

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
        private void ButtonDodajPrzedmiot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nazwa = txtNazwaPrzedmiotu.Text;

                // Walidacja ECTS
                if (!int.TryParse(txtEcts.Text, out int ects))
                    throw new Exception("Punkty ECTS muszą być liczbą całkowitą.");

                // Pobranie prowadzącego z ComboBoxa
                if (cmbProwadzacy.SelectedItem is not Prowadzacy wybranyProwadzacy)
                    throw new Exception("Wybierz prowadzącego przedmiot.");

                // Tworzenie przedmiotu
                Przedmiot nowyPrzedmiot = new Przedmiot(nazwa, wybranyProwadzacy, ects);

                // Dodanie do aktualnie tworzonego semestru
                _aktualnieTworzonySemestr.DodajPrzedmiot(nowyPrzedmiot);

                // Aktualizacja widoku (np. ListBox pokazujący przedmioty w obecnym semestrze)
                OdswiezPodgladPrzedmiotowWSemestrze();

                // Wyczyszczenie pól przedmiotu
                txtNazwaPrzedmiotu.Clear();
                txtEcts.Clear();
                cmbProwadzacy.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd dodawania przedmiotu", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            private void Button_DodajSemestrClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtRokAkademicki.Text, out int rok))
                    throw new Exception("Rok akademicki musi być liczbą.");

                if (cmbTypSemestru.SelectedItem == null)
                    throw new Exception("Wybierz typ semestru.");

                // Ustawienie danych semestru
                _aktualnieTworzonySemestr.RokAkademicki = rok;
                _aktualnieTworzonySemestr.Typ = (EnumTyp)cmbTypSemestru.SelectedItem;

                // Sprawdzenie czy taki semestr już nie został dodany do listy tymczasowej
                bool istnieje = _tymczasoweSemestryKierunku.Any(s =>
                    s.RokAkademicki == _aktualnieTworzonySemestr.RokAkademicki &&
                    s.Typ == _aktualnieTworzonySemestr.Typ);

                if (istnieje)
                    throw new Exception("Taki semestr został już dodany do listy.");

                // Dodanie gotowego semestru do listy tymczasowej
                _tymczasoweSemestryKierunku.Add(_aktualnieTworzonySemestr);

                // Ważne: Tworzymy NOWĄ instancję dla kolejnego semestru, aby nie nadpisywać starego
                _aktualnieTworzonySemestr = new Semestr();

                // Odświeżenie widoku (ListBox pokazujący dodane semestry)
                OdswiezPodgladSemestrowTworzonych();
                OdswiezPodgladPrzedmiotowWSemestrze(); // Czyści widok przedmiotów, bo nowy semestr jest pusty

                MessageBox.Show("Dodano semestr do kierunku.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd dodawania semestru", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OdswiezlisteSemestrow()
        {
            listaSemestrow.Items.Clear();
            if (listaKierunkow.SelectedIndex > -1)
            {
                Kierunek wybranyKierunek = uczelnia.Kierunki[listaKierunkow.SelectedIndex];
                foreach (var s in wybranyKierunek.Semestry)
                {
                    listaSemestrow.Items.Add(s.PobierzInformacjeS());
                }
            }
        }

        private void Button_ZapiszKierunek_Click(object sender, RoutedEventArgs e)
        {
            string nazwaKierunku = txtNazwaKierunku.Text;
            if(!string.IsNullOrWhiteSpace(nazwaKierunku))
            {
                Kierunek nowyKierunek = new Kierunek(nazwaKierunku);
                uczelnia.DodajKierunek(nowyKierunek);
                OdswiezlisteKierunkow();
                txtNazwaKierunku.Clear();
                MessageBox.Show("Dodano kierunek", "Sukces");
            }
            else
            {
                MessageBox.Show("Nazwa kierunku nie może być pusta", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}