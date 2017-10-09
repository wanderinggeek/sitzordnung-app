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
using System.Windows.Shapes;
using System.Xaml;
using Sitzplanverteilung.ViewModels;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace Sitzplanverteilung
{
    /// <summary>
    /// Interaktionslogik für SitzplanGUI.xaml
    /// </summary>
    public partial class SitzplanGUI : Window
    {
        SitzplanKartei sk = SitzplanKartei.Instance;
        List<Sitzplan> sitzplaene;
        Sitzplan aktiverBlock;
        int blockNummer;
        public string klasse {get; set;}

        //TODO: FIX SitzplanMit5TischenView sizing issue with top two tables currently using shared size group that is not resizing

        public SitzplanGUI()
        {
            sitzplaene = sk.getSitzplaene();
            InitializeComponent();
            sitzungSpeichern.IsEnabled = true;
            pdf.IsEnabled = true;
            druck.IsEnabled = true;
            startseite.IsEnabled = true;
            schuelerdatenedit.IsEnabled = true;
            verteilungskriterien.IsEnabled = true;
            setKlassennummer();

        }

        private void setKlassennummer()
        {
            List<Schueler> schuelerListe = sk.getSchuelerListe();
            Schueler schueler = schuelerListe[1];
            klasse = schueler.klasse;
            Klassennummer.Text = klasse;
        }

        private void assignSitzplanView(int anzahlDerTische)
        {
            switch (anzahlDerTische)
            {
                case 1:

                    this.DataContext = null;
                    this.UpdateLayout();
                    this.DataContext = new SitzplanMit1TischModel();
                    break;
                case 2:

                    this.DataContext = null;
                    this.UpdateLayout();
                    this.DataContext = new SitzplanMit2TischenModel();
                    break;
                case 3:

                    this.DataContext = null;
                    this.UpdateLayout();
                    this.DataContext = new SitzplanMit3TischenModel();
                    break;
                case 4:

                    this.DataContext = null;
                    this.UpdateLayout();
                    this.DataContext = new SitzplanMit4TischenModel();
                    break;
                case 5:
                    this.DataContext = null;
                    this.UpdateLayout();
                    this.DataContext = new SitzplanMit5TischenModel();
                    break;
                case 6:

                    this.DataContext = null;
                    this.UpdateLayout();
                    this.DataContext = new SitzplanMit6TischenModel();
                    break;
            }
        }

        private void BlockButton_Checked(object sender, RoutedEventArgs e)
        {
            string buttonName;

            RadioButton radio = sender as RadioButton;

            if (radio != null)
            {
                buttonName = radio.Name.ToString();
            }
            else
            {
                return;
            }

            blockNummer = Int32.Parse(Regex.Replace(buttonName, "[^0-9 _]", "")) - 1; 
            aktiverBlock = sitzplaene[blockNummer];
            var tischGruppen = aktiverBlock.getTischgruppen();
            App.Current.Properties["Block"] = aktiverBlock;
            assignSitzplanView(tischGruppen.Count);
        }  
        
        private void MakePDF(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            MakeBlockPictures();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Datei (*.pdf)|*.pdf";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.FileName = "Sitzplan";
            if (saveFileDialog.ShowDialog() == true)
                try
                {
                    PDFCreation.MakePDF(saveFileDialog.FileName);
                    MessageBox.Show("Die Datei " + saveFileDialog.FileName + " wurde gespeichert.", "Ergebnis", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch(Exception error)
                {
                    MessageBox.Show("Fehler: " + error, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            this.Cursor = Cursors.Arrow;
        }

        private void MakeBlockPictures()
        {
            string tmpPath = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]).Replace("\\bin\\Debug", "\\tmp\\");
            string picName = "Sitzplan-Block{}.png";

            for (int i = 5; i >= 0; i--)
            {
                var block = sitzplaene[i];
                var tischGruppen = block.getTischgruppen();
                App.Current.Properties["Block"] = block;
                assignSitzplanView(tischGruppen.Count);
                this.UpdateLayout();

                string tmpPicName = picName.Replace("{}", Convert.ToString(i+1));

                ImageCapturer.SaveToPNG(this.contentControl, tmpPath + tmpPicName);
            }
        }

        private void Neu(object sender, RoutedEventArgs e)
        {
            Menue.Startseite(this);
        }

        private void End(object sender, RoutedEventArgs e)
        {
            Menue.ExitProgram();
        }

        private void Info(object sender, RoutedEventArgs e)
        {
            Menue.Info();
        }

        private void Documentation(object sender, RoutedEventArgs e)
        {
            Menue.Documentation();
        }
        
        private void SitzungSpeichern(object sender, RoutedEventArgs e)
        {
            sk.Save();
        }

        private void Drucken(object sender, RoutedEventArgs e)
        {
            MakeBlockPictures();
            string tmpPath = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]).Replace("\\bin\\Debug", "\\tmp\\");
            PDFCreation.MakePDF(tmpPath + "tmp_Sitzplan.pdf");

            WebBrowser wb = new WebBrowser();
            wb.Navigate(new Uri(tmpPath + "tmp_Sitzplan.pdf"));

            PdfGUI pdfGUI = new PdfGUI();

            pdfGUI.Content = wb;
            pdfGUI.Show();
        }

        private void Startseite(object sender, RoutedEventArgs e)
        {
            Menue.Startseite(this);
        }

        private void Schuelerdaten(object sender, RoutedEventArgs e)
        {
            SchuelerDatenEditierenGUI schuelerDatenGUI = new SchuelerDatenEditierenGUI(sk.getSchuelerListe());
            schuelerDatenGUI.Show();
            this.Close();
        }

        private void Kriterien(object sender, RoutedEventArgs e)
        {
            Menue.Verteilungskriterien(this);
        }


        private void BlockIconViewbox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Viewbox vb = sender as Viewbox;
            string vbName = vb.Name.ToString();


            blockNummer = Int32.Parse(Regex.Replace(vbName, "[^0-9 _]", "")) - 1;
            TischgruppenAendernGUI taGUI = new TischgruppenAendernGUI(blockNummer);
            taGUI.Show();
            this.Close();
        }

        private void NeuGenerierenButton_Click(object sender, RoutedEventArgs e)
        {
            VerteilungskriteriumGUI vkGUI = new VerteilungskriteriumGUI();
            vkGUI.Show();
            this.Close();
        }

         

    }
}