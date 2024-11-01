﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class SalesController : Controller
    {
        private readonly IService<Satis> _service;
        private readonly IService<Arac> _serviceArac;
        private readonly IService<Musteri> _serviceMusteri;




        public SalesController(IService<Satis> service, IService<Arac> serviceArac, IService<Musteri> serviceMusteri)
        {
            _service = service;
            _serviceArac = serviceArac;
            _serviceMusteri = serviceMusteri;


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

  
        public async Task<ActionResult> Create()
        {
            ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
            ViewBag.MusteriId = new SelectList(await _serviceMusteri.GetAllAsync(), "Id", "Adi");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Satis satis)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(satis);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata oluştu!");
                }
            }

            ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
            ViewBag.MusteriId = new SelectList(await _serviceMusteri.GetAllAsync(), "Id", "Adi");
            return View(satis);
        }

        // GET: SalesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
            ViewBag.MusteriId = new SelectList(await _serviceMusteri.GetAllAsync(), "Id", "Adi");
            return View(model);
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Satis satis)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     _service.Update(satis);
                    await _service.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata oluştu!");
                }
            }

            ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
            ViewBag.MusteriId = new SelectList(await _serviceMusteri.GetAllAsync(), "Id", "Adi");
            return View(satis);
        }

        // GET: SalesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);

        }

        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Satis satis)
        {
            try
            {
               _service.Delete(satis);
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
