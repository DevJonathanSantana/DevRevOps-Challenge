using REVOPS.DevChallenge.Clients.Models;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
namespace REVOPS.DevChallenge.Clients;

public interface ITalkClient
{
    Task<Chat?> GetChatAsync(string chatId);
    Task<ContactDetail?> GetContactDetailAsync(string contactId);
    
    Task<List<AgentDetail>> GetOrganizationMembersAsync();

    Task<List<ChatMessageItem>> GetMessagesAsync(string chatId);
}

public class TalkClient : ITalkClient
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<TalkClient> _logger;
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public TalkClient(HttpClient httpClient, ILogger<TalkClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger; 
    }

    public async Task<Chat?> GetChatAsync(string chatId)
    {
        var orgId = "YT-h9PtKxKdBtkrR"; 
        var url = $"v1/chats/{chatId}?organizationId={orgId}";

        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<Chat>(_options);
    }

    public async Task<ContactDetail?> GetContactDetailAsync(string contactId)
    {
        try
        {
            var orgId = "YT-h9PtKxKdBtkrR";
            var url = $"v1/contacts/{contactId}?organizationId={orgId}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<ContactDetail>(_options);
        }
        catch { return null; }
    }

    
    public async Task<List<AgentDetail>> GetOrganizationMembersAsync()
    {
        try
        {
            var orgId = "YT-h9PtKxKdBtkrR";
            
        
            var url = $"v1/organizations/{orgId}"; 

            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode) 
            {
                _logger.LogError("Falha ao buscar organização. URL: {Url} | Status: {Status}", url, response.StatusCode);
                
                return new List<AgentDetail>();
            }

        
            var orgData = await response.Content.ReadFromJsonAsync<OrganizationResponse>(_options);
            
            return orgData?.Members ?? new List<AgentDetail>();
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex, "Erro técnico ao buscar membros da organização.");
            return new List<AgentDetail>();
        }
    }

    public async Task<List<ChatMessageItem>> GetMessagesAsync(string chatId)
    {
        try
        {
            var orgId = "YT-h9PtKxKdBtkrR";
            
            var url = $"v1/chats/{chatId}/messages?organizationId={orgId}&take=100&sortBy=createdAt&descending=true"; 

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return new List<ChatMessageItem>();

            var data = await response.Content.ReadFromJsonAsync<ChatMessageResponse>(_options);
            return data?.Items ?? new List<ChatMessageItem>();
        }
        catch { return new List<ChatMessageItem>(); }
    }
    }



public class ChatMessageResponse
{
    [System.Text.Json.Serialization.JsonPropertyName("items")]
    public List<ChatMessageItem>? Items { get; set; }
}