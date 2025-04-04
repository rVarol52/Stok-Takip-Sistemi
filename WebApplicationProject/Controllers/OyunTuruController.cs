using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationProject.Models;
using WebApplicationProject.Utility;

namespace WebApplicationProject.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class OyunTuruController : Controller
    {

        private readonly IOyunTuruRepository _oyunTuruRepository;

        public OyunTuruController(IOyunTuruRepository context) 
        {
            _oyunTuruRepository = context;
        }
        public IActionResult Index()
        {
            List<OyunTuru> objOyunTuruList = _oyunTuruRepository.GetAll().ToList();
            return View(objOyunTuruList);
        }

        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(OyunTuru oyunTuru)
        {
            if(ModelState.IsValid) 
            {
                _oyunTuruRepository.Ekle(oyunTuru);
                _oyunTuruRepository.Kaydet(); //SaveChanges() yapmazsan veriler db'ye kaydolmaz 
                TempData["basarili"] = "Yeni Oyun Türü başarıyla oluşturuldu!";
                return RedirectToAction("Index", "OyunTuru");
            }
            return View();
        }

        public IActionResult Guncelle(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            OyunTuru? oyunTuruDb = _oyunTuruRepository.Get(u => u.Id == id);
            if (oyunTuruDb == null) { return NotFound(); }

            return View(oyunTuruDb);
        }
        [HttpPost]
        public IActionResult Guncelle(OyunTuru oyunTuru)
        {
            if (ModelState.IsValid)
            {
                _oyunTuruRepository.Guncelle(oyunTuru);
                _oyunTuruRepository.Kaydet(); //SaveChanges() yapmazsan veriler db'ye kaydolmaz 
                TempData["basarili"] = "Yeni Oyun Türü başarıyla güncellendi!";
                return RedirectToAction("Index", "OyunTuru");
            }
            return View();
        }
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            OyunTuru? oyunTuruDb = _oyunTuruRepository.Get(u => u.Id == id);
            if (oyunTuruDb == null) { return NotFound(); }

            return View(oyunTuruDb);
        }
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            OyunTuru? oyunTuru = _oyunTuruRepository.Get(u => u.Id == id);
            if (oyunTuru == null) 
            {
                return NotFound();
            }
            _oyunTuruRepository.Sil(oyunTuru);
            _oyunTuruRepository.Kaydet();
            TempData["basarili"] = "Kayıt Silme işlemi başarılı!";
            return RedirectToAction("Index", "OyunTuru");

        }
    }
}
