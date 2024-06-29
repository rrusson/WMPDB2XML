using System.Threading.Tasks;
using MediaInfo;

namespace ConsoleApp1
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            using var export = new Export("output.xml");
            await export.Write();
        }
    }
}
