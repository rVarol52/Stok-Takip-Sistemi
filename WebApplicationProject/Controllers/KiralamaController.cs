using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml;
using WebApplicationProject.Models;
using WebApplicationProject.Utility;

namespace WebApplicationProject.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KiralamaController : Controller
    {

        private readonly IKiralamaRepository _kiralamaRepository;
        private readonly IOyunRepository _oyunRepository;

        public KiralamaController(IKiralamaRepository kiralamaRepository, IOyunRepository oyunRepository)
        {
            _kiralamaRepository = kiralamaRepository;
            _oyunRepository = oyunRepository;
        }
        public IActionResult Index()
        {
            List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps:"Oyun").ToList();
            return View(objKiralamaList);
        }

        public IActionResult Ekle()
        {
            
            IEnumerable<SelectListItem> OyunList = _oyunRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.OyunAdi,
                Value = k.Id.ToString()

            });

            ViewBag.OyunList=OyunList;

            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Kiralama kiralama)
        {
            if(ModelState.IsValid) 
            {
                kiralama.KiraBaslangic = DateTime.Now;
                kiralama.KiraBitis = DateTime.Now.AddDays(7);
                _kiralamaRepository.Ekle(kiralama);
                _kiralamaRepository.Kaydet(); //SaveChanges() yapmazsan veriler db'ye kaydolmaz 
                TempData["basarili"] = "Yeni Kiralama işlemi başarıyla oluşturuldu!";
                return RedirectToAction("Index", "Kiralama");
            }
            return View();
        }

        public IActionResult Guncelle(int? id)
        {

            IEnumerable<SelectListItem> OyunList = _oyunRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.OyunAdi,
                Value = k.Id.ToString()
            });
            ViewBag.OyunList = OyunList;
            if (id == null || id==0)
            {
                return NotFound();
            }
            Kiralama? KiralamaDb = _kiralamaRepository.Get(u => u.Id == id);
            if (KiralamaDb == null) { return NotFound(); }

            return View(KiralamaDb);
        }
        [HttpPost]
        public IActionResult Guncelle(Kiralama kiralama)
        {
            if (ModelState.IsValid)
            {
                
                _kiralamaRepository.Guncelle(kiralama);
                _kiralamaRepository.Kaydet(); //SaveChanges() yapmazsan veriler db'ye kaydolmaz 
                TempData["basarili"] = "Yeni Kiralama işlemi başarıyla güncellendi!";
                return RedirectToAction("Index", "Kiralama");
            }
            return View();
        }
        public IActionResult Sil(int? id)
        {

            IEnumerable<SelectListItem> OyunList = _oyunRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.OyunAdi,
                Value = k.Id.ToString()
            });
            ViewBag.OyunList = OyunList;
            if (id == null || id==0)
            {
                return NotFound();
            }
            Kiralama? KiralamaDb = _kiralamaRepository.Get(u => u.Id == id);
            if (KiralamaDb == null) { return NotFound(); }

            return View(KiralamaDb);
        }
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
            if (kiralama == null) 
            {
                return NotFound();
            }
            _kiralamaRepository.Sil(kiralama);
            _kiralamaRepository.Kaydet();
            TempData["basarili"] = "Kayıt Silme işlemi başarılı!";
            return RedirectToAction("Index", "Kiralama");

        }
    }
}
