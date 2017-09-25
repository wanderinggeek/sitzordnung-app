using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class Tischgruppe
    {
        List<Schueler> gruppe;

        public Tischgruppe() 
        {
            gruppe = new List<Schueler>();
        }

        public Tischgruppe(List<Schueler> gruppe)
        {
            this.gruppe = gruppe;
        }
        public Tischgruppe(int plaetze)
        {
            this.gruppe = new List<Schueler>();
            for (int i = 0; i < plaetze; i++ )
            {
                this.gruppe.Add(null);
            }
        }

        public List<Schueler> getGruppe() 
        {
            return this.gruppe;
        }
        public void setGruppe(List<Schueler> gruppe) 
        {
            this.gruppe = gruppe;
        }

        public void addSchueler(Schueler schueler, int position) 
        {
            if (position < gruppe.Count)
            {
                gruppe[position] = schueler;
            }
        }
        public void removeSchueler(Schueler schueler) 
        {
            int index = this.gruppe.IndexOf(schueler);
            this.gruppe[index] = null;
        }

        public void removeSchueler(int position)
        {
            gruppe[position] = null;
        }

        public void swapSchueler(int positionVon, int positionBis) 
        {
            Schueler speicher = gruppe[positionVon];
            gruppe[positionVon] = gruppe[positionBis];
            gruppe[positionBis] = speicher;
        }

        public void swapSchueler(Schueler von, Schueler bis) 
        {
            int i = 0;
            bool istGetauscht = false;
            foreach (Schueler s in gruppe)
            {
                if (s.Equals(von))
                {
                    gruppe[i] = bis;
                    istGetauscht = true;
                }
                if (s.Equals(bis) && !istGetauscht)
                {
                    gruppe[i] = von;
                }
                istGetauscht = false;
                i++;
            }
        }

        public int getGruppengroesse() 
        {
            return gruppe.Count();
        }
    }
}
