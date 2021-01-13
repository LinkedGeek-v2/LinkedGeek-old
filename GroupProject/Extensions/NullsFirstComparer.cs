using System.Collections.Generic;

namespace GroupProject.Extensions
{
    public class NullsFirstComparer<T> : Comparer<T>
    {
        private readonly IComparer<T> _comparer;

        public NullsFirstComparer() : this(null) { }

        public NullsFirstComparer(IComparer<T> comparer)
        {
            _comparer = comparer ?? Default;
        }

        public override int Compare(T x, T y)
        {
            if (x == null) return (y == null) ? 0 : 1;
            if (y == null) return -1; 
            return _comparer.Compare(x, y);
        }
    }
}