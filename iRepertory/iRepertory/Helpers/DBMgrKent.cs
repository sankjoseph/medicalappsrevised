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

namespace iRepertory
{
    public class DBMgrKent :BaseViewModel
    {
        public ObservableCollection<RepertoryItem> RepItems { get; set; }
        private static DBMgrKent instance;
        public List<RepertoryItem> FilteredRepItems;
        public List<RepertoryItem> FilteredSectionRepItems;
        public List<string> Sections;

        private SqliteConnection conn = null;

        public static DBMgrKent Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBMgrKent();
                }
                return instance;
            }
        }
        private DBMgrKent()
        {
            RepItems = new ObservableCollection<RepertoryItem>();
            Sections = new List<string>();
        }

        public void FullLoad(string filename, string type)
        {
            OpenDatbase(filename, type);
            LoadReps();
            FilteredRepItems = RepItems.ToList();
            FilteredSectionRepItems = RepItems.ToList();
            LoadSections();
        }
        public void OpenDatbase(string filename, string type)
        {

            var existingDb = NSBundle.MainBundle.PathForResource(filename, type);
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                         filename + "." + type);

            if (!File.Exists(dbPath))
                File.Copy(existingDb, dbPath);

            // Create connection to the database
            conn = new SqliteConnection("Data Source=" + dbPath);
            conn.Open();

        }
        public void LoadReps()
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
                command.CommandText = "SELECT Id,Section,SubSection,filename from [tblKent] ORDER by Id";
                try
                {
                    //await DataStore.GetItemsAsync(true);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RepertoryItem item = new RepertoryItem();
                            // Pull values back into class
                            item.Id = Convert.ToString(reader[0]);
                            item.Section = Convert.ToString(reader[1]);
                            item.SubSection = Convert.ToString(reader[2]);
                            item.FileName = Convert.ToString(reader[3]);

                            RepItems.Add(item);
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
        }////


        public void LoadSections()
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
                command.CommandText = "SELECT Distinct Section from [tblKent] ORDER by Id";
                try
                {
                    //await DataStore.GetItemsAsync(true);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RepertoryItem item = new RepertoryItem();
                            // Pull values back into class
                           string section = Convert.ToString(reader[0]);

                            Sections.Add(section);
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
