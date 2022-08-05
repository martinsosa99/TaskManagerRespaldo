using TaskManager.Domain;

namespace TaskManager.IBusinessLogic;

public interface ILoginService
{

    public Session Login(string email, string password);

    public bool UserIsLogged(string usersToken);

}