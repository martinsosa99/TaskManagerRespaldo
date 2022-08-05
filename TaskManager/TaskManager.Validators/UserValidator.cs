using System.Text.RegularExpressions;
using TaskManager.Domain;
using TaskManager.Exceptions.UsersExceptions;

namespace TaskManager.Validators;

public class UserValidator
{

    public void ValidateUser(User user)
    {
        this.ValidateIfUserIsNull(user);
        this.ValidateEmailAddress(user.EmailAddress);
        this.ValidatePassword(user.Password);
    }

    private void ValidateIfUserIsNull(User user)
    {
        if (user == null)
            throw new UserIsNullException("User can not be empty.");
    }

    private void ValidateEmailAddress(string usersEmailAddress)
    {
        string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        if (String.IsNullOrWhiteSpace(usersEmailAddress) || !Regex.IsMatch(usersEmailAddress, pattern))
            throw new UserMailIsInvalidException("User's mail is invalid.");
    }

    private void ValidatePassword(string usersPassword)
    {
        if (String.IsNullOrWhiteSpace(usersPassword))
            throw new UserPasswordIsInvalidException("Users's password can't be empty.");
    }

}