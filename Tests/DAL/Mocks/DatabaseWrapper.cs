using Moq;
using PokerTracker.DAL.Wrappers;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerTracker.Tests.DAL.Mocks
{
    public class DatabaseWrapperMock : Mock<IDatabaseWrapper>
    {
        private readonly IDictionary<string, IEnumerable> Lists = new Dictionary<string, IEnumerable>();

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

        private async Task<List<T>> GetListAsync<T>()
        {
            return await Task.Run(() => GetList<T>());
        }

        private async Task SaveToListAsync<T>(T entity)
        {
            await Task.Run(() => GetList<T>().Add(entity));
        }
    }
}