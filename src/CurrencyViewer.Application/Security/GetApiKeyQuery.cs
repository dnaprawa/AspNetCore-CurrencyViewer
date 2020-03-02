using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Security
{
    public class GetApiKeyQuery : IGetApiKeyQuery
    {
        const string DefaultAPIKey = "5908D47C-85D3-4024-8C2B-6EC9464398AD";
        public Task<ApiKey> Execute(string providedApiKey)
        {
            //NOTE: this is only for testing
            //API keys should be retrieved from Database and there should be also a API key revocation facility.

            ApiKey apiKey = null;
            if(providedApiKey == DefaultAPIKey)
            {
                apiKey = new ApiKey(1, "Authorized User", DefaultAPIKey, DateTime.UtcNow, new List<string>()
                {
                    Roles.Readonly
                });

                return Task.FromResult(apiKey);
            }

            return Task.FromResult(apiKey);
        }
    }
}
