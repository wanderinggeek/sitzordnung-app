using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class Schueler
    {
        String name;
        String vorname;
        String klasse;
        String firma;
        char geschlecht;
        String berufssgruppe;
        //Bild?

        public Schueler() 
        {
            this.name = "Musterschüler";
            this.vorname = "Manni";
            this.firma = "Musterschüler KG";
            this.berufssgruppe = "Sohn vom Eigentümer";
            this.geschlecht = 'm';
        }

        public Schueler(String name, String vorname, String firma, String berufssgruppe, char geschlecht) 
        {
            this.name = name;
            this.vorname = vorname;
            this.firma = firma;
            this.berufssgruppe = berufssgruppe;
            // geschlecht mit dem Character <m>ännlich oder <w>eiblich
            this.geschlecht = geschlecht;
        }

        public String getName() 
        {
            return this.name;
        }
        public String getVorname()
        {
            return this.vorname;
        }
        public String getFirma()
        {
            return this.firma;
        }
        public String getBerufsgruppe()
        {
            return this.berufssgruppe;
        }
        public char getGeschlecht()
        {
            return this.geschlecht;
        }
        public String getKlasse()
        {
            return this.klasse;
        }
        public void setName(String name)
        {
            this.name = name;
        }
        public void setVorname(String vorname)
        {
            this.vorname = vorname;
        }
        public void setFirma(String firma)
        {
            this.firma = firma;
        }
        public void setBerufsgruppe(String berufsgruppe)
        {
            this.berufssgruppe = berufsgruppe;
        }
        public void setGeschlecht(char geschlecht)
        {
            if (geschlecht.Equals(Char.ToUpper('m')) || geschlecht.Equals(Char.ToUpper('w'))) 
            {
                this.geschlecht = geschlecht;
            }
        }
        public void setKlasse(String klasse)
        {
            this.klasse = klasse;
        }

        public override string ToString()
        {
            return this.name + ", " + this.vorname + ", " + this.firma + ", " + this.berufssgruppe + ", " + this.geschlecht;
        }
        
    }
}
