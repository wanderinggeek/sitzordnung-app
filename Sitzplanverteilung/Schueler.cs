using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class Schueler
    {
        public String name { get; set; }
        public String vorname { get; set; }
        public String klasse { get; set; }
        public String firma { get; set; }
        public char geschlecht { get; set; }
        public String berufsgruppe { get; set; }
        public String bild { get; set; }

        public Schueler()
        {
            this.name = "Musterschüler";
            this.vorname = "Manni";
            this.firma = "Musterschüler KG";
            this.klasse = "Musterklasse";
            this.berufsgruppe = "Sohn vom Eigentümer";
            this.geschlecht = 'm';
            this.bild = "Bild";
        }

        public Schueler(String name, String vorname, String klasse, String firma, char geschlecht, String berufsgruppe)
        {
            this.name = name;
            this.vorname = vorname;
            this.firma = firma;
            this.klasse = klasse;
            this.berufsgruppe = berufsgruppe;
            // geschlecht mit dem Character <m>ännlich oder <w>eiblich
            this.geschlecht = geschlecht;
            this.bild = "Bild";
        }


        public String getVollerName()
        {
            return this.name + " " + this.vorname;
        }

        public override string ToString()
        {
            return this.name + ", " + this.vorname + ", " + this.firma + ", " + this.berufsgruppe + ", " + this.geschlecht + ", " + this.klasse + ", " + this.bild;
        }


    }
}
