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

        public VerteilungskriteriumGUI()
        {
            InitializeComponent();      
        }

        private void SitzplaeneGenerierenButton_Click(object sender, RoutedEventArgs e)
        {
            anzahlDerTische = (int)AnzahlDerTischeTool.Value;
            schuelerProTisch = (int)SchuelerProTischTool.Value;
            sitzplanKartei.sitzplaeneGenerieren(anzahlDerTische, schuelerProTisch);
            SitzplanGUI sitzplanGUI = new SitzplanGUI();
            sitzplanGUI.Show();
            this.Close();
        }

        private void SchuelerdatenEinsehenButton_Click(object sender, RoutedEventArgs e)
        {
            SchuelerDatenEditierenGUI schuelerDatenGUI = new SchuelerDatenEditierenGUI(sitzplanKartei.getSchuelerListe());
            schuelerDatenGUI.Show();
            this.Close();
        }
    }
}
