using System.Threading.Tasks;

namespace CurrencyViewer.Application.Security
{
    public interface IGetApiKeyQuery
    {
        Task<ApiKey> Execute(string providedApiKey);
    }
}
