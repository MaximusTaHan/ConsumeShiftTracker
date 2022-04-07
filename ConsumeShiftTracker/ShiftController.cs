// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ConsumeShiftTracker.Models;

internal class ShiftController
{
    private static readonly HttpClient client = new HttpClient();
    internal static async Task GetShifts()
    {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var stringTask = client.GetStringAsync("https://localhost:7029/api/Shifts");

        var msg = await stringTask;
        Console.Write(msg);
    }

    internal static async Task CreateShift(DateTime dateTimeStart, DateTime dateTimeEnd, decimal minutes, decimal pay)
    {
        Shift shift = new()
        {
            shiftId = 0,
            start = dateTimeStart,
            end = dateTimeEnd,
            minutes = minutes,
            pay = pay,
            location = ""
        };

        var shiftJson = JsonSerializer.Serialize(shift);
        var data = new StringContent(shiftJson, Encoding.UTF8, "application/json");
        var url = "https://localhost:7029/api/Shifts";

        using var client = new HttpClient();
        var response = await client.PostAsync(url, data);

    }
}