using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.WebApi.Controllers;
using TaskManager.DTO;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.WebApiTesting.EndpointsTesting;

[TestClass]
public class LoginTesting
{

    [TestMethod]
    public void LoginTest()
    {
        
        string emailAddress = "username@sdd.com";
        string password = "1234560";

        User user = new User();
        user.Id = 1;
        user.EmailAddress = emailAddress;
        user.Password = password;

        Session session = new Session();
        session.Id = 1;
        session.Token = "token";
        session.User = user;

        var autenticationMock = new Mock<ILoginService>(MockBehavior.Strict);

        autenticationMock.Setup(r => r.Login(emailAddress, password)).Returns(session);

        var controller = new LoginController(autenticationMock.Object);

        var result = controller.LoginAsync(emailAddress, password);
        var okResult = result.Result as OkObjectResult;
        var sessionDTO = okResult.Value as SessionDTO;

        autenticationMock.VerifyAll();

        Assert.IsTrue(((SessionDTO)sessionDTO).Token == "token");
    }

}