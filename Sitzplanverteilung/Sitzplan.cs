using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class Sitzplan
    {
        List<Tischgruppe> tischgruppen;
        int maxProTisch;

        public Sitzplan()
        {
            this.tischgruppen = new List<Tischgruppe>();
            this.maxProTisch = 6;
            for (int i = 0; i < 5; i++)
            {
                this.tischgruppen.Add(new Tischgruppe());
            }
        }

        public Sitzplan(int anzahlGruppen, int max)
        {
            this.tischgruppen = new List<Tischgruppe>();
            this.maxProTisch = max;
            for (int i = 0; i < anzahlGruppen; i++)
            {
                this.tischgruppen.Add(new Tischgruppe());
            }
        }

        public Sitzplan(Sitzplan sitzplan)
        {
            this.maxProTisch = 6;
            this.tischgruppen = new List<Tischgruppe>();
            for (int i = 0; i < sitzplan.getTischgruppen().Count; i++)
            {
                int j = 0;
                this.tischgruppen.Add(new Tischgruppe());
                foreach (Schueler schueler in sitzplan.getTischgruppe(i).getSitzplaetze()) 
                {
                    this.tischgruppen[i].setzeSchueler(schueler, j);
                    j++;
                }
            }
        }

        public void verteileSchueler(List<Schueler> schuelerListe)
        {
            int moeglichePlaetze = tischgruppen.Count * maxProTisch;
            decimal schuelerProTischTatsaechlich = schuelerListe.Count() / tischgruppen.Count;
            int restPlaetze = schuelerListe.Count() % tischgruppen.Count;
            if (moeglichePlaetze > schuelerListe.Count())
            {
                if (schuelerProTischTatsaechlich < 6)
                {
                    SortedList<String, int> firmenVerteilung = ermittleFirmen(schuelerListe);
                    int k = 0;
                    //Verteilung der Schüler
                    foreach (Tischgruppe tisch in tischgruppen)
                    {
                        int i = 0;
                        //zufälliges Sortieren der Schüler 
                        schuelerListe = Verwaltungskram.Shuffle(schuelerListe);
                        List<Schueler> zufallsListe = new List<Schueler>();
                        zufallsListe.AddRange(schuelerListe);
                        foreach (Schueler schueler in zufallsListe)
                        {
                            if (i < schuelerProTischTatsaechlich) //mehr Bedingungen abfragen, ob Firma schon in Betrieb
                            {
                                //tisch.addSchueler(schueler, i);
                                this.tischgruppen[k].setzeSchueler(schueler, i);
                                schuelerListe.Remove(schueler);
                                i++;
                            }
                        }
                        if (restPlaetze > 0)
                        {
                            this.tischgruppen[k].setzeSchueler(schuelerListe[0], i);
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

        public int berechneStrafpunkte(List<Schueler> schuelerListe, bool beachteFirma, bool beachteBeruf)
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
            int strafPunkte = 0;
            int firmenPlatzregel = 30;
            int berufsPlatzregel = 5;
            int geschlechtPlatzregel = 1;
            SortedList<String, int> firmenGesamt = ermittleFirmen(schuelerListe);
            //Anwender ist es egal, ob Schüler mit gleicher Firmenzugehörigkeit zusammensitzen
            if (!beachteFirma) 
            {
                firmenPlatzregel = 0;
            }
            //Anwender ist es egal, ob Schüler mit gleichem Beruf zusammensitzen
            if (!beachteBeruf) 
            {
                berufsPlatzregel = 0;
            }

            foreach (Tischgruppe tisch in this.tischgruppen)
            {
                SortedList<String, int> firmenAmTisch = ermittleFirmen(tisch.getSitzplaetze());
                //Prüfen, ob an einem Tisch mehr Schüler einer Firma sitzen als zugelassen
                foreach(String key in firmenAmTisch.Keys)
                {
                    if ((firmenGesamt[key] / tischgruppen.Count) + 1 < firmenAmTisch[key]) 
                    {
                        strafPunkte += 100;
                    }
                }
                //Strafpunkte vergeben durch Sitzen neben Mitschüler aus gleicher Firma
                //Strafpunkte vergeben durch Sitzen neben Mitschüler aus gleichem Beruf
                //Strafpunkte vergeben durch Sitzen neben Mitschüler mit gleichem Geschlecht

                if (tisch.getSitzplaetze().Count > 2 && tisch.getSitzplaetze()[1] != null)
                {
                    if (tisch.getSitzplaetze()[0].firma.Equals(tisch.getSitzplaetze()[1].firma))
                    {
                        strafPunkte += firmenPlatzregel;
                    }
                    if (tisch.getSitzplaetze()[0].berufsgruppe.Equals(tisch.getSitzplaetze()[1].berufsgruppe))
                    {
                        strafPunkte += berufsPlatzregel;
                    }
                    if (tisch.getSitzplaetze()[0].geschlecht.Equals(tisch.getSitzplaetze()[1].geschlecht))
                    {
                        strafPunkte += geschlechtPlatzregel;
                    }
                }

                if (tisch.getSitzplaetze().Count > 3 && tisch.getSitzplaetze()[2] != null)
                {
                    if (tisch.getSitzplaetze()[1].firma.Equals(tisch.getSitzplaetze()[2].firma))
                    {
                        strafPunkte += firmenPlatzregel;
                    }
                    if (tisch.getSitzplaetze()[1].berufsgruppe.Equals(tisch.getSitzplaetze()[2].berufsgruppe))
                    {
                        strafPunkte += berufsPlatzregel;
                    }
                    if (tisch.getSitzplaetze()[1].geschlecht.Equals(tisch.getSitzplaetze()[2].geschlecht))
                    {
                        strafPunkte += geschlechtPlatzregel;
                    }
                }
                if (tisch.getSitzplaetze().Count > 4 && tisch.getSitzplaetze()[3] != null)
                {
                    if (tisch.getSitzplaetze()[2].firma.Equals(tisch.getSitzplaetze()[3].firma))
                    {
                        strafPunkte += firmenPlatzregel;
                    }
                    if (tisch.getSitzplaetze()[2].berufsgruppe.Equals(tisch.getSitzplaetze()[3].berufsgruppe))
                    {
                        strafPunkte += berufsPlatzregel;
                    }
                    if (tisch.getSitzplaetze()[2].geschlecht.Equals(tisch.getSitzplaetze()[3].geschlecht))
                    {
                        strafPunkte += geschlechtPlatzregel;
                    }
                }
                if (tisch.getSitzplaetze().Count > 5 && tisch.getSitzplaetze()[4] != null)
                {
                    if (tisch.getSitzplaetze()[3].firma.Equals(tisch.getSitzplaetze()[4].firma))
                    {
                        strafPunkte += firmenPlatzregel;
                    }
                    if (tisch.getSitzplaetze()[3].berufsgruppe.Equals(tisch.getSitzplaetze()[4].berufsgruppe))
                    {
                        strafPunkte += berufsPlatzregel;
                    }
                    if (tisch.getSitzplaetze()[3].geschlecht.Equals(tisch.getSitzplaetze()[4].geschlecht))
                    {
                        strafPunkte += geschlechtPlatzregel;
                    }
                }
                if (tisch.getSitzplaetze().Count == 6 && tisch.getSitzplaetze()[5] != null)
                {
                    if (tisch.getSitzplaetze()[4].firma.Equals(tisch.getSitzplaetze()[5].firma))
                    {
                        strafPunkte += firmenPlatzregel;
                    }
                    if (tisch.getSitzplaetze()[4].berufsgruppe.Equals(tisch.getSitzplaetze()[5].berufsgruppe))
                    {
                        strafPunkte += berufsPlatzregel;
                    }
                    if (tisch.getSitzplaetze()[4].geschlecht.Equals(tisch.getSitzplaetze()[5].geschlecht))
                    {
                        strafPunkte += geschlechtPlatzregel;
                    }
                }

            }
            return strafPunkte;
        }

        public void verteilerDummy(List<Schueler> schuelerListe)
        {
            int k = 0;
            for (int i = 0; i < this.tischgruppen.Count; i++)
            {
                this.tischgruppen[i] = new Tischgruppe();
                for (int j = 0; j < this.maxProTisch && k < schuelerListe.Count(); j++)
                {
                    tischgruppen[i].setzeSchueler(schuelerListe[k], j);
                    k++;
                }
            }
        }

        

        public List<Tischgruppe> getTischgruppen()
        {
            return tischgruppen;
        }

        public Tischgruppe getTischgruppe(int index)
        {
            return tischgruppen[index];
        }

        public void setTischgruppen(List<Tischgruppe> tischgruppen)
        {
            this.tischgruppen = tischgruppen;
        }
        public void setTischgruppe(Tischgruppe tischgruppe, int index)
        {
            this.tischgruppen[index] = tischgruppe;
        }

        public List<Schueler> sortiereSchuelerListe(List<Schueler> schuelerListe)
        {
            Schueler speicher;
            //Sortieren nach Geschlecht
            for (int i = 0; i < schuelerListe.Count; i++)
            {
                for (int j = 0; j < schuelerListe.Count - 1; j++)
                {
                    if (schuelerListe[j].geschlecht > schuelerListe[j + 1].geschlecht)
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

        public SortedList<String, int> ermittleFirmen(List<Schueler> schuelerListe)
        {
            SortedList<String, int> zuordnung = new SortedList<string, int>();
            foreach (Schueler schueler in schuelerListe)
            {
                if (schueler != null)
                {
                    if (zuordnung.ContainsKey(schueler.firma))
                    {
                        zuordnung[schueler.firma] += 1;
                    }
                    else
                    {
                        zuordnung.Add(schueler.firma, 1);
                    }
                }

            }
            return zuordnung;
        }

        //tauschen mit Positionangaben --> wird benötigt Schüler auf bisher unbesetzte Plätze (null) zu verschieben
        public void tauschePlaetze(int tischA, int platzA, int tischB, int platzB) 
        {
            Schueler speicher = this.tischgruppen[tischA].getSitzplaetze()[platzA];
            this.tischgruppen[tischA].setzeSchueler(this.tischgruppen[tischB].getSitzplaetze()[platzB], platzA);
            this.tischgruppen[tischB].setzeSchueler(speicher, platzB);
        }

        //Tauschen hinweg mit Schülern --> Nukk Elemente können hiermit nicht angesprochen werden
        public void tauschePlaetze(Schueler schuelerA, Schueler schuelerB) 
        {
            int tischA =-1;
            int platzA =-1;
            int tischB =-1;
            int platzB =-1;
            foreach (Tischgruppe gruppe in this.tischgruppen) 
            {
                if (gruppe.getSitzplaetze().IndexOf(schuelerA) >= 0) 
                {
                    tischA = this.tischgruppen.IndexOf(gruppe);
                    platzA = gruppe.getSitzplaetze().IndexOf(schuelerA);
                }
                if (gruppe.getSitzplaetze().IndexOf(schuelerB) >= 0)
                {
                    tischB = this.tischgruppen.IndexOf(gruppe);
                    platzB = gruppe.getSitzplaetze().IndexOf(schuelerB);
                }
            }
            tauschePlaetze(tischA, platzA, tischB, platzB);
        }
        public override string ToString()
        {
            int i = 1;
            String ausgabe = "";
            foreach (Tischgruppe tischgruppe in tischgruppen)
            {
                ausgabe += "Tischgruppe " + i + ":\n";
                foreach (Schueler schueler in tischgruppen[i - 1].getSitzplaetze())
                {
                    if (schueler != null) 
                    {
                        ausgabe += schueler.ToString() + "\n";
                    }
                }
                i++;
            }
            return ausgabe;
        }
    }
}
