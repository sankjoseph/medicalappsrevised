using Foundation;
using System;
using UIKit;
using System.IO;
using System.Linq;
//using System.Collections.Generic;

namespace iRepertory.iOS
{
    public partial class FileSearchViewController : UITableViewController
    {
 
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            DirectoryInfo d = new DirectoryInfo(NSBundle.MainBundle.BundlePath);//Assuming Test is your Folder
            var allFilesInfo = d.GetFiles("*.html"); //Getting Text files

        /*string strsearch = "anxiety";

            var Patterns = allFilesInfo.Where(x => strsearch.Length != 0)
        .Select(x => new { x.FullName, txt = File.ReadAllText(x.FullName) })
        .Where(a => a.txt.IndexOf(strsearch, StringComparison.OrdinalIgnoreCase) >= 0)
        .Select(a => a.FullName);

           var files = Patterns.ToList();*/



           /* string strsearch = "anxiety";
            string strMainpath = NSBundle.MainBundle.BundlePath;

            var Patterns = DBMgrKent.Instance.RepItems.Where(x => strsearch.Length != 0)
                                    .Select(x => new { x.FileName, txt = File.ReadAllText(strMainpath + "/" + x.FileName) })
        .Where(a => a.txt.IndexOf(strsearch, StringComparison.OrdinalIgnoreCase) >= 0)
                                    .Select(a => a.FileName);

            ///////var files = Patterns.ToList();

            var files = Patterns.ToList();

            IEnumerable<RepertoryItem> query =
              (from item1 in DBMgrKent.Instance.RepItems
                where files.Any(k => item1.FileName.ToUpper().Contains(k.ToUpper()))

               select item1);

            DBMgrKent.Instance.FilteredAdvSearchItems = query.ToList();*/

            int x = 10;

          /*  int i = 0;
            foreach (FileInfo file in Files)
            {
                i++;
                bool bFound = false;

                DirectoryInfo dir = file.Directory;

                string text = File.ReadAllText(dir.FullName + "/" + file.Name);
            }*/
        }
        public FileSearchViewController (IntPtr handle) : base (handle)
        {
            
        }
    }
}