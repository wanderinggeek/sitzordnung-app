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
            SchuelerDatenEditierenGUI win2 = new SchuelerDatenEditierenGUI();
            win2.Show();
            this.Close();
            string appPath = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            string tmpPath = appPath.Replace("\\bin\\Debug", "\\tmp\\");
            string fileName = "Sitzplan.png";
            Console.WriteLine(tmpPath);
            ImageCapturer.SaveToPNG(win2.contentControl, tmpPath + fileName);
        }
    }
}

