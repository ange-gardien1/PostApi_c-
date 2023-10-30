using tweeter_api.Models;

namespace tweeter_api.Repositories;

public interface IAuthService 
{
    User CreateUser(User user);
    string SignIn(string username, string password);
    User? GetUserById(int id);
    IEnumerable<User> SearchUsers(string text);
}
