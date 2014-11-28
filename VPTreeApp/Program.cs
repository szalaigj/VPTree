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
            DistanceAssigner<int, string> distanceAssigner = new DistanceAssigner<int, string>(distance);
            //IPivotSelector<int, string> selectionStrategy = new SimplePivotSelector<int, string>();
            IPivotSelector<int, string> selectionStrategy = new CornerPivotSelector<int, string>(3, 3, 0, 
                distanceAssigner, (a, b) => Math.Abs(a - b));
            IBuilder<int, string> builder = new DefaultBuilder<int, string>(distanceAssigner, selectionStrategy);
            List<string> inputData = loader.loadData(args[0]);
            //int dist = distance.calculateDistance("ATGTA", "GCGC");
            Tree<int, string> tree = builder.buildTree(inputData);
            tree.printTree();
            Console.WriteLine("Please the query type or exit:");
            Console.WriteLine("\t'exact' - exact match query");
            Console.WriteLine("\t'range' - range query");
            Console.WriteLine("\t'knn' - k nearest neighbour query");
            Console.WriteLine("\t'exit' - exit program");
            ExactMatchSeeker<int, string> exactMatchSeeker = new ExactMatchSeeker<int, string>(distance);
            RangeSeeker<int, string> rangeSeeker = new RangeSeeker<int, string>(distance, (a, b) => a - b,
                (a, b) => { int maxAB = Math.Max(a, b); return Math.Max(maxAB, 0); });
            KNNSeeker<int, string> kNNSeeker = new KNNSeeker<int, string>(distance, (a, b) => a - b);
            bool done = false;
            while (!done)
            {
                Console.Write("query type>>>");
                string action = Console.ReadLine();
                switch (action)
                {
                    case "exact":
                        exactMatchQueryCase(exactMatchSeeker, tree.Root);
                        break;
                    case "range":
                        rangeQueryCase(rangeSeeker, tree.Root);
                        break;
                    case "knn":
                        kNNQueryCase(kNNSeeker, tree.Root);
                        break;
                    case "exit":
                        done = true;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }

        private static void exactMatchQueryCase(ExactMatchSeeker<int, string> exactMatchSeeker, INode<int, string> root)
        {
            Console.WriteLine("Please give the query point:");
            string queryPoint = Console.ReadLine();
            bool result = exactMatchSeeker.search(queryPoint, root);
            Console.WriteLine("The query point is contained by the tree? : {0}", result);
        }

        private static void rangeQueryCase(RangeSeeker<int, string> rangeSeeker, INode<int, string> root)
        {
            Console.WriteLine("Please give the query point:");
            string queryPoint = Console.ReadLine();
            Console.WriteLine("Please give the max distance from query point:");
            int maxDist = Int32.Parse(Console.ReadLine());
            List<string> resultSet = rangeSeeker.search(queryPoint, maxDist, root);
            Console.WriteLine("The result set:");
            foreach (var item in resultSet)
            {
                Console.WriteLine(item);
            }
        }

        private static void kNNQueryCase(KNNSeeker<int, string> kNNSeeker, INode<int, string> root)
        {
            Console.WriteLine("Please give the query point:");
            string queryPoint = Console.ReadLine();
            Console.WriteLine("Please give the number of neighbours of query point which you would like to find:");
            int kNN = Int32.Parse(Console.ReadLine());

            List<KeyValuePair<int, string>> resultDict = kNNSeeker.search(queryPoint, kNN, root, 10000);
            int cnt = 1;
            foreach (KeyValuePair<int, string> p in resultDict)
            {
                Console.WriteLine("{0}. neighbour: {1} with distance {2}", cnt, p.Value, p.Key);
                cnt++;
            }
        }
    }
}
