using System;

namespace HearThePicture.Services
{
	public class NoiseMaker
	{
		public void Play(double freq)
		{
			Console.Beep((int) Math.Round(freq), 1000);
		}
	}
}