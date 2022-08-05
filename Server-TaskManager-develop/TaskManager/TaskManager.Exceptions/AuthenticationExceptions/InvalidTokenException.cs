namespace TaskManager.Exceptions.AuthenticationExceptions
{

    public class InvalidTokenException : SystemAuthenticationException
    {

        public InvalidTokenException(string mensaje) : base(mensaje)
        {

        }
    }

}

