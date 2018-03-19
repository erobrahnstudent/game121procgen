using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    int height = 20;
    int width = 20;
    int[,] InitialSpace;
    int[,] split3;
    int[,] split4;

    string printstr = "";
	void Start () {
        InitialSpace = new int[width, height];
        Split(out split3, out split4);
        MakeRooms(split3, split4);
        ConnectRooms();
        TestPrint();

	}
	
	void Split(out int[,] split1, out int[,] split2)
    {
        split1 = new int[width / 2, height];
        split2 = new int[width / 2, height];
    }

    void MakeRooms(int[,] split1, int[,] split2)
    {
        for (int x = 0; x <= split1.GetLength(0) - 1; x++)
        {
            for (int y = 0; y <= split1.GetLength(1) - 1; y++)
            {
                if (!(y >= 19) && x != 0 && !(x >= 9) && y != 0)
                {
                    split1[x, y] = 1;
                }
            }
        }
        for (int x = 0; x <= split2.GetLength(0) - 1; x++)
        {
            for (int y = 0; y <= split2.GetLength(1) - 1; y++)
            {
                if (!(y >= 19) && x != 0 && !(x >= 9) && y != 0)
                {
                    split2[x, y] = 1;
                }
            }
        }
        for (int x = 0; x <= InitialSpace.GetLength(0) - 1; x++)
        {
            for (int y = 0; y <= InitialSpace.GetLength(1) - 1; y++)
            {
                if (x < 10)
                {
                    InitialSpace[x, y] = split1[x, y];
                }
                else if (x == 10)
                {
                    //meh, do nothing.
                }
                else if (x > 10)
                {
                    InitialSpace[x, y] = split2[x - 10, y];
                }
            }
        }
    }

    void ConnectRooms()
    {
        InitialSpace[10, 10] = 1;
        InitialSpace[9, 10] = 1;
        //InitialSpace[10, 9] = 1;
        //InitialSpace[9, 9] = 1;
    }

    void TestPrint()
    {
        for (int y = 0; y <= InitialSpace.GetLength(1) - 1; y++)
        {
            for (int x = 0; x <= InitialSpace.GetLength(0) - 1; x++)
            {
                printstr += InitialSpace[x, y];
            }
            printstr += "\n";
        }
        Debug.Log(printstr);
    }
}
