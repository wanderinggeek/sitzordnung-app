using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Sitzplanverteilung
{
    /// <summary>
    /// Interaktionslogik für TischgruppenAendernGUI.xaml
    /// </summary>
    public partial class TischgruppenAendernGUI : Window
    {
        ObservableCollection<Schueler> schuelerCollection;
        List<Schueler> schuelerListDerKartei = new List<Schueler>();
        Sitzplan sitzplanFuerBlock;
        SitzplanKartei sitzplanKartei = SitzplanKartei.Instance;
        Schueler selectedSchueler = null;
     

        public TischgruppenAendernGUI(int blocknummer)
        {
            this.Title = string.Join(" ", "Block", (blocknummer + 1).ToString(), "ändern");
            InitializeComponent();
            startseite.IsEnabled = true;
            schuelerListDerKartei.AddRange(sitzplanKartei.getSchuelerListe());

            sitzplanFuerBlock = sitzplanKartei.getSitzplan(blocknummer);
            setUpData();
            setUpDataGrid();
        }


        private void setUpDataGrid()
        {
            schuelerGrid.ItemsSource = this.schuelerCollection;
            schuelerGrid.Items.Refresh();
        }

        private void setUpData()
        {
            schuelerCollection = new ObservableCollection<Schueler>(schuelerListDerKartei);

            List<Tischgruppe> gruppen = sitzplanFuerBlock.getTischgruppen();

            foreach (Schueler schueler in schuelerCollection)
            {
                // with index
                foreach (var tischgruppe in gruppen.Select((value, i) => new { i, value }))
                {
                    var tisch = tischgruppe.value;
                    var index = tischgruppe.i;
                    Tischgruppe tischGruppe = (Tischgruppe)tischgruppe.value;
                    List<Schueler> sitzplaetze = tischGruppe.getSitzplaetze();
                    foreach (var sitzplatz in sitzplaetze.Select((value, i) => new { i, value }))
                    {
                        if (sitzplatz.value != null && sitzplatz.value.name == schueler.name)
                        {
                            int echterSitzplatz = sitzplatz.i + 1;
                            int echteTischnummer = tischgruppe.i + 1;
                            schueler.sitzplatznummer = echterSitzplatz.ToString();
                            schueler.tischnummer = echteTischnummer.ToString();
                        }
                    }
                }
            }

        }

        private void schuelerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dg = sender as DataGrid;
            if (dg == null) return;
            var index = dg.SelectedIndex;
            var itemCount = dg.Items.Count;
            if (index != -1 && index != (itemCount - 1))
            {
                DataGridRow row = dg.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                selectedSchueler = (Schueler)dg.ItemContainerGenerator.ItemFromContainer(row);
            }

        }

        private void anderungenSpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            entferneHaeckchen();
            SitzplanGUI sitzplanGUI = new SitzplanGUI();
            sitzplanGUI.Show();
            this.Close();
        }
        private void swapSchuelerButton_Click(object sender, RoutedEventArgs e) 
        {
            int anzahl = 0;
            Schueler schueler1 = new Schueler();
            Schueler schueler2 = new Schueler();
            foreach (Schueler schueler in schuelerGrid.ItemsSource)
            {
                if (((CheckBox)checkGrid.GetCellContent(schueler)).IsChecked == true)
                {
                    if (anzahl == 0) 
                    {
                        schueler1 = schueler;
                        anzahl++;
                    }
                    else if (anzahl == 1) 
                    {
                        schueler2 = schueler;
                        break;
                    }
                }
            }

            sitzplanFuerBlock.tauschePlaetze(schueler1, schueler2);
            String tischnummerSpeicher = schuelerCollection[schuelerCollection.IndexOf(schueler1)].tischnummer;
            String platzSpeicher = schuelerCollection[schuelerCollection.IndexOf(schueler1)].sitzplatznummer;
            entferneHaeckchen();
            schuelerCollection[schuelerCollection.IndexOf(schueler1)].tischnummer = schuelerCollection[schuelerCollection.IndexOf(schueler2)].tischnummer;
            schuelerCollection[schuelerCollection.IndexOf(schueler1)].sitzplatznummer = schuelerCollection[schuelerCollection.IndexOf(schueler2)].sitzplatznummer;

            schuelerCollection[schuelerCollection.IndexOf(schueler2)].tischnummer = tischnummerSpeicher;
            schuelerCollection[schuelerCollection.IndexOf(schueler2)].sitzplatznummer = platzSpeicher;
            schuelerGrid.Items.Refresh();
            MessageBox.Show("Schüler " + schueler1.vorname +" "+ schueler1.name +" wurde mit Schüler "+ schueler2.vorname +" "+ schueler2.name +" getauscht");
        }

        private void entferneHaeckchen() 
        {
            foreach (Schueler schueler in schuelerCollection) 
            {
                schuelerCollection[schuelerCollection.IndexOf(schueler)].istAusgewaehlt = false;
            }
        }


        private void NeuGenerierenButton_Click(object sender, RoutedEventArgs e)
        {
            VerteilungskriteriumGUI vkGUI = new VerteilungskriteriumGUI();
            vkGUI.Show();
            this.Close();
        }
        void OnChecked(object sender, RoutedEventArgs e)
        {
            int anzahl=0;
            foreach (Schueler schueler in this.schuelerCollection)
            {
                if (((CheckBox)checkGrid.GetCellContent(schueler)).IsChecked == true)
                {
                    anzahl++;
                    if (anzahl > 2)
                    {
                        MessageBox.Show("Sie dürfen nicht mehr als 2 Schüler gleichzeigig auswählen.");
                        break;
                    }
                }
            }
            if (anzahl == 2)
            {
                swapSchuelerButton.IsEnabled = true;
            }
            else
            {
                swapSchuelerButton.IsEnabled = false;
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

        private void Startseite(object sender, RoutedEventArgs e)
        {
            Menue.Startseite(this);
        }            
            
        }
    }
}
