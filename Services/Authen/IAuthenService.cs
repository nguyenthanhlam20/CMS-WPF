namespace Services.Authen
{
    public interface IAuthenService
    {
        Task<bool> Login(string username, string password);
    }
}
