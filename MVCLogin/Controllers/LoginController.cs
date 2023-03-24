using MVCLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(MVCLogin.Models.Account account)
        {
            using (AccountDataEntities db = new AccountDataEntities())
            {
                var userDetails = db.Accounts.Where(x => x.username == account.username && x.password == account.password).FirstOrDefault();
                if (userDetails == null)
                {
                    account.LoginErrorMessage = "Wrong username or password";
                    return View("Index", account);
                }
                else
                {
                    Session["MaNV"] = userDetails.MaNV;
                    if (userDetails.permission == 0)
                        return RedirectToAction("Index", "Admin");
                    else
                        return RedirectToAction("Index", "Staff");
                }
            }
        }
    }
}