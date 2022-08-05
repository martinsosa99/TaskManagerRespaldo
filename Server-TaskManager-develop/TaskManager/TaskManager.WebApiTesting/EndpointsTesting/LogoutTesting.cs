using TaskManager.IBusinessLogic;
using TaskManager.WebApi.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.WebApiTesting.EndpointsTesting;

[TestClass]
public class LogoutTesting
{

    [TestMethod]
    public void LogoutTest()
    {
        string token = "tokensito";

        var autenticationMock = new Mock<ILogoutService>(MockBehavior.Strict);

        autenticationMock.Setup(r => r.Logout(token)).Verifiable();

        var controller = new LogoutController(autenticationMock.Object);

        var result = controller.LogoutAsync(token);
        var okResult = result.Result as OkResult;

        autenticationMock.VerifyAll();

        Assert.IsInstanceOfType(okResult, typeof(OkResult));
    }

}