using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly.RealEstate
{
    public class MonopolyStreetGroups : IEnumerable<KeyValuePair<string, IEnumerable<Street>>>
    {
        public IEnumerable<Street> Indigo
        {
            get
            {
                yield return new Street(1, 2, 60);
                yield return new Street(3, 4, 60);
            }
        }

        public IEnumerable<Street> SkyBlue
        {
            get
            {
                yield return new Street(6, 6, 100);
                yield return new Street(8, 6, 100);
                yield return new Street(9, 8, 120);
            }
        }

        public IEnumerable<Street> DarkOrchid
        {
            get
            {
                yield return new Street(11, 10, 140);
                yield return new Street(13, 10, 140);
                yield return new Street(14, 12, 160);
            }
        }

        public IEnumerable<Street> Orange
        {
            get
            {
                yield return new Street(16, 14, 180);
                yield return new Street(18, 14, 180);
                yield return new Street(19, 16, 200);
            }
        }

        public IEnumerable<Street> Red
        {
            get
            {
                yield return new Street(21, 18, 220);
                yield return new Street(23, 18, 220);
                yield return new Street(24, 20, 240);
            }
        }

        public IEnumerable<Street> Yellow
        {
            get
            {
                yield return new Street(26, 22, 260);
                yield return new Street(27, 22, 260);
                yield return new Street(29, 24, 280);
            }
        }

        public IEnumerable<Street> Green
        {
            get
            {
                yield return new Street(31, 26, 300);
                yield return new Street(32, 26, 300);
                yield return new Street(34, 28, 320);
            }
        }

        public IEnumerable<Street> Blue
        {
            get
            {
                yield return new Street(37, 35, 350);
                yield return new Street(39, 50, 400);
            }
        }

        #region IEnumerable implementation

        public IEnumerator<KeyValuePair<string, IEnumerable<Street>>> GetEnumerator()
        {
            return GetType().GetProperties().Select(
                streetGroup => new KeyValuePair<string, IEnumerable<Street>>(
                    streetGroup.Name,
                    (IEnumerable<Street>)streetGroup.GetValue(this)))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
