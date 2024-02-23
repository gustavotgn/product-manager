using Microsoft.VisualStudio.TestTools.UnitTesting;
using Product.Domain.Error;
using System.Threading.Tasks;
using System;
using Product.UnitTest.Builders.Entities;
using System.Linq;
using Product.UnitTest.Extensions;
using System.Collections.Generic;
using Product.Domain.Entities;
using Product.UnitTest.Builders.DTOs.Product;

namespace Product.UnitTest.Services.v1
{
    [TestClass]
    public class ProductServiceTest : BaseTest
    {
        #region GetAsync

        [TestMethod]
        public async Task GetAsync_Success()
        {
            var supplier = new SupplierEntityBuilder().Default().Build();
            var product = new ProductEntityBuilder().Default().WithSupplierId(supplier.Id).Build();

            //Mocks
            await SupplierRepository.InsertAsync(supplier);
            await ProductRepository.InsertAsync(product);

            //Act
            var result = await ProductService.GetAsync(product.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public async Task GetAsync_Empty_Success()
        {
            //Act
            var result = await ProductService.GetAsync(1);

            var expected = EErrors.ProductGetAsyncProductNotFound;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual(expected, result.GetEnumError());

        }

        #endregion GetAsync

        #region CreateAsync

        [TestMethod]
        public async Task CreateAsync_Success()
        {
            var request = new CreateProductDTOBuilder().Default().Build();
            var product = new ProductEntityBuilder().Default().WithIsActive(false).Build();
            var supplier = new SupplierEntityBuilder().Default().Build();

            //Mocks
            await ProductRepository.InsertAsync(product);
            await SupplierRepository.InsertAsync(supplier);


            //Act
            var result = await ProductService.CreateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(ApplicationDbContext.Products.Any());
        }

        [TestMethod]
        [DataRow("aa")]
        [DataRow("lacus suspendisse faucibus interdum posuere lorem ipsum dolor sit amet interdum posuere lorem ipsum dolor sit amet")]
        public async Task CreateAsync_Description_Fail(string description)
        {
            var request = new CreateProductDTOBuilder().Default()
                .WithDescription(description)
                .Build();

            //Act
            var result = await ProductService.CreateAsync(request);

            var expected = EErrors.ProductCreateAsyncDescriptionInvalid;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);

            Assert.AreEqual(expected, result.GetEnumError());
        }

        [TestMethod]
        [DataRow("2024-02-03 14:59:06", "2024-02-03 14:59:06")]
        [DataRow("2024-02-03 14:59:07", "2024-02-03 14:59:06")]
        public async Task CreateAsync_ManufacturingDateInvalid_Fail(string manufacturingDate, string expirationDate)
        {
            var request = new CreateProductDTOBuilder().Default()
                .WithManufacturingDate(DateTime.Parse(manufacturingDate))
                .WithExpirationDate(DateTime.Parse(expirationDate))
                .Build();

            //Act
            var result = await ProductService.CreateAsync(request);

            var expected = EErrors.ProductCreateAsyncManufacturingDateInvalid;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);

            Assert.AreEqual(expected, result.GetEnumError());
        }

        [TestMethod]
        public async Task CreateAsync_SupplierIdInvalid_Fail()
        {
            var request = new CreateProductDTOBuilder().Default().Build();

            //Act
            var result = await ProductService.CreateAsync(request);

            var expected = EErrors.ProductCreateAsyncSupplierIdInvalid;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);

            Assert.AreEqual(expected, result.GetEnumError());
        }

        [TestMethod]
        public async Task CreateAsync_ProductAlreadyExists_Fail()
        {
            var supplier = new SupplierEntityBuilder().Default().Build();
            var request = new CreateProductDTOBuilder().Default().WithSupplierId(supplier.Id).Build();
            var product = new ProductEntityBuilder()
                .Default()
                .WithManufacturingDate(request.ManufacturingDate)
                .WithExpirationDate(request.ExpirationDate)
                .WithSupplierId(supplier.Id).Build();

            //Mocks
            await SupplierRepository.InsertAsync(supplier);
            await ProductRepository.InsertAsync(product);

            //Act
            var result = await ProductService.CreateAsync(request);

            var expected = EErrors.ProductCreateAsyncProductAlreadyExists;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);

            Assert.AreEqual(expected, result.GetEnumError());
        }

        #endregion CreateAsync

        #region DeleteAsync

        [TestMethod]
        public async Task DeleteAsync_Success()
        {
            var entity = new ProductEntityBuilder().Default().WithIsActive(false).Build();

            //Mocks
            await ProductRepository.InsertAsync(entity);

            //Act
            var result = await ProductService.DeleteAsync(entity.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(!ApplicationDbContext.Products.Any());
        }

        [TestMethod]
        public async Task DeleteAsync_ProductNotFound_Fail()
        {
            //Act
            var result = await ProductService.DeleteAsync(1);

            var expected = EErrors.ProductDeleteAsyncProductNotFound;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expected, result.GetEnumError());
        }

        #endregion DeleteAsync

        #region ListAsync

        [TestMethod]
        [DataRow("Arroz")]
        [DataRow(null)]
        public async Task GetAllAsync_Success(string filter)
        {
            var entity = new ProductEntityBuilder().Default().Build();

            //Mock
            await ProductRepository.InsertAsync(entity);

            //Act
            var result = await ProductService.ListAsync(filter);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(result.Data.Items.Any());
            Assert.IsTrue(result.Data.Total.Equals(1));
        }

        [TestMethod]
        [DataRow("São João", 2, 2, 1)]
        [DataRow(null, 2, 2, 1)]
        [DataRow("NotFound", 2, 2, 0)]
        public async Task GetAllAsync_PageEmpty_Success(string filter, int page, int pageSize, int total)
        {
            var supplier = new SupplierEntityBuilder().Default().Build();
            var entities = new List<ProductEntity>
            {
                new ProductEntityBuilder()
                    .Default().WithId(1).WithSupplierId(supplier.Id)
                    .WithIsActive(false).Build(),
                new ProductEntityBuilder()
                    .Default().WithId(2).WithSupplierId(supplier.Id)
                    .Build() 
            };

            //Mock
            await SupplierRepository.InsertAsync(supplier);
            await ProductRepository.InsertRangeAsync(entities);

            //Act
            var result = await ProductService.ListAsync(filter, page, pageSize);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(!result.Data.Items.Any());
            Assert.IsTrue(result.Data.Total.Equals(total));
        }

        #endregion GetAllAsync

        #region UpdateAsync

        [TestMethod]
        public async Task UpdateAsync_Success()
        {
            var supplier = new SupplierEntityBuilder().Default().Build();
            var request = new UpdateProductDTOBuilder().Default().WithDescription("Arroz Prato fino").Build();
            var entity = new ProductEntityBuilder().Default().WithSupplierId(supplier.Id).Build();

            //Mocks
            await ProductRepository.InsertAsync(entity);

            //Act
            await SupplierRepository.InsertAsync(supplier);
            var result = await ProductService.UpdateAsync(entity.Id, request);

            entity = await ProductRepository.SelectAsync(entity.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(entity);
            Assert.AreEqual(entity.Description, request.Description);
            Assert.AreEqual(entity.ManufacturingDate, request.ManufacturingDate);
            Assert.AreEqual(entity.ExpirationDate, request.ExpirationDate);
        }

        [TestMethod]
        public async Task UpdateAsync_ProductNotFound_Fail()
        {
            var supplier = new SupplierEntityBuilder().Default().Build();
            var request = new UpdateProductDTOBuilder().Default().WithDescription("Arroz Prato fino").Build();
            var entity = new ProductEntityBuilder().Default().WithIsActive(false).WithSupplierId(supplier.Id).Build();

            //Mocks
            await ProductRepository.InsertAsync(entity);

            //Act
            await SupplierRepository.InsertAsync(supplier);
            var result = await ProductService.UpdateAsync(1, request);

            var expected = EErrors.ProductUpdateAsyncProductNotFound;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expected, result.GetEnumError());
        }

        #endregion UpdateAsync
    }
}
