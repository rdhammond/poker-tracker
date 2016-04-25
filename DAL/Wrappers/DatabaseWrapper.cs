using AsyncPoco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Wrappers
{
    public interface IDatabaseWrapper : IDisposable
    {
        int CommandTimeout { get; set; }
        IDbConnection Connection { get; }
        bool EnableAutoSelect { get; set; }
        bool EnableNamedParams { get; set; }
        bool KeepConnectionAlive { get; set; }
        object[] LastArgs { get; }
        string LastCommand { get; }
        string LastSQL { get; }
        int OneTimeCommandTimeout { get; set; }

        void AbortTransaction();
        Task BeginTransactionAsync();
        void CloseSharedConnection();
        void CompleteTransaction();
        DbCommand CreateCommand(DbConnection connection, string sql, params object[] args);
        Task<int> DeleteAsync(object poco);
        Task<int> DeleteAsync(string tableName, string primaryKeyName, object poco);
        Task<int> DeleteAsync(string tableName, string primaryKeyName, object poco, object primaryKeyValue);
        Task<int> DeleteAsync<T>(object pocoOrPrimaryKey);
        Task<int> DeleteAsync<T>(Sql sql);
        Task<int> DeleteAsync<T>(string sql, params object[] args);
        Task<int> ExecuteAsync(Sql sql);
        Task<int> ExecuteAsync(string sql, params object[] args);
        Task<T> ExecuteScalarAsync<T>(Sql sql);
        Task<T> ExecuteScalarAsync<T>(string sql, params object[] args);
        Task<bool> ExistsAsync<T>(object primaryKey);
        Task<bool> ExistsAsync<T>(string sqlCondition, params object[] args);
        Task<List<T>> FetchAsync<T>(Sql sql);
        Task<List<T>> FetchAsync<T>(string sql, params object[] args);
        Task<List<T>> FetchAsync<T>(long page, long itemsPerPage, Sql sql);
        Task<List<TRet>> FetchAsync<TRet>(Type[] types, object cb, string sql, params object[] args);
        Task<List<T>> FetchAsync<T>(long page, long itemsPerPage, string sql, params object[] args);
        Task<List<T1>> FetchAsync<T1, T2>(Sql sql);
        Task<List<T1>> FetchAsync<T1, T2>(string sql, params object[] args);
        Task<List<T1>> FetchAsync<T1, T2, T3>(Sql sql);
        Task<List<TRet>> FetchAsync<T1, T2, TRet>(Func<T1, T2, TRet> cb, Sql sql);
        Task<List<T1>> FetchAsync<T1, T2, T3>(string sql, params object[] args);
        Task<List<TRet>> FetchAsync<T1, T2, TRet>(Func<T1, T2, TRet> cb, string sql, params object[] args);
        Task<List<T1>> FetchAsync<T1, T2, T3, T4>(Sql sql);
        Task<List<T1>> FetchAsync<T1, T2, T3, T4>(string sql, params object[] args);
        Task<List<TRet>> FetchAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, Sql sql);
        Task<List<TRet>> FetchAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, string sql, params object[] args);
        Task<List<TRet>> FetchAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, Sql sql);
        Task<List<TRet>> FetchAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, string sql, params object[] args);
        Task<T> FirstAsync<T>(Sql sql);
        Task<T> FirstAsync<T>(string sql, params object[] args);
        Task<T> FirstOrDefaultAsync<T>(Sql sql);
        Task<T> FirstOrDefaultAsync<T>(string sql, params object[] args);
        string FormatCommand(IDbCommand cmd);
        string FormatCommand(string sql, object[] args);
        Task<ITransaction> GetTransactionAsync();
        Task<object> InsertAsync(object poco);
        Task<object> InsertAsync(string tableName, string primaryKeyName, object poco);
        Task<object> InsertAsync(string tableName, string primaryKeyName, bool autoIncrement, object poco);
        bool IsNew(object poco);
        bool IsNew(string primaryKeyName, object poco);
        void OnBeginTransaction();
        void OnConnectionClosing(IDbConnection conn);
        DbConnection OnConnectionOpened(DbConnection conn);
        void OnEndTransaction();
        bool OnException(Exception x);
        void OnExecutedCommand(IDbCommand cmd);
        void OnExecutingCommand(IDbCommand cmd);
        Task OpenSharedConnectionAsync();
        Task<Page<T>> PageAsync<T>(long page, long itemsPerPage, Sql sql);
        Task<Page<T>> PageAsync<T>(long page, long itemsPerPage, string sql, params object[] args);
        Task<Page<T>> PageAsync<T>(long page, long itemsPerPage, Sql sqlCount, Sql sqlPage);
        Task<Page<T>> PageAsync<T>(long page, long itemsPerPage, string sqlCount, object[] countArgs, string sqlPage, object[] pageArgs);
        Task QueryAsync<T>(string sql, Action<T> action);
        Task QueryAsync<T>(Sql sql, Action<T> action);
        Task QueryAsync<T>(Sql sql, Func<T, bool> func);
        Task QueryAsync<T>(string sql, Func<T, bool> func);
        Task QueryAsync<T>(string sql, object[] args, Func<T, bool> func);
        Task QueryAsync<T>(string sql, object[] args, Action<T> action);
        Task QueryAsync<TRet>(Type[] types, object cb, string sql, object[] args, Action<TRet> action);
        Task QueryAsync<T1, T2>(Sql sql, Action<T1> action);
        Task QueryAsync<T1, T2>(string sql, object[] args, Action<T1> action);
        Task QueryAsync<T1, T2, T3>(Sql sql, Action<T1> action);
        Task QueryAsync<T1, T2, TRet>(Func<T1, T2, TRet> cb, Sql sql, Action<TRet> action);
        Task QueryAsync<T1, T2, T3>(string sql, object[] args, Action<T1> action);
        Task QueryAsync<T1, T2, TRet>(Func<T1, T2, TRet> cb, string sql, object[] args, Action<TRet> action);
        Task QueryAsync<T1, T2, T3, T4>(Sql sql, Action<T1> action);
        Task QueryAsync<T1, T2, T3, T4>(string sql, object[] args, Action<T1> action);
        Task QueryAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, Sql sql, Action<TRet> action);
        Task QueryAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, string sql, object[] args, Action<TRet> action);
        Task QueryAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, Sql sql, Action<TRet> action);
        Task QueryAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, string sql, object[] args, Action<TRet> action);
        Task SaveAsync(object poco);
        Task SaveAsync(string tableName, string primaryKeyName, object poco);
        Task<T> SingleAsync<T>(Sql sql);
        Task<T> SingleAsync<T>(object primaryKey);
        Task<T> SingleAsync<T>(string sql, params object[] args);
        Task<T> SingleOrDefaultAsync<T>(Sql sql);
        Task<T> SingleOrDefaultAsync<T>(object primaryKey);
        Task<T> SingleOrDefaultAsync<T>(string sql, params object[] args);
        Task<List<T>> SkipTakeAsync<T>(long skip, long take, Sql sql);
        Task<List<T>> SkipTakeAsync<T>(long skip, long take, string sql, params object[] args);
        Task<int> UpdateAsync(object poco);
        Task<int> UpdateAsync(object poco, object primaryKeyValue);
        Task<int> UpdateAsync(object poco, IEnumerable<string> columns);
        Task<int> UpdateAsync(object poco, object primaryKeyValue, IEnumerable<string> columns);
        Task<int> UpdateAsync(string tableName, string primaryKeyName, object poco);
        Task<int> UpdateAsync(string tableName, string primaryKeyName, object poco, IEnumerable<string> columns);
        Task<int> UpdateAsync(string tableName, string primaryKeyName, object poco, object primaryKeyValue);
        Task<int> UpdateAsync(string tableName, string primaryKeyName, object poco, object primaryKeyValue, IEnumerable<string> columns);
        Task<int> UpdateAsync<T>(Sql sql);
        Task<int> UpdateAsync<T>(string sql, params object[] args);
    }

    public class DatabaseWrapper : Database, IDatabaseWrapper
    {
        public DatabaseWrapper(DbConnection connection)
            : base(connection)
        { }

        public DatabaseWrapper(string connectionStringName)
            : base(connectionStringName)
        { }

        public DatabaseWrapper(string connectionString, DbProviderFactory provider)
            : base(connectionString, provider)
        { }

        public DatabaseWrapper(string connectionString, string providerName)
            : base(connectionString, providerName)
    }
}
