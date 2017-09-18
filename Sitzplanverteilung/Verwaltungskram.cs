using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class Verwaltungskram
    {
        public static List<Schueler> importiereSchuelerListe ()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            String zeile;
            List<Schueler> schuelerListe = new List<Schueler>();
            if (openFileDialog.ShowDialog() == true)
            {
                Console.WriteLine(openFileDialog.FileName);
                Console.ReadLine();
                StreamReader sr = new StreamReader(openFileDialog.FileName);
                do
                {
                    zeile = sr.ReadLine();
                    schuelerListe.Add(leseEingabeDatei(zeile));
                } while (!sr.EndOfStream);
                sr.Close();
            }
            //Zwei Zeilen Überschriften enfernen
            schuelerListe.Remove(schuelerListe[0]);
            schuelerListe.Remove(schuelerListe[0]);
            return schuelerListe;
        }

        private static Schueler leseEingabeDatei(String schuelerDaten) 
        {
            Schueler schueler = new Schueler();
            Console.WriteLine(schuelerDaten);
            String[] substrings = schuelerDaten.Split(';');
            schueler.setName(substrings[0]);
            schueler.setVorname(substrings[1]);
            schueler.setFirma(substrings[2]);
            schueler.setBerufsgruppe(substrings[3]);
            schueler.setGeschlecht(substrings[4][0]); 
            return schueler;

        }
    }
}
