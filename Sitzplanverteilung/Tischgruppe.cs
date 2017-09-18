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

        public List<Schueler> getGruppe() 
        {
            return this.gruppe;
        }
        public void setGruppe(List<Schueler> gruppe) 
        {
            this.gruppe = gruppe;
        }

        public void addSchueler(Schueler schueler, int anzahl) 
        {
            if (this.gruppe.Count() < anzahl) 
            {
                gruppe.Add(schueler);
            }
        }
        public void removeSchueler(Schueler schueler) 
        {
            gruppe.Remove(schueler);
        }

        public int getGruppengroesse() 
        {
            return gruppe.Count();
        }
    }
}
