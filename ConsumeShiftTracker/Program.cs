// See https://aka.ms/new-console-template for more information
using System.Threading.Tasks;
using System.Net.Http.Headers;
class Program
{
    static async Task Main(string[] args)
    {
        MainMenu mainMenu = new();
        await mainMenu.Menu();
    }
    //private static readonly HttpClient client = new HttpClient();
    //static async Task Main(string[] args)
    //{
    //    await ProcessGet();
    //}

    //private static async Task ProcessGet()
    //{
    //    client.DefaultRequestHeaders.Accept.Clear();
    //    client.DefaultRequestHeaders.Accept.Add(
    //        new MediaTypeWithQualityHeaderValue("application/json"));

    //    var stringTask = client.GetStringAsync("https://localhost:7029/api/Shifts");

    //    var msg = await stringTask;
    //    Console.Write(msg);
    //}
}
