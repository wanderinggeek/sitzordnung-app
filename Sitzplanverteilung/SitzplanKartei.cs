﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class SitzplanKartei
    {
        Sitzplan[] sitzplan;

        public SitzplanKartei()
        {
            this.sitzplan = new Sitzplan[6];
        }

        public SitzplanKartei(Sitzplan[] sitzplan)
        {
            this.sitzplan = sitzplan;
        }

        public void sitzplaeneGenerierenMitDatei()
        {
            //Schülerliste importieren
            List<Schueler> liste = Verwaltungskram.importiereSchuelerListe();

            //Erstellen der einzelnen Sitzpläne
            for (int i = 0; i < 6; i++)
            {
                sitzplan[i] = new Sitzplan();
                sitzplan[i].verteileSchueler(liste);
            }
        }

        public Sitzplan[] getSitzplaene() 
        {
            return this.sitzplan;
        }

        public void setSitzplaene(Sitzplan[] sitzplaene) 
        {
            this.sitzplan = sitzplaene;
        }
           
    }
}
