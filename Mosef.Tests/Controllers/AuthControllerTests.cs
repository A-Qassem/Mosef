using Xunit;
using Mosef.Controllers;
using Mosef;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace Mosef.Tests.Controllers
{
    public class AuthControllerTests
    {
        private MosefDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<MosefDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid())
                .Options;
            return new MosefDbContext(options);
        }

        [Fact]
        public async Task Register_WithValidData_ReturnsOk()
        {
            var context = GetInMemoryDb();
            var controller = new AuthController(context);

            var dto = new RegisterDto
            {
                ssn = "123456789",
                firstName = "Ahmed",
                lastName = "Ali",
                email = "ahmed@test.com",
                password = "StrongPass1",
                phone = "0100000000",
                location = "Cairo"
            };

            var result = await controller.Register(dto);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Register_WithDuplicateEmail_ReturnsBadRequest()
        {
            var context = GetInMemoryDb();
            var controller = new AuthController(context);

            var dto1 = new RegisterDto
            {
                ssn = "123",
                firstName = "Test",
                lastName = "User",
                email = "test@mail.com",
                password = "Pass1234",
                phone = "010",
                location = "Alex"
            };
            await controller.Register(dto1);

            var dto2 = new RegisterDto
            {
                ssn = "456",
                firstName = "Another",
                lastName = "User",
                email = "test@mail.com", // ‰›” «·≈Ì„Ì·
                password = "Pass5678",
                phone = "011",
                location = "Giza"
            };
            var result = await controller.Register(dto2);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Register_WithWeakPassword_ReturnsBadRequest()
        {
            var context = GetInMemoryDb();
            var controller = new AuthController(context);

            var dto = new RegisterDto
            {
                ssn = "999",
                firstName = "Weak",
                lastName = "Password",
                email = "weak@mail.com",
                password = "weak", // ÷⁄Ì›
                phone = "012",
                location = "Tanta"
            };

            var result = await controller.Register(dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOk()
        {
            var context = GetInMemoryDb();
            var controller = new AuthController(context);

            var dto = new RegisterDto
            {
                ssn = "777",
                firstName = "Log",
                lastName = "In",
                email = "login@test.com",
                password = "ValidPass1",
                phone = "0111",
                location = "Mansoura"
            };

            await controller.Register(dto);

            var loginResult = await controller.Login(new LoginDto
            {
                email = "login@test.com",
                password = "ValidPass1"
            });

            Assert.IsType<OkObjectResult>(loginResult);
        }

        [Fact]
        public async Task Login_WithWrongPassword_ReturnsBadRequest()
        {
            var context = GetInMemoryDb();
            var controller = new AuthController(context);

            var dto = new RegisterDto
            {
                ssn = "888",
                firstName = "Wrong",
                lastName = "Pass",
                email = "wrongpass@test.com",
                password = "CorrectPass1",
                phone = "0112",
                location = "Aswan"
            };

            await controller.Register(dto);

            var result = await controller.Login(new LoginDto
            {
                email = "wrongpass@test.com",
                password = "WrongPass" // €·ÿ
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
