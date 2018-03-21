using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int height = 20;
    public int width = 20;
    int[,] InitialSpace;
    int[,] split3;
    int[,] split4;
    bool widthIsOdd;

    System.Text.StringBuilder sb = new System.Text.StringBuilder();
    void Start()
    {
        InitialSpace = new int[width, height];
        if (width % 2 == 1) widthIsOdd = true;
        Debug.Log(width / 2);
        Split(out split3, out split4);
        PrintSplit(split3);
        PrintSplit(split4);
        MakeRooms(split3, split4);
        ConnectRooms();
        TestPrint();
    }

    void Split(out int[,] split1, out int[,] split2)
    {
        if (widthIsOdd)
        {
            split1 = new int[width / 2, height];
            split2 = new int[(width + 1) / 2, height];
        }
        else
        {
            split1 = new int[width / 2, height];
            split2 = new int[width / 2, height];
        }
        for (int x = 0; x <= split1.GetLength(0) - 1; x++)
        {
            for (int y = 0; y <= split1.GetLength(1) - 1; y++)
            {
                if (!(y >= split1.GetLength(1) - 1) && x > 0 && !(x >= split1.GetLength(0) - 1) && y > 0)
                {
                    split1[x, y] = 1;
                }
            }
        }
        for (int x = 0; x <= split2.GetLength(0) - 1; x++)
        {
            for (int y = 0; y <= split2.GetLength(1) - 1; y++)
            {
                if (!(y >= split2.GetLength(1) - 1) && x != 0 && !(x >= split2.GetLength(0) - 1) && y != 0)
                {
                    split2[x, y] = 1;
                }
            }
        }
    }
    void MakeRooms(int[,] split1, int[,] split2)
    {
        //for (int x = 0; x <= split1.GetLength(0) - 1; x++)
        //{
        //    for (int y = 0; y <= split1.GetLength(1) - 1; y++)
        //    {
        //        if (!(y >= split1.GetLength(1) - 1) && x > 0 && !(x >= split1.GetLength(0) - 1) && y > 0)
        //        {
        //            split1[x, y] = 1;
        //        }
        //    }
        //}
        //for (int x = 0; x <= split2.GetLength(0) - 1; x++)
        //{
        //    for (int y = 0; y <= split2.GetLength(1) - 1; y++)
        //    {
        //        if (!(y >= split2.GetLength(1) - 1) && x != 0 && !(x >= split2.GetLength(0) - 1) && y != 0)
        //        {
        //            split2[x, y] = 1;
        //        }
        //    }
        //}
        for (int x = 0; x <= InitialSpace.GetLength(0) - 1; x++)
        {
            for (int y = 0; y <= InitialSpace.GetLength(1) - 1; y++)
            {
                if (x < width / 2)
                {
                    InitialSpace[x, y] = split1[x, y];
                }
                else if (x > width / 2)
                {
                    if (widthIsOdd)
                    {
                        InitialSpace[x, y] = split2[x - ((width - 1) / 2), y];
                    }
                    else
                    {
                        InitialSpace[x, y] = split2[x - (width / 2), y];
                    }
                }
            }
        }
    }

    void ConnectRooms()
    {
        if (widthIsOdd)
        {
            InitialSpace[width / 2, height / 2 - 1] = 2;
            InitialSpace[width / 2 - 1, height / 2 - 1] = 2;
        }
        else
        {
            InitialSpace[width / 2, height / 2 - 1] = 2;
            InitialSpace[width / 2 - 1, height / 2 - 1] = 2;
        }
    }
    void PrintSplit(int[,] split)
    {
        string s = "";
        Debug.Log("Split:");
        for (int y = 0; y <= split.GetLength(1) - 1; y++)
        {
            for (int x = 0; x <= split.GetLength(0) - 1; x++)
            {
                if (split[x, y] == 0)
                {
                    s += "E";
                }
                else if (split[x, y] == 1)
                {
                    s += "R";
                }
            }
            s += "\n";
        }
        Debug.Log(s);
    }
    void TestPrint()
    {
        for (int y = 0; y <= InitialSpace.GetLength(1) - 1; y++)
        {
            for (int x = 0; x <= InitialSpace.GetLength(0) - 1; x++)
            {
                if (InitialSpace[x, y] == 0)
                {
                    sb.Append("E");
                }
                else if (InitialSpace[x, y] == 1)
                {
                    sb.Append("R");
                }
                else if (InitialSpace[x, y] == 2)
                {
                    sb.Append("C");
                }
            }
            sb.Append("\n");
        }
        Debug.Log(sb.ToString());
    }
}
