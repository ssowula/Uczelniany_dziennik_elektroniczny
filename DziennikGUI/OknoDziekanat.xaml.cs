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
        public ObservableCollection<Semestr> SemestryDlaNowegoKierunku { get; set; }
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
            SemestryDlaNowegoKierunku = new ObservableCollection<Semestr>();
            listaSemestrow.ItemsSource = SemestryDlaNowegoKierunku;
            OdswiezPodgladSemestrowTworzonych();

            cmbProwadzacy.ItemsSource = uczelnia.Prowadzacy;

            cmbTytulNaukowy.ItemsSource = Enum.GetValues(typeof(EnumTytulNaukowy));
            cmbTytulNaukowy.SelectedIndex = 0;
            OdswiezListeProwadzacych();
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
        private void ButtonGenerujRaport_Click(object sender, RoutedEventArgs e)
        {
            if(listaStudentow.SelectedIndex >= 0)
            {
                try
                {
                    int indeks = listaStudentow.SelectedIndex;
                    Student wybranyStudent = uczelnia.Studenci[indeks];
                    string trescRaportu = wybranyStudent.GenerujRaport();
                    MessageBox.Show(trescRaportu, "Raport studenta", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ButtonPrzypisz_Cick(object sender, RoutedEventArgs e)
        {
            if (listaStudentow.SelectedIndex < 0 || cmbKierunkow.SelectedItem == null)
            {
                MessageBox.Show("Wybierz studenta i kierunek", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            try
            {
                int indeks = listaStudentow.SelectedIndex;
                Student wybranyStudent = uczelnia.Studenci[indeks];
                Kierunek wybranyKierunek = (Kierunek)cmbKierunkow.SelectedItem;
                wybranyStudent.Kierunek = wybranyKierunek;
                if (wybranyKierunek.Semestry.Count > 0)
                {
                    Semestr pierwszySemestr = wybranyKierunek.Semestry[0];

                    int licznikPrzedmiotow = 0;

                    foreach (var przedmiot in pierwszySemestr.Przedmioty)
                    {
                        try
                        {
                            wybranyStudent.DodajPrzedmiot(przedmiot);
                            licznikPrzedmiotow++;
                        }
                        catch
                        {
                        }
                    }
                    MessageBox.Show($"Przypisano kierunek {wybranyKierunek.NazwaKierunku} studentowi {wybranyStudent.Imie} {wybranyStudent.Nazwisko}", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonDodajPrzedmiot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nazwa = txtNazwaPrzedmiotu.Text;
                if (string.IsNullOrWhiteSpace(nazwa)) throw new Exception("Podaj nazwę przedmiotu");

                if (!int.TryParse(txtEcts.Text, out int ects)) throw new Exception("ECTS musi być liczbą");

                if (cmbProwadzacy.SelectedItem == null) throw new Exception("Wybierz prowadzącego");
                Prowadzacy wybranyProwadzacy = (Prowadzacy)cmbProwadzacy.SelectedItem;

                Przedmiot p = new Przedmiot(nazwa, wybranyProwadzacy, ects);

                _aktualnieTworzonySemestr.DodajPrzedmiot(p);

                listaPrzedmiotow.ItemsSource = null;
                listaPrzedmiotow.ItemsSource = _aktualnieTworzonySemestr.Przedmioty;

                txtNazwaPrzedmiotu.Clear();
                txtEcts.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonDodajSemestr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtRokAkademicki.Text, out int rok)) throw new Exception("Podaj poprawny rok");
                if (cmbTypSemestru.SelectedItem == null) throw new Exception("Wybierz typ semestru");

                _aktualnieTworzonySemestr.RokAkademicki = rok;
                _aktualnieTworzonySemestr.Typ = (EnumTyp)cmbTypSemestru.SelectedItem;

                SemestryDlaNowegoKierunku.Add(_aktualnieTworzonySemestr);

                MessageBox.Show($"Dodano semestr {_aktualnieTworzonySemestr.Typ} {rok} do bufora kierunku.");

                _aktualnieTworzonySemestr = new Semestr();
                listaPrzedmiotow.ItemsSource = null;
                OdswiezPodgladSemestrowTworzonych();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_ZapiszKierunek_Click(object sender, RoutedEventArgs e)
        {
            string nazwaKierunku = txtNazwaKierunku.Text;

            if (string.IsNullOrWhiteSpace(nazwaKierunku))
            {
                MessageBox.Show("Nazwa kierunku nie może być pusta", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SemestryDlaNowegoKierunku.Count == 0)
            {
                var decyzja = MessageBox.Show("Dodajesz kierunek bez semestrów. Czy na pewno?", "Pusty kierunek", MessageBoxButton.YesNo);
                if (decyzja == MessageBoxResult.No) return;
            }

            Kierunek nowyKierunek = new Kierunek(nazwaKierunku);

            foreach (var semestr in SemestryDlaNowegoKierunku)
            {
                try
                {
                    nowyKierunek.DodajSemestr(semestr);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd przy dodawaniu semestru: {ex.Message}");
                }
            }

            uczelnia.DodajKierunek(nowyKierunek);

            SemestryDlaNowegoKierunku.Clear();
            _aktualnieTworzonySemestr = new Semestr();
            txtNazwaKierunku.Clear();
            txtRokAkademicki.Clear();

            OdswiezlisteKierunkow(); 
            OdswiezPodgladSemestrowTworzonych(); 

            MessageBox.Show("Dodano kierunek wraz z semestrami i przedmiotami", "Sukces");
        }
        private void OdswiezlisteKierunkow()
        {
            ListaKierunkow.Clear();
            foreach (var k in uczelnia.Kierunki)
            {
                ListaKierunkow.Add(k);
            }
        }

        private void OdswiezPodgladSemestrowTworzonych()
        {
<<<<<<< HEAD
            listaSemestrow.ItemsSource = null;
            listaSemestrow.ItemsSource = _tymczasoweSemestryKierunku;
        }

        private void OdswiezListeProwadzacych()
        {
            listaProwadzacych.Items.Clear();
            foreach (var p in uczelnia.Prowadzacy)
=======
            foreach (Semestr s in _tymczasoweSemestryKierunku)
>>>>>>> 7d867c16d8ef7e6a936c69d6843c80c51924a77f
            {
                listaProwadzacych.Items.Add(p.PobierzInformacje());
            }
            cmbProwadzacy.ItemsSource = null;
            cmbProwadzacy.ItemsSource = uczelnia.Prowadzacy;
        }

        private void WyczyscPolaProwadzacego()
        {
            txtImieProw.Clear();
            txtNazwiskoProw.Clear();
            txtPeselProw.Clear();
            cmbTytulNaukowy.SelectedIndex = 0;
            listaPrzedmiotowProwadzacego.Items.Clear();
        }

        private void ButtonDodajProw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string imie = txtImieProw.Text;
                string nazwisko = txtNazwiskoProw.Text;
                string pesel = txtPeselProw.Text;
                EnumTytulNaukowy tytul = (EnumTytulNaukowy)cmbTytulNaukowy.SelectedItem;

                Prowadzacy nowy = new Prowadzacy(imie, nazwisko, pesel, tytul);
                uczelnia.DodajProwadzacego(nowy);

                OdswiezListeProwadzacych();
                WyczyscPolaProwadzacego();
                MessageBox.Show("Dodano prowadzącego", "Sukces");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void ButtonUsunProw_Click(object sender, RoutedEventArgs e)
        {
            if (listaProwadzacych.SelectedIndex < 0)
            {
                MessageBox.Show("Wybierz prowadzącego", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int indeks = listaProwadzacych.SelectedIndex;
                Prowadzacy wybrany = uczelnia.Prowadzacy[indeks];
                uczelnia.UsunProwadzacego(wybrany);

                OdswiezListeProwadzacych();
                WyczyscPolaProwadzacego();
                MessageBox.Show("Usunięto prowadzącego", "Sukces");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void ListaProwadzacych_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            bool czyWybrano = listaProwadzacych.SelectedIndex >= 0;
            if (btnUsunProw != null) btnUsunProw.IsEnabled = czyWybrano;

            if (!czyWybrano) return;

            Prowadzacy wybrany = uczelnia.Prowadzacy[listaProwadzacych.SelectedIndex];
            txtImieProw.Text = wybrany.Imie;
            txtNazwiskoProw.Text = wybrany.Nazwisko;
            txtPeselProw.Text = wybrany.Pesel;
            cmbTytulNaukowy.SelectedItem = wybrany.TytulNaukowy;

            listaPrzedmiotowProwadzacego.Items.Clear();
            var przedmioty = wybrany.ZnajdzPrzedmiotyProwadzacego(uczelnia);
            foreach (var pp in przedmioty)
            {
                listaPrzedmiotowProwadzacego.Items.Add($"{pp.Przedmiot.Nazwa} ({pp.Kierunek.NazwaKierunku})");
            }
        }
    }
}
