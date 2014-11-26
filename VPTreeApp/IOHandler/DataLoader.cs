using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.IOHandler
{
    public class DataLoader
    {
        public List<string> loadData(string inputFileName)
        {
            string line;
            List<string> result = new List<string>();
            Console.WriteLine("Now loading data file {0}...", inputFileName);
            using (StreamReader sr = new StreamReader(inputFileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }
            return result;
        }
    }
}
