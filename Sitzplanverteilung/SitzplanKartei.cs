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
            List<Schueler> liste = Verwaltungskram.importiereSchuelerListe();

            //Erstellen der einzelnen Sitzpläne
            for (int i = 0; i < 6; i++)
            {
                sitzplaene[i] = new Sitzplan();
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
