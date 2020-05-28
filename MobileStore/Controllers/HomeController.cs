using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileStore.Models;
using MobileStore.Patterns;

namespace MobileStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        MobileContext db;
        UnitOfWork unitOfWork;
        public HomeController(MobileContext context)
        {
            db = context;
            unitOfWork = new UnitOfWork(db);
        }
        public IActionResult Index()
        {
            return View(unitOfWork.Mobiles.GetItemsList());
        }
        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.PhoneId = id;
            return View();
        }
        [HttpPost]
        public RedirectResult Buy(Order order)
        {
            unitOfWork.Orders.Create(order);
            unitOfWork.Save();
            var orderAdd = db.Orders.Where(u => u==order).FirstOrDefault();
            var user = db.Users.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            unitOfWork.Orders.Update(orderAdd);
            unitOfWork.Save();
            return new RedirectResult("/Home/Index");
        }
        [HttpPost]
        public RedirectResult Exit()
        {
            return new RedirectResult("/Account/Logout");
        }
    }
}
