using System;
using System.Data;

namespace ISUTest.Interfaces
{
    public interface ISQLHelper
    {
        /// <summary>
        /// Execute a sql query command into the database
        /// </summary>
        /// <param name="QueryCommand">SQL query command to be executed</param>
        /// <param name="ConnectionString">Database connection</param>
        /// <param name="ResultSet">SQL query result, if any</param>
        void ExecuteQueryCommand( string QueryCommand, IDbConnection Connection
            , out DataTable ResultSet );

        /// <summary>
        ///  Gets data from a SQL database using a given stored procedure
        /// </summary>
        /// <param name="StoredProcedureName">SQL stored procedure to be executed</param>
        /// <param name="ConnectionString">Database connection</param>
        /// <param name="ResultSet">SQL query result, if any</param>
        void ExecuteStoredProcedure( string StoredProcedureName, IDbConnection Connection
            , out DataTable ResultSet );

        /// <summary>
        /// Inserts data into a database using BulkCopy object
        /// </summary>
        /// <param name="Data">Dataset to insert in the database</param>
        //void InsertData( DataTable Data/*, IDbConnection Connection*/, IDictionary<string, string> dictConnectionSettings );
    }
}
