namespace MyPasswords.Exceptions
{
    public class InvalidCredentialsException : AppException
    {
        public InvalidCredentialsException() : base("Email or Password is invalid") { }
    }
}
