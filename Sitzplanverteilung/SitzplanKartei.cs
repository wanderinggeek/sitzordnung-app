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
                do
                {
                    sitzplaene[i] = new Sitzplan(sitzplanGenerieren(eingabeListe));
                } while (i > 0 && !moeglichstWenigSitzpartnerDopplungen(i, eingabeListe));
            }
            foreach (Sitzplan sitz in sitzplaene)
            {
                Console.WriteLine("Block " + sitzplaene.IndexOf(sitz));
                Console.WriteLine(sitz.ToString());
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
            Console.WriteLine("sitzplan fertiggestellt");
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

        //Sitzplan mit den vorherigen Plänen vergleichen
        public Boolean moeglichstWenigSitzpartnerDopplungen(int index, List<Schueler> schuelerListe)
        {
            Sitzplan pruefen = new Sitzplan(this.sitzplaene[index]);
            int strafPunkte = 0;
            
            foreach (Schueler schuelerAusListe in schuelerListe) 
            {
                SortedList<String, int> sitznachbarn = new SortedList<string, int>();
                //Vergleichen mit früheren Sitzplänen
                for (int i = index; i >= 0; i--)
                {
                    foreach (Tischgruppe tischgruppe in this.sitzplaene[i].getTischgruppen())
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
                }
                for(int k = 0; k < sitznachbarn.Count; k++)
                {
                    if(sitznachbarn.Values[k] > 1)
                    {
                        strafPunkte += sitznachbarn.Values[k];
                    }
                }
            }
            if (strafPunkte > 100) 
            {
                //Console.WriteLine("Zuviele Dopplungen : " + strafPunkte);
                return false;
            }
            else 
            {
                //Console.WriteLine("Dopplungen in Ordnung: " + strafPunkte);
                return true;
            }
            
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
