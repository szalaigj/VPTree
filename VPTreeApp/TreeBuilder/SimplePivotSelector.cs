using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.TreeBuilder
{
    public class SimplePivotSelector<T, I> : IPivotSelector<T, I>
        where T : IComparable<T>
        where I : IComparable<I>
    {
        public I selectPivot(List<I> inputData)
        {
            int rndElementIdx = RandomWrapper.rnd.Next(inputData.Count);
            I result = inputData[rndElementIdx];
            inputData.RemoveAt(rndElementIdx);
            return result;
        }
    }
}
