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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sitzplanverteilung
{
    /// <summary>
    /// Interaktionslogik für VerteilungskriteriumGUI.xaml
    /// </summary>
    public partial class VerteilungskriteriumGUI : Window
    {
        SitzplanKartei sitzplanKartei = SitzplanKartei.Instance;
        int anzahlDerTische;
        int schuelerProTisch;
        bool filterNachFirma;
        bool filterNachBeruf;

        public VerteilungskriteriumGUI()
        {
            InitializeComponent();
            filterNachFirma = firmaCheckbox.IsChecked.Value;
            filterNachBeruf = berufCheckbox.IsChecked.Value;
        }

        private void SitzplaeneGenerierenButton_Click(object sender, RoutedEventArgs e)
        {
            anzahlDerTische = (int)AnzahlDerTischeTool.Value;
            schuelerProTisch = (int)SchuelerProTischTool.Value;
            try
            {
                this.Cursor = Cursors.Wait;

                sitzplanKartei.sitzplaeneGenerieren(anzahlDerTische, schuelerProTisch, filterNachFirma, filterNachBeruf);
                SitzplanGUI sitzplanGUI = new SitzplanGUI();
                sitzplanGUI.Show();
                this.Close();
            }
            catch (ArgumentOutOfRangeException exception) 
            {
                if (exception.ParamName.Equals("Schüleranzahl")) 
                {
                    MessageBox.Show("Nicht genug Sitzplätze.");
                }
            }
        }

        private void SchuelerdatenEinsehenButton_Click(object sender, RoutedEventArgs e)
        {
            SchuelerDatenEditierenGUI schuelerDatenGUI = new SchuelerDatenEditierenGUI(sitzplanKartei.getSchuelerListe());
            schuelerDatenGUI.Show();
            this.Close();
        }

        private void firmaCheckbox_Click(object sender, RoutedEventArgs e)
        {
            filterNachFirma = !filterNachFirma;
        }

        private void berufCheckbox_Click(object sender, RoutedEventArgs e)
        {
            filterNachBeruf = !filterNachBeruf;
        }
    }
}
