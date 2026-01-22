using dziennik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DziennikGUI
{
    /// <summary>
    /// Logika interakcji dla klasy OknoProwadzacy.xaml
    /// </summary>
    public partial class OknoProwadzacy : Window
    {
        private Prowadzacy zalogowanyProwadzacy;
        private Uczelnia uczelnia;
        public OknoProwadzacy(Prowadzacy prowadzacy, Uczelnia uczelnia)
        {
            InitializeComponent();
            zalogowanyProwadzacy = prowadzacy;
            this.DataContext = zalogowanyProwadzacy;
            this.uczelnia = uczelnia;
            odswiezListePrzedmiotow();
        }

        public void WyswietlProwadzacego()
        {
            if (zalogowanyProwadzacy != null)
            {

            }
        }

        public void odswiezListePrzedmiotow()
        {
            if (zalogowanyProwadzacy != null && uczelnia != null)
            {
                var przedmioty = zalogowanyProwadzacy.ZnajdzPrzedmiotyProwadzacego(uczelnia);
                listaPrzedmiotow.ItemsSource = przedmioty;
            }
        }

        private void listaPrzedmiotow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var wybrany = listaPrzedmiotow.SelectedItem as ProwadzonyPrzedmiot;

            if (wybrany == null) return;

            panelDanePrzedmiotu.DataContext = wybrany;
        }
    }
}
