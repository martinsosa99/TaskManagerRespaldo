namespace TaskManager.Exceptions.AuthenticationExceptions;

public class UserIsNotLoggedException : SystemAuthenticationException
{

    public UserIsNotLoggedException(string mensaje) : base(mensaje)
    {
    }

}

