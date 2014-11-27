using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.TreeBuilder
{
    class LeafNode<T, I> : INode<T, I>
        where T : IComparable<T>
    {
        private I data;

        public I Data
        {
            get
            {
                return data;
            }
        }

        public LeafNode(I data)
        {
            this.data = data;
        }

        public void print(int level, string direction)
        {
            Console.WriteLine("Level {0} - {1}", level, direction);
            string tabs = new String('\t', level);
            Console.WriteLine("{0}Data: {1}", tabs, data);
        }
    }
}
