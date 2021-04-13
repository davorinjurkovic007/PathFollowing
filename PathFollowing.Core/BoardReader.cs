using PathFollowing.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFollowing.Core
{
    public class BoardReader
    {
        private char[,] Board { get; set; }
        private int StartRowIndex { get; set; }
        private int StartColumnIndex { get; set; }

        private List<char> pathOfBoard = new List<char>();
        private List<char> pathOfLettersOnBoard = new List<char>();
        private Location previousLocation = new Location();
        private Location currentLocation = new Location();

        int boardLengthRow = -1;
        int boardLengthColumn = -1;

        public List<char> PathOfBoard { get; set; }
        public List<char> PathOfLettersOnBoard { get; set; }

        public BoardReader(char[,] board, int startRowIndex, int startColumnIndex)
        {
            Board = board;
            StartRowIndex = startRowIndex;
            StartColumnIndex = startColumnIndex;

            boardLengthRow = Board.GetLength(0);
            boardLengthColumn = Board.GetLength(1);
        }

        public void StartReading()
        {
            SetupFirstPositionForSearch();
            StartFindingElements();

            PathOfBoard = pathOfBoard;
            PathOfLettersOnBoard = pathOfLettersOnBoard;
        }

        private void SetupFirstPositionForSearch()
        {
            pathOfBoard.Add(Board[StartRowIndex, StartColumnIndex]);
            previousLocation.Element = Board[StartRowIndex, StartColumnIndex];
            previousLocation.PositionX = StartRowIndex;
            previousLocation.PositionY = StartColumnIndex;

            currentLocation = ReturnFirstCurrentLocation(previousLocation);

            pathOfBoard.Add(currentLocation.Element);
            AddAlphabeticalLetterToList(currentLocation.Element);
        }

        private Location ReturnFirstCurrentLocation(Location currentLocation)
        {
            List<Location> listFindedLocation = new List<Location>();
            Location findedLocation = new Location();

            int positionX = currentLocation.PositionX;
            int positionY = currentLocation.PositionY;

            int positionXUp = positionX - 1;
            int positionXDown = positionX + 1;
            int positionYLeft = positionY - 1;
            int positionYRigth = positionY + 1;

            if (positionXUp >= 0)
            {
                char element = Board[positionXUp, positionY];
                listFindedLocation.Add(new Location
                {
                    Element = element,
                    PositionX = positionXUp,
                    PositionY = positionY
                });
            }

            if (positionYRigth < boardLengthColumn)
            {
                char element = Board[positionX, positionYRigth];
                listFindedLocation.Add(new Location
                {
                    Element = element,
                    PositionX = positionX,
                    PositionY = positionYRigth
                });
            }

            if (positionXDown < boardLengthRow)
            {
                char element = Board[positionXDown, positionY];
                listFindedLocation.Add(new Location
                {
                    Element = element,
                    PositionX = positionXDown,
                    PositionY = positionY
                });
            }

            if (positionYLeft >= 0)
            {
                char element = Board[positionX, positionYLeft];
                listFindedLocation.Add(new Location
                {
                    Element = element,
                    PositionX = positionX,
                    PositionY = positionYLeft
                });
            }

            foreach (var locatons in listFindedLocation)
            {
                if (locatons.Element != Elements.EmptyPosition)
                {
                    findedLocation = locatons;
                }
            }

            return findedLocation;
        }

        private void AddAlphabeticalLetterToList(char element)
        {
            if(Elements.ListOfAlphabeticalLetters.Contains(element))
            {
                if(!pathOfLettersOnBoard.Contains(element))
                {
                    pathOfLettersOnBoard.Add(element);
                }
            }
        }

        private void StartFindingElements()
        {
            while(true)
            {
                var currentElement = FindNextElement();

                if(currentElement == Elements.EndingPosition)
                {
                    break;
                }
            }
        }

        private char FindNextElement()
        {
            List<Location> listFindedLocation = new List<Location>();

            int previousPositionX = previousLocation.PositionX;
            int previousPositionY = previousLocation.PositionY;

            int currentPositionX = currentLocation.PositionX;
            int currentPositionY = currentLocation.PositionY;

            int positionXUp = currentPositionX - 1;
            int positionXDown = currentPositionX + 1;
            int positionYLeft = currentPositionY - 1;
            int positionYRigth = currentPositionY + 1;

            if (positionXUp >= 0)
            {
                if (positionXUp != previousPositionX)
                {
                    char element = Board[positionXUp, currentPositionY];
                    listFindedLocation.Add(new Location
                    {
                        Element = element,
                        PositionX = positionXUp,
                        PositionY = currentPositionY
                    });
                }
            }

            if (positionYRigth < boardLengthColumn)
            {
                if (positionYRigth != previousPositionY)
                {
                    char element = Board[currentPositionX, positionYRigth];
                    listFindedLocation.Add(new Location
                    {
                        Element = element,
                        PositionX = currentPositionX,
                        PositionY = positionYRigth
                    });
                }
            }

            if (positionXDown < boardLengthRow)
            {
                if (positionXDown != previousPositionX)
                {
                    char element = Board[positionXDown, currentPositionY];
                    listFindedLocation.Add(new Location
                    {
                        Element = element,
                        PositionX = positionXDown,
                        PositionY = currentPositionY
                    });
                }
            }

            if (positionYLeft >= 0)
            {
                if (positionYLeft != previousPositionY)
                {
                    char element = Board[currentPositionX, positionYLeft];
                    listFindedLocation.Add(new Location
                    {
                        Element = element,
                        PositionX = currentPositionX,
                        PositionY = positionYLeft
                    });
                }
            }

            char currentFindedElement = ' ';
            bool isNextElementFinded = false;
            bool isTForkPossible = false;
            Location tempLocation = new Location();
            int numberOfNotNullLocations = CheckNotNulElementsInList(listFindedLocation);
            foreach (var locations in listFindedLocation)
            {
                if (locations.Element != Elements.EmptyPosition)
                {
                    isNextElementFinded = CheckElement(locations, numberOfNotNullLocations);

                    if(isNextElementFinded)
                    {
                        tempLocation = locations;

                        if(isTForkPossible)
                        {
                            throw new TForkExcetion($"This map has T Fork.");
                        }

                        isTForkPossible = true;
                    }
                }
            }

            if(tempLocation.Element == '\0')
            {
                throw new EmptyElementException($"Invalid map! Empty element found on path!");
            }

            pathOfBoard.Add(tempLocation.Element);
            AddAlphabeticalLetterToList(tempLocation.Element);

            previousLocation = currentLocation;
            currentLocation = tempLocation;

            currentFindedElement = tempLocation.Element;

            return currentFindedElement;
        }

        private int CheckNotNulElementsInList(List<Location> listFilledLocation)
        {
            int numberOfNotNullElements = 0;

            foreach(var location in listFilledLocation)
            {
                if(location.Element != Elements.EmptyPosition)
                {
                    numberOfNotNullElements++;
                }
            }

            return numberOfNotNullElements;
        }

        private bool CheckElement(Location locationToCheck, int numberOfElements)
        {
            bool usElementToMove = false;

            switch (currentLocation.Element)
            {
                case Elements.DirectionLeftRight:
                    usElementToMove = CheckDirectionLeftRight(locationToCheck);
                    break;
                case Elements.DirectionUpDown:
                    usElementToMove = CheckDirectionUpDown(locationToCheck);
                    break;
                case Elements.PlusPosition:
                    usElementToMove = CheckPlusDirection(locationToCheck, numberOfElements);
                    break;
                case Elements.LetterA:
                case Elements.LetterB:
                case Elements.LetterC:
                case Elements.LetterD:
                    usElementToMove = CheckLetterDirection(locationToCheck, numberOfElements);
                    break;
            }

            return usElementToMove;
        }

        private bool CheckDirectionLeftRight(Location locationToCheck)
        {
            int previosLocationX = previousLocation.PositionX;
            int previosLocationY = previousLocation.PositionY;

            int currentLocationX = currentLocation.PositionX;
            int currentLocationY = currentLocation.PositionY;

            bool directionXToCheck = false;
            bool directionYToCheck = false;
            bool goWithY = false;

            bool directionX = previosLocationX == currentLocationX ? true : false;
            if (directionX)
            {
                directionXToCheck = currentLocationX == locationToCheck.PositionX ? true : false;
            }

            bool directionY = previosLocationY == currentLocationY ? true : false;
            if (directionY)
            {
                directionYToCheck = currentLocationY == locationToCheck.PositionY ? true : false;
                if (directionYToCheck)
                {
                    goWithY = true;
                }
            }

            if (directionXToCheck || goWithY)
            {
                return true;
            }

            return false;
        }

        private bool CheckDirectionUpDown(Location locationToCheck)
        {
            int previosLocationX = previousLocation.PositionX;
            int previosLocationY = previousLocation.PositionY;

            int currentLocationX = currentLocation.PositionX;
            int currentLocationY = currentLocation.PositionY;

            bool directionXToCheck = false;
            bool directionYToCheck = false;

            bool goWithX = false;

            bool directionX = previosLocationX == currentLocationX ? true : false;
            if (directionX)
            {
                directionXToCheck = currentLocationX == locationToCheck.PositionX ? true : false;
                if (directionXToCheck)
                {
                    goWithX = true;   
                }
            }

            bool directionY = previosLocationY == currentLocationY ? true : false;
            if (directionY)
            {
                directionYToCheck = currentLocationY == locationToCheck.PositionY ? true : false;
            }

            if (directionYToCheck || goWithX)
            {
                return true;
            }

            return false;

        }

        private bool CheckLetterDirection(Location locationToCheck, int numberOfElements)
        {
            if (numberOfElements == 1)
            {
                return true;
            }
            else
            {
                int previosLocationX = previousLocation.PositionX;
                int previosLocationY = previousLocation.PositionY;

                int currentLocationX = currentLocation.PositionX;
                int currentLocationY = currentLocation.PositionY;

                bool directionXToCheck = false;
                bool directionYToCheck = false;

                bool directionX = previosLocationX == currentLocationX ? true : false;
                if (directionX)
                {
                    directionXToCheck = currentLocationX == locationToCheck.PositionX ? true : false;
                }

                bool directionY = previosLocationY == currentLocationY ? true : false;
                if (directionY)
                {
                    directionYToCheck = currentLocationY == locationToCheck.PositionY ? true : false;
                }

                if (directionXToCheck || directionYToCheck)
                {
                    return true;
                }

                return false;
            }
        }

        private bool CheckPlusDirection(Location locationToCheck, int numberOfElements)
        {
            if (numberOfElements == 1)
            {
                return true;
            }
            else
            {
                int previosLocationX = previousLocation.PositionX;
                int previosLocationY = previousLocation.PositionY;

                int currentLocationX = currentLocation.PositionX;
                int currentLocationY = currentLocation.PositionY;

                bool directionXToCheck = false;
                bool directionYToCheck = false;

                bool directionX = previosLocationX == currentLocationX ? true : false;
                if (directionX)
                {
                    directionXToCheck = currentLocationX != locationToCheck.PositionX ? true : false;
                }

                bool directionY = previosLocationY == currentLocationY ? true : false;
                if (directionY)
                {
                    directionYToCheck = currentLocationY != locationToCheck.PositionY ? true : false;
                }

                if (directionXToCheck || directionYToCheck)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
