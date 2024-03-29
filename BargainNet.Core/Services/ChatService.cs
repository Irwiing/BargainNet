﻿using BargainNet.Core.Contracts.Repositories;
using BargainNet.Core.Contracts.Services;
using BargainNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Services
{
    public class ChatService : IChatService
    {
        private readonly IUserService _userService;
        private readonly IAuctionService _auctionService;
        private readonly IChatRepository _chatRepository;
        public ChatService(IUserService userService,
            IAuctionService auctionService,
             IChatRepository chatRepository)
        {
            _userService = userService;
            _auctionService = auctionService;
            _chatRepository = chatRepository;
        }
        public async Task<Guid> CreateChat(string ownerId, Guid auctionId)
        {
            var owner = await _userService.GetUser(ownerId);
            var auction = await _auctionService.GetAuction(auctionId);
            var winner = _auctionService.GetWinner(auction);

            Chat newChat = new Chat()
            {
                Auction = auction,
                AuctionOwner = owner,
                AuctionWinner = winner,
            };

            await _chatRepository.AddAsync(newChat);
            return newChat.Id;
        }
        public async Task<Chat> GetChatByAuctionId(Guid auctionId)
        {
            return await _chatRepository.GetChatByAuctionId(auctionId);
            
        }
        public async Task<Chat> GetChat(Guid id)
        {
            return await _chatRepository.GetByIdAsync(id);
        }

        public async Task SaveMessage(Guid idChat, string message, string userId)
        {
            var chat = await _chatRepository.GetByIdAsync(idChat);
            var sender = await _userService.GetUser(userId);
            ChatMessage newMessage = new ChatMessage()
            {
                Message = message,
                Sender = sender
            };
            chat.Messages.Add(newMessage);
            await _chatRepository.UpdateAsync(chat);
        }

        public async Task EndChat(Guid id)
        {
            var chat = await _chatRepository.GetByIdAsync(id);
            chat.Status = Status.Inactive;
            await _chatRepository.UpdateAsync(chat);
        }
        public async Task GiveRate(Guid chatId, string userId, int rate)
        {
            var chat = await _chatRepository.GetByIdAsync(chatId);

            if (chat.AuctionOwner.Id == userId)
            {
                int totalPoints = chat.AuctionWinner.UserProfile.BarganhaPoints + rate;
                if (totalPoints <= 999)
                {
                    chat.AuctionWinner.UserProfile.BarganhaPoints = totalPoints;
                    await _userService.UpdateUser(chat.AuctionWinner);
                }
                else
                {
                    chat.AuctionWinner.UserProfile.BarganhaPoints = 999;
                    await _userService.UpdateUser(chat.AuctionWinner);
                }
            }
            else
            {
                int totalPoints = chat.AuctionOwner.UserProfile.BarganhaPoints + rate;
                if (totalPoints <= 999)
                {
                    chat.AuctionOwner.UserProfile.BarganhaPoints = totalPoints;
                    await _userService.UpdateUser(chat.AuctionOwner);
                }
                else
                {
                    chat.AuctionOwner.UserProfile.BarganhaPoints = 999;
                    await _userService.UpdateUser(chat.AuctionOwner);
                }
            }
        }
    }
}
