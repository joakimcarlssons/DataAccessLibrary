using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        void Dispose();
        string GetConnectionString(string connectionStringName);
        IEnumerable<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, U paramaters, string connectionStringName);
        IEnumerable<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        Task<IEnumerable<T>> LoadDataInTransactionAsync<T, U>(string storedProcedure, U parameters);
        void RollbackTransaction();
        void SaveData<U>(string storedProcedure, U parameters, string connectionStringName);
        Task SaveDataAsync<U>(string storedProcedure, U parameters, string connectionStringName);
        void SaveDataInTransaction<U>(string storedProcedure, U parameters);
        Task SaveDataInTransactionAsync<U>(string storedProcedure, U parameters);
        void StartTransaction(string connectionStringName);
    }
}