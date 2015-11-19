using System;
using System.Collections.ObjectModel;

namespace rsp

{
        
    public class Entite
    {
        public int type {get; set;}
        public int level {get; set;}
        public string data {get; set;}
        public ObservableCollection<Entite> subs { get; set; }
        //public readableData dictionnary //contient les données parsées

        public Entite(string data){
            this.data = data;
            this.subs = new ObservableCollection<Entite>();
        }
    }


    public class RspFile {
        private string filePath { get; set; }
        public int reference {get;set;}
        public ObservableCollection<Entite> entites { get; set; }

        public RspFile() {
            this.entites = new ObservablesCollection<Entite>();
        }

        public void parse() { }//charge le contenu du fichier, et remplit la collection entites.
    }

    public class RspList
    {
        private ObservableCollection<RspFile> membres;
        private ObservableCollection<Rspfilter> filters;
    }

    public class RspFilter
    {
        public bool active { get; set; }
        public int entiteType { get; set; }
        public int color;//pour differencier les filtres sur une même liste.
    }
}