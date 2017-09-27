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
        String bild;

        public Schueler() 
        {
            this.name = "Musterschüler";
            this.vorname = "Manni";
            this.firma = "Musterschüler KG";
            this.berufssgruppe = "Sohn vom Eigentümer";
            this.geschlecht = 'm';
            this.bild = erstelleBilddateiName();
        }

        public Schueler(String name, String vorname, String firma, String berufssgruppe, char geschlecht) 
        {
            this.name = name;
            this.vorname = vorname;
            this.firma = firma;
            this.berufssgruppe = berufssgruppe;
            // geschlecht mit dem Character <m>ännlich oder <w>eiblich
            this.geschlecht = geschlecht;
            this.bild = erstelleBilddateiName();
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
        public void setName(String name)
        {
            this.name = name;
            this.bild = erstelleBilddateiName();
        }
        public void setVorname(String vorname)
        {
            this.vorname = vorname;
            this.bild = erstelleBilddateiName();
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

        public override string ToString()
        {
            return this.name + ", " + this.vorname + ", " + this.firma + ", " + this.berufssgruppe + ", " + this.geschlecht;
        }
        private String erstelleBilddateiName()
        {
            return this.name +"_"+ this.vorname.Replace(' ', '_') + ".jpg"; ;
        }

        
    }
}
