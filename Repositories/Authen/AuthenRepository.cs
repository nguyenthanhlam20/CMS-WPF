
namespace Repositories.Authen
{
    public class AuthenRepository : IAuthenRepository
    {
        public async Task<bool> Login(string username, string password)
        {
            return true;
        }
    }
}
