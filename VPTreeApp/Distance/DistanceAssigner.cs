using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.Distance
{
    public class DistanceAssigner<T, I>
        where T : IComparable<T>
        where I : IComparable<I>
    {
        private IDistance<T, I> distance;

        public DistanceAssigner(IDistance<T, I> distance)
        {
            this.distance = distance;
        }

        public List<T> assignDistances(List<I> inputData, I pivotPoint)
        {
            List<T> distances = new List<T>();
            foreach (var item in inputData)
            {
                T currentDist = distance.calculateDistance(pivotPoint, item);
                distances.Add(currentDist);            }
            return distances;
        }

        public Dictionary<T, List<I>> assignDistancesToData(List<I> inputData, I pivotPoint)
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
    }
}
