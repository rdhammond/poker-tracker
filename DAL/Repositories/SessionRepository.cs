using AsyncPoco;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Factories;
using System;
using System.Threading.Tasks;

namespace PokerTracker.DAL.Repositories
{
    public interface ISessionRepository : IReadOnlyRepository<SessionDao>
    {
        Task StartSessionAsync(SessionDao session, Database database);

        Task FinalizeSessionAsync(SessionDao session, DateTime endTime, decimal hoursActive, Database database);
        Task FinalizeSessionAsync(SessionDao session, DateTime endTime, decimal hoursActive, string optionalNotes, Database database);
    }

    public class SessionRepository : ReadOnlyRepository<SessionDao>, ISessionRepository
    {
        public SessionRepository(IDatabaseFactory dbFactory)
            : base(dbFactory)
        { }

        public async Task StartSessionAsync(SessionDao session, Database database)
        {
            await database.ExecuteAsync(
                ";EXEC dbo.usp_StartSession @StartTime, @CardRoomId, @GameId, @SmallBlind, @BigBlind, @StartingStackSize",
                session
            );
        }

        public async Task FinalizeSessionAsync(
            SessionDao session,
            DateTime endTime,
            decimal hoursActive,
            Database database
        )
        {
            await FinalizeSessionAsync(session, endTime, hoursActive, null, database);
        }

        public async Task FinalizeSessionAsync(
            SessionDao session,
            DateTime endTime,
            decimal hoursActive,
            string optionalNotes,
            Database database
        )
        {
            await database.ExecuteAsync(
                ";EXEC dbo.usp_FinalizeSession @SessionId, @EndTime, @HoursActive, @OptionalNotes",
                new { SessionId = session.Id, EndTime = endTime, HoursActive = hoursActive, OptionalNotes = optionalNotes }
            );
        }
    }
}
