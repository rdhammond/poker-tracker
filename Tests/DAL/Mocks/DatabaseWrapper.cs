using Moq;
using PokerTracker.DAL.Wrappers;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.Tests.DAL.Mocks
{
    public class DatabaseWrapperMock : Mock<IDatabaseWrapper>
    {
        private readonly Dictionary<string, IEnumerable> _lists =
            new Dictionary<string, IEnumerable>();

        public Dictionary<string, IEnumerable> Lists
        {
            get { return _lists; }
        }

        public void AddList<T>(List<T> list)
        {
            Lists.Add(typeof(T).Name, list);
            Setup(x => x.FetchAsync<T>(It.IsAny<string>())).Returns(() => GetListAsync<T>());
            Setup(x => x.SaveAsync(It.IsAny<T>())).Returns<T>(x => SaveToListAsync(x));
        }
        
        private List<T> GetList<T>()
        {
            return (List<T>)Lists[typeof(T).Name];
        }

        private Task<List<T>> GetListAsync<T>()
        {
            return Task.Run(() => GetList<T>());
        }

        private Task SaveToListAsync<T>(T entity)
        {
            return Task.Run(() => GetList<T>().Add(entity));
        }
    }
}