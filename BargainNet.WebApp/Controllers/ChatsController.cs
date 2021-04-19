using BargainNet.Core.Contracts.Services;
using BargainNet.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BargainNet.WebApp.Controllers
{
    public class ChatsController : Controller
    {
        private readonly IChatService _chatService;
        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var chat = await _chatService.GetChat(id);
            if (chat != null && chat.Status == Status.Active)
            {
                if (userId == chat.AuctionOwner.Id || userId == chat.AuctionWinner.Id)
                {
                    return View(chat);
                }
            }
            return RedirectToAction("Details", "UserProfile");
        }

        // POST: ChatsController/Create
        public async Task<ActionResult> Create(string idOwner, Guid auctionId)
        {
            var idChat = await _chatService.CreateChat(idOwner, auctionId);
            return RedirectToAction(nameof(Details), new { id = idChat });
        }

        public async Task<ActionResult> GetMessages(Guid id)
        {
            var chat = await _chatService.GetChat(id);

            if (chat == null)
            {
                return View("_ChatMessages");
            }
            return View("_ChatMessages", chat.Messages);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid idChat, string message)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _chatService.SaveMessage(idChat, message, userId);
            return RedirectToAction(nameof(Details), new { id = idChat });
        }


        // GET: ChatsController/Delete/5
        public ActionResult Delete()
        {
            return View("_ConfirmModal");
        }

        // POST: ChatsController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _chatService.EndChat(id);
            return Ok();
        }

        public async Task<ActionResult> GetRate(Guid id)
        {
            ViewData["chatId"] = id;
            var chat = await _chatService.GetChat(id);
            if (chat.Status == Status.Inactive)
            {
                return View("_RateModal");
            }
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> GiveRate(Guid id, int rate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _chatService.GiveRate(id, userId, rate);
            return RedirectToAction("Details", "UserProfiles");
        }
    }
}
