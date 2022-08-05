using System;
namespace TaskManager.Exceptions.UsersExceptions
{
    public class UserPasswordIsInvalidException : UserException
    {
        public UserPasswordIsInvalidException(string mensaje) : base(mensaje)
        {

        }
    }
}

