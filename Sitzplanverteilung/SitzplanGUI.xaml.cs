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
        List<Sitzplan> karteien;

        public SitzplanGUI()
        {
            sitzplaeneDatenOrdnen();
          
            InitializeComponent();
            DataContext = new SitzplanMit2TischenModel();
        }

        private void sitzplaeneDatenOrdnen ()
        {
            sk.sitzplaeneGenerierenMitDatei();
            karteien = sk.getSitzplaene();
        }

       //TODO Make radio buttons change to purple when selected
    }
}
