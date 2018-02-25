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

namespace iMateriaMedica
{
    public sealed class DBMgr :BaseViewModel
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
        public void FullLoad(string filename)
        {
            OpenDatbase(filename);
            LoadRemedies();
            FilteredRemedies = RemedyItems.ToList();
         }
        public void OpenDatbase(string filename)
        {

            var existingDb = NSBundle.MainBundle.PathForResource("medicaLite", "db3");
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);

                if (!File.Exists(dbPath))
                    File.Copy(existingDb, dbPath);
  
            // Create connection to the database
            conn = new SqliteConnection("Data Source=" + dbPath);
            conn.Open();
           
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
                command.CommandText = "SELECT Id,Name,Subheading,Generaldesc FROM [tblRemediesLite]";
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
                            item.Desciption = Convert.ToString(reader[3]);

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
