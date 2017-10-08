using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;

namespace Sitzplanverteilung
{
    [DataContract]
    public class Schueler
    {
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public String vorname { get; set; }
        [DataMember]
        public String klasse { get; set; }
        [DataMember]
        public String firma { get; set; }
        [DataMember]
        public String firmenkuerzel { get; set; }
        [DataMember]
        public char geschlecht { get; set; }
        [DataMember]
        public String berufsgruppe { get; set; }
        [DataMember]
        public String bild { get; set; }

        public Schueler()
        {
            this.name = "Musterschüler";
            this.vorname = "Manni";
            this.firma = "Musterschüler KG";
            this.firmenkuerzel = "MKG";
            this.klasse = "Musterklasse";
            this.berufsgruppe = "Sohn vom Eigentümer";
            this.geschlecht = 'm';
            this.bild = null;
        }

        public Schueler(String name, String vorname, String klasse, String firma, String kuerzel, char geschlecht, String berufsgruppe)
        {
            this.name = name;
            this.vorname = vorname;
            this.firma = firma;
            this.firmenkuerzel = kuerzel;
            this.klasse = klasse;
            this.berufsgruppe = berufsgruppe;
            // geschlecht mit dem Character <m>ännlich oder <w>eiblich
            this.geschlecht = geschlecht;
            this.bild = this.name + "_" + this.vorname + ".jpg";
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
