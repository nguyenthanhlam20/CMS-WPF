using Repositories.Authen;

namespace Services.Authen
{
    public class AuthenService : IAuthenService
    {
        private readonly IAuthenRepository _repository;

        public AuthenService()
        {
            _repository = new AuthenRepository();
        }

        public async Task<bool> Login(string username, string password) => await _repository.Login(username, password);
    }
}
