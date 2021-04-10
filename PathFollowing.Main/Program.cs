using System;

namespace PathFollowing.Main
{
    public class Program
    {
        const int ExitProgram = 0;

        public static void Main(string[] args)
        {
            int selectedMap = MapSelection();

            if (selectedMap != ExitProgram)
            {
                Console.WriteLine("Continue program");
            }
            else
            {
                Console.WriteLine("Program is Exit");
            }
        }

        

        private static int MapSelection()
        {
            MapSelection mapSelection = new MapSelection();

            Console.WriteLine("Find the way.");

            int selectedMap = mapSelection.GetNumberForMap(ShowPosibleMaps());

            return selectedMap;
        }

        private static string ShowPosibleMaps()
        {
            Console.WriteLine("Please select one number from 1 to 10:");
            Console.WriteLine("1: Map 1 - a basic example.");
            Console.WriteLine("2: Map 2 - go straight through intersections.");
            Console.WriteLine("3: Map 3 - letters may be found on turns.");
            Console.WriteLine("4: Map 4 - do not collect letters twice.");
            Console.WriteLine("5: Map 5 - keep direction, even in a compact space.");
            Console.WriteLine("6: Map 6 - no start.");
            Console.WriteLine("7: Map 7 - no end.");
            Console.WriteLine("8: Map 8 - multiple starts.");
            Console.WriteLine("9: Map 9 - multiple ends.");
            Console.WriteLine("10: Map 10 - T forks.");
            Console.WriteLine("Any other choice: Exit");

            var selectedPath = Console.ReadLine();

            return selectedPath;
        }
    }
}
