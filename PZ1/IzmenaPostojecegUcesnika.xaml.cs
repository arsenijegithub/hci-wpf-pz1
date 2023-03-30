using Klasa;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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
    /// Interaction logic for IzmenaPostojecegUcesnika.xaml
    /// </summary>
    public partial class IzmenaPostojecegUcesnika : Window
    {
        private int index = 0;
        private string pomocna = "";
        private string fajl_pomocni = "";
        private string slika_pomocna = "";

        public IzmenaPostojecegUcesnika(int idx)
        {
            InitializeComponent();
            Ucesnik ucesnik = PrikazAdmin.Ucesnici[idx];
            index = idx;

            ComboBoxFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            ComboBoxSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
            ComboBoxFamily.SelectedIndex = 2;
            ComboBoxColor.ItemsSource = new List<string>() { "Black", "White", "Yellow", "Red", "Purple", "Orange", "Green", "Brown", "Blue" };
            ComboBoxColor.SelectedIndex = 0;

            textBoxImeUcesnika.Text = ucesnik.imeUcesnika;
            textBoxGodinaRodjenja.Text = Convert.ToString(ucesnik.godinaRodjenja);

            labelSlika.Content = "";
            labelSlika.Background = null;

            slika_pomocna = ucesnik.Slika;
            Uri uri = new Uri(ucesnik.Slika);
            imageSlikaUcesnika.Source = new BitmapImage(uri);

            datePickerDatumDodavanja.Text = Convert.ToString(ucesnik.datumDodavanja);

            fajl_pomocni = ucesnik.Fajl;

            TextRange textRange;
            System.IO.FileStream fileStream;

            if (System.IO.File.Exists(fajl_pomocni))
            {
                textRange = new TextRange(rtbOpisUcesnika.Document.ContentStart, rtbOpisUcesnika.Document.ContentEnd);
                using (fileStream = new System.IO.FileStream(fajl_pomocni, System.IO.FileMode.OpenOrCreate))
                {
                    textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
                }

            }

        }

        private bool validate()
        {
            bool result = true;

            if (textBoxImeUcesnika.Text.Trim().Equals("") || textBoxImeUcesnika.Text.Trim().Equals("Unesite ime:"))
            {
                result = false;
                labelImeUcesnikaGreska.Content = "Obavezno polje!";
                labelImeUcesnikaGreska.Foreground = Brushes.Red;
                textBoxImeUcesnika.BorderBrush = Brushes.Red;
            }

            else
            {
                labelImeUcesnikaGreska.Content = "";
                textBoxImeUcesnika.BorderBrush = Brushes.Gray;
            }

            if (textBoxGodinaRodjenja.Text.Trim().Equals("") || textBoxGodinaRodjenja.Text.Trim().Equals("Unesite godinu rođenja:"))
            {
                result = false;
                textBoxGodinaRodjenja.Foreground = Brushes.Red;
                textBoxGodinaRodjenja.BorderBrush = Brushes.Red;
                textBoxGodinaRodjenja.BorderThickness = new Thickness(1);
                labelGodinaRodjenjaGreska.Content = "Obavezno polje!";
                labelGodinaRodjenjaGreska.Foreground = Brushes.Red;
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
                        labelGodinaRodjenjaGreska.Content = "";
                    }

                    else
                    {
                        result = false;
                        textBoxGodinaRodjenja.Foreground = Brushes.Red;
                        textBoxGodinaRodjenja.BorderBrush = Brushes.Red;
                        textBoxGodinaRodjenja.BorderThickness = new Thickness(1);
                        labelGodinaRodjenjaGreska.Content = "Unesite pozitivan broj (godinu rođenja)";
                    }

                }
                else
                {
                    result = false;
                    labelGodinaRodjenjaGreska.Content = "Unesite broj!";
                }
            }

            var start = rtbOpisUcesnika.Document.ContentStart;
            var end = rtbOpisUcesnika.Document.ContentEnd;
            int difference = start.GetOffsetToPosition(end);

            if (IsRichTextBoxEmpty(rtbOpisUcesnika))
            {
                result = false;
                labelOpisGreska.Foreground = Brushes.Red;
                labelOpisGreska.Content = "Obavezno polje!";
                rtbOpisUcesnika.BorderBrush = Brushes.Red;
                rtbOpisUcesnika.BorderThickness = new Thickness(1);
            }

            if (labelSlika.Content.Equals("slika"))
            {
                result = false;
                buttonIzaberiSliku.BorderBrush = Brushes.Red;
                buttonIzaberiSliku.BorderThickness = new Thickness(1);
                labelSlikaGreska.Content = "Obavezno!";
                labelSlikaGreska.Foreground = Brushes.Red;
                labelSlikaGreska.BorderThickness = new Thickness(1);

            }
            else
            {
                labelSlika.BorderBrush = Brushes.Green;
                labelSlika.BorderThickness = new Thickness(0);
                labelSlikaGreska.Content = "";
                labelSlikaGreska.BorderThickness = new Thickness(0);
                labelSlika.Content = "";
            }

            if (datePickerDatumDodavanja.SelectedDate == null)
            {
                result = false;
                labelDatumGreska.FontSize = 12;
                labelDatumGreska.Content = "Obavezno polje!";
            }
            else
            {
                labelDatumGreska.Content = "";
                labelDatumGreska.BorderThickness = new Thickness(0);

            }

            return result;
        }

        public bool IsRichTextBoxEmpty(RichTextBox rtb)
        {
            if (rtb.Document.Blocks.Count == 0) return true;
            TextPointer startPointer = rtb.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
            TextPointer endPointer = rtb.Document.ContentEnd.GetNextInsertionPosition(LogicalDirection.Backward);

            return startPointer.CompareTo(endPointer) == 0;
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {
                if (pomocna == "")
                {
                    pomocna = slika_pomocna;
                }
                PrikazAdmin.Ucesnici[index] = new Ucesnik(Int32.Parse(textBoxGodinaRodjenja.Text), textBoxImeUcesnika.Text, pomocna, fajl_pomocni, datePickerDatumDodavanja.SelectedDate.Value.Date);

                TextRange textRange;
                FileStream fileStream;
                textRange = new TextRange(rtbOpisUcesnika.Document.ContentStart, rtbOpisUcesnika.Document.ContentEnd);
                fileStream = new FileStream(fajl_pomocni, FileMode.Open);
                textRange.Save(fileStream, DataFormats.Rtf);
                fileStream.Close();

                this.Close();
            }
            else
            {
                MessageBox.Show("Popunite polja!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxImeUcesnika.Foreground = Brushes.Black;
                textBoxGodinaRodjenja.Foreground = Brushes.Black;
            }
        }

        private void buttonNazad_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonIzadji_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBoxFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxFamily.SelectedItem != null)
            {
                rtbOpisUcesnika.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, ComboBoxFamily.SelectedItem);
            }

        }

        private void ComboBoxSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ComboBoxSize.SelectedValue != null)
            {
                rtbOpisUcesnika.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxSize.SelectedValue);
            }

        }

        private void ComboBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxColor.SelectedValue != null)
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

        }

        private void CountWords()
        {
            int count = 0;
            int index = 0;
            string richText = new TextRange(rtbOpisUcesnika.Document.ContentStart, rtbOpisUcesnika.Document.ContentEnd).Text;

            while (index < richText.Length && char.IsWhiteSpace(richText[index]))
            {
                index++;
            }

            while (index < richText.Length)
            {
                while (index < richText.Length && !char.IsWhiteSpace(richText[index]))
                    index++;

                count++;

                while (index < richText.Length && char.IsWhiteSpace(richText[index]))
                    index++;

            }
            TextBlockBrojReci.Text = count.ToString();

        }

        private void rtbOpisUcesnika_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountWords();

        }

        private void buttonIzaberiSliku_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                pomocna = openFileDialog.FileName;
                Uri uri = new Uri(pomocna);
                imageSlikaUcesnika.Source = new BitmapImage(uri);
                labelSlika.Content = "";
                labelSlika.Background = null;
            }

        }


    }
}
