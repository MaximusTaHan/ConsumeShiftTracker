using ConsoleTableExt;
using ConsumeShiftTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeShiftTracker
{
    internal class ShiftVisualizer
    {
        internal static void ShowShifts<T>(List<T> tableData) where T : class
        {
            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}
