using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class Sitzplan
    {
        List<Tischgruppe> tischgruppe;
        int maxProTisch;

        public Sitzplan()
        {
            this.tischgruppe = new List<Tischgruppe>();
            this.maxProTisch = 5;
            for (int i = 0; i < 5; i++) 
            {
                hinzufuegenTischgruppe(new Tischgruppe(Decimal.ToInt32(maxProTisch)));
            }
           
        }
        
        public Sitzplan(int anzahlGruppen, int max) 
        {
            this.tischgruppe = new List<Tischgruppe>();
            this.maxProTisch = max;
            for (int i = 0; i < anzahlGruppen; i++)
            {
                hinzufuegenTischgruppe(new Tischgruppe(max));
            }
            
        }

        public void verteileSchueler(List<Schueler> schuelerListe) 
        {
            int moeglichePlaetze = tischgruppe.Count * maxProTisch;
            decimal schuelerProTischTatsaechlich = schuelerListe.Count()/tischgruppe.Count;
            int restPlaetze = schuelerListe.Count() % tischgruppe.Count;
            if (moeglichePlaetze > schuelerListe.Count())
            {
                if (schuelerProTischTatsaechlich < 6) 
                {
                    SortedList<String, int> firmenVerteilung = ermittleFirmen(schuelerListe); 
                    int k = 0;
                    //Verteilung der Schüler
                    foreach (Tischgruppe tisch in tischgruppe) 
                    {
                        int i = 0;
                        //zufälliges Sortieren der Schüler 
                        schuelerListe = Verwaltungskram.Shuffle(schuelerListe);
                        List<Schueler> zufallsListe = new List<Schueler>();
                        zufallsListe.AddRange(schuelerListe);
                        foreach(Schueler schueler in zufallsListe)
                        {
                            if (i < schuelerProTischTatsaechlich) //mehr Bedingungen abfragen, ob Firma schon in Betrieb
                            {
                                //tisch.addSchueler(schueler, i);
                                this.tischgruppe[k].addSchueler(schueler, i);
                                schuelerListe.Remove(schueler);
                                i++;
                            }
                        }
                        if (restPlaetze > 0) 
                        {
                            this.tischgruppe[k].addSchueler(schuelerListe[0], i);
                            schuelerListe.Remove(schuelerListe[0]);
                            restPlaetze--;
                        }
                        k++;                      
                    }
                }
                else
                {
                    //Fehler maximal 6 Schüler pro Tisch
                }
            }
            else 
            {
                //Fehler nicht genug Platz an den Tischen
                //Beispiel User gibt 5 TGs an und 4 Schüler pro Tisch, also Platz für 20 Schüler. Aber 25 Schüler wurden angegeben. 
            }
            //verteilerDummy(schuelerListe);
        }

        public int berechneStrafpunkte() 
        {
            /*
                        Aufbau Tisch:
                        +-----+-----+
                        |-----|-----|
                        |--2--|--3--|
                        |-----|-----|
                        +-----+-----+
                        |-----|-----|
                        |--1--G--4--|
                        |-----|-----|
                        +-----+-----+
                        |-----|-----|
                        |--0--G--5--|
                        |-----|-----|
                        +-----+-----+
                        Gegenüber zählt nicht!!!
                        0 sitzt neben 1 
                        1 sitzt neben 0 und 2
                        2 sitzt neben 1 und 3
                        3 sitzt neben 4 und 2
                        4 sitzt neben 5 und 3 
                        5 sitzt neben 4
              */
            int strafPunkte= 0;
            
            foreach (Tischgruppe tisch in this.tischgruppe) 
            {
                SortedList<String, int> firmenAmTisch = ermittleFirmen(tisch.getGruppe());
                

                //Strafpunkte vergeben durch Sitzen neben Mitschüler aus gleicher Firma
                //Strafpunkte vergeben durch Sitzen neben Mitschüler aus gleichem Beruf
                //Strafpunkte vergeben durch Sitzen neben Mitschüler mit gleichem Geschlecht
                
                if (tisch.getGruppe().Count > 2 && tisch.getGruppe()[1] != null)
                {
                    if (tisch.getGruppe()[0].getFirma().Equals(tisch.getGruppe()[1].getFirma()))
                    {
                        strafPunkte += 3000;
                    }
                    if (tisch.getGruppe()[0].getBerufsgruppe().Equals(tisch.getGruppe()[1].getBerufsgruppe()))
                    {
                        strafPunkte += 2000;
                    }
                    if (tisch.getGruppe()[0].getGeschlecht().Equals(tisch.getGruppe()[1].getGeschlecht()))
                    {
                        strafPunkte += 1000;
                    }
                }

                if (tisch.getGruppe().Count > 3 && tisch.getGruppe()[2] != null)
                {
                    if (tisch.getGruppe()[1].getFirma().Equals(tisch.getGruppe()[2].getFirma()))
                    {
                        strafPunkte += 3000;
                    }
                    if (tisch.getGruppe()[1].getBerufsgruppe().Equals(tisch.getGruppe()[2].getBerufsgruppe()))
                    {
                        strafPunkte += 2000;
                    }
                    if (tisch.getGruppe()[1].getGeschlecht().Equals(tisch.getGruppe()[2].getGeschlecht()))
                    {
                        strafPunkte += 1000;
                    }
                }
                if (tisch.getGruppe().Count > 4 && tisch.getGruppe()[3] != null)
                {
                    if (tisch.getGruppe()[2].getFirma().Equals(tisch.getGruppe()[3].getFirma()))
                    {
                        strafPunkte += 3000;
                    }
                    if (tisch.getGruppe()[2].getBerufsgruppe().Equals(tisch.getGruppe()[3].getBerufsgruppe()))
                    {
                        strafPunkte += 2000;
                    }
                    if (tisch.getGruppe()[2].getGeschlecht().Equals(tisch.getGruppe()[3].getGeschlecht()))
                    {
                        strafPunkte += 1000;
                    }
                }
                if (tisch.getGruppe().Count > 5 && tisch.getGruppe()[4] != null)
                {
                    if ( tisch.getGruppe()[3].getFirma().Equals(tisch.getGruppe()[4].getFirma()))
                    {
                        strafPunkte += 3000;
                    }
                    if (tisch.getGruppe()[3].getBerufsgruppe().Equals(tisch.getGruppe()[4].getBerufsgruppe()))
                    {
                        strafPunkte += 2000;
                    }
                    if (tisch.getGruppe()[3].getGeschlecht().Equals(tisch.getGruppe()[4].getGeschlecht()))
                    {
                        strafPunkte += 1000;
                    }
                }
                if (tisch.getGruppe().Count == 6 && tisch.getGruppe()[5] != null)
                {
                    if (tisch.getGruppe()[4].getFirma().Equals(tisch.getGruppe()[5].getFirma()))
                    {
                        strafPunkte += 3000;
                    }
                    if (tisch.getGruppe()[4].getBerufsgruppe().Equals(tisch.getGruppe()[5].getBerufsgruppe()))
                    {
                        strafPunkte += 2000;
                    }
                    if (tisch.getGruppe()[4].getGeschlecht().Equals(tisch.getGruppe()[5].getGeschlecht()))
                    {
                        strafPunkte += 1000;
                    }
                }
                //Strafpunkte vergeben durch Sitzen neben Mitschüler aus gleicher Berufsgruppe
                //Strafpunkte vergeben durch Sitzen neben Mitschüler aus gleicher Geschlecht
            }
            return strafPunkte;
        }

        public void verteilerDummy(List<Schueler> schuelerListe) 
        {
            int k = 0;
            for (int i = 0; i < this.tischgruppe.Count; i++) 
            {
                this.tischgruppe[i] = new Tischgruppe();
                for (int j = 0; j <= this.maxProTisch && k < schuelerListe.Count(); j++) 
                {
                    tischgruppe[i].addSchueler(schuelerListe.ElementAt(k), j);
                    k++;
                }
            }
        }

        public SortedList<String, int> ermittleFirmen(List<Schueler> schuelerListe) 
        {
            SortedList<String, int> zuordnung = new SortedList<string, int>();
            foreach (Schueler schueler in schuelerListe) 
            {
                if (schueler != null)
                {
                    if (zuordnung.ContainsKey(schueler.getFirma()))
                    {
                        zuordnung[schueler.getFirma()] += 1;
                    }
                    else
                    {
                        zuordnung.Add(schueler.getFirma(), 1);
                    }
                }

            }
            return zuordnung;
        }


        public List<Tischgruppe> getTischgruppen()
        {
            return tischgruppe;
        }

        public Tischgruppe getTischgruppe(int index) 
        {
            return tischgruppe[index];
        }

        public void setTischgruppen(List<Tischgruppe> tischgruppen) 
        {
            this.tischgruppe = tischgruppen;
        }
        public void setTischgruppe(Tischgruppe tischgruppe, int index)
        {
            this.tischgruppe[index] = tischgruppe;
        }

        public List<Schueler> sortiereSchuelerListe(List<Schueler> schuelerListe)
        {
            Schueler speicher;
            //Sortieren nach Geschlecht
            for (int i = 0; i < schuelerListe.Count; i++) 
            {
                for(int j = 0; j< schuelerListe.Count -1; j++)
                {
                    if (schuelerListe[j].getGeschlecht() > schuelerListe[j + 1].getGeschlecht())
                    {
                        speicher = schuelerListe[j];
                        schuelerListe[j] = schuelerListe[j + 1];
                        schuelerListe[j + 1] = speicher;
                    }
                }
            }
            //Sortieren nach Berufsgruppe
            
            //Sortieren nach Firma
                return schuelerListe;
        }
        public void hinzufuegenTischgruppe(Tischgruppe tischgruppe) 
        {
            this.tischgruppe.Add(tischgruppe);
        }

        public override string ToString()
        {
            int i = 1;
            String ausgabe="";
            foreach (Tischgruppe tisch in tischgruppe) 
            {
                ausgabe += "Tischgruppe " + i + ":\n";
                foreach (Schueler schueler in tischgruppe[i - 1].getGruppe()) 
                {
                    ausgabe += schueler.ToString()+"\n";
                }
                i++;
            }
            return ausgabe;
        }

    }
}
