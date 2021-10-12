﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerkshireForm.Models;
using System.Data.SQLite;
using Dapper;
using BerkshireForm.SpecialExceptions;

namespace BerkshireForm.Database.DataAccess
{
    class ReasonMaster
    {
        private string connString = "DataSource = BHHC_DB.db;Version=3;New=False;Compress=True";

        //private string connString = "DataSource = C:\\Users\\lpchi\\source\\repos\\BerkshireForm\\Database\\BHHC_DB.db;Version=3;New=False;Compress=True";
        //private string connString = $"DataSource = {AppDomain.CurrentDomain.BaseDirectory}BHHC_DB.db;Version=3;New=False;Compress=True";

        public void TableSetup()
        {
            string sql = "CREATE TABLE IF NOT EXISTS [Reason] ( [Id] integer primary key not null, [ReasonText] nvarchar(2083))";
            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Execute(sql);
            }
        }

        public List<Reason> Get()
        {
            try
            {
                string sql = "select * from Reason";

                using (SQLiteConnection connection = new SQLiteConnection(connString))
                {
                    //var trans = connection.BeginTransaction();
                    return connection.Query<Reason>(sql).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new ReasonDataException("", "Query", "", ex);
            }
        }

        public Reason Get(int reasonId)
        {
            try
            {
                string sql = "select * from Reason where Id = @Id";
                using (SQLiteConnection connection = new SQLiteConnection(connString))
                {
                    //var trans = connection.BeginTransaction();
                    return connection.QuerySingleOrDefault<Reason>(sql, new { Id = reasonId });

                }
            }
            catch (Exception ex)
            {
                throw new ReasonDataException("", "Query", "", ex);
            }
        }

        public int Update(int reasonId, string newReasonText)
        {
            int affectedRows = 0;
            string sql = "Update Reason SET ReasonText = @ReasonText where Id = @Id";

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                affectedRows = connection.Execute(sql, new { Id = reasonId, ReasonText = newReasonText });

            }

            return affectedRows;
        }

        public int Delete(int reasonId)
        {
            int affectedRows = 0;
            string sql = "DELETE from Reason WHERE Id = @Id";

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                affectedRows = connection.Execute(sql, new { Id = reasonId });

            }

            return affectedRows;
        }

        //public void Add1(string reasonText)
        //{
        //    using (SQLiteConnection connection = new SQLiteConnection(connString))
        //    {
        //        connection.Execute("insert into Reason (ReasonText) values (@ReasonText)", new { ReasonText = reasonText });
        //    }
        //}

        public int Add(string reasonText)
        {
            try
            {
                int newId;
                string sql = "insert into Reason (ReasonText) values (@ReasonText); select last_insert_rowid()";

                using (SQLiteConnection connection = new SQLiteConnection(connString))
                {
                    //var trans = connection.BeginTransaction();
                    newId = connection.QuerySingle<int>(sql, new { ReasonText = reasonText });
                
                }

                return newId;
            }
            catch (Exception ex)
            {
                throw new ReasonDataException("", "Add", reasonText, ex);
            }
            //using (SQLiteConnection connection = new SQLiteConnection(connString))
            //{
            //    var trans = connection.BeginTransaction();

            //    try
            //    {
            //        connection.Execute("insert into Reason (ReasonText) values (@ReasonText); select last_insert_rowid()", new { ReasonText = reasonText }, trans);
            //    }
            //    catch
            //    {

            //    }

            //}
        }
    }
}