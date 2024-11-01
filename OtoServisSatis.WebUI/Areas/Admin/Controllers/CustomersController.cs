using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]

    public class CustomersController : Controller
    {
        private readonly IService<Musteri> _service;
        private readonly IService<Arac> _serviceArac;



        public CustomersController(IService<Musteri> service, IService<Arac> serviceArac)
        {
            _service = service;
            _serviceArac = serviceArac;
        }


        public ActionResult Index()
        {
            var model = _service.GetAll();
            return View(model);
        }

    
        public ActionResult Details(int id)
        {
            return View();
        }

   
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");

            return View();
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Musteri musteri)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(musteri);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                   ModelState.AddModelError("","Hata oluştu!");
                }
            }
                 
            ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
            return View();
        }

      
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
            return View(model);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     _service.Update(musteri);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata oluştu!");
                }
            }

            ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
            return View();
        }

        // GET: CustomersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Musteri musteri)
        {
            try
            {
                _service.Delete(musteri);
                 _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                return View();
            }
        }
    }
}
