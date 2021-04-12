using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFollowing.Main
{
    public class MapSelection
    {
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

            if(numberOfSelectedMap <= 0 || numberOfSelectedMap > 11)
            {
                Console.WriteLine("That map does not exist.");

                return 0;
            }

            return numberOfSelectedMap;
        }
    }
}
