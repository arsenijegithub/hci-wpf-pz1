using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klasa
{

    [Serializable]
    public class Ucesnik
    {
        public int godinaRodjenja;
        public string imeUcesnika;
        public string slika;
        public string fajl;
        public DateTime datumDodavanja;

        public bool IsSelected {
            get;
            set;
        }

        public Ucesnik(int godinaRodjenja, string imeUcesnika, string slika, string fajl, DateTime datumDodavanja)
        {
            this.godinaRodjenja = godinaRodjenja;
            this.imeUcesnika = imeUcesnika;
            this.slika = slika;
            this.fajl = fajl;
            this.datumDodavanja = datumDodavanja;
        }

        public Ucesnik(){}

        public int GodinaRodjenja
        {
            get { return godinaRodjenja; }
            set { godinaRodjenja = value; }
        }
        public string ImeUcesnika {
            get { return imeUcesnika; }
            set { imeUcesnika = value; }
        }
        public string Slika {
            get { return slika; }
            set { slika = value; }
        }
        public string Fajl {
            get { return fajl; }
            set { fajl = value; }
        }
        public DateTime DatumDodavanja
        {
            get { return DateTime.Now; }
        }

        static public void Main(String[] args)
        {

        }

    }
}
