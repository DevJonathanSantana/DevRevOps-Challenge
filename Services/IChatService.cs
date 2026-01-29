using REVOPS.DevChallenge.Context.Entities;

namespace REVOPS.DevChallenge.Services
{
    public interface IChatService
    {
        // Apenas a assinatura do método. Sem chaves { }, sem código.
        Task<ChatInfo?> BuscarEProcessarChat(string chatId);

        Task<List<ChatInfo>> ObterHistoricoAsync();
    }
}