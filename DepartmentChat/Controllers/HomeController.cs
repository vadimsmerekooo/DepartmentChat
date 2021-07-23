using DepartmentChat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DepartmentChat.Areas.Identity.Data;
using System.Linq;

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
        public IActionResult Index()
        {
            string user = _userManager.GetUserId(User);
            Chats chat = new Chats()
            {
                Messages = new List<Messages>()
                {
                    new Messages()
                    {
                        User = user,
                        ContentMessage = new ContentMessages()
                        {
                            Content = "aasldkaklsjdlkjldskflaksjdkfj!!K!KLlkaskldjaslkjdK!KJ!@!@J#!J@#JKL@!JlkJFas"
                        }
                    },
                    new Messages()
                    {
                        User = user,
                        ContentMessage = new ContentMessages()
                        {
                            Content = "231512"
                        }
                    }
                },
                Users = new List<string>() { user }
            };

            _context.Chats.Add(chat);
            _context.SaveChanges();


            return View();
        }
    }
}
