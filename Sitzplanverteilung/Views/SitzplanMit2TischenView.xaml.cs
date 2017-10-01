using Sitzplanverteilung.ViewModels;
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
    /// Interaktionslogik für SitzplanMit2TischenView.xaml
    /// </summary>
    public partial class SitzplanMit2TischenView : UserControl
    {
        public SitzplanMit2TischenView()
        {
            InitializeComponent();
            DataContext = new SitzplanMit1TischModel();     
        }

        // wahrscheinlich brauch ich das hier überhaupt nicht aber ich will es da lassen zur info

        //private void schuelerGenerien()
        //{
        //    var numberOfLabels = 6;
        //    var numberOfTables = 2;
        //    for (int a = 1; a <= numberOfTables; a++)
        //    {
        //        for (int i = 1; i <= numberOfLabels; i++)
        //        {
        //            var labelName = string.Format("tisch{0}Platz{1}Name", a, i);
        //            var labelFirma = string.Format("tisch{0}Platz{1}Firma", a, i);
        //            var bildName = string.Format("tisch{0}Platz{1}Bild", a, i);
        //            var labelFuerName = (Label)this.FindName(labelName);
        //            var labelFuerFirma = (Label)this.FindName(labelFirma);
        //            var bild = (Image)this.FindName(bildName);
        //            labelFuerName.Content = "Musterman, Max";
        //            labelFuerFirma.Content = "Deutsche Bahn";

        //            bild.Source = new BitmapImage(new Uri(@"/Bilder/Cat_Melon.jpg", UriKind.Relative));
        //        }
        //    }
               
        //}
    }
}
