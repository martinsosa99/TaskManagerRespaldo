using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.BusinessLogic.Services.Authentication;
using TaskManager.IDataAccess;
using Moq;

namespace TaskManager.BusinessLogicTesting.LoginTesting;

[TestClass]
public class LoginSuccessfullyTesting
{

    private Mock<IAuthenticationDataAccess> AuthenticationMock { get; set; }

    private Mock<IUserDataAccess> UserMock { get; set; }

    private ILoginService LoginService { get; set; }

    private User FirstUser { get; set; }

    [TestInitialize]
    public void TestSetup()
    {
        this.FirstUser = new User();
        this.FirstUser.Id = 1;
        this.FirstUser.EmailAddress = "Martin";
        this.FirstUser.Password = "Sosa";

        this.AuthenticationMock = new Mock<IAuthenticationDataAccess>(MockBehavior.Strict);
        this.UserMock = new Mock<IUserDataAccess>(MockBehavior.Strict);

        this.LoginService = new LoginService(this.AuthenticationMock.Object,
            this.UserMock.Object);
    }

    [TestMethod]
    public void LoginTestOne()
    {
        this.FirstUser.EmailAddress = "Martinuser@gmail.com";
        this.FirstUser.Password = "password!!";

        string email = this.FirstUser.EmailAddress;
        string password = this.FirstUser.Password;

        this.UserMock.Setup(userDataAccess => userDataAccess.GetUserByEmail(email)).Returns(this.FirstUser);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.UserIsLogged(this.FirstUser)).Returns(false);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.Login(this.FirstUser, It.IsAny<string>())).Returns(It.IsAny<Session>());

        this.LoginService.Login(email, password);

        this.UserMock.VerifyAll();
    }

    [TestMethod]
    public void LoginTestTwo()
    {
        this.FirstUser.EmailAddress = "Martinuser@gmail.com.uy";
        this.FirstUser.Password = "password!!!";

        string email = this.FirstUser.EmailAddress;
        string password = this.FirstUser.Password;

        this.UserMock.Setup(userDataAccess => userDataAccess.GetUserByEmail(email)).Returns(this.FirstUser);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.UserIsLogged(this.FirstUser)).Returns(false);
        this.AuthenticationMock.Setup(authenticationDataAccess => authenticationDataAccess.Login(this.FirstUser, It.IsAny<string>())).Returns(It.IsAny<Session>());

        this.LoginService.Login(email, password);

        this.UserMock.VerifyAll();
    }

}