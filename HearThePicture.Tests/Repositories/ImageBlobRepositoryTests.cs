using System.IO;
using System.Linq;
using System.Reflection;
using HearThePicture.Repositories;
using NUnit.Framework;

namespace HearThePicture.Tests.Repositories
{
	[TestFixture]
	public class ImageBlobRepositoryTests
	{
		[Test]
		public void Initialize_DoesntThrow()
		{
			var repo = new ImageBlobRepository(false);

			Assert.DoesNotThrow(repo.Initialize);
		}

		[Test]
		public void Initialize_SetsContainer()
		{
			var repo = new ImageBlobRepository(false);

			repo.Initialize();

			// This will break if the name of the 'Container' property on the ImageBlobRepository is changed. Beware :P
			var container = typeof(ImageBlobRepository).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).Single(x => x.Name == "Container");

			var repoContainer = container.GetValue(repo);

			Assert.That(repoContainer, Is.Not.Null);
		}

		[Test]
		public void Store_DoesntThrow()
		{
			FileStream stream = File.Open("Assets/2x2Purple.bmp", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

			var repo = new ImageBlobRepository();

			Assert.DoesNotThrow(()=>repo.Store(stream));
		}
	}
}
