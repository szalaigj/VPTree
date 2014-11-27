using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPTreeApp.Distance;
using VPTreeApp.TreeBuilder;

namespace VPTreeApp.Algorithm
{
    public class KNNSeeker<T, I>
        where T : IComparable<T>
        where I : IComparable<I>
    {
        private IDistance<T, I> distance;
        private Func<T, T, T> subtract;

        public KNNSeeker(IDistance<T, I> distance, Func<T, T, T> subtract)
        {
            this.distance = distance;
            this.subtract = subtract;
        }

        public SortedDictionary<T, List<I>> search(I queryPoint, int k, INode<T, I> root, T maximalElementInT)
        {
            SortedDictionary<T, List<I>> resultDict = new SortedDictionary<T, List<I>>();
            T sigma = maximalElementInT;
            doSearch(queryPoint, k, root, sigma, resultDict);
            return resultDict;
        }

        private T doSearch(I queryPoint, int k, INode<T, I> node, T sigma, SortedDictionary<T, List<I>> resultDict)
        {
            if (node.GetType() == typeof(InnerNode<T, I>))
            {
                sigma = doSearchInnerNode(queryPoint, k, node, sigma, resultDict);
            }
            else if (node.GetType() == typeof(LeafNode<T, I>))
            {
                sigma = doSearchLeafNode(queryPoint, k, node, sigma, resultDict);
            }
            return sigma;
        }

        private T doSearchInnerNode(I queryPoint, int k, INode<T, I> node, T sigma, SortedDictionary<T, List<I>> resultDict)
        {
            InnerNode<T, I> castedNode = (InnerNode<T, I>)node;
            T dist = distance.calculateDistance(queryPoint, castedNode.PivotPoint);
            sigma = increaseResultDict(queryPoint, k, sigma, resultDict, castedNode.PivotPoint, dist);
            T d1 = this.subtract(dist, sigma);
            if (d1.CompareTo(castedNode.LowerBounds[1]) <= 0)
            {
                doSearch(queryPoint, k, castedNode.LeftNode, sigma, resultDict);
            }
            T d2 = this.subtract(castedNode.UpperBounds[0], sigma);
            if (dist.CompareTo(d2) <= 0)
            {
                doSearch(queryPoint, k, castedNode.RightNode, sigma, resultDict);
            }
            return sigma;
        }

        private T doSearchLeafNode(I queryPoint, int k, INode<T, I> node, T sigma, SortedDictionary<T, List<I>> resultDict)
        {
            LeafNode<T, I> castedNode = (LeafNode<T, I>)node;
            T dist = distance.calculateDistance(queryPoint, castedNode.Data);
            sigma = increaseResultDict(queryPoint, k, sigma, resultDict, castedNode.Data, dist);
            return sigma;
        }

        private T increaseResultDict(I queryPoint, int k, T sigma, SortedDictionary<T, List<I>> resultDict, I newHit, T currentDist)
        {
            if (currentDist.CompareTo(sigma) <= 0)
            {
                List<I> currentListValue;
                if (resultDict.TryGetValue(currentDist, out currentListValue))
                {
                    currentListValue.Add(newHit);
                }
                else
                {
                    currentListValue = new List<I>() { newHit };
                    resultDict[currentDist] = currentListValue;
                }
                //int cnt = 0;
                //SortedDictionary<T, List<I>> tempDict = resultDict;
                //foreach (var key in resultDict.Keys)
                //{
                //    if (cnt > k)
                //    {
                //        tempDict.Remove(key);
                //    }
                //    cnt += resultDict[key].Count;
                //}
                if (resultDict.Count > k)
                {
                    List<T> sortedKeys = resultDict.Keys.ToList();
                    for (int idx = k; idx < sortedKeys.Count; idx++)
                    {
                        resultDict.Remove(sortedKeys[idx]);
                    }
                }
                if (resultDict.Count == k)
                {
                    sigma = resultDict.Keys.ToList()[k - 1];
                }
            }
            return sigma;
        }


    }
}
