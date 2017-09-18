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
            verteilerDummy(schuelerListe);
        }

        public void verteilerDummy(List<Schueler> schuelerListe) 
        {
            int k = 0;
            for (int i = 0; i < this.tischgruppe.Length; i++) 
            {
                this.tischgruppe[i] = new Tischgruppe();
                for (int j = 0; j < this.maxProTisch && k < schuelerListe.Count(); j++) 
                {
                    tischgruppe[i].addSchueler(schuelerListe.ElementAt(k), maxProTisch);
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
