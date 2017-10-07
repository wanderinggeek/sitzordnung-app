using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class SitzplanKartei
    {
        private static SitzplanKartei instance;
        List<Sitzplan> sitzplaene;
        List<Schueler> schuelerListe;

        private SitzplanKartei()
        {
            this.sitzplaene = new List<Sitzplan>();
            for (int i = 0; i < 6; i++)
            {
                this.sitzplaene.Add(null);
            }
            this.schuelerListe = new List<Schueler>();
        }
        public static SitzplanKartei Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SitzplanKartei();
                }
                return instance;
            }
        }

        public void neuerSchuelerInListe(Schueler schueler)
        {
            this.schuelerListe.Add(schueler);
        }
        
        public void entferneSchuelerInListe(Schueler schueler)
        {
            this.schuelerListe.Remove(schueler);
        }
        
        public void entferneSchuelerInListe(int index)
        {
            Schueler schueler = schuelerListe[index];
            entferneSchuelerInListe(schueler);
        }
        
        public void aenderSchuelerInListe(Schueler schuelerAlt, Schueler schuelerNeu)
        {
            this.schuelerListe[this.schuelerListe.IndexOf(schuelerAlt)] = schuelerNeu;
        }
        
        public void aenderSchuelerInListe(int index, Schueler schuelerNeu)
        {
            this.schuelerListe[index] = schuelerNeu;
        }

        //Schülerliste importieren aus .csv-Datei
        public void holeSchuelerAusCSV() 
        {
            schuelerListe.AddRange(Verwaltungskram.importiereSchuelerListe());
        }

        // Standard mit 5 Tischgruppen und 6 Schülern maximal pro Tisch
        public void sitzplaeneGenerieren()
        {
            if (schuelerListe.Count > 0)
            {
                //Erstellen der einzelnen Sitzpläne
                for (int i = 0; i < 6; i++)
                {
                    sitzplaene[i] = new Sitzplan(sitzplanGenerieren(i));
                }
                Console.WriteLine("sitzplan fertiggestellt");
            }
        }


        //Sitzplan verteilen mit variablen Gruppengrößen/Anzahl Tischgruppen
        public void sitzplaeneGenerieren(int anzahlTische, int schuelerMaxProTisch)
        {
            if (schuelerListe.Count > 0)
            {
                //Erstellen der 6 einzelnen Sitzpläne
                for (int i = 0; i < 6; i++)
                {
                    sitzplaene[i] = new Sitzplan(sitzplanGenerieren(anzahlTische, schuelerMaxProTisch, i));
                }
                Console.WriteLine("sitzplan fertiggestellt");
            }
        }

        private Sitzplan sitzplanGenerieren(int index)
        {
            int strafPunkteMin;
            int strafPunkte = 0;
            List<Schueler> liste = new List<Schueler>();
            Sitzplan preSitzplan = new Sitzplan();
            Sitzplan sitzplan = new Sitzplan();
            liste.AddRange(this.schuelerListe);
            sitzplan.verteileSchueler(liste);
            strafPunkteMin = sitzplan.berechneStrafpunkte(this.schuelerListe);
            //Den Besten Sitzplan aus 500 bestimmen
            for (int j = 0; j < 500; j++)
            {
                liste = new List<Schueler>();
                liste.AddRange(this.schuelerListe);
                preSitzplan.verteileSchueler(liste);
                strafPunkte = preSitzplan.berechneStrafpunkte(this.schuelerListe);
                if (index > 0)
                {
                    strafPunkte += moeglichstWenigSitzpartnerDopplungen(index, preSitzplan);
                }
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

        private Sitzplan sitzplanGenerieren(int anzahlTische, int schuelerMaxProTisch, int index)
        {
            int strafPunkteMin;
            int strafPunkte = 0;
            List<Schueler> liste = new List<Schueler>();
            Sitzplan preSitzplan = new Sitzplan(anzahlTische, schuelerMaxProTisch);
            Sitzplan sitzplan = new Sitzplan(anzahlTische, schuelerMaxProTisch);
            liste.AddRange(this.schuelerListe);
            sitzplan.verteileSchueler(liste);
            strafPunkteMin = sitzplan.berechneStrafpunkte(this.schuelerListe);
            //Den Besten Sitzplan aus 500 bestimmen
            for (int j = 0; j < 500; j++)
            {
                liste = new List<Schueler>();
                liste.AddRange(this.schuelerListe);
                preSitzplan.verteileSchueler(liste);
                strafPunkte = preSitzplan.berechneStrafpunkte(this.schuelerListe);
                if (index > 0)
                {
                    strafPunkte += moeglichstWenigSitzpartnerDopplungen(index, preSitzplan);
                }
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

        //Sitzplan mit den vorherigen Plänen vergleichen
        private int moeglichstWenigSitzpartnerDopplungen(int index, Sitzplan sitzplan)
        {
            Sitzplan pruefen = new Sitzplan(sitzplan);
            int strafPunkte = 0;
            
            foreach (Schueler schuelerAusListe in this.schuelerListe) 
            {
                SortedList<String, int> sitznachbarn = new SortedList<string, int>();
                //Vergleichen mit früheren Sitzplänen
                for (int i = index -1; i >= 0; i--)
                {
                    sitznachbarn = sitzNachbarnbestimmen(this.sitzplaene[i], schuelerAusListe, sitznachbarn);
                }
                sitznachbarn = sitzNachbarnbestimmen(sitzplan, schuelerAusListe, sitznachbarn);
                for(int k = 0; k < sitznachbarn.Count; k++)
                {
                    if(sitznachbarn.Values[k] > 1)
                    {
                        strafPunkte += sitznachbarn.Values[k] * 1000;
                    }
                }
            }
            return strafPunkte;
        }

        private SortedList<String, int> sitzNachbarnbestimmen(Sitzplan sitzplan, Schueler schuelerAusListe, SortedList<String, int> sitznachbarn) 
        {
            foreach (Tischgruppe tischgruppe in sitzplan.getTischgruppen())
            {
                int sitzposition = tischgruppe.getSitzplaetze().IndexOf(schuelerAusListe);
                if (sitzposition >= 0)
                {
                    //0 sitzt neben 1
                    if (sitzposition == 0)
                    {
                        sitznachbarn = bestimmeSitznachbar(sitznachbarn, tischgruppe.getSitzplaetze()[1]);
                    }
                    //1 sitzt neben 0 und 2
                    if (sitzposition == 1)
                    {
                        sitznachbarn = bestimmeSitznachbar(sitznachbarn, tischgruppe.getSitzplaetze()[2]);
                        sitznachbarn = bestimmeSitznachbar(sitznachbarn, tischgruppe.getSitzplaetze()[0]);
                    }
                    //2 sitzt neben 1 und 3
                    if (sitzposition == 2)
                    {
                        sitznachbarn = bestimmeSitznachbar(sitznachbarn, tischgruppe.getSitzplaetze()[1]);
                        sitznachbarn = bestimmeSitznachbar(sitznachbarn, tischgruppe.getSitzplaetze()[3]);
                    }
                    //3 sitzt neben 4 und 2
                    if (sitzposition == 3)
                    {
                        sitznachbarn = bestimmeSitznachbar(sitznachbarn, tischgruppe.getSitzplaetze()[4]);
                        sitznachbarn = bestimmeSitznachbar(sitznachbarn, tischgruppe.getSitzplaetze()[2]);
                    }
                    //4 sitzt neben 5 und 3 
                    if (sitzposition == 4)
                    {
                        sitznachbarn = bestimmeSitznachbar(sitznachbarn, tischgruppe.getSitzplaetze()[5]);
                        sitznachbarn = bestimmeSitznachbar(sitznachbarn, tischgruppe.getSitzplaetze()[3]);
                    }
                    //5 sitzt neben 4
                    if (sitzposition == 5)
                    {
                        sitznachbarn = bestimmeSitznachbar(sitznachbarn, tischgruppe.getSitzplaetze()[4]);
                    }
                }
            }
            return sitznachbarn;
        }

        private SortedList<String,int> bestimmeSitznachbar(SortedList<String,int> sitznachbarn, Schueler sitznachbar)
        {
            //Sitzplatz auf Besetztheit prüfen
            if (sitznachbar != null)
            {
                if (sitznachbarn.ContainsKey(sitznachbar.getVollerName()))
                {
                    sitznachbarn[sitznachbar.getVollerName()] += 1;
                }
                else
                {
                    sitznachbarn.Add(sitznachbar.getVollerName(), 1);
                }
            }
            return sitznachbarn;
        }

        public List<Sitzplan> getSitzplaene() 
        {
            return this.sitzplaene;
        }

        public void setSitzplaene(List<Sitzplan> sitzplaene) 
        {
            this.sitzplaene = sitzplaene;
        }

        public List<Schueler> getSchuelerListe()
        {
            return this.schuelerListe;
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
