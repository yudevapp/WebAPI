using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.Contracts;
using WebAPI.Data;

namespace WebAPI.Services
{
    public class ApiKeyValidatorService(HotelListingDbContext db) : IApiKeyValidatorService
    {
        public async Task<bool> IsValidKeyAsync(string apiKey, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) return false;

            var apiKeyEntity = await db.APIKeys
               .AsNoTracking()
               .FirstOrDefaultAsync(k => k.Key == apiKey, ct);

            if (apiKeyEntity is null) return false;

            return apiKeyEntity.IsActive;

        } 
    }
}
