using TaskManager.Domain;
using TaskManager.IDataAccess;

namespace TaskManager.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    private DataBaseContext DataBaseContext { get; set; }

    public UserDataAccess(DataBaseContext dataBaseContext)
    {
        this.DataBaseContext = dataBaseContext;
    }

    public void AddUser(User user)
    {
        this.DataBaseContext.Set<User>().Add(user);
        this.DataBaseContext.SaveChanges();
    }

    public User GetUserByEmail(string email)
    {
        List<User> systemUsers = this.Users();

        User userToReturn = null;

        for (int index = 0; index < systemUsers.Count; index++)
        {
            User systemUser = systemUsers[index];

            if (systemUser.EmailAddress == email)
            {
                userToReturn = systemUser;
                break;
            }
        }

        return userToReturn;
    }

    public List<User> Users()
    {
        return this.DataBaseContext.Set<User>().ToList<User>();
    }

}