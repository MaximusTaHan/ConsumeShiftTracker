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
}
