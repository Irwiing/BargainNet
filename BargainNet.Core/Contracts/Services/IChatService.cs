using BargainNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Services
{
    public interface IChatService
    {
        Task<Guid> CreateChat(string ownerId, Guid auctionId);
        Task<Chat> GetChat(Guid id);
        Task SaveMessage(Guid idChat, string message, string userId);
        Task EndChat(Guid id);
        Task GiveRate(Guid chatId, string userId, int rate);
        Task<Chat> GetChatByAuctionId(Guid auctionId);
    }
}
