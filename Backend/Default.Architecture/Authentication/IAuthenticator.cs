using System.Threading.Tasks;

namespace Default.Architecture.Authentication
{
    public interface IAuthenticator<T> where T : class
    {
        Task<string> Login(T identity);
    }
}
