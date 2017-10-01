using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class SitzplanKartei
    {
        List<Sitzplan> sitzplaene;

        public SitzplanKartei()
        {
            this.sitzplaene = new List<Sitzplan>();
            for (int i = 0; i < 6; i++ )
            {
                this.sitzplaene.Add(null);
            }
        }

        public SitzplanKartei(List<Sitzplan> sitzplaene)
        {
            this.sitzplaene = sitzplaene;
        }

        public void sitzplaeneGenerierenMitDatei()
        {
            //Schülerliste importieren
            List<Schueler> eingabeListe = new List<Schueler>();
            eingabeListe.AddRange(Verwaltungskram.importiereSchuelerListe());
            List<Schueler> liste;
            
            //Erstellen der einzelnen Sitzpläne
            int strafPunkteMin;
            int strafPunkte;
            for (int i = 0; i < 6; i++)
            {
                Sitzplan preSitzplan = new Sitzplan();
                sitzplaene[i] = new Sitzplan();
                liste = new List<Schueler>();
                liste.AddRange(eingabeListe);
                sitzplaene[i].verteileSchueler(liste);
                strafPunkteMin = sitzplaene[i].berechneStrafpunkte();
                for (int j = 0; j < 1000; j++)
                {
                    liste = new List<Schueler>();
                    liste.AddRange(eingabeListe);
                    preSitzplan.verteileSchueler(liste);
                    strafPunkte = preSitzplan.berechneStrafpunkte();
                    if (strafPunkteMin > strafPunkte) 
                    {
                        strafPunkteMin = strafPunkte;
                        sitzplaene[i] = preSitzplan;
                    }
                    Console.WriteLine(strafPunkte);
                }
            }
            Console.WriteLine("sitzplan fertiggestellt");
        }

        //Sitzplan verteilen mit variablen Gruppengrößen/Anzahl Tischgruppen
        public void sitzplaeneGenerierenMitDatei(int anzahlGruppen, int maxSchueler)
        {
            //Schülerliste importieren
            List<Schueler> liste = Verwaltungskram.importiereSchuelerListe();

            //Erstellen der 6 einzelnen Sitzpläne
            for (int i = 0; i < 6; i++)
            {
                sitzplaene[i] = new Sitzplan(anzahlGruppen, maxSchueler);
                sitzplaene[i].verteileSchueler(liste);
            }
            Console.WriteLine("fertig");
        }

        public List<Sitzplan> getSitzplaene() 
        {
            return this.sitzplaene;
        }

        public void setSitzplaene(List<Sitzplan> sitzplaene) 
        {
            this.sitzplaene = sitzplaene;
        }

        public Sitzplan getSitzplan(int index)
        {
            return this.sitzplaene[index];
        }


        public void setSitzplaene(Sitzplan sitzplan, int index)
        {
            this.sitzplaene[index] = sitzplan;
        }
           
    }
}
