using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;

namespace ISUTest.Data.Utils
{
    public class SQLHelper //: ISQLHelper
    {
        
        public void ExecuteQueryCommand( string QueryCommand, IDbConnection Connection, out DataTable ResultSet )
        {
            using ( SqlConnection DbConnection = new SqlConnection( Connection.ConnectionString ) )
            {
                using ( SqlCommand SqlCommand = new SqlCommand( QueryCommand, DbConnection ) )
                {
                    DbConnection.Open();
                    SqlDataReader DataReader = SqlCommand.ExecuteReader( CommandBehavior.CloseConnection );
                    try
                    {
                        ResultSet = new DataTable();
                        ResultSet.Load( DataReader );
                    }
                    finally
                    {
                        DbConnection.Close();
                    }
                }
            }
        }

        public void ExecuteStoredProcedure( string StoredProcedureName,
            IDbConnection Connection, out DataTable ResultSet )
        {
            using ( SqlConnection DbConnection = new SqlConnection( Connection.ConnectionString ) )
            {
                using ( SqlCommand SqlCommand = new SqlCommand( StoredProcedureName, DbConnection ) )
                {
                    SqlCommand.CommandType = CommandType.StoredProcedure;
                    DbConnection.Open();
                    SqlDataReader DataReader = SqlCommand.ExecuteReader();
                    try
                    {
                        ResultSet = new DataTable();
                        ResultSet.Load( DataReader );
                    }
                    finally
                    {
                        DbConnection.Close();
                    }
                }
            }
        }

        /*public void InsertData( DataTable Data, IDictionary<string, string> dictConnectionSettings )
        {
            using ( var DbConnection = GetEFConnection() )
            {
                DbConnection.Open();
                using ( var BulkCopy = new SqlBulkCopy( DbConnection ) )
                {
                    BulkCopy.BatchSize = 10000;
                    BulkCopy.DestinationTableName = $"{dictConnectionSettings["schema"]}.[{dictConnectionSettings["tableName"]}]";
                    try
                    {
                        foreach ( DataColumn column in Data.Columns )
                        {
                            BulkCopy.ColumnMappings.Add( column.ColumnName, column.ColumnName );
                        }
                        BulkCopy.WriteToServer( Data );
                    }
                    catch ( Exception ex )
                    {
                        throw new InvalidOperationException( ex.Message );
                    }
                }
                DbConnection.Close();
            }
        }*/

        /*protected static SqlConnection GetEFConnection()
        {
            SqlConnection connection = new SqlConnection( ConfigurationManager.ConnectionStrings["DataContextConnection"].ConnectionString );
            connection.AccessToken = new AzureServiceTokenProvider().GetAccessTokenAsync( "https://database.windows.net/" ).Result;
            return connection;
        }*/

    }
}
