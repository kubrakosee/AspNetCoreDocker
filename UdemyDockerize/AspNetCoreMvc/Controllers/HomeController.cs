﻿using AspNetCoreMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //burda metod oluşturalım
        public IActionResult ImageSave()
        {
            return View();
        }
        //resim ekle dediğinde basılacak olan metodu da yazalım tabiki text olacağı için async bir şekilde yapabiliriz.Task da ne döneceksek biz ıactionresult döceğiz
        [HttpPost]
        public async Task<IActionResult> ImageSave(IFormFile imageFile)
        {//if diyoruz imageFile geliyor mu gelmiyor mu kontrol edelim
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                //rastgee üretsin bu yüzden guid diyorum.
                //Path.GetExtension dediğimde sadece uzantısını al diyoruz
                //burdaki şu şekilde gelicez örnek
                //resim1.jpg sadece jpg kısmı alıcak.

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
