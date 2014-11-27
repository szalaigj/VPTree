using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.TreeBuilder
{
    public class Tree<T, I>
        where T : IComparable<T>
        where I : IComparable<I>
    {
        private INode<T, I> root;

        public INode<T, I> Root
        { 
            get 
            { 
                return root; 
            } 
        }

        public Tree(INode<T, I> root)
        {
            this.root = root;
        }

        public void printTree()
        {
            Console.WriteLine("Printing the tree...");
            root.print(0, "");
        }
    }
}
