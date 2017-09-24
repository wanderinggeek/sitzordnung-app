using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class Tischgruppe
    {
        Schueler[] gruppe;

        public Tischgruppe() 
        {
            gruppe = new Schueler[6];
        }

        public Tischgruppe(Schueler[] gruppe)
        {
            this.gruppe = gruppe;
        }
        public Tischgruppe(int plaetze)
        {
            this.gruppe = new Schueler[plaetze];
        }

        public Schueler[] getGruppe() 
        {
            return this.gruppe;
        }
        public void setGruppe(Schueler[] gruppe) 
        {
            this.gruppe = gruppe;
        }

        public void addSchueler(Schueler schueler, int position) 
        {
            gruppe[position] = schueler;
        }
        public void removeSchueler(Schueler schueler) 
        {
            int i = 0;
            foreach (Schueler s in gruppe) 
            {
                if (s.Equals(schueler)) 
                {
                    gruppe[i] = null;
                    break;
                }
                i++;
            }
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
