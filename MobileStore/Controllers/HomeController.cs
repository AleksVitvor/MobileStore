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

namespace MobileStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        MobileContext db;
        public HomeController(MobileContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Phones.ToList());
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
            db.Orders.Add(order);
            db.SaveChanges();
            var orderAdd = db.Orders.Where(u => u==order).FirstOrDefault();
            var user = db.Users.Where(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            user.Orders.Add(orderAdd);
            db.SaveChanges();
            return new RedirectResult("/Home/Index");
        }
        [HttpPost]
        public RedirectResult Exit()
        {
            return new RedirectResult("/Account/Logout");
        }
    }
}
