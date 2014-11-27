using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPTreeApp.Algorithm;
using VPTreeApp.Distance;
using VPTreeApp.IOHandler;
using VPTreeApp.TreeBuilder;

namespace VPTreeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataLoader loader = new DataLoader();
            IDistance<int, string> distance = new LevenshteinDistance();
            IPivotSelector<string> selectionStrategy = new SimplePivotSelector<string>();
            IBuilder<int, string> builder = new DefaultBuilder<int, string>(distance, selectionStrategy);
            List<string> inputData = loader.loadData(args[0]);
            //int dist = distance.calculateDistance("ATGTA", "GCGC");
            Tree<int, string> tree = builder.buildTree(inputData);
            tree.printTree();
            ExactMatchSeeker<int, string> exactMatchSeeker = new ExactMatchSeeker<int, string>(distance);
            Console.WriteLine("Give the query point:");
            string queryPoint = Console.ReadLine();
            bool result = exactMatchSeeker.search(queryPoint, tree.Root);
            Console.WriteLine("The query point is contained by the tree? : {0}", result);
            Console.WriteLine("Give the query point:");
            queryPoint = Console.ReadLine();
            Console.WriteLine("Give the max distance from query point:");
            int maxDist = Int32.Parse(Console.ReadLine());
            RangeSeeker<int, string> rangeSeeker = new RangeSeeker<int, string>(distance, (a, b) => a - b,
                (a, b) => { int maxAB = Math.Max(a, b); return Math.Max(maxAB, 0); });
            List<string> resultSet = rangeSeeker.search(queryPoint, maxDist, tree.Root);
            Console.WriteLine("The result set:");
            foreach (var item in resultSet)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Give the query point:");
            queryPoint = Console.ReadLine();
            Console.WriteLine("Give the number of neighbours of query point which you would like to find:");
            int kNN = Int32.Parse(Console.ReadLine());
            KNNSeeker<int, string> kNNSeeker = new KNNSeeker<int, string>(distance, (a, b) => a - b);
            List<KeyValuePair<int, string>> resultDict = kNNSeeker.search(queryPoint, kNN, tree.Root, 10000);
            int cnt = 1;
            foreach (KeyValuePair<int, string> p in resultDict)
            {
                Console.WriteLine("{0}. neighbour: {1} with distance {2}", cnt, p.Value, p.Key);
                cnt++;
            }
            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }
}
