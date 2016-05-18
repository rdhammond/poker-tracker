using Moq;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerTracker.Tests.DAL.Mocks
{
    public class DatabaseMock<T> : Mock<IDatabase>
        where T : IDao
    {
        private readonly List<T> _daoList;

        public SwingDao Swing { get; set; }

        public List<T> DaoList
        {
            get { return _daoList; }
        }

        public DatabaseMock(List<T> daoList = null)
        {
            _daoList = daoList ?? new List<T>();

            Setup(x => x.FetchAllAsync<T>()).Returns(() => Task.FromResult(_daoList));
            Setup(x => x.InsertAsync(It.IsAny<T>())).Returns<T>(e => Task.Run(() => _daoList.Add(e)));
            Setup(x => x.InsertAsync(It.IsAny<IEnumerable<T>>())).Returns<IEnumerable<T>>(l => Task.Run(() => _daoList.AddRange(l)));

            Setup(x => x.RunAsync<SwingDao>(It.Is<string>(v => v == "usp_BiggestDownswing"), It.IsAny<object>()))
                .Returns(() => Task.FromResult(new[] { Swing }.AsEnumerable()));

            Setup(x => x.RunAsync<SwingDao>(It.Is<string>(v => v == "usp_BiggestUpswing"), It.IsAny<object>()))
                .Returns(() => Task.FromResult(new[] { Swing }.AsEnumerable()));
        }
    }
}
