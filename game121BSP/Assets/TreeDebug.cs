using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class TreeDebug : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        BinaryTree<int> sampleTree = new BinaryTree<int>(42);

        BinaryTreeNode<int> left = sampleTree.Root().AddChild(5);
        BinaryTreeNode<int> right = sampleTree.Root().AddChild(17);

        left.AddChild(-6);
        left.AddChild(12);

        right.AddChild(128);
        right.AddChild(1024);

        BinaryTreeNode<int> treeRoot = sampleTree.Root();
        List<BinaryTreeNode<int>> leaves = new List<BinaryTreeNode<int>>();
        //CollectLeaves(treeRoot, leaves);
        int sum = 0;
        foreach(BinaryTreeNode<int> leaf in leaves)
        {
            print("Leaf discovered with value " + leaf.Value() + " and parent value " + leaf.parent.Value());
            print("Sum to root is: " + CountFromNodeToRoot(leaf));
        }
        print(sum);

        //int leftofLeftSum = CountFromNodeToRoot(leftofLeft);
        //int rightofLeftSum = CountFromNodeToRoot(rightofLeft);
        //int leftofRightSum = CountFromNodeToRoot(leftofRight);
        //int rightofRightSum = CountFromNodeToRoot(rightofRight);

        //print("leftofLeftSum = " + leftofLeftSum);
        //print("rightofLeftSum = " + rightofLeftSum);
        //print("leftofRightSum = " + leftofRightSum);
        //print("rightofRightSum = " + rightofRightSum);

    }

    void TestPrint(int[,] output)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int y = 0; y < output.GetLength(1); y++)
        {
            for (int x = 0; x < output.GetLength(0); x++)
            {
                sb.Append(output[x, y]);
            }
            sb.Append("\n");
        }
        Debug.Log(sb.ToString());
    }

    //private void CollectLeaves(BinaryTreeNode<int> currentNode, List<BinaryTreeNode<int>> leaves)
    //{
    //    if (currentNode == null) return;

    //    if (currentNode.isLeaf())
    //    {
    //        leaves.Add(currentNode);
    //        return;
    //    }
    //    else
    //    {
    //        CollectLeaves(currentNode.leftChild, leaves);
    //        CollectLeaves(currentNode.rightChild, leaves);
    //    }
    //}

    private int CountFromNodeToRoot(BinaryTreeNode<int> node)
    {
        int sum = 0;
        BinaryTreeNode<int> current = node;
        while (current != null)
        {
            sum += current.Value();
            current = current.parent;
        }
        return sum;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
