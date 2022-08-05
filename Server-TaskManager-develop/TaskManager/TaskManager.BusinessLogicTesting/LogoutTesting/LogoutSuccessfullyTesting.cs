using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.BusinessLogic.Services.Authentication;
using TaskManager.IDataAccess;
using Moq;

namespace TaskManager.BusinessLogicTesting.LogoutTesting;

[TestClass]
public class LogoutSuccessfullyTesting
{

    private Mock<IAuthenticationDataAccess> AuthenticationMock { get; set; }

    private ILogoutService LogoutService { get; set; }

    private User User { get; set; }

    [TestInitialize]
    public void TestSetup()
    {
        this.User = new User();
        this.User.Id = 1;
        this.User.EmailAddress = "martin@gmail.com";
        this.User.Password = "password1";

        Session session = new Session();
        session.Id = 1;
        session.Token = "asdasda";
        session.User = this.User;

        this.AuthenticationMock = new Mock<IAuthenticationDataAccess>(MockBehavior.Strict);

        this.LogoutService = new LogoutService(this.AuthenticationMock.Object);
    }

    [TestMethod]
    public void LogoutSuccessfulTestOne()
    {
        string token = "kdjsnkjdjkdfs";

        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.UserIsLogged(token)).Returns(true);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.Logout(token)).Verifiable();

        this.LogoutService.Logout(token);

        this.AuthenticationMock.VerifyAll();
    }

    [TestMethod]
    public void LogoutSuccesfulTestTwo()
    {
        string token = "kdjsnkjdjkdfsdskjjdks";

        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.UserIsLogged(token)).Returns(true);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.Logout(token)).Verifiable();

        this.LogoutService.Logout(token);

        this.AuthenticationMock.VerifyAll();
    }

}