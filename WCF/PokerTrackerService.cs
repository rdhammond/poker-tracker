using PokerTracker.BLL.Objects;
using PokerTracker.BLL.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using System.Web;

namespace PokerTracker.WCF
{
    [ServiceContract]
    public interface IPokerTrackerService
    {
        [WebGet(UriTemplate = "CardRooms", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<Dictionary<Guid, string>> GetCardRoomsAsync();

        [WebGet(UriTemplate = "GameTypes", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<Dictionary<Guid, string>> GetGameTypesAsync();

        [WebInvoke(Method = "OPTIONS", UriTemplate = "Session")]
        void SaveSessionOPTIONS();

        [WebInvoke(
            Method = "POST",
            UriTemplate = "Session",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        Task<bool> SaveSessionAsync(Session session);

        [WebGet(UriTemplate = "Summaries", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<Dictionary<Guid,Summary>> GetSessionSummariesAsync();

        [WebGet(UriTemplate = "Statistics", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<Statistics> GetStatisticsAsync();
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PokerTrackerService : IPokerTrackerService
    {
        private ICardRoomsService CardRoomsSvc;
        private IGamesService GamesSvc;
        private ISessionService SessionSvc;
        private ISummaryService SummarySvc;
        private IStatisticsService StatisticsSvc;

        public PokerTrackerService()
        {
            using (var container = new Container(new WCFRegistry()))
            {
                Initialize(container.GetInstance<ICardRoomsService>(),
                    container.GetInstance<IGamesService>(),
                    container.GetInstance<ISessionService>(),
                    container.GetInstance<ISummaryService>(),
                    container.GetInstance<IStatisticsService>()
                );
            }
        }

        public PokerTrackerService(
            ICardRoomsService cardRoomsSvc,
            IGamesService gamesSvc,
            ISessionService sessionSvc,
            ISummaryService summarySvc,
            IStatisticsService statisticsSvc
        )
        {
            Initialize(cardRoomsSvc, gamesSvc, sessionSvc, summarySvc, statisticsSvc);
        }

        private void Initialize(
            ICardRoomsService cardRoomsSvc,
            IGamesService gamesSvc,
            ISessionService sessionSvc,
            ISummaryService summarySvc,
            IStatisticsService statisticsSvc
        )
        {
            CardRoomsSvc = cardRoomsSvc;
            GamesSvc = gamesSvc;
            SessionSvc = sessionSvc;
            SummarySvc = summarySvc;
            StatisticsSvc = statisticsSvc;
        }

        public async Task<Dictionary<Guid, string>> GetCardRoomsAsync()
        {
            try
            {
                return (await CardRoomsSvc.GetAllAsync())
                    .ToDictionary(x => x.Id, x => x.Name);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e);
            }
        }

        public async Task<Dictionary<Guid, string>> GetGameTypesAsync()
        {
            try
            {
                return (await GamesSvc.GetAllAsync())
                    .ToDictionary(x => x.Id, x => x.Name);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e);
            }
        }

        public async Task<Dictionary<Guid,Summary>> GetSessionSummariesAsync()
        {
            try
            {
                return (await SummarySvc.GetAllAsync())
                    .ToDictionary(x => x.Id);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e);
            }
        }

        public async Task<Statistics> GetStatisticsAsync()
        {
            try
            {
                return await StatisticsSvc.GetAsync();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e);
            }
        }

        public async Task<bool> SaveSessionAsync(Session session)
        {
            try
            {
                session.Id = Guid.NewGuid();
                session.TimeEntries.ForEach(x => x.Id = Guid.NewGuid());

                await SessionSvc.SaveSessionAsync(session);
                return true;
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e);
            }
        }

        public void SaveSessionOPTIONS()
        { }
    }
}