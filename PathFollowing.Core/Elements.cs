using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFollowing.Core
{
    public class Elements
    {
        public const char StartingPosition = '@';
        public const char EndingPosition = 'x';
        public const char DirectionLeftRight = '-';
        public const char DirectionUpDown = '|';
        public const char EmptyPosition = ' ';
        public const char PlusPosition = '+';
        public const char LetterA = 'A';
        public const char LetterB = 'B';
        public const char LetterC = 'C';
        public const char LetterD = 'D';

        public static List<char> ListOfAlphabeticalLetters = new List<char>
        {
            LetterA,
            LetterB,
            LetterC,
            LetterD
        };
    }
}
