namespace TaskManager.Exceptions.AuthenticationExceptions
{

    public class UserIsLoggedException : SystemAuthenticationException
    {

        public UserIsLoggedException(string mensaje) : base(mensaje)
        {
        }

    }

}