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
                    "Rcompatible,Rincompatible FROM [tblRemedies]";
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
