using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sitzplanverteilung
{
    public class Menue
    {
        public Menue()
        {
        }

        public static void ExitProgram()
        {
            Application.Current.Shutdown();
        }

        public static void Startseite(Window window)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            window.Close();
        }

        public static void SchuelerDatenEditieren(Window window)
        {
            SchuelerDatenEditierenGUI sde= new SchuelerDatenEditierenGUI();
            sde.Show();
            window.Close();
        }

        public static void Verteilungskriterien(Window window)
        {
            VerteilungskriteriumGUI vk = new VerteilungskriteriumGUI();
            vk.Show();
            window.Close();
        }

        
        public static void Info()
        {
            MessageBox.Show(
                "Sitzordnung-Planer\nVersion 0.95\n\nEntwickelt von\nStefan Apel\nDaniel Berg\nVivian Schmidt\n\nPandabären sind nutzlos!",
                "Information",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        public static void Documentation()
        {
            //
        }
    }
}
