namespace WebAPI.Contracts
{
    public interface IApiKeyValidatorService
    {
        Task<bool> IsValidKeyAsync(string apiKey, CancellationToken ct = default);
    }
}