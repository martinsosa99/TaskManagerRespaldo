using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.BusinessLogic.Services.Authentication;
using TaskManager.IDataAccess;
using Moq;
using TaskManager.Exceptions.AuthenticationExceptions;

namespace TaskManager.BusinessLogicTesting.LogoutTesting;

[TestClass]
public class LogoutUnsuccessfullyTesting
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

    [ExpectedException(typeof(InvalidTokenException))]
    [TestMethod]
    public void UnsuccesfulLogoutBecauseTokenIsNotValidTestOne()
    {
        string token = "";

        this.LogoutService.Logout(token);
    }

    [ExpectedException(typeof(InvalidTokenException))]
    [TestMethod]
    public void UnsuccesfulLogoutBecauseTokenIsNotValidTestTwo()
    {
        string token = null;

        this.LogoutService.Logout(token);
    }

    [ExpectedException(typeof(UserIsNotLoggedException))]
    [TestMethod]
    public void UnsuccesfulLogoutBecauseUserIsNotLoggedOrDoesntExistTest()
    {
        string token = "fhjdhjfdhjd";

        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.UserIsLogged(token)).Returns(false);

        this.LogoutService.Logout(token);
    }

}