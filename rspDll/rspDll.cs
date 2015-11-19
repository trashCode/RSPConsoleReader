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
            this.parse();
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
