using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class BrandsController : Controller
    {
        #region DIs
        private readonly IService<Marka> _service;

        public BrandsController(IService<Marka> service)
        {
            _service = service;
        }

        #endregion

        public async Task<ActionResult> Index()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

      
        public ActionResult Details(int id)
        {
            return View();
        }

     
        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Marka marka)
        {
            try
            {
                await _service.AddAsync(marka);
                await _service.SaveAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Hata oluştu!");
            }
            return View(marka);

        }


        public async Task<ActionResult> Edit(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Marka marka)
        {
            try
            {
                 _service.Update(marka);
                await _service.SaveAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Hata oluştu!");
            }
            return View(marka);
        }

        #region Delete

        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model =  await _service.FindAsync(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Marka marka)
        {
            try
            {
                _service.Delete(marka);
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
