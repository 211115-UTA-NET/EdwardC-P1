namespace Project1_Api.DataStorage
{
    public interface ILoginRepo
    {
        Task<List<string>> checkUsernamePassword(string username, string password);
    }
}