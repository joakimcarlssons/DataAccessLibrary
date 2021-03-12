using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    /// <summary>
    /// Query a database using these generic functions.
    /// </summary>
    /// <remarks>
    /// Built on the idea that you have a connection string in you configuration file (like appsettings.json) 
    /// </remarks>
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {
        #region Private Members

        private readonly IConfiguration _config;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        #endregion

        #region Private Properties

        /// <summary>
        /// Flag indicating if a transaction is closed correctly
        /// </summary>
        private bool TransactionIsClosed { get; set; }

        #endregion

        #region Constructor

        public SqlDataAccess(IConfiguration config) => (_config) = (config);

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the connection string from the config file
        /// </summary>
        /// <param name="connectionStringName">The name of the connection string</param>
        /// <returns></returns>
        public string GetConnectionString(string connectionStringName) => _config.GetConnectionString(connectionStringName);

        /// <summary>
        /// Used when a callback is expected from the query
        /// </summary>
        /// <typeparam name="T">The type to be returned</typeparam>
        /// <typeparam name="U">The type of the parameters</typeparam>
        /// <param name="storedProcedure">The name of the stored procedure to call</param>
        /// <param name="paramaters">The parameters needed for the stored procedure</param>
        /// <param name="connectionStringName">The name of the connection string to get from the config file</param>
        /// <returns>An <see cref="IEnumerable{T}"/></returns>
        /// <remarks>
        /// Can also be used when you for example wants to save a record and expect a callback from the query
        /// The parameters can be both a model or a dynamic type created on the fly
        /// Example of use: _sql.LoadData<Model, dynamic>("storedProcedure", new { Id = modelId }, "Default");
        /// </remarks>
        public IEnumerable<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            using (var connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                return connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Used when a callback is expected from the query async
        /// </summary>
        /// <typeparam name="T">The type to be returned</typeparam>
        /// <typeparam name="U">The type of the parameters</typeparam>
        /// <param name="storedProcedure">The name of the stored procedure to call</param>
        /// <param name="paramaters">The parameters needed for the stored procedure</param>
        /// <param name="connectionStringName">The name of the connection string to get from the config file</param>
        /// <returns>An <see cref="IEnumerable{T}"/></returns>
        /// <remarks>
        /// Can also be used when you for example wants to save a record and expect a callback from the query
        /// The parameters can be both a model or a dynamic type created on the fly
        /// Example of use: _sql.LoadData<Model, dynamic>("storedProcedure", new { }, "Default");
        /// </remarks>
        public async Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, U paramaters, string connectionStringName)
        {
            using (var connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                return await connection.QueryAsync<T>(storedProcedure, paramaters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Saves data from the database without any expected return type
        /// </summary>
        /// <typeparam name="U">The type of the parameters</typeparam>
        /// <param name="storedProcedure">The name of the stored procedure to call</param>
        /// <param name="paramaters">The parameters needed for the stored procedure</param>
        /// <param name="connectionStringName">The name of the connection string to get from the config file</param>
        /// <remarks>
        /// The parameters can be both a model or a dynamic type created on the fly
        /// Example of use: _sql.SaveData<Model>("storedProcedure", new { Id = modelId }, "Default");
        /// </remarks>
        public void SaveData<U>(string storedProcedure, U parameters, string connectionStringName)
        {
            using (var connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Saves data from the database without any expected return type
        /// </summary>
        /// <typeparam name="U">The type of the parameters</typeparam>
        /// <param name="storedProcedure">The name of the stored procedure to call</param>
        /// <param name="paramaters">The parameters needed for the stored procedure</param>
        /// <param name="connectionStringName">The name of the connection string to get from the config file</param>
        /// <remarks>
        /// The parameters can be both a model or a dynamic type created on the fly
        /// Example of use: _sql.SaveData<Model>("storedProcedure", model, "Default");
        /// </remarks>
        public async Task SaveDataAsync<U>(string storedProcedure, U parameters, string connectionStringName)
        {
            using (var connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        #region Transactions

        /// <summary>
        /// Start the transaction
        /// </summary>
        /// <param name="connectionStringName">The name of connection string to use</param>
        public void StartTransaction(string connectionStringName)
        {
            // Open the connection
            _connection = new SqlConnection(GetConnectionString(connectionStringName));
            _connection.Open();

            // Begin transaction
            _transaction = _connection.BeginTransaction();

            // Make sure to mark the transction flag as it being open
            TransactionIsClosed = false;
        }

        /// <summary>
        /// Used when a callback is expected from the query
        /// </summary>
        /// <typeparam name="T">The type of the callback</typeparam>
        /// <typeparam name="U">The type of the parameters</typeparam>
        /// <param name="storedProcedure">The name of the stored procedure to call</param>
        /// <param name="parameters">The parameters needed for the stored procedure</param>
        /// <returns>The callback from the query</returns>
        public IEnumerable<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            return _connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        /// <summary>
        /// Used when a callback is expected from the query async
        /// </summary>
        /// <typeparam name="T">The type of the callback</typeparam>
        /// <typeparam name="U">The type of the parameters</typeparam>
        /// <param name="storedProcedure">The name of the stored procedure to call</param>
        /// <param name="parameters">The parameters needed for the stored procedure</param>
        /// <returns>The callback from the query</returns>
        public async Task<IEnumerable<T>> LoadDataInTransactionAsync<T, U>(string storedProcedure, U parameters)
        {
            return await _connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        /// <summary>
        /// Used when no callback is expected
        /// </summary>
        /// <typeparam name="U">The type of the parameters</typeparam>
        /// <param name="storedProcedure">The name of the stored procedure to call</param>
        /// <param name="parameters">The parameters needed for the stored procedure</param>
        public void SaveDataInTransaction<U>(string storedProcedure, U parameters)
        {
            _connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        /// <summary>
        /// Used when no callback is expected
        /// </summary>
        /// <typeparam name="U">The type of the parameters</typeparam>
        /// <param name="storedProcedure">The name of the stored procedure to call</param>
        /// <param name="parameters">The parameters needed for the stored procedure</param>
        public async Task SaveDataInTransactionAsync<U>(string storedProcedure, U parameters)
        {
            await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        /// <summary>
        /// Commit the ongoing transaction
        /// </summary>
        public void CommitTransaction()
        {
            // Commit the transaction
            _transaction?.Commit();

            // Close the open connection
            _connection?.Close();

            // Flag the transaction as closed
            TransactionIsClosed = true;
        }

        /// <summary>
        /// Rollback an ongoing transaction
        /// </summary>
        public void RollbackTransaction()
        {
            // Rollback the transaction
            _transaction?.Rollback();

            // Close the connection
            _connection.Close();

            // Flag the transaction as closed
            TransactionIsClosed = true;
        }

        public void Dispose()
        {
            // Check if the transaction is still open
            if (!TransactionIsClosed)
            {
                // If it's still open, try to commit the ongoing transaction and close the connection
                try
                {
                    CommitTransaction();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            _transaction = null;
            _connection = null;
        }


        #endregion


        #endregion
    }
}
