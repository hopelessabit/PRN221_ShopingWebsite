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

namespace Test.Test
{
    [TestClass]
    public class UnitTest2
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
        public async Task DeleteProduct_Test_Repo()
        {
            // Giả sử id là một số nguyên đại diện cho sản phẩm bạn muốn xóa
            int id = 1; // Ví dụ: ID của sản phẩm cần xóa là 1

            // Thực hiện xóa
            var result = await _repository.Remove(id);

            // Kiểm tra kết quả
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateProduct_Test_Repo()
        {
            // Giả sử id là một số nguyên đại diện cho sản phẩm bạn muốn cập nhật
            int id = 1; // Ví dụ: ID của sản phẩm cần cập nhật là 1

            // Tạo một sản phẩm mới với các thông tin cập nhật
            var updatedProduct = new Product
            {
                ProductId = id, // Sử dụng ID của sản phẩm cần cập nhật
                // Thiết lập các thông tin cập nhật cho sản phẩm
                // Ví dụ:
                ProductName = "New Product Name",
                QuantityPerUnit = "New Quantity Per Unit",
                UnitPrice = 99.99m // Giả sử cập nhật giá sản phẩm
            };

            // Thực hiện cập nhật
            var result = await _repository.Upsert(updatedProduct,id);

            // Kiểm tra kết quả
            Assert.IsTrue(result);
        }
    }
}
