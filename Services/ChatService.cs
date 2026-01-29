using Microsoft.EntityFrameworkCore;
using REVOPS.DevChallenge.Clients;
using REVOPS.DevChallenge.Context;
using REVOPS.DevChallenge.Context.Entities;

namespace REVOPS.DevChallenge.Services
{
    public class ChatService : IChatService
    {

        public async Task<List<ChatInfo>> ObterHistoricoAsync()
        {
            // Retorna os últimos 10 pesquisados, do mais recente para o antigo
            return await _context.ChatInfos
                .OrderByDescending(c => c.Id) 
                .Take(10)
                .ToListAsync();
        }
        private readonly ITalkClient _talkClient;
        private readonly ChallengeContext _context;

        public ChatService(ITalkClient talkClient, ChallengeContext context)
        {
            _talkClient = talkClient;
            _context = context;
        }

        public async Task<ChatInfo?> BuscarEProcessarChat(string chatId)
        {
            var chatApi = await _talkClient.GetChatAsync(chatId);
            if (chatApi == null) return null;

            
            string? agentName = null;
            
            var memberId = chatApi.OrganizationMember?.Id ?? chatApi.LastOrganizationMember?.Id;

            

            if (!string.IsNullOrEmpty(memberId))
            {
                var todosMembros = await _talkClient.GetOrganizationMembersAsync();

                

                
                if (todosMembros.Count > 0)
                {
                
                }

                // Busca pelo ID
                var agente = todosMembros.FirstOrDefault(a => a.Id == memberId);

                agentName = agente?.NomeFinal;
                
            
            }

        
            string emailFinal = "Sem Email";
            if (chatApi.Contact != null && !string.IsNullOrEmpty(chatApi.Contact.Id))
            {
                var contatoCompleto = await _talkClient.GetContactDetailAsync(chatApi.Contact.Id);
                if (contatoCompleto != null)
                {
                    emailFinal = !string.IsNullOrEmpty(contatoCompleto.Email) 
                                ? contatoCompleto.Email 
                                : contatoCompleto.Emails?.FirstOrDefault() ?? "Sem Email";
                }
            }

        
            var tagsString = "Sem etiquetas";
            if (chatApi.Contact?.Tags != null || chatApi.Tags != null)
            {
                var tagsList = (chatApi.Contact?.Tags?.Select(t => t.Name) ?? Enumerable.Empty<string>())
                        .Concat(chatApi.Tags?.Select(t => t.Name) ?? Enumerable.Empty<string>())
                        .Where(t => !string.IsNullOrEmpty(t))
                        .Distinct();
                if (tagsList.Any()) tagsString = string.Join(", ", tagsList);
            }
            var setor = chatApi.Sector?.Name ?? "Sem Setor";

            DateTime ultimaAtividade = chatApi.CreatedAt.DateTime;
            bool dataEncontrada = false;

        
            var tiposReais = new[] { "Text", "Image", "Audio", "Video", "Document" };

            
            if (chatApi.LastMessage != null)
            {
                bool ehTipoReal = !string.IsNullOrEmpty(chatApi.LastMessage.MessageType) && 
                                tiposReais.Contains(chatApi.LastMessage.MessageType, StringComparer.OrdinalIgnoreCase);
                
                // Ignora se a fonte for explicitamente "System" (notas internas)
                bool ehSistema = string.Equals(chatApi.LastMessage.Source, "System", StringComparison.OrdinalIgnoreCase);

                if (ehTipoReal && !ehSistema)
                {
                    ultimaAtividade = chatApi.LastMessage.CreatedAt.DateTime;
                    dataEncontrada = true;
                }
            }

            
            if (!dataEncontrada)
            {
                var messages = await _talkClient.GetMessagesAsync(chatId);
                if (messages.Any())
                {
                    var lastRealMessage = messages
                        .Where(m => 
                            !string.IsNullOrEmpty(m.MessageType) &&
                            tiposReais.Contains(m.MessageType, StringComparer.OrdinalIgnoreCase) &&
                            !string.Equals(m.Source, "System", StringComparison.OrdinalIgnoreCase)
                        )
                        .OrderByDescending(m => m.CreatedAt)
                        .FirstOrDefault();

                    if (lastRealMessage != null)
                    {
                        ultimaAtividade = lastRealMessage.CreatedAt.DateTime;
                    }
                }
            }

            var chatInfo = new ChatInfo
            {
                ChatId = chatApi.Id,
                IsAnyAttendantAssigned = !string.IsNullOrEmpty(chatApi.OrganizationMember?.Id),
                ContactName = chatApi.Contact?.Name ?? "Sem Nome",
                ContactEmail = emailFinal,
                CreatedAt = chatApi.CreatedAt.DateTime,
                Tags = tagsString,
                Sector = setor,
                AgentName = agentName,
                LastActivityAt = ultimaAtividade
            };

            bool jaExiste = await _context.ChatInfos.AnyAsync(c => c.ChatId == chatInfo.ChatId);
            if (!jaExiste)
            {
                await _context.ChatInfos.AddAsync(chatInfo);
                await _context.SaveChangesAsync();
            }
            
            return chatInfo;
        }
    }
}