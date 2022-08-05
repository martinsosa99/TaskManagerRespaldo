namespace TaskManager.Exceptions.AuthenticationExceptions;

public class SystemAuthenticationException : Exception
{

    public SystemAuthenticationException(string mensaje) : base(mensaje)
    {
    }

}

