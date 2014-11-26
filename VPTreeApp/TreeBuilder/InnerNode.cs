using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.TreeBuilder
{
    class InnerNode<T, I> : INode<T, I>
    {
        private I pivotPoint;
        private T[] lowerBounds;
        private T[] upperBounds;

        public I PivotPoint
        {
            get { return pivotPoint; }
        }

        public T[] LowerBounds
        {
            get { return lowerBounds; }
        }

        public T[] UpperBounds
        {
            get { return upperBounds; }
        }

        public InnerNode(I pivotPoint, T lowerBoundLo, T lowerBoundHi, T upperBoundLo, T upperBoundHi)
        {
            InitializeMembers(pivotPoint, new T[]{lowerBoundLo, lowerBoundHi}, new T[]{upperBoundLo, upperBoundHi});
        }

        private void InitializeMembers(I pivotPoint, T[] lowerBounds, T[] upperBounds)
        {
            this.pivotPoint = pivotPoint;
            this.lowerBounds = lowerBounds;
            this.upperBounds = upperBounds;
        }

    }
}
