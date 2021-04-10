using PathFollowing.Main;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace PathFollowing.Test.Main
{
    public class MapSelectionReturnValue
    {
        private readonly MapSelection mapSelection;

        public MapSelectionReturnValue()
        {
            mapSelection = new MapSelection();
        }

        [Theory]
        [MainInputValueData]
        public void MapSelectionIsReturingValue(string inputValue, int expectedValue)
        {
            int returningValue = mapSelection.GetNumberForMap(inputValue);

            Assert.Equal(expectedValue, returningValue);
        }
    }
}
