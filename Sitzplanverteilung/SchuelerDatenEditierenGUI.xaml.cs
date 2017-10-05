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
        
        ObservableCollection<Schueler> schueler;
        List<Schueler> schuelerList;

        public SchuelerDatenEditierenGUI()
        {
            InitializeComponent();
            schuelerList = Verwaltungskram.importiereSchuelerListe();
            this.schueler = new ObservableCollection<Schueler>(schuelerList);
            setUpDataGrid();
            
        }

      
        private void setUpDataGrid()
        {
           
            schuelerGrid.ItemsSource = this.schueler;
            schuelerGrid.Items.Refresh();
        }

        private void SchuelerdatenAnnehmenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SchuelerErstellenButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
