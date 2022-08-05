using System;
namespace TaskManager.Exceptions.UsersExceptions
{
    public class UserNotAllowedToProceedException : UserException
    {
        public UserNotAllowedToProceedException(string mensaje) : base(mensaje)
        {

        }
    }
}

