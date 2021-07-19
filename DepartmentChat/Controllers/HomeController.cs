using DepartmentChat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using DepartmentChat.Services;

namespace DepartmentChat.Controllers
{
    public class HomeController : Controller
    {
        IHubContext<ChatHub> hubContext;
        public HomeController(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string product)
        {
            await hubContext.Clients.All.SendAsync("Notify", $"Добавлено: {product} - {DateTime.Now.ToShortTimeString()}");
            return RedirectToAction("Index");
        }
    }
}
