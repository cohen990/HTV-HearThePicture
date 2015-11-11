using System;
using System.Web;
using System.Web.Mvc;
using HearThePicture.Services;

namespace HearThePicture.Controllers
{
    public class UploadController : Controller
	{
	    private ImageUploadService _service;

	    public UploadController()
	    {
		    _service = new ImageUploadService();
	    }

		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(HttpPostedFileBase image)
		{
			if (!ModelState.IsValid)
				return View();

			var uri = _service.Upload(image);
			return RedirectToAction("Play", new { uri = uri });
		}

	    public ActionResult Success()
	    {
		    return View();
	    }

	    public ActionResult Play(Uri uri)
	    {
		    return View("Play", uri );
	    }

        [HttpGet]
        public ActionResult Tsu()
        {
            return View();
        }
	}
}