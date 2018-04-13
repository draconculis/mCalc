using System;

namespace Dek572.Cls
{
    public interface IMPoint
    {
        decimal Amount { get; set; }
        DateTime Date { get; set; }
        long Ticks { get; set; }

        int CompareTo(object obj);
    }
}