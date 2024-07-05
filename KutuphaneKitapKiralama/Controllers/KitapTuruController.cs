using KutuphaneKitapKiralama.Models;
using KutuphaneKitapKiralama.Utility;
using Microsoft.AspNetCore.Mvc;

namespace KutuphaneKitapKiralama.Controllers
{
    public class KitapTuruController : Controller
    {
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public KitapTuruController(IKitapTuruRepository context)
        {
            _kitapTuruRepository = context;
        }
        public IActionResult Index() //kitap turlerinin listesini icerir
        {
            List<KitapTuru> objKitapTuruList = _kitapTuruRepository.GetAll().ToList();
            return View(objKitapTuruList);

        }
        public IActionResult Ekle () 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru) 
        {                                             
            if (ModelState.IsValid) //modelde verdigimiz ornekler calisiyorsa 
            {
                _kitapTuruRepository.Ekle(kitapTuru);
                _kitapTuruRepository.Kaydet();
                TempData["basarili"] = "Kayit ekleme islemi basarili!";
                return RedirectToAction("Index");
            }
           return View();

        }
        public IActionResult Guncelle(int? id) //burada guncellenecek olan kitabin idsini belirtmek icin parametre gonderdik
        {
            if(id== null || id== 0) // id null veya 0 gelirse noufound penceresi acilir
            {
                return NotFound();
            }
            KitapTuru? kitapTuruVt = _kitapTuruRepository.Get(u=>u.Id==id); //bizim gonderdigimiz id'yi veritabanindan bulur ve getirir 
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }

        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru) //Ekle.cshtml'dden gelen post metodu karsilar ve girilen degeri add ve savechange ile veritabanina aktarir
        {                                              //return'u index actionuna yaparak kitap turlerinin listesini yeni ekledigi degerle gosterir.
            if (ModelState.IsValid) //modelde verdigimiz ornekler calisiyorsa 
            {
                _kitapTuruRepository.Guncelle(kitapTuru);
                _kitapTuruRepository.Kaydet();
                TempData["basarili"] = "Kayit guncelleme islemi basarili!";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }
            KitapTuru? kitapTuruVt = _kitapTuruRepository.Get(u=>u.Id==id); 
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }

        [HttpPost]
        [ActionName("Sil")]
        public IActionResult SilPost(int? id) 
        {
            KitapTuru? kitapTuru = _kitapTuruRepository.Get(u => u.Id == id);
            if (kitapTuru == null)
            {
                return NotFound();
            }
            _kitapTuruRepository.Sil(kitapTuru);
            _kitapTuruRepository.Kaydet();
            TempData["basarili"] = "Kayit silme islemi basarili!";
            return RedirectToAction("Index");

        }
    }
}
