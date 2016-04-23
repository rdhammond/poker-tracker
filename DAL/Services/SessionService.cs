using System;
using System.Threading.Tasks;
using PokerTracker.DAL.DAO;
using AsyncPoco;
using PokerTracker.Common;
using System.Collections.Generic;

namespace PokerTracker.DAL.Services
{
    public interface ISessionService : IDisposable
    {
        void StartSession(Session session);
        Task StartSessionAsync(Session session);

        Guid InsertTimeEntry(TimeEntry timeEntry);
        Task<Guid> InsertTimeEntryAsync(TimeEntry timeEntry);

        void FinalizeSession(DateTime endTime, Decimal hoursActive, string optionalNotes);
        Task FinalizeSessionAsync(DateTime endTime, Decimal hoursActive, string optionalNotes);
    }

    public class SessionService : ISessionService
    {
        private bool _disposed;

        private string _connectionString;
        private Database _db;
        private ITransaction _transaction;
        private Guid? _sessionId;

        internal SessionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        ~SessionService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            DisconnectFromDb();

            _disposed = true;
        }

        private void CheckSessionInactive()
        {
            if (_sessionId.HasValue)
                throw new InvalidOperationException("Another session is already active.");
        }

        public void CheckSessionActive()
        {
            if (!_sessionId.HasValue)
                throw new InvalidOperationException("No active sessions.");
        }

        private async Task ConnectToDb()
        {
            _db = new Database(_connectionString);

            try
            {
                _transaction = await _db.GetTransactionAsync();
            }
            catch
            {
                _db.Dispose();
                throw;
            }
        }

        private void DisconnectFromDb(bool commit = false)
        {
            if (_transaction != null)
            {
                if (commit) _transaction.Complete();
                _transaction.Dispose();
                _transaction = null;
            }

            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public void StartSession(Session session)
        {
            AsyncHelper.RunSync(() => StartSessionAsync(session));
        }

        public async Task StartSessionAsync(Session session)
        {
            CheckSessionInactive();
            await ConnectToDb();

            try
            {
                await _db.InsertAsync(session);
                _sessionId = session.Id;
            }
            catch
            {
                DisconnectFromDb();
                throw;
            }
        }

        public Guid InsertTimeEntry(TimeEntry timeEntry)
        {
            return AsyncHelper.RunSync(() => InsertTimeEntryAsync(timeEntry));
        }

        public async Task<Guid> InsertTimeEntryAsync(TimeEntry timeEntry)
        {
            CheckSessionActive();

            await _db.InsertAsync(timeEntry);
            return timeEntry.Id;
        }

        public void FinalizeSession(DateTime endTime, Decimal hoursActive, string optionalNotes)
        {
            AsyncHelper.RunSync(() => FinalizeSessionAsync(endTime, hoursActive, optionalNotes));
        }

        public async Task FinalizeSessionAsync(DateTime endTime, Decimal hoursActive, string optionalNotes)
        {
            CheckSessionActive();

            await _db.ExecuteAsync(
                "EXEC usp_FinalizeSession @sessionId, @endTime, @hoursActive, @optionalNotes",
                new { sessionId = _sessionId, endTime, hoursActive, optionalNotes }
            );

            DisconnectFromDb(true);
            _sessionId = null;
        }
    }
}