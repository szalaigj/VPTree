using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //int dist = distance.calculateDistance("ATAGCCT", "ACATC");
            Tree<int, string> tree = builder.buildTree(inputData);
            tree.printTree();
            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }
}
