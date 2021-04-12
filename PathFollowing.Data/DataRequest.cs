using PathFollowing.Data.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PathFollowing.Data
{
    public class DataRequest
    {
        private const string FolderName = "Files";

        private int fileNumberRequest;
        private string filePath = string.Empty;
        List<string> fileInListOfStrings = new List<string>();

        public char[,] Board { get; set; }
        public int StartRowIndex { get; set; }
        public int StartColumnIndex { get; set; }


        public DataRequest(int fileNumberRequest)
        {
            this.fileNumberRequest = fileNumberRequest;
        }

        
        public void GetBoard()
        {
            filePath = GetFilePath(fileNumberRequest);
            ReadFile(filePath);
            CreateBoard();
        }

        private string GetFilePath(int fileNumber)
        {
            string filePath = string.Empty;

            switch (fileNumber)
            {
                case 1:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 1 - Basic example.txt");
                    break;
                case 2:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 2 - go straight through intersections.txt");
                    break;
                case 3:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 3 - letters may be found on turns.txt");
                    break;
                case 4:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 4 - do not collect letters twice.txt");
                    break;
                case 5:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 5 - keep direction, even in a compact space.txt");
                    break;
                case 6:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 6 - no start.txt");
                    break;
                case 7:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 7 - no end.txt");
                    break;
                case 8:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 8 - multiple starts.txt");
                    break;
                case 9:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 9 - multiple ends.txt");
                    break;
                case 10:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 10 - T forks.txt");
                    break;
                case 11:
                    filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FolderName, "Map 11 - Real T fork example.txt");
                    break;
            }

            return filePath;
        }

        private void ReadFile(string filePath)
        {
            try
            {
                using (var inputStreamReader = new StreamReader(filePath))
                {
                    while (!inputStreamReader.EndOfStream)
                    {
                        string line = inputStreamReader.ReadLine();
                        Console.WriteLine(line);
                        fileInListOfStrings.Add(line);
                    }
                }
            }
            catch
            {
                throw new FilePahtException($"File path {filePath} or file is not exist.");
            }
        }

        private void CreateBoard()
        {
            var rowDimension = fileInListOfStrings.Count;
            var columnDimension = fileInListOfStrings[0].Length;

            char[,] board = new char[rowDimension, columnDimension];

            bool startPositioFound = false;
            bool endPositionFound = false;

            int startRowIndex = -1;
            int starColumnIndex = -1;

            bool illegalCharacter = false;
            char illegalLetter = ' ';
            bool doubleStartPosition = false;
            bool doubleEndPosition = false;

            for (int i = 0; i < rowDimension; i++)
            {
                var inputRow = fileInListOfStrings[i];
                for (int j = 0; j < columnDimension; j++)
                {
                    char letter = inputRow[j];

                    if (!CheckIsAsciiElement(letter))
                    {
                        illegalCharacter = true;
                        illegalLetter = letter;
                        break;
                    }

                    if (IsStartPosition(letter))
                    {
                        if (startPositioFound)
                        {
                            doubleStartPosition = true;
                            break;
                        }

                        startPositioFound = true;
                        startRowIndex = i;
                        starColumnIndex = j;
                    }

                    if (IsEndPosition(letter))
                    {
                        if (endPositionFound)
                        {
                            doubleEndPosition = true;
                            break;
                        }

                        endPositionFound = true;
                    }

                    board[i, j] = letter;
                }

                if (illegalCharacter)
                {
                    throw new IllegalSignException($"Character {illegalLetter} is not allow in file.");
                }

                if (doubleStartPosition)
                {
                    throw new MoreThanOneStartPositionException($"There is more than one start position in {filePath}.");
                }

                if (doubleEndPosition)
                {
                    throw new MoreThanOneEndPositionException($"There is more than one end position in {filePath}.");
                }
            }

            if (!startPositioFound)
            {
                throw new NoneStartPositionException($"There is no start position for {filePath}.");
            }

            if (!endPositionFound)
            {
                throw new NoneEndPositionException($"There is no end position for {filePath}.");
            }

            Board = board;
            StartRowIndex = startRowIndex;
            StartColumnIndex = StartColumnIndex;
        }

        private static bool CheckIsAsciiElement(char letter)
        {
            if (letter <= sbyte.MaxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsStartPosition(char letter)
        {
            if (letter == Elements.StartingPosition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsEndPosition(char letter)
        {
            if (letter == Elements.EndingPosition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
