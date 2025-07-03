using Billing.Domain.Configurations;
using Billing.Domain.DTOs;
using Billing.Domain.Entities;
using Billing.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Billing.Application.Services;

public class ExternalBillingApiService : IExternalBillingInformationApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public ExternalBillingApiService(HttpClient httpClient, IOptions<ExternalApiConfig> config)
    {
        _httpClient = httpClient;
        _apiUrl = config.Value.BillingApi;
    }

    public async Task<List<ExternalBillingDto>> GetAllExternalBillingInformationAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<List<ExternalBillingDto>>(_apiUrl);

            return response ?? new List<ExternalBillingDto>();
        }
        catch (HttpRequestException ex)
        {

            throw new ApplicationException("Failed to access external API.", ex);
        }
    }
}
