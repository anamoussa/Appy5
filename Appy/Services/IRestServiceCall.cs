using Appy.Models;

namespace Appy.Services;

public interface IRestServiceCall
{
    Task<UserBasicInfo> Login(string username, string password, string phoneId);
    Task<SearchResult> Search(string search, string phoneId);
    Task<object> Translate(string local);
}
