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
using System.Windows.Shapes;
using System.Xaml;
using Sitzplanverteilung.ViewModels;
namespace Sitzplanverteilung
{
    /// <summary>
    /// Interaktionslogik für SitzplanGUI.xaml
    /// </summary>
    public partial class SitzplanGUI : Window
    {
        SitzplanKartei sk = new SitzplanKartei();
        List<Sitzplan> sitzplaene;

        //TODO: FIX SitzplanMit5TischenView sizing issue with top two tables currently using shared size group that is not resizing

        public SitzplanGUI()
        {
            sitzplaeneDatenOrdnen();
            InitializeComponent();
        }

        private void sitzplaeneDatenOrdnen()
        {
            sk.sitzplaeneGenerierenMitDatei();
            sitzplaene = sk.getSitzplaene();
        }

        private void assignSitzplanView(int anzahlDerTische)
        {
            switch (anzahlDerTische)
            {
                case 1: DataContext = new SitzplanMit1TischModel();
                    break;
                case 2: DataContext = new SitzplanMit2TischenModel();
                    break;
                case 3: DataContext = new SitzplanMit3TischenModel();
                    break;
                case 4: DataContext = new SitzplanMit4TischenModel();
                    break;
                case 5: DataContext = new SitzplanMit5TischenModel();
                    break;
                case 6: DataContext = new SitzplanMit6TischenModel();
                    break;
            }
        }

        private void Block1Button_Checked(object sender, RoutedEventArgs e)
        {
            Sitzplan block = sitzplaene[0];
            var tischGruppen = block.getTischgruppen();
            App.Current.Properties["Block"] = block;
            assignSitzplanView(tischGruppen.Count);
        }

        private void Block2Button_Checked(object sender, RoutedEventArgs e)
        {
            var block = sitzplaene[1];
            var tischGruppen = block.getTischgruppen();
            App.Current.Properties["Block"] = block;
            assignSitzplanView(tischGruppen.Count);
        }

        private void Block6Button_Checked(object sender, RoutedEventArgs e)
        {
            Sitzplan block = sitzplaene[5];
            var tischGruppen = block.getTischgruppen();
            App.Current.Properties["Block"] = block;
            assignSitzplanView(tischGruppen.Count);
        }

        private void Block5Button_Checked(object sender, RoutedEventArgs e)
        {
            Sitzplan block = sitzplaene[4];
            var tischGruppen = block.getTischgruppen();
            App.Current.Properties["Block"] = block;
            assignSitzplanView(tischGruppen.Count);
        }

        private void Block4Button_Checked(object sender, RoutedEventArgs e)
        {
            Sitzplan block = sitzplaene[3];
            var tischGruppen = block.getTischgruppen();
            App.Current.Properties["Block"] = block;
            assignSitzplanView(tischGruppen.Count);
        }

        private void Block3Button_Checked(object sender, RoutedEventArgs e)
        {
            Sitzplan block = sitzplaene[2];
            var tischGruppen = block.getTischgruppen();
            App.Current.Properties["Block"] = block;
            assignSitzplanView(tischGruppen.Count);
        }

    }
}
