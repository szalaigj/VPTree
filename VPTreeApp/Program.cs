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
            IBuilder<int, string> builder = new DefaultBuilder<int, string>(distance);
            List<string> inputData = loader.loadData(args[0]);
            //int dist = distance.calculateDistance("ATAGCCT", "ACATC");
            Tree tree = builder.buildTree(inputData);
        }
    }
}
