using PathFollowing.Core;
using PathFollowing.Core.Exceptions;
using Xunit;

namespace PathFollowing.Test.Core
{
    public class BoardReaderTest
    {
        [Fact]
        private void BoardReaderReadsMinimumHoriznontal()
        {
            BoardReader boardReader = new BoardReader(new char[1, 3] { { '@', '-', 'x' } }, 0, 0);

            boardReader.StartReading();

            var returnedResult = new string(boardReader.PathOfBoard.ToArray());

            Assert.Equal("@-x", returnedResult);
        }

        [Fact]
        private void BoardReaderReadsMinimumVertical()
        {
            BoardReader boardReader = new BoardReader(new char[3, 1] { { '@' },
                                                                        { '|'},
                                                                        {'x'} }, 0, 0);

            boardReader.StartReading();

            var returnedResult = new string(boardReader.PathOfBoard.ToArray());

            Assert.Equal("@|x", returnedResult);
        }

        [Fact]
        private void BorderReaderReadsSimpleMaps()
        {
            BoardReader boardReader = new BoardReader(new char[5, 9] { { '@', '-', '-', '-', 'A', '-', '-', '-', '+' },
                                                                       { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
                                                                       { 'x', '-', 'B', '-', '+', ' ', ' ', ' ', 'C' },
                                                                       { ' ', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' },
                                                                       { ' ', ' ', ' ', ' ', '+', '-', '-', '-', '+' } }, 0, 0);

            boardReader.StartReading();

            var returnedResultCharacterPaht = new string(boardReader.PathOfBoard.ToArray());
            var returnedResultLetters = new string(boardReader.PathOfLettersOnBoard.ToArray());

            Assert.Equal("@---A---+|C|+---+|+-B-x", returnedResultCharacterPaht);
            Assert.Equal("ACB", returnedResultLetters);
        }

        [Fact]
        private void BorderReaderReadsThroughIntersections()
        {
            BoardReader boardReader = new BoardReader(new char[7, 10] { { ' ', ' ', ' ', ' ', ' ', '+', '+', ' ', ' ', ' ' },
                                                                        { '@', ' ', '+', 'C', ' ', '|', '|', ' ', ' ', ' ' },
                                                                        { '|', ' ', 'B', '|', ' ', '|', '|', ' ', ' ', ' ' },
                                                                        { 'A', ' ', '|', '|', ' ', '|', 'D', ' ', ' ', ' ' },
                                                                        { '+', '-', '-', '+', ' ', '+', '|', '-', 'D', 'x' },
                                                                        { ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ' },
                                                                        { ' ', ' ', '+', '-', '-', '-', '+', ' ', ' ', ' ' } }, 1, 0);

            boardReader.StartReading();

            var returnedResultCharacterPaht = new string(boardReader.PathOfBoard.ToArray());
            var returnedResultLetters = new string(boardReader.PathOfLettersOnBoard.ToArray());

            Assert.Equal("@|A+--+||C+B|-|+---+||D||++|||+|-Dx", returnedResultCharacterPaht);
            Assert.Equal("ACBD", returnedResultLetters);
        }

        [Fact]
        private void BorderReaderReadsLettersOnTurn()
        {
            BoardReader boardReader = new BoardReader(new char[5, 9] { { 'A', '-', 'A', ' ', ' ', ' ', ' ', ' ', 'x' },
                                                                       { '|', ' ', '|', ' ', ' ', ' ', ' ', ' ', '|' },
                                                                       { '|', ' ', 'B', '-', 'C', ' ', ' ', ' ', 'D' },
                                                                       { '+', '@', ' ', ' ', '|', ' ', ' ', ' ', '|' },
                                                                       { ' ', ' ', ' ', ' ', 'C', '-', '-', '-', '+' } }, 3, 1);

            boardReader.StartReading();

            var returnedResultCharacterPaht = new string(boardReader.PathOfBoard.ToArray());
            var returnedResultLetters = new string(boardReader.PathOfLettersOnBoard.ToArray());

            Assert.Equal("@+||A-A|B-C|C---+|D|x", returnedResultCharacterPaht);
            Assert.Equal("ABCD", returnedResultLetters);

        }

        [Fact]
        private void BorderReaderReadsDoNotCollectLetterTwice()
        {
            BoardReader boardReader = new BoardReader(new char[5, 12] { { ' ', ' ', ' ', '+', '-', '-', 'B', '-', '-', '+', ' ', ' ' },
                                                                        { ' ', ' ', ' ', '|', ' ', ' ', ' ', '+', '-', 'C', '-', '+' },
                                                                        { '@', '-', '-', 'A', '-', '+', ' ', '|', ' ', '|', ' ', '|' },
                                                                        { ' ', ' ', ' ', '|', ' ', '|', ' ', '+', '-', '+', ' ', 'D' },
                                                                        { ' ', ' ', ' ', '+', '-', '+', ' ', ' ', ' ', ' ', ' ', 'x' } }, 2, 0);

            boardReader.StartReading();

            var returnedResultCharacterPaht = new string(boardReader.PathOfBoard.ToArray());
            var returnedResultLetters = new string(boardReader.PathOfLettersOnBoard.ToArray());

            Assert.Equal("@--A-+|+-+|A|+--B--+C|+-+|+-C-+|Dx", returnedResultCharacterPaht);
            Assert.Equal("ABCD", returnedResultLetters);
        }

        [Fact]
        private void BorderReaderReadsKeepDirection()
        {
            BoardReader boardReader = new BoardReader(new char[4, 8] {  { ' ', '+', '-', 'B', '-', '+', ' ', ' ' },
                                                                        { ' ', '|', ' ', ' ', '+', 'C', '-', '+' },
                                                                        { '@', 'A', '+', ' ', '+', '+', ' ', 'D' },
                                                                        { ' ', '+', '+', ' ', ' ', ' ', ' ', 'x' } }, 2, 0);

            boardReader.StartReading();

            var returnedResultCharacterPaht = new string(boardReader.PathOfBoard.ToArray());
            var returnedResultLetters = new string(boardReader.PathOfLettersOnBoard.ToArray());

            Assert.Equal("@A+++A|+-B-+C+++C-+Dx", returnedResultCharacterPaht);
            Assert.Equal("ABCD", returnedResultLetters);
        }

        [Fact]
        private void BorderReaderRecognizeTFork()
        {
            BoardReader boardReader = new BoardReader(new char[7, 8] {  { ' ', ' ', ' ', ' ', ' ', 'x', '-', 'B' },
                                                                        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
                                                                        { '@', '-', '-', 'A', '-', '-', '-', '+' },
                                                                        { ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
                                                                        { ' ', ' ', ' ', '+', '-', '-', '-', 'C' },
                                                                        { ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' },
                                                                        { ' ', ' ', ' ', '+', '-', '-', '-', '+' } }, 2, 0);

            TForkExcetion ex = Assert.Throws<TForkExcetion>(() => boardReader.StartReading());
            Assert.Equal("This map has T Fork.", ex.Message);
        }

        [Fact]
        private void BorderReaderReadsMapWithIrregularPath()
        {
            BoardReader boardReader = new BoardReader(new char[5, 9] { { '@', '-', '-', '-', 'A', '-', '-', ' ', '+' },
                                                                       { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
                                                                       { 'x', '-', 'B', '-', '+', ' ', ' ', ' ', 'C' },
                                                                       { ' ', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' },
                                                                       { ' ', ' ', ' ', ' ', '+', '-', '-', '-', '+' } }, 0, 0);

            EmptyElementException ex = Assert.Throws<EmptyElementException>(() => boardReader.StartReading());
            Assert.Equal("Invalid map! Empty element found on path!", ex.Message);
        }
    }
}
