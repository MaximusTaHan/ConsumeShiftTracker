// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

internal class MainMenu
{
    string pattern = @"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d))$";
    internal async Task Menu()
    {
        bool closeApp = false;
        while(closeApp == false)
        {
            Console.WriteLine("\n\nSHIFT TRACKER\n\n");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("Press 1 to view all Shifts");
            Console.WriteLine("Press 2 to create new Shift");
            Console.WriteLine("Press 3 to Update a Shift");
            Console.WriteLine("Press 4 to Delete a Shift");
            Console.WriteLine("Press 0 to Close the Application");

            string commandInput = Console.ReadLine();

            while (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                commandInput = Console.ReadLine();
            }

            switch (commandInput)
            {
                case "0":
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    await ProcessGet();
                    break;
                case "2":
                    await ProcessCreate();
                    break;
                    //case "3":
                    //    ProcessDelete();
                    //    break;
                    //case "4":
                    //    ProcessUpdate();
                    //    break;
            }
        }
    }

    private async Task ProcessCreate()
    {
        Console.WriteLine("\nPlease enter your Starting time (HH:mm)");
        string start = Console.ReadLine();

        CheckHourMinute(start);

        Console.WriteLine("\nPlease enter your End time (HH:mm)");
        string end = Console.ReadLine();

        CheckHourMinute(end);
        // Create a check for catching if start time is later than end time
        var dateTimeStart = DateTime.Now.ToShortDateString() + " " +  start;
        var dateTimeEnd = DateTime.Now.ToShortDateString() + " " + end;
        var finalStart = DateTime.Parse(dateTimeStart);
        var finalEnd = DateTime.Parse(dateTimeEnd);

        var minutes = CalculateMinutes(finalStart, finalEnd);
        var pay = CalculatePay(minutes);

        await ShiftController.CreateShift(finalStart, finalEnd, minutes, pay);
    }

    private decimal CalculatePay(decimal minutes)
    {
        //Would give you pay of 120kr (SEK) per hour
        decimal payPerMinute = 2;
        return payPerMinute * minutes;
    }

    private decimal CalculateMinutes(DateTime finalStart, DateTime finalEnd)
    {
        TimeSpan ts = finalEnd - finalStart;

        return (decimal)ts.TotalMinutes;
    }

    private void CheckHourMinute(string? input)
    {
        Regex regex = new Regex(pattern);

        while (!regex.IsMatch(input))
        {
            Console.WriteLine("Please enter the correct format (HH:mm)");
            input = Console.ReadLine();
        }
    }

    private async Task ProcessGet()
    {
        await ShiftController.GetShifts();
    }
}