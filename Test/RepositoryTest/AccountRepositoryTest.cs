using Core.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test
{
    [TestClass]
    public class AccountRepositoryTest
    {
        PMSDbContext _context;
        IAccountRepository _repository;
        IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            var loggerMock = new Mock<ILogger>();
            _context = new PMSDbContext();
            _repository = new AccountRepository(_context, loggerMock.Object);
        }


        [TestMethod]
        public void Test3()
        {
            var result = _repository.GetAll();
            Assert.IsNotNull(result);
        }
    }
}