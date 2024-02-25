
namespace JwtAuthHandler.Models
{
    public class AuthenticateResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public int ExpiredIn { get; set; }
    }
}
