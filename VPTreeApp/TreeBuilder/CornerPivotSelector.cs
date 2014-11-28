using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPTreeApp.Distance;

namespace VPTreeApp.TreeBuilder
{
    public class CornerPivotSelector<T, I> : IPivotSelector<T, I>
        where T : IComparable<T>
        where I : IComparable<I>
    {
        private int sampleSizeOfPivots;
        private int sampleSizeOfData;
        private T initSpread;
        private DistanceAssigner<T, I> distanceAssigner;
        private Func<T, T, T> differ;

        public CornerPivotSelector(int sampleSizeOfPivots, int sampleSizeOfData, T initSpread,
            DistanceAssigner<T, I> distanceAssigner, Func<T, T, T> differ)
        {
            this.sampleSizeOfPivots = sampleSizeOfPivots;
            this.sampleSizeOfData = sampleSizeOfData;
            this.initSpread = initSpread;
            this.distanceAssigner = distanceAssigner;
            this.differ = differ;
        }

        public I selectPivot(List<I> inputData)
        {
            List<I> pivotPoints = createSample(inputData, sampleSizeOfPivots);
            I bestPivot = pivotPoints[0];
            T bestSpread = initSpread;
            foreach (var pivotPoint in pivotPoints)
            {
                List<I> dataPoints = createSample(inputData, sampleSizeOfData);
                List<T> distances = distanceAssigner.assignDistances(inputData, pivotPoint);
                distances.Sort();
                T median = distances[distances.Count / 2];
                T spread = determineMedianAbsoluteDeviation(distances, median);
                if (bestSpread.CompareTo(spread) < 0)
                {
                    bestSpread = spread;
                    bestPivot = pivotPoint;
                }
            }
            return bestPivot;
        }

        public List<I> createSample(List<I> inputData, int sampleSize)
        {
            List<I> dataList = inputData.ToList();
            List<I> resultSample = new List<I>();
            I data;
            for (int idx = 0; idx < Math.Min(sampleSize, inputData.Count); idx++)
            {
                data = dataList[RandomWrapper.rnd.Next(dataList.Count)];
                resultSample.Add(data);
                dataList.Remove(data);
            }
            return resultSample;
        }
        private T determineMedianAbsoluteDeviation(List<T> distances, T median)
        {
            List<T> diffList = new List<T>();
            foreach (var distance in distances)
            {
                diffList.Add(differ(distance, median));
            }
            diffList.Sort();
            return diffList[diffList.Count / 2];
        }
    }
}
