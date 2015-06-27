using System.Web.Mvc;
using HearThePicture.Services;

namespace HearThePicture.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var converter = new ImageToSoundConverter();

			converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\Blue.bmp");

			return View();
		}

		public ActionResult Blue()
		{
			var converter = new ImageToSoundConverter();

			converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\Blue.bmp");

			return View();
		}

		public ActionResult Red()
		{
			var converter = new ImageToSoundConverter();

			converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\SinglePixel.bmp");

			return View();
		}

		public ActionResult TwoxOneGreen()
		{
			var converter = new ImageToSoundConverter();

			converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\2x1Green.bmp");

			return View();
		}

		public ActionResult TwoxTwoPurple()
		{
			var converter = new ImageToSoundConverter();

			converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\2x2Purple.bmp");

			return View();
		}

		public ActionResult Windows()
		{
			var converter = new ImageToSoundConverter();

			converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\windows.bmp");

			return View("Index");
		}

		public ActionResult Psychadelic()
		{
			var converter = new ImageToSoundConverter();

			converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\psychadelic.bmp", 11025);

			return View();
		}

		public ActionResult Scream()
		{
			var converter = new ImageToSoundConverter();

			converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\scream.jpg", 11025);

			return View();
		}
	}
}