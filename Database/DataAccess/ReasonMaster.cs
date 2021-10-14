using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerkshireForm.Models;
using System.Data.SQLite;
using Dapper;
using BerkshireForm.SpecialExceptions;
using System.Data;
using Dapper.Contrib.Extensions;

namespace BerkshireForm.Database.DataAccess
{
    /*Data access for Reason table will be handled through ReasonMaster*/
    /*ReasonMaster implements the singleton pattern, so it is a sealed class*/
    public sealed class ReasonMaster
    {
        /*Singleton has a static variable that holds the single instance
            "Eager" static initialization is thread-safe in .NET, so no locking would be necessary*/
        private static readonly ReasonMaster instance = new ReasonMaster();

        /*Define connection string as a private property, as it's used by every method but should not be accessible elsewhere*/
        private string connString = "DataSource = BHHC_DB.db;Version=3;New=False;Compress=True";

        /*Singleton has a private constructor*/
        private ReasonMaster()
        { }

        /*Singleton requires a public, static access point*/
        public static ReasonMaster GetReasonMaster()
        {
            return instance;
        }

        /*Upon loading, create the table if it doesn't exist*/
        public void TableSetup()
        {
            try
            {
                string sql = "CREATE TABLE IF NOT EXISTS [Reason] ( [Id] integer primary key not null, [ReasonText] nvarchar(2083))";

                using (IDbConnection connection = new SQLiteConnection(connString))
                {
                    connection.Execute(sql);
                }
            }
            catch (Exception ex)
            {
                throw new ReasonDataException("Database setup failed", "Table creation", "Reason", ex);
            }
            
        }

        public List<Reason> Get()
        {
            try
            {

                using (IDbConnection connection = new SQLiteConnection(connString))
                {
                    return connection.GetAll<Reason>().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ReasonDataException("Failed to load saved reasons", "Select all", "Reason", ex);
            }
        }

        public Reason Get(int reasonId)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(connString))
                {
                    return connection.Get<Reason>(reasonId);
                }
            }
            catch (Exception ex)
            {
                throw new ReasonDataException("Failed to retrieve reason", "Select by Id", $"Reason with Id: {reasonId}", ex);
            }
        }

        /*Return "success" indicator to let the form know that the UI can be updated*/
        public bool Update(int reasonId, string newReasonText)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(connString))
                {
                    return connection.Update(new Reason { Id = reasonId, ReasonText = newReasonText });

                }
            }
            catch (Exception ex)
            {
                throw new ReasonDataException("Failed to update reason to: ", $"Update by Id to Reason table for Id = {reasonId}", newReasonText, ex);
            }
            
        }

        /*Return "success" indicator to let the form know that the item can be removed from the bindingList*/
        public bool Delete(int reasonId)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(connString))
                {

                    return connection.Delete(new Reason { Id = reasonId });

                }
            }
            catch (Exception ex)
            {
                throw new ReasonDataException("Failed to delete reason", "Delete by Id", $"Reason with Id: {reasonId}", ex);
            }
            

        }
        public int Add(string reasonText)
        {
            try
            {
                int newId;
                string sql = "insert into Reason (ReasonText) values (@ReasonText); select last_insert_rowid()";

                /*Not using dapper.contrib "Insert" because the selection call for the new id must occur in the same call as the insert operation*/
                using (IDbConnection connection = new SQLiteConnection(connString))
                {
                    newId = connection.QuerySingle<int>(sql, new { ReasonText = reasonText });
                
                }

                return newId;
            }
            catch (Exception ex)
            {
                throw new ReasonDataException("Unable to add reason", "Insert and select last inserted Id", reasonText, ex);
            }
        }
    }
}
