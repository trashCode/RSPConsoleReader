using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using rspDll;


namespace RSPConsoleReader
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string rspPath = @"H:\FichiersB2\retours\69080041\";
            
            //Deux methodes pour enumerer les fichiers :
            //Methode 1 : GetFile Renvois seulement les noms des fichiers (en string)
            IEnumerable<string> fileList = System.IO.Directory.GetFiles(rspPath,"*.rsp");
            foreach(string file in fileList){
                Console.WriteLine(file);
            }
            Console.WriteLine("Nous avons trouvé {0} fichiers rsp", fileList.Count());

            //Methode 2 : DirectoryInfo.EnumerateFiles renvois des FileInfo (plus proche de ce que je connais en pshell)
            System.IO.DirectoryInfo dossier = new System.IO.DirectoryInfo(rspPath);
            IEnumerable<FileInfo> fichiers = dossier.EnumerateFiles("*.rs_",System.IO.SearchOption.TopDirectoryOnly);
            List<RspFile> retours = new List<RspFile>();
            
            //On creer un RspFile pour chaque fichier (Le parsing a lieu ici)
            foreach (FileInfo file in fichiers)
            {
                //Console.WriteLine("{0}:{1}",file.Name,(float)file.Length/1000);
                try
                {
                    retours.Add(new RspFile(file.FullName));
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} CANNOT BE PARSED", file.FullName);
                    Console.ResetColor();
                }
            }
            Console.WriteLine("Nous avons parsé {0} fichiers rs_", retours.Count());
            
            //on affiche pour tout les retours la ref trouvée. (pour verifier que le parsing est ok)
            foreach (RspFile retour in retours){
                //Console.WriteLine(retour.reference);
            }

            //Pour le premier : affichage de l'arbre des entités
            
            RspFile retour0 = retours.First();//First est une methode etendues, definie par linq.
            Entite premiere = retour0.entites[0];
            premiere.display();
            
            /*
            foreach (string line in retour0.rawEntite){
                int pad = 0;
                Int32.TryParse(line.Substring(3, 2), out pad);
                if (pad == 99) { pad = 12; } else { pad *= 2; }
                Console.WriteLine("{0}",line.Substring(0,3).PadLeft(pad));
            }*/
            

            /*Console.WriteLine("retour0 : {0} entites, {1} lines ", retour0.entites.Count , retour0.rawEntite.Count() );
            foreach (Entite e in retour0.entites)
            {
                Console.WriteLine("{0}: {1}", e.type, e.data.PadLeft(e.data.Length+e.level%10));
            }*/

            /************************************
             * Operations de tri
             ************************************/
            
            /*
             
            //on affiche les 10 premiers retours:
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0} , {1}" , retours[i].filePath, retours[i].rawEntite.Count());
            }

            //trions les fichiers par nombre d'entite : 
            retours.Sort(delegate(RspFile x, RspFile y)
            {
                return y.rawEntite.Count().CompareTo(x.rawEntite.Count());//tri descendant.
            });
            Console.WriteLine("===apres tri (nb Entitse):");

            //on affiche les 10 premiers retours:
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0} , {1}", retours[i].filePath, retours[i].rawEntite.Count());
            }


            //trions les fichiers par date
            retours.Sort(delegate(RspFile x, RspFile y)
            {
                FileInfo xf = new FileInfo(x.filePath);
                FileInfo yf = new FileInfo(y.filePath);
                return yf.LastWriteTime.CompareTo(xf.LastWriteTime);
            });
            Console.WriteLine("===apres tri (nb Entitse):");

            //on affiche les 10 premiers retours:
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0} , {1}", retours[i].filePath, (new FileInfo(retours[i].filePath)).LastWriteTime );
            }
             
            
            */

            /************************************
             * Operations de filtrage
             ************************************/

            /*

            //Les retours avec plus de 100 entites
            Predicate<RspFile> test = delegate(RspFile f) { return f.rawEntite.Count() > 450; };
            List<RspFile> grosRetours = retours.FindAll(test);

            grosRetours.ForEach(delegate(RspFile f){Console.WriteLine(f.rawEntite.Count());} );

            Console.WriteLine("avec lambda");
            //Meme chose, avec une lambda expression
            foreach (RspFile f in retours.Where(r => r.rawEntite.Count() > 450))
            {
                Console.WriteLine("{0} {1}", f.filePath, f.rawEntite.Count());
            }

            Console.WriteLine("sans lambda");
            foreach (RspFile f in retours.FindAll(delegate(RspFile r) { return r.rawEntite.Count() > 450; }))
            {
                Console.WriteLine("{0} {1}", f.filePath, f.rawEntite.Count());
            }
            */

        }

        void displayEntite(Entite e)
        {
            Console.WriteLine("{0} {1}", e.level, e.type);
            foreach (Entite e2 in e.subs)
            {
                displayEntite(e2);
            }
        }
    }
}
