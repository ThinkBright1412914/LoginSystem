using LoginSystem.Controllers;
using LoginSystem.Idenitity.Services;
using LoginSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LoginSystem.Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void  TestMethod1()
		{

		}

		[TestMethod]
		public async Task GetUserById_ReturnsUser()
		{
			// Arrange
			UserDataVM model = new UserDataVM
			{
				UserId = new Guid("740f2110-db15-4f37-8ac9-512c3a25eb87"),
				UserName = "Test",
				Email = "Test@gmail.com",
				isForcePswdReset = false,
				IsActive = true,
			};

			var service = new Mock<IAdminUserService>();
			service.Setup(x => x.GetUserById(It.IsAny<Guid>())).ReturnsAsync(model); 

			var controller = new UserController(service.Object);

			// Act
			var result = await controller.GetUsersById(new Guid("740f2110-db15-4f37-8ac9-512c3a25eb87")) as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode); 
			var userData = result.Value as UserDataVM;
			Assert.IsNotNull(userData);
			Assert.AreEqual("Test", userData.UserName); 
		}

	}
}