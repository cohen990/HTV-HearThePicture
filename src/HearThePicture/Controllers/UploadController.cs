using System.Threading.Tasks;
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

			_service.Upload(image);
			return RedirectToAction("Success");
		}

	    public ActionResult Success()
	    {
		    return View();
	    }
    }
}