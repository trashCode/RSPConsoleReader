using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using rspDll;

namespace RspGuiReader
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            string rspPath = @"H:\FichiersB2\retours\69080041\";
            System.IO.DirectoryInfo dossier = new System.IO.DirectoryInfo(rspPath);
            IEnumerable<FileInfo> fichiers = dossier.EnumerateFiles("*.rs_", System.IO.SearchOption.TopDirectoryOnly);
            List<RspFile> retours = new List<RspFile>();
            
            foreach (FileInfo file in fichiers)
            {
                //Console.WriteLine("{0}:{1}",file.Name,(float)file.Length/1000);
                try
                {
                    retours.Add(new RspFile(file.FullName));
                }
                catch (Exception e)
                {
                   //rien !
                }
            }
            monTree.ItemsSource = retours;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (object item in monTree.Items)
            {
                TreeViewItem treeItem = monTree.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeItem != null)
                    ExpandAll(treeItem, true);
                treeItem.IsExpanded = true;
            }

        }

        private void ExpandAll(ItemsControl items, bool expand)
        {
            foreach (object obj in items.Items)
            {
                ItemsControl childControl = items.ItemContainerGenerator.ContainerFromItem(obj) as ItemsControl;
                if (childControl != null)
                {
                    ExpandAll(childControl, expand);
                }
                TreeViewItem item = childControl as TreeViewItem;
                if (item != null)
                    item.IsExpanded = true;
            }
        }

    }
}
