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
            this.maxProTisch = 5;
            for (int i = 0; i < 5; i++) 
            {
                hinzufuegenTischgruppe(new Tischgruppe());
            }
        }
        
        public Sitzplan(int anzahlGruppen, int max) 
        {
            this.tischgruppen = new List<Tischgruppe>();
            this.maxProTisch = max;
            for (int i = 0; i < anzahlGruppen; i++) 
            {
                hinzufuegenTischgruppe(new Tischgruppe());
            }
        }

        public void verteileSchueler(List<Schueler> schuelerListe) 
        {
            verteilerDummy(schuelerListe);
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

        public override string ToString()
        {
            int i = 1;
            String ausgabe="";
            foreach (Tischgruppe tischgruppe in tischgruppen) 
            {
                ausgabe += "Tischgruppe " + i + ":\n";
                foreach (Schueler schueler in tischgruppen[i - 1].getGruppe()) 
                {
                    ausgabe += schueler.ToString()+"\n";
                }
                i++;
            }
            return ausgabe;
        }

        public void hinzufuegenTischgruppe(Tischgruppe tischgruppe) 
        {
            this.tischgruppen.Add(tischgruppe);
        }

    }
}
