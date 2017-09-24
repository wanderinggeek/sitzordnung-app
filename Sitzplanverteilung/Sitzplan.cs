using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class Sitzplan
    {
        Tischgruppe[] tischgruppe;
        int maxProTisch;

        public Sitzplan()
        {
            this.tischgruppe = new Tischgruppe[5];
            this.maxProTisch = 5;
        }
        
        public Sitzplan(int anzahlGruppen, int max) 
        {
            this.tischgruppe = new Tischgruppe[anzahlGruppen];
            this.maxProTisch = max;
        }

        public void verteileSchueler(List<Schueler> schuelerListe) 
        {
            int moeglichePlaetze = tischgruppe.Length * maxProTisch;
            decimal schuelerProTischTatsaechlich = schuelerListe.Count()/tischgruppe.Length;
            if (moeglichePlaetze > schuelerListe.Count())
            {
                if (schuelerProTischTatsaechlich > 6) 
                {
                    //Sortieren der Schüler?
 
                    //Verteilung der Schüler
                    foreach (Schueler schueler in schuelerListe) 
                    {

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
            verteilerDummy(schuelerListe);
        }

        public void verteilerDummy(List<Schueler> schuelerListe) 
        {
            int k = 0;
            for (int i = 0; i < this.tischgruppe.Length; i++) 
            {
                this.tischgruppe[i] = new Tischgruppe();
                for (int j = 0; j <= this.maxProTisch && k < schuelerListe.Count(); j++) 
                {
                    tischgruppe[i].addSchueler(schuelerListe.ElementAt(k), j);
                    k++;
                }
            }
        }

        public Tischgruppe[] getTischgruppen()
        {
            return tischgruppe;
        }

        public Tischgruppe getTischgruppe(int index) 
        {
            return tischgruppe[index];
        }

        public void setTischgruppen(Tischgruppe[] tischgruppen) 
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
