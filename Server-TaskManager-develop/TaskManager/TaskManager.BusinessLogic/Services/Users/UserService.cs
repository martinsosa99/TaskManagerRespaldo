using TaskManager.Domain;
using TaskManager.Exceptions.UsersExceptions;
using TaskManager.IBusinessLogic;
using TaskManager.IDataAccess;
using TaskManager.Validators;

namespace TaskManager.BusinessLogic.Services.Users;

public class UserService : IUserService
{

    public IUserDataAccess UserDataAccess { get; set; }

    public UserService(IUserDataAccess userDataAccess)
    {
        this.UserDataAccess = userDataAccess;
    }

    public void AddUser(User user)
    {
        this.ValidateUserAttributes(user);
        this.ValidateThatUserIsNotRepeated(user);

        this.UserDataAccess.AddUser(user);
    }

    private void ValidateUserAttributes(User user)
    {
        UserValidator userValidator = new UserValidator();
        userValidator.ValidateUser(user);
    }

    private void ValidateThatUserIsNotRepeated(User user)
    {
        List<User> systemUsers = this.UserDataAccess.Users();

        User userRepeated = systemUsers.FirstOrDefault(currentUser => currentUser.Equals(user));

        if (userRepeated != null)
            throw new UserIsRepeatedException("User is repeated.");
    }

}