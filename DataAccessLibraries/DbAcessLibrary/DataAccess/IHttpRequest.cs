using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public interface IHttpRequest
    {
        Task<T> GetJsonAsync<T>(string url);
    }
}