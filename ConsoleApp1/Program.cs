namespace ConsoleApp1
{
    using System.Threading.Tasks;
    using ClassLibrary1;

    class Program
    {
        static async Task Main(string[] args)
        {
            using var export = new Export("output.xml");
            await export.Write();
        }
    }
}
