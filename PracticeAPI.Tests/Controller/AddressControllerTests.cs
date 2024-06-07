using PracticeAPI.Data;
using PracticeAPI.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;

namespace PracticeAPI.Tests.Controller
{
    public class AddressControllerTests
    {

        [Fact]
        public void Get_ReturnsOkResult_WithAddress()
        {
            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "Get_ReturnsOkResult_WithAddress")
                .Options;

            var addressId = 1;

            using (var context = new Data.DbContext(options))
            {
                var address = new Address(addressId, Enums.Country.Ukraine, "Test", "Test", "13-a", null);
                context.Address.Add(address);
                context.SaveChanges();
            }

            using (var context = new Data.DbContext(options))
            {
                var controller = new AddressController(context);
                var result = controller.Get(addressId);
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnAddress = Assert.IsType<Address>(okResult.Value);
                Assert.Equal(addressId, returnAddress.Id);
            }
            
        }

        [Fact]
        public void Get_ReturnsNotFound_WhenAddressDoesNotExist()
        {

            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "Get_ReturnsNotFound_WhenAddressDoesNotExist")
                .Options;

            using (var context = new Data.DbContext(options))
            {
                var controller = new AddressController(context);
                var result = controller.Get(1);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithAddresses()
        {

            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "GetAll_ReturnsOkResult_WithAddresses")
                .Options;

            using (var context = new Data.DbContext(options))
            {
                var address1 = new Address(1, Enums.Country.Ukraine, "Test", "Test", "13-a", null);
                var address2 = new Address(2, Enums.Country.Ukraine, "Test", "Test", "13-a", null);
                context.Address.AddRange(address1, address2);
                context.SaveChanges();
            }

            using (var context = new Data.DbContext(options))
            {
                var controller = new AddressController(context);
                var result = controller.GetAll();

                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnAddresses = Assert.IsType<List<Address>>(okResult.Value);

                Assert.Equal(2, returnAddresses.Count);

            }
        }
        [Fact]
        public void GetAll_ReturnsNotFound_WhenNoAddressesExist()
        {
            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "GetAll_ReturnsNotFound_WhenNoAddressesExist")
                .Options;

            using (var context = new Data.DbContext(options))
            {
                var controller = new AddressController(context);
                var result = controller.GetAll();
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public void Post_ReturnsOkResult_WhenAddressIsValid()
        {
            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "Post_ReturnsOkResult_WhenAddressIsValid")
                .Options;

            using (var context = new Data.DbContext(options))
            {
                var addressModel = new AddressCreateModel(Enums.Country.Ukraine, "Test", "Test", "13-a", null);
                var controller = new AddressController(context);
                var result = controller.Post(addressModel);
                Assert.IsType<OkResult>(result);
            }
        }


        [Fact]
        public void Post_ReturnsBadRequest_WhenAddressIsInvalid()
        {
            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "Post_ReturnsBadRequest_WhenAddressIsInvalid")
                .Options;
            using (var context = new Data.DbContext(options))
            {
                var addressModel = new AddressCreateModel(Enums.Country.Ukraine, "", "Test", "13-a", null);
                var controller = new AddressController(context);
                var result = controller.Post(addressModel);
                Assert.IsType<BadRequestResult>(result);
            }
        }
        [Fact]
        public void Put_ReturnsOkResult_WhenAddressIsValidAndExists()
        {
            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "Put_ReturnsOkResult_WhenAddressIsValidAndExists")
                .Options;

            var addressId = 1;

            using (var context = new Data.DbContext(options))
            {
                var address = new Address(addressId, Enums.Country.Ukraine, "Test", "Test", "13-a", null);
                context.Address.Add(address);
                context.SaveChanges();
            }

            using (var context = new Data.DbContext(options))
            {
                var controller = new AddressController(context);
                var address = new Address(addressId, Enums.Country.None, "A", "A", "12", 1);
                var result = controller.Put(addressId, address);
                var resultGet = controller.Get(addressId);

                Assert.IsType<OkResult>(result);


                var okResultGet = Assert.IsType<OkObjectResult>(resultGet);
                var returnAddress = Assert.IsType<Address>(okResultGet.Value);
                Assert.Equal(returnAddress.Street, address.Street);
            }
        }

        [Fact]
        public void Put_ReturnsBadRequest_WhenAddressIsInvalid()
        {
            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "Put_ReturnsBadRequest_WhenAddressIsInvalid")
                .Options;

            var addressId = 1;

            using (var context = new Data.DbContext(options))
            {
                var address = new Address(addressId, Enums.Country.Ukraine, "Test", "Test", "13-a", null);
                context.Address.Add(address);
                context.SaveChanges();
            }


            using (var context = new Data.DbContext(options))
            {
                var controller = new AddressController(context);
                var updatedAddress = new AddressCreateModel(Enums.Country.Ukraine, "", "Test", "13-a", null);
                var result = controller.Put(addressId, updatedAddress);

                Assert.IsType<BadRequestResult>(result);
            }
            
        }

        [Fact]
        public void Put_ReturnsNotFound_WhenAddressDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "Put_ReturnsNotFound_WhenAddressDoesNotExist")
                .Options;


            using (var context = new Data.DbContext(options))
            {
                var addressId = 1;
                var controller = new AddressController(context);
                var updatedAddress = new AddressCreateModel(Enums.Country.Ukraine, "Test", "Test", "13-a", null);
                var result = controller.Put(addressId, updatedAddress);
                Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public void Delete_ReturnsOkResult_WhenAddressExists()
        {
            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_ReturnsOkResult_WhenAddressExists")
                .Options;

            var addressId = 1;

            using (var context = new Data.DbContext(options))
            {
                var address = new Address(addressId, Enums.Country.Ukraine, "Test", "Test", "13-a", null);
                context.Address.Add(address);
                context.SaveChanges();
            }

            using (var context = new Data.DbContext(options))
            {
                var controller = new AddressController(context);
                var result = controller.Delete(addressId);
                Assert.IsType<OkResult>(result);
                Assert.True(!context.Address.Any());
            }
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenAddressDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<Data.DbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_ReturnsNotFound_WhenAddressDoesNotExist")
                .Options;

            using (var context = new Data.DbContext(options))
            {
                var addressId = 1;
                var controller = new AddressController(context);
                var result = controller.Delete(addressId);
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
