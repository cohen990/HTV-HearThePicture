namespace HearThePicture.Models
{
	public class Tone
	{
		public const double MinimumDuration = 0.25;
		public const double MaximumDuration = 4;

		public double Frequency { get;set; }

		public double Duration { get; set; }
	}
}