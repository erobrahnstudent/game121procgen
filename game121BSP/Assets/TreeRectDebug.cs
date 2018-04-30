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

        IterateOnTree(sampleRectTree, cycles, false);
        List<BinaryTreeNode<RectInt>> leaves = new List<BinaryTreeNode<RectInt>>();
        CollectLeaves(sampleRectTree.Root(), leaves);
        List<BinaryTreeNode<RectInt>> rooms = new List<BinaryTreeNode<RectInt>>();
        MakeRooms(leaves, rooms);
        List<BinaryTreeNode<RectInt>> nodes = new List<BinaryTreeNode<RectInt>>();
        collectNodes(rooms, nodes);
        List<BinaryTreeNode<RectInt>> corridors = new List<BinaryTreeNode<RectInt>>();
        List<RectInt> cor = new List<RectInt>();
        // makeConnections(nodes, corridors);
        makeConnections(nodes, cor);
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
        //foreach (BinaryTreeNode<RectInt> corr in corridors)
        //{
        //    for (int x = 0; x < output.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < output.GetLength(1); y++)
        //        {
        //            RectInt rct = NodeRectWorld(corr);
        //            if (rct.Contains(new Vector2Int(x, y)))
        //            {
        //                output[x, y] = 2;
        //            }
        //        }
        //    }
        //}
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
            leaf.rightChild = ract;
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
                else if (output[x, y] == 2)
                {
                    sb.Append(" C");
                }
                else if (output[x, y] == 0)
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

        if (currentNode.leftChild != null)
        {
            CollectLeaves(currentNode.leftChild, leaves);
        }
        if (currentNode.rightChild != null)
        {
            CollectLeaves(currentNode.rightChild, leaves);
        }
        if (currentNode.rightChild == null && currentNode.leftChild == null)
        {
            leaves.Add(currentNode);
        }
    }
    
    private void collectNodes(List<BinaryTreeNode<RectInt>> rooms, List<BinaryTreeNode<RectInt>> leaves)
    {
        foreach (BinaryTreeNode<RectInt> room in rooms)
        {
            if (!leaves.Contains(room.parent.parent))
            {
                leaves.Add(room.parent.parent);
            }
        }
    }

    private void getNextLevelUp(List<BinaryTreeNode<RectInt>> nodes, List<BinaryTreeNode<RectInt>> branches)
    {
        foreach (BinaryTreeNode<RectInt> node in nodes)
        {
            if (node.parent != null)
            {
                if (!branches.Contains(node.parent))
                {
                    branches.Add(node.parent);
                }
            }
        }
    }

    private void makeConnections(List<BinaryTreeNode<RectInt>> nodes, List<BinaryTreeNode<RectInt>> corridors)
    {
        List<BinaryTreeNode<RectInt>> nx = nodes;
        for (int x = 0; x < nodes.Count; x++)
        {
            RectInt room1 = NodeRectWorld(nodes[x].leftChild.rightChild);
            RectInt room2 = NodeRectWorld(nodes[x].rightChild.rightChild);

            //if (room1.y == room2.y)
            //{
            int corridorStart = nodes[x].Value().height / 2;
            nodes[x].leftChild = new BinaryTreeNode<RectInt>(new RectInt(nodes[x].leftChild.Value().width - 1, corridorStart, 2, 1));
            nodes[x].leftChild.parent = nodes[x];
            corridors.Add(nodes[x].leftChild);
            //}
        }
        while (nx.Count > 1) // because it'll BE one when we're at root
        {
            List<BinaryTreeNode<RectInt>> nx2 = nx;
            nx.Clear();
            getNextLevelUp(nx2, nx);
        }
    }

    void makeConnections(List<BinaryTreeNode<RectInt>> nodes, List<RectInt> corridors)
    {
        int iter = 0;
        while (iter < cycles)
        {
            foreach (BinaryTreeNode<RectInt> node in nodes)
            {
                if (node.leftChild.Value().y == node.rightChild.Value().y)
                {

                }
                else if (node.leftChild.Value().x == node.rightChild.Value().x)
                {

                }
            }
            List<BinaryTreeNode<RectInt>> x2 = nodes;
            nodes.Clear();
            getNextLevelUp(x2, nodes);
            iter++;
        }
    }

    List<int> getPossibilites(BinaryTreeNode<RectInt> node, List<RectInt> part1, List<RectInt> part2, bool axis)
    {
        List<int> possible = new List<int>();

        if(axis == true)
        {
            // vert
            for (int x = 0; x < node.Value().width; x++)
            {

            }
        }
        if(axis == false)
        {
            // horiz
            for (int y = 0; y < node.Value().height; y++)
            {

            }
        }

        return possible;
    }

    void getRooms(BinaryTreeNode<RectInt> node, List<RectInt> rooms)
    {
        if (node.rightChild != null && node.leftChild == null)
        {
            rooms.Add(node.leftChild.Value());
        }
        else if (node.rightChild != null && node.leftChild != null)
        {
            getRooms(node.leftChild, rooms);
            getRooms(node.rightChild, rooms);
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
