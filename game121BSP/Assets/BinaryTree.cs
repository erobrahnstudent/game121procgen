using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class BinaryTree<T>
    {
        private BinaryTreeNode<T> root;
        public BinaryTreeNode<T> Root()
        {
            return root;
        }


        //public BinaryTree(BinaryTreeNode<T> rootNode)
        //{
        //    root = rootNode;
        //}
        public BinaryTree(T rootValue)
        {
            this.root = new BinaryTreeNode<T>(rootValue);
        }
    }

    public class BinaryTreeNode<T>
    {
        public BinaryTreeNode<T> parent;
        public BinaryTreeNode<T> leftChild;
        public BinaryTreeNode<T> rightChild;
        //public BinaryTreeNode<T> room;

        private T innerValue;
        public T Value()
        {
            return innerValue;
        }

        //public bool isLeaf()
        //{
        //    return leftChild == null && rightChild == null;
        //}

        public BinaryTreeNode(T nodeValue)
        {
            this.innerValue = nodeValue;
        }

        public BinaryTreeNode<T> AddChild(T childValue)
        {

            if (leftChild == null)
            {
                leftChild = new BinaryTreeNode<T>(childValue);
                leftChild.parent = this;
                return leftChild;
            }
            if (rightChild == null)
            {
                rightChild = new BinaryTreeNode<T>(childValue);
                rightChild.parent = this;
                return rightChild;
            }

            throw new InvalidOperationException("Cannot add more than two children to a binary node.");
        }
    }
}
