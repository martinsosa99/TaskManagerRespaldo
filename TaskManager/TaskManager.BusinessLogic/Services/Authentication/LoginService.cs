using TaskManager.IDataAccess;
using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.Exceptions.AuthenticationExceptions;
using TaskManager.Exceptions.UsersExceptions;

namespace TaskManager.BusinessLogic.Services.Authentication;

public class LoginService : ILoginService
{

    public IAuthenticationDataAccess AuthenticationDataAccess { get; set; }

    public IUserDataAccess UserDataAccess { get; set; }

    private const int EMPTY = 0;

    public LoginService(IAuthenticationDataAccess authenticationDataAccess,
        IUserDataAccess userDataAccess)
    {
        this.AuthenticationDataAccess = authenticationDataAccess;
        this.UserDataAccess = userDataAccess;
    }

    public Session Login(string email, string password)
    {
        User userThatWantsToLogin = this.UserThatWantsToLogin(email, password);

        Guid guid = Guid.NewGuid();
        string token = guid.ToString().Trim();

        Session sessionToReturn = this.AuthenticationDataAccess.Login(userThatWantsToLogin, token);

        return sessionToReturn;
    }

    public bool UserIsLogged(string usersToken)
    {
        this.ValidateToken(usersToken);

        return this.AuthenticationDataAccess.UserIsLogged(usersToken);
    }

    private User UserThatWantsToLogin(string email, string password)
    {
        User userThatWantsToLogin = this.UserDataAccess.GetUserByEmail(email);

        if (userThatWantsToLogin != null)
            this.ValidateThatPasswordMatchesUserRealPassword(userThatWantsToLogin.Password,
                password);
        else
            throw new UserIsNullException("User doesn't exists.");

        return userThatWantsToLogin;
    }

    private void ValidateThatPasswordMatchesUserRealPassword(string userPasswordOnDataBase,
        string passwordFromLogin)
    {
        if (userPasswordOnDataBase != passwordFromLogin)
            throw new PasswordMatchesUserRealPasswordException("Password doesn't match user's password.");
    }

    private void ValidateToken(string usersToken)
    {
        if (usersToken == null || usersToken.Trim().Length == EMPTY)
            throw new InvalidTokenException("Token is not valid.");
    }

}