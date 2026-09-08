using System.Security.Claims;

namespace MyPasswords.Services.ObtainUserLogged
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoggedUser(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public int Id
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?
                    .User.FindFirstValue(ClaimTypes.NameIdentifier);
                return int.TryParse(value, out var id) ? id : 0;
            }
        }
    }
}
