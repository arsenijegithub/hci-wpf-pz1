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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PZ1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool validacija()
        {
            bool rez = true;

            if (textBoxKorisnickoIme.Text.Trim().Equals("") || textBoxKorisnickoIme.Text.Trim().Equals("Korisničko ime:"))
            {
                rez = false;
                textBoxKorisnickoIme.BorderBrush = Brushes.Red;
                textBoxKorisnickoIme.BorderThickness = new Thickness(1);

                textBoxKorisnickoImeGreska.Text = "Obavezno polje [admin/posetilac]";
                textBoxKorisnickoImeGreska.Foreground = Brushes.Red;

                // buttonPrijava.Background = Brushes.Red;
            }
            else
            {
                textBoxKorisnickoIme.BorderBrush = Brushes.Green;
                textBoxKorisnickoIme.BorderThickness = new Thickness(1);
                textBoxKorisnickoImeGreska.Text = "";
            }

            if (passwordBoxSifra.Password.Trim().Equals("") || passwordBoxSifra.Password.Trim().Equals("Šifra:"))
            {
                rez = false;
                passwordBoxSifra.BorderBrush = Brushes.Red;
                passwordBoxSifra.BorderThickness = new Thickness(1);

                textBoxSifraGreska.Text = "Obavezno polje";
                textBoxSifraGreska.Foreground = Brushes.Red;

                // buttonPrijava.Background = Brushes.Red;

            }
            else
            {
                passwordBoxSifra.BorderBrush = Brushes.Green;
                passwordBoxSifra.BorderThickness = new Thickness(1);
                textBoxSifraGreska.Text = "";
            }

            return rez;
        }

        private void Button_Click_Prijava(object sender, RoutedEventArgs e)
        {
            if (validacija())
            {
                if (textBoxKorisnickoIme.Text.Trim().ToLower().Equals("admin"))
                {
                    PrikazAdmin prikazAdmin = new PrikazAdmin();
                    prikazAdmin.buttonDetalji.Visibility = Visibility.Hidden;
                    prikazAdmin.labelOpisAplikacijePosetilac.Visibility = Visibility.Hidden;

                    prikazAdmin.ShowDialog();
                }
                else if (textBoxKorisnickoIme.Text.Trim().ToLower().Equals("posetilac"))
                {
                    PrikazAdmin prikazAdmin = new PrikazAdmin();
                    prikazAdmin.buttonDodajNovog.Visibility = Visibility.Hidden;
                    prikazAdmin.buttonIzmeni.Visibility = Visibility.Hidden;
                    prikazAdmin.buttonObrisi.Visibility = Visibility.Hidden;
                    prikazAdmin.labelOpisAplikacijeAdmin.Visibility = Visibility.Hidden;
                    
                    prikazAdmin.ShowDialog();
                }
            }
        }

        private void Button_Click_Izadji(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
