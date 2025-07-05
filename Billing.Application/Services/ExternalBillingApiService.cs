using Billing.Domain.Configurations;
using Billing.Domain.DTOs;
using Billing.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

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

    public async Task<ExternalBillingDto> CreateExternalBillingAsync(ExternalBillingDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync(_apiUrl, dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExternalBillingDto>();
    }
    public async Task<ExternalBillingDto> GetExternalBillingByIdAsync(Guid id)
    {
        var response = await _httpClient.GetFromJsonAsync<ExternalBillingDto>($"{_apiUrl}/{id}");
        return response ?? throw new KeyNotFoundException("External billing not found");
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
    public async Task<bool> UpdateExternalBillingAsync(Guid id, ExternalBillingDto dto)
    {
        try
        {
            var jsonContent = JsonSerializer.Serialize(dto);
            var content = new StringContent(jsonContent);

            var response = await _httpClient.PutAsync($"{_apiUrl}/{id}", content);

            response.EnsureSuccessStatusCode();

            return true;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }

    }
    public async Task<bool> DeleteExternalBillingAsync(Guid id)
    {
        try
        {
            var jsonContent = JsonSerializer.Serialize(id);
            var content = new StringContent(jsonContent);

            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");

            response.EnsureSuccessStatusCode();

            return true;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
    }
}
