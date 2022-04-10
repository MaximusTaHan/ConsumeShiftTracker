// See https://aka.ms/new-console-template for more information
using ConsumeShiftTracker;
using ConsumeShiftTracker.Models;
using System.Text.RegularExpressions;
internal class MainMenu
{
    //Hour minute regex string
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
                case "3":
                    await ProcessUpdate();
                    break;
                case "4":
                    await ProcessDelete();
                    break;
            }
        }
    }

    private async Task ProcessUpdate()
    {
        var ShiftList = await ShiftController.GetShifts();
        bool found = false;
        ShiftVisualizer.ShowShifts(ShiftList);
        Console.WriteLine("What shift Entry would you like to Update? (By ID). (Or 0 to return to Main Menu)");
        string input = Console.ReadLine();

        while (found == false && int.TryParse(input, out int num))
        {
            if (num == 0)
            {
                await Menu();
            }
            foreach (var shift in ShiftList)
            {
                if (num == shift.shiftId)
                {
                    found = true;
                    var UpdateShiftDTO = await SetShiftValues();
                    UpdateShiftDTO.shiftId = num;
                    await ShiftController.UpdateShift(UpdateShiftDTO);
                }
            }
            Console.WriteLine("Please enter an Existing ID number");
            input = Console.ReadLine();
        }
    }

    private async Task ProcessDelete()
    {
        var ShiftList = await ShiftController.GetShifts();
        bool found = false;
        ShiftVisualizer.ShowShifts(ShiftList);
        Console.WriteLine("What shift Entry would you like to Delete? (By ID). (Or 0 to return to Main Menu)");
        string input = Console.ReadLine();

        while (found == false && int.TryParse(input, out int num))
        {
            if(num == 0)
            {
               await Menu();
            }
            foreach (var shift in ShiftList)
            {
                if(num == shift.shiftId)
                {
                    found = true;
                    await ShiftController.DeleteShift(num);
                    await Menu();
                }
            }
            Console.WriteLine("Please enter an Existing ID number");
            input = Console.ReadLine();
        }
    }

    private async Task ProcessCreate()
    {
        var CreateShiftDTO = await SetShiftValues();

        await ShiftController.CreateShift(CreateShiftDTO);
    }

    private async Task<Shift> SetShiftValues()
    {
        Console.WriteLine("\nPlease enter your Starting time (HH:mm). (Type 0 to return to Main Menu)");

        string start = await CheckHourMinute(Console.ReadLine());

        Console.WriteLine("\nPlease enter your End time (HH:mm). (Type 0 to return to Main Menu)");
        string end = await CheckHourMinute(Console.ReadLine());

        // Create a check for catching if start time is later than end time
        var dateTimeStart = DateTime.Now.ToShortDateString() + " " + start;
        var dateTimeEnd = DateTime.Now.ToShortDateString() + " " + end;
        var finalStart = DateTime.Parse(dateTimeStart);
        var finalEnd = DateTime.Parse(dateTimeEnd);

        var minutes = CalculateMinutes(finalStart, finalEnd);
        var pay = CalculatePay(minutes);

        Shift shift = new Shift();
        {
            shift.start = finalStart;
            shift.end = finalEnd;
            shift.minutes = minutes;
            shift.pay = pay;
            shift.location = "";
        }
        return shift;
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

    private async Task<string> CheckHourMinute(string? input)
    {
        Regex regex = new Regex(pattern);

        if (input == "0")
            await Menu();

        while (!regex.IsMatch(input))
        {
            Console.WriteLine("Please enter the correct format (HH:mm). (Type 0 to return to Main Menu)");
            input = Console.ReadLine();
            if (input == "0")
                await Menu();
        }
        return input;
    }

    private async Task ProcessGet()
    {
        var shiftList = await ShiftController.GetShifts();
        ShiftVisualizer.ShowShifts(shiftList);
    }
}