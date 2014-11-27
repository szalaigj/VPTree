using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPTreeApp.Distance;

namespace VPTreeApp.TreeBuilder
{
    public class DefaultBuilder<T, I> : IBuilder<T, I>
        where T : IComparable<T>
        where I : IComparable<I>
    {
        private IDistance<T, I> distance;
        private IPivotSelector<I> selectionStrategy;
        
        public DefaultBuilder(IDistance<T, I> distance, IPivotSelector<I> selectionStrategy)
        {
            this.distance = distance;
            this.selectionStrategy = selectionStrategy;
        }

        public Tree<T, I> buildTree(List<I> inputData)
        {
            INode<T, I> root = doBuildTree(inputData);
            Tree<T, I> result = new Tree<T, I>(root);
            return result;
        }

        private INode<T, I> doBuildTree(List<I> inputData)
        {
            INode<T, I> result;
            if (inputData.Count == 1)
            {
                result = new LeafNode<T, I>(inputData[0]);
            }
            else if (inputData.Count > 1)
            {
                I pivotPoint = selectionStrategy.selectPivot(inputData);
                Dictionary<T, List<I>> distanceToData = assignDistancesToData(inputData, pivotPoint);
                List<T> sortedKeys = distanceToData.Keys.ToList<T>();
                sortedKeys.Sort();
                int medianIdx = sortedKeys.Count / 2;
                List<I> leftInputData = determineDataForSubTree(distanceToData, sortedKeys, 0, medianIdx);
                List<I> rightInputData = determineDataForSubTree(distanceToData, sortedKeys,
                    medianIdx + 1, sortedKeys.Count - 1);
                INode<T, I> leftNode = doBuildTree(leftInputData);
                INode<T, I> rightNode = doBuildTree(rightInputData);
                T lowerBoundLo = sortedKeys[0];
                T lowerBoundHi = sortedKeys[medianIdx];
                T upperBoundLo = (rightNode == null) ? sortedKeys[0] : sortedKeys[medianIdx + 1];
                T upperBoundHi = sortedKeys[sortedKeys.Count - 1];
                result = new InnerNode<T, I>(pivotPoint, lowerBoundLo, lowerBoundHi,
                    upperBoundLo, upperBoundHi, leftNode, rightNode);
            }
            else
            {
                // When the inputData is empty because the process is in the leaf level.
                result = null;
            }
            return result;
        }

        private Dictionary<T, List<I>> assignDistancesToData(List<I> inputData, I pivotPoint)
        {
            Dictionary<T, List<I>> distanceToData = new Dictionary<T, List<I>>();
            foreach (var item in inputData)
            {
                T currentDist = distance.calculateDistance(pivotPoint, item);
                List<I> currentListValue;
                if (distanceToData.TryGetValue(currentDist, out currentListValue))
                {
                    currentListValue.Add(item);
                }
                else
                {
                    currentListValue = new List<I>() { item };
                    distanceToData[currentDist] = currentListValue;
                }
            }
            return distanceToData;
        }

        private static List<I> determineDataForSubTree(Dictionary<T, List<I>> distanceToData, 
            List<T> sortedKeys, int startIdx, int endIdx)
        {
            List<I> result = new List<I>();
            for (int idx = startIdx; idx <= endIdx; idx++)
            {
                foreach (var item in distanceToData[sortedKeys[idx]])
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
