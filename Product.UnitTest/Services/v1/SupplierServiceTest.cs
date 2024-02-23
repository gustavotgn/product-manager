using Microsoft.VisualStudio.TestTools.UnitTesting;
using Product.Domain.Entities;
using Product.Domain.Error;
using Product.UnitTest.Builders.Entities;
using Product.UnitTest.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.UnitTest.Builders.DTOs.Supplier;

namespace Product.UnitTest.Services.v1
{
    [TestClass]
    public class SupplierServiceTest : BaseTest
    {
        #region GetAsync

        [TestMethod]
        public async Task GetAsync_Success()
        {
            var supplier = new SupplierEntityBuilder().Default().Build();

            //Mocks
            await SupplierRepository.InsertAsync(supplier);

            //Act
            var result = await SupplierService.GetAsync(supplier.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public async Task GetAsync_Empty_Success()
        {
            //Act
            var result = await SupplierService.GetAsync(1);

            var expected = EErrors.SupplierGetAsyncSupplierNotFound;

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
            var request = new CreateSupplierDTOBuilder().Default().Build();
            
            var supplier = new SupplierEntityBuilder().Default().WithIsActive(false).Build();

            //Mocks
            
            await SupplierRepository.InsertAsync(supplier);

            //Act
            var result = await SupplierService.CreateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(ApplicationDbContext.Suppliers.Any());
        }

        [TestMethod]
        [DataRow("aa")]
        [DataRow("lacus suspendisse faucibus interdum posuere lorem ipsum dolor sit amet interdum posuere lorem ipsum dolor sit amet")]
        public async Task CreateAsync_Description_Fail(string description)
        {
            var request = new CreateSupplierDTOBuilder().Default()
                .WithDescription(description)
                .Build();

            //Act
            var result = await SupplierService.CreateAsync(request);

            var expected = EErrors.SupplierCreateAsyncDescriptionInvalid;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);

            Assert.AreEqual(expected, result.GetEnumError());
        }


        [TestMethod]
        public async Task CreateAsync_SupplierAlreadyExists_Fail()
        {
            var supplier = new SupplierEntityBuilder().Default().Build();
            var request = new CreateSupplierDTOBuilder().Default().Build();
            
            //Mocks
            await SupplierRepository.InsertAsync(supplier);
            
            //Act
            var result = await SupplierService.CreateAsync(request);

            var expected = EErrors.SupplierCreateAsyncSupplierAlreadyExists;

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
            var entity = new SupplierEntityBuilder().Default().WithIsActive(false).Build();

            //Mocks
            await SupplierRepository.InsertAsync(entity);

            //Act
            var result = await SupplierService.DeleteAsync(entity.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(!ApplicationDbContext.Suppliers.Any());
        }

        [TestMethod]
        public async Task DeleteAsync_SupplierNotFound_Fail()
        {
            //Act
            var result = await SupplierService.DeleteAsync(1);

            var expected = EErrors.SupplierDeleteAsyncSupplierNotFound;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expected, result.GetEnumError());
        }

        #endregion DeleteAsync

        #region GetAllAsync

        [TestMethod]
        [DataRow("São João")]
        [DataRow(null)]
        public async Task GetAllAsync_Success(string filter)
        {
            var entity = new SupplierEntityBuilder().Default().Build();

            //Mock
            await SupplierRepository.InsertAsync(entity);

            //Act
            var result = await SupplierService.ListAsync(filter);

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
            var entities = new List<SupplierEntity>
            {
                new SupplierEntityBuilder()
                    .Default().WithId(1)
                    .WithIsActive(false).Build(),
                new SupplierEntityBuilder()
                    .Default().WithId(2)
                    .Build()
            };

            //Mock
            await SupplierRepository.InsertRangeAsync(entities);

            //Act
            var result = await SupplierService.ListAsync(filter, page, pageSize);

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
            var entity = new SupplierEntityBuilder().Default().Build();
            var update = new UpdateSupplierDTOBuilder().Default().WithDescription("AutoGlass").Build();

            //Mocks
            await SupplierRepository.InsertAsync(entity);

            //Act
            var result = await SupplierService.UpdateAsync(entity.Id, update);

            entity = await SupplierRepository.SelectAsync(entity.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(entity);
            Assert.AreEqual(entity.Description, update.Description);
        }

        [TestMethod]
        public async Task UpdateAsync_SupplierNotFound_Fail()
        {
            var entity = new SupplierEntityBuilder().Default().Build();
            var update = new UpdateSupplierDTOBuilder().Default().WithDescription("AutoGlass").Build();

            //Mocks
            await SupplierRepository.InsertAsync(entity);

            //Act
            var result = await SupplierService.UpdateAsync(1, update);

            var expected = EErrors.SupplierUpdateAsyncSupplierNotFound;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(expected, result.GetEnumError());
        }

        #endregion UpdateAsync
    }
}
