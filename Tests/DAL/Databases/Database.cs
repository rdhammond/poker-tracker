using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerTracker.DAL.Attributes;
using PokerTracker.DAL.DAO;
using PokerTracker.DAL.Databases;
using System.Transactions;
using PokerTracker.Tests.DAL.Mocks;

namespace PokerTracker.Tests.DAL.Databases
{
    [TestClass]
    public class DatabaseTests
    {
        [TableName("GoodClasses")]
        public class GoodClass : IDao
        {
            public const string ExpectedSelect = "SELECT * FROM [GoodClasses]";
            public const string ExpectedInsert = "INSERT INTO [GoodClasses]([Test1],[Test2],[Test3]) VALUES(@Test1,@Test2,@Test3)";

            public string Test1 { get; set; }
            public int? Test2 { get; set; }
            public char Test3 { get; set; }
        }

        public class BadClass : IDao
        { }

        private DbConnectionMock<GoodClass> _dbConnMock;
        private DbConnectionFactoryMock<GoodClass> _dbConnFactMock;
        private IDatabase _database;

        [TestInitialize]
        public void SetUp()
        {
            _dbConnMock = new DbConnectionMock<GoodClass>();
            _dbConnFactMock = new DbConnectionFactoryMock<GoodClass>(_dbConnMock.Object);
            _database = new Database(_dbConnFactMock.Object);
        }

        [TestMethod]
        public void BeginTransaction_Works()
        {
            Assert.IsNull(Transaction.Current);

            using (var transaction = Database.BeginTransaction())
            {
                Assert.IsNotNull(Transaction.Current);
            }
        }

        [TestMethod]
        public void Transaction_UsesIfAvailable()
        {
            using (var transaction = Database.BeginTransaction())
            {
                _database.PulseTestAsync().Wait();
                _dbConnMock.Verify(x => x.EnlistTransaction(Transaction.Current));
            }
        }
        
        [TestMethod]
        public void Transaction_StillCompletesAfterUse()
        {
            using (var transaction = Database.BeginTransaction())
            {
                _database.PulseTestAsync().Wait();
                transaction.Complete();
            }
        }

        [TestMethod]
        public void Transaction_DoesNotCreateNew()
        {
            Assert.IsNull(Transaction.Current);
            _database.PulseTestAsync().Wait();
            Assert.IsNull(Transaction.Current);
        }

        [TestMethod]
        public void SelectAllSql_Works()
        {
            Assert.AreEqual(GoodClass.ExpectedSelect, Database.SelectAllSql<GoodClass>());
        }

        [TestMethod]
        public void SelectAllSql_NoTableName_Throws()
        {
            AssertHelper.Throws(() => { Database.SelectAllSql<BadClass>(); });
        }

        [TestMethod]
        public void InsertSql_Works()
        {
            Assert.AreEqual(GoodClass.ExpectedInsert, Database.InsertSql<GoodClass>());
        }

        [TestMethod]
        public void InsertSql_NoTableName_Throws()
        {
            AssertHelper.Throws(() => { Database.InsertSql<BadClass>(); });
        }
    }
}
