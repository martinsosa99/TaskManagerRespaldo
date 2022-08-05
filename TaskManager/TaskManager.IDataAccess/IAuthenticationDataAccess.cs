using TaskManager.Domain;

namespace TaskManager.IDataAccess;

public interface IAuthenticationDataAccess
{

    public Session Login(User userWhoWantsToLogin, string token);

    public void Logout(string usersToken);

    public List<Session> Sessions();

    public bool UserIsLogged(User user);

    public bool UserIsLogged(string usersToken);

    public User GetUserByToken(string usersToken);

}