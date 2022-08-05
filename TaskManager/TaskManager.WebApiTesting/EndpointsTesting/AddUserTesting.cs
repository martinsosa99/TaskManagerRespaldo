using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.WebApi.Controllers;
using TaskManager.DTO;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.WebApiTesting.EndpointsTesting;

[TestClass]
public class AddUserTesting
{

    [TestMethod]
    public void AddUserTest()
    {
        UserDTO userDTO = new UserDTO();
        userDTO.Id = 1;
        userDTO.EmailAddress = "hfjdshjf@gfhjds.com";
        userDTO.Password = "hfgjdkhdfg";

        User user = UserDTO.UserDTOToUser(userDTO);

        var userServiceMock = new Mock<IUserService>(MockBehavior.Strict);

        userServiceMock.Setup(r => r.AddUser(user)).Verifiable();

        var controller = new UsersController(userServiceMock.Object);

        var result = controller.AddUserAsync(userDTO);
        var okResult = result.Result as OkResult;

        userServiceMock.VerifyAll();

        Assert.IsInstanceOfType(okResult, typeof(OkResult));
    }

}