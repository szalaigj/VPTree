using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.TreeBuilder
{
    public interface INode<T, I>
        where T : IComparable<T>
        where I : IComparable<I>
    {
        void print(int level, string direction);
    }
}
