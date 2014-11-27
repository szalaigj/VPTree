using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPTreeApp.Distance;
using VPTreeApp.TreeBuilder;

namespace VPTreeApp.Algorithm
{
    public class RangeSeeker<T, I>
        where T : IComparable<T>
        where I : IComparable<I>
    {
        private IDistance<T, I> distance;
        private Func<T, T, T> subtract;
        private Func<T, T, T> maximumButNonNegative;

        public RangeSeeker(IDistance<T, I> distance, Func<T, T, T> subtract, Func<T, T, T> maximumButNonNegative)
        {
            this.distance = distance;
            this.subtract = subtract;
            this.maximumButNonNegative = maximumButNonNegative;
        }

        public List<I> search(I queryPoint, T maxDist, INode<T, I> root)
        {
            List<I> resultSet = new List<I>();
            doSearch(queryPoint, maxDist, root, resultSet);
            return resultSet;
        }

        private void doSearch(I queryPoint, T maxDist, INode<T, I> node, List<I> resultSet)
        {
            if (node.GetType() == typeof(InnerNode<T, I>))
            {
                doSearchInnerNode(queryPoint, maxDist, node, resultSet);
            }
            else if (node.GetType() == typeof(LeafNode<T, I>))
            {
                LeafNode<T, I> castedNode = (LeafNode<T, I>)node;
                T dist = distance.calculateDistance(queryPoint, castedNode.Data);
                if (dist.CompareTo(maxDist) <= 0)
                {
                    resultSet.Add(castedNode.Data);
                }
            }
        }

        private void doSearchInnerNode(I queryPoint, T maxDist, INode<T, I> node, List<I> resultSet)
        {
            InnerNode<T, I> castedNode = (InnerNode<T, I>)node;
            T dist = distance.calculateDistance(queryPoint, castedNode.PivotPoint);
            if (dist.CompareTo(maxDist) <= 0)
            {
                resultSet.Add(castedNode.PivotPoint);
            }
            doSearchInOneDirection(queryPoint, maxDist, resultSet, castedNode, dist, castedNode.LeftNode);
            doSearchInOneDirection(queryPoint, maxDist, resultSet, castedNode, dist, castedNode.RightNode);
        }

        private void doSearchInOneDirection(I queryPoint, T maxDist, List<I> resultSet,
            InnerNode<T, I> castedNode, T dist, INode<T, I> nodeInOneDirection)
        {
            T d1 = this.subtract(dist, castedNode.LowerBounds[1]);
            T d2 = this.subtract(castedNode.LowerBounds[0], dist);
            T tempDist = this.maximumButNonNegative(d1, d2);
            if ((nodeInOneDirection != null) && (tempDist.CompareTo(maxDist) <= 0))
            {
                doSearch(queryPoint, maxDist, nodeInOneDirection, resultSet);
            }
        }
    }
}
