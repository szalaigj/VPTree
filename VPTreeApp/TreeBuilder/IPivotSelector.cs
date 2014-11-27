using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.TreeBuilder
{
    public interface IPivotSelector<I>
    {
        I selectPivot(List<I> inputData);
    }
}
