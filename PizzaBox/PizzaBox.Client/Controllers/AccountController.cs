using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Client.Models;
using PizzaBox.Domain.Models;
using PizzaBox.Storage.Repositories;

namespace PizzaBox.Client.Controllers
{
  public class AccountController : Controller
  {

    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Login(AccountViewModel account)
    {
      if(ModelState.IsValid)
      {

        if(account.CheckAccount(account.Username, account.Password))
        {
          
          account.user = account.GetUser(account);
          account = account.GetByName(account);
          ViewData["Username"] = account.Username;
          TempData["Un"] = account.Username;
          TempData["oid"] = "x";

          if(account.user.isStore)
            {

              return View("Store");
            }

          return View("User");
        }
      }
      return View("Login");
    }

    public IActionResult Logout()
    {
      return View();
    }
    
    [HttpPost]
    public IActionResult NewOrder()
    {

        return View(new OrderViewModel(TempData["Un"] as string, TempData["oid"] as string));
      
    }
  }
}