using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HearThePicture.Services;

namespace HearThePicture.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var converter = new ImageToSoundConverter();

			converter.Convert("Assets/OneRedPixel.bmp");

			return View();
		}
	}
}