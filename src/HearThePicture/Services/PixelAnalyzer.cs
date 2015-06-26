using System;
using System.Drawing;

namespace HearThePicture.Services
{
	public class PixelAnalyzer
	{
		private const float MinFrequency = 80;
		private const float MaxFrequency = 1200;

		public double GetFrequency(Color pixel)
		{
			float hue = pixel.GetHue();

			var freq = ConvertToFrequency(hue);

			return freq;
		}

		public double ConvertToFrequency(float hue)
		{
			var min = Math.Log10(MinFrequency);
			var max = Math.Log10(MaxFrequency);

			var temp = (hue/360.0)*(max - min) + min;

			double frequency = Math.Pow(10, temp);

			return frequency;
		}
	}
}