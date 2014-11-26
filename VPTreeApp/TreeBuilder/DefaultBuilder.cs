using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPTreeApp.Distance;

namespace VPTreeApp.TreeBuilder
{
    public class DefaultBuilder<T, I> : IBuilder<T, I>
    {
        private IDistance<T, I> distance;

        public IDistance<T, I> Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public DefaultBuilder(IDistance<T, I> distance)
        {
            this.distance = distance;
        }

        public Tree buildTree(List<string> inputData)
        {
            return null;
        }
    }
}
