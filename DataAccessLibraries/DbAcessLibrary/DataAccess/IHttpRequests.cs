using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public interface IHttpRequests
    {
        Task<T> GetJsonAsync<T>(string url);
    }
}