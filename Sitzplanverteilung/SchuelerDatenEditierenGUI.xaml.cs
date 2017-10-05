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
        Schueler item = null;

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
            foreach(Schueler schueler in schuelerCollection)
            {
                sitzplanKartei.neuerSchuelerInListe(schueler);
            }
        }

        private void SchuelerdatenAnnehmenButton_Click(object sender, RoutedEventArgs e)
        {
            if (schuelerList != null)
            {
                loadAllScheulerInKartei();
                SitzplanGUI sitzplanGUI = new SitzplanGUI();
                sitzplanGUI.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Bitte fügen Sie Schüler hinzu");
            }
        }

        private void SchuelerLoeschenButton_Click(object sender, RoutedEventArgs e)
        {
            if(item != null)
            {
                schuelerCollection.Remove(item);
                item = null;
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
            if (index != -1)
            {
                DataGridRow row = dg.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                item = (Schueler)dg.ItemContainerGenerator.ItemFromContainer(row);
            }
   
        }

        private void schuelerGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
        
        }
    }
}
