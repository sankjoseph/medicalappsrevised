using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using Foundation;
using System.Diagnostics;

using System.Collections.ObjectModel;
using System.Linq;

using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace iMateriaMedica
{
    public sealed class DBMgr : BaseViewModel
    {
        public ObservableCollection<Remedy> RemedyItems { get; set; }

        public List<Remedy> FilteredRemedies;

        public  Dictionary<string, List<Remedy>> genIndexedTableItems;
        public string[] genKeys;

        private static DBMgr instance;

        private SqliteConnection conn = null;

        public static DBMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBMgr();
                }
                return instance;
            }
        }
        public Command LoadItemsCommand { get; set; }
        private DBMgr()
        {
            RemedyItems = new ObservableCollection<Remedy>();
        }
        public void FullLoad(string filename, string type)
        {
            OpenDatbase(filename,type);
            LoadRemedies();
            FilteredRemedies = RemedyItems.ToList();

            // key based index list
            genIndexedTableItems = new Dictionary<string, List<Remedy>>();
            foreach (var t in DBMgr.Instance.RemedyItems)
            {
                if (genIndexedTableItems.ContainsKey(t.Name[0].ToString()))
                {
                    genIndexedTableItems[t.Name[0].ToString()].Add(t);
                }
                else
                {
                    genIndexedTableItems.Add(t.Name[0].ToString(), new List<Remedy>() { t });
                }
            }
            genKeys = genIndexedTableItems.Keys.ToArray();

        }
        public void OpenDatbase(string filename, string type)
        {

            var existingDb = NSBundle.MainBundle.PathForResource(filename, type);
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
                                         filename+ "."+ type);

                if (!File.Exists(dbPath))
                    File.Copy(existingDb, dbPath);
  
            // Create connection to the database
            conn = new SqliteConnection("Data Source=" + dbPath);
            conn.Open();
           
        }
        private string ReplaceText(string str)
        {
            // replace any " with ' for html read 
          return Regex.Replace(str, "\"", "'", RegexOptions.IgnoreCase);
 
        }

        public void LoadRemedies()
        {
        if (IsBusy)
            return;

            IsBusy = true;

            bool shouldClose = false;

            // Is the database already open?
            if (conn.State != ConnectionState.Open)
            {
                shouldClose = true;
                conn.Open();
            }

            // Execute query
            using (var command = conn.CreateCommand())
            {
                // Create new command
                command.CommandText = "SELECT Id,Name,Subheading,Generaldesc,Mind," +
                    "Head,Face,Eyes,Ears,Nose,Mouth,Heart,Chest,Stomach,Abdomen,Rectum," +
                    "Urinary,Respiratory,Extremities,Skin,Male,Female,Fever,Back,Sleep," +
                    "Modalities,Rgeneral,Rantidote,Rinimical,Rcompliment,Rcompare," +
                    "Rcompatible,Rincompatible,Dose,Throat,Uses,Stool,Tissues,Nerves," +
                    "Bones,Tongue,Circulatory,Blood,Spine,Bowels,Teeth,Breast," +
                    "Kidney,Gastro,Spleen,Neck,Urine,PhysiologicDosage,AlimentaryCanal," +
                    "Liver,Sexual FROM [tblRemedies]";
                try
                {
                    //await DataStore.GetItemsAsync(true);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Remedy item = new Remedy();
                            // Pull values back into class
                            item.Id = Convert.ToString(reader[0]);
                            item.Name = Convert.ToString(reader[1]);
                            item.SubHeading = Convert.ToString(reader[2]);

                            item.Desciption = ReplaceText( Convert.ToString(reader[3]));
                            item.Mind = ReplaceText(Convert.ToString(reader[4]));
                            item.Head = ReplaceText(Convert.ToString(reader[5]));
                            item.Face = ReplaceText(Convert.ToString(reader[6]));
                            item.Eyes = ReplaceText(Convert.ToString(reader[7]));
                            item.Ears = ReplaceText(Convert.ToString(reader[8]));
                            item.Nose = ReplaceText(Convert.ToString(reader[9]));

                            item.Mouth = ReplaceText(Convert.ToString(reader[10]));
                            item.Heart = ReplaceText(Convert.ToString(reader[11]));
                            item.Chest = ReplaceText(Convert.ToString(reader[12]));
                            item.Stomach = ReplaceText(Convert.ToString(reader[13]));
                            item.Abdomen = ReplaceText(Convert.ToString(reader[14]));
                            item.Rectum = ReplaceText(Convert.ToString(reader[15]));
                            item.Urinary = ReplaceText(Convert.ToString(reader[16]));
                            item.Respiratory = ReplaceText(Convert.ToString(reader[17]));
                            item.Extremities = ReplaceText(Convert.ToString(reader[18]));
                            item.Skin = ReplaceText(Convert.ToString(reader[19]));
                            item.Male = ReplaceText(Convert.ToString(reader[20]));
                            item.Female = ReplaceText(Convert.ToString(reader[21]));
                            item.Fever = ReplaceText(Convert.ToString(reader[22]));
                            item.Back = ReplaceText(Convert.ToString(reader[23]));
                            item.Sleep = ReplaceText(Convert.ToString(reader[24]));
                            item.Modalities = ReplaceText(Convert.ToString(reader[25]));

                            item.Rgeneral = ReplaceText(Convert.ToString(reader[26]));
                            item.Rantidote = ReplaceText(Convert.ToString(reader[27]));

                            item.Rinimical = ReplaceText(Convert.ToString(reader[28]));
                            item.Rcompliment = ReplaceText(Convert.ToString(reader[29]));
                            item.Rcompare = ReplaceText(Convert.ToString(reader[30]));
                            item.Rcompatible = ReplaceText(Convert.ToString(reader[31]));
                            item.Rincompatible = ReplaceText(Convert.ToString(reader[32]));

                            item.Dose = ReplaceText(Convert.ToString(reader[33]));
                            item.Throat = ReplaceText(Convert.ToString(reader[34]));
                            item.Uses = ReplaceText(Convert.ToString(reader[35]));
                            item.Stool = ReplaceText(Convert.ToString(reader[36]));
                            item.Tissues = ReplaceText(Convert.ToString(reader[37]));
                            item.Nerves = ReplaceText(Convert.ToString(reader[38]));
                            item.Bones = ReplaceText(Convert.ToString(reader[39]));
                            item.Tongue = ReplaceText(Convert.ToString(reader[40]));
                            item.Circulatory = ReplaceText(Convert.ToString(reader[41]));
                            item.Blood = ReplaceText(Convert.ToString(reader[42]));
                            item.Spine = ReplaceText(Convert.ToString(reader[43]));
                            item.Bowels = ReplaceText(Convert.ToString(reader[44]));
                            item.Teeth = ReplaceText(Convert.ToString(reader[45]));
                            item.Breast = ReplaceText(Convert.ToString(reader[46]));
                            item.Kidney = ReplaceText(Convert.ToString(reader[47]));
                            item.Gastro = ReplaceText(Convert.ToString(reader[48]));
                            item.Spleen = ReplaceText(Convert.ToString(reader[49]));
                            item.Neck = ReplaceText(Convert.ToString(reader[50]));
                            item.Urine = ReplaceText(Convert.ToString(reader[51]));
                            item.PhysiologicDosage = ReplaceText(Convert.ToString(reader[52]));
                            item.AlimentaryCanal = ReplaceText(Convert.ToString(reader[53]));
                            item.Liver = ReplaceText(Convert.ToString(reader[54]));
                            item.Sexual = ReplaceText(Convert.ToString(reader[55]));

                            RemedyItems.Add(item);
                        }
                    }                    
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            // Should we close the connection to the database
            if (shouldClose)
            {
                conn.Close();
            }
        }
            
    }
}
