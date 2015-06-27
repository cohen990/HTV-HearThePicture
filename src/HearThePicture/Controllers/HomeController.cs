using System.Web.Mvc;
using HearThePicture.Services;

namespace HearThePicture.Controllers
{
	public class HomeController : Controller
	{
		private ImageToSoundConverter _converter;

		public ActionResult Index()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\Blue.bmp");

			return View();
		}

		public ActionResult Blue()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\Blue.bmp");

			return View();
		}

		public ActionResult Red()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\SinglePixel.bmp");

			return View();
		}

		public ActionResult TwoxOneGreen()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\2x1Green.bmp");

			return View();
		}

		public ActionResult TwoxTwoPurple()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\2x2Purple.bmp");

			return View();
		}

		public ActionResult Windows()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\windows.bmp");

			return View("Index");
		}

		public ActionResult Psychadelic()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\psychadelic.bmp", 11025);

			return View();
		}

		public ActionResult Scream()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\scream.jpg", 11025);

			return View();
		}

		public ActionResult Whitenoise()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\whitenoise.jpg", 11025);

			return View();
		}

		public ActionResult StarryNight()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\starrynight.jpg", 11025);

			return View();
		}

		public ActionResult Sunflower()
		{
			_converter = new ImageToSoundConverter();

			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\sunflower.jpg", 11025);

			return View();
		}
	}
}