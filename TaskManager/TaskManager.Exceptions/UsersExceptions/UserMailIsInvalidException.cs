using System;
namespace TaskManager.Exceptions.UsersExceptions
{
    public class UserMailIsInvalidException : UserException
    {
        public UserMailIsInvalidException(string mensaje) : base(mensaje)
        {

        }
    }
}

