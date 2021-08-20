using System;
using DepartmentChat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DepartmentChat.Areas.Identity.Data;
using System.Linq;
using System.Threading.Tasks;
using DepartmentChat.Services;
using Microsoft.EntityFrameworkCore;

namespace DepartmentChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager<DepartmentUser> _userManager;
        ChatContext _context;

        public HomeController(UserManager<DepartmentUser> userManager, ChatContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("")]
        [Route("chats")]
        public async Task<IActionResult> Index()
        {
            AspNetUsers user = await _context.AspNetUsers
                .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserAsync(User).Result.Id);
            await _context.Entry(user).Collection(c => c.Chats).LoadAsync();


            List<Chats> chats = await _context.Chats.Include(u => u.AspNetUsers)
                .Where(cu => cu.AspNetUsers.Any(u => u.Id == user.Id) && cu.AspNetUsers.Count >= 2)
                .Include(x => x.Groups)
                .Include(x => x.Messages)
                .ThenInclude(m => m.ContentMessage)
                .OrderByDescending(s => s.Messages.Max(x => x.Created))
                .ToListAsync();

            ChatsUserViewModel viewModel = new ChatsUserViewModel()
            { 
                Chats = chats,
                UsersList = await _context.AspNetUsers.Where(u => u.Id != user.Id).ToListAsync(),
                UserView = user,
                ViewModel = chats.Count == 0 ? null : chats.First()
            };

            return View(viewModel);
        }

        // [Route("chats/{chatCode:int}")]
        public async Task<IActionResult> Chat(int chatCode)
        {
            
            AspNetUsers user = await _context.AspNetUsers
                .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserAsync(User).Result.Id);
            await _context.Entry(user).Collection(c => c.Chats).LoadAsync();
            
            List<Chats> chats = await _context.Chats.Include(u => u.AspNetUsers)
                .Where(cu => cu.AspNetUsers.Any(u => u.Id == user.Id) && cu.AspNetUsers.Count >= 2)
                .Include(x => x.Groups)
                .Include(x => x.Messages)
                .ThenInclude(m => m.ContentMessage)
                .OrderByDescending(s => s.Messages.Max(x => x.Created))
                .ToListAsync();

            
            Chats chat;
            
            if (user.Chats.All(u => u.Url != chatCode))
            {
                return PartialView("MainViewPartial/_ChatNotFoundPartial");
            }
            chat = user.Chats.FirstOrDefault(u => u.Url == chatCode);
            
            if(chat is null)
                return PartialView("MainViewPartial/_ChatNotFoundPartial");
            
            ChatsUserViewModel viewModel = new ChatsUserViewModel()
            {
                Chats = chats,
                UsersList = await _context.AspNetUsers.Where(netUser => netUser.Id != user.Id).ToListAsync(),
                UserView = user,
                ViewModel = chat
            };

            return PartialView("MainViewPartial/_ChatPartial", viewModel);
        }
        
        
        [Route("chats/connect/{userChatCode:int}")]
        public async Task<IActionResult> ChatConnect(int userChatCode)
        {
            AspNetUsers user = await _context.AspNetUsers
                .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserAsync(User).Result.Id);
            AspNetUsers userConnect = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.UserChatId == userChatCode);

            if (_context.Chats.Any(x => x.AspNetUsers.Contains(user) && x.AspNetUsers.Contains(userConnect) && x.TypeChat == TypeChat.Direct))
            {
                Chats chat = await _context.Chats.FirstOrDefaultAsync(x =>
                    x.AspNetUsers.Contains(user) && x.AspNetUsers.Contains(userConnect) &&
                    x.TypeChat == TypeChat.Direct);
                
                return RedirectToAction(nameof(Chat), new { chatCode = chat.Url });
            }
            else
            {
                Chats chat = new Chats()
                {
                    Url = new Random().Next(int.MaxValue),
                    AspNetUsers = new List<AspNetUsers>() {userConnect, user}
                };
                await _context.Chats.AddAsync(chat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Chat), new { chatCode = chat.Url });
            }

        }

        public async Task<PartialViewResult> SidebarChatUpdate()
        {
            AspNetUsers user = await _context.AspNetUsers
                .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserAsync(User).Result.Id);


            List<Chats> chats = await _context.Chats.Include(u => u.AspNetUsers)
                .Where(cu => cu.AspNetUsers.Any(u => u.Id == user.Id) && cu.AspNetUsers.Count >= 2)
                .Include(x => x.Groups)
                .Include(x => x.Messages)
                .ThenInclude(m => m.ContentMessage)
                .OrderByDescending(s => s.Messages.Max(x => x.Created))
                .ToListAsync();

            ChatsUserViewModel viewModel = new ChatsUserViewModel()
            { 
                Chats = chats,
                UsersList = await _context.AspNetUsers.Where(u => u.Id != user.Id).ToListAsync(),
                UserView = user,
                ViewModel = chats.Count == 0 ? null : chats.First()
            };
            return PartialView("MainViewPartial/_SidebarChatsPartial", viewModel);
        }
    }
}