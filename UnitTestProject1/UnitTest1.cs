namespace UnitTestProject1
{
    using System.Threading.Tasks;
    using ClassLibrary1;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            using var export = new Export("output.xml");
            await export.Write();
        }
    }
}
