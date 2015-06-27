using System;
using System.Drawing;
using HearThePicture.Models;

namespace HearThePicture.Services
{
	public class PixelAnalyzer
	{
		private const float MinFrequency = 80;
		private const float MaxFrequency = 1200;

		public Tone GetTone(Color pixel)
		{
			float hue = pixel.GetHue();
			float brightness = pixel.GetBrightness();

			var frequency = ConvertToFrequency(hue);
			var duration = ConvertToDuration(brightness);

			return new Tone {Frequency = frequency, Duration = duration};
		}

		public double ConvertToDuration(float brightness)
		{
			var min = Math.Log(Tone.MinimumDuration, 2);
			var max = Math.Log(Tone.MaximumDuration, 2);

			var logOfDuration = brightness*(max - min) + min;

			var duration = Math.Pow(2, logOfDuration);

			return duration;
		}

		public double ConvertToFrequency(float hue)
		{
			var min = Math.Log10(MinFrequency);
			var max = Math.Log10(MaxFrequency);

			var logOfFrequency = (hue/360.0)*(max - min) + min;

			double frequency = Math.Pow(10, logOfFrequency);

			return frequency;
		}
	}
}