using System.Threading.Tasks;

using MediaInfo;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MediaInfoTests
{
	[TestClass]
	public class MediaInfoTests
	{
		[TestMethod]
		public async Task ExportTest()
		{
			using var export = new Export("output.xml");
			await export.Write();
		}
	}
}
