using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.BLL
{
    public interface ISessionManager
    {
        void AddSession(
            Session session,
            IEnumerable<TimeEntry> timeEntries,
            DateTime endTime,
            Decimal hoursActive,
            string optionalNotes = null
        );

        Task AddSessionAsync(
            Session session,
            IEnumerable<TimeEntry> timeEntries,
            DateTime endTime,
            Decimal hoursActive,
            string optionalNotes = null
        );
    }

    public class SessionManager : ISessionManager
    {
        private readonly ISessionServiceFactory _sessionSvcFact;

        public SessionManager(ISessionServiceFactory sessionSvcFact)
        {
            _sessionSvcFact = sessionSvcFact;
        }

        public void AddSession(
            Session session,
            IEnumerable<TimeEntry> timeEntries,
            DateTime endTime,
            Decimal hoursActive,
            string optionalNotes = null
        )
        {
            AsyncHelper.RunSync(() => AddSessionAsync(session, timeEntries, endTime, hoursActive, optionalNotes));
        }

        public async Task AddSessionAsync(
            Session session,
            IEnumerable<TimeEntry> timeEntries, 
            DateTime endTime,
            Decimal hoursActive,
            string optionalNotes = null
        )
        {
            using (var sessionSvc = _sessionSvcFact.Create())
            {
                await sessionSvc.StartSessionAsync(session);

                foreach (var timeEntry in timeEntries)
                {
                    await sessionSvc.InsertTimeEntryAsync(timeEntry);
                }

                await sessionSvc.FinalizeSessionAsync(endTime, hoursActive, optionalNotes);
            }
        }
    }
}
