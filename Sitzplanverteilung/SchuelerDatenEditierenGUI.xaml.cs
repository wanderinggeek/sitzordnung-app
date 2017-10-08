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
    /// Interaktionslogik für SchuelerDatenEditierenGUI.xaml
    /// </summary>
    public partial class SchuelerDatenEditierenGUI : Window
    {
        ObservableCollection<Schueler> schuelerCollection;
        List<Schueler> schuelerList;
        SitzplanKartei sitzplanKartei = SitzplanKartei.Instance;
        Schueler selectedSchueler = null;

        public SchuelerDatenEditierenGUI(List<Schueler> _schuelerList = null)
        {
            InitializeComponent();
            schuelerList = _schuelerList;
            if (schuelerList != null)
            {
                this.schuelerCollection = new ObservableCollection<Schueler>(schuelerList);
            }
            else
            {
                this.schuelerCollection = new ObservableCollection<Schueler>();
            }
            setUpDataGrid();

        }


        private void setUpDataGrid()
        {
            schuelerGrid.ItemsSource = this.schuelerCollection;
            schuelerGrid.Items.Refresh();
        }

        private void loadAllScheulerInKartei()
        {
            foreach (Schueler schueler in schuelerCollection)
            {
                if (!schueler.name.Equals("") || !schueler.vorname.Equals("") || !schueler.berufsgruppe.Equals("") || !schueler.firma.Equals(""))
                {
                    if (Char.ToUpper(schueler.geschlecht) == 'M' || Char.ToUpper(schueler.geschlecht) == 'W')
                    {
                        sitzplanKartei.neuerSchuelerInListe(schueler);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Geschlecht", schueler, "Das Geschlecht ist nicht gültig");
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Leer", "Datensatz nicht vollständig ausgefüllt");
                }    
            }
        }

        private void SchuelerdatenAnnehmenButton_Click(object sender, RoutedEventArgs e)
        {
            if (schuelerCollection != null && schuelerCollection.Count > 0)
            {
                try
                {
                    loadAllScheulerInKartei();
                    VerteilungskriteriumGUI verteilungskriteriumGUI = new VerteilungskriteriumGUI();
                    verteilungskriteriumGUI.Show();
                    this.Close();
                }
                catch (ArgumentOutOfRangeException exception) 
                {
                    if (exception.ParamName.Equals("Geschlecht")) 
                    {
                        Schueler s = (Schueler)exception.ActualValue;
                        MessageBox.Show("Der Schüler " + s.name + ", " + s.vorname + " hat ein ungültiges Geschlecht. Nur m und w sind gültige Werte.");
                    }
                    if (exception.ParamName.Equals("Leer"))
                    {
                        MessageBox.Show("Ein oder mehrere Schüler sind nicht vollständig ausgefüllt.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Bitte fügen Sie Schüler hinzu");
            }
        }

        private void SchuelerLoeschenButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSchueler != null)
            {
                schuelerCollection.Remove(selectedSchueler);
                selectedSchueler = null;
                schuelerGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Wählen Sie einen Schüler aus");
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

        private void BilderImportierenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                sitzplanKartei.PictureFolder = dialog.SelectedPath;
            }
        }       

        private void addSchuelerButton_Click(object sender, RoutedEventArgs e)
        {
            Schueler newSchueler = new Schueler();
            schuelerCollection.Add(newSchueler);
        }

     
    }
}
