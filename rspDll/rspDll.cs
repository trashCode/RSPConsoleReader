using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace rspDll
{
    public class Entite
    {
        public string type { get; set; }
        public int level { get; set; }
        public string data { get; set; }
        public ObservableCollection<Entite> subs { get; set; }

        //public readableData dictionnary //contient les données parsées

        public Entite(string data)
        {
            this.data = data;
            this.subs = new ObservableCollection<Entite>();
            this.type = data.Substring(0, 3);
            
            if (this.type == "000" || this.type == "999") {
                this.level = 0;
            } else {
                this.level = int.Parse(this.data.Substring(3, 2));
            }
        }

        public Entite(string[] rawData, int index = 0)
        {
            this.type = rawData[index].Substring(0, 3);

            if (this.type == "000" || this.type == "999")
            {
                this.level = 1;
            }
            else
            {
                this.level = int.Parse(rawData[index].Substring(3, 2));
            }
            
            this.subs = new ObservableCollection<Entite>();
            int nextLevel = 0;

            //tant que :
            //pas la fin 
            //pas la derniere ligne 
            //pas fini ce level 
            do
            {
                index++;
                Int32.TryParse(rawData[index].Substring(3, 2), out nextLevel);

                if (nextLevel == this.level + 1)
                {
                    this.subs.Add(new Entite(rawData, index));

                }
            }
            while (index < rawData.Length && rawData[index].Substring(0, 3) != "999" && int.Parse(rawData[index].Substring(3, 2)) != this.level);


        }

        public void display()
        {
            Console.WriteLine("{0} {1}", this.level, this.type);
            foreach (Entite e in this.subs)
            {
                e.display();
            }
        }

        //dummy static function
        public static string wololo()
        {
            return "wololo";
        }
    }


    public class RspFile
    {
        public string filePath { get; set; }
        public string[] rawEntite { get; set; }
        public string reference { get; set; }
        public ObservableCollection<Entite> entites { get; set; }

        public RspFile()
        {
            this.entites = new ObservableCollection<Entite>();
        }

        public RspFile(string path)
        {
            this.filePath = path;
            this.rawEntite = (File.ReadAllText(path)).Split('@');
            this.reference = rawEntite[0].Substring(61, 3);
            this.entites = new ObservableCollection<Entite>();
            
            //methode recursive
            this.entites.Add(new Entite(this.rawEntite));
            //Methode lineaire
            //this.parse();
        }

        public void parse() { //Version 1 : l'objet RspFile contient la liste de toutes les entités.
            foreach (string line in this.rawEntite)
            {
                this.entites.Add(new Entite(line));
            }
        }
    }

    public class RspFilter
    {
        public bool active { get; set; }
        public int entiteType { get; set; }
        public int color;//pour differencier les filtres sur une même liste.

        public RspFilter()
        {
            this.active = true;
        }
    }

    public class RspList
    {
        private ObservableCollection<RspFile> membres;
        //private ObservableCollection<RspFilter> filters;
    }


}
