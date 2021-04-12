using PathFollowing.Core;
using PathFollowing.Core.Exceptions;
using PathFollowing.Data;
using PathFollowing.Data.Exceptions;
using System;

namespace PathFollowing.Main
{
    public class Program
    {
        const int ExitProgram = 0;

        static char[,] pathMatrix;
        static int startPositionX = -1;
        static int startPositionY = -1;

        public static void Main(string[] args)
        {
            int selectedMap = MapSelection();
            ReturnedBoard(selectedMap, out pathMatrix);
            FoundedPath();
        }

        private static int MapSelection()
        {
            MapSelection mapSelection = new MapSelection();

            Console.WriteLine("Find the way.");

            int selectedMap = mapSelection.GetNumberForMap(ShowPosibleMaps());

            if(selectedMap == ExitProgram)
            {
                Console.WriteLine("Exit application.");
                Environment.Exit(1);
            }

            return selectedMap;
        }

        private static string ShowPosibleMaps()
        {
            Console.WriteLine("Please select one number from 1 to 11:");
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
            Console.WriteLine("11: Map 11 - Real T fork example.");
            Console.WriteLine("Any other choice: Exit");

            var selectedPath = Console.ReadLine();

            return selectedPath;
        }

        private static void ReturnedBoard(int selectedMap, out char[,] board)
        {
            DataRequest dataRequest = new DataRequest(selectedMap);
           
            try
            {
                dataRequest.GetBoard();
            }
            catch(FilePahtException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exit application");
                Environment.Exit(2);
            }
            catch(IllegalSignException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exit application");
                Environment.Exit(2);
            }
            catch(MoreThanOneStartPositionException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exit application");
                Environment.Exit(2);
            }
            catch(MoreThanOneEndPositionException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exit application");
                Environment.Exit(2);
            }
            catch(NoneStartPositionException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exit application");
                Environment.Exit(2);
            }
            catch(NoneEndPositionException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exit application");
                Environment.Exit(2);
            }

            board = dataRequest.Board;
            startPositionX = dataRequest.StartRowIndex;
            startPositionY = dataRequest.StartColumnIndex;
        }

        private static void FoundedPath()
        {
            BoardReader boardReader = new BoardReader(pathMatrix, startPositionX, startPositionY);
            try
            {
                boardReader.StartReading();
            }
            catch(TForkExcetion ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exit application");
                Environment.Exit(2);
            }

            var letters = boardReader.PathOfLettersOnBoard;
            var pathAsCharacter = boardReader.PathOfBoard;

            Console.WriteLine("Letters");
            foreach(var letter in letters)
            {
                Console.Write(letter);
            }

            Console.WriteLine();
            Console.WriteLine("Path as characters:");
            foreach(var elementOfPath in pathAsCharacter)
            {
                Console.Write(elementOfPath);
            }
            Console.WriteLine();
            Console.WriteLine("End");
        }

    }
}
