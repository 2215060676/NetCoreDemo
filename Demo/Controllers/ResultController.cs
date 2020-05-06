using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Demo.Model;
using Demo.Repository;
using Demo.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Controllers
{
   // [Authorize]
    public class ResultController : Controller
    {

        private static string filepath;
        private  IResultRepository ResultRepository;
        private  IResultTypeRepository ResultTypeRepository;
        public ResultController(
           IResultRepository _resultRepository,
           IResultTypeRepository _resultTypeRepository
           )
        {
            ResultRepository = _resultRepository;
            ResultTypeRepository = _resultTypeRepository;
        }
        public IActionResult Index(int pageindex = 1, int pagesize = 5)
        {
         
            var results = ResultRepository.PageList(pageindex, pagesize, out int pagrcount);
            ViewBag.pagrcount = pagrcount;
            ViewBag.pageindex = pageindex;
            return View(results);
        }
        public async Task<IActionResult> Add()
        {
            var types = await ResultTypeRepository.typeListAsync();
            ViewBag.types = types.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.ResultTypeid.ToString()
            });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ResultModel model, [FromServices]IHostingEnvironment env)
        {
            //判断传过来的数据是否合法
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string filename = string.Empty;
            if (model.Attachment != null)
            {
                //拼接起来
                filename = Path.Combine("file", Guid.NewGuid().ToString() + Path.GetExtension(model.Attachment.FileName));
                //var stream = new FileStream(Path.Combine(env.WebRootPath,filename), FileMode.CreateNew);
                using (var stream = new FileStream(Path.Combine(env.WebRootPath, filename), FileMode.CreateNew))
                {
                    model.Attachment.CopyTo(stream);
                }
            }
            await ResultRepository.AddAsync(new Result
            {
                strName = model.strName,
                Title = model.Title,
                Create = DateTime.Now,
                Discription = model.Discription,
                TypeId = model.TypeId,
                Password = model.Password,
                Attachment = filename


            });
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var result = await ResultRepository.GetBIdAsync(id);
            if (string.IsNullOrEmpty(result.Password))
            {
                return View();
            }
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Details(int id, string password)
        {
            var result = await ResultRepository.GetBIdAsync(id);
            {
                if (!result.Password.Equals(password))
                {
                    return BadRequest("密码错误");
                }
                else
                {
                    return View(result);
                }
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var types = await ResultTypeRepository.typeListAsync();
            ViewBag.Types = types.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.ResultTypeid.ToString()
            });

            Result result = await ResultRepository.GetBIdAsync(id);
            filepath = result.Attachment;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromServices] IHostingEnvironment env, ResultModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string filename = filepath;
            if (model.Attachment != null)
            {
                filename = Path.Combine("file", Guid.NewGuid().ToString() + Path.GetExtension(model.Attachment.FileName));
                using (var stream = new FileStream(Path.Combine(env.WebRootPath, filename), FileMode.CreateNew))
                {
                    model.Attachment.CopyTo(stream);
                }
            }
            Result r = new Result()
            {
                id = model.id,
                Title = model.Title,
                strName = model.strName,
                Discription = model.Discription,
                TypeId = model.TypeId,
                Create = DateTime.Now,
                Password = model.Password,
                Attachment = filename,
            };
            await ResultRepository.updateAsync(r);
            return RedirectToAction("index");

        }


        public async Task<IActionResult> Delect(int id)
        {
            Result result = await ResultRepository.GetBIdAsync(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delect(int id, string name)
        {
            Result result = await ResultRepository.GetBIdAsync(id);
            await ResultRepository.DeleteAsync(result);
            return RedirectToAction("index");
        }
    }
}