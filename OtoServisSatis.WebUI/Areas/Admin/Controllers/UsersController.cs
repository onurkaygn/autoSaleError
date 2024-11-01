using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class UsersController : Controller
    {
        #region DIs
        private readonly IService<Kullanici> _service;
        private readonly IService<Rol> _serviceRol;


        public UsersController(IService<Kullanici> service, IService<Rol> serviceRol)
        {
            _service = service;
            _serviceRol = serviceRol;
        }
        #endregion

        #region Index and Details
        public async Task<ActionResult> Index()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }


        public ActionResult Details(int id)
        {
            return View();
        }
        #endregion

        #region Create

        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.RolId = new SelectList(await _serviceRol.GetAllAsync(), "Id", "Adi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(kullanici);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ViewBag.RolId = new SelectList(await _serviceRol.GetAllAsync(), "Id", "Adi");
            return View(kullanici);
        }
        #endregion

        #region Edit
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.RolId = new SelectList(await _serviceRol.GetAllAsync(), "Id", "Adi");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Kullanici kullanici)
        {

            if (ModelState.IsValid)
            {
                try
                {
                     _service.Update(kullanici);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ViewBag.RolId = new SelectList(await _serviceRol.GetAllAsync(), "Id", "Adi");
            return View(kullanici);
        }
        #endregion

        #region Delete
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Kullanici kullanici)
        {
            try
            {
                _service.Delete(kullanici);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
