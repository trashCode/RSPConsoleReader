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
            
            //on affiche pour tout les retours la ref trouvée. (pour verifier que le parsing est ok)
            foreach (RspFile retour in retours){
                //Console.WriteLine(retour.reference);
            }

            //Pour le premier 
            RspFile retour0 = retours.First();//First est une methode etendues, definie par linq.
            /*
            foreach (string line in retour0.rawEntite){
                int pad = 0;
                Int32.TryParse(line.Substring(3, 2), out pad);
                if (pad == 99) { pad = 12; } else { pad *= 2; }
                Console.WriteLine("{0}",line.Substring(0,3).PadLeft(pad));
            }
            */
            /*Console.WriteLine("retour0 : {0} entites, {1} lines ", retour0.entites.Count , retour0.rawEntite.Count() );
            foreach (Entite e in retour0.entites)
            {
                Console.WriteLine("{0}: {1}", e.type, e.data.PadLeft(e.data.Length+e.level%10));
            }*/

            Console.WriteLine(rspDll.Entite.wololo());
        }
    }
}
