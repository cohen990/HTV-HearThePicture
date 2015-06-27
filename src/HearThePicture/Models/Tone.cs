namespace HearThePicture.Models
{
	public class Tone
	{
		public const double MinimumDuration = 0.25;
		public const double MaximumDuration = 4;
		public const double MinimumVolume = 0.5;
		public const double MaximumVolume = 1;

		public double Frequency { get;set; }

		public double Duration { get; set; }

		public double Volume { get; set; }
	}
}