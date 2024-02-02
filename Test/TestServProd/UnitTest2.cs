using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.TestServProd
{
    [TestClass]
    public class UnitTest2
    {
        PMSDbContext _context;
        ProductService _service;
        IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            var loggerMock = new Mock<ILogger>(); // Khởi tạo logger mock
            loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(loggerMock.Object); // Thiết lập factory để trả về logger mock

            _context = new PMSDbContext();

            _unitOfWork = new UnitOfWork(_context, loggerFactoryMock.Object);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();
            _service = new ProductService(_unitOfWork, mapper);
        }

        [TestMethod]
        public void DeleteProduct_Test_Se()
        {
            Assert.IsNotNull(_service.DeleteProductAsync(1));
        }

        [TestMethod]
        public async Task UpdateProduct_Test_Se()
        {
            // Giả sử id là một số nguyên đại diện cho sản phẩm bạn muốn cập nhật
            int id = 1; // Ví dụ: ID của sản phẩm cần cập nhật là 1

            // Tạo một sản phẩm mới với các thông tin cập nhật
            var updatedProduct = new ProductDTO
            {
                // Thiết lập các thông tin cập nhật cho sản phẩm
                // Ví dụ:
                ProductName = "New Product Name",
                QuantityPerUnit = "New Quantity Per Unit",
                UnitPrice = 99.99m // Giả sử cập nhật giá sản phẩm
            };

            // Thực hiện cập nhật
            var result = await _service.UpdateProductAsync(updatedProduct, id);

            // Kiểm tra kết quả
            Assert.IsTrue(result);
        }
    }
}
