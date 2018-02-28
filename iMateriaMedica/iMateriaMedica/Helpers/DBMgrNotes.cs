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
    public class DBMgrNotes : BaseViewModel
    {
        public ObservableCollection<MMNote> NoteItems { get; set; }
        private static DBMgrNotes instance;

        private SqliteConnection conn = null;

        public static DBMgrNotes Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBMgrNotes();
                }
                return instance;
            }
        }
        public DBMgrNotes()

        {
            NoteItems = new ObservableCollection<MMNote>();
        }
        public void FullLoad(string filename, string type)
        {
            OpenDatbase(filename, type);
            LoadNotes();
        }

        public void DeleteNote(string Noteid)
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
                command.CommandText = "Delete from [tblNotes] where Id = " + Noteid;
                try
                {
                    int affected = command.ExecuteNonQuery();
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

        public string IfFoundNoteName(MMNote theNote)
        {
            string retId = "";
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
                //INSERT INTO klant(klant_id,naam,voornaam) VALUES(@param1,@param2,@param3)
                command.CommandText = "SELECT Id from [tblNotes] where UPPER(Name) = '" + theNote.Name.ToUpper() + "'";
                try
                {
                    //await DataStore.GetItemsAsync(true);
                    using (var reader = command.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            retId = Convert.ToString(reader[0]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                   
                }
            }
            // Should we close the connection to the database
            if (shouldClose)
            {
                conn.Close();
            }
            return retId;
        }
        public void UpdateNote(MMNote theNote)         {             if (IsBusy)                 return;              IsBusy = true;              bool shouldClose = false;              // Is the database already open?             if (conn.State != ConnectionState.Open)             {                 shouldClose = true;                 conn.Open();             }              // Execute query             using (var command = conn.CreateCommand())             {                 // Create new command                 //INSERT INTO klant(klant_id,naam,voornaam) VALUES(@param1,@param2,@param3)                 command.CommandText = "Update tblNotes SET NAME = '" + theNote.Name + "'" +
                    " ,Observations = '" + theNote.Observations + "'" + " WHERE Id = '" + theNote.Id + "'";                 try                 {                     int affected = command.ExecuteNonQuery();
                    if (affected <=0)
                    {
                        
                    }                 }                  catch (Exception ex)                 {                     Debug.WriteLine(ex);                 }                 finally                 {                     IsBusy = false;                 }             }             // Should we close the connection to the database             if (shouldClose)             {                 conn.Close();             }         } 
        public void InsertNote(MMNote theNote)
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
                //INSERT INTO klant(klant_id,naam,voornaam) VALUES(@param1,@param2,@param3)
                command.CommandText = "INSERT into tblNotes(Name,Observations) VALUES('" + theNote.Name + "','" + theNote.Observations+ "')";
                try
                {
                    int affected = command.ExecuteNonQuery();
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
        /// <summary>
        /// Gets the max identifier.
        /// </summary>
        /// <returns>The max identifier.</returns>
        public string GetMaxId()
        {
            string retMax = "";
            if (IsBusy)
                return retMax;

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
                command.CommandText = "SELECT Count(*) from [tblNotes]";
                try
                {
                    //await DataStore.GetItemsAsync(true);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retMax =  Convert.ToString(reader[0]);
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
            return retMax;
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
        public void ReloadNotes()
        {
            NoteItems.Clear();
            LoadNotes();
        }
        public void LoadNotes()         {         if (IsBusy)             return;              IsBusy = true;              bool shouldClose = false;              // Is the database already open?             if (conn.State != ConnectionState.Open)             {                 shouldClose = true;                 conn.Open();             }              // Execute query             using (var command = conn.CreateCommand())             {
                // Create new command
                command.CommandText = "SELECT Id,Name,Observations from [tblNotes] ORDER by Id DESC";                 try                 {                     //await DataStore.GetItemsAsync(true);                     using (var reader = command.ExecuteReader())                     {                         while (reader.Read())                         {                             MMNote item = new MMNote();                             // Pull values back into class                             item.Id = Convert.ToString(reader[0]);                             item.Name = Convert.ToString(reader[1]);                             item.Observations = Convert.ToString(reader[2]);                               NoteItems.Add(item);                         }                     }                                     }                  catch (Exception ex)                 {                     Debug.WriteLine(ex);                 }                 finally                 {                     IsBusy = false;                 }             }             // Should we close the connection to the database             if (shouldClose)             {                 conn.Close();             }         } 
    }
}
