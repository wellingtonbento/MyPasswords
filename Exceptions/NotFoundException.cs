namespace MyPasswords.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException() : base("Entity with Id Not found") { }
    }
}
