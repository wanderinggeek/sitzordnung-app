using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitzplanverteilung
{
    class SitzplanKartei
    {
        List<Sitzplan> sitzplan;

        public SitzplanKartei()
        {
            this.sitzplan = new List<Sitzplan>();
            for (int i = 0; i < 6; i++)
            {
                this.sitzplan.Add(null);
            }
        }

        public SitzplanKartei(List<Sitzplan> sitzplan)
        {
            this.sitzplan = sitzplan;
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
                sitzplan[i] = new Sitzplan();
                liste = new List<Schueler>();
                liste.AddRange(eingabeListe);
                sitzplan[i].verteileSchueler(liste);
                strafPunkteMin = sitzplan[i].berechneStrafpunkte();
                for (int j = 0; j < 1000; j++)
                {
                    liste = new List<Schueler>();
                    liste.AddRange(eingabeListe);
                    preSitzplan.verteileSchueler(liste);
                    strafPunkte = preSitzplan.berechneStrafpunkte();
                    if (strafPunkteMin > strafPunkte) 
                    {
                        strafPunkteMin = strafPunkte;
                        sitzplan[i] = preSitzplan;
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
                sitzplan[i] = new Sitzplan(anzahlGruppen, maxSchueler);
                sitzplan[i].verteileSchueler(liste);
            }
        }

        public List<Sitzplan> getSitzplaene() 
        {
            return this.sitzplan;
        }

        public void setSitzplaene(List<Sitzplan> sitzplaene) 
        {
            this.sitzplan = sitzplaene;
        }
           
    }
}
