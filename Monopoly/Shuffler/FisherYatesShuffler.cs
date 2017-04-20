using System;
using System.Collections.Generic;
using System.Linq;

namespace Shuffler
{
    // http://stackoverflow.com/a/1653204/3697120
    public class FisherYatesShuffler : IShuffler
    {
        private readonly Random _random;

        public FisherYatesShuffler(Random random)
        {
            _random = random;
        }

        public IEnumerable<T> Shuffle<T>(IEnumerable<T> source)
        {
            var buffer = source.ToList();
            for (var i = 0; i < buffer.Count; i++)
            {
                var j = _random.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}
