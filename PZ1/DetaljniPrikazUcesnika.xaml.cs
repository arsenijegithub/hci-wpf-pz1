using Klasa;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PZ1
{
    /// <summary>
    /// Interaction logic for DetaljniPrikazUcesnika.xaml
    /// </summary>
    public partial class DetaljniPrikazUcesnika : Window
    {
        public DetaljniPrikazUcesnika(int index)
        {
            Ucesnik ucesnik = PrikazAdmin.Ucesnici[index];
            InitializeComponent();

            textBoxIme.Text = ucesnik.ImeUcesnika;
            textBoxGodine.Text = Convert.ToString(ucesnik.GodinaRodjenja);

            TextRange textRange;
            System.IO.FileStream fileStream;

            if (System.IO.File.Exists(ucesnik.Fajl))
            {
                textRange = new TextRange(rtbOpisUcesnika.Document.ContentStart, rtbOpisUcesnika.Document.ContentEnd);
                using (fileStream = new System.IO.FileStream(ucesnik.Fajl, System.IO.FileMode.OpenOrCreate))
                {
                    textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
                }
            }

            textBoxDatumDodavanja.Text = ucesnik.DatumDodavanja.ToString("dd/MM/yyyy");

            Uri uri = new Uri(ucesnik.Slika);
            imageSlika.Source = new BitmapImage(uri);

        }

        private void buttonNazad_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonIzadji_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
