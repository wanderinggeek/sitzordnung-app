using Microsoft.Win32;
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

namespace Sitzplanverteilung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // 6 Schulblöcke sind für eine Berufsschulklasse üblich.
        const int bloecke = 6;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // SitzplanKartei sk = new SitzplanKartei();
            // sk.sitzplaeneGenerierenMitDatei();
            SitzplanGUI win2 = new SitzplanGUI();
            win2.Show();
            this.Close();

        }
    }
}
