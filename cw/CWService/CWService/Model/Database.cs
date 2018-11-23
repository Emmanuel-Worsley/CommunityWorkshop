using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SQLite;
using System.IO;

namespace CWService.Model
{
    public class Database
    {
        public static SQLiteConnection GetConnection()
        {
            var databaseFileName = "community_workshop.db"; // name i want to call my db file
            var appDataPath = HttpContext.Current.Server.MapPath("~/App_Data"); // the exact location of app data
            var databasePath = Path.Combine(appDataPath, databaseFileName); // combines the file and directory making sure it is inputted correctly
            var connectionString = $"Data Source ={databasePath}";
            return new SQLiteConnection(connectionString);
        }

        public static void CreateDatabase()
        {
            using (var db = GetConnection())
            {
                var appDataPath = HttpContext.Current.Server.MapPath("~/App_Data"); // the exact location of app data
                var scriptFileName = "community_workshop_database.sql"; // name of the script with instructions to make the db file
                var scriptPath = Path.Combine(appDataPath, scriptFileName); // combine file and directory ensuring correct input
                var queries = File.ReadAllText(scriptPath); // reads everything inside the script
                db.Execute(queries); // executes the sql inside the script
            }
        }

    }
}