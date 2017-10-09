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

            SitzplanGUI sitzplanGUI = new SitzplanGUI();
            sitzplanGUI.Show();
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
