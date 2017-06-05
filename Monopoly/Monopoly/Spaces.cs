using System.Collections;
using System.Collections.Generic;
using System.Linq;

using BoardGame;

namespace Monopoly
{
    public class Spaces : IEnumerable<ISpace>
    {
        public static IEnumerable<string> Names
        {
            get
            {
                yield return "Go";
                yield return "Mediterranean Avenue";
                yield return "Community Chest";
                yield return "Baltic Avenue";
                yield return "Income Tax";
                yield return "Reading Railroad";
                yield return "Oriental Avenue";
                yield return "Chance";
                yield return "Vermont Avenue";
                yield return "Connecticut Avenue";
                yield return "In Jail/Just Visiting";
                yield return "St. Charles Place";
                yield return "Electric Company";
                yield return "States Avenue";
                yield return "Virginia Avenue";
                yield return "Pennsylvania Railroad";
                yield return "St. James Place";
                yield return "Community Chest";
                yield return "Tennessee Avenue";
                yield return "New York Avenue";
                yield return "Free Parking";
                yield return "Kentucky Avenue";
                yield return "Chance";
                yield return "Indiana Avenue";
                yield return "Illinois Avenue";
                yield return "B&O Railroad";
                yield return "Atlantic Avenue";
                yield return "Ventnor Avenue";
                yield return "Water Works";
                yield return "Marvin Gardens";
                yield return "Go To Jail";
                yield return "Pacific Avenue";
                yield return "North Carolina Avenue";
                yield return "Community Chest";
                yield return "Pennsylvania Avenue";
                yield return "Short Line";
                yield return "Chance";
                yield return "Park Place";
                yield return "Luxury Tax";
                yield return "Boardwalk";
            }
        }

        public IEnumerator<ISpace> GetEnumerator()
        {
            return Names.Select(n => new Space(n)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
