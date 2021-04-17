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
        // GET: ChatsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ChatsController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            return View(await _chatService.GetChat(id));
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
            if(chat == null)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChatsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
