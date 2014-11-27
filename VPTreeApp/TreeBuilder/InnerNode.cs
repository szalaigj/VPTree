﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.TreeBuilder
{
    public class InnerNode<T, I> : INode<T, I>
        where T : IComparable<T>
    {
        private I pivotPoint;
        private T[] lowerBounds;
        private T[] upperBounds;
        private INode<T, I> leftNode;
        private INode<T, I> rightNode;

        public I PivotPoint
        {
            get { return pivotPoint; }
        }

        public T[] LowerBounds
        {
            get { return lowerBounds; }
        }

        public T[] UpperBounds
        {
            get { return upperBounds; }
        }

        public InnerNode(I pivotPoint, T lowerBoundLo, T lowerBoundHi,
            T upperBoundLo, T upperBoundHi, INode<T, I> leftNode, INode<T, I> rightNode)
        {
            InitializeMembers(pivotPoint, new T[] { lowerBoundLo, lowerBoundHi },
                new T[] { upperBoundLo, upperBoundHi }, leftNode, rightNode);
        }

        private void InitializeMembers(I pivotPoint, T[] lowerBounds, T[] upperBounds,
            INode<T, I> leftNode, INode<T, I> rightNode)
        {
            this.pivotPoint = pivotPoint;
            this.lowerBounds = lowerBounds;
            this.upperBounds = upperBounds;
            this.leftNode = leftNode;
            this.rightNode = rightNode;
        }

        public void print(int level, string direction)
        {
            Console.WriteLine("Level {0} - {1}", level, direction);
            string tabs = new String('\t', level);
            Console.WriteLine("{0}Pivot point: {1}", tabs, pivotPoint);
            if (leftNode != null)
            {
                leftNode.print(level + 1, direction + "left - ");    
            }
            if (rightNode != null)
            {
                rightNode.print(level + 1, direction + "right - ");    
            }
        }
    }
}
