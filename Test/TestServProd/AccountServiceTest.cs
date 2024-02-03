using AutoMapper;
using Core.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test
{
    [TestClass]
    public class AccountServiceTest
    {
        PMSDbContext _context;
        AccountService _service;
        IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            _context = new PMSDbContext();

            _unitOfWork = new UnitOfWork(_context, loggerFactoryMock.Object);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();
            _service = new AccountService(_unitOfWork, mapper);
        }

        [TestMethod]
        public void Test1()
        {
            var result = _service.GetAccountAsync().Result;
            Assert.IsNotNull(result);
        }
    }
}