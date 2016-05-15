using Moq;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.Tests.BLL.Mocks
{
    public class DatabaseMock<T> : Mock<IDatabase>
        where T : IDao
    {
        private readonly List<T> _daoList = new List<T>();

        public List<T> DaoList
        {
            get { return _daoList; }
        }

        public DatabaseMock()
        {
            Setup(x => x.FetchAllAsync<T>()).Returns(() => Task.FromResult(DaoList));
            Setup(x => x.InsertAsync(It.IsAny<T>())).Callback<T>(e => Task.Run(() => DaoList.Add(e)));
        }
    }
}