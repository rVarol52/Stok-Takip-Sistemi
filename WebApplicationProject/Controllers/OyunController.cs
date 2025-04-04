using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationProject.Models;
using WebApplicationProject.Utility;

namespace WebApplicationProject.Controllers
{
    
    public class OyunController : Controller
    {

        private readonly IOyunRepository _oyunRepository;
        private readonly IOyunTuruRepository _oyunTuruRepository;

        public OyunController(IOyunRepository oyunRepository, IOyunTuruRepository oyunTuruRepository)
        {
            _oyunRepository = oyunRepository;
            _oyunTuruRepository = oyunTuruRepository;
        }
        [Authorize(Roles = "Admin, Musteri")]
        public IActionResult Index()
        {
            List<Oyun> objOyunList = _oyunRepository.GetAll(includeProps:"OyunTuru").ToList();
            return View(objOyunList);
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Ekle()
        {
            IEnumerable<SelectListItem> OyunTuruList = _oyunTuruRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Ad,
                Value = k.Id.ToString()
            });

            ViewBag.OyunTuruList=OyunTuruList;

            return View();
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Ekle(Oyun oyun)
        {
            if(ModelState.IsValid) 
            {
                _oyunRepository.Ekle(oyun);
                _oyunRepository.Kaydet(); //SaveChanges() yapmazsan veriler db'ye kaydolmaz 
                TempData["basarili"] = "Yeni Oyun başarıyla oluşturuldu!";
                return RedirectToAction("Index", "Oyun");
            }
            return View();
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Guncelle(int? id)
        {
            IEnumerable<SelectListItem> OyunTuruList = _oyunTuruRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Ad,
                Value = k.Id.ToString()
            });
            ViewBag.OyunTuruList = OyunTuruList;
            if (id == null || id==0)
            {
                return NotFound();
            }
            Oyun? oyunDb = _oyunRepository.Get(u => u.Id == id);
            if (oyunDb == null) { return NotFound(); }

            return View(oyunDb);
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Guncelle(Oyun oyun)
        {
            if (ModelState.IsValid)
            {
                _oyunRepository.Guncelle(oyun);
                _oyunRepository.Kaydet(); //SaveChanges() yapmazsan veriler db'ye kaydolmaz 
                TempData["basarili"] = "Yeni Oyun başarıyla güncellendi!";
                return RedirectToAction("Index", "Oyun");
            }
            return View();
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Oyun? oyunDb = _oyunRepository.Get(u => u.Id == id);
            if (oyunDb == null) { return NotFound(); }

            return View(oyunDb);
        }
        [HttpPost, ActionName("Sil")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult SilPOST(int? id)
        {
            Oyun? oyun = _oyunRepository.Get(u => u.Id == id);
            if (oyun == null) 
            {
                return NotFound();
            }
            _oyunRepository.Sil(oyun);
            _oyunRepository.Kaydet();
            TempData["basarili"] = "Kayıt Silme işlemi başarılı!";
            return RedirectToAction("Index", "Oyun");

        }
    }
}
