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

namespace iOrganon
{
    public sealed class DBMgr :BaseViewModel
    {
        public ObservableCollection<ChapterRead> ChapterItems { get; set; }
        public ObservableCollection<IntroRead> IntroItems { get; set; }
        public ObservableCollection<SummaryRead> SummaryItems { get; set; }

        public List<IntroRead> FilteredIntros;

        public List<ChapterRead> FilteredChapters;

        public List<SummaryRead> FilteredSummaries;

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
            ChapterItems = new ObservableCollection<ChapterRead>();
            IntroItems = new ObservableCollection<IntroRead>();
            SummaryItems = new ObservableCollection<SummaryRead>();
        }
        public void FullLoad(string filename)
        {
            OpenDatbase(filename);
            LoadChapters();
            LoadIntros();
            LoadSummary();
            FilteredIntros = IntroItems.ToList();
            FilteredSummaries = SummaryItems.ToList();
            FilteredChapters = ChapterItems.ToList();
        }
        public void OpenDatbase(string filename)
        {

            var existingDb = NSBundle.MainBundle.PathForResource("Organon", "db3");
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);

                if (!File.Exists(dbPath))
                    File.Copy(existingDb, dbPath);
  
            // Create connection to the database
            conn = new SqliteConnection("Data Source=" + dbPath);
            conn.Open();
           
        }

        public void LoadChapters()
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
                command.CommandText = "SELECT Id,Chapter,Reading FROM [tblChapters]";
                try
                {
                    //await DataStore.GetItemsAsync(true);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChapterRead item = new ChapterRead();
                            // Pull values back into class
                            item.Id = Convert.ToString(reader[0]);
                            item.Chapter = Convert.ToString(reader[1]);
                            item.Reading = Convert.ToString(reader[2]);

                            ChapterItems.Add(item);
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
        /////load Intros
        public void LoadIntros()
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
                command.CommandText = "SELECT Id,Para FROM [tblIntroduction]";
                try
                {
                    //await DataStore.GetItemsAsync(true);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IntroRead item = new IntroRead();
                            // Pull values back into class
                            item.Id = Convert.ToString(reader[0]);
                            item.Para = Convert.ToString(reader[1]);
                     
                            IntroItems.Add(item);
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
        /// end intros
        /////load summary
        public void LoadSummary()
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
                command.CommandText = "SELECT Id,Chapters,Summary FROM [tblSummaries]";
                try
                {
                    //await DataStore.GetItemsAsync(true);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SummaryRead item = new SummaryRead();
                            // Pull values back into class
                            item.Id = Convert.ToString(reader[0]);
                            item.Chapters = Convert.ToString(reader[1]);
                            item.Summary = Convert.ToString(reader[2]);
                            SummaryItems.Add(item);
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
        /// end summary
    
    }
}
