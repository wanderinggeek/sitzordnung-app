using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class SitzplanKartei
    {
        List<Sitzplan> sitzplaene;

        public SitzplanKartei()
        {
            this.sitzplaene = new List<Sitzplan>();
            for (int i = 0; i < 6; i++ )
            {
                this.sitzplaene.Add(null);
            }
        }

        public SitzplanKartei(List<Sitzplan> sitzplaene)
        {
            this.sitzplaene = new List<Sitzplan>();
            for (int i = 0; i < 6; i++)
            {
                this.sitzplaene.Add(new Sitzplan(sitzplaene[i]));
            }
        }

        // Standard mit 5 Tischgruppen und 6 Schülern maximal pro Tisch
        public void sitzplaeneGenerieren()
        {
            //Schülerliste importieren
            List<Schueler> eingabeListe = new List<Schueler>();
            eingabeListe.AddRange(Verwaltungskram.importiereSchuelerListe());
            
            //Erstellen der einzelnen Sitzpläne
            for (int i = 0; i < 6; i++)
            {
                sitzplaene[i] = new Sitzplan(sitzplanGenerieren(eingabeListe));
            }
            Console.WriteLine("sitzplan fertiggestellt");
        }


        //Sitzplan verteilen mit variablen Gruppengrößen/Anzahl Tischgruppen
        public void sitzplaeneGenerieren(int anzahlTische, int schuelerMaxProTisch)
        {
            //Schülerliste importieren
            List<Schueler> liste = Verwaltungskram.importiereSchuelerListe();

            //Erstellen der 6 einzelnen Sitzpläne
            for (int i = 0; i < 6; i++)
            {
                sitzplaene[i] = new Sitzplan(sitzplanGenerieren(liste, anzahlTische, schuelerMaxProTisch));
            }
            Console.WriteLine("fertig");
        }

        private Sitzplan sitzplanGenerieren(List<Schueler> schuelerListe)
        {
            int strafPunkteMin;
            int strafPunkte = 0;
            List<Schueler> liste = new List<Schueler>();
            Sitzplan preSitzplan = new Sitzplan();
            Sitzplan sitzplan = new Sitzplan();
            liste.AddRange(schuelerListe);
            sitzplan.verteileSchueler(liste);
            strafPunkteMin = sitzplan.berechneStrafpunkte(schuelerListe);
            //Den Besten Sitzplan aus 500 bestimmen
            for (int j = 0; j < 500; j++)
            {
                liste = new List<Schueler>();
                liste.AddRange(schuelerListe);
                preSitzplan.verteileSchueler(liste);
                strafPunkte = preSitzplan.berechneStrafpunkte(schuelerListe);
                if (strafPunkteMin > strafPunkte)
                {
                    strafPunkteMin = strafPunkte;
                    sitzplan = new Sitzplan(preSitzplan);
                    if (strafPunkteMin == 0)
                    {
                        j = 500;
                    }
                }
            }
            return sitzplan;
        }

        private Sitzplan sitzplanGenerieren(List<Schueler> schuelerListe, int anzahlTische, int schuelerMaxProTisch)
        {
            int strafPunkteMin;
            int strafPunkte = 0;
            List<Schueler> liste = new List<Schueler>();
            Sitzplan preSitzplan = new Sitzplan(anzahlTische, schuelerMaxProTisch);
            Sitzplan sitzplan = new Sitzplan(anzahlTische, schuelerMaxProTisch);
            liste.AddRange(schuelerListe);
            sitzplan.verteileSchueler(liste);
            strafPunkteMin = sitzplan.berechneStrafpunkte(schuelerListe);
            //Den Besten Sitzplan aus 500 bestimmen
            for (int j = 0; j < 500; j++)
            {
                liste = new List<Schueler>();
                liste.AddRange(schuelerListe);
                preSitzplan.verteileSchueler(liste);
                strafPunkte = preSitzplan.berechneStrafpunkte(schuelerListe);
                if (strafPunkteMin > strafPunkte)
                {
                    strafPunkteMin = strafPunkte;
                    sitzplan = new Sitzplan(preSitzplan);
                    if (strafPunkteMin == 0)
                    {
                        j = 500;
                    }
                }
            }
            return sitzplan;
        }

        public List<Sitzplan> getSitzplaene() 
        {
            return this.sitzplaene;
        }

        public void setSitzplaene(List<Sitzplan> sitzplaene) 
        {
            this.sitzplaene = sitzplaene;
        }

        public Sitzplan getSitzplan(int index)
        {
            return this.sitzplaene[index];
        }


        public void setSitzplaene(Sitzplan sitzplan, int index)
        {
            this.sitzplaene[index] = sitzplan;
        }
           
    }
}
