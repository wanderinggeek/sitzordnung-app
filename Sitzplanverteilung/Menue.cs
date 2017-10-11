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
            var dir = new System.IO.DirectoryInfo(System.IO.Path.GetTempPath());

            foreach (var file in dir.EnumerateFiles("*Sitzplan*"))
            {
                try
                {
                    file.Delete();
                }
                catch(Exception error)
                {
                    MessageBox.Show(
                        "Fehler beim Löschen der temporären Dateien:" + error,
                        "Fehler", 
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
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
                "Sitzordnung-Planer\nVersion 1.00\n\nEntwickelt von\nStefan Apel\nDaniel Berg\nVivian Schmidt\n\nPandabären sind nutzlos!",
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
