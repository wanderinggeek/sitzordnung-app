using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class Tischgruppe
    {
        List<Schueler> sitzplaetze;

        public Tischgruppe() 
        {
            sitzplaetze = new List<Schueler>();
            //Maximal dürfen 6 Schüler an einer Tischgruppe sitzen.
            //Plaetze werden
            for (int i = 0; i < 6; i++) 
            {
                this.sitzplaetze.Add(null);
            }
        }

        public Tischgruppe(List<Schueler> sitzplaetze)
        {
            this.sitzplaetze = sitzplaetze;
        }

        //public Tischgruppe(int plaetze)
        //{
        //    this.sitzplaetze = new List<Schueler>();
        //    for (int i = 0; i < plaetze; i++ )
        //    {
        //        this.sitzplaetze.Add(null);
        //    }
        //}

        public List<Schueler> getSitzplaetze() 
        {
            return this.sitzplaetze;
        }
        public void setSitzplaetze(List<Schueler> sitzplaetze) 
        {
            this.sitzplaetze = sitzplaetze;
        }
		
        public void setzeSchueler(Schueler schueler, int position) 
        {
            if (position < this.sitzplaetze.Count()) 
            {
                sitzplaetze[position] = schueler;
            }
        }
        public void removeSchueler(Schueler schueler) 
        {
            int index = this.sitzplaetze.IndexOf(schueler);
            this.sitzplaetze[index] = null;
        }

        public int getGruppengroesse()
        {
            int groesse = 0;
            foreach (Schueler schueler in this.sitzplaetze) 
            {
                if (schueler != null) 
                {
                    groesse++;
                }
            }
            return groesse;
        }
    }
}
