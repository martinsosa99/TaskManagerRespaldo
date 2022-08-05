namespace TaskManager.Exceptions.AuthenticationExceptions
{
    public class PasswordMatchesUserRealPasswordException : SystemAuthenticationException
    {
        public PasswordMatchesUserRealPasswordException(string mensaje) : base(mensaje)
        {

        }

    }

}

