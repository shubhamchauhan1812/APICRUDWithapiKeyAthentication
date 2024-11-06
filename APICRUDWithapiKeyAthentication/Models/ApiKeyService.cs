using System.Security.Cryptography;

namespace APICRUDWithapiKeyAthentication.Models
{
    public class ApiKeyService
    {
        private readonly ApplicationContext _context;

        //public ApiKeyService(global::Castle.Core.Configuration.IConfiguration configuration, ApplicationContext context)
        //{
        //    _context = context;
        //}

        public string GenerateApiKey(string userId)
        {
            var apiKey = new tbl_ApiKey
            {
                ApiKey = GenerateUniqueKey(),
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                IsRevoked = false
            };

            _context.tbl_ApiKey.Add(apiKey);
            _context.SaveChanges();

            return apiKey.ApiKey;
        }

        private string GenerateUniqueKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var byteArray = new byte[32];
                rng.GetBytes(byteArray);
                return Convert.ToBase64String(byteArray);
            }
        }

        public bool ValidateApiKey(string apiKey)
        {
            var key = _context.tbl_ApiKey.FirstOrDefault(k => k.ApiKey == apiKey && !k.IsRevoked);
            return key != null;
        }
    }
}
