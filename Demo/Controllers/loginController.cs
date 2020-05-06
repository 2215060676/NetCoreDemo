using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Model;
using Demo.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.Controllers
{
    public class loginController : Controller
    {
        //用于记录日志
        private readonly ILogger<loginController> _logger;

        //UserManager处理用户相关逻辑 如添加删除 修改密码 添加角色等
        public readonly UserManager<IdentityUser> UserManager;
        //SignInManager 处理注册登录 相关业务逻辑
        public readonly SignInManager<IdentityUser> Sinninmanager;

        public loginController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<loginController> logger
            )
        {
            UserManager = userManager;
            Sinninmanager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(loginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await Sinninmanager.PasswordSignInAsync(model.UserName, model.PassWord, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("login in {0}", model.UserName);
                    return RedirectToAction("index", "Result");
                }
            }
            _logger.LogInformation("login in {0}", model.UserName);
            ModelState.AddModelError("", "用户名或者密码错误");
            return View(model);
        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var user = new IdentityUser
                {
                    UserName = model.UserName
                    
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                //user 可以修改？不可以，创建的用户带上密码，否则，注册的密码存储不了
                //result 实际为 Identity 类弄，该类代表了创建用户的结果，其中 Succeeded 代表了操作成功
                //Erroes 属性 包含了IdentityErroes对象集合 ，IdentityErroes对象集合描述了错误信息 
                if (result.Succeeded)
                {
                    _logger.LogInformation("logged in {userName}.", model.UserName);
                    return RedirectToAction("index", "login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            var username = HttpContext.User.Identity.Name;
            await Sinninmanager.SignOutAsync();
            _logger.LogInformation("logged in {userName}.", username);
            return RedirectToAction("index", "login");
        }
    }
}