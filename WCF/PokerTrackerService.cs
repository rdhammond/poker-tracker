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

        [WebGet(UriTemplate = "Session/New", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Session CreateSession();

        [WebInvoke(Method = "POST", UriTemplate = "SessionPOST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task SaveSessionAsync(Session session, DateTime endTime,
            decimal hoursActive, string optionalNotes = null);

        [WebGet(UriTemplate = "Summaries", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<IEnumerable<Summary>> GetSessionSummariesAsync();

        [WebGet(UriTemplate = "TotalHourlyRate", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Task<decimal> GetTotalHourlyRateAsync();
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PokerTrackerService : IPokerTrackerService
    {
        private readonly ICardRoomsService CardRoomsSvc;
        private readonly IGamesService GamesSvc;
        private readonly ISessionService SessionSvc;
        private readonly ISummaryService SummarySvc;

        public PokerTrackerService()
        {
            using (var container = new Container(new WCFRegistry()))
            {
                CardRoomsSvc = container.GetInstance<ICardRoomsService>();
                GamesSvc = container.GetInstance<IGamesService>();
                SessionSvc = container.GetInstance<ISessionService>();
                SummarySvc = container.GetInstance<ISummaryService>();
            }
        }

        public Session CreateSession()
        {
            return new Session { Id = Guid.NewGuid() };
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

        public async Task<IEnumerable<Summary>> GetSessionSummariesAsync()
        {
            try
            {
                return await SummarySvc.GetAllAsync();
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

        public async Task SaveSessionAsync(Session session, DateTime endTime, decimal hoursActive, string optionalNotes = null)
        {
            try
            {
                await SessionSvc.SaveSessionAsync(session, endTime, hoursActive, optionalNotes);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e);
            }
        }
    }
}