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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sitzplanverteilung.Views
{
    /// <summary>
    /// Interaktionslogik für SitzplanMit1TischView.xaml
    /// </summary>
    public partial class SitzplanMit1TischView : UserControl
    {
        List<Tischgruppe> sitzplan = (List<Tischgruppe>)App.Current.Properties["Tischgruppe"];
        Tischgruppe aktiveTischgruppe;
        int tischNummer = (int)App.Current.Properties["tischNummer"];
        List<Schueler> schueler;

        public SitzplanMit1TischView()
        {
            InitializeComponent();
            setNextGroup();
            schuelerGenerien();
        }

        private void schuelerGenerien()
        {
            int tischgroese = aktiveTischgruppe.getGruppengroesse();
            for (int i = 1; i <= tischgroese; i++)
            {
                int aktiverSitzplatz = i - 1;
                var labelName = string.Format("platz{0}NameTB", i);
                var labelFirma = string.Format("platz{0}FirmaTB", i);
                var bildName = string.Format("platz{0}Bild", i);
                var labelFuerName = (TextBlock)this.FindName(labelName);
                var labelFuerFirma = (TextBlock)this.FindName(labelFirma);
                var bild = (Image)this.FindName(bildName);
                if (schueler[aktiverSitzplatz] != null)
                {
                    string firma = schueler[aktiverSitzplatz].firma;
                    string nachname = schueler[aktiverSitzplatz].name;
                    string vorname = schueler[aktiverSitzplatz].vorname;
                    string name = string.Join(",", nachname, vorname);
                    labelFuerName.Text = name;
                    labelFuerName.ToolTip = name;
                    labelFuerFirma.ToolTip = firma;
                    labelFuerFirma.Text = firma;
                }
                else
                {
                    var platzName = string.Format("platz{0}", i);
                    var platz = (Grid)this.FindName(platzName);
                    platz.Visibility = Visibility.Hidden;
                }


                bild.Source = new BitmapImage(new Uri(@"/Bilder/Cat_Melon.jpg", UriKind.Relative));
            }
            App.Current.Properties["tischNummer"] = tischNummer + 1;

        }

        private void setNextGroup()
        {

            aktiveTischgruppe = sitzplan[tischNummer];
            tischNummerLabel.Content =string.Join(" ", "Tisch", (tischNummer + 1).ToString());
            schueler = aktiveTischgruppe.getSitzplaetze();

        }
    }
}
