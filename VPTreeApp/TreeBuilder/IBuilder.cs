using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPTreeApp.Distance;

namespace VPTreeApp.TreeBuilder
{
    public interface IBuilder<T, I>
        where T : IComparable<T>
        where I : IComparable<I>
    {
        Tree<T, I> buildTree(List<I> inputData);
    }
}
