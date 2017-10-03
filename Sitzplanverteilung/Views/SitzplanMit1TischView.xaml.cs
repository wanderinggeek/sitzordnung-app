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
                var labelName = string.Format("platz{0}Name", i);
                var labelFirma = string.Format("platz{0}Firma", i);
                var bildName = string.Format("platz{0}Bild", i);
                var labelFuerName = (Label)this.FindName(labelName);
                var labelFuerFirma = (Label)this.FindName(labelFirma);
                var bild = (Image)this.FindName(bildName);
                if (schueler[aktiverSitzplatz] != null)
                {
                    string nachname = schueler[aktiverSitzplatz].getName();
                    string vorname = schueler[aktiverSitzplatz].getVorname();
                    labelFuerName.Content = string.Join(",", nachname, vorname);
                    labelFuerFirma.Content = schueler[aktiverSitzplatz].getFirma();
                }
                else
                {
                    var platzName = string.Format("platz{0}", i);
                    var platz = (Viewbox)this.FindName(platzName);
                    platz.Visibility = Visibility.Hidden;
                }


                bild.Source = new BitmapImage(new Uri(@"/Bilder/Cat_Melon.jpg", UriKind.Relative));

            }
            App.Current.Properties["tischNummer"] = tischNummer + 1;


        }

        private void setNextGroup()
        {

            aktiveTischgruppe = sitzplan[tischNummer];
            schueler = aktiveTischgruppe.getSitzplaetze();

            int test = (int)App.Current.Properties["tischNummer"];
        }
    }
}
