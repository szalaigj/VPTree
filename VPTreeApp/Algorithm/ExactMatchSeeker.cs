using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPTreeApp.Distance;
using VPTreeApp.TreeBuilder;

namespace VPTreeApp.Algorithm
{
    public class ExactMatchSeeker<T, I>
        where T : IComparable<T>
        where I : IComparable<I>
    {
        private IDistance<T, I> distance;

        public ExactMatchSeeker(IDistance<T, I> distance)
        {
            this.distance = distance;
        }

        public bool search(I queryPoint, INode<T, I> root)
        {
            List<I> resultSet = new List<I>();
            doSearch(queryPoint, root, resultSet);
            return (resultSet.Count > 0);
        }

        private void doSearch(I queryPoint, INode<T, I> node, List<I> resultSet)
        {
            if (node.GetType() == typeof(InnerNode<T, I>))
            {
                doSearchInnerNode(queryPoint, node, resultSet);
            }
            else if (node.GetType() == typeof(LeafNode<T, I>))
            {
                LeafNode<T, I> castedNode = (LeafNode<T, I>) node;
                if (queryPoint.CompareTo(castedNode.Data) == 0)
                {
                    resultSet.Add(queryPoint);
                }
            }
        }

        private void doSearchInnerNode(I queryPoint, INode<T, I> node, List<I> resultSet)
        {
            InnerNode<T, I> castedNode = (InnerNode<T, I>)node;
            if (queryPoint.CompareTo(castedNode.PivotPoint) == 0)
            {
                resultSet.Add(queryPoint);
            }
            else
            {
                T dist = distance.calculateDistance(queryPoint, castedNode.PivotPoint);
                if (dist.CompareTo(castedNode.LowerBounds[1]) <= 0)
                {
                    doSearch(queryPoint, castedNode.LeftNode, resultSet);
                }
                else if (dist.CompareTo(castedNode.UpperBounds[0]) <= 0)
                {
                    doSearch(queryPoint, castedNode.RightNode, resultSet);
                }
            }
        }
    }
}
