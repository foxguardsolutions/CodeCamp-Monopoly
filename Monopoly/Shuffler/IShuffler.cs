using System.Collections.Generic;

namespace Shuffler
{
    public interface IShuffler
    {
        IEnumerable<T> Shuffle<T>(IEnumerable<T> source);
    }
}