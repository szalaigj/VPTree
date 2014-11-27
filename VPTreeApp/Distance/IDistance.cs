using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.Distance
{
    public interface IDistance<T, I>
        where T : IComparable<T>
    {
        T calculateDistance(I input, I otherInput);
    }
}
