using System.Web.Mvc;
using HearThePicture.Services;

namespace HearThePicture.Controllers
{
	public class HomeController : Controller
	{
		private readonly ImageToSoundConverter _converter;

		public HomeController()
		{
			_converter = new ImageToSoundConverter();
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Blue()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\Blue.bmp");

			return View();
		}

		public ActionResult Red()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\SinglePixel.bmp");

			return View();
		}

		public ActionResult TwoxOneGreen()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\2x1Green.bmp");

			return View();
		}

		public ActionResult TwoxTwoPurple()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\2x2Purple.bmp");

			return View();
		}

		public ActionResult Windows()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\windows.bmp");

			return View();
		}

		public ActionResult Row()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\row.png");

			return View();
		}

		public ActionResult Gradient()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\gradient.bmp", 689);

			return View();
		}

		public ActionResult Psychadelic()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\psychadelic.bmp", 11025);

			return View();
		}

		public ActionResult Scream()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\scream.jpg", 11025);

			return View();
		}

		public ActionResult Whitenoise()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\whitenoise.jpg", 11025);

			return View();
		}

		public ActionResult StarryNight()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\starrynightsmall.jpg", 11025);

			return View();
		}

		public ActionResult StarryNightNoSynth()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\starrynightsmall.jpg", 11025, false);

			return View();
		}

		public ActionResult Sunflower()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\sunflower.jpg", 11025);

			return View();
		}

		public ActionResult DogFace()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\DogFace.png", 11025);

			return View();
		}

		public ActionResult Pillars()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\pillars-small.png", 11025);

			return View();
		}

		public ActionResult RealStar()
		{
			_converter.Convert("E:\\Git\\HTV-HearThePicture\\src\\HearThePicture\\Assets\\realstar.png", 11025);

			return View();
		}
	}
}