using TaskManager.Domain;

namespace TaskManager.IDataAccess;

public interface IUserDataAccess
{

    public void AddUser(User user);

    public User GetUserByEmail(string email);

    public List<User> Users();

}