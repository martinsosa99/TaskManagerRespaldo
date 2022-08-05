using TaskManager.IDataAccess;
using TaskManager.IBusinessLogic;
using TaskManager.Exceptions.AuthenticationExceptions;

namespace TaskManager.BusinessLogic.Services.Authentication;

public class LogoutService : ILogoutService
{

    public IAuthenticationDataAccess AuthenticationDataAccess { get; set; }

    private const int EMPTY = 0;

    public LogoutService(IAuthenticationDataAccess authenticationDataAccess)
    {
        this.AuthenticationDataAccess = authenticationDataAccess;
    }

    public void Logout(string usersToken)
    {
        this.ValidateToken(usersToken);

        if (!this.AuthenticationDataAccess.UserIsLogged(usersToken))
            throw new UserIsNotLoggedException("If you want to logout, first, you need to be logged.");

        this.AuthenticationDataAccess.Logout(usersToken);
    }

    private void ValidateToken(string usersToken)
    {
        if (usersToken == null || usersToken.Trim().Length == EMPTY)
            throw new InvalidTokenException("Token is not valid.");
    }

}