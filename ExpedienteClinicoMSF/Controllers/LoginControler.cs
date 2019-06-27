using ExpedienteClinicoMSF.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace ExpedienteClinicoMSF.Controllers
{
  
        public class LoginController : Controller
        {
            UserDataAccessLayer objUser = new UserDataAccessLayer();
          
      
        [HttpGet]
            public IActionResult UserLogin()
            {
            return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> UserLogin([Bind] Usuarios user)
            {
               // ModelState.Remove("FirstName");
              //  ModelState.Remove("LastName");
              // ModelState.Aggregate("Rol.Rol");

                if (ModelState.IsValid)
                {
                    string LoginStatus = objUser.ValidateLogin(user);

                    if (LoginStatus == "Success")
                    {
                      
                        var claims = new List<Claim>

                        {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role,objUser.RoleUsers(user.Email))


                        };

                    
                        ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.SignInAsync(principal);
                        return RedirectToAction("Index", "Home");
                        

                    }
                    else
                    {
                        TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
                        return View();
                    }
                }
                else
                    return View();

            }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
    
}
