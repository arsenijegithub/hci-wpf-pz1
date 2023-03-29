﻿using Classes;
using Klasa;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for DodavanjeNovogUcesnika.xaml
    /// </summary>
    public partial class DodavanjeNovogUcesnika : Window
    {
        private string tempX = "";

        public DodavanjeNovogUcesnika()
        {
            InitializeComponent();
            textBoxImeUcesnika.Text = "Unesite ime ucesnika:";
            textBoxImeUcesnika.Foreground = Brushes.Thistle;

            textBoxGodinaRodjenja.Text = "Unesite godinu rodjenja ucesnika:";
            textBoxGodinaRodjenja.Foreground = Brushes.Thistle;

            ComboBoxFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            ComboBoxSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
            ComboBoxFamily.SelectedIndex = 2;
            ComboBoxColor.ItemsSource = new List<string>() { "Black", "White", "Yellow", "Red", "Purple", "Orange", "Green", "Brown", "Blue" };
            ComboBoxColor.SelectedIndex = 0;
        }

        private void buttonNazad_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonIzaberiSliku_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                tempX = ofd.FileName;
                Uri uri = new Uri(tempX);
                imageSlikaUcesnika.Source = new BitmapImage(uri);
                labelSlika.Content = "";
                labelSlika.Background = null;
            }
        }

        private bool validate()
        {
            bool result = true;

            if (textBoxImeUcesnika.Text.Trim().Equals("") || textBoxImeUcesnika.Text.Trim().Equals("Ime usesnika:"))
            {
                result = false;
                textBoxImeUcesnika.Foreground = Brushes.Red;
                textBoxImeUcesnika.BorderBrush = Brushes.Red;
                textBoxImeUcesnika.BorderThickness = new Thickness(1);
                /*
                textBoxKorisnickoImeGreska.Text = "Obavezno polje";
                textBoxKorisnickoImeGreska.Foreground = Brushes.Red;
                */
                buttonDodaj.Background = Brushes.Red;
            }
            
            else
            {
                textBoxImeUcesnika.Foreground = Brushes.Black;
                textBoxImeUcesnika.BorderBrush = Brushes.Green;
                textBoxImeUcesnika.BorderThickness = new Thickness(1);
                // textBoxKorisnickoImeGreska.Text = "";
            }

            if (textBoxGodinaRodjenja.Text.Trim().Equals("") || textBoxGodinaRodjenja.Text.Trim().Equals("Godina rodjenja:"))
            {
                result = false;
                textBoxGodinaRodjenja.Foreground = Brushes.Red;
                textBoxGodinaRodjenja.BorderBrush = Brushes.Red;
                textBoxGodinaRodjenja.BorderThickness = new Thickness(1);
                buttonDodaj.Background = Brushes.Red;
            }
            else
            {
                bool isNumeric = int.TryParse(textBoxGodinaRodjenja.Text, out _);
                if (isNumeric)
                {
                    if (Int32.Parse(textBoxGodinaRodjenja.Text) > 0)
                    {
                        textBoxGodinaRodjenja.Foreground = Brushes.Black;
                        textBoxGodinaRodjenja.BorderBrush = Brushes.Green;
                        textBoxGodinaRodjenja.BorderThickness = new Thickness(1);
                        // textBoxGreskaCena.Text = "";
                    }

                    else
                    {
                        result = false;
                        textBoxGodinaRodjenja.Foreground = Brushes.Red;
                        textBoxGodinaRodjenja.BorderBrush = Brushes.Red;
                        textBoxGodinaRodjenja.BorderThickness = new Thickness(1);
                        // textBoxGreskaCena.Text = "Unesite pozitivan broj!";
                    }

                }
                else
                {
                    result = false;
                    textBoxGodinaRodjenja.Foreground = Brushes.Red;
                    textBoxGodinaRodjenja.BorderBrush = Brushes.Red;
                    textBoxGodinaRodjenja.BorderThickness = new Thickness(1);
                    // textBoxGreskaCena.Text = "Unesite broj!";
                }
            }

            /*
            if (textBoxSlika.Text.Trim().Equals("Slika"))
            {
                result = false;
                borderSlika.BorderBrush = Brushes.Red;
                borderSlika.BorderThickness = new Thickness(1);
                labelaGreskaSlika.Content = "Obavezno!";
                labelaGreskaSlika.Background = Brushes.LightGray;
                labelaGreskaSlika.Foreground = Brushes.Red;
                labelaGreskaSlika.BorderThickness = new Thickness(1);



            }
            else
            {
                borderSlika.BorderBrush = Brushes.Green;
                borderSlika.BorderThickness = new Thickness(0);
                labelaGreskaSlika.Content = "";
                labelaGreskaSlika.BorderThickness = new Thickness(0);
                textBoxSlika.Text = "";
            }
            */

            if (datePickerDatumDodavanja.SelectedDate == null)
            {
                result = false;

            }
            else
            {

            }

            return result;
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                if (buttonDodaj.Content.Equals("Dodaj"))
                {
                    string tempNaziv = "";
                    tempNaziv = textBoxImeUcesnika.Text + ".rtf";

                    TextRange textRange;
                    FileStream filestream;
                    textRange = new TextRange(rtbOpisUcesnika.Document.ContentStart, rtbOpisUcesnika.Document.ContentEnd);
                    filestream = new FileStream(tempNaziv, FileMode.Create);
                    textRange.Save(filestream, DataFormats.Rtf);
                    filestream.Close();
                    this.Close();

                    PrikazAdmin.Ucesnici.Add(new Klasa.Ucesnik(Int32.Parse(textBoxGodinaRodjenja.Text), textBoxImeUcesnika.Text, tempX, tempNaziv, datePickerDatumDodavanja.SelectedDate.Value.Date));
                }
                else
                {
                    MessageBox.Show("Popunite sva polja!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ComboBoxFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxFamily.SelectedItem != null && !rtbOpisUcesnika.Selection.IsEmpty)
            {
                rtbOpisUcesnika.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, ComboBoxFamily.SelectedItem);
            }

        }

        private void ComboBoxSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxSize.SelectedValue != null && !rtbOpisUcesnika.Selection.IsEmpty)
            {
                rtbOpisUcesnika.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxSize.SelectedValue);
            }

        }

        private void ComboBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxColor.SelectedValue != null && !rtbOpisUcesnika.Selection.IsEmpty)
            {
                rtbOpisUcesnika.Selection.ApplyPropertyValue(Inline.ForegroundProperty, ComboBoxColor.SelectedValue);
            }

        }

        private void rtbUcesniciSelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbOpisUcesnika.Selection.GetPropertyValue(Inline.FontStyleProperty);
            tglButtonItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = rtbOpisUcesnika.Selection.GetPropertyValue(Inline.FontWeightProperty);
            tglButtonBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = rtbOpisUcesnika.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            tglButtonUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtbOpisUcesnika.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            ComboBoxFamily.SelectedItem = temp;

            temp = rtbOpisUcesnika.Selection.GetPropertyValue(Inline.FontSizeProperty);
            ComboBoxSize.Text = temp.ToString();

            temp = rtbOpisUcesnika.Selection.GetPropertyValue(Inline.ForegroundProperty);

        }

        private void brojReci()
        {
            int brojac = 0;
            int pozicija = 0;
            string richText = new TextRange(rtbOpisUcesnika.Document.ContentStart, rtbOpisUcesnika.Document.ContentEnd).Text;

            while (pozicija < richText.Length && char.IsWhiteSpace(richText[pozicija]))
            {
                pozicija++;
            }

            while (pozicija < richText.Length)
            {
                while (pozicija < richText.Length && !char.IsWhiteSpace(richText[pozicija]))
                    pozicija++;
                brojac++;

                while (pozicija < richText.Length && char.IsWhiteSpace(richText[pozicija]))
                    pozicija++;

            }
            TextBlockBrojReci.Text = brojac.ToString();

        }

        private void rtbOpisUcesnika_TextChanged(object sender, TextChangedEventArgs e)
        {
            brojReci();

        }

        private void buttonIzadji_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
