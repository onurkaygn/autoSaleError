using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.WebUI.Utils;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy =  "AdminPolicy")]
    public class CarsController : Controller
    {
        #region DIs
        private readonly IService<Arac> _service;
        private readonly IService<Marka> _serviceMarka;


        public CarsController(IService<Arac> service, IService<Marka> serviceMarka)
        {
            _service = service;
            _serviceMarka = serviceMarka;
        }

        #endregion
        public ActionResult Index()
        {
            var model = _service.GetAll();
            return View(model);
        }

      
        public ActionResult Details(int id)
        {
            return View();
        }

       
        public  async Task<ActionResult> CreateAsync()
        {
            ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id", "Adi");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Arac arac, IFormFile? Resim1, IFormFile? Resim2, IFormFile? Resim3)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    arac.Resim1 = await FileHelper.FileLoaderAsync(Resim1, "/Img/Cars/");
                    arac.Resim2 = await FileHelper.FileLoaderAsync(Resim2, filePath:"/Img/Cars/");
                    arac.Resim3 = await FileHelper.FileLoaderAsync(Resim3, filePath: "/Img/Cars/");
                    await _service.AddAsync(arac);
                    await _service.SaveAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata oluştu!");
                }
            }
            ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id", "Adi");

            return View(arac);
        }

     
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id", "Adi");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Arac arac, IFormFile? Resim1, IFormFile? Resim2, IFormFile? Resim3)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Resim1 is not null) { 
                    arac.Resim1 = await FileHelper.FileLoaderAsync(Resim1);

                    }
                    if (Resim2 is not null)
                    {
                        arac.Resim2 = await FileHelper.FileLoaderAsync(Resim2);

                    }
                    if (Resim3 is not null)
                    {
                        arac.Resim3 = await FileHelper.FileLoaderAsync(Resim3);

                    }
                   
                    _service.Update(arac);
                    await _service.SaveAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata oluştu!");
                }
            }
            ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id", "Adi");
            return View(arac);
        }

       
        public async Task<ActionResult> DeleteAsync(int id)
        {

            var model = await _service.FindAsync(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Arac arac)
        {
            try
            {
                _service.Delete(arac);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
