using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;

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
                if (openFileDialog.FileName.Substring(openFileDialog.FileName.Length - 3) != "csv")
                {
                    MessageBox.Show("Bitte wählen Sie eine CSV-Datei aus.", "Datei", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return schuelerListe;
                }
                StreamReader sr = new StreamReader(openFileDialog.FileName, System.Text.Encoding.Default);
                do
                {
                    zeilenNummer++;
                    zeile = sr.ReadLine();
                    String[] daten = zeile.Split(';'); 
                    // daten Index = Wert in CSV Datei (eventuell dem Konstruktor der Klasse Schüler anpassen?)
                    // bevorzugtes Format   || Faule Lehrer Format
                    // 0 = Nachname         || 0=Nachname
                    // 1 = Vorname          || 1=Vorname
                    // 2 = Klasse           || 2=Klasse
                    // 3 = Firma            || 3=Firma
                    // 4 = Kuerzel          || 4=Geschlecht
                    // 5 = Geschlecht       || 5=Berufsgruppe 
                    // 6 = Berufsgruppe
                    if (pruefeDatensatz(daten))
                    {
                        schuelerListe.Add(new Schueler(daten[0], daten[1], daten[2], daten[3], daten[4], daten[5][0], daten[6]));
                    }
                    else
                    {
                        if (pruefeDatensatzFauleLehrer(daten)) 
                        {
                            schuelerListe.Add(new Schueler(daten[0], daten[1], daten[2], daten[3], "", daten[4][0], daten[5]));
                        }
                        else
                        {
                            fehlerhafteZeilen.Add(zeilenNummer);
                            error = true;
                        }
                    }
                } while (!sr.EndOfStream);
                sr.Close();
            }
            else
            {
                return schuelerListe;
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
            if (daten.Count() != 7)
            {
                return false;
            }
            // Geschlecht nicht M oder W
            if (!(daten[5].ToLower().Equals("m") || daten[5].ToLower().Equals("w")))
            {
                return false;
            }
            return true;
        }
        private static bool pruefeDatensatzFauleLehrer(String[] daten)
        {
            // Fehlerhafte Anzahl an Werten im Datensatz
            if (daten.Count() != 6)
            {
                return false;
            }
            // Geschlecht nicht M oder W
            if (!(daten[4].ToLower().Equals("m") || daten[4].ToLower().Equals("w")))
            {
                return false;
            }
            return true;
        }

        //Mischen der SchülerListe
        public static List<Schueler> Shuffle(List<Schueler> list)
        {

            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                Schueler value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
    }
}
