using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets;

public class TreeRectDebug : MonoBehaviour
{
    public int levelWidth;
    public int levelHeight;
    public int cycles;
    // Use this for initialization
    void Start()
    {
        BinaryTree<RectInt> sampleRectTree = new BinaryTree<RectInt>(new RectInt(0, 0, levelWidth, levelHeight));

        IterateOnTree(sampleRectTree, cycles, true);
        List<BinaryTreeNode<RectInt>> leaves = new List<BinaryTreeNode<RectInt>>();
        CollectLeaves(sampleRectTree.Root(), leaves);
        List<BinaryTreeNode<RectInt>> rooms = new List<BinaryTreeNode<RectInt>>();
        MakeRooms(leaves, rooms);
        int[,] output = new int[levelWidth, levelHeight];
        //int change = 1;
        //foreach (BinaryTreeNode<RectInt> node in leaves)
        //{
        //    for (int x = 0; x < output.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < output.GetLength(1); y++)
        //        {
        //            if (NodeRectWorld(node).Contains(new Vector2Int(x, y)))
        //            {
        //                output[x, y] = 0;
        //            }
        //        }
        //    }
        //    //change++;
        //    //print("Partition: " + node.Value() + " at WorldRect: " + NodeRectWorld(node));
        //}
        foreach (BinaryTreeNode<RectInt> node in rooms)
        {
            for (int x = 0; x < output.GetLength(0); x++)
            {
                for (int y = 0; y < output.GetLength(1); y++)
                {
                    if (NodeRectWorld(node).Contains(new Vector2Int(x, y)))
                    {
                        output[x, y] = 1;
                    }
                }
            }
        }
        TestPrint(output);
    }

    void MakeRooms(List<BinaryTreeNode<RectInt>> leaves, List<BinaryTreeNode<RectInt>> rooms)
    {
        foreach (BinaryTreeNode<RectInt> leaf in leaves)
        {
            RectInt basenode = leaf.Value();
            RectInt rct = new RectInt(1, 1, basenode.width - 2, basenode.height - 2);
            BinaryTreeNode<RectInt> ract = new BinaryTreeNode<RectInt>(rct);
            ract.parent = leaf;
            leaf.room = ract;
            rooms.Add(ract);
        }
    }

    void TestPrint(int[,] output)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int y = 0; y < output.GetLength(1); y++)
        {
            for (int x = 0; x < output.GetLength(0); x++)
            {
                if (output[x, y] == 1)
                {
                    sb.Append(" F");
                }
                else
                {
                    sb.Append(" E");
                }
            }
            sb.AppendLine("");
        }
        File.WriteAllText("C:\\Temp\\Debug.txt", sb.ToString());
        Debug.Log(sb.ToString());
    }



    private void IterateOnTree(BinaryTree<RectInt> tree, int times, bool startingsplit)
    {
        List<BinaryTreeNode<RectInt>> leaves = new List<BinaryTreeNode<RectInt>>();
        for (int x = 0; x < times; x++)
        {
            CollectLeaves(tree.Root(), leaves);
            foreach (BinaryTreeNode<RectInt> node in leaves)
            {
                rectSplit(node, startingsplit);
            }
            if (startingsplit) startingsplit = false;
            else startingsplit = true;
            leaves.Clear();
        }
    }

    private void CollectLeaves(BinaryTreeNode<RectInt> currentNode, List<BinaryTreeNode<RectInt>> leaves)
    {
        if (currentNode == null) return;

        if (currentNode.isLeaf())
        {
            leaves.Add(currentNode);
            return;
        }
        else
        {
            CollectLeaves(currentNode.leftChild, leaves);
            CollectLeaves(currentNode.rightChild, leaves);
        }
    }

    private void rectSplit(BinaryTreeNode<RectInt> basenode, bool VertOrHoriz)
    {
        RectInt split1 = new RectInt();
        RectInt split2 = new RectInt();
        RectInt basenodeRect = basenode.Value();
        // true = vertical, false = horizontal
        if (VertOrHoriz == true)
        {
            split1 = new RectInt(0, 0, basenodeRect.width / 2, basenodeRect.height);
            split2 = new RectInt(basenodeRect.width / 2, 0, basenodeRect.width / 2, basenodeRect.height);
        }
        else
        {
            split1 = new RectInt(0, 0, basenodeRect.width, basenodeRect.height / 2);
            split2 = new RectInt(0, basenodeRect.height / 2, basenodeRect.width, basenodeRect.height / 2);
        }

        basenode.AddChild(split1);
        basenode.AddChild(split2);
    }

    private RectInt NodeRectWorld(BinaryTreeNode<RectInt> node)
    {
        BinaryTreeNode<RectInt> current = node;
        RectInt rectWorld = node.Value();
        rectWorld.x = 0;
        rectWorld.y = 0;
        while (current != null)
        {
            rectWorld.x += current.Value().x;
            rectWorld.y += current.Value().y;

            current = current.parent;
        }
        return rectWorld;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
