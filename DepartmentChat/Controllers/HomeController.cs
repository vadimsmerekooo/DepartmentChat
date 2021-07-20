using DepartmentChat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using DepartmentChat.Services;
using Microsoft.AspNetCore.Authorization;

namespace DepartmentChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("chats")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
