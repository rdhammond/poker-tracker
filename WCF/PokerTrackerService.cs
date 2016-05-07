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

        [WebInvoke(
            Method = "POST",
            UriTemplate = "Session",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        Task SaveSessionAsync(Session session);

        [WebGet(UriTemplate = "Summaries", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<Dictionary<Guid,Summary>> GetSessionSummariesAsync();

        [WebGet(UriTemplate = "TotalHourlyRate", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<decimal> GetTotalHourlyRateAsync();
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PokerTrackerService : IPokerTrackerService
    {
        private ICardRoomsService CardRoomsSvc;
        private IGamesService GamesSvc;
        private ISessionService SessionSvc;
        private ISummaryService SummarySvc;

        public PokerTrackerService()
        {
            using (var container = new Container(new WCFRegistry()))
            {
                Initialize(container.GetInstance<ICardRoomsService>(),
                    container.GetInstance<IGamesService>(),
                    container.GetInstance<ISessionService>(),
                    container.GetInstance<ISummaryService>());
            }
        }

        public PokerTrackerService(
            ICardRoomsService cardRoomsSvc,
            IGamesService gamesSvc,
            ISessionService sessionSvc,
            ISummaryService summarySvc
        )
        {
            Initialize(cardRoomsSvc, gamesSvc, sessionSvc, summarySvc);
        }

        private void Initialize(
            ICardRoomsService cardRoomsSvc,
            IGamesService gamesSvc,
            ISessionService sessionSvc,
            ISummaryService summarySvc
        )
        {
            CardRoomsSvc = cardRoomsSvc;
            GamesSvc = gamesSvc;
            SessionSvc = sessionSvc;
            SummarySvc = summarySvc;
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

        public async Task<decimal> GetTotalHourlyRateAsync()
        {
            try
            {
                return await SummarySvc.GetTotalHourlyRateAsync();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e);
            }
        }

        public async Task SaveSessionAsync(Session session)
        {
            try
            {
                await SessionSvc.SaveSessionAsync(session);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e);
            }
        }
    }
}