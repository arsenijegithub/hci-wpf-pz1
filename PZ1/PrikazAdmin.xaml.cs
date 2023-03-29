using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Classes;
using Klasa;

namespace PZ1
{
    /// <summary>
    /// Interaction logic for PrikazAdmin.xaml
    /// </summary>
    public partial class PrikazAdmin : Window
    {
        private DataIO serializer = new DataIO();

        public static BindingList<Ucesnik> Ucesnici { get; set; }

        public string MyString { get; set; }
        public bool MyBool { get; set; }


        public PrikazAdmin()
        {
            Ucesnici = serializer.DeSerializeObject<BindingList<Ucesnik>>("ucesnici.xml");
            if (Ucesnici == null)
            {
                Ucesnici = new BindingList<Ucesnik>();
            }
            //Ucesnici = new BindingList<Ucesnik>();
            DataContext = this;

            InitializeComponent();
        }

        /*
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // Save the data to the data source
            // For example, if you are using a List<T>, you can do this:
            Ucesnici = (BindingList<Ucesnik>)dataGridUcesnici.ItemsSource;
        }
        */

        private void buttonNazad_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonDodajNovog_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeNovogUcesnika dodavanjeNovogUcesnika = new DodavanjeNovogUcesnika();
            dodavanjeNovogUcesnika.ShowDialog();
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            Ucesnici.RemoveAt(dataGridUcesnici.SelectedIndex);
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            IzmenaPostojecegUcesnika izmenaPostojecegUcesnika = new IzmenaPostojecegUcesnika(dataGridUcesnici.SelectedIndex);
            izmenaPostojecegUcesnika.ShowDialog();
        }

        private void buttonDetalji_Click(object sender, RoutedEventArgs e)
        {
            DetaljniPrikazUcesnika detaljniPrikazUcesnika = new DetaljniPrikazUcesnika(dataGridUcesnici.SelectedIndex);
            detaljniPrikazUcesnika.ShowDialog();
        }
        private void save(object sender, CancelEventArgs e)
        {
            serializer.SerializeObject<BindingList<Ucesnik>>(Ucesnici, "ucesnici.xml");
        }

    }
}
