using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sitzplanverteilung
{
    class Verwaltungskram
    {
        public static List<Schueler> importiereSchuelerListe ()
        {
            String zeile;
            int zeilenNummer = 0;
            List<Schueler> schuelerListe = new List<Schueler>();
            List<Int32> fehlerhafteZeilen = new List<Int32>();
            bool error = false;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Datei (*.csv)|*.csv"; // Es sollen nur CSV-Dateien geöffnet werden können
            if (openFileDialog.ShowDialog() == true)
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName, System.Text.Encoding.Default);
                do
                {
                    zeilenNummer++;
                    zeile = sr.ReadLine();
                    String[] daten = zeile.Split(';'); 
                    // daten Index = Wert in CSV Datei (eventuell dem Konstruktor der Klasse Schüler anpassen?)
                    // 0 = Nachname
                    // 1 = Vorname
                    // 2 = Klasse
                    // 3 = Firma
                    // 4 = Geschlecht
                    // 5 = Berufsgruppe
                    if (pruefeDatensatz(daten))
                    {
                        schuelerListe.Add(new Schueler(daten[0], daten[1], daten[2], daten[3], daten[4][0], daten[5]));
                    }
                    else
                    {
                        fehlerhafteZeilen.Add(zeilenNummer);
                        error = true;
                    }
                } while (!sr.EndOfStream);
                sr.Close();
            }
            string bericht = Convert.ToString(zeilenNummer - fehlerhafteZeilen.Count) + " von " + Convert.ToString(zeilenNummer) + " Datensätze eingelesen.";
            if (error)
            {
                bericht += "\n\nFehlerhafte Datensätze in den Zeilen " + String.Join(" , ", fehlerhafteZeilen) + " gefunden.";
                bericht += "\n\nBitte den Aufbau der Datensätze gemäß des Anwenderhandbuchs kontrollieren.";
            }
            MessageBox.Show(bericht, "Import Ergebnis", MessageBoxButton.OK, MessageBoxImage.Information);
            return schuelerListe;
        }

        private static bool pruefeDatensatz(String[] daten) 
        {
            // Fehlerhafte Anzahl an Werten im Datensatz
            if (daten.Count() != 6)
            {
                return false;
            }
            return true;
        }
    }
}
