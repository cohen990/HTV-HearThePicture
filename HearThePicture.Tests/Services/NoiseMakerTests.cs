using HearThePicture.Services;
using NUnit.Framework;

namespace HearThePicture.Tests.Services
{
	class NoiseMakerTests
	{
		public NoiseMaker NoiseMaker { get; set; }

		[SetUp]
		public void SetUp()
		{
			NoiseMaker = new NoiseMaker();
		}
	}

	[TestFixture]
	class PlayTests : NoiseMakerTests
	{
		[Test]
		[Ignore("Noisy")]
		public void GivenFreq80_PlaysTone()
		{
			var freq = 80.0;

			NoiseMaker.Play(freq);
		}

		[Test]
		[Ignore("Noisy")]
		public void GivenFreq200_PlaysTone()
		{
			var freq = 200.0;

			NoiseMaker.Play(freq);
		}

		[Test]
		[Ignore("Noisy")]
		public void GivenFreq1200_PlaysTone()
		{
			var freq = 1200.0;

			NoiseMaker.Play(freq);
		}
	}
}
