using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GroupProject.Extensions
{
    public class FileNamesOrderByDate<T> : Comparer<T>
    {
        private readonly IComparer<T> _comparer;

        public FileNamesOrderByDate() : this(null) { }

        public FileNamesOrderByDate(IComparer<T> comparer)
        {
            _comparer = comparer ?? Default;
        }

        public override int Compare(T x, T y)
        {
            var dt1 = DateTime.Parse(Regex.Match(x.ToString(), "([0-9]*[.][0-9]*[.][0-9]*)").Groups[0].Value);
            var dt2 = DateTime.Parse(Regex.Match(y.ToString(), "([0-9]*[.][0-9]*[.][0-9]*)").Groups[0].Value);
            if (dt1 > dt2) return 1;
            else return 0;
        }
    }
}