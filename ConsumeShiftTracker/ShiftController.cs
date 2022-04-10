// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsumeShiftTracker.Models;
using Newtonsoft.Json;

internal class ShiftController
{
    internal static async Task<List<Shift>> GetShifts()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = client.GetAsync("https://localhost:7029/api/Shifts").Result;

        if(response.IsSuccessStatusCode)
        {
            var resp = response.Content.ReadAsStringAsync().Result;
            List<Shift> shifts = JsonConvert.DeserializeObject<List<Shift>>(resp);
            return shifts;
        }
        else
        {
            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        }

        return null;
    }

    internal static async Task CreateShift(Shift shift)
    {

        using var client = new HttpClient();
        var response = client.PostAsJsonAsync("https://localhost:7029/api/Shifts", shift).Result;

        if (response.IsSuccessStatusCode)
            Console.WriteLine("Success");
        else
            Console.WriteLine("Error");

    }

    internal static async Task DeleteShift(int num)
    {
        using var client = new HttpClient();
        var response = client.DeleteAsync($"https://localhost:7029/api/Shifts/{num}").Result;
        if (response.IsSuccessStatusCode)
            Console.WriteLine("Success");

        else
            Console.WriteLine("Error");
    }

    internal static async Task UpdateShift(Shift updateShiftDTO)
    {
        using var client = new HttpClient();
        var response = client.PutAsJsonAsync($"https://localhost:7029/api/Shifts/{updateShiftDTO.shiftId}", updateShiftDTO).Result;
        if (response.IsSuccessStatusCode)
            Console.WriteLine("Success");

        else
            Console.WriteLine("Error");
    }
}