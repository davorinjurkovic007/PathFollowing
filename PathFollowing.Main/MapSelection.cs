using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFollowing.Main
{
    public class MapSelection
    {
        const int PosibleMinimumSelectedMap = 1;
        const int PosibleMaximumSelectedMap = 11;

        public int GetNumberForMap(string chosenAnswer)
        {
            int selectedMap = CheckMapSelection(chosenAnswer);

            return selectedMap;
        }

        private int CheckMapSelection(string selectedMap)
        {
            int numberOfSelectedMap;
            bool mapIsSelected = int.TryParse(selectedMap, out numberOfSelectedMap);
            if (!mapIsSelected)
            {
                Console.WriteLine("You chose exit");

                return 0;
            }

            if(numberOfSelectedMap < PosibleMinimumSelectedMap || numberOfSelectedMap > PosibleMaximumSelectedMap)
            {
                Console.WriteLine("That map does not exist.");

                return 0;
            }

            return numberOfSelectedMap;
        }
    }
}
