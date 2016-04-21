using PokerTracker.Config;
using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace PokerTracker.DAL.Services
{
    public interface ISessionService
    {
        Guid StartSession(DateTime startTime, Guid cardRoomId, Guid gameId, int smallBlind, int bigBlind, int startingStackSize);
        Guid InsertTimeEntry(Guid sessionId, DateTime currentTime, int stackSize, int dealerTokes, int serverTips);
        void FinalizeSession(Guid sessionId, DateTime endTime, Decimal hoursActive, string optionalNotes);

        Task<Guid> StartSessionAsync(DateTime startTime, Guid cardRoomId, Guid gameId, int smallBlind, int bigBlind, int startingStackSize);
        Task<Guid> InsertTimeEntryAsync(Guid sessionId, DateTime currentTime, int stackSize, int dealerTokes, int serverTips);
        Task FinalizeSessionAsync(Guid sessionId, DateTime endTime, Decimal hoursActive, string optionalNotes);
    }

    public class SessionService : DataService, ISessionService
    {
        private readonly IConfig Config;

        public SessionService(IConfig config)
            :base (config)
        { }

        public Guid StartSession(DateTime startTime, Guid cardRoomId, Guid gameId, int smallBlind, int bigBlind, int startingStackSize)
        {
            return AsyncHelper.RunSync(() => StartSessionAsync(
                startTime,
                cardRoomId,
                gameId,
                smallBlind,
                bigBlind,
                startingStackSize
            ));
        }

        public async Task<Guid> StartSessionAsync(DateTime startTime, Guid cardRoomId, Guid gameId, int smallBlind, int bigBlind, int startingStackSize)
        {
            return await RunQueryAsync(async (connection) => {
                return (await connection.QueryAsync<Guid>(
                    "usp_StartSession",
                    new
                    {
                        StartTime = startTime,
                        CardRoomId = cardRoomId,
                        GameId = gameId,
                        SmallBlind = smallBlind,
                        BigBlind = bigBlind,
                        StartingStackSize = startingStackSize
                    },
                    commandType: CommandType.StoredProcedure
                )).FirstOrDefault();
            });
        }

        public Guid InsertTimeEntry(Guid sessionId, DateTime currentTime, int stackSize, int dealerTokes, int serverTips)
        {
            return AsyncHelper.RunSync(() => InsertTimeEntryAsync(
                sessionId,
                currentTime,
                stackSize,
                dealerTokes,
                serverTips
            ));
        }

        public async Task<Guid> InsertTimeEntryAsync(Guid sessionId, DateTime currentTime, int stackSize, int dealerTokes, int serverTips)
        {
            return await RunQueryAsync(async (connection) =>
            {
                return (await connection.QueryAsync<Guid>(
                    "usp_Insert_TimeEntry",
                    new
                    {
                        SessionId = sessionId,
                        CurrentTime = currentTime,
                        StackSize = stackSize,
                        DealerTokes = dealerTokes,
                        ServerTips = serverTips
                    },
                    commandType: CommandType.StoredProcedure
                )).FirstOrDefault();
            });
        }

        public void FinalizeSession(Guid sessionId, DateTime endTime, Decimal hoursActive, string optionalNotes)
        {
            AsyncHelper.RunSync(() => FinalizeSessionAsync(
                sessionId,
                endTime,
                hoursActive,
                optionalNotes
            ));
        }

        public async Task FinalizeSessionAsync(Guid sessionId, DateTime endTime, Decimal hoursActive, string optionalNotes)
        {
            await RunQueryAsync(async (connection) =>
            {
                await connection.QueryAsync<bool>(
                    "usp_FinalizeSession",
                    new
                    {
                        SessionId = sessionId,
                        EndTime = endTime,
                        HoursActive = hoursActive,
                        OptionalNotes = optionalNotes
                    },
                    commandType: CommandType.StoredProcedure
                );
            });
        }
    }
}
