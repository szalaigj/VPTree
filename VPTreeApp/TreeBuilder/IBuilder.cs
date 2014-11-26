using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPTreeApp.Distance;

namespace VPTreeApp.TreeBuilder
{
    public interface IBuilder<T, I>
    {
        IDistance<T, I> Distance { get; set; }

        Tree buildTree(List<string> inputData);
    }
}
