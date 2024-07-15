namespace Repositories.Authen
{
    public interface IAuthenRepository
    {
        Task<bool> Login(string username, string password);
    }
}
