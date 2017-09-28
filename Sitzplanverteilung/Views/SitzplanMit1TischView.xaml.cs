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
        public SitzplanMit1TischView()
        {
            InitializeComponent();
            schuelerGenerien();
        }

        private void schuelerGenerien()
        {
            var numberOfLabels = 6;
            for (int i = 1; i <= numberOfLabels; i++)
            {
                var labelName = string.Format("platz{0}Name", i);
                var labelFirma = string.Format("platz{0}Firma", i);
                var bildName = string.Format("platz{0}Bild", i);
                var labelFuerName = (Label)this.FindName(labelName);
                var labelFuerFirma = (Label)this.FindName(labelFirma);
                var bild = (Image)this.FindName(bildName);
                labelFuerName.Content = "Musterman, Max";
                labelFuerFirma.Content = "Deutsche Bahn";

                bild.Source = new BitmapImage(new Uri(@"/Bilder/Cat_Melon.jpg", UriKind.Relative));

            }
        }
    }
}
