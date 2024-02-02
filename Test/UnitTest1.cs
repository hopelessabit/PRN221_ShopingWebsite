using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Core.Entities;
using Core.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        PMSDbContext _context;
        IProductRepository _repository;
        IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            var loggerMock = new Mock<ILogger>();
            _context = new PMSDbContext();
            _repository = new ProductRepository(_context, loggerMock.Object);
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            // Giả sử id là một số nguyên đại diện cho sản phẩm bạn muốn xóa
            int id = 1; // Ví dụ: ID của sản phẩm cần xóa là 1

            // Thực hiện xóa
            var result = await _repository.Remove(id);

            // Kiểm tra kết quả
            Assert.IsTrue(result);
        }
    }
}
