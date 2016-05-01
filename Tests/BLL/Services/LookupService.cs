using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.BLL.Objects;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Repositories;
using PokerTracker.Tests.BLL.Mocks;
using System;
using System.Collections.Generic;

namespace PokerTracker.Tests.BLL.Services
{
    public abstract class LookupServiceTests<TRepo, TDao, TRepoMock>
        where TRepo : class, IReadOnlyRepository<TDao>
        where TRepoMock : ReadOnlyRepositoryMock<TRepo, TDao>, new()
    {
        private IMapper _mapper;
        private TRepoMock _repoMock;

        protected IMapper Mapper { get { return _mapper; } }
        protected TRepoMock RepoMock {  get { return _repoMock; } }

        protected List<TDao> DaoList
        {
            get { return _repoMock.DaoList; }
        }

        protected void Setup()
        {
            _mapper = MapperFactory.Create();
            _repoMock = new TRepoMock();
        }

        #region AssertListEquals

        protected void AssertListEquals<TSrc,TDest>(
            TSrc[] daos,
            Dictionary<Guid,TDest> objects
        )
            where TSrc : IdNameDao
            where TDest : IdNameObject
        {
            AssertListEquals(
                daos,
                objects,
                (d, o) => d.Id == o.Id && d.Name == o.Name
            );
        }

        protected void AssertListEquals<TSrc,TDest>(
            TSrc[] daos,
            Dictionary<Guid,TDest> objects,
            Func<TSrc,TDest,bool> assertEquals
        )
            where TSrc : IdDao
            where TDest : IdObject
        {
            foreach (var expectedItem in daos)
            {
                var actualItem = objects[expectedItem.Id];
                Assert.IsNotNull(actualItem);

                var expectedIdName = expectedItem as IdNameDao;
                var actualIdName = actualItem as IdNameObject;

                if (expectedIdName != null && actualIdName != null)
                {
                    Assert.AreEqual(expectedIdName.Id, actualIdName.Id);
                    Assert.AreEqual(expectedIdName.Name, actualIdName.Name);
                    continue;
                }

                Assert.IsTrue(assertEquals(expectedItem, actualItem));
            }
        }

        #endregion
    }
}
